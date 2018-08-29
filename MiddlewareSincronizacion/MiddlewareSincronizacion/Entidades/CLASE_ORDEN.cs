using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CLASE_ORDEN
    {
        public string AUART { get; set; }
        public string AUTYP { get; set; }
        public string TXT_ES { get; set; }
        public string TXT_EN { get; set; }

        public CLASE_ORDEN()
        {
            AUART = string.Empty;
            AUTYP = string.Empty;
            TXT_ES = string.Empty;
            TXT_EN = string.Empty;
        }
    }
}
