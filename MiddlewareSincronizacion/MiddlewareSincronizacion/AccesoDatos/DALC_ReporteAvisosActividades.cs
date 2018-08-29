using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteAvisosActividades
    {
        #region Instacia
        private static DALC_ReporteAvisosActividades instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteAvisosActividades ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteAvisosActividades();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteAvisosActividades(EntityConnectionStringBuilder connection, ReporteAvisosActividad ra)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_avisos_mod_actividades_rep_MDL(ra.FOLIO_SAM);
        }
        public void IngresarReporteAvisosActividades(EntityConnectionStringBuilder connection, ReporteAvisosActividad ra)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_reporte_avisos_mod_actividades_MDL(ra.FOLIO_SAM,
                                                              ra.QMNUM,
                                                              ra.FECHA,
                                                              ra.HORA,
                                                              ra.RECIBIDO,
                                                              ra.PROCESADO,
                                                              ra.MENSAJE,
                                                              ra.HORA_RECIBIDO,
                                                              ra.FECHA_RECIBIDO,
                                                              ra.UNAME,
                                                              ra.WERKS);
        }
        public void ActualizarReporteAvisosActividades(EntityConnectionStringBuilder connection, ReporteAvisosActividad ra)
        {
            var context = new samEntities(connection.ToString());
            if(ra.MENSAJE.Equals(""))
            {
                context.DELETE_reporte_avisos_mod_actividades_MDL(ra.FOLIO_SAM);
            }
            else
            {
                context.UPDATE_cabecera_modificacion_avisos_sap_crea_rep_MDL(ra.FOLIO_SAM,
                                                                             ra.RECIBIDO,
                                                                             ra.MENSAJE);
            }
        }
    }
}
