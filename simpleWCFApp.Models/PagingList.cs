using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace simpleWCFApp.Models
{
    [DataContract]
    public class PagingList<T>
    {
        public PagingList(IEnumerable<T> items = null, int? count = null, PagingListOption options = null)
        {
            this.Items = items;
            if (count.HasValue)
            {
                this.Count = count.Value;
            }
            else
            {
                if (items != null)
                {
                    this.Count = this.Items.Count();
                }
            }
            if (options != null)
            {
                this.Page = options.Page;
                this.Limit = options.Limit;
            }
        }

        [DataMember(Name="items")]
        public IEnumerable<T> Items { get; set; }

        [DataMember(Name = "total")]
        public int Count { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
