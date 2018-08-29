using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Material_Almacen_Centro
    {
        public string MATNR { get; set; }
        public string ACTUALIZADO { get; set; }
        public string LVORM { get; set; }

        public Material_Almacen_Centro()
        {
            MATNR = string.Empty;
            ACTUALIZADO = string.Empty;
            LVORM = string.Empty;
        }
    }
}
