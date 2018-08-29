using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_StatusOrdenes
    {
        #region Instancia
        private static ACC_StatusOrdenes instance = null;
        private static readonly object padlock = new object();

        public static ACC_StatusOrdenes ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_StatusOrdenes();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<status_ordenesGET_mdl_Result> obtenerStausOrdnes(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.status_ordenesGET_mdl();
        }

    }
}
