﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class InfoTipo
    {
        public string VALUED { get; set; }
        public string DDTEXT_ES { get; set; }
        public string DDTEXT_EN { get; set; }

        public InfoTipo()
        {
            VALUED = string.Empty;
            DDTEXT_ES = string.Empty;
            DDTEXT_EN = string.Empty;
        }
    }
}
