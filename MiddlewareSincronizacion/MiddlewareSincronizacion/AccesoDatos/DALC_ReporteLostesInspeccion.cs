using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteLostesInspeccion
    {
        #region Instacia
        private static DALC_ReporteLostesInspeccion instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteLostesInspeccion ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteLostesInspeccion();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteLotesInspeccion(EntityConnectionStringBuilder connection, ReporteLotesInsp ri)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_lotesinspeccion_MDL(ri.FOLIO_SAM);
        }
        public void InsertReporteLotesInspeccion(EntityConnectionStringBuilder connection, ReporteLotesInsp ri)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_reporte_lotesinspeccion_MDL(ri.FOLIO_SAM,
                                                       ri.PRUEFLOS,
                                                       ri.DATUM,
                                                       ri.UZEIT,
                                                       ri.MATNR,
                                                       ri.MAKTX,
                                                       ri.EBELN,
                                                       ri.FOLIO_MOV,
                                                       ri.MBLNR,
                                                       ri.WERKS,
                                                       ri.ERSTELLER,
                                                       ri.RECIBIDO,
                                                       ri.PROCESADO,
                                                       ri.MENSAJE,
                                                       ri.FECHA_RECIBIDO,
                                                       ri.HORA_RECIBIDO,
                                                       ri.PRUEFER,
                                                       ri.UNAME);
        }
        public void ActualizarReporteLotesInspeccion(EntityConnectionStringBuilder connection, ReporteLotesInsp ri)
        {
            var context = new samEntities(connection.ToString());
            if(ri.PRUEFLOS.Equals(""))
            {
                context.UPDATE_reporte_lotesinspeccion_MDL(ri.FOLIO_SAM,
                                                           ri.PROCESADO,
                                                           ri.MENSAJE);
            }
            else
            {
                context.DELETE_reporte_lotesinspeccion_rep_MDL(ri.FOLIO_SAM);
            }
        }
    }
}
