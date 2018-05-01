using System.Runtime.Serialization;

namespace simpleWCFApp.Models
{
    [DataContract]
    public enum ACCESS_LEVEL
    {
        [EnumMember(Value = "admin")]
        ADMIN = 2,
        [EnumMember(Value = "user")]
        USER = 1,
        [EnumMember(Value = "public")]
        PUBLIC = 0
    }
}
