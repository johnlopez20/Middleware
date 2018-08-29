using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteDMS
    {
        #region Instacia
        private static DALC_ReporteDMS instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteDMS ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteDMS();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteDMS(EntityConnectionStringBuilder connection, ReporteDMS dms)
        {
            var context = new samEntities(connection.ToString());
            //context.DELETE_reporte_dms_rep_MDL(dms.FOLIO_DMS);
        }
        public void IngresarReporteDMS(EntityConnectionStringBuilder connection, ReporteDMS dms)
        {
            var context = new samEntities(connection.ToString());
            //context.INSERT_reporte_dms_MDL(dms.FOLIO_DMS,
            //                               dms.TABIX,
            //                               dms.DOCNUMBER,
            //                               dms.DATUM,
            //                               dms.UZEIT,
            //                               dms.WSAPPLICATION,
            //                               dms.OBJECTTYPE,
            //                               dms.DOCUMENTTYPE,
            //                               dms.DESCRIPTION,
            //                               dms.UNAME,
            //                               dms.DOCFILE,
            //                               dms.RECIBIDO,
            //                               dms.PROCESADO,
            //                               dms.MENSAJE,
            //                               dms.FECHA_RECIBIDO,
            //                               dms.HORA_RECIBIDO,
            //                               dms.WERKS);
        }
        public void ActualizarReporteDMS(EntityConnectionStringBuilder connection, ReporteDMS dms)
        {
            var context = new samEntities(connection.ToString());
            if(dms.FOLIO_DMS.Equals(""))
            {
                //context.UPDATE_reporte_dms_MDL(dms.FOLIO_DMS,
                //                               dms.PROCESADO,
                //                               dms.MENSAJE);
            }
            else
            {
                //context.DELETE_reporte_dms_MDL(dms.FOLIO_DMS);
            }
        }
    }
}
