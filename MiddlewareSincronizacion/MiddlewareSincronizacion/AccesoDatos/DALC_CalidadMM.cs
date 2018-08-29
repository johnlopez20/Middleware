using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_CalidadMM
    {
        #region Instacia
        private static DALC_CalidadMM instance = null;
        private static readonly object padlock = new object();

        public static DALC_CalidadMM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_CalidadMM();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_lotemov_id_MDL_Result> ObtenerDatosIdLoteMM(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_lotemov_id_MDL(id);
        }
        public IEnumerable<SELECT_lotemov_valida_hora_MDL_Result> ObtenerValidacionHoraLoteMM(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lotemov_valida_hora_MDL(id,
                                                          hora);
        }
        public IEnumerable<SELEC_fol_lotemov_menos_MDL_Result> ObtenerFolioMenosLoteMM(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_lotemov_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_lotemov_MDL_Result> ObtenerTodoFolioLoteMM(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_lotemov_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_lotes_inspeccion_movimientos_crea_li_MDL_Result> ObtenerListaLotes(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_lotes_inspeccion_movimientos_crea_li_MDL(fecha,
                                                                                      hora);
        }
        public IEnumerable<SELECT_cabecera_lotes_inspeccion_movimientos_crea_Folio_MDL_Result> ObtenerCabLotInsFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context =  new samEntities(connection.ToString());
            return context.SELECT_cabecera_lotes_inspeccion_movimientos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_lotes_inspeccion_movimientos_crea_Folio_MDL_Result> ObtenerPosLotFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_lotes_inspeccion_movimientos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_lotes_inspeccion_movimientos_crea_MDL_Result> ObtenerCabeceraLotInsMov(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_lotes_inspeccion_movimientos_crea_MDL();
        }
        public IEnumerable<SELECT_posiciones_lotes_inspeccion_movimientos_crea_MDL_Result> ObtenerPosicionesLotInsMov(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_lotes_inspeccion_movimientos_crea_MDL();
        }
        public void ActulizaCalidadMovMat(EntityConnectionStringBuilder connection, CalidadMovimientosMateriales camo)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_lotes_inspeccion_movimientos_crea_MDL(camo.FOLIO_SAM,
                                                                          camo.RECIBIDO);
        }
        public void ActualizaCalidadPosMov(EntityConnectionStringBuilder connection, CalidadMMPos pomov)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_lotes_inspeccion_movimientos_crea_MDL(pomov.FOLIO_SAM,
                                                                            pomov.RECIBIDO);
        }
    }
}
