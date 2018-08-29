using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CatalogoGrupoCentroCostos
    {
        public string ZSAM_GK { get; set; }
        public string DESCRIPCION { get; set; }
        public CatalogoGrupoCentroCostos()
        {
            ZSAM_GK = string.Empty;
            DESCRIPCION = string.Empty;
        }
    }
}
