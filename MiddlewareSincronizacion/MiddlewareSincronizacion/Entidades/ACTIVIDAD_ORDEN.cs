using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ACTIVIDAD_ORDEN
    {
        public string AUART { get; set; }
        public string ILART { get; set; }
        public string ILATX_ES { get; set; }
        public string ILATX_EN { get; set; }

        public ACTIVIDAD_ORDEN()
        {
            AUART = string.Empty;
            ILART = string.Empty;
            ILATX_ES = string.Empty;
            ILATX_EN = string.Empty;
        }
    }
}
