using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Inventario
    {
        public string MATNR { get; set; }
        public string MAKTX_ES { get; set; }
        public string MAKTX_EN { get; set; }
        public string WERKS { get; set; }
        public string MEINS { get; set; }
        public string LGORT { get; set; }
        public string LGOBE { get; set; }
        public string CLABS { get; set; }
        public string CINSM { get; set; }
        public string CSPEM { get; set; }
        public string CUMLM { get; set; }
        public string CHARG { get; set; }
        public string MTART { get; set; }
        public string MATKL { get; set; }
        public string SERNR { get; set; }
        public string XCHPF { get; set; }
        public Inventario()
        {
            MATNR = string.Empty;
            MAKTX_ES = string.Empty;
            MAKTX_EN = string.Empty;
            WERKS = string.Empty;
            MEINS = string.Empty;
            LGORT = string.Empty;
            LGOBE = string.Empty;
            CLABS = string.Empty;
            CINSM = string.Empty;
            CSPEM = string.Empty;
            CUMLM = string.Empty;
            CHARG = string.Empty;
            MTART = string.Empty;
            MATKL = string.Empty;
            SERNR = string.Empty;
            XCHPF = string.Empty;
        }
    }
}
