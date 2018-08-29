using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class DMS
    {
        public string QMNUM { get; set; }
        public string DOKAR { get; set; }
        public string DOKNR { get; set; }
        public string LATEST_REL { get; set; }
        public string LATEST_VERSION { get; set; }
        public string ICON_STATUS { get; set; }
        public string DOKTL { get; set; }
        public string DOKVR { get; set; }
        public string DKTXT { get; set; }

        public DMS()
        {
            QMNUM = string.Empty;
            DOKAR = string.Empty;
            DOKNR = string.Empty;
            LATEST_REL = string.Empty;
            LATEST_VERSION = string.Empty;
            ICON_STATUS = string.Empty;
            DOKTL = string.Empty;
            DOKVR = string.Empty;
            DKTXT = string.Empty;
        }
    }
}
