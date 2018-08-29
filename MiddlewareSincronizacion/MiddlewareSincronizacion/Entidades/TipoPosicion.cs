using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TipoPosicion
    {
        public string PSTYP { get; set; }
        public string EPSTP_ES { get; set; }
        public string PTEXT_ES { get; set; }
        public string EPSTP_EN { get; set; }
        public string PTEXT_EN { get; set; }


        public TipoPosicion()
        {
            PSTYP = string.Empty;
            EPSTP_ES = string.Empty;
            PTEXT_ES = string.Empty;
            EPSTP_EN = string.Empty;
            PTEXT_EN = string.Empty;
        }
    }
}
