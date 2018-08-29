using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ClasePedido
    {
        public string AUART {get;set;}
        public string BEZEI { get; set; }
        public ClasePedido()
        {
            AUART = string.Empty;
            BEZEI = string.Empty;
        }
    }
}
