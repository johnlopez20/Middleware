using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReportesReservas
    {
        #region Instancia
        private static DALC_ReportesReservas instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReportesReservas ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReportesReservas();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteReservas(EntityConnectionStringBuilder connection, ReporteReservas re)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reportes_reservas_MDL(re.FOLIO_SAM,
                                                 re.WERKS);
        }
        public void IngresaReportesReservas(EntityConnectionStringBuilder connection, ReporteReservas re)
        {
            var context = new samEntities(connection.ToString());
            context.reportes_reservas_MDL(re.FOLIO_SAM,
                                          re.TABIX,
                                          re.FOLIO_SAP,
                                          re.DATUM,
                                          re.UZEIT,
                                          re.WERKS,
                                          re.BWART,
                                          re.LGORT,
                                          re.KOSTL,
                                          re.AUFNR,
                                          re.RECIBIDO,
                                          re.PROCESADO,
                                          re.ERROR);
        }
        public void ActualizaReportesReservas(EntityConnectionStringBuilder connection, ReporteReservas re)
        {
            var context = new samEntities(connection.ToString());
            if(re.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reportes_reservas_reportes_MDL(re.FOLIO_SAM,
                                                              re.PROCESADO,
                                                              re.ERROR);
            }
            else
            {
                context.DELETE_reportes_reservas_reporte_MDL(re.FOLIO_SAM);
            }
        }
    }
}
