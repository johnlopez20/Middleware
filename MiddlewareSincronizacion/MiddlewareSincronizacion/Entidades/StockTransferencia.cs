using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class StockTransferencia
    {
        public string WERKS { get; set; }
        public string MATNR { get; set; }
        public string UMLMC { get; set; }
        public string XCHPF { get; set; }
        public string MEINS { get; set; }
        public string MAKTX { get; set; }
        public string MAKTX_E { get; set; }
        public string LVORM { get; set; }
        public string LGORT { get; set; }
        public string CHARG { get; set; }
        public string SOBKZ { get; set; }
        public string KDAUF { get; set; }
        public string KDPOS { get; set; }
        public string UMLGO { get; set; }

        public StockTransferencia()
        {
            WERKS = string.Empty;
            MATNR = string.Empty;
            UMLMC = string.Empty;
            XCHPF = string.Empty;
            MEINS = string.Empty;
            MAKTX = string.Empty;
            MAKTX_E = string.Empty;
            LVORM = string.Empty;
            LGORT = string.Empty;
            CHARG = string.Empty;
            SOBKZ = string.Empty;
            KDAUF = string.Empty;
            KDPOS = string.Empty;
            UMLGO = string.Empty;

        }
    }
}
