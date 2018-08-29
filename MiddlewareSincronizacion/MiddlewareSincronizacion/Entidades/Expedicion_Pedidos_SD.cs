using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Expedicion_Pedidos_SD
    {
        public string VBELN { get; set; }
        public string XBLNR { get; set; }
        public string POSNR { get; set; }
        public string WERKS { get; set; }
        public string NAME1 { get; set; }
        public string LGORT { get; set; }
        public string LGOBE { get; set; }
        public string VSTEL { get; set; }
        public string VTEXT { get; set; }
        public string ROUTE { get; set; }
        public string BEZEIPX { get; set; }
        public string LPRIO { get; set; }
        public string BEZEIPE { get; set; }
        public string NTGEW { get; set; }
        public string GEWEI { get; set; }
        public string BRGEW { get; set; }
        public Expedicion_Pedidos_SD()
        {
            VBELN = string.Empty;
            XBLNR = string.Empty;
            POSNR = string.Empty;
            WERKS = string.Empty;
            NAME1 = string.Empty;
            LGORT = string.Empty;
            LGOBE = string.Empty;
            VSTEL = string.Empty;
            VTEXT = string.Empty;
            ROUTE = string.Empty;
            BEZEIPX = string.Empty;
            LPRIO = string.Empty;
            BEZEIPE = string.Empty;
            NTGEW = string.Empty;
            GEWEI = string.Empty;
            BRGEW = string.Empty;
        }
    }
}
