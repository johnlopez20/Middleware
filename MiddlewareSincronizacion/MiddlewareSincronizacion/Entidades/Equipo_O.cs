using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Equipo_O
    {
        public string EQUNR { get; set; }
        public string MATNR { get; set; }
        public string SERNR { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string AUFNR { get; set; }
        public string SUPEQUI { get; set; }
        public string CHARG { get; set; }
        public string MONTADO { get; set; }

        public Equipo_O()
        {
            EQUNR = string.Empty;
            MATNR = string.Empty;
            SERNR = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            AUFNR = string.Empty;
            SUPEQUI = string.Empty;
            CHARG = string.Empty;
            MONTADO = string.Empty;
        }
    }
}
