using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class GrupoVendedores
    {
        public string VKGRP {get;set;}
        public string BEZEI { get; set; }
        public GrupoVendedores()
        {
            VKGRP = string.Empty;
            BEZEI = string.Empty;
        }
    }
}
