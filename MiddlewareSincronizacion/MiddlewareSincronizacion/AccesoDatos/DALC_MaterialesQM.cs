using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_MaterialesQM
    {
        #region Instancia
        private static DALC_MaterialesQM instance = null;
        private static readonly object padlock = new object();

        public static DALC_MaterialesQM ObtenerIntancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_MaterialesQM();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTablaMaterialesQM(EntityConnectionStringBuilder connection, MaterialesQM mq)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_materiales_planes_inspeccion_MDL(mq.MATNR,
                                                            mq.WERKS);
        }
        public void IngresarMaterialesQM(EntityConnectionStringBuilder connection, MaterialesQM mq)
        {
            var context = new samEntities(connection.ToString());
            context.materiales_planes_inspeccion_MDL(mq.MATNR,
                                                     mq.WERKS,
                                                     mq.PLNTY,
                                                     mq.PLNNR,
                                                     mq.PLNAL,
                                                     mq.ZKRIZ,
                                                     mq.ZAEHL,
                                                     mq.SPRAS,
                                                     mq.MAKTX,
                                                     mq.LOEKZ,
                                                     mq.VERWE,
                                                     mq.STATU);
        }
    }
}
