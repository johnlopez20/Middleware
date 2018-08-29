using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_BOOM_MATE_PP
    {
        #region Instancia
        private static ACC_BOOM_MATE_PP instance = null;
        private static readonly object padlock = new object();

        public static ACC_BOOM_MATE_PP ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_BOOM_MATE_PP();
                }
                return instance;
            }
        }
        #endregion
        public void InsertarBoomMate(EntityConnectionStringBuilder connection, Boom_MatePP bm)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.Insert_BoomMate_MDL(bm.WERKS,
                                            bm.STLTY,
                                            bm.STLNR,
                                            bm.STLAL,
                                            bm.STKOZ,
                                            bm.STLKN,
                                            bm.STPOZ,
                                            bm.STASZ,
                                            bm.MATNR,
                                            bm.BMENG,
                                            bm.DATUV,
                                            bm.LKENZ,
                                            bm.LOEKZ,
                                            bm.ANDAT,
                                            bm.ANNAM,
                                            bm.IDNRK,
                                            bm.PSWRK,
                                            bm.POSTP,
                                            bm.SPOSN,
                                            bm.SORTP,
                                            bm.KMPME,
                                            bm.KMPMG,
                                            bm.FMENG);
            }
            catch (Exception) { }
        }
        public void EliminarBoomMaterialesPP(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_Boom_Mate_MDL(centro);
        }
    }
}
