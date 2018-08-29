using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Sol_Ped
    {
        #region Instancia
        private static DALC_Sol_Ped instance = null;
        private static readonly object padlock = new object();

        public static DALC_Sol_Ped ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Sol_Ped();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_solped_id_MDL_Result> ObtenerDatosIdSolped(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_solped_id_MDL(id);
        }
        public IEnumerable<SELECT_solped_valida_hora_MDL_Result> ObtenerValidacionHoraSolped(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_solped_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELEC_fol_solped_menos_MDL_Result> ObtenerFolioMenosSolped(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_solped_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_solped_MDL_Result> ObtenerTodoFolioSolped(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_solped_MDL(folio_sam);
        }
        public void IngresaCabeceraSolPed(EntityConnectionStringBuilder connection, SolPed_Vis_Cab cab)
        {
            var context = new samEntities(connection.ToString());
            context.InsertSolpedCabeceraVis_MDL(cab.FOLIO_SAM,
                                                cab.FOLIO_SAP,
                                                cab.BBSRT,
                                                cab.TEXTO_CAB,
                                                cab.AFNAM,
                                                cab.EWERK,
                                                cab.BADAT,
                                                cab.LFDAT,
                                                cab.FRGDT,
                                                cab.BSART,
                                                cab.WERKS);
        }
        public void BorrarCabeceraSolPed(EntityConnectionStringBuilder connection, SolPed_Vis_Cab cab)
        {
            var context = new samEntities(connection.ToString());
            context.DeleteSolpedCabeceraVis_MDL(cab.FOLIO_SAP,
                                                cab.EWERK);
        }
        public void IngresaPosicionesSolPed(EntityConnectionStringBuilder connection, SolPed_Pos pos)
        {
            var context = new samEntities(connection.ToString());
            context.InsertSolpedPosicionesVis_MDL(pos.FOLIO_SAM,
                                                  pos.PREQ_ITEM,
                                                  pos.ITEM_CAT,
                                                  pos.MATERIAL,
                                                  pos.DATUM,
                                                  pos.UZEIT,
                                                  pos.FOLIO_SAP,
                                                  pos.DOC_TYPE,
                                                  pos.ACCTASSCAT,
                                                  pos.SHORT_TEXT,
                                                  pos.QUANTITY,
                                                  pos.UNIT,
                                                  pos.DEL_DATCAT,
                                                  pos.DELIV_DATE,
                                                  pos.MAT_GRP,
                                                  pos.PLANT,
                                                  pos.STORE_LOC,
                                                  pos.PUR_GROUP,
                                                  pos.PREQ_NAME,
                                                  pos.PREQ_DATE,
                                                  pos.DES_VENDOR,
                                                  pos.LIFNR,
                                                  pos.TABIX,
                                                  pos.EPSTP,
                                                  pos.INFNR,
                                                  pos.EKORG,
                                                  pos.G_L_ACCT,
                                                  pos.COST_CTR,
                                                  pos.KONNR,
                                                  pos.KTPNR,
                                                  pos.VRTKZ,
                                                  pos.SAKTO,
                                                  pos.AUFNR,
                                                  pos.KOKRS,
                                                  pos.KOSTL,
                                                  pos.TEXTO_CAB,
                                                  pos.TEXTO_POS,
                                                  pos.RECIBIDO,
                                                  pos.PROCESADO,
                                                  pos.ERROR,
                                                  pos.PREIS,
                                                  pos.WAERS,
                                                  pos.FRGGR,
                                                  pos.FRGRL,
                                                  pos.FRGKZ,
                                                  pos.FRGZU,
                                                  pos.FRGST,
                                                  pos.FRGC1,
                                                  pos.FRGCT,
                                                  pos.LOEKZ,
                                                  pos.PEDIDO);
        }
        public void BorrarPosicionesSolPed(EntityConnectionStringBuilder connection, SolPed_Pos pos)
        {
            var context = new samEntities(connection.ToString());
            context.DeleteSolpedPosicionesVis_MDL(pos.FOLIO_SAP,
                                                  pos.PLANT);

        }
        public void IngresaServiciosSolPed(EntityConnectionStringBuilder connection, SolPed_Ser_Vis ser)
        {
            var context = new samEntities(connection.ToString());
            context.InsertSolpedServiciosVis_MDL(ser.FOLIO_SAM,
                                                 ser.BANFN,
                                                 ser.BNFPO,
                                                 ser.EXTROW,
                                                 ser.SRVPOS,
                                                 ser.MENGE,
                                                 ser.MEINS,
                                                 ser.KTEXT1,
                                                 ser.TBTWR,
                                                 ser.WAERS,
                                                 ser.KOSTL,
                                                 ser.NETWR);
        }
        public void BorrarServiciosSolPed(EntityConnectionStringBuilder connection, SolPed_Ser_Vis ser)
        {
            var context = new samEntities(connection.ToString());
            context.DeleteSolpedServiciosVis_MDL(ser.BANFN);
        }
        public void VaciarTextoCSV(EntityConnectionStringBuilder connection, TextoCabeceraSolped txc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_texto_cabecera_solped_vis_MDL(txc.FOLIO_SAP);
        }
        public void IngresarTextoCSV(EntityConnectionStringBuilder connection, TextoCabeceraSolped txc)
        {
            var context = new samEntities(connection.ToString());
            context.texto_cabecera_solped_vis_MDL(txc.FOLIO_SAM,
                                                  txc.FOLIO_SAP,
                                                  txc.INDICE,
                                                  txc.TDFORMAT,
                                                  txc.TDLINE);
        }
        public void VaciarTextoPSV(EntityConnectionStringBuilder connection, TextoPosicionesSolped txp)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_textos_posiciones_solped_vis_MDL(txp.FOLIO_SAP);
        }
        public void IngresarTextoPSV(EntityConnectionStringBuilder connection, TextoPosicionesSolped txp)
        {
            var context = new samEntities(connection.ToString());
            context.textos_posiciones_solped_vis_MDL(txp.FOLIO_SAM,
                                                     txp.FOLIO_SAP,
                                                     txp.PREQ_ITEM,
                                                     txp.INDICE,
                                                     txp.TDFORMAT,
                                                     txp.TDLINE);
        }
        public void VaciarSolpedRep(EntityConnectionStringBuilder connection, SolPed_Rep sol)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reportes_solped_MDL(sol.FOLIO_SAM,
                                               sol.PLANT);
        }
        public void IngresaRepSolPed(EntityConnectionStringBuilder connection, SolPed_Rep s)
        {
            var context = new samEntities(connection.ToString());
            context.reportes_solped_MDL(s.FOLIO_SAM,
                                        s.FOLIO_SAP,
                                        s.MATERIAL,
                                        s.DATUM,
                                        s.UZEIT,
                                        s.DOC_TYPE,
                                        s.TEXTO_CAB,
                                        s.SHORT_TEXT,
                                        s.QUANTITY,
                                        s.PLANT,
                                        s.PREQ_DATE,
                                        s.DELIV_DATE,
                                        s.PREIS,
                                        s.WAERS,
                                        s.RECIBIDO,
                                        s.PROCESADO,
                                        s.ERROR);
        }
        public void ActualizaRepSolPed(EntityConnectionStringBuilder connection, SolPed_Rep sol)
        {
            var context = new samEntities(connection.ToString());
            if (sol.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reportes_solped_reportes_MDL(sol.FOLIO_SAM,
                                                            sol.PROCESADO,
                                                            sol.ERROR);
            }
            else
            {
                context.DELETE_reportes_solped_reporte_MDL(sol.FOLIO_SAM);
            }
        }
        public IEnumerable<SELECT_solped_crea_MDL_Result> ObtenerSolicitudes(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_solped_crea_MDL();
        }
        public IEnumerable<SELECT_solped_crea_Fol_MDL_Result> ObtenerCabecera(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_solped_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_solped_servicios_crea_MDL_Result> ObtenerSolicitudS(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_solped_servicios_crea_MDL();
        }
        public IEnumerable<SELECT_solped_servicios_crea_Fol_MDL_Result> ObtenerServicios(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_solped_servicios_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_cabecera_solped_MDL_Result> ObtenerTextCab(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_cabecera_solped_MDL();
        }
        public IEnumerable<SELECT_textos_cabecera_solped_Fol_MDL_Result> ObtenerTextoCabecera(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_cabecera_solped_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_posiciones_solped_MDL_Result> ObtenerTextPos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_posiciones_solped_MDL();
        }
        public IEnumerable<SELECT_textos_posiciones_solped_Fol_MDL_Result> ObtenerTextoPosicion(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_posiciones_solped_Fol_MDL(folio_sam);
        }
        public void ActualizaSolpedCrea(EntityConnectionStringBuilder connection, SolPed_Crea s)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_SolpedCrea_Error_MDL(s.FOLIO_SAM,
                                                s.PREQ_ITEM,
                                                s.RECIBIDO,
                                                s.PROCESADO,
                                                s.ERROR);
        }
        public void ActualizaSolpedServ(EntityConnectionStringBuilder connection, SolPed_Serv sps)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_solped_servicios_crea_MDL(sps.FOLIO_SAM,
                                                     sps.PREQ_ITEM,
                                                     sps.RECIBIDO);
        }
        public IEnumerable<SELECT_cancela_solped_MDL_Result> ObtenerCancelacionesSolPed(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cancela_solped_MDL();
        }
        public void ActulizaCancelacion(EntityConnectionStringBuilder connection, SolPedCancela spc)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cancela_solped_MDL(spc.FOLIO_SAP,
                                              spc.PREQ_ITEM,
                                              spc.RECIBIDO);
        }
        //public IEnumerable<SELECT_solped_crea_fecha_MDL_Result> ObtenerHora(EntityConnectionStringBuilder connection)
        //{
        //    var context = new samEntities(connection.ToString());
        //    return context.SELECT_solped_crea_fecha_MDL();
        //}
        public IEnumerable<SELECT_solped_crea_li_MDL_Result> ObtenerListaFolios(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_solped_crea_li_MDL(fecha,
                                                     hora);
        }
        public void BorrarSolPed(EntityConnectionStringBuilder connection, string folio_sap)
        {
            var context = new samEntities(connection.ToString());
            context.Delete_Solped_Vis_MDL(folio_sap);
        }
        public void EliminarRegistroCrea(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_SOLPED_FOL_MDL(folio_sam);
        }
    }
}
