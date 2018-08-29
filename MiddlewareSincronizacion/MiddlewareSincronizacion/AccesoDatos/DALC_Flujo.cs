using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Flujo
    {
        #region Instancia
        private static DALC_Flujo instance = null;
        private static readonly object padlock = new object();

        public static DALC_Flujo ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Flujo();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarFlujo(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_flujo_documentos_MDL();
        }
        public void InsertarFlujo(EntityConnectionStringBuilder connection, FlujoSD fu)
        {
            string fecha = "", ano = "", mes = "", dia = "";
            if(!fu.MATWA.Equals("00000000"))
            {
                ano = fu.MATWA.Substring(0, 4);
                mes = fu.MATWA.Substring(4, 2);
                dia = fu.MATWA.Substring(6, 2);
                fecha = ano + "-" + mes + "-" + dia;
            }
            else
            {
                fecha = fecha;
            }
            var context = new samEntities(connection.ToString());
            context.INSERT_flujo_documentos_v2_MDL(fu.VBELN,
                                                fu.POSNR,
                                                fu.ERDAT,
                                                fu.WAERK,
                                                fu.VKORG,
                                                fu.VTWEG,
                                                fu.SPART,
                                                fu.VKGRP,
                                                fu.VKBUR,
                                                fu.AUART,
                                                fu.KUNNR,
                                                fu.KOSTL,
                                                fu.VGTYP,
                                                fu.XBLNR,
                                                fu.ZUONR,
                                                fu.MATNR,
                                                fecha,
                                                //fu.MATWA,
                                                fu.PMATN,
                                                fu.CHARG,
                                                fu.MATKL,
                                                fu.ARKTX,
                                                fu.PRODH,
                                                fu.ZMENG,
                                                fu.ZIEME,
                                                fu.NETWR,
                                                fu.WERKS,
                                                fu.LGORT,
                                                fu.ROUTE,
                                                fu.AUFNR,
                                                fu.VBELV_LI,
                                                fu.POSNR_LI,
                                                fu.RFMNG,
                                                fu.MEINS,
                                                fu.VBELV_8,
                                                fu.POSNR_8,
                                                fu.RFMNG_8,
                                                fu.MEINS_8,
                                                fu.VBELV_R,
                                                fu.POSNR_R,
                                                fu.RFMNG_R,
                                                fu.MEINS_R,
                                                fu.VBELV_M,
                                                fu.POSNR_M,
                                                fu.RFMNG_M,
                                                fu.MEINS_M,
                                                fu.KUWEV_KUNNR,
                                                fu.LIKP_BTGEW,
                                                fu.LIKP_LDDAT,
                                                fu.LIKP_LIFEX,
                                                fu.LIKP_NTGEW,
                                                fu.LIKP_TDDAT,
                                                fu.LIKP_VSTEL,
                                                fu.LIPS_CHARG,
                                                fu.LIPS_GEWEI,
                                                fu.LIPSD_G_LFIMG,
                                                fu.TVROT_BEZEI,
                                                fu.VBAK_ERNAM,
                                                fu.VBKD_BSTKD,
                                                fu.VFKK_FKART,
                                                fu.VFKK_FKNUM,
                                                fu.VFKK_STABR,
                                                fu.VFKP_NETWR,
                                                fu.VFKP_EBELN,
                                                fu.VTTK_ADD01,
                                                fu.VTTK_ADD02,
                                                fu.VTTK_ADD03,
                                                fu.VTTK_ADD04,
                                                fu.VTTK_BFART,
                                                fu.VTTK_EXTI1,
                                                fu.VTTK_EXTI2,
                                                fu.VTTK_ROUTE,
                                                fu.VTTK_SHTYP,
                                                fu.VTTK_SIGNI,
                                                fu.VTTK_STTRG,
                                                fu.VTTK_TDLNR,
                                                fu.VTTK_TPBEZ,
                                                fu.VTTK_VSART,
                                                fu.ACTUAL_START_DATE,
                                                fu.ACTUAL_FINISH_DATE,
                                                fu.SYSTEM_STATUS,
                                                fu.KALAB,
                                                fu.FLAG,
                                                fu.NAME1,
                                                fu.AUDAT);
        }
    }
}
