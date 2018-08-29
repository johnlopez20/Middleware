using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class MaterialesAlmacen
    {
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string MAKTX { get; set; }
        public string LVORM { get; set; }

        public MaterialesAlmacen()
        {
            MATNR = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            MAKTX = string.Empty;
            LVORM = string.Empty;
        }
    }
}
