using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_FlujoDeudores
    {
        #region Instacia
        private static DALC_FlujoDeudores instance = null;
        private static readonly object padlock = new object();

        public static DALC_FlujoDeudores ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_FlujoDeudores();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTablaFlujo(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_Flujo_deudores_MDL();
        }
        public void IngresarFlujoDeudores(EntityConnectionStringBuilder connection, FlujoDeudores f)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_Flujo_deudores_MDL(f.GJAHR,
                                              f.MONAT,
                                              f.BUDAT,
                                              f.ZFBDT,
                                              f.ZBD3T,
                                              f.VBELN,
                                              f.DMBTR,
                                              f.WAERS,
                                              f.KUNNR,
                                              f.NAME1,
                                              f.VKGRP,
                                              f.BEZEI);
        }
    }
}
