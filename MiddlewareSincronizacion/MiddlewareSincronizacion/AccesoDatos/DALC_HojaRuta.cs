using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_HojaRuta
    {
        #region instancia
        private static DALC_HojaRuta instance = null;
        private static readonly object padlock = new object();

        public static DALC_HojaRuta obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_HojaRuta();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarHojasRuta(EntityConnectionStringBuilder connection, HojasRuta hj)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_hojas_de_ruta_MDL(hj.PLNNR,
                                             hj.WERKS);
        }
        public void IngresaHojasRuta(EntityConnectionStringBuilder connection, HojasRuta hj)
        {
            var context = new samEntities(connection.ToString());
            context.hojas_de_ruta_MDL(hj.EQUNR,
                                      hj.PLNNR,
                                      hj.PLNAL,
                                      hj.ZKRIZ,
                                      hj.ZAEHL,
                                      hj.PLNTY,
                                      hj.PLNKN,
                                      hj.PLNFL,
                                      hj.ZAEHL1,
                                      hj.ZAEHL2,
                                      hj.OBJTY,
                                      hj.OBJID,
                                      hj.LOEKZ,
                                      hj.WERKS,
                                      hj.STEUS,
                                      hj.ARBID,
                                      hj.BMSCH,
                                      hj.DAUNO,
                                      hj.DAUNE,
                                      hj.ARBEI,
                                      hj.ARBEH,
                                      hj.STATU,
                                      hj.KTEXT,
                                      hj.STLNR,
                                      hj.STLAL,
                                      hj.VORNR,
                                      hj.WERKSI,
                                      hj.LTXA1,
                                      hj.LTXA2,
                                      hj.TXTSP,
                                      hj.MEINH,
                                      hj.UMREN,
                                      hj.UMREZ,
                                      hj.ZMERH,
                                      hj.ZEIER,
                                      hj.LAR01,
                                      hj.VGE01,
                                      hj.VGW01,
                                      hj.LAR02,
                                      hj.VGE02,
                                      hj.VGW02,
                                      hj.LAR03,
                                      hj.VGE03,
                                      hj.VGW03,
                                      hj.LAR04,
                                      hj.VGE04,
                                      hj.VGW04,
                                      hj.LAR05,
                                      hj.VGE05,
                                      hj.VGW05,
                                      hj.LAR06,
                                      hj.VGE06,
                                      hj.VGW06,
                                      hj.LIFNR,
                                      hj.PEINH,
                                      hj.SAKTO,
                                      hj.WAERS,
                                      hj.INFNR,
                                      hj.ESOKZ,
                                      hj.EKORG,
                                      hj.EKGRP,
                                      hj.MATKL,
                                      hj.ARBPL,
                                      hj.ANZZL,
                                      hj.SRVPOS,
                                      hj.KTEXT1);
        }
    }
}
