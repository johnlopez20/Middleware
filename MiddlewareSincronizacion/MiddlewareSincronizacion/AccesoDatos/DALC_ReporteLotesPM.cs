using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteLotesPM
    {
        #region Instacia
        private static DALC_ReporteLotesPM instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteLotesPM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteLotesPM();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteLotesPM(EntityConnectionStringBuilder connection, ReporteLotesPM rl)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_lotes_inspeccion_PM_rep_MDL(rl.FOLIO_SAM);
        }
        public void IngresarLotesPM(EntityConnectionStringBuilder connection, ReporteLotesPM rl)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_reporte_lotes_inspeccion_PM_MDL(rl.FOLIO_SAM,
                                                           rl.PRUEFLOS,
                                                           rl.DATUM,
                                                           rl.UZEIT,
                                                           rl.AUFNR,
                                                           rl.VORNR,
                                                           rl.KTEXTLOS,
                                                           rl.WERK,
                                                           rl.ERSTELLER,
                                                           rl.ENSTEHDAT,
                                                           rl.ENTSTEZEIT,
                                                           rl.AENDERER,
                                                           rl.AENDERDAT,
                                                           rl.AENDERZEIT,
                                                           rl.RECIBIDO,
                                                           rl.PROCESADO,
                                                           rl.MENSAJE,
                                                           rl.FECHA_RECIBIDO,
                                                           rl.HORA_RECIBIDO,
                                                           rl.PRUEFER,
                                                           rl.UNAME,
                                                           rl.FOLIO_ORD);
        }
        public void ActualizarLotesPMcrea(EntityConnectionStringBuilder connection, ReporteLotesPM rl)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                if (rl.MENSAJE.Equals("Se grabaron los resultados."))
                {
                    context.DELETE_reporte_lotes_inspeccion_PM_MDL(rl.FOLIO_SAM);
                }
                else
                {
                    context.UPDATE_reporte_lotes_inspecciones_PM_MDL(rl.FOLIO_SAM,
                                                                     rl.PROCESADO,
                                                                     rl.MENSAJE);
                }
            }
            catch (Exception) { }
        }
    }
}
