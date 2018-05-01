using System;
using System.Net;
using System.ServiceModel.Web;

namespace simpleWCFApp.WebService
{
    public class WebServiceContext: IWebServiceContext
    {
        private OutgoingWebResponseContext response;

        public WebServiceContext()
        {
            this.response = WebOperationContext.Current.OutgoingResponse;
        }

        public void NotFound(string message = "Not found")
        {
            throw new WebFaultException<string>(message, HttpStatusCode.NotFound);
        }

        public void ServerError(string message = "Server error")
        {
            throw new WebFaultException<string>(message, HttpStatusCode.InternalServerError);
        }

        public void Forbiden(string message = "Cannot access")
        {
            throw new WebFaultException<string>(message, HttpStatusCode.Forbidden);
        }

        public Models.User CurrentUser { get; set; }
    }
}