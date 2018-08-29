using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_QMDefectos
    {
        #region Instacia
        private static DALC_QMDefectos instance = null;
        private static readonly object padlock = new object();

        public static DALC_QMDefectos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_QMDefectos();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_defectos_datos_id_MDL_Result> ObtenerDatosIdDefectos(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_defectos_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_defectos_valida_hora_MDL_Result> ObtenerValidacionHoraDefectos(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_defectos_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELEC_fol_defectos_menos_MDL_Result> ObtenerFolioMenosDefectos(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_defectos_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_defectos_MDL_Result> ObtenerTodoFolioDefectos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_defectos_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_defectos_crea_list_MDL_Result> ObtenerListaDefectos(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_defectos_crea_list_MDL(fecha,
                                                                  hora);
        }
        public IEnumerable<SELECT_cabecera_defectos_crea_Folio_MDL_Result> ObtenerCabeceraFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_defectos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_defectos_crea_Folio_MDL_Result> ObtenerPosicionesDefectosFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_defectos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_defectos_crea_Folio_MDL_Result> ObtenerTextosDeFolios(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_defectos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_defectos_crea_MDL_Result> ObtenerCabeceraDefectos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_defectos_crea_MDL();
        }
        public IEnumerable<SELECT_posiciones_defectos_crea_MDL_Result> ObtenerPosicionesDefectos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_defectos_crea_MDL();
        }
        public IEnumerable<SELECT_textos_defectos_crea_MDL_Result> ObtenerTextosDefectos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_defectos_crea_MDL();
        }
        public void ActulizarCabeceraDefectos(EntityConnectionStringBuilder connection, Cabecera_defectos cab)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_defectos_crea_MDL(cab.FOLIO_SAM,
                                                      cab.RECIBIDO);
        }
        public void ActulizarPosicionesCrea(EntityConnectionStringBuilder connection, Posiciones_defectos pos)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_defectos_crea_MDL(pos.FOLIO_SAM,
                                                        pos.RECIBIDO);
        }
    }
}
