using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_MMEntradasSam
    {
        #region Instancia
        private static DALC_MMEntradasSam instance = null;
        private static readonly object padlock = new object();

        public static DALC_MMEntradasSam ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_MMEntradasSam();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_entrada_id_MDL_Result> ObtenerDatosIdEntrada(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_entrada_id_MDL(id);
        }
        public IEnumerable<SELECT_entrada_valida_hora_MDL_Result> ObtenerValidacionHoraEntrada(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_entrada_valida_hora_MDL(id,
                                                          hora);
        }
        public IEnumerable<SELECT_lista_folios_entrada_MDL_Result> ObtenerTodoFolioEntrada(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_entrada_MDL(folio_sam);
        }
        public IEnumerable<SELEC_fol_entrada_menos_MDL_Result> ObtenerFolioMenosEntrada(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_entrada_menos_MDL(id);
        }
        public IEnumerable<S_entrada_servicios_crea_MDL_Result> ObtenerEntradaServiciosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.S_entrada_servicios_crea_MDL();
        }
        public IEnumerable<SELECT_entrada_servicios_crea_F_MDL_Result> ObtenerFoliosLista(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_entrada_servicios_crea_F_MDL(fecha,
                                                               hora);
        }
        public IEnumerable<SELECT_entrada_servicios_crea_Todo_MDL_Result> ObtenerEntradas(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_entrada_servicios_crea_Todo_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_entrada_servicios_Fol_MDL_Result> ObtenerTextosEn(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_entrada_servicios_Fol_MDL(folio_sam);
        }
        public void ActualizaEntradaServiciosCrea(EntityConnectionStringBuilder connection, EntradaServiciosCrea en)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_entrada_servicios_crea_MDL(en.FOLIO_SAM,
                                                      en.RECIBIDO);
        }
        public IEnumerable<SELECT_textos_entrada_servicios_MDL_Result> ObtenerTextosEntradaServiciosCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_entrada_servicios_MDL();
        }
        public void ElimanarRegistroCreacion(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_Entrada_FOL_MM_MDL(folio_sam);
        }
        public void ActualizarResultado(EntityConnectionStringBuilder connection, string folio_sam, string folio_sap, string mensaje)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Entradas_MM_MDL(folio_sam,
                                           folio_sap,
                                           mensaje);
        }
    }
}
