using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteAvisosCalidad
    {
        #region Instacia
        private static DALC_ReporteAvisosCalidad instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteAvisosCalidad ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteAvisosCalidad();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTReporteAvisosCalidad(EntityConnectionStringBuilder connection, ReporteActualizaAvisoCalidad cal)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_avisos_calidad_textos_rep_MDL(cal.FOLIO_SAM);
        }
        public void IngresarReporteAvisosCalidad(EntityConnectionStringBuilder connection, ReporteActualizaAvisoCalidad cal)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_reporte_avisos_calidad_textos_MDL(cal.FOLIO_SAM,
                                                             cal.NOTIF_NO,
                                                             cal.TASK_KEY,
                                                             cal.TASK_CODE,
                                                             cal.UNAME,
                                                             cal.DATUM,
                                                             cal.UZEIT,
                                                             cal.RECIBIDO,
                                                             cal.PROCESADO,
                                                             cal.MENSAJE,
                                                             cal.HORA_RECIBIDO,
                                                             cal.FECHA_RECIBIDO,
                                                             cal.WERKS);
        }
        public void ActualizaReporteAvisosCalidad(EntityConnectionStringBuilder connection, ReporteActualizaAvisoCalidad cal)
        {
            var context = new samEntities(connection.ToString());
            if(cal.MENSAJE.Equals("Se grabaron los resultados."))
            {
                context.DELETE_reporte_avisos_calidad_textos_MDL(cal.FOLIO_SAM);
            }
            else
            {
                context.UPDATE_reporte_avisos_calidad_textos_MDL(cal.FOLIO_SAM,
                                                                 cal.PROCESADO,
                                                                 cal.MENSAJE);
            }
        }
    }
}
