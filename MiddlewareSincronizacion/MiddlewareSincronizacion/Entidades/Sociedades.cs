using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Sociedades
    {
        public string BUKRS { get; set; }
        public string BUTXT_ES { get; set; }
        public string BUTXT_EN { get; set; }
        public string ORT01 { get; set; }
        public string WAERS { get; set; }

        public Sociedades()
        {
            BUKRS = string.Empty;
            BUTXT_ES = string.Empty;
            BUTXT_EN = string.Empty;
            ORT01 = string.Empty;
            WAERS = string.Empty;
        }
    }
}
