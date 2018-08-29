using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TMIGO_CAB
    {
        public string MBLNR { get; set; }
        public string MJAHR { get; set; }
        public string BLDAT { get; set; }
        public string BUDAT { get; set; }
        public string LFSNR { get; set; }
        public string FRBNR { get; set; }
        public string BKTXT { get; set; }
        public string LIFNR { get; set; }
        public string NAME1 { get; set; }
        public string FOLIO_SAM { get; set; }
        public string UMLGO { get; set; }
        public string LVORM { get; set; }

        public TMIGO_CAB()
        {
            MBLNR = string.Empty;
            MJAHR = string.Empty;
            BLDAT = string.Empty;
            BUDAT = string.Empty;
            LFSNR = string.Empty;
            FRBNR = string.Empty;
            BKTXT = string.Empty;
            LIFNR = string.Empty;
            NAME1 = string.Empty;
            FOLIO_SAM = string.Empty;
            UMLGO = string.Empty;
            LVORM = string.Empty;
        }
    }
}
