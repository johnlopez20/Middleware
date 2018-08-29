using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ClaseOrdenPP
    {
        public string WERKS { get; set; }
        public string PP_AUFART { get; set; }

        public ClaseOrdenPP()
        {
            WERKS = string.Empty;
            PP_AUFART = string.Empty;
        }
    }
}
