using System;
using System.Runtime.Serialization;

namespace simpleWCFApp.Models
{
    [DataContract]
    public class User
    {
        [DataMember(Name="id", IsRequired=true)]
        public Guid Uuid { get; set; }

        [DataMember(Name="login", IsRequired=true)]
        public string Login { get; set; }

        [IgnoreDataMember]
        public string Password { get; set; }

        [DataMember(Name="userName", IsRequired=true)]
        public string Name { get; set; }

        [DataMember(IsRequired=false, Name="url")]
        public string Url { 
            get {
                return string.Format("users/{0}", this.Uuid.ToString());
            }
        }
    }
}
