using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_MovNotPP
    {
        #region Instancia
        private static ACC_MovNotPP instance = null;
        private static readonly object padlock = new object();

        public static ACC_MovNotPP ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_MovNotPP();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<cab_MovNotGETPP_mdl_Result> obtenerCabMovNot(EntityConnectionStringBuilder connection, string folio)
        {
            var context = new samEntities(connection.ToString());
            return context.cab_MovNotGETPP_mdl(folio);
        }
        public IEnumerable<pos_MovNotGETPP_mdl_Result> obtenerPosMovNot(EntityConnectionStringBuilder connection, string folio)
        {
            var context = new samEntities(connection.ToString());
            return context.pos_MovNotGETPP_mdl(folio);
        }
    }
}
