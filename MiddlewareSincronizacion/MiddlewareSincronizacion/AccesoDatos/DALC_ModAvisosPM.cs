using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ModAvisosPM
    {
        #region Instacia
        private static DALC_ModAvisosPM instance = null;
        private static readonly object padlock = new object();

        public static DALC_ModAvisosPM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ModAvisosPM();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_cabecera_modificacion_avisos_sap_crea_datos_id_MDL_Result> ObtenerDatosIdModAviSAP(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_modificacion_avisos_sap_crea_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_cabecera_modificacion_avisos_sap_crea_valida_hora_MDL_Result> ObtenerHoraAvisoModSAP(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_modificacion_avisos_sap_crea_valida_hora_MDL(id,
                                                                                        hora);
        }
        public IEnumerable<SELECT_fol_cabecera_modificacion_avisos_sap_crea_menos_MDL_Result> ObtenerFolioMenosAviModSAP(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_fol_cabecera_modificacion_avisos_sap_crea_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result> ObtenerListaFoliosAviSAP(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_modificacion_avisos_sap_crea_Folio_MDL_Result> ObtenerDatosCabAvisoSAPFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_modificacion_avisos_sap_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_modificacion_avisos_sap_crea_Folio_MDL_Result> ObtenerDatosPosAviSAPFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_modificacion_avisos_sap_crea_Folio_MDL(folio_sam);
        }
        public void ActualizarCabAviModSAP(EntityConnectionStringBuilder connection, CanAvisosSAPModificaciones cab)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_modificacion_avisos_sap_crea_MDL(cab.FOLIO_SAM,
                                                                     cab.RECIBIDO);
        }
        public void ActualizarPosAviModSAP(EntityConnectionStringBuilder connection, PosAvisosSAPModificaciones pos)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_modificacion_avisos_sap_crea_MDL(pos.FOLIO_SAM,
                                                                       pos.RECIBIDO);
        }
    }
}
