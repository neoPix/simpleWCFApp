using simpleWCFApp.Models;
using System;

namespace simpleWCFApp.WebService.Attributes
{
    public class OperationAccessAttribute : Attribute
    {
        public OperationAccessAttribute()
        {
            this.RequiredAccessLevel = ACCESS_LEVEL.PUBLIC;
        }

        public ACCESS_LEVEL RequiredAccessLevel { get; set; }
    }
}