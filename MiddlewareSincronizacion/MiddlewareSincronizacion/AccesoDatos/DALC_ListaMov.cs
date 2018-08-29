using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ListaMov
    {
        #region instancia
        private static DALC_ListaMov instance = null;
        private static readonly object padlock = new object();

        public static DALC_ListaMov ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ListaMov();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTSAM_MOV_CABR(EntityConnectionStringBuilder connection, TMIGO_CAB tc)
        {
            var context = new samEntities(connection.ToString());
            context.cabecera_doc_materiales_delete_MDL(tc.MBLNR);
        }
        public void IngresaTMIGO_CAB(EntityConnectionStringBuilder connection, TMIGO_CAB tc)
        {
            var context = new samEntities(connection.ToString());
            context.cabecera_doc_materiales_MDL(tc.MBLNR,
                                                tc.MJAHR,
                                                tc.BLDAT,
                                                tc.BUDAT,
                                                tc.LFSNR,
                                                tc.FRBNR,
                                                tc.BKTXT,
                                                tc.LIFNR,
                                                tc.NAME1,
                                                tc.FOLIO_SAM,
                                                tc.UMLGO,
                                                tc.LVORM);
        }
        public void VaciarTSAM_MOVR(EntityConnectionStringBuilder connection, TMIGO_POS tp)
        {
            var context = new samEntities(connection.ToString());
            context.detalles_doc_materiales_delete_MDL(tp.MBLNR,
                                                       tp.WERKS);
        }
        public void IngresaTMIGO_POS(EntityConnectionStringBuilder connection, TMIGO_POS um)
        {
            var context = new samEntities(connection.ToString());
            context.detalles_doc_materiales_MDL(um.MBLNR,
                                                um.ZEILE,
                                                um.LIFNR,
                                                um.CHARG,
                                                um.BWART,
                                                um.EBELN,
                                                um.EBELP,
                                                um.ABLAD,
                                                um.WEMPF,
                                                um.MATNR,
                                                um.MAKTX_ES,
                                                um.MAKTX_EN,
                                                um.ERFMG,
                                                um.ERFME,
                                                um.KOSTL,
                                                um.AUFNR,
                                                um.BUKRS,
                                                um.SAKTO,
                                                um.IDNLF,
                                                um.SOBKZ,
                                                um.WERKS,
                                                um.NAME2,
                                                um.LGORT,
                                                um.LGOBE,
                                                um.EMLIF,
                                                um.NAME3,
                                                um.SHKZG,
                                                um.LPROVEDOR,
                                                um.MATKL,
                                                um.FOLIO_SAM,
                                                "",
                                                um.UMLGO,
                                                um.LICHA,
                                                "");
        }
    }
}
