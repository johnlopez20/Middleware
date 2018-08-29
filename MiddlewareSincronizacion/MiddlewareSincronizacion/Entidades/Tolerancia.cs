using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Tolerancia
    {
        public string WERKS { get; set; }
        public string EBELN { get; set; }
        public string EBELP { get; set; }
        public string TOLERANCIA { get; set; }
        public Tolerancia()
        {
            WERKS = string.Empty;
            EBELN = string.Empty;
            EBELP = string.Empty;
            TOLERANCIA = string.Empty;
        }
    }
}
