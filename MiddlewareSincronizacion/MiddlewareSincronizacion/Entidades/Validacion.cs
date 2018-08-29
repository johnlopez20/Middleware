using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Validacion
    {
        public string KSCHL { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string VKGRP { get; set; }
        public string PLTYP { get; set; }
        public string KUNNR { get; set; }
        public string MATNR { get; set; }
        public string KFRST { get; set; }
        public string DATBI { get; set; }
        public string DATAB { get; set; }
        public string KBSTAT { get; set; }
        public string KNUMH { get; set; }
        public Validacion()
        {
            KSCHL = string.Empty;
            VKORG = string.Empty;
            VTWEG = string.Empty;
            VKGRP = string.Empty;
            PLTYP = string.Empty;
            KUNNR = string.Empty;
            MATNR = string.Empty;
            KFRST = string.Empty;
            DATBI = string.Empty;
            DATAB = string.Empty;
            KBSTAT = string.Empty;
            KNUMH = string.Empty;
        }
    }
}
