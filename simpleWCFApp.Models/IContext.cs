using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleWCFApp.Models
{
    public interface IContext
    {
        User CurrentUser { get; set; }
    }
}
