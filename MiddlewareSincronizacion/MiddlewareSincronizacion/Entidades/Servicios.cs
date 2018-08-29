using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Servicios
    {
        public string ASNUM { get; set; }
        public string ASKTX_ES { get; set; }
        public string ASKTX_EN { get; set; }
        public string MEINS { get; set; }
        public string BKLAS { get; set; }
        public string BKBEZ_ES { get; set; }
        public string BKBEZ_EN { get; set; }
        public string MATKL { get; set; }
        public string SPART { get; set; }
        public string ASTYP { get; set; }
        public string ASTYP_TXT_ES { get; set; }
        public string ASTYP_TXT_EN { get; set; }
        public string LVORM { get; set; }

        public Servicios()
        {
            ASNUM = string.Empty;
            ASKTX_ES = string.Empty;
            ASKTX_EN = string.Empty;
            MEINS = string.Empty;
            BKLAS = string.Empty;
            BKBEZ_ES = string.Empty;
            BKBEZ_EN = string.Empty;
            MATKL = string.Empty;
            SPART = string.Empty;
            ASTYP = string.Empty;
            ASTYP_TXT_ES = string.Empty;
            ASTYP_TXT_EN = string.Empty;
            LVORM = string.Empty;
        }
    }
}
