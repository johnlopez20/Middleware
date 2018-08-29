using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Tiempo
    {
        public string WERKS { get; set; }
        public string BBP_TABL { get; set; }
        public string PERIODO { get; set; }
        public Tiempo()
        {
            WERKS = string.Empty;
            BBP_TABL = string.Empty;
            PERIODO = string.Empty;
        }
    }
}
