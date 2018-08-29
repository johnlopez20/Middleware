using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Canal_Distribucion
    {
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string VTEXT_ES { get; set; }
        public string VTEXT_EN { get; set; }

        public Canal_Distribucion()
        {
            VKORG = string.Empty;
            VTWEG = string.Empty;
            VTEXT_ES = string.Empty;
            VTEXT_EN = string.Empty;
        }
    }
}
