using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CabOrdenesCrea
    {
        public string FOLIO_SAM { get; set; }
        public string AUFNR { get; set; }
        public string FUNC_LOC { get; set; }
        public string UZEIT { get; set; }
        public string DATUM { get; set; }
        public string TABIX { get; set; }
        public string PPLANT { get; set; }
        public string ORDTYPE { get; set; }
        public string MNWKCTR { get; set; }
        public string SHORTTXT { get; set; }
        public string EQUIP { get; set; }
        public string START_DATE { get; set; }
        public string FINISH_DATE { get; set; }
        public string FOLIO_SAP { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }

        public CabOrdenesCrea()
        {
            FOLIO_SAM = string.Empty;
            AUFNR = string.Empty;
            FUNC_LOC = string.Empty;
            UZEIT = string.Empty;
            DATUM = string.Empty;
            TABIX = string.Empty;
            PPLANT = string.Empty;
            ORDTYPE = string.Empty;
            MNWKCTR = string.Empty;
            SHORTTXT = string.Empty;
            EQUIP = string.Empty;
            START_DATE = string.Empty;
            FINISH_DATE = string.Empty;
            FOLIO_SAP = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
        }
    }
}
