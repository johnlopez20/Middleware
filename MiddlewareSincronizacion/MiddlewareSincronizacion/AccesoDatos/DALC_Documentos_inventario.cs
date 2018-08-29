using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Documentos_inventario
    {
        #region instancia
        private static DALC_Documentos_inventario instance = null;
        private static readonly object padlock = new object();

        public static DALC_Documentos_inventario obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Documentos_inventario();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarDocumentosInventario(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_documentos_inventario_MDL(centro);
        }
        public void InsertarCabeceraDocumentos(EntityConnectionStringBuilder connection, Cabecera_documentos_inventario cab)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_cabecera_documentos_inventario_MDL(cab.IBLNR,
                                                              cab.FOLIO_SAM,
                                                              cab.GJAHR,
                                                              cab.VGART,
                                                              cab.WERKS,
                                                              cab.LGORT,
                                                              cab.SOBKZ,
                                                              cab.BLDAT,
                                                              cab.GIDAT,
                                                              cab.ZLDAT,
                                                              cab.BUDAT,
                                                              cab.MONAT,
                                                              cab.USNAM,
                                                              "");
        }
        public void InsertarPosicionesDocumentosInventario(EntityConnectionStringBuilder connection, Posiciones_documento_inventario pos)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_posiciones_documentos_inventario_MDL(pos.IBLNR,
                                                                pos.FOLIO_SAM,
                                                                pos.ZEILI,
                                                                pos.MATNR,
                                                                pos.WERKS,
                                                                pos.LGORT,
                                                                pos.CHARG,
                                                                pos.SOBKZ,
                                                                pos.BSTAR,
                                                                pos.KDAUF,
                                                                pos.KDPOS,
                                                                pos.KDEIN,
                                                                pos.LIFNR,
                                                                pos.KUNNR,
                                                                pos.MEINS);
        }
    }
}
