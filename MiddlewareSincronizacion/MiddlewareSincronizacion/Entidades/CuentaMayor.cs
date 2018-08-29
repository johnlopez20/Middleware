using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CuentaMayor
    {
        public string SAKNR { get; set; }
        public string KTOPL { get; set; }
        public string TXT50_ES { get; set; }
        public string TXT50_EN { get; set; }

        public CuentaMayor()
        {
            SAKNR = string.Empty;
            KTOPL = string.Empty;
            TXT50_ES = string.Empty;
            TXT50_EN = string.Empty;
        }
    }
}
