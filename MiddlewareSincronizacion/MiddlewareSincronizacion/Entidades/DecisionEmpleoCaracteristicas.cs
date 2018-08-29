using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class DecisionEmpleoCaracteristicas
    {
        public string PRUEFLOS { get; set; }
        public string EQUNR { get; set; }

        public string VCODE { get; set; }

        public string VCODEGRP { get; set; }

        public string VBEWERTUNG { get; set; }
        public string KURZTEXT { get; set; }
        public DecisionEmpleoCaracteristicas()
        {
            PRUEFLOS = string.Empty;
            EQUNR = string.Empty;
            VCODE = string.Empty;
            VCODEGRP = string.Empty;
            VBEWERTUNG = string.Empty;
            KURZTEXT = string.Empty;
        }
    }
}
