using simpleWCFApp.Models;
using simpleWCFApp.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace simpleWCFApp.WebService
{
    [ServiceContract]
    public class User : WebService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/users")]
        public PagingList<Models.User> GetList()
        {
            try
            {
                UserService userService = new UserService();
                return userService.GetUsers(new PagingListOption());
            }
            catch (Exception e)
            {
                this.Context.ServerError(e.Message);
                return null;
            }
        }

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, UriTemplate = "/users")]
        public Models.User Add(Models.User user)
        {
            try
            {
                UserService userService = new UserService();
                return userService.Add(user);
            }
            catch (Exception e)
            {
                this.Context.ServerError(e.Message);
                return null;
            }
        }

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/users/{id}")]
        public Models.User GetSingle(string id)
        {
            try
            {
                UserService userService = new UserService();
                Models.User user = userService.GetSingle(Guid.Parse(id));
                if (user == null)
                {
                    this.Context.NotFound();
                    return null;
                }
                return user;
            }
            catch (Exception e)
            {
                this.Context.ServerError(e.Message);
                return null;
            }
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/users/{id}")]
        public Models.User Delete(string id)
        {
            try
            {
                UserService userService = new UserService();
                return userService.Delete(Guid.Parse(id));
            }
            catch (Exception e)
            {
                this.Context.ServerError(e.Message);
                return null;
            }
        }

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/users/{id}")]
        public Models.User Update(string id, Models.User user)
        {
            try
            {
                if (Guid.Parse(id).Equals(user.Uuid))
                {
                    UserService userService = new UserService();
                    return userService.Update(user);
                }
                return null;
            }
            catch (Exception e)
            {
                this.Context.ServerError(e.Message);
                return null;
            }
        }

    }
}
