using System;
using System.Runtime.Serialization;

namespace simpleWCFApp.Models
{
    [DataContract]
    public class PagingListOption
    {
        public PagingListOption(int page = 1, int limit = 10)
        {
            this.Page = page;
            this.Limit = limit;
        }

        [DataMember(Name="page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
