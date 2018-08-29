using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_HOJA_RUTA_PP
    {
        private static ACC_HOJA_RUTA_PP instance = null;
        private static readonly object padlock = new object();

        public static ACC_HOJA_RUTA_PP ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_HOJA_RUTA_PP();
                }
                return instance;
            }
        }
        public void InsertarHojasRuta(EntityConnectionStringBuilder connection, Hoja_RutaPP hoja)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.Insert_HojaRuta_MDL(hoja.MATNR,
                                            hoja.PLNNR,
                                            hoja.PLNAL,
                                            hoja.ZKRIZ,
                                            hoja.ZAEHL,
                                            hoja.PLNTY,
                                            hoja.PLNKN,
                                            hoja.PLNFL,
                                            hoja.ZAEHL1,
                                            hoja.ZAEHL2,
                                            hoja.OBJTY,
                                            hoja.OBJID,
                                            hoja.LOEKZ,
                                            hoja.WERKS,
                                            hoja.STEUS,
                                            hoja.ARBID,
                                            hoja.BMSCH,
                                            hoja.DAUNO,
                                            hoja.DAUNE,
                                            hoja.ARBEI,
                                            hoja.ARBEH,
                                            hoja.STATU,
                                            hoja.KTEXT,
                                            hoja.STLNR,
                                            hoja.STLAL,
                                            hoja.VORNR,
                                            hoja.WERKSI,
                                            hoja.LTXA1,
                                            hoja.LTXA2,
                                            hoja.TXTSP,
                                            hoja.MEINH,
                                            hoja.UMREN,
                                            hoja.UMREZ,
                                            hoja.ZMERH,
                                            hoja.ZEIER,
                                            hoja.LAR01,
                                            hoja.VGE01,
                                            hoja.VGW01,
                                            hoja.LAR02,
                                            hoja.VGE02,
                                            hoja.VGW02,
                                            hoja.LAR03,
                                            hoja.VGE03,
                                            hoja.VGW03,
                                            hoja.LAR04,
                                            hoja.VGE04,
                                            hoja.VGW04,
                                            hoja.LAR05,
                                            hoja.VGE05,
                                            hoja.VGW05,
                                            hoja.LAR06,
                                            hoja.VGE06,
                                            hoja.VGW06,
                                            hoja.LIFNR,
                                            hoja.PEINH,
                                            hoja.SAKTO,
                                            hoja.WAERS,
                                            hoja.INFNR,
                                            hoja.ESOKZ,
                                            hoja.EKORG,
                                            hoja.EKGRP,
                                            hoja.MATKL,
                                            hoja.ARBPL,
                                            hoja.ANZZL,
                                            hoja.ANDAT,
                                            hoja.AEDAT,
                                            hoja.SRVPOS,
                                            hoja.KTEXT1);
            }
            catch (Exception) { }
        }
        public void EliminarHojasRuta(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_Hoja_Ruta_MDL(centro);
        }
    }
}
