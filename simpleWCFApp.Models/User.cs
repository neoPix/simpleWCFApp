using System;
using System.Runtime.Serialization;

namespace simpleWCFApp.Models
{
    [DataContract]
    public class User
    {
        public User()
        {
            this.Level = ACCESS_LEVEL.USER;
        }

        [DataMember(Name="id", IsRequired=true)]
        public Guid? Uuid { get; set; }

        [DataMember(Name="login", IsRequired=true)]
        public string Login { get; set; }

        [DataMember(Name = "level", IsRequired = false)]
        public ACCESS_LEVEL Level { get; set; }

        [IgnoreDataMember]
        public string Password { get; set; }

        [DataMember(Name="userName", IsRequired=true)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, Name = "url")]
        public string Url
        {
            get
            {
                return string.Format("/users/{0}", this.Uuid.ToString());
            }
            set
            {
                return;
            }
        }

        public User From(User user)
        {
            this.Name = user.Name;
            this.Login = user.Login;
            return this;
        }
    }
}
