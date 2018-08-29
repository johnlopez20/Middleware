using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Inventarios
    {
        #region Instancia
        private static DALC_Inventarios instance = null;
        private static readonly object padlock = new object();

        public static DALC_Inventarios obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Inventarios();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarInventario(EntityConnectionStringBuilder connection, Inventarios ins)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_inventarios_MDL(ins.WERKS);
        }
        public void IngresarInventarios(EntityConnectionStringBuilder connection, Inventarios ins)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_inventarios_MDL(ins.MATNR,
                                           ins.MAKTX_ES,
                                           ins.MAKTX_EN,
                                           ins.WERKS,
                                           ins.MEINS,
                                           ins.LGORT,
                                           ins.LGOBE,
                                           ins.CLABS,
                                           ins.CINSM,
                                           ins.CSPEM,
                                           ins.CUMLM,
                                           ins.CHARG,
                                           ins.MTART,
                                           ins.MATKL,
                                           ins.SERNR,
                                           ins.XCHPF,
                                           ins.CLABS,
                                           "0",
                                           "0",
                                           ins.CUMLM);
        }
        public void EliminarDatosRepetidos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.SELECT_duplicados_inventario_MDL();
        }
    }
}
