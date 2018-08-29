using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;


namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Relacion
    {
        #region Instancia
        private static DALC_Relacion instance = null;
        private static readonly object padlock = new object();

        public static DALC_Relacion obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Relacion();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarRelacion(EntityConnectionStringBuilder connection, Relacion rl)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_relacion_MDL(rl.EQUNR);
        }
        public void IngresaRelacion(EntityConnectionStringBuilder connection, Relacion rl)
        {
            var context = new samEntities(connection.ToString());
            context.relacion_MDL(rl.JERARQUIA,
                                 rl.NIVEL,
                                 rl.TPLNR,
                                 rl.EQUNR,
                                 rl.EQUNR.Substring(4, 3),
                                 rl.EQUNR01,
                                 rl.EQUNR02,
                                 rl.EQUNR03,
                                 rl.EQUNR04,
                                 rl.EQUNR05,
                                 rl.EQUNR06,
                                 rl.EQUNR07,
                                 rl.EQUNR08,
                                 rl.EQUNR09,
                                 rl.EQUNR10,
                                 rl.EQUNR11,
                                 rl.EQUNR12,
                                 rl.EQUNR13,
                                 rl.EQUNR14,
                                 rl.EQUNR15,
                                 rl.EQUNR16,
                                 rl.EQUNR17,
                                 rl.EQUNR18,
                                 rl.EQUNR19,
                                 rl.EQUNR20,
                                 rl.MATNR,
                                 rl.CHARG,
                                 rl.WERKS,
                                 rl.LGORT,
                                 rl.SERNR,
                                 rl.POINT,
                                 rl.MDOCM,
                                 rl.RECDV,
                                 rl.RECDU,
                                 rl.EQKTX,
                                 rl.ICONO,
                                 rl.RENGLON,
                                 rl.NRECDV,
                                 rl.READG,
                                 rl.NREADG,
                                 rl.LVORM);
        }
        public void VaciarRelacion2(EntityConnectionStringBuilder connection, Relacion2 rl)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_relacion2_MDL(rl.EQUNR,
                                         rl.IWERK);
        }
        public void IngresaRelacion2(EntityConnectionStringBuilder connection, Relacion2 rl)
        {
            var context = new samEntities(connection.ToString());
            context.relacion2_MDL(rl.JERARQUIA,
                                  rl.NIVEL,
                                  rl.WAPOS,
                                  rl.ABNUM,
                                  rl.AUFNR,
                                  rl.ADDAT,
                                  rl.WARPL,
                                  rl.WSTRA,
                                  rl.WPPOS,
                                  rl.PSTXT,
                                  rl.EQUNR,
                                  rl.OBKNR,
                                  rl.ERKNZ,
                                  rl.AEKNZ,
                                  rl.ERNAM,
                                  rl.ERSDT,
                                  rl.AEDAT,
                                  rl.AENAM,
                                  rl.PLNTY,
                                  rl.LAUFN,
                                  rl.PLNNR,
                                  rl.PLNAL,
                                  rl.STATUS,
                                  rl.LTKNZ,
                                  rl.WPGRP,
                                  rl.OBJTY,
                                  rl.GEWRK,
                                  rl.IWERK,
                                  rl.LANGU,
                                  rl.ILOAN,
                                  rl.ILOAI,
                                  rl.SERIALNR,
                                  rl.SERMAT,
                                  rl.MATNR,
                                  rl.CHARG,
                                  rl.WERKS,
                                  rl.LGORT,
                                  rl.SERNR,
                                  rl.POINT,
                                  rl.MDOCM,
                                  rl.RECDV,
                                  rl.RECDU,
                                  rl.EQKTX,
                                  rl.ICONO,
                                  rl.RENGLON,
                                  rl.L_LINE,
                                  rl.GSTRP);
        }
    }
}
