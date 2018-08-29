using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_AvisosCalidad
    {
        #region Instacia
        private static DALC_AvisosCalidad instance = null;
        private static readonly object padlock = new object();

        public static DALC_AvisosCalidad ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_AvisosCalidad();
                }
                return instance;
            }
        }
        #endregion
        public void DeleteAvisosCalidad(EntityConnectionStringBuilder connection, AvisosCalidad ac)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.DELETE_avisos_calidad_vis_MDL(ac.NOTIF_NO);
            }
            catch (Exception) { }
        }
        public void InsertAvisosCalidad(EntityConnectionStringBuilder connection, AvisosCalidad ac)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_avisos_calidad_vis_MDL(ac.NOTIF_NO,
                                                  ac.TASK_KEY,
                                                  ac.TASK_CAT_TYP,
                                                  ac.TASK_CODEGRP,
                                                  ac.TASK_CODE,
                                                  ac.TASK_TEXT,
                                                  ac.CREATED_BY,
                                                  ac.CREATED_DATE,
                                                  ac.CHANGED_BY,
                                                  ac.CHANGED_DATE,
                                                  ac.PLND_START_DATE,
                                                  ac.PLND_END_DATE,
                                                  ac.OBJECT_NO,
                                                  ac.LONG_TEXT,
                                                  ac.PRILANG,
                                                  ac.PLND_START_TIME,
                                                  ac.PLND_END_TIME,
                                                  ac.CARRIED_OUT_BY,
                                                  ac.CARRIED_OUT_DATE,
                                                  ac.CARRIED_OUT_TIME,
                                                  ac.RESUBMIT_DATE,
                                                  ac.ITEM_KEY,
                                                  ac.CREATED_TIME,
                                                  ac.CHANGED_TIME,
                                                  ac.PARTN_ROLE,
                                                  ac.PARTNER,
                                                  ac.DELETE_FLAG,
                                                  ac.TASK_SORT_NO,
                                                  ac.TXT_TASKGRP,
                                                  ac.TXT_TASKCD,
                                                  ac.STATUS,
                                                  ac.USERSTATUS_FLAG,
                                                  ac.USERSTATUS);
        }
        public void DeleteTextosAvisoCalidad(EntityConnectionStringBuilder connection, TextosAvisosCalidad tac)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_textos_avisos_calidad_vis_MDL(tac.NOTIF_NO);
        }
        public void InsertTextosAvisosCalidad(EntityConnectionStringBuilder connection, TextosAvisosCalidad tac)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_textos_avisos_calidad_vis_MDL(tac.NOTIF_NO,
                                                         tac.TASK_KEY,
                                                         tac.TASK_SORT_NO,
                                                         tac.LINEA,
                                                         tac.TEXTO,
                                                         tac.FOIO_SAM);
        }
    }
}
