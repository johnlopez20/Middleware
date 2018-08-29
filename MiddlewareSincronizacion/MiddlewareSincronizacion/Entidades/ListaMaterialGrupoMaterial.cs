using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ListaMaterialGrupoMaterial
    {
        public string MTART { get; set; }
        public string MATNR { get; set; }
        public string ZSAM_GU { get; set; }
        public ListaMaterialGrupoMaterial()
        {
            MTART = string.Empty;
            MATNR = string.Empty;
            ZSAM_GU = string.Empty;
        }
    }
}
