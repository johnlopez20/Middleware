using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ClaveMoneda
    {
        public string WAERS { get; set; }
        public string LTEXT_ES { get; set; }
        public string LTEXT_EN { get; set; }

        public ClaveMoneda()
        {
            WAERS = string.Empty;
            LTEXT_ES = string.Empty;
            LTEXT_EN = string.Empty;
        }
    }
}
