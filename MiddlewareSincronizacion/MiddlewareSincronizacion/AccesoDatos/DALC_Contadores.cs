using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Contadores
    {
        #region Instancia
        private static DALC_Contadores instance = null;
        private static readonly object padlock = new object();

        public static DALC_Contadores ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Contadores();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_contadores_datos_id_MDL_Result> ObtenerDatosIdContador(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_contadores_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_contador_valida_hora_MDL_Result> ObtenerValidacionHoraContador(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_contador_valida_hora_MDL(id,
                                                           hora);
        }
        public IEnumerable<SELEC_fol_contador_menos_MDL_Result> ObtenerFolioMenosContador(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_contador_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_contadores_MDL_Result> ObtenerTodoFolioContadores(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_contadores_MDL(folio_sam);
        }
        public IEnumerable<SELECT_contadores_crea_list_MDL_Result> ObtenerListaContadores(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_contadores_crea_list_MDL(fecha,
                                                           hora);
        }
        public IEnumerable<SELECT_contadores_crea_Folio_MDL_Result> ObtenerContadoresFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_contadores_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<sincronizacion_input_MDL_Result> ObtenerSincronizacionInput(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.sincronizacion_input_MDL();
        }
        public IEnumerable<SELECT_contadores_crea_MDL_Result> ObtenerContadores(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_contadores_crea_MDL();
        }
        public void ActulizarContadores(EntityConnectionStringBuilder connection, Contadores con)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_contadores_crea_MDL(con.FOLIO_SAM,
                                               con.RECIBIDO);
        }
    }
}
