using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_TipoMovi
    {
        #region Instacia
        private static DALC_TipoMovi instance = null;
        private static readonly object padlock = new object();

        public static DALC_TipoMovi ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_TipoMovi();
                }
                return instance;
            }
        }
        #endregion
        public void IngresarTipoMov(EntityConnectionStringBuilder connection, TiposMovimientos tp)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tipo_movimientos_MDL(tp.BWART,
                                                tp.BTEXT);
        }
        public void ActualizarTipoMov(EntityConnectionStringBuilder connection, TiposMovimientos tp)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_tipo_movimientos_MDL(tp.BWART,
                                                tp.BTEXT);
        }
    }
}
