using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Vendedores
    {
        #region Instancia
        private static DALC_Vendedores instance = null;
        private static readonly object padlock = new object();

        public static DALC_Vendedores ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Vendedores();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTablaAvisosVendedor(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_avisos_vendedor_vis_MDL();
        }
        public void InsertarAvisosVendedor(EntityConnectionStringBuilder connection, Avisos_vendedor av)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_avisos_vendedor_vis_MDL(av.VKGRP,
                                                   av.BEZEI,
                                                   av.TEXTO1,
                                                   av.TEXTO2,
                                                   av.TEXTO3,
                                                   av.FCREACION,
                                                   av.HCREACION);
        }
    }
}
