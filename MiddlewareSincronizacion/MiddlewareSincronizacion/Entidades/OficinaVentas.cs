using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class OficinaVentas
    {
        public string VKBUR {get;set;}
        public string BEZEI { get; set; }
        public OficinaVentas()
        {
            VKBUR = string.Empty;
            BEZEI = string.Empty;
        }
    }
}
