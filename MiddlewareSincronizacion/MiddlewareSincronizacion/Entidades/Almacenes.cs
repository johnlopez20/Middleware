using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Almacenes
    {
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string LGOBE_ES { get; set; }
        public string LGOBE_EN { get; set; }

        public Almacenes()
        {
            WERKS = string.Empty;
            LGORT = string.Empty;
            LGOBE_ES = string.Empty;
            LGOBE_EN = string.Empty;
        }
    }
}
