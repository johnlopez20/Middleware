using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class OrdenesPlanificacion
    {
        public string AUFNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string WARPL { get; set; }
        public string WAPOS { get; set; }
        public string PLNTY { get; set; }
        public string PLNNR { get; set; }
        public string PLNAL { get; set; }
        public OrdenesPlanificacion()
        {
            AUFNR = string.Empty;
            FOLIO_SAM = string.Empty;
            WARPL = string.Empty;
            WAPOS = string.Empty;
            PLNTY = string.Empty;
            PLNNR = string.Empty;
            PLNAL = string.Empty;
        }
    }
}
