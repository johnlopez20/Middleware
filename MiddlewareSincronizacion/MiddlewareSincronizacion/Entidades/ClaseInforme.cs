using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ClaseInforme
    {
        public string FEART { get; set; }
        public string KURZTEXT_ES { get; set; }
        public string KURZTEXT_EN { get; set; }
        public string RBNR { get; set; }
        public string RBNRX_ES { get; set; }
        public string RBNRX_EN { get; set; }
        public string BWART { get; set; }
        public string BTEXT_ES { get; set; }
        public string BTEXT_EN { get; set; }
        public ClaseInforme()
        {
            FEART = string.Empty;
            KURZTEXT_ES = string.Empty;
            KURZTEXT_EN = string.Empty;
            RBNR = string.Empty;
            RBNRX_ES = string.Empty;
            RBNRX_EN = string.Empty;
            BWART = string.Empty;
            BTEXT_ES = string.Empty;
            BTEXT_EN = string.Empty;
        }
    }
}
