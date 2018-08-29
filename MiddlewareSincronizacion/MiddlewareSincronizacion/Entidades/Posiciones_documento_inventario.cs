using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Posiciones_documento_inventario
    {
        public string IBLNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string ZEILI { get; set; }
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string CHARG { get; set; }
        public string SOBKZ { get; set; }
        public string BSTAR { get; set; }
        public string KDAUF { get; set; }
        public string KDPOS { get; set; }
        public string KDEIN { get; set; }
        public string LIFNR { get; set; }
        public string KUNNR { get; set; }
        public string MEINS { get; set; }
        public Posiciones_documento_inventario()
        {
            IBLNR = string.Empty;
            FOLIO_SAM = string.Empty;
            ZEILI = string.Empty;
            MATNR = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            CHARG = string.Empty;
            SOBKZ = string.Empty;
            BSTAR = string.Empty;
            KDAUF = string.Empty;
            KDPOS = string.Empty;
            KDEIN = string.Empty;
            LIFNR = string.Empty;
            KUNNR = string.Empty;
            MEINS = string.Empty;
        }
    }
}
