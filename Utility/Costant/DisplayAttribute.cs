using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility.Costant
{
    internal class DisplayAttribute : Attribute
    {
        public string Name { get; set; }
    }
}