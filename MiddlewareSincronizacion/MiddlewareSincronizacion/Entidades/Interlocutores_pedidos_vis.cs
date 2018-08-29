using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Interlocutores_pedidos_vis
    {
        public string VBELN { get; set; }
        public string XBLNR { get; set; }
        public string POSNR { get; set; }
        public string PARVW { get; set; }
        public string KUNNR { get; set; }
        public string XCPDK { get; set; }
        public Interlocutores_pedidos_vis()
        {
            VBELN = string.Empty;
            XBLNR = string.Empty;
            POSNR = string.Empty;
            PARVW = string.Empty;
            KUNNR = string.Empty;
            XCPDK = string.Empty;
        }
    }
}
