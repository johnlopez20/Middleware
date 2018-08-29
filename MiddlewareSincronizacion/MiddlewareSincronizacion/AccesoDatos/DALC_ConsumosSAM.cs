using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ConsumosSAM
    {
        #region Instancia
        private static DALC_ConsumosSAM instance = null;
        private static readonly object padlock = new object();

        public static DALC_ConsumosSAM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ConsumosSAM();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_consumos_datos_id_MDL_Result> ObtenerDatosIdConsumo(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_consumos_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_consumos_valida_hora_MDL_Result> ObtenerValidacionHoraConsumo(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_consumos_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELEC_fol_consumos_menos_MDL_Result> ObtenerFolioMenosConsumo(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_consumos_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_consumos_MDL_Result> ObtenerTodoFolioConsumo(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_consumos_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_consumos_crea_list_MDL_Result> ObtenerCabeceraConsuLista(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_consumos_crea_list_MDL(fecha,
                                                                 hora);
        }
        public IEnumerable<SELECT_cabecera_consumos_crea_Folio_MDL_Result> ObtenerCabConsumoFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_consumos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_consumos_crea_Folio_MDL_Result> ObtenerPosicionesConsumosFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_consumos_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_consumos_crea_MDL_Result> ObtenerCabConsumosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_consumos_crea_MDL();
        }
        public IEnumerable<SELECT_posiciones_consumos_crea_MDL_Result> ObtenerPosConsumosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_consumos_crea_MDL();
        }
        public void ActualizaCabConsumosCrea(EntityConnectionStringBuilder connection, CabConsumosCrea cabconsumos)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_consumos_crea_MDL(cabconsumos.FOLIO_SAM,
                                                      cabconsumos.RECIBIDO);
        }
        public void ActualizaPosConsumosCrea(EntityConnectionStringBuilder connection, PosConsumosCrea poscon)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_consumos_crea_MDL(poscon.FOLIO_SAM,
                                                        poscon.RECIBIDO);
        }
    }
}
