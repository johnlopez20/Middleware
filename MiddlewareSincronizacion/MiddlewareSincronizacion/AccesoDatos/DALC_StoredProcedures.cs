using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_StoredProcedures
    {
        #region Instancia
        private static DALC_StoredProcedures instance = null;
        private static readonly object padlock = new object();

        public static DALC_StoredProcedures ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_StoredProcedures();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<centro_MDL_Result> ObtenerCentros(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.centro_MDL();
        }
        public IEnumerable<centros303_MDL_Result> obtenercentros303(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.centros303_MDL();
        }
        public IEnumerable<OBTENER_RFCO_CENTRO_MDL_Result> ObtOut(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            return context.OBTENER_RFCO_CENTRO_MDL(centro);
        }
        public IEnumerable<sincronizacion_output_MDL_Result> sOutput(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.sincronizacion_output_MDL();
        }
        
        public IEnumerable<sincronizacion_input_MDL_Result> sInput(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.sincronizacion_input_MDL();
        }
        public IEnumerable<OBTENER_RFCP_CENTRO_MDL_Result> ObtPro(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            return context.OBTENER_RFCP_CENTRO_MDL(centro);
        }
        public IEnumerable<OBTENER_RFCT_CENTRO_MDL_Result> ObtenerRfcT(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            return context.OBTENER_RFCT_CENTRO_MDL(centro);
        }
        public IEnumerable<sincronizacion_proceso_MDL_Result> Proceso(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.sincronizacion_proceso_MDL();
        }
        public IEnumerable<SELECT_tiempos_O_MDL_Result> ObtenerTiempoO(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_tiempos_O_MDL();
        }
        public IEnumerable<SELECT_tiempos_P_MDL_Result> ObtenerTiempoP(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_tiempos_P_MDL();
        }
        public IEnumerable<SELECT_tiempos_Transferencia_MDL_Result> ObtenerTiemposT(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_tiempos_Transferencia_MDL();
        }
    }

}
