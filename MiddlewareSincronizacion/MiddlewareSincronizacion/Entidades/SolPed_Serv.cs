using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class SolPed_Serv
    {
        public string FOLIO_SAM { get; set; }
        public string PREQ_ITEM { get; set; }
        public string NUM_SER { get; set; }
        public string SERVICE { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string SHORT_TEXT { get; set; }
        public string QUANTITY { get; set; }
        public string BASE_UOM { get; set; }
        public string PRICE_UNIT { get; set; }
        public string GR_PRICE { get; set; }
        public string MATL_GROUP { get; set; }
        public string G_L_ACCT { get; set; }
        public string COST_CTR { get; set; }
        public string AUFNR { get; set; }
        public string FOLIO_SAP { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MODIFICADO { get; set; }
        public string ERROR { get; set; }

        public SolPed_Serv()
        {
            FOLIO_SAM = string.Empty;
            PREQ_ITEM = string.Empty;
            NUM_SER = string.Empty;
            SERVICE = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            SHORT_TEXT = string.Empty;
            QUANTITY = string.Empty;
            BASE_UOM = string.Empty;
            PRICE_UNIT = string.Empty;
            GR_PRICE = string.Empty;
            MATL_GROUP = string.Empty;
            G_L_ACCT = string.Empty;
            COST_CTR = string.Empty;
            AUFNR = string.Empty;
            FOLIO_SAP = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MODIFICADO = string.Empty;
            ERROR = string.Empty;
        }
    }
}
