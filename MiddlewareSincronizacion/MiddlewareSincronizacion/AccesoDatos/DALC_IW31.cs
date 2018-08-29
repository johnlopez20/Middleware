using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_IW31
    {
        #region instancia
        private static DALC_IW31 instance = null;
        private static readonly object padlock = new object();

        public static DALC_IW31 obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_IW31();
                }
                return instance;
            }
        }
        #endregion
        public String comilla(String cpo)
        {
            int r = cpo.IndexOf("'");
            if (cpo.IndexOf("'") >= 1)
            {
                return cpo.Replace("'", "''");
            }
            return cpo;
        }
        public void VaciarCLASE_ORDEN(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_clase_orden_MDL();
        }
        public void IngresaCLASE_ORDEN(EntityConnectionStringBuilder connection, CLASE_ORDEN um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_clase_orden_MDL(um.AUART,
                                           um.AUTYP,
                                           um.TXT_ES,
                                           um.TXT_EN);
        }
        public void VaciarCENTRO_PLANIFICACION(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_centro_planificacion_MDL();
        }
        public void IngresaCENTRO_PLANIFICACION(EntityConnectionStringBuilder connection, CENTRO_PLANIFICACION um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_centro_planificacion_MDL(um.IWERK);
        }
        public void VaciarGRUPOS_PLANIFICACION(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_grupo_planificacion_MDL();
        }
        public void IngresaGRUPOS_PLANIFICACION(EntityConnectionStringBuilder connection, GRUPOS_PLANIFICACION um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_grupo_planificacion_MDL(um.INGRP,
                                                   um.IWERK);
        }
        public void VaciarPUESTO_TRABAJO(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_puesto_trabajo_MDL();
        }
        public void IngresaPUESTO_TRABAJO(EntityConnectionStringBuilder connection, PUESTO_TRABAJO um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_puesto_trabajo_MDL(um.VERWE,
                                              um.WERKS,
                                              um.ARBPL,
                                              um.KTEXT_UP_ES,
                                              um.KTEXT_UP_EN,
                                              um.KTEXT_ES,
                                              um.KTEXT_EN);
        }
        public void VaciarACTIVIDAD_ORDEN(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_actividad_orden_MDL();
        }
        public void IngresaACTIVIDAD_ORDEN(EntityConnectionStringBuilder connection, ACTIVIDAD_ORDEN um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_actividad_orden_MDL(um.AUART,
                                               um.ILART,
                                               um.ILATX_ES,
                                               um.ILATX_EN);
        }
        public void VaciarPRIORIDAD(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_prioridad_MDL();
        }
        public void IngresaPRIORIDAD(EntityConnectionStringBuilder connection, PRIORIDAD um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_prioridad_MDL(um.ARTPR,
                                         um.PRIOK,
                                         um.PRIOKX_ES,
                                         um.PRIOKX_EN);
        }
        public void VaciarCONTROL_OPERACION(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_control_operacion_MDL();
        }
        public void IngresaCONTROL_OPERACION(EntityConnectionStringBuilder connection, CONTROL_OPERACION um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_control_operacion_MDL(um.STEUS,
                                                 comilla(um.TXT_ES),
                                                 comilla(um.TXT_EN));
        }
        public void VaciarUNIDADES_MEDIDA(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_unidades_medida_MDL();
        }
        public void IngresaUNIDADES_MEDIDA(EntityConnectionStringBuilder connection, UNIDADES_MEDIDA um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_unidades_medida_MDL(um.MSEHI,
                                               um.MSEHL_ES,
                                               um.MSEHL_EN,
                                               um.DECIMALES);
        }
    }
}
