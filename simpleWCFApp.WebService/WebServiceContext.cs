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
            this.response.StatusCode = HttpStatusCode.NotFound;
            this.response.StatusDescription = message;
        }

        public void ServerError(string message = "Server error")
        {
            this.response.StatusCode = HttpStatusCode.InternalServerError;
            this.response.StatusDescription = message;
        }

        public void Forbiden(string message = "Cannot access")
        {
            this.response.StatusCode = HttpStatusCode.Forbidden;
            this.response.StatusDescription = message;
        }
        
        public Models.User CurentUser
        {
            get {
                return null;
            }
        }
    }
}