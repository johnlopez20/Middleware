using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class DecisionEmpleoLoteInps
    {
        public string PRUEFLOS { get; set; }
        public string VCODE { get; set; }
        public string VCODEGRP { get; set; }
        public string VBEWERTUNG { get; set; }
        public string KURZTEXT { get; set; }
        public DecisionEmpleoLoteInps()
        {
            PRUEFLOS = string.Empty;
            VCODE = string.Empty;
            VCODEGRP = string.Empty;
            VBEWERTUNG = string.Empty;
            KURZTEXT = string.Empty;
        }
    }
}
