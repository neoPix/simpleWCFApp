using simpleWCFApp.WebService.Attributes;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace simpleWCFApp.WebService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults=true, InstanceContextMode=InstanceContextMode.PerCall, ConcurrencyMode=ConcurrencyMode.Multiple)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceContractSecurity]
    public class WebService
    {
        private IWebServiceContext context;

        public WebService()
        {
            this.context = new WebServiceContext();
        }

        public IWebServiceContext Context { get { return this.context; } }
    }
}