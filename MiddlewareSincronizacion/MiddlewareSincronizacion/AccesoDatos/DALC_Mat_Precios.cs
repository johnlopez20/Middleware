using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Mat_Precios
    {
        #region Instacia
        private static DALC_Mat_Precios instance = null;
        private static readonly object padlock = new object();

        public static DALC_Mat_Precios ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Mat_Precios();
                }
                return instance;
            }
        }
        #endregion
        public void InsertarPreciosMat(EntityConnectionStringBuilder connection, mat_precios m)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_precios_material_MDL(m.material,
                                                m.antiguo,
                                                m.centro,
                                                m.texto_breve);
        }
    }
}
