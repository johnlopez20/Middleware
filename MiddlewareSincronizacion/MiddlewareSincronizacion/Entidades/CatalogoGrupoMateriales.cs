using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CatalogoGrupoMateriales
    {
        public string ZSAM_GU { get; set; }
        public string DESCRIPCION { get; set; }
        public CatalogoGrupoMateriales()
        {
            ZSAM_GU = string.Empty;
            DESCRIPCION = string.Empty;
        }
    }
}
