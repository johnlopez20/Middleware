using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CentroCoste
    {
        public string KOKRS { get; set; }
        public string KOSTL { get; set; }
        public string KTEXT_ES { get; set; }
        public string KTEXT_EN { get; set; }

        public CentroCoste()
        {
            KOKRS = string.Empty;
            KOSTL = string.Empty;
            KTEXT_ES = string.Empty;
            KTEXT_EN = string.Empty;
        }
    }
}
