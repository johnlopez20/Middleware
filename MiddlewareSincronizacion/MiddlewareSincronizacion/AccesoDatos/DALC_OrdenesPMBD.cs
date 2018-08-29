using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_OrdenesPMBD
    {
        #region Instacia
        private static DALC_OrdenesPMBD instance = null;
        private static readonly object padlock = new object();

        public static DALC_OrdenesPMBD ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_OrdenesPMBD();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_ordenes_datos_id_MDL_Result> ObtenerDatosIdOrden(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_ordenes_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_ordenes_valida_hora_MDL_Result> ObtenerValidacionHoraOrden(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_ordenes_valida_hora_MDL(id,
                                                          hora);
        }
        public IEnumerable<SELEC_fol_ordenes_menos_MDL_Result> ObtenerFolioMenosOrden(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_ordenes_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_ordenes_MDL_Result> ObtenerTodoFolioOrden(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_ordenes_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_ordenes_crea_list_MDL_Result> ObtenerOrdenesLista(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_ordenes_crea_list_MDL(fecha,
                                                                 hora);
        }
        public IEnumerable<SELECT_operaciones_ordenes_crea_Folio_MDL_Result> ObtenerOperacionesFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_operaciones_ordenes_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_servicios_ordenes_crea_Folio_MDL_Result> ObtenerServiciosFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_servicios_ordenes_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_materiales_ordenes_crea_Folio_MDL_Result> ObtenerMaterialesFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_materiales_ordenes_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_ordenes_crea_Folio_MDL_Result> ObtenerCabFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_ordenes_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_texto_posicion_ordenes_crea_Folio_MDL_Result> ObtenerTextoPosFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_texto_posicion_ordenes_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_operaciones_ordenes_crea_MDL_Result> ObtenerOperacionesOrdenesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_operaciones_ordenes_crea_MDL();
        }
        public IEnumerable<SELECT_servicios_ordenes_crea_MDL_Result> ObtenerServiciosOrdenesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_servicios_ordenes_crea_MDL();
        }
        public IEnumerable<SELECT_materiales_ordenes_crea_MDL_Result> ObtenerMaterialesOrdenesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_materiales_ordenes_crea_MDL();
        }
        public IEnumerable<SELECT_cabecera_ordenes_crea_MDL_Result> ObtenerCabeceraOrdenesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_ordenes_crea_MDL();
        }
        public void ActualizaCabOrdenesCrea(EntityConnectionStringBuilder connection, CabOrdenesCrea cabord)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_ordenes_crea_MDL(cabord.FOLIO_SAM,
                                                     cabord.RECIBIDO);
        }
        public IEnumerable<SELECT_texto_posicion_ordenes_crea_MDL_Result> ObetenerTextoPosicionOrdenesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_texto_posicion_ordenes_crea_MDL();
        }
    }
}

