using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PebMaps.Components
{
    public partial class TwoSeventyMeter
    {
        protected static int democratEV { get; set; } = 280;
        protected static int republicanEV { get; set; } = 260;
        protected string blue { get => getViewWidth(democratEV); set { } }
        protected string red { get => getViewWidth(republicanEV); set { } }

        private string getViewWidth(int arg)
        {
            var thing = $"width:{(arg * 100) / 538}%";
            return thing;
        }

    }
}
