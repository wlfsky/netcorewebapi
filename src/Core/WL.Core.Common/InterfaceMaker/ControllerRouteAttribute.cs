using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Common.InterfaceMaker
{
    public class ControllerRouteAttribute : Attribute
    {
        public string Route { get; set; } = "";
        public ControllerRouteAttribute(string route)
        {
            this.Route = route;
        }
    }
}
