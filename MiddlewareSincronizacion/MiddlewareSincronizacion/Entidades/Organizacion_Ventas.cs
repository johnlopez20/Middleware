using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Organizacion_Ventas
    {
        public string VKORG { get; set; }
        public string VTEXT { get; set; }

        public Organizacion_Ventas()
        {
            VKORG = string.Empty;
            VTEXT = string.Empty;
        }
    }
}
