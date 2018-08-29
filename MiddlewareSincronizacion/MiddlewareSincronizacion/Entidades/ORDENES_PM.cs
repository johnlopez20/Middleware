using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ORDENES_PM
    {
        public string AUART { get; set; }
        public string KOKRS { get; set; }
        public string AUFNR { get; set; }
        public string ESTATUS { get; set; }
        public string KTEXT { get; set; }
        public string AUTYP { get; set; }
        public string IWERK { get; set; }
        public string LVORM { get; set; }

        public ORDENES_PM()
        {
            AUART = string.Empty;
            KOKRS = string.Empty;
            AUFNR = string.Empty;
            ESTATUS = string.Empty;
            KTEXT = string.Empty;
            AUTYP = string.Empty;
            IWERK = string.Empty;
            LVORM = string.Empty;
        }
    }
}
