using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class UsuarioGrupoMateriales
    {
        public string PERNR { get; set; }
        public string ZSAM_GU { get; set; }

        public UsuarioGrupoMateriales()
        {
            PERNR = string.Empty;
            ZSAM_GU = string.Empty;
        }
    }
}
