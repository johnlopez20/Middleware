using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.Objects;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_MM_MovMat
    {
        #region Instancia
        private static DALC_MM_MovMat instance = null;
        private static readonly object padlock = new object();

        public static DALC_MM_MovMat ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_MM_MovMat();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_mov_id_MDL_Result> ObtenerDatosId(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_mov_id_MDL(id);
        }
        public IEnumerable<SELECT_mov_valida_hora_MDL_Result> ObtenerValidacionHora(EntityConnectionStringBuilder connection, int id,  string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_mov_valida_hora_MDL(id,
                                                      hora);
        }
        public IEnumerable<SELECT_lista_folios_mov_MDL_Result> ObtenerTodoFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_mov_MDL(folio_sam);
        }
        public IEnumerable<SELEC_fol_mov_menos_MDL_Result> ObtenerFolioMenos(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_mov_menos_MDL(id);
        }
        public IEnumerable<SELECT_movimientos_cabecera_crea_MDL_Result> ObtenerListaFolios(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_movimientos_cabecera_crea_MDL();
        }
        public IEnumerable<SELECT_movimientos_cabecera_crea_Fol_MDL_Result> ObtenerCabeceraMov(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_movimientos_cabecera_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_movimientos_detalle_crea_Fol_MDL_Result> ObtenerDetallesMov(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_movimientos_detalle_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_movimientos_detalle_crea_MDL_Result> ObtenerMovDetalleCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_movimientos_detalle_crea_MDL();
        }
        public IEnumerable<SELECT_movimientos_cabecera_crea_MDL_Result> ObtenerMovCabeceraCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_movimientos_cabecera_crea_MDL();
        }
        public void ActualizaMov_cabecera_crea(EntityConnectionStringBuilder connection, Mov_cabecera_crea movcab)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_movimientos_cabecera_crea_MDL(movcab.FOLIO_SAM,
                                                         movcab.RECIBIDO);
        }
        public void ActualizaMov_detalles_crea(EntityConnectionStringBuilder connection, Mov_detalle_crea movdet)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_movimientos_detalle_crea_MDL(movdet.FOLIO_SAM,
                                                        movdet.RECIBIDO);
        }
    }
}
