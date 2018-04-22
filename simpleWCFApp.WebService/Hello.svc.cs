using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace simpleWCFApp.WebService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Hello
    {
        [OperationContract]
        [WebInvoke(Method="GET", ResponseFormat=WebMessageFormat.Json, BodyStyle=WebMessageBodyStyle.WrappedRequest, RequestFormat=WebMessageFormat.Json, UriTemplate="hello")]
        public string GetHello()
        {
            return "Hello";
        }
    }
}
