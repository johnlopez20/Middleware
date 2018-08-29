using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_GuardaAvisos
    {
        #region Instancia
        private static DALC_GuardaAvisos instance = null;
        private static readonly object padlock = new object();

        public static DALC_GuardaAvisos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_GuardaAvisos();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_avisos_datos_id_MDL_Result> ObtenerDatosIdAvisos(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_avisos_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_avisos_MDL_Result> ObtenerTodoFolioAvisos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_avisos_MDL(folio_sam);
        }
        public IEnumerable<SELECT_avisos_valida_hora_MDL_Result> ObtenerValidacionHoraAvisos(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_avisos_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELEC_fol_avisos_menos_MDL_Result> ObtenerFolioMenosAvisos(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_avisos_menos_MDL(id);
        }
        public IEnumerable<SELECT_cabecera_avisos_crea_lista_MDL_Result> ObtenerListaFolios(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_avisos_crea_lista_MDL(fecha,
                                                                 hora);
        }
        public IEnumerable<SELECT_cabecera_avisos_crea_Folio_MDL_Result> ObtenerCabAvisosFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_avisos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_qmm_avisos_crea_Folio_MDL_Result> ObtenerQmFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_qmm_avisos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_texto_avisos_crea_Folio_MDL_Result> ObtenerTextoFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_texto_avisos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_avisos_crea_MDL_Result> ObtenerCabAvisosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_avisos_crea_MDL();
        }
        public IEnumerable<SELECT_qmm_avisos_crea_MDL_Result> ObtenerQmAvisosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_qmm_avisos_crea_MDL();
        }
        public IEnumerable<SELECT_texto_avisos_crea_MDL_Result> ObtenerTextAvisosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_texto_avisos_crea_MDL();
        }
        public void ActualizaCabAvisosCrea(EntityConnectionStringBuilder connection, CabAvisosCrea cabavi)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_avisos_crea_MDL(cabavi.FOLIO_SAM,
                                                    cabavi.RECIBIDO);
        }
        public void ActualizarAvisoCreacion(EntityConnectionStringBuilder connection, string folio_sam, string mensaje)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_AVISOS_FOL_MDL(folio_sam,
                                          mensaje);
        }
        public void EliminarRegistroAvisos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_AVISOS_FOL_MDL(folio_sam);
        }
    }
}
