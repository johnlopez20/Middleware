using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Org_ventas
    {
        public string VKORG { get; set; }
        public string VTEXT { get; set; }
        public Org_ventas()
        {
            VKORG = string.Empty;
            VTEXT = string.Empty;
        }
    }
}
