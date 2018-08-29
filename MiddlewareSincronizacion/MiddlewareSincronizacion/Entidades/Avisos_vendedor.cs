using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Avisos_vendedor
    {
        public string VKGRP { get; set; }
        public string BEZEI { get; set; }
        public string TEXTO1 { get; set; }
        public string TEXTO2 { get; set; }
        public string TEXTO3 { get; set; }
        public string FCREACION { get; set; }
        public string HCREACION { get; set; }
        public Avisos_vendedor()
        {
            VKGRP = string.Empty;
            BEZEI = string.Empty;
            TEXTO1 = string.Empty;
            TEXTO2 = string.Empty;
            TEXTO3 = string.Empty;
            FCREACION = string.Empty;
            HCREACION = string.Empty;
        }
    }
}
