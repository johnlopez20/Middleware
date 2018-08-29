using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteMovimientos
    {
        #region Instancia
        private static DALC_ReporteMovimientos instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteMovimientos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteMovimientos();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarReporteMovimientos(EntityConnectionStringBuilder connection, ReporteMovimientos um)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reportes_movimientos_MDL(um.FOLIO_SAM,
                                                    um.WERKS);
        }
        public void IngresarReporteMovimientos(EntityConnectionStringBuilder connection, ReporteMovimientos av)
        {
            var context = new samEntities(connection.ToString());
            context.reportes_movimientos_MDL(av.FOLIO_SAM,
                                             av.UZEIT,
                                             av.DATUM,
                                             av.FOLIO_SAP,
                                             av.WERKS,
                                             av.LGORT,
                                             av.ANULA,
                                             av.TEXTO,
                                             av.TEXCAB,
                                             av.TIPO,
                                             av.UMLGO,
                                             av.FACTURA,
                                             av.PEDIM,
                                             av.LFSNR,
                                             av.HU,
                                             av.RECIBIDO,
                                             av.PROCESADO,
                                             av.ERROR,
                                             av.IBORRADO);
        }
        public void ActualizaReporteMovimientos(EntityConnectionStringBuilder connection, ReporteMovimientos um)
        {
            var context = new samEntities(connection.ToString());
            if(um.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reportes__movimientos_reportes_MDL(um.FOLIO_SAM,
                                                                  um.PROCESADO,
                                                                  um.ERROR);
            }
            else
            {
                context.DELETE_reportes_movimientos_reporte_MDL(um.FOLIO_SAM);
            }
        }
        public IEnumerable<SELECT_Errores_Movimientos_MDL_Result> ObtenerErrores(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_Errores_Movimientos_MDL();
        }
        public IEnumerable<SELECT_lista_mov_eliminar_MDL_Result> ObtenerListaEliminar(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_mov_eliminar_MDL();
        }
        public void EliminarMovimiento(EntityConnectionStringBuilder connection, String folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_mov_cab_rep_MDL(folio_sam);
        }
    }
}
