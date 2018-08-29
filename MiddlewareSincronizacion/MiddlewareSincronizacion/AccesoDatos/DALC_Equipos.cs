using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Equipos
    {
        #region instancia
        private static DALC_Equipos instance = null;
        private static readonly object padlock = new object();

        public static DALC_Equipos obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Equipos();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarEquipos(EntityConnectionStringBuilder connection, Equipos eq)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_equipos_MDL(eq.EQUNR,
                                       eq.SWERK);
        }
        public void IngresaEquipo(EntityConnectionStringBuilder connection, Equipos eq)
        {
            var context = new samEntities(connection.ToString());
            context.equipos_MDL(eq.TPLNR,
                                eq.EQUNR,
                                eq.EQKTX_ES,
                                eq.EQKTX_EN,
                                eq.EQTYP,
                                eq.EQART,
                                eq.BEGRU,
                                eq.HERLD,
                                eq.HERST,
                                eq.TYPBZ,
                                eq.EMATN,
                                eq.SERGE,
                                eq.BAUJJ,
                                eq.BAUMM,
                                eq.SWERK,
                                eq.STORT,
                                eq.BEBER,
                                eq.EQFNR,
                                eq.BUKRS,
                                eq.ANLNR,
                                eq.KOSTL,
                                eq.IWERK,
                                eq.GEWRK,
                                eq.ERNAM,
                                eq.WKCTR,
                                eq.MATNR,
                                eq.MAKTX_ES,
                                eq.MAKTX_EN,
                                eq.SERNR,
                                eq.LBBSA,
                                eq.WERK,
                                eq.LAGER,
                                eq.B_CHARGE,
                                eq.SOBKZ,
                                eq.KUNNR,
                                eq.LIFNR,
                                eq.DATLWB,
                                eq.POINT,
                                eq.POINTM,
                                eq.MRNGU,
                                eq.ABCKZ,
                                eq.INGRP,
                                eq.HEQUI,
                                eq.B_WERK,
                                eq.B_LAGER,
                                eq.CHARGE,
                                eq.LVORM,
                                eq.INVNR,
                                eq.GROES,
                                eq.MAPAR);
        }
    }
}
