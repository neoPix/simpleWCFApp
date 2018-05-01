using JWT.Algorithms;
using JWT.Builder;
using simpleWCFApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public string Authenticate(string login, string password)
        {
            User dbUser = (from user in userStore.Users where user.Login.Equals(login) select user).FirstOrDefault();
            if (dbUser != null)
            {
                return new JwtBuilder()
                        .WithAlgorithm(new HMACSHA256Algorithm())
                        .WithSecret(Properties.Settings.Default.JWTSecret)
                        .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(4).ToUnixTimeSeconds())
                        .AddClaim("guid", dbUser.Uuid.Value.ToString())
                        .Build();
            }
            return null;
        }

        public User FromToken(string token)
        {
            try
            {
                IDictionary<string, Object> properties = new JwtBuilder()
                         .WithSecret(Properties.Settings.Default.JWTSecret)
                         .MustVerifySignature()
                         .Decode<IDictionary<string, Object>>(token);
                return this.GetSingle(Guid.Parse(properties["guid"].ToString()));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
