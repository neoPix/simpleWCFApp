using simpleWCFApp.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace simpleWCFApp.Services
{
    public class UserService
    {
        static private UserStore userStore = new UserStore();

        public PagingList<User> GetUsers(PagingListOption options)
        {
            return new PagingList<User>(
                userStore.Users
                    .Skip((options.Page - 1) * options.Limit)
                    .Take(options.Limit), 
                userStore.Users.Count(), 
                options
            );
        }

        public User GetSingle(Guid id)
        {
            return (from user in userStore.Users where user.Uuid.HasValue && user.Uuid.Value.Equals(id) select user).FirstOrDefault();
        }

        public User Add(User user)
        {
            user.Uuid = Guid.NewGuid();
            List<User> userList = (List<User>)userStore.Users;
            userList.Add(user);
            return user;
        }

        public User Delete(Guid guid)
        {
            List<User> userList = (List<User>)userStore.Users;
            User user = this.GetSingle(guid);
            userList.Remove(user);
            user.Uuid = null;
            return user;
        }

        public User Update(User user)
        {
            User dbUser = this.GetSingle(user.Uuid.Value);
            dbUser.From(user);
            return dbUser;
        }
    }
}
