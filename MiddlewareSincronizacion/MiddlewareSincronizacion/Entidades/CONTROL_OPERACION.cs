﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CONTROL_OPERACION
    {
        public string STEUS { get; set; }
        public string TXT_ES { get; set; }
        public string TXT_EN { get; set; }

        public CONTROL_OPERACION()
        {
            STEUS = string.Empty;
            TXT_ES = string.Empty;
            TXT_EN = string.Empty;
        }
    }
}
