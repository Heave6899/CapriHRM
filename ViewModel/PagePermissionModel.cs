using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModel
{
    public class PagePermissionModel
    {
        public int  Id { get; set; }
        public string  ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PermissionName { get; set; }
    }
}