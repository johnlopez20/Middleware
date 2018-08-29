using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Cabecera_Recuento_Inventario
    {
        public string FOLIO_SAM { get; set; }
        public string IBLNR_REC { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public Cabecera_Recuento_Inventario()
        {
            FOLIO_SAM = string.Empty;
            IBLNR_REC = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
        }
    }
}
