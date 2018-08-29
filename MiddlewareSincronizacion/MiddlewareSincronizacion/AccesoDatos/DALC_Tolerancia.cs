using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Tolerancia
    {
        #region Instancia
        private static DALC_Tolerancia instance = null;
        private static readonly object padlock = new object();

        public static DALC_Tolerancia ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Tolerancia();
                }
                return instance;
            }
        }
        #endregion
        public void ActualizaTolerancia(EntityConnectionStringBuilder connection, Tolerancia tol)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_tolerancia_MDL(tol.WERKS,
                                          tol.TOLERANCIA);
        }
        public void ActualizarToleranciaPedido(EntityConnectionStringBuilder connection, Tolerancia tot)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_tolerancia_pedidos_MDL(tot.WERKS,
                                                  tot.EBELN,
                                                  tot.EBELP,
                                                  tot.TOLERANCIA);
        }
        public void IngresarToleranciaPedido(EntityConnectionStringBuilder connection, Tolerancia tot)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tolerancia_pedidos_MDL(tot.WERKS,
                                                  tot.EBELN,
                                                  tot.EBELP,
                                                  tot.TOLERANCIA);
        }
    }
}
