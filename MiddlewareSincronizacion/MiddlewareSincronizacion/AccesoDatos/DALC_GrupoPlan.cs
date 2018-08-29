using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_GrupoPlan
    {
        #region Instancia
        private static DALC_GrupoPlan instance = null;
        private static readonly object padlock = new object();

        public static DALC_GrupoPlan ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_GrupoPlan();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarGrupoPlan(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_grupo_planificacion_avisos_MDL();
        }
        public void IngresaGrupoPlan(EntityConnectionStringBuilder connection, GrupoPlan um)
        {
            var context = new samEntities(connection.ToString());
            context.grupo_planificacion_avisos_MDL(um.IWERK,
                                                   um.INGRP,
                                                   um.INNAM,
                                                   um.INTEL,
                                                   um.AUART_WP);
        }
        public void VaciarResponsable(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_puesto_trabajo_responsable_MDL();
        }
        public void IngresaResponsable(EntityConnectionStringBuilder connection, ResponsableTrab um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_puesto_trabajo_responsable_MDL(um.VERAN,
                                                          um.WERKS,
                                                          um.ARBPL,
                                                          um.KTEXT_UP,
                                                          um.KTEXT,
                                                          um.SPRAS);
        }
        public void VaciarClaseAviso(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_clase_aviso_MDL();
        }
        public void IngresaClaseAviso(EntityConnectionStringBuilder connection, ClaseAviso cla)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_clase_aviso_MDL(cla.QMART);
        }
    }
}
