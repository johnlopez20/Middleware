using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Connection
    {
        #region Instancia
        private static DALC_Connection instance = null;
        private static readonly object padlock = new object();

        public static DALC_Connection ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Connection();
                }
                return instance;
            }
        }
        #endregion

        public SqlConnection ObtenerConexion(String datos)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(datos);
                connection.Open();
            }
            catch (Exception e) { String s = e.Message; }

            return connection;
        }
        public void TerminarConexion(SqlConnection Connection)
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
