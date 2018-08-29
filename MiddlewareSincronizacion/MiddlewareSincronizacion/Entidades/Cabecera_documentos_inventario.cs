using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Cabecera_documentos_inventario
    {
        public string IBLNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string GJAHR { get; set; }
        public string VGART { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string SOBKZ { get; set; }
        public string BLDAT { get; set; }
        public string GIDAT { get; set; }
        public string ZLDAT { get; set; }
        public string BUDAT { get; set; }
        public string MONAT { get; set; }
        public string USNAM { get; set; }
        public Cabecera_documentos_inventario()
        {
            IBLNR = string.Empty;
            FOLIO_SAM = string.Empty;
            GJAHR = string.Empty;
            VGART = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            SOBKZ = string.Empty;
            BLDAT = string.Empty;
            GIDAT = string.Empty;
            ZLDAT = string.Empty;
            BUDAT = string.Empty;
            MONAT = string.Empty;
            USNAM = string.Empty;
        }
    }
}
