using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_NotTiempos
    {
        #region Instancia
        private static ACC_NotTiempos instance = null;
        private static readonly object padlock = new object();

        public static ACC_NotTiempos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_NotTiempos();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<pos_NotTiempGETPP2_mdl_Result> obtenerPosNotTiempos(EntityConnectionStringBuilder connection, string folio)
        {
            var context = new samEntities(connection.ToString());
            return context.pos_NotTiempGETPP2_mdl(folio);
        }
    }
}
