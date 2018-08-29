﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CaracteristicasLote
    {
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string CHARG { get; set; }
        public string CLABS { get; set; }
        public string NUM_EQUI { get; set; }
        public string FECHA_MONT { get; set; }
        public string ULTIM_MEDI { get; set; }
        public string STATUS_MONT { get;set; }
        public string VAL_OVER { get; set; }
        public string FECHA_DESM { get; set; }
        public CaracteristicasLote()
        {
            MATNR = string.Empty;
            WERKS = string.Empty;
            CHARG = string.Empty;
            CLABS = string.Empty;
            NUM_EQUI = string.Empty;
            FECHA_MONT = string.Empty;
            ULTIM_MEDI = string.Empty;
            STATUS_MONT = string.Empty;
            VAL_OVER = string.Empty;
            FECHA_DESM = string.Empty;
        }
    }
}