using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteNotificaciones
    {
        #region Instancia
        private static DALC_ReporteNotificaciones instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteNotificaciones ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteNotificaciones();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarReporteNotificaciones(EntityConnectionStringBuilder connection, ReporteNotificaciones um)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reportes_notificaciones_MDL(um.FOLIO_SAM,
                                                       um.WERKS);
        }
        public void IngresarReporteNotificaciones(EntityConnectionStringBuilder connection, ReporteNotificaciones av)
        {
            var context = new samEntities(connection.ToString());
            context.reportes_notificaciones_MDL(av.FOLIO_SAM,
                                                av.VORNR,
                                                av.RMZHL,
                                                av.AUFNR,
                                                av.UZEIT,
                                                av.DATUM,
                                                av.WERKS,
                                                av.PERNR,
                                                av.ISMNW_2,
                                                av.ISMNU,
                                                av.IDAUR,
                                                av.IDAUE,
                                                av.AUERU,
                                                av.LTXA1,
                                                av.ARBEH,
                                                av.DAUNE,
                                                av.FOLIO_SAP,
                                                av.RECIBIDO,
                                                av.PROCESADO,
                                                av.ERROR,
                                                av.FECHA_RECIBIDO,
                                                av.HORA_RECIBIDO);
        }
        public void ActualizaReporteNotificaciones(EntityConnectionStringBuilder connection, ReporteNotificaciones um)
        {
            var context = new samEntities(connection.ToString());
            if(um.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reporte_notificaciones_reportes_MDL(um.FOLIO_SAM,
                                                                   um.PROCESADO,
                                                                   um.ERROR);
            }
            else
            {
                context.DELETE_reporte_notificaciones_reporte_MDL(um.FOLIO_SAM);
            }
        }
    }
}
