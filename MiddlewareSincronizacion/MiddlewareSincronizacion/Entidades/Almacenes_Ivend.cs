using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Almacenes_Ivend
    {
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string IVEND { get; set; }
        public string ILGORT { get; set; }
        public string LOCATION { get; set; }

        public Almacenes_Ivend()
        {
            WERKS = string.Empty;
            LGORT = string.Empty;
            IVEND = string.Empty;
            ILGORT = string.Empty;
            LOCATION = string.Empty;
        }
    }
}
