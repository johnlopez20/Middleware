using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_ClaseOrdenPP
    {
        #region Instancia
        private static ACC_ClaseOrdenPP instance = null;
        private static readonly object padlock = new object();

        public static ACC_ClaseOrdenPP ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_ClaseOrdenPP();
                }
                return instance;
            }
        }
        #endregion
        public void InsertarClasOrd(EntityConnectionStringBuilder connection, ClaseOrdenPP clas)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.InsertaClaseOrdenPP_mdl(clas.WERKS,
                                               clas.PP_AUFART);
            }
            catch (Exception) { }
        }
        public void TruncaClasOrd(EntityConnectionStringBuilder connection)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.truncaClaseOrden_mdl();
            }
            catch (Exception) { }
        }
        public IEnumerable<SELECT_Clase_Orden_MDL_Result> claseOrden(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_Clase_Orden_MDL(centro);
        }
        public void DeleteOrdenesPP(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_Ordenes_PP_MDL(centro);
        }
    }
}
