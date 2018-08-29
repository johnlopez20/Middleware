using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class GRUPOS_PLANIFICACION
    {
        public string INGRP { get; set; }
        public string IWERK { get; set; }

        public GRUPOS_PLANIFICACION()
        {
            INGRP = string.Empty;
            IWERK = string.Empty;
        }
    }
}
