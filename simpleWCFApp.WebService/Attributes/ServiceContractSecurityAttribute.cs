using simpleWCFApp.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Linq;
using simpleWCFApp.Models;
using System.ServiceModel.Web;
using System.Net;

namespace simpleWCFApp.WebService.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceContractSecurityAttribute : Attribute, IParameterInspector, IServiceBehavior
    {
        private WebService webService;

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        { }

        public object BeforeCall(string operationName, object[] inputs)
        {
            Object currentWebService = OperationContext.Current.InstanceContext.GetServiceInstance();
            if (currentWebService != null)
            {
                this.webService = currentWebService as WebService;
                MethodInfo method = currentWebService.GetType().GetMethod(operationName);
                this.webService.Context.CurrentUser = this.GetCurrentUser();
                this.Apply(method);
            }
            return null;
        }

        private Models.User GetCurrentUser()
        {
            string token = HttpContext.Current.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token) == false)
            {
                UserService userService = new UserService();
                return userService.FromToken(token);
            }
            return null;
        }

        private void Apply(MethodInfo method)
        {
            if (method != null)
            {
                IEnumerable<Attribute> attributes = Attribute.GetCustomAttributes(method, typeof(OperationAccessAttribute), true);
                IEnumerable<ACCESS_LEVEL> levels = (from attr in attributes select (attr as OperationAccessAttribute).RequiredAccessLevel);
                if (levels.Contains(ACCESS_LEVEL.PUBLIC))
                {
                    return;
                }
                else if (levels.Count((level) => level > this.webService.Context.CurrentUser.Level) > 0)
                {
                    this.webService.Context.Forbiden("You don't have the permissions to do this");
                }
                return;
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                if (channelDispatcher == null)
                {
                    continue;
                }

                foreach (EndpointDispatcher endpoint in channelDispatcher.Endpoints)
                {
                    if (endpoint == null)
                    {
                        continue;
                    }

                    foreach (var operation in endpoint.DispatchRuntime.Operations)
                    {
                        operation.ParameterInspectors.Add(this);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        { }
    }
}