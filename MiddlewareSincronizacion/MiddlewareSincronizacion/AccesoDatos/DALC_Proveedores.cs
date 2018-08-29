using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Proveedores
    {
        #region Instancia
        private static DALC_Proveedores instance = null;
        private static readonly object padlock = new object();

        public static DALC_Proveedores obtenerInstacia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Proveedores();
                }
                return instance;
            }
        }
        #endregion
        public void IngresaProveedor(EntityConnectionStringBuilder connection, Proveedores pr)
        {
            var context = new samEntities(connection.ToString());
            context.proveedores_MDL(pr.LIFNR,
                                    pr.BUKRS,
                                    pr.EKORG,
                                    pr.ADRC,
                                    pr.DATE_FROM,
                                    pr.NATION,
                                    pr.DATE_TO,
                                    pr.TITLE,
                                    pr.NAME1,
                                    pr.NAME2,
                                    pr.NAME3,
                                    pr.NAME4,
                                    pr.CITY1,
                                    pr.CITY2,
                                    pr.CITY_CODE,
                                    pr.CITYP_CODE,
                                    pr.HOME_CITY,
                                    pr.STREET,
                                    pr.HOUSE_NUM1,
                                    pr.KTOKK,
                                    pr.STCD1,
                                    pr.ZTERM,
                                    pr.AKONT,
                                    pr.MINBW,
                                    pr.WAERS,
                                    pr.INCO1,
                                    pr.INCO2,
                                    pr.BSTAE,
                                    pr.EKGRP,
                                    pr.LFABC,
                                    pr.LOEVMB,
                                    pr.SPERR,
                                    pr.LOEVM,
                                    pr.LVORM);
        }
        //public void VaciarProveedor(EntityConnectionStringBuilder connection, Proveedores pr)
        //{
        //    var context = new samEntities(connection.ToString());
        //    context.proveedores_delete_MDL(pr.LIFNR,
        //                                   pr.BUKRS);
        //}
        public void VaciarProveedor(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_proveedores_MDL();
        }
    }
}
