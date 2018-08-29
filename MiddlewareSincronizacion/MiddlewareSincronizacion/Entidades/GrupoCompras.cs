using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class GrupoCompras
    {
        public string EKGRP { get; set; }
        public string EKNAM { get; set; }

        public GrupoCompras()
        {
            EKGRP = string.Empty;
            EKNAM = string.Empty;
        }
    }
}
