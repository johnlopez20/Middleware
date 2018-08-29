using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ClaseDoc
    {
        public string BSART { get; set; }
        public string BATXT_ES { get; set; }
        public string BATXT_EN { get; set; }

        public ClaseDoc()
        {
            BSART = string.Empty;
            BATXT_ES = string.Empty;
            BATXT_EN = string.Empty;
        }
    }
}
