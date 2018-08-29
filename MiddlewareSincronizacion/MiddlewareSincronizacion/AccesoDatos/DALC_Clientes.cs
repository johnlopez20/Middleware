using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Clientes
    {
        #region Instancia
        private static DALC_Clientes instance = null;
        private static readonly object padlock = new object();

        public static DALC_Clientes obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Clientes();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarCliente(EntityConnectionStringBuilder connection, Clientes cl)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.clientes_delete_MDL(cl.KUNNR,
                                            cl.BUKRS);
            }
            catch (Exception) { }
        }
        public void VaciarRelacionInterlocutores(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_RelacionInterlocutores_MDL();
        }
        public void IngresaCliente(EntityConnectionStringBuilder connection, Clientes cl)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.clientes_MDL(cl.KUNNR,
                                     cl.BUKRS,
                                     cl.VKORG,
                                     cl.VTWEG,
                                     cl.SPART,
                                     cl.ADRC,
                                     cl.DATE_FROM,
                                     cl.NATION,
                                     cl.DATE_TO,
                                     cl.TITLE,
                                     cl.NAME1,
                                     cl.NAME2,
                                     cl.NAME3,
                                     cl.NAME4,
                                     cl.CITY1,
                                     cl.CITY2,
                                     cl.CITY_CODE,
                                     cl.CITYP_CODE,
                                     cl.HOME_CITY,
                                     cl.STREET,
                                     cl.HOUSE_NUM1,
                                     cl.KTOKK,
                                     cl.STCD1,
                                     cl.ZTERM,
                                     cl.AKONT,
                                     cl.WAERS,
                                     cl.INCO1,
                                     cl.INCO2,
                                     cl.VKGRP,
                                     cl.LFABC,
                                     cl.LOEVMB,
                                     cl.SPERR,
                                     cl.LOEVM,
                                     cl.LVORM);
            }
            catch (Exception) { }
        }
        //public void VaciarClienteSD(EntityConnectionStringBuilder connection, Clientes cl)
        //{
        //    var context = new samEntities(connection.ToString());
        //    context.clientes_delete_MDL(cl.KUNNR, "1000");
        //}
        public void InsertarClientes(EntityConnectionStringBuilder connection, Clientes cl)
        {
            var context = new samEntities(connection.ToString());
            context.clientes_MDL(cl.KUNNR,
                                 cl.BUKRS,
                                 cl.VKORG,
                                 cl.VTWEG,
                                 cl.SPART,
                                 cl.ADRC,
                                 cl.DATE_FROM,
                                 cl.NATION,
                                 cl.DATE_TO,
                                 cl.TITLE,
                                 cl.NAME1,
                                 cl.NAME2,
                                 cl.NAME3,
                                 cl.NAME4,
                                 cl.CITY1,
                                 cl.CITY2,
                                 cl.CITY_CODE,
                                 cl.CITYP_CODE,
                                 cl.HOME_CITY,
                                 cl.STREET,
                                 cl.HOUSE_NUM1,
                                 cl.KTOKK,
                                 cl.STCD1,
                                 cl.ZTERM,
                                 cl.AKONT,
                                 cl.WAERS,
                                 cl.INCO1,
                                 cl.INCO2,
                                 cl.VKGRP,
                                 cl.LFABC,
                                 cl.LOEVMB,
                                 cl.SPERR,
                                 cl.LOEVM, "");
        }
        public void InsertarRelacion(EntityConnectionStringBuilder connection, RelacionInterlocutores re)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_relacion_interlocutores_MDL(re.KUNNR,
                                                       re.VKORG,
                                                       re.VTWEG,
                                                       re.SPART,
                                                       re.PARVW,
                                                       re.KUNN2);
        }
        public void VaciarValidacion(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_cliente_vendedor_material_MDL();
        }
        public void InsertarValidacion(EntityConnectionStringBuilder connection, Validacion v)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_cliente_vendedor_material_MDL(v.KSCHL,
                                                         v.VKORG,
                                                         v.VTWEG,
                                                         v.VKGRP,
                                                         v.PLTYP,
                                                         v.KUNNR,
                                                         v.MATNR,
                                                         v.KFRST,
                                                         v.DATBI,
                                                         v.DATAB,
                                                         v.KBSTAT,
                                                         v.KNUMH);
        }
    }
}
