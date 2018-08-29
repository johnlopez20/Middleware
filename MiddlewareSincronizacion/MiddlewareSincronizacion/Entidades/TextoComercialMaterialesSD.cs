using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TextoComercialMaterialesSD
    {
        public string MATNR { get; set; }
        public string VKORG { get; set; }
        public string SPART { get; set; }
        public string INDICE { get; set; }
        public string TDFORMAT { get; set; }
        public string TDLINE { get; set; }
        public TextoComercialMaterialesSD()
        {
            MATNR = string.Empty;
            VKORG = string.Empty;
            SPART = string.Empty;
            INDICE = string.Empty;
            TDFORMAT = string.Empty;
            TDLINE = string.Empty;
        }
    }
}
