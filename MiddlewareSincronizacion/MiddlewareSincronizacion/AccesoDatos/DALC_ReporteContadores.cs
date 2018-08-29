using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteContadores
    {
        #region Instacia
        private static DALC_ReporteContadores instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteContadores ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteContadores();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteContadores(EntityConnectionStringBuilder connection, ReporteContadores um)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_contadores_MDL(um.FOLIO_SAM,
                                                  um.WERKS);
        }
        public void IngresarReporteContadores(EntityConnectionStringBuilder connection, ReporteContadores um)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_reporte_contadores_MDL(um.FOLIO_SAM,
                                                  um.VALOR_CNT,
                                                  um.FOLIO_SAP,
                                                  um.DATUM,
                                                  um.UZEIT,
                                                  um.NIVEL,
                                                  um.ICONO,
                                                  um.EQUNR,
                                                  um.EQKTX,
                                                  um.POINT,
                                                  um.MDOCM,
                                                  um.NRECDV,
                                                  um.NREADG,
                                                  um.RECDU,
                                                  um.TPLNR,
                                                  um.MATNR,
                                                  um.WERKS,
                                                  um.SERNR,
                                                  um.LGORT,
                                                  um.CHARG,
                                                  um.RECIBIDO,
                                                  um.PROCESADO,
                                                  um.ERROR,
                                                  um.FECHA_RECIBIDO,
                                                  um.HORA_RECIBIDO,
                                                  um.UNAME);
        }
        public void ActulizarReporteContadores(EntityConnectionStringBuilder connection, ReporteContadores um)
        {
            var context = new samEntities(connection.ToString());
            if(um.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reporte_contadores_reportes_MDL(um.FOLIO_SAM,
                                                           um.PROCESADO,
                                                           um.ERROR);
            }
            else
            {
                context.DELETE_reporte_contadores_crea_MDL(um.FOLIO_SAM);
            }
            
        }
    }
}
