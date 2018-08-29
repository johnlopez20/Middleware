using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Cecos
    {
        public string BUKRS { get; set; }
        public string KSTAR { get; set; }
        public string LTEXT { get; set; }
        public string KOSTL { get; set; }
        public string KTEXT_ES { get; set; }

        public Cecos()
        {
            BUKRS = string.Empty;
            KSTAR = string.Empty;
            LTEXT = string.Empty;
            KOSTL = string.Empty;
            KTEXT_ES = string.Empty;
        }
    }
}
