using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class DatosInspeccion
    {
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string ART { get; set; }
        public string KURZTEXT_ES { get; set; }
        public string KURZTEXT_EN { get; set; }
        public string APA { get; set; }
        public string AKTIV { get; set; }
        public string CHG { get; set; }
        public DatosInspeccion()
        {
            MATNR = string.Empty;
            WERKS = string.Empty;
            ART = string.Empty;
            KURZTEXT_ES = string.Empty;
            KURZTEXT_EN = string.Empty;
            APA = string.Empty;
            AKTIV = string.Empty;
            CHG = string.Empty;
        }
    }
}
