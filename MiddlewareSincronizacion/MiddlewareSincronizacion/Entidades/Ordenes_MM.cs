using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Ordenes_MM
    {
        public string AUART { get; set; }
        public string KOKRS { get; set; }
        public string AUFNR { get; set; }
        public string KTEXT { get; set; }
        public string ESTATUS { get; set; }
        public string LVORM { get; set; }

        public Ordenes_MM()
        {

            AUART = string.Empty;
            KOKRS = string.Empty;
            AUFNR = string.Empty;
            KTEXT = string.Empty;
            ESTATUS = string.Empty;
            LVORM = string.Empty;
        }
    }
}
