using simpleWCFApp.Models;
using System.Collections.Generic;

namespace simpleWCFApp.Services
{
    public class UserService
    {
        static private UserStore userStore = new UserStore();

        PagingList<User> GetUsers(PagingListOption options)
        {
            return new PagingList<User>(userStore.Users, null, options);
        }
    }
}
