using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite
{
    public class DescriptionAttribute : Attribute
    {
        public int No { get; set; }

        public string Name { get; set; }
    }
}