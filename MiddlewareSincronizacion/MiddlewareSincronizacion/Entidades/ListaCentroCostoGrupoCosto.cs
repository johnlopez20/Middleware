using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ListaCentroCostoGrupoCosto
    {
        public string ZSAM_GK { get; set; }
        public string KOSTL { get; set; }
        public string BUKRS { get; set; }
        public string KSTAR { get; set; }
        public ListaCentroCostoGrupoCosto()
        {
            ZSAM_GK = string.Empty;
            KOSTL = string.Empty;
            BUKRS = string.Empty;
            KSTAR = string.Empty;
        }
    }
}
