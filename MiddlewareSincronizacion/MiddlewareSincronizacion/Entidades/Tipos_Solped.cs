using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Tipos_Solped
    {
        public string WERKS { get; set; }
        public string BSART { get; set; }
        public string MTART { get; set; }
        public string BATXT_ES { get; set; }
        public string BATXT_EN { get; set; }
        public Tipos_Solped()
        {
            WERKS = string.Empty;
            BSART = string.Empty;
            MTART = string.Empty;
            BATXT_ES = string.Empty;
            BATXT_EN = string.Empty;
        }
    }
}
