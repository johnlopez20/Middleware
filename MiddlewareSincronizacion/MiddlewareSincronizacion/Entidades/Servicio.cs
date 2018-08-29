using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Servicio
    {
        public string EBELN { get; set; }
        public string EBELP { get; set; }
        public string EXTROW { get; set; }
        public string SRVPOS { get; set; }
        public string MENGE { get; set; }
        public string MEINS { get; set; }
        public string KTEXT1 { get; set; }
        public string TBTWR { get; set; }
        public string WAERS { get; set; }
        public string NETWR { get; set; }
        public string ACT_MENGE { get; set; }
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string DATUM { get; set; }
        public Servicio()
        {
            EBELN = string.Empty;
            EBELP = string.Empty;
            EXTROW = string.Empty;
            SRVPOS = string.Empty;
            MENGE = string.Empty;
            MEINS = string.Empty;
            KTEXT1 = string.Empty;
            TBTWR = string.Empty;
            WAERS = string.Empty;
            NETWR = string.Empty;
            ACT_MENGE = string.Empty;
            AUFNR = string.Empty;
            WERKS = string.Empty;
            DATUM = string.Empty;
        }
    }
}
