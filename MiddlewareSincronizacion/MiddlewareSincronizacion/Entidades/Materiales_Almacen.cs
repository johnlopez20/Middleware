using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Materiales_Almacen
    {
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string MAKTX_ES { get; set; }
        public string MAKTX_EN { get; set; }
        public string LVORM { get; set; }
        public string HABLT { get; set; }

        public Materiales_Almacen()
        {
            MATNR = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            MAKTX_ES = string.Empty;
            MAKTX_EN = string.Empty;
            LVORM = string.Empty;
            HABLT = string.Empty;
        }
    }
}
