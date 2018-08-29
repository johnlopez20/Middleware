using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Pedidos_I
    {
        public string EBELN { get; set; }
        public string EBELP { get; set; }
        public string MATNR { get; set; }
        public string TXZ01 { get; set; }
        public string MENGE { get; set; }
        public string MEINS { get; set; }
        public string BEWTP { get; set; }
        public string BWART { get; set; }
        public string MBLNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string BUZEI { get; set; }
        public string CANTIADE { get; set; }
        public string UM_PED { get; set; }
        public string PEINH { get; set; }
        public string EINDT { get; set; }
        public string BUDAT { get; set; }
        public string LVORM { get; set; }


        public Pedidos_I()
        {
            EBELN = string.Empty;
            EBELP = string.Empty;
            MATNR = string.Empty;
            TXZ01 = string.Empty;
            MENGE = string.Empty;
            MEINS = string.Empty;
            BEWTP = string.Empty;
            BWART = string.Empty;
            MBLNR = string.Empty;
            FOLIO_SAM = string.Empty;
            BUZEI = string.Empty;
            CANTIADE = string.Empty;
            UM_PED = string.Empty;
            PEINH = string.Empty;
            EINDT = string.Empty;
            BUDAT = string.Empty;
            LVORM = string.Empty;
        }
    }
}
