using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_Materiales
    {
        #region Instancia
        private static ACC_Materiales instance = null;
        private static readonly object padlock = new object();

        public static ACC_Materiales ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_Materiales();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<VALIDA_MATERIAL_MDL_Result> ValidarMaterial(EntityConnectionStringBuilder connection, Materiales m)
        {
            var context = new samEntities(connection.ToString());
            return context.VALIDA_MATERIAL_MDL(m.WERKS,
                                               m.MATNR);
        }
        public void insertMat(EntityConnectionStringBuilder connection, Materiales ms)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.Materiales_mdl(ms.MATNR,
                                        ms.WERKS,
                                        ms.MEINS,
                                        "",
                                        ms.MATKL,
                                        ms.MTART,
                                        "",
                                        "",
                                        ms.XCHPF,
                                        ms.MAKTX_ES,
                                        ms.MAKTX_EN,
                                        "",
                                        "",
                                        "",
                                        "",
                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                                        "", "", "", "", "", "", "", "",
                                        "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            catch (Exception) { }
        }
        public void ActualizaMaterial(EntityConnectionStringBuilder connection, Materiales m)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_MATERIAL_MDL(m.MATNR,
                                        m.WERKS,
                                        m.MEINS,
                                        m.BISMT,
                                        m.MATKL,
                                        m.MTART,
                                        "",
                                        m.EKGRP,
                                        m.XCHPF,
                                        m.MAKTX_ES,
                                        m.MAKTX_EN);
        }
    }
}
