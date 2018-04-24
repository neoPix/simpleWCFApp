using simpleWCFApp.Models;

namespace simpleWCFApp.WebService
{
    public interface IWebServiceContext : IContext
    {
        void NotFound(string message = "Not found");
        void ServerError(string message = "Server error");
        void Forbiden(string message = "Cannot access");
    }
}
