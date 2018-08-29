using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class PUESTO_TRABAJO
    {
        public string VERWE { get; set; }
        public string WERKS { get; set; }
        public string ARBPL { get; set; }
        public string KTEXT_UP_ES { get; set; }
        public string KTEXT_UP_EN { get; set; }
        public string KTEXT_ES { get; set; }
        public string KTEXT_EN { get; set; }

        public PUESTO_TRABAJO()
        {
            VERWE = string.Empty;
            WERKS = string.Empty;
            ARBPL = string.Empty;
            KTEXT_UP_ES = string.Empty;
            KTEXT_UP_EN = string.Empty;
            KTEXT_ES = string.Empty;
            KTEXT_EN = string.Empty;
        }
    }
}
