using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CabeceraPedidosSD
    {
        public string FOLIO_SAM { get; set; }
        public string VBELN { get; set; }
        public string DOC_TYPE { get; set; }
        public string SALES_ORG { get; set; }
        public string DISTR_CHAN { get; set; }
        public string DIVISION { get; set; }
        public string SALES_GRP { get; set; }
        public string SALES_OFF { get; set; }
        public string REQ_DATE_H { get; set; }
        public string PRICE_DATE { get; set; }
        public string PURCH_NO_C { get; set; }
        public string UNAME { get; set; }
        public string FECHA_SAM { get; set; }
        public string HORA_SAM { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public CabeceraPedidosSD()
        {
            FOLIO_SAM = string.Empty;
            VBELN = string.Empty;
            DOC_TYPE = string.Empty;
            SALES_ORG = string.Empty;
            DISTR_CHAN = string.Empty;
            DIVISION = string.Empty;
            SALES_GRP = string.Empty;
            SALES_OFF = string.Empty;
            REQ_DATE_H = string.Empty;
            PRICE_DATE = string.Empty;
            PURCH_NO_C = string.Empty;
            UNAME = string.Empty;
            FECHA_SAM = string.Empty;
            HORA_SAM = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
        }
    }
}
