using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class SolPed_Vis_Cab
    {
        public string FOLIO_SAM { get; set; }
        public string FOLIO_SAP { get; set; }
        public string BBSRT { get; set; }
        public string TEXTO_CAB { get; set; }
        public string AFNAM { get; set; }
        public string EWERK { get; set; }
        public string BADAT { get; set; }
        public string LFDAT { get; set; }
        public string FRGDT { get; set; }
        public string BSART { get; set; }
        public string WERKS { get; set; }

        public SolPed_Vis_Cab()
        {
            FOLIO_SAM = string.Empty;
            FOLIO_SAP = string.Empty;
            BBSRT = string.Empty;
            TEXTO_CAB = string.Empty;
            AFNAM = string.Empty;
            EWERK = string.Empty;
            BADAT = string.Empty;
            LFDAT = string.Empty;
            FRGDT = string.Empty;
            BSART = string.Empty;
            WERKS = string.Empty;
        }
    }
}
