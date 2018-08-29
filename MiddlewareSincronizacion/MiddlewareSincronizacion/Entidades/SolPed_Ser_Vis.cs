using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class SolPed_Ser_Vis
    {
        public string FOLIO_SAM { get; set; }
        public string BANFN { get; set; }
        public string BNFPO { get; set; }
        public string EXTROW { get; set; }
        public string SRVPOS { get; set; }
        public string MENGE { get; set; }
        public string MEINS { get; set; }
        public string KTEXT1 { get; set; }
        public string TBTWR { get; set; }
        public string WAERS { get; set; }
        public string KOSTL { get; set; }
        public string NETWR { get; set; }

        public SolPed_Ser_Vis()
        {
            FOLIO_SAM = string.Empty;
            BANFN = string.Empty;
            BNFPO = string.Empty;
            EXTROW = string.Empty;
            SRVPOS = string.Empty;
            MENGE = string.Empty;
            MEINS = string.Empty;
            KTEXT1 = string.Empty;
            TBTWR = string.Empty;
            WAERS = string.Empty;
            KOSTL = string.Empty;
            NETWR = string.Empty;
        }
    }
}
