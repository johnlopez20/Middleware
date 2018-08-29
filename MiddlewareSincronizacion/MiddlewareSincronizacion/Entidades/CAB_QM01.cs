using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CAB_QM01
    {
        public string AUFNR { get; set; }
        public string KTEXTLOS { get; set; }
        public string PRUEFLOS { get; set; }
        public string WERK { get; set; }
        public string ERSTELLER { get; set; }
        public string ENSTEHDAT { get; set; }
        public string ENTSTEZEIT { get; set; }
        public string AENDERER { get; set; }
        public string AENDERDAT { get; set; }
        public string AENDERZEIT { get; set; }

        public CAB_QM01()
        {
            AUFNR = string.Empty;
            KTEXTLOS = string.Empty;
            PRUEFLOS = string.Empty;
            WERK = string.Empty;
            ERSTELLER = string.Empty;
            ENSTEHDAT = string.Empty;
            ENTSTEZEIT = string.Empty;
            AENDERER = string.Empty;
            AENDERDAT = string.Empty;
            AENDERZEIT = string.Empty;
        }
    }
}
