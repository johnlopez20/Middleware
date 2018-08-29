using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;


namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Pedidos
    {
        #region Instancia
        private static DALC_Pedidos instance = null;
        private static readonly object padlock = new object();

        public static DALC_Pedidos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Pedidos();
                }
                return instance;
            }
        }

        #endregion

        public void IngresaPedido_I(EntityConnectionStringBuilder connection, Pedidos_I pd)
        {
            if (pd.BEWTP == "E")
            {
                pd.BEWTP = "WE";
            }
            else if (pd.BEWTP == "D")
            {
                pd.BEWTP = "Lerf";
            }
            else if (pd.BEWTP != "D" || pd.BEWTP != "E")
            {
                pd.BEWTP = pd.BEWTP;
            }
            var context = new samEntities(connection.ToString());
            context.InsertPedidosHistorial_MDL(pd.EBELN,
                                               pd.EBELP,
                                               pd.MATNR,
                                               pd.TXZ01,
                                               pd.MENGE,
                                               pd.MEINS,
                                               pd.BEWTP,
                                               pd.BWART,
                                               pd.MBLNR,
                                               pd.BUZEI,
                                               pd.CANTIADE,
                                               pd.UM_PED,
                                               pd.EINDT,
                                               pd.BUDAT,
                                               pd.FOLIO_SAM,
                                               pd.LVORM);
        }

        public void BorrarPedido_I(EntityConnectionStringBuilder connection, Pedidos_I pedd)
        {
            var context = new samEntities(connection.ToString());
            context.DeletePedidosHistorial_MDL(pedd.EBELN);
        }
        public void IngresaPedido_II(EntityConnectionStringBuilder connection, Pedidos_II pd)
        {
            var context = new samEntities(connection.ToString());
            context.InsertPedidosDetalle_MDL(pd.BSTYP,
                                             pd.EBELN,
                                             pd.LIFNR,
                                             pd.NAME1,
                                             pd.BEDAT,
                                             pd.G_NAME,
                                             pd.EKORG,
                                             pd.EKOTX,
                                             pd.EKGRP,
                                             pd.EKNAM,
                                             pd.BUKRS,
                                             pd.BUTXT,
                                             pd.EBELP,
                                             pd.KNTTP,
                                             pd.PSTYP,
                                             pd.MATNR,
                                             pd.TXZ01,
                                             pd.MENGE,
                                             pd.MEINS,
                                             pd.EINDT,
                                             pd.WERKS,
                                             pd.LGORT,
                                             pd.CHARG,
                                             pd.AFNAM,
                                             pd.INFNR,
                                             pd.BANFN,
                                             pd.BNFPO,
                                             pd.KONNR,
                                             pd.MATKL,
                                             pd.IDNLF,
                                             pd.EAN11,
                                             pd.LICHA,
                                             pd.BSART,
                                             pd.BATXT,
                                             pd.G_POS,
                                             pd.PACKNO,
                                             pd.SUB_PACKNO,
                                             pd.BWART,
                                             pd.CANTIDADE,
                                             pd.KOSTL,
                                             pd.AUFNR,
                                             pd.SAKTO,
                                             pd.FRGGR,
                                             pd.FRGSX,
                                             pd.FRGKE,
                                             pd.FRGZU,
                                             
                                             "0", 
                                             "", 
                                             "0.000",
                                             pd.BEDNR,
                                             pd.FRGC1,
                                             pd.FRGCT,
                                             pd.LVORM);
        }
        public void BorrarPedido_II(EntityConnectionStringBuilder connection, Pedidos_II ped2)
        {
            var context = new samEntities(connection.ToString());
            context.DeletePedidosDetalle_MDL(ped2.EBELN,
                                             ped2.WERKS);
        }
        public void IngresaServicio(EntityConnectionStringBuilder connection, Servicio pd)
        {
            var context = new samEntities(connection.ToString());
            context.InsertPedidoServicios_MDL(pd.EBELN,
                                              pd.EBELP,
                                              pd.EXTROW,
                                              pd.SRVPOS,
                                              pd.MENGE,
                                              pd.MEINS,
                                              pd.KTEXT1,
                                              pd.TBTWR,
                                              pd.WAERS,
                                              pd.NETWR,
                                              pd.ACT_MENGE,
                                              pd.AUFNR,
                                              pd.WERKS,
                                              pd.DATUM);
        }
        public void BorrarServicio(EntityConnectionStringBuilder connection, Servicio ped3)
        {
            var context = new samEntities(connection.ToString());
            context.DeletePedidoServicios_MDL(ped3.EBELN,
                                              ped3.WERKS);
        }
    }
}
