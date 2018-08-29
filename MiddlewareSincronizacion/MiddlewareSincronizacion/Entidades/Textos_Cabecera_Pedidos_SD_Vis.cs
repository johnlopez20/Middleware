using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Textos_Cabecera_Pedidos_SD_Vis
    {
        public string FOLIO_SAM { get; set; }
        public string VBELN { get; set; }
        public string INDICE { get; set; }
        public string TDLINE { get; set; }
        public string UNAME { get; set; }
        public Textos_Cabecera_Pedidos_SD_Vis()
        {
            FOLIO_SAM = string.Empty;
            VBELN = string.Empty;
            INDICE = string.Empty;
            TDLINE = string.Empty;
            UNAME = string.Empty;
        }
    }
}
