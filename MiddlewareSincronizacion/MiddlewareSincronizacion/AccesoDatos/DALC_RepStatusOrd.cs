using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_RepStatusOrd
    {
        #region Instancia
        private static DALC_RepStatusOrd instance = null;
        private static readonly object padlock = new object();

        public static DALC_RepStatusOrd ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_RepStatusOrd();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarStatusOrdenes(EntityConnectionStringBuilder connection, StatusOrdenes sor)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_status_ordenes_MDL(sor.FOLIO_SAM,
                                              sor.WERKS);
        }
        public void IngresaStatusOrdenes(EntityConnectionStringBuilder connection, StatusOrdenes so)
        {
            var context = new samEntities(connection.ToString());
            context.status_ordenes_MDL(so.FOLIO_ORD,
                                       so.FOLIO_SAM,
                                       so.DATUM,
                                       so.UZEIT,
                                       so.AUFNR,
                                       so.OPERACION,
                                       so.RECIBIDO,
                                       so.PROCESADO,
                                       so.MENSAJE,
                                       so.WERKS);
        }
        public void ActualizaStatusOrdenes(EntityConnectionStringBuilder connection, StatusOrdenes so)
        {
            var context = new samEntities(connection.ToString());
            if(so.FOLIO_ORD.Equals(""))
            {

            }
            else
            {

            }
        }
    }
}
