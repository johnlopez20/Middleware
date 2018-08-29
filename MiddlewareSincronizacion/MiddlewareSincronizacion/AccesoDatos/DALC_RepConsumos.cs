using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_RepConsumos
    {
        #region Instancia
        private static DALC_RepConsumos instance = null;
        private static readonly object padlock = new object();

        public static DALC_RepConsumos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_RepConsumos();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarConsumos(EntityConnectionStringBuilder connection, Consumos con)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_consumos_cabecera_rep_MDL(con.FOLIO_SAM,
                                                     con.WERKS);
        }
        public void IngresaConsumos(EntityConnectionStringBuilder connection, Consumos co)
        {
            var context = new samEntities(connection.ToString());
            context.consumos_cabecera_rep_MDL(co.FOLIO_SAP,
                                              co.FOLIO_SAM,
                                              co.FOLIO_ORD,
                                              co.AUFNR,
                                              co.WERKS,
                                              co.UZEIT,
                                              co.DATUM,
                                              co.RECIBIDO,
                                              co.PROCESADO,
                                              co.ERROR);
        }
        public void ActualizaConsumos(EntityConnectionStringBuilder connection, Consumos con)
        {
            var context = new samEntities(connection.ToString());
            if(con.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reportes__consumos_cabecera_rep_reportes_MDL(con.FOLIO_SAM,
                                                                            con.PROCESADO,
                                                                            con.ERROR);
            }
            else
            {
                context.DELETE_reportes_consumos_cabecera_rep_reporte_MDL(con.FOLIO_SAM);
            }
        }
    }
}
