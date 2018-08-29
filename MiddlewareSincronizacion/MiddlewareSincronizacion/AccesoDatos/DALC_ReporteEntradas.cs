using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteEntradas
    {
        #region Instancia
        private static DALC_ReporteEntradas instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteEntradas ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteEntradas();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteEntradas(EntityConnectionStringBuilder connection, ReporteEntradas re)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reportes_entradas_MDL(re.FOLIO_SAM,
                                                 re.WERKS);
        }
        public void IngresaReporteEntradas(EntityConnectionStringBuilder connection, ReporteEntradas re)
        {
            var context = new samEntities(connection.ToString());
            context.reportes_entradas_MDL(re.FOLIO_SAM,
                                          re.EBELN,
                                          re.EBELP,
                                          re.WERKS,
                                          re.SERVICE,
                                          re.QUANTITY,
                                          re.SHORTTEX,
                                          re.GR_PRICE,
                                          re.PRICEUNI,
                                          re.DATUM,
                                          re.UZEIT,
                                          re.FOLIO_SAP,
                                          re.RECIBIDO,
                                          re.PROCESADO,
                                          re.ERROR);
        }
        public void ActualizaReporteEntradas(EntityConnectionStringBuilder connection, ReporteEntradas re)
        {
            var context = new samEntities(connection.ToString());
            if (re.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reportes_entradas_reportes_MDL(re.FOLIO_SAM,
                                                              re.PROCESADO,
                                                              re.ERROR);
            }
            else
            {
                context.DELETE_reportes_entradas_reportes_MDL(re.FOLIO_SAM);
            }
        }
    }
}
