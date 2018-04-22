using System;
using simpleWCFApp.Models;
using System.Collections.Generic;

namespace simpleWCFApp.Services
{
    public class UserStore
    {
        public UserStore()
        {
            this.Users = new List<User>();
        }

        public IEnumerable<User> Users { get; set; }

        public void Populate()
        {
            List<User> list = (List<User>)this.Users;
            list.Add(new User() { 
                Uuid = Guid.NewGuid(),
                Login = "user01",
                Name = "Dylan",
                Password = "123456789"
            });
            for (int i = 50; i-- >= 0; )
            {
                list.Add(new User()
                {
                    Uuid = Guid.NewGuid(),
                    Login = string.Format("user{0}", i),
                    Name = string.Format("Awesome user{0}", i),
                    Password = "123456789"
                });
            }
        }
    }
}
