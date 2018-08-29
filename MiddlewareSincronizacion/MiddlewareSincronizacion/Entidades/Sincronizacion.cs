using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Sincronizacion
    {
        public string VKORG { get; set; }
        public string HY_INTERFAZ { get; set; }
        public string NAME { get; set; }
        public string SECUENCIA { get; set; }
        public string ACTIVA { get; set; }
        public string BBP_TABL { get; set; }
        public string PERIODO { get; set; }
        public string SELEC { get; set; }
        public string SINCRONIZACION {get;set;}
        public string CARGA_INICIAL { get; set; }

        public Sincronizacion()
        {
            VKORG = string.Empty;
            HY_INTERFAZ = string.Empty;
            NAME = string.Empty;
            SECUENCIA = string.Empty;
            ACTIVA = string.Empty;
            BBP_TABL = string.Empty;
            PERIODO = string.Empty;
            SELEC = string.Empty;
            SINCRONIZACION = string.Empty;
            CARGA_INICIAL = string.Empty;
        }
    }
}
