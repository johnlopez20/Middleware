using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Ubicaciones
    {
        #region instancia
        private static DALC_Ubicaciones instance = null;
        private static readonly object padlock = new object();

        public static DALC_Ubicaciones obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Ubicaciones();
                }
                return instance;
            }
        }
        #endregion
        public void IngresaUbicacion(EntityConnectionStringBuilder connection, Ubicaciones ub)
        {
            var context = new samEntities(connection.ToString());
            context.ubicaciones_tecnicas_MDL(ub.TPLNR,
                                             ub.PLTXT_ES,
                                             ub.PLTXT_EN,
                                             ub.IWERK,
                                             ub.TPLKZ,
                                             ub.BEGRU,
                                             ub.HERLD,
                                             ub.TYPBZ,
                                             ub.MAPAR,
                                             ub.SERGE,
                                             ub.BAUJJ,
                                             ub.SWERK,
                                             ub.STORT,
                                             ub.BEBER,
                                             ub.EQFNR,
                                             ub.BUKRS,
                                             ub.KOSTL,
                                             ub.INGRP,
                                             ub.LGWID,
                                             ub.IEQUI,
                                             ub.EINZL,
                                             ub.PPSID,
                                             ub.ARBPL,
                                             ub.LVORM,
                                             ub.ABCKZ,
                                             ub.INVNR,
                                             ub.GROES);
        }

        public void VaciarUbicacion(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_ubicaciones_tecnicas_MDL(centro);
        }
    }
}
