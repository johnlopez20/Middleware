using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
using MiddlewareSincronizacion.Entidades;
using MiddlewareSincronizacion.AccesoDatos;
using System.Data.Entity.Core.EntityClient;
using System.Threading;
using System.Data.Entity.Core.Objects;
using System.Drawing.Printing;

namespace MiddlewareSincronizacion.Clases
{
    public class Principal
    {
        private String fechaN;
        private String FAntes;
        private string FDespues;
        RfcDestination rfcDesti = null;
        public bool sincroniza = false;
        EntityConnectionStringBuilder connection;

        public Principal(ConfigSAP cSAP, EntityConnectionStringBuilder conn)
        {
            DateTime dt = DateTime.Now;
            fechaN = string.Format("{0:yyyy/MM/dd}", dt);
            fechaN = fechaN.Substring(0, 10).Replace("/", "");
            this.FAntes = FchAntes(fechaN.Substring(0, 4), fechaN.Substring(4, 2), fechaN.Substring(6, 2));
            this.FDespues = FchDespues(fechaN.Substring(0, 4), fechaN.Substring(4, 2), fechaN.Substring(6, 2));
            try
            {
                this.connection = conn;
                RfcConfigParameters parametros = new RfcConfigParameters();
                parametros.Add(RfcConfigParameters.SAPRouter, cSAP.SAProuter as string);
                parametros.Add(RfcConfigParameters.Client, cSAP.client as string);
                parametros.Add(RfcConfigParameters.Language, cSAP.language as string);
                parametros.Add(RfcConfigParameters.User, cSAP.user as string);
                parametros.Add(RfcConfigParameters.Password, cSAP.password as string);
                parametros.Add(RfcConfigParameters.AppServerHost, cSAP.applicationServer as string);
                parametros.Add(RfcConfigParameters.SystemNumber, cSAP.systemNumberSAP as string);
                parametros.Add(RfcConfigParameters.SystemID, cSAP.systemSAP as string);
                parametros.Add(RfcConfigParameters.Name, cSAP.systemSAP as string);
                try
                {
                    RfcDestination rfcDest = RfcDestinationManager.GetDestination(parametros);
                }
                catch (Exception) { }
                rfcDesti = null;
                rfcDesti = RfcDestinationManager.GetDestination(cSAP.systemSAP as string);
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCION = repo.CreateFunction("ZMATERIALES");
            }
            catch (Exception) { }
        }
        private string FchAntes(string anio, string mes, string dia)
        {
            switch (mes)
            {
                case "01": { return Convert.ToInt32(anio) - 1 + "1001"; }
                case "02": { return Convert.ToInt32(anio) - 1 + "1101"; }
                case "03": { return Convert.ToInt32(anio) - 1 + "1201"; }
                case "04": { return anio + "0101"; }
                case "05": { return anio + "0201"; }
                case "06": { return anio + "0301"; }
                case "07": { return anio + "0401"; }
                case "08": { return anio + "0501"; }
                case "09": { return anio + "0601"; }
                case "10": { return anio + "0701"; }
                case "11": { return anio + "0801"; }
                case "12": { return anio + "0901"; }
                default: return "";
            }
        }
        private string FchDespues(string anio, string mes, string dia)
        {
            switch (mes)
            {
                case "01": { return anio + "0401"; }
                case "02": { return anio + "0501"; }
                case "03": { return anio + "0601"; }
                case "04": { return anio + "0701"; }
                case "05": { return anio + "0801"; }
                case "06": { return anio + "0901"; }
                case "07": { return anio + "1001"; }
                case "08": { return anio + "1101"; }
                case "09": { return anio + "1201"; }
                case "10": { return Convert.ToInt32(anio) + 1 + "0101"; }
                case "11": { return Convert.ToInt32(anio) + 1 + "0201"; }
                case "12": { return Convert.ToInt32(anio) + 1 + "0301"; }
                default: return "";
            }
        }
        public bool Ping()
        {
            try
            {
                rfcDesti.Ping();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool P_ZSAM_RFC_SINCRONIZAR()
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSAM_RFC_SINCRONIZAR");
            IRfcTable tsi = FUNCTION.GetTable("INTERFAZ");
            //FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tsi)
            {
                Sincronizacion sinco = new Sincronizacion();
                try
                {
                    sinco.VKORG = linea.GetValue("WERKS").ToString();
                    sinco.HY_INTERFAZ = linea.GetValue("INTERFAZ").ToString();
                    sinco.NAME = linea.GetValue("NAME").ToString();
                    sinco.SECUENCIA = linea.GetValue("SECUENCIA").ToString();
                    sinco.ACTIVA = linea.GetValue("ACTIVA").ToString();
                    sinco.BBP_TABL = linea.GetValue("BBP_TABL").ToString();
                    sinco.PERIODO = linea.GetValue("PERIODO").ToString();
                    sinco.SELEC = linea.GetValue("SELEC").ToString();
                    sinco.SINCRONIZACION = linea.GetValue("SINCRONIZACION").ToString();
                    sinco.CARGA_INICIAL = linea.GetValue("CARGA_INICIAL").ToString();
                    DALC_sincronizacion.ObtenerInstancia().IngresaSincronizacion(connection, sinco);
                }
                catch (Exception) { }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_RFC_SINCRONIZAR", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSAM_RFC_SINCRONIZAR", "X", "1000");
            return true;
        }
        public bool P_ZTIEMPO_RFC(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZTIEMPO_RFC");
            IRfcTable tie = FUNCTION.GetTable("TIEMPO");
            FUNCTION.SetValue("CENTRO", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tie)
            {
                Tiempo ti = new Tiempo();
                try
                {
                    ti.WERKS = linea.GetValue("WERKS").ToString();
                    ti.BBP_TABL = linea.GetValue("BBP_TABL").ToString();
                    ti.PERIODO = linea.GetValue("PERIODO").ToString();

                    DALC_sincronizacion.ObtenerInstancia().IngresarTiempos(connection, ti);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZTIEMPO_RFC", "Correcto");
            return true;
        }
        public bool P_ZSAM_USER_PERMISOS(string centro)
        {
            RfcRepository mca = rfcDesti.Repository;
            IRfcFunction FUNCTION = mca.CreateFunction("ZSAM_USER_PERMISOS");
            IRfcTable usu = FUNCTION.GetTable("ZSAM_PAHCME");
            IRfcTable usga = FUNCTION.GetTable("ZSAM_USGA");
            IRfcTable usgcc = FUNCTION.GetTable("ZSAM_USGK");
            IRfcTable cagcc = FUNCTION.GetTable("ZSAM_GKL");
            IRfcTable cagma = FUNCTION.GetTable("ZSAM_GUL");
            IRfcTable lmgm = FUNCTION.GetTable("ZSAM_LMGU");
            IRfcTable lcgc = FUNCTION.GetTable("ZSAM_LCGK");
            //FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
                DALC_Usuarios.ObtenerInstancia().VaciarGrupoCC(connection);
                DALC_Usuarios.ObtenerInstancia().VaciarGrupoMateriales(connection);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in usu)
            {
                Usuarios us = new Usuarios();
                try
                {
                    us.PERNR = linea.GetValue("PERNR").ToString();
                    us.ENDDA = linea.GetValue("ENDDA").ToString();
                    us.BEGDA = linea.GetValue("BEGDA").ToString();
                    us.NACHN = linea.GetValue("NACHN").ToString();
                    us.NACH2 = linea.GetValue("NACH2").ToString();
                    us.VORNA = linea.GetValue("VORNA").ToString();
                    us.STRAS = linea.GetValue("STRAS").ToString();
                    us.ORT01 = linea.GetValue("ORT01").ToString();
                    us.ORT02 = linea.GetValue("ORT02").ToString();
                    us.PSTLZ = linea.GetValue("PSTLZ").ToString();
                    us.LAND1 = linea.GetValue("LAND1").ToString();
                    us.TELNR = linea.GetValue("TELNR").ToString();
                    us.NUM01 = linea.GetValue("NUM01").ToString();
                    us.WERKS = linea.GetValue("WERKS").ToString();
                    us.PERMISOS = linea.GetValue("PERMISOS").ToString();
                    us.ZSAM_AC = linea.GetValue("ZSAM_AC").ToString();
                    us.USER_SAP = linea.GetValue("USER_SAP").ToString();
                    us.EMAIL = linea.GetValue("EMAIL").ToString();
                    us.LGORT = linea.GetValue("LGORT").ToString();
                    us.FIELD86 = linea.GetValue("FIELD86").ToString();
                    us.FIELD87 = linea.GetValue("FIELD87").ToString();
                    List<SELECT_usuario_MDL_Result> u = DALC_Usuarios.ObtenerInstancia().BuscarUsuario(connection, us).ToList();
                    if (u.Count == 0)
                    {
                        DALC_Usuarios.ObtenerInstancia().InsertarUsuario(connection, us);
                    }
                    else
                    {
                        DALC_Usuarios.ObtenerInstancia().ActualizarUsuario(connection, us);
                    }
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in usga)
            {
                UsuarioGrupoMateriales ugm = new UsuarioGrupoMateriales();
                try
                {
                    ugm.PERNR = linea.GetValue("PERNR").ToString();
                    ugm.ZSAM_GU = linea.GetValue("ZSAM_GU").ToString();
                    DALC_Usuarios.ObtenerInstancia().VaciarGrupoMateriales(connection, ugm);
                    DALC_Usuarios.ObtenerInstancia().IngresarGrupoMateriales(connection, ugm);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in usga)
            //{
            //    UsuarioGrupoMateriales ugm = new UsuarioGrupoMateriales();
            //    try
            //    {
            //        ugm.PERNR = linea.GetValue("PERNR").ToString();
            //        DALC_Usuarios.ObtenerInstancia().VaciarGrupoMateriales(connection, ugm);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in usga)
            //{
            //    UsuarioGrupoMateriales ugm = new UsuarioGrupoMateriales();
            //    try
            //    {
            //        ugm.PERNR = linea.GetValue("PERNR").ToString();
            //        ugm.ZSAM_GU = linea.GetValue("ZSAM_GU").ToString();
            //        DALC_Usuarios.ObtenerInstancia().IngresarGrupoMateriales(connection, ugm);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in usgcc)
            {
                UsuarioGrupoCentroCoste ugcc = new UsuarioGrupoCentroCoste();
                try
                {
                    ugcc.PERNR = linea.GetValue("PERNR").ToString();
                    ugcc.ZSAM_GK = linea.GetValue("ZSAM_GK").ToString();
                    DALC_Usuarios.ObtenerInstancia().VaciarUsuarioGrupoCentroCoste(connection, ugcc);
                    DALC_Usuarios.ObtenerInstancia().IngresarUsuarioGrupoCentroCoste(connection, ugcc);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in usgcc)
            //{
            //    UsuarioGrupoCentroCoste ugcc = new UsuarioGrupoCentroCoste();
            //    try
            //    {
            //        ugcc.PERNR = linea.GetValue("PERNR").ToString();
            //        DALC_Usuarios.ObtenerInstancia().VaciarUsuarioGrupoCentroCoste(connection, ugcc);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in usgcc)
            //{
            //    UsuarioGrupoCentroCoste ugcc = new UsuarioGrupoCentroCoste();
            //    try
            //    {
            //        ugcc.PERNR = linea.GetValue("PERNR").ToString();
            //        ugcc.ZSAM_GK = linea.GetValue("ZSAM_GK").ToString();
            //        DALC_Usuarios.ObtenerInstancia().IngresarUsuarioGrupoCentroCoste(connection, ugcc);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in cagcc)
            {
                CatalogoGrupoCentroCostos cgc = new CatalogoGrupoCentroCostos();
                try
                {
                    cgc.ZSAM_GK = linea.GetValue("ZSAM_GK").ToString();
                    cgc.DESCRIPCION = linea.GetValue("DESCRIPCION").ToString();
                    DALC_Usuarios.ObtenerInstancia().IngresaCatalogoGrupoCentroCostos(connection, cgc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in cagma)
            {
                CatalogoGrupoMateriales cgm = new CatalogoGrupoMateriales();
                try
                {
                    cgm.ZSAM_GU = linea.GetValue("ZSAM_GU").ToString();
                    cgm.DESCRIPCION = linea.GetValue("DESCRIPCION").ToString();
                    DALC_Usuarios.ObtenerInstancia().IngresaCatalogoGrupoMateriales(connection, cgm);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in lmgm)
            //{
            //    ListaMaterialGrupoMaterial lmg = new ListaMaterialGrupoMaterial();
            //    try
            //    {
            //        lmg.MATNR = linea.GetValue("MATNR").ToString();
            //        DALC_Usuarios.ObtenerInstancia().VaciarListaMaterialesGrupoMaterial(connection, lmg);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in lmgm)
            //{
            //    ListaMaterialGrupoMaterial lmg = new ListaMaterialGrupoMaterial();
            //    try
            //    {
            //        lmg.MTART = linea.GetValue("MTART").ToString();
            //        lmg.MATNR = linea.GetValue("MATNR").ToString();
            //        lmg.ZSAM_GU = linea.GetValue("ZSAM_GU").ToString();
            //        DALC_Usuarios.ObtenerInstancia().IngresaListaMaterialGrupoMateerial(connection, lmg);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in lmgm)
            {
                ListaMaterialGrupoMaterial lmg = new ListaMaterialGrupoMaterial();
                try
                {
                    lmg.MTART = linea.GetValue("MTART").ToString();
                    lmg.MATNR = linea.GetValue("MATNR").ToString();
                    lmg.ZSAM_GU = linea.GetValue("ZSAM_GU").ToString();
                    DALC_Usuarios.ObtenerInstancia().VaciarListaMaterialesGrupoMaterial(connection, lmg);
                    DALC_Usuarios.ObtenerInstancia().IngresaListaMaterialGrupoMateerial(connection, lmg);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in lcgc)
            //{
            //    ListaCentroCostoGrupoCosto lccgc = new ListaCentroCostoGrupoCosto();
            //    try
            //    {
            //        lccgc.ZSAM_GK = linea.GetValue("ZSAM_GK").ToString();
            //        DALC_Usuarios.ObtenerInstancia().VaciarListaCentroCostoGrupoCosto(connection, lccgc);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in lcgc)
            //{
            //    ListaCentroCostoGrupoCosto lccgc = new ListaCentroCostoGrupoCosto();
            //    try
            //    {
            //        lccgc.ZSAM_GK = linea.GetValue("ZSAM_GK").ToString();
            //        lccgc.KOSTL = linea.GetValue("KOSTL").ToString();
            //        lccgc.BUKRS = linea.GetValue("BUKRS").ToString();
            //        lccgc.KSTAR = linea.GetValue("KSTAR").ToString();
            //        DALC_Usuarios.ObtenerInstancia().IngresaListaCentroCostoGrupoCosto(connection, lccgc);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in lcgc)
            {
                ListaCentroCostoGrupoCosto lccgc = new ListaCentroCostoGrupoCosto();
                try
                {
                    lccgc.ZSAM_GK = linea.GetValue("ZSAM_GK").ToString();
                    lccgc.KOSTL = linea.GetValue("KOSTL").ToString();
                    lccgc.BUKRS = linea.GetValue("BUKRS").ToString();
                    lccgc.KSTAR = linea.GetValue("KSTAR").ToString();
                    DALC_Usuarios.ObtenerInstancia().VaciarListaCentroCostoGrupoCosto(connection, lccgc);
                    DALC_Usuarios.ObtenerInstancia().IngresaListaCentroCostoGrupoCosto(connection, lccgc);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_USER_PERMISOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSAM_USER_PERMISOS", "X", centro);
            return true;
        }
        public bool P_ZMM_OBYC(string centro)
        {
            RfcRepository mca = rfcDesti.Repository;
            IRfcFunction FUNCTION = mca.CreateFunction("ZMM_OBYC");
            IRfcTable ttre = FUNCTION.GetTable("ZT030");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ttre)
            {
                OBYC onyc = new OBYC();
                try
                {
                    onyc.KTOPL = linea.GetValue("KTOPL").ToString();
                    DALC_Obyc.ObtenerInstancia().borraObyc(connection, onyc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ttre)
            {
                OBYC obyc = new OBYC();
                try
                {
                    obyc.KTOPL = linea.GetValue("KTOPL").ToString();
                    obyc.KTOSL = linea.GetValue("KTOSL").ToString();
                    obyc.KOMOK = linea.GetValue("KOMOK").ToString();
                    obyc.BKLAS = linea.GetValue("BKLAS").ToString();
                    obyc.KONTS = linea.GetValue("KONTS").ToString();

                    DALC_Obyc.ObtenerInstancia().InsertarObyc(connection, obyc);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_OBYC", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_OBYC", "X", centro);
            return true;
        }
        public bool P_ZMATERIAL_CENTRO_303(string centro)
        {
            RfcRepository dm = rfcDesti.Repository;
            IRfcFunction FUNCTION = dm.CreateFunction("ZMATERIAL_CENTRO_303");
            FUNCTION.SetValue("CENTRO", centro);
            IRfcTable mat = FUNCTION.GetTable("MATERIALES");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in mat)
            {
                try
                {
                    MaterialesAlmacen adm = new MaterialesAlmacen();
                    adm.MATNR = linea.GetValue("MATNR").ToString();
                    adm.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_MaterialesAlmacen.ObtenerInstancia().BorrarMateralAlm303(connection, adm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in mat)
            {
                MaterialesAlmacen adm = new MaterialesAlmacen();
                try
                {
                    adm.MATNR = linea.GetValue("MATNR").ToString();
                    adm.WERKS = linea.GetValue("WERKS").ToString();
                    adm.LGORT = linea.GetValue("LGORT").ToString();
                    adm.MAKTX = linea.GetValue("MAKTX_ES").ToString();
                    adm.LVORM = linea.GetValue("LVORM").ToString();

                    DALC_MaterialesAlmacen.ObtenerInstancia().IngresarMaterialesAlmacen_303(connection, adm);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMATERIAL_CENTRO_303", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMATERIAL_CENTRO_303", "X", centro);
            return true;
        }
        public bool P_ZMATERIAL_CENTRO(string centro)
        {
            RfcRepository dm = rfcDesti.Repository;
            IRfcFunction FUNCTION = dm.CreateFunction("ZMATERIAL_CENTRO");
            FUNCTION.SetValue("CENTRO", centro);
            IRfcTable mat = FUNCTION.GetTable("MATERIALES");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            try
            {
                DALC_MaterialesAlmacen.ObtenerInstancia().BorrarMaterialesAlmacen(connection, centro);
            }
            catch (Exception) { return false; }
            //foreach (IRfcStructure linea in mat)
            //{
            //    MaterialesAlmacen adm = new MaterialesAlmacen();
            //    try
            //    {
            //        adm.MATNR = linea.GetValue("MATNR").ToString();
            //        adm.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_MaterialesAlmacen.ObtenerInstancia().BorrarMateralAlm(connection, adm);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in mat)
            {
                MaterialesAlmacen adm = new MaterialesAlmacen();
                try
                {
                    adm.MATNR = linea.GetValue("MATNR").ToString();
                    adm.WERKS = linea.GetValue("WERKS").ToString();
                    adm.LGORT = linea.GetValue("LGORT").ToString();
                    adm.MAKTX = linea.GetValue("MAKTX_ES").ToString();
                    adm.LVORM = linea.GetValue("LVORM").ToString();
                    DALC_MaterialesAlmacen.ObtenerInstancia().IngresarMaterialesAlmacen(connection, adm);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMATERIAL_CENTRO", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMATERIAL_CENTRO", "X", centro);
            return true;
        }
        public bool P_ZSAM_ORDENES_NOPM(string centro)
        {
            RfcRepository rep = rfcDesti.Repository;
            IRfcFunction FUNCTION = rep.CreateFunction("ZSAM_ORDENES_NOPM");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tabOrdenes = FUNCTION.GetTable("ORDENES");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabOrdenes)
            {
                Ordenes_MM orm = new Ordenes_MM();
                try
                {
                    //falta centro para eliminar
                    orm.AUFNR = linea.GetValue("AUFNR").ToString();
                    DALC_OrdenesMM.ObtenerInstancia().VaciarORDENES_MM(connection, orm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tabOrdenes)
            {
                Ordenes_MM om = new Ordenes_MM();
                try
                {
                    om.AUART = linea.GetValue("AUART").ToString();
                    om.KOKRS = linea.GetValue("KOKRS").ToString();
                    om.AUFNR = linea.GetValue("AUFNR").ToString();
                    om.KTEXT = linea.GetValue("KTEXT").ToString();
                    om.ESTATUS = linea.GetValue("ESTATUS").ToString();
                    om.LVORM = linea.GetValue("LVORM").ToString();

                    DALC_OrdenesMM.ObtenerInstancia().IngresaORDENES_MM(connection, om);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_ORDENES_NOPM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSAM_ORDENES_NOPM", "X", centro);
            return true;
        }
        public bool P_ZPM_VIS_NOTIFICACIONES(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZPM_VIS_NOTIFICACIONES");
            FUNCTION.SetValue("GSTRP_LOW", FAntes);
            FUNCTION.SetValue("GLTRP_HIGH", FDespues);
            FUNCTION.SetValue("PWERK", centro);
            IRfcTable ta1 = FUNCTION.GetTable("NOTIFICACIONES");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                NOTIFICACIONES not = new NOTIFICACIONES();
                try
                {
                    not.AUFNR = linea.GetValue("AUFNR").ToString();
                    not.VORNR = linea.GetValue("VORNR").ToString();
                    not.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_VisNot.ObtenerInstancia().VaciarNOTIFICACIONES(connection, not);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                NOTIFICACIONES um = new NOTIFICACIONES();
                try
                {
                    um.AUFNR = linea.GetValue("AUFNR").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.CABECERA = linea.GetValue("CABECERA").ToString();
                    um.VORNR = linea.GetValue("VORNR").ToString();
                    um.UVORN = linea.GetValue("UVORN").ToString();
                    um.KAPAR = linea.GetValue("KAPAR").ToString();
                    um.RMZHL = linea.GetValue("RMZHL").ToString();
                    um.AUERU = linea.GetValue("AUERU").ToString();
                    um.STOKZ = linea.GetValue("STOKZ").ToString();
                    um.BUDAT = linea.GetValue("BUDAT").ToString();
                    um.ARBPL = linea.GetValue("ARBPL").ToString();
                    um.ISMNW_2 = linea.GetValue("ISMNW_2").ToString();
                    um.ISMNU = linea.GetValue("ISMNU").ToString();
                    um.LEARR = linea.GetValue("LEARR").ToString();
                    um.LTXA1 = linea.GetValue("LTXA1").ToString();
                    um.SATZA = linea.GetValue("SATZA").ToString();
                    um.ISDD = linea.GetValue("ISDD").ToString();
                    um.IEDD = linea.GetValue("IEDD").ToString();
                    um.OFMNW = linea.GetValue("OFMNW").ToString();
                    um.ARBEI = linea.GetValue("ARBEI").ToString();
                    um.FSAVD = linea.GetValue("FSAVD").ToString();
                    um.SSAVD = linea.GetValue("SSAVD").ToString();
                    um.FSEDD = linea.GetValue("FSEDD").ToString();
                    um.SSEDD = linea.GetValue("SSEDD").ToString();
                    um.ARBID = linea.GetValue("ARBID").ToString();
                    um.LVORM = linea.GetValue("LVORM").ToString();

                    DALC_VisNot.ObtenerInstancia().IngresaNOTIFICACIONES(connection, um);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPM_VIS_NOTIFICACIONES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZPM_VIS_NOTIFICACIONES", "X", centro);
            return true;
        }
        public bool P_ZHOJA_RUTA_SAM(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZHOJA_RUTA_SAM");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tab = FUNCTION.GetTable("HOJA_RUTA");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tab)
            {
                HojasRuta hr = new HojasRuta();
                try
                {
                    hr.PLNNR = linea.GetValue("PLNNR").ToString();
                    hr.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_HojaRuta.obtenerInstania().VaciarHojasRuta(connection, hr);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tab)
            {
                HojasRuta hr = new HojasRuta();
                try
                {
                    hr.EQUNR = linea.GetValue("EQUNR").ToString();
                    hr.PLNNR = linea.GetValue("PLNNR").ToString();
                    hr.PLNAL = linea.GetValue("PLNAL").ToString();
                    hr.ZKRIZ = linea.GetValue("ZKRIZ").ToString();
                    hr.ZAEHL = linea.GetValue("ZAEHL").ToString();
                    hr.PLNTY = linea.GetValue("PLNTY").ToString();
                    hr.PLNKN = linea.GetValue("PLNKN").ToString();
                    hr.PLNFL = linea.GetValue("PLNFL").ToString();
                    hr.ZAEHL1 = linea.GetValue("ZAEHL1").ToString();
                    hr.ZAEHL2 = linea.GetValue("ZAEHL2").ToString();
                    hr.OBJTY = linea.GetValue("OBJTY").ToString();
                    hr.OBJID = linea.GetValue("OBJID").ToString();
                    hr.LOEKZ = linea.GetValue("LOEKZ").ToString();
                    hr.WERKS = linea.GetValue("WERKS").ToString();
                    hr.STEUS = linea.GetValue("STEUS").ToString();
                    hr.ARBID = linea.GetValue("ARBID").ToString();
                    hr.BMSCH = linea.GetValue("BMSCH").ToString();
                    hr.DAUNO = linea.GetValue("DAUNO").ToString();
                    hr.DAUNE = linea.GetValue("DAUNE").ToString();
                    hr.ARBEI = linea.GetValue("ARBEI").ToString();
                    hr.ARBEH = linea.GetValue("ARBEH").ToString();
                    hr.STATU = linea.GetValue("STATU").ToString();
                    hr.KTEXT = linea.GetValue("KTEXT").ToString();
                    hr.STLNR = linea.GetValue("STLNR").ToString();
                    hr.STLAL = linea.GetValue("STLAL").ToString();
                    hr.VORNR = linea.GetValue("VORNR").ToString();
                    hr.WERKSI = linea.GetValue("WERKSI").ToString();
                    hr.LTXA1 = linea.GetValue("LTXA1").ToString();
                    hr.LTXA2 = linea.GetValue("LTXA2").ToString();
                    hr.TXTSP = linea.GetValue("TXTSP").ToString();
                    hr.MEINH = linea.GetValue("MEINH").ToString();
                    hr.UMREN = linea.GetValue("UMREN").ToString();
                    hr.UMREZ = linea.GetValue("UMREZ").ToString();
                    hr.ZMERH = linea.GetValue("ZMERH").ToString();
                    hr.ZEIER = linea.GetValue("ZEIER").ToString();
                    hr.LAR01 = linea.GetValue("LAR01").ToString();
                    hr.VGE01 = linea.GetValue("VGE01").ToString();
                    hr.VGW01 = linea.GetValue("VGW01").ToString();
                    hr.LAR02 = linea.GetValue("LAR02").ToString();
                    hr.VGE02 = linea.GetValue("VGE02").ToString();
                    hr.VGW02 = linea.GetValue("VGW02").ToString();
                    hr.LAR03 = linea.GetValue("LAR03").ToString();
                    hr.VGE03 = linea.GetValue("VGE03").ToString();
                    hr.VGW03 = linea.GetValue("VGW03").ToString();
                    hr.LAR04 = linea.GetValue("LAR04").ToString();
                    hr.VGE04 = linea.GetValue("VGE04").ToString();
                    hr.VGW04 = linea.GetValue("VGW04").ToString();
                    hr.LAR05 = linea.GetValue("LAR05").ToString();
                    hr.VGE05 = linea.GetValue("VGE05").ToString();
                    hr.VGW05 = linea.GetValue("VGW05").ToString();
                    hr.LAR06 = linea.GetValue("LAR06").ToString();
                    hr.VGE06 = linea.GetValue("VGE06").ToString();
                    hr.VGW06 = linea.GetValue("VGW06").ToString();
                    hr.LIFNR = linea.GetValue("LIFNR").ToString();
                    hr.PEINH = linea.GetValue("PEINH").ToString();
                    hr.SAKTO = linea.GetValue("SAKTO").ToString();
                    hr.WAERS = linea.GetValue("WAERS").ToString();
                    hr.INFNR = linea.GetValue("INFNR").ToString();
                    hr.ESOKZ = linea.GetValue("ESOKZ").ToString();
                    hr.EKORG = linea.GetValue("EKORG").ToString();
                    hr.EKGRP = linea.GetValue("EKGRP").ToString();
                    hr.MATKL = linea.GetValue("MATKL").ToString();
                    hr.ARBPL = linea.GetValue("ARBPL").ToString();
                    hr.ANZZL = linea.GetValue("ANZZL").ToString();
                    hr.SRVPOS = linea.GetValue("SRVPOS").ToString();
                    hr.KTEXT1 = linea.GetValue("KTEXT1").ToString();

                    DALC_HojaRuta.obtenerInstania().IngresaHojasRuta(connection, hr);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZHOJA_RUTA_SAM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZHOJA_RUTA_SAM", "X", centro);
            return true;
        }
        public bool P_ZUBI_EQU(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZUBI_EQU");
            FUNCTION.SetValue("IWERK", centro);
            IRfcTable tab = FUNCTION.GetTable("EQUIPOS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tab)
            {
                Equipos eq = new Equipos();
                try
                {
                    eq.EQUNR = linea.GetValue("EQUNR").ToString();
                    eq.SWERK = linea.GetValue("SWERK").ToString();
                    DALC_Equipos.obtenerInstancia().VaciarEquipos(connection, eq);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tab)
            {
                Equipos eq = new Equipos();
                try
                {
                    eq.TPLNR = linea.GetValue("TPLNR").ToString();
                    eq.EQUNR = linea.GetValue("EQUNR").ToString();
                    eq.EQKTX_ES = linea.GetValue("EQKTX_ES").ToString();
                    eq.EQKTX_EN = linea.GetValue("EQKTX_EN").ToString();
                    eq.EQTYP = linea.GetValue("EQTYP").ToString();
                    eq.EQART = linea.GetValue("EQART").ToString();
                    eq.BEGRU = linea.GetValue("BEGRU").ToString();
                    eq.HERLD = linea.GetValue("HERLD").ToString();
                    eq.HERST = linea.GetValue("HERST").ToString();
                    eq.TYPBZ = linea.GetValue("TYPBZ").ToString();
                    eq.EMATN = linea.GetValue("EMATN").ToString();
                    eq.SERGE = linea.GetValue("SERGE").ToString();
                    eq.BAUJJ = linea.GetValue("BAUJJ").ToString();
                    eq.BAUMM = linea.GetValue("BAUMM").ToString();
                    eq.SWERK = linea.GetValue("SWERK").ToString();
                    eq.STORT = linea.GetValue("STORT").ToString();
                    eq.BEBER = linea.GetValue("BEBER").ToString();
                    eq.EQFNR = linea.GetValue("EQFNR").ToString();
                    eq.BUKRS = linea.GetValue("BUKRS").ToString();
                    eq.ANLNR = linea.GetValue("ANLNR").ToString();
                    eq.KOSTL = linea.GetValue("KOSTL").ToString();
                    eq.IWERK = linea.GetValue("IWERK").ToString();
                    eq.GEWRK = linea.GetValue("GEWRK").ToString();
                    eq.ERNAM = linea.GetValue("ERNAM").ToString();
                    eq.WKCTR = linea.GetValue("WKCTR").ToString();
                    eq.MATNR = linea.GetValue("MATNR").ToString();
                    eq.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                    eq.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                    eq.SERNR = linea.GetValue("SERNR").ToString();
                    eq.LBBSA = linea.GetValue("LBBSA").ToString();
                    eq.WERK = linea.GetValue("WERK").ToString();
                    eq.LAGER = linea.GetValue("LAGER").ToString();
                    eq.B_CHARGE = linea.GetValue("B_CHARGE").ToString();
                    eq.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    eq.KUNNR = linea.GetValue("KUNNR").ToString();
                    eq.LIFNR = linea.GetValue("LIFNR").ToString();
                    eq.DATLWB = linea.GetValue("DATLWB").ToString();
                    eq.POINT = linea.GetValue("POINT").ToString();
                    eq.POINTM = linea.GetValue("POINTM").ToString();
                    eq.MRNGU = linea.GetValue("MRNGU").ToString();
                    eq.ABCKZ = linea.GetValue("ABCKZ").ToString();
                    eq.INGRP = linea.GetValue("INGRP").ToString();
                    eq.HEQUI = linea.GetValue("HEQUI").ToString();
                    eq.B_WERK = linea.GetValue("B_WERK").ToString();
                    eq.B_LAGER = linea.GetValue("B_LAGER").ToString();
                    eq.CHARGE = linea.GetValue("CHARGE").ToString();
                    eq.LVORM = linea.GetValue("LVORM").ToString();
                    eq.INVNR = linea.GetValue("INVNR").ToString();
                    eq.GROES = linea.GetValue("GROES").ToString();
                    eq.MAPAR = linea.GetValue("MAPAR").ToString();

                    DALC_Equipos.obtenerInstancia().IngresaEquipo(connection, eq);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZUBI_EQU", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZUBI_EQU", "X", centro);
            return true;
        }
        public bool P_ZPM_NOTIFICA(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZPM_NOTIFICA");
            FUNCTION.SetValue("CENTRO", centro);
            IRfcTable ta1 = FUNCTION.GetTable("NOTIFICA");
            IRfcTable ta2 = FUNCTION.GetTable("ACTIVIDADES");
            IRfcTable ta3 = FUNCTION.GetTable("TEXTOS");
            IRfcTable ta4 = FUNCTION.GetTable("DMS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                Avisos av = new Avisos();
                try
                {
                    av.QMNUM = linea.GetValue("QMNUM").ToString();
                    av.IWERK = linea.GetValue("IWERK").ToString();
                    DALC_Avisos.obtenerInstancia().VaciarAvisos(connection, av);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                Avisos av = new Avisos();
                try
                {
                    av.QMNUM = linea.GetValue("QMNUM").ToString();
                    av.QMART = linea.GetValue("QMART").ToString();
                    av.QMTXT = linea.GetValue("QMTXT").ToString();
                    av.TXT04_ES = linea.GetValue("TXT04_ES").ToString();
                    av.TXT04_EN = linea.GetValue("TXT04_EN").ToString();
                    av.TXT30_ES = linea.GetValue("TXT30_ES").ToString();
                    av.TXT30_EN = linea.GetValue("TXT30_EN").ToString();
                    av.TPLNR = linea.GetValue("TPLNR").ToString();
                    av.PLTXT = linea.GetValue("PLTXT").ToString();
                    av.EQUNR = linea.GetValue("EQUNR").ToString();
                    av.EQKTX = linea.GetValue("EQKTX").ToString();
                    av.BAUTL = linea.GetValue("BAUTL").ToString();
                    av.INGRP = linea.GetValue("INGRP").ToString();
                    av.IWERK = linea.GetValue("IWERK").ToString();
                    av.INNAM = linea.GetValue("INNAM").ToString();
                    av.ARBPL = linea.GetValue("ARBPL").ToString();
                    av.SWERK = linea.GetValue("SWERK").ToString();
                    av.KTEXT = linea.GetValue("KTEXT").ToString();
                    av.I_PARNR = linea.GetValue("I_PARNR").ToString();
                    av.NAME_LIST = linea.GetValue("NAME_LIST").ToString();
                    av.PARNR_VERA = linea.GetValue("PARNR_VERA").ToString();
                    av.NAME_VERA = linea.GetValue("NAME_VERA").ToString();
                    av.QMNAM = linea.GetValue("QMNAM").ToString();
                    av.QMDAT = linea.GetValue("QMDAT").ToString();
                    av.MZEIT = linea.GetValue("MZEIT").ToString();
                    av.QMGRP = linea.GetValue("QMGRP").ToString();
                    av.HEADKTXT = linea.GetValue("HEADKTXT").ToString();
                    av.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    av.LVORM = linea.GetValue("LVORM").ToString();
                    av.AUFNR = linea.GetValue("ORDEN").ToString();

                    DALC_Avisos.obtenerInstancia().IngresarAvisos(connection, av);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Actividad ac = new Actividad();
                try
                {
                    ac.QMNUM = linea.GetValue("QMNUM").ToString();
                    //agregar centro
                    DALC_Avisos.obtenerInstancia().VaciarActividad(connection, ac);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Actividad ac = new Actividad();
                try
                {
                    ac.QMNUM = linea.GetValue("QMNUM").ToString();
                    ac.FENUM = linea.GetValue("FENUM").ToString();
                    ac.MNGRP = linea.GetValue("MNGRP").ToString();
                    ac.MNCOD = linea.GetValue("MNCOD").ToString();
                    ac.KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
                    ac.KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();
                    ac.MATXT = linea.GetValue("MATXT").ToString();
                    ac.MNGFA = linea.GetValue("MNGFA").ToString();
                    ac.PSTER = linea.GetValue("PSTER").ToString();
                    ac.PSTUR = linea.GetValue("PSTUR").ToString();
                    ac.PETER = linea.GetValue("PETER").ToString();
                    ac.PETUR = linea.GetValue("PETUR").ToString();
                    ac.MANUM = linea.GetValue("MANUM").ToString();
                    ac.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();

                    DALC_Avisos.obtenerInstancia().IngresarActividad(connection, ac);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta3)
            {
                Textos tx = new Textos();
                try
                {
                    tx.QMNUM = linea.GetValue("QMNUM").ToString();
                    //falta centro
                    DALC_Avisos.obtenerInstancia().VaciarTextos(connection, tx);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta3)
            {
                Textos tx = new Textos();
                try
                {
                    tx.QMNUM = linea.GetValue("QMNUM").ToString();
                    tx.TEXTO = linea.GetValue("TEXTO").ToString();
                    tx.FOIO_SAM = linea.GetValue("FOIO_SAM").ToString();

                    DALC_Avisos.obtenerInstancia().IngresarTextos(connection, tx);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta4)
            {
                DMS dm = new DMS();
                try
                {
                    dm.QMNUM = linea.GetValue("QMNUM").ToString();
                    //agregar centro
                    DALC_Avisos.obtenerInstancia().VaciarDMS(connection, dm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta4)
            {
                DMS dm = new DMS();
                try
                {
                    dm.QMNUM = linea.GetValue("QMNUM").ToString();
                    dm.DOKAR = linea.GetValue("DOKAR").ToString();
                    dm.DOKNR = linea.GetValue("DOKNR").ToString();
                    dm.LATEST_REL = linea.GetValue("LATEST_REL").ToString();
                    dm.LATEST_VERSION = linea.GetValue("LATEST_VERSION").ToString();
                    dm.ICON_STATUS = linea.GetValue("ICON_STATUS").ToString();
                    dm.DOKTL = linea.GetValue("DOKTL").ToString();
                    dm.DOKVR = linea.GetValue("DOKVR").ToString();
                    dm.DKTXT = linea.GetValue("DKTXT").ToString();

                    DALC_Avisos.obtenerInstancia().IngresarDMS(connection, dm);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPM_NOTIFICA", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZPM_NOTIFICA", "X", centro);
            return true;
        }
        public bool P_ZUBI_TEC(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZUBI_TEC");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable ta1 = FUNCTION.GetTable("UBICACION");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Ubicaciones.obtenerInstania().VaciarUbicacion(connection, centro);
            //foreach (IRfcStructure linea in ta1)
            //{
            //    Ubicaciones ub = new Ubicaciones();
            //    try
            //    {
            //        ub.TPLNR = linea.GetValue("TPLNR").ToString();
            //        ub.IWERK = linea.GetValue("IWERK").ToString();
            //        DALC_Ubicaciones.obtenerInstania().VaciarUbicacion(connection, ub);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in ta1)
            {
                Ubicaciones ub = new Ubicaciones();
                try
                {
                    ub.TPLNR = linea.GetValue("TPLNR").ToString();
                    ub.PLTXT_ES = linea.GetValue("PLTXT_ES").ToString();
                    ub.PLTXT_EN = linea.GetValue("PLTXT_EN").ToString();
                    ub.IWERK = linea.GetValue("IWERK").ToString();
                    ub.TPLKZ = linea.GetValue("TPLKZ").ToString();
                    ub.BEGRU = linea.GetValue("BEGRU").ToString();
                    ub.HERLD = linea.GetValue("HERLD").ToString();
                    ub.TYPBZ = linea.GetValue("TYPBZ").ToString();
                    ub.MAPAR = linea.GetValue("MAPAR").ToString();
                    ub.SERGE = linea.GetValue("SERGE").ToString();
                    ub.BAUJJ = linea.GetValue("BAUJJ").ToString();
                    ub.SWERK = linea.GetValue("SWERK").ToString();
                    ub.STORT = linea.GetValue("STORT").ToString();
                    ub.BEBER = linea.GetValue("BEBER").ToString();
                    ub.EQFNR = linea.GetValue("EQFNR").ToString();
                    ub.BUKRS = linea.GetValue("BUKRS").ToString();
                    ub.KOSTL = linea.GetValue("KOSTL").ToString();
                    ub.INGRP = linea.GetValue("INGRP").ToString();
                    ub.LGWID = linea.GetValue("LGWID").ToString();
                    ub.IEQUI = linea.GetValue("IEQUI").ToString();
                    ub.EINZL = linea.GetValue("EINZL").ToString();
                    ub.PPSID = linea.GetValue("PPSID").ToString();
                    ub.ARBPL = linea.GetValue("ARBPL").ToString();
                    ub.LVORM = linea.GetValue("LVORM").ToString();
                    ub.ABCKZ = linea.GetValue("ABCKZ").ToString();
                    ub.INVNR = linea.GetValue("INVNR").ToString();
                    ub.GROES = linea.GetValue("GROES").ToString();

                    DALC_Ubicaciones.obtenerInstania().IngresaUbicacion(connection, ub);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZUBI_TEC", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZUBI_TEC", "X", centro);
            return true;
        }
        public bool P_ZORDENES_PM(string centro)
        {
            List<SELECT_lista_clase_orden_MDL_Result> co = DALC_ClaseOrdenesPMSAM.ObtenerInstancia().ObtenerClaseOrden(connection).ToList();
            if (co.Count > 0)
            {
                foreach (SELECT_lista_clase_orden_MDL_Result cla in co)
                {
                    RfcRepository repo = rfcDesti.Repository;
                    IRfcFunction FUNCTION = repo.CreateFunction("ZORDENES_PM");
                    //FUNCTION.SetValue("GSTRP_LOW", FAntes);
                    //FUNCTION.SetValue("GLTRP_HIGH", FDespues);
                    FUNCTION.SetValue("PWERK", centro);
                    FUNCTION.SetValue("AUART", cla.clase_orden);
                    IRfcTable ta1 = FUNCTION.GetTable("ORDENES_PM");
                    IRfcTable ta2 = FUNCTION.GetTable("OPERACIONES_PM");
                    IRfcTable ta3 = FUNCTION.GetTable("COMPONENTES");
                    IRfcTable ser = FUNCTION.GetTable("SERVICIOS");
                    IRfcTable txt = FUNCTION.GetTable("TEXTOS_OPERACION");
                    IRfcTable ocon = FUNCTION.GetTable("ORDENES_CONTROL");
                    IRfcTable oemp = FUNCTION.GetTable("ORDENES_EMPLAZAMIENTO");
                    IRfcTable opla = FUNCTION.GetTable("ORDENES_PLANIFICACION");
                    try
                    {
                        FUNCTION.Invoke(rfcDesti);
                    }
                    catch (Exception) { return false; }
                    foreach (IRfcStructure linea in ta1)
                    {
                        PP_Plan pp = new PP_Plan();
                        try
                        {
                            pp.AUFNR = linea.GetValue("AUFNR").ToString();
                            pp.WAWRK = linea.GetValue("WAWRK").ToString();
                            DALC_PP_Plan.obtenerInstancia().VaciarPlan(connection, pp);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta1)
                    {
                        PP_Plan pp = new PP_Plan();
                        try
                        {
                            pp.AUFNR = linea.GetValue("AUFNR").ToString();
                            pp.FOLIOSAM = linea.GetValue("FOLIO_SAM").ToString();
                            pp.AUART = linea.GetValue("AUART").ToString();
                            pp.ESTATUS = linea.GetValue("ESTATUS").ToString();
                            pp.KTEXT = linea.GetValue("KTEXT").ToString();
                            pp.VAPLZ = linea.GetValue("VAPLZ").ToString();
                            pp.WAWRK = linea.GetValue("WAWRK").ToString();
                            pp.QMNUM = linea.GetValue("QMNUM").ToString();
                            pp.USER4 = linea.GetValue("USER4").ToString();
                            pp.ILART = linea.GetValue("ILART").ToString();
                            pp.ANLZU = linea.GetValue("ANLZU").ToString();
                            pp.GSTRP = linea.GetValue("GSTRP").ToString();
                            pp.GLTRP = linea.GetValue("GLTRP").ToString();
                            pp.PRIOK = linea.GetValue("PRIOK").ToString();
                            pp.REVNR = linea.GetValue("REVNR").ToString();
                            pp.AUFPL = linea.GetValue("AUFPL").ToString();
                            pp.TPLNR = linea.GetValue("TPLNR").ToString();
                            pp.PLTXT = linea.GetValue("PLTXT").ToString();
                            pp.EQUNR = linea.GetValue("EQUNR").ToString();
                            pp.EQKTX = linea.GetValue("EQKTX").ToString();
                            pp.BAUTL = linea.GetValue("BAUTL").ToString();
                            pp.LVORM = linea.GetValue("LVORM").ToString();

                            DALC_PP_Plan.obtenerInstancia().IngresaPlan(connection, pp);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta2)
                    {
                        PP_PlanOP op = new PP_PlanOP();
                        try
                        {
                            op.AUFNR = linea.GetValue("AUFNR").ToString();
                            op.WERKS = linea.GetValue("WERKS").ToString();
                            DALC_PP_Plan.obtenerInstancia().VaciarPlanOP(connection, op);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta2)
                    {
                        PP_PlanOP op = new PP_PlanOP();
                        try
                        {
                            op.AUFNR = linea.GetValue("AUFNR").ToString();
                            op.FOLIOSAM = linea.GetValue("FOLIO_SAM").ToString();
                            op.VORNR = linea.GetValue("VORNR").ToString();
                            op.UVORN = linea.GetValue("UVORN").ToString();
                            op.ARBPL = linea.GetValue("ARBPL").ToString();
                            op.WERKS = linea.GetValue("WERKS").ToString();
                            op.STEUS = linea.GetValue("STEUS").ToString();
                            op.KTSCH = linea.GetValue("KTSCH").ToString();
                            op.ANLZU = linea.GetValue("ANLZU").ToString();
                            op.LTXA1 = linea.GetValue("LTXA1").ToString();
                            op.ISMNW = linea.GetValue("ISMNW").ToString();
                            op.ARBEI = linea.GetValue("ARBEI").ToString();
                            op.ARBEH = linea.GetValue("ARBEH").ToString();
                            op.ANZZL = linea.GetValue("ANZZL").ToString();
                            op.DAUNO = linea.GetValue("DAUNO").ToString();
                            op.DAUNE = linea.GetValue("DAUNE").ToString();
                            op.INDET = linea.GetValue("INDET").ToString();
                            op.LARNT = linea.GetValue("LARNT").ToString();
                            op.WEMPF = linea.GetValue("WEMPF").ToString();
                            op.ABLAD = linea.GetValue("ABLAD").ToString();
                            op.AUFKT = linea.GetValue("AUFKT").ToString();
                            op.FLG_KMP = linea.GetValue("FLG_KMP").ToString();
                            op.FLG_FHM = linea.GetValue("FLG_FHM").ToString();
                            op.TPLNR = linea.GetValue("TPLNR").ToString();
                            op.EQUNR = linea.GetValue("EQUNR").ToString();
                            op.QMNUM = linea.GetValue("QMNUM").ToString();
                            op.NPLDA = linea.GetValue("NPLDA").ToString();
                            op.AUDISP = linea.GetValue("AUDISP").ToString();
                            op.FSAVZ = linea.GetValue("FSAVZ").ToString();
                            op.FSAVD = linea.GetValue("FSAVD").ToString();
                            op.FSEDD = linea.GetValue("FSEDD").ToString();
                            op.FSEDZ = linea.GetValue("FSEDZ").ToString();

                            DALC_PP_Plan.obtenerInstancia().IngresaPlanOP(connection, op);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta3)
                    {
                        Componentes cc = new Componentes();
                        try
                        {
                            cc.AUFNR = linea.GetValue("AUFNR").ToString();
                            cc.WERKS = linea.GetValue("WERKS").ToString();
                            DALC_PP_Plan.obtenerInstancia().VaciarComponente(connection, cc);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta3)
                    {
                        Componentes cc = new Componentes();
                        try
                        {
                            cc.AUFNR = linea.GetValue("AUFNR").ToString();
                            cc.FOLIOSAM = linea.GetValue("FOLIO_SAM").ToString();
                            cc.POSNR = linea.GetValue("POSNR").ToString();
                            cc.MATNR = linea.GetValue("MATNR").ToString();
                            cc.MATXT = linea.GetValue("MATXT").ToString();
                            cc.MENGE = linea.GetValue("MENGE").ToString();
                            cc.EINHEIT = linea.GetValue("EINHEIT").ToString();
                            cc.POSTP = linea.GetValue("POSTP").ToString();
                            cc.SOBKZ_D = linea.GetValue("SOBKZ_D").ToString();
                            cc.LGORT = linea.GetValue("LGORT").ToString();
                            cc.WERKS = linea.GetValue("WERKS").ToString();
                            cc.VORNR = linea.GetValue("VORNR").ToString();
                            cc.CHARG = linea.GetValue("CHARG").ToString();
                            cc.WEMPF = linea.GetValue("WEMPF").ToString();
                            cc.ABLAD = linea.GetValue("ABLAD").ToString();
                            cc.XLOEK = linea.GetValue("XLOEK").ToString();
                            cc.SCHGT = linea.GetValue("SCHGT").ToString();
                            cc.RGEKZ = linea.GetValue("RGEKZ").ToString();
                            cc.AUDISP = linea.GetValue("AUDISP").ToString();

                            DALC_PP_Plan.obtenerInstancia().IngresaComponente(connection, cc);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ser)
                    {
                        ServiciosPM sp = new ServiciosPM();
                        try
                        {
                            sp.AUFNR = linea.GetValue("AUFNR").ToString();
                            //falta centro
                            DALC_PP_Plan.obtenerInstancia().VaciarServiciosPM(connection, sp);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ser)
                    {
                        ServiciosPM sp = new ServiciosPM();
                        try
                        {
                            sp.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                            sp.AUFNR = linea.GetValue("AUFNR").ToString();
                            sp.PACKNO = linea.GetValue("PACKNO").ToString();
                            sp.INTROW = linea.GetValue("INTROW").ToString();
                            sp.EXTROW = linea.GetValue("EXTROW").ToString();
                            sp.DEL = linea.GetValue("DEL").ToString();
                            sp.SRVPOS = linea.GetValue("SRVPOS").ToString();
                            sp.RANG = linea.GetValue("RANG").ToString();
                            sp.EXTGROUP = linea.GetValue("EXTGROUP").ToString();
                            sp.PAQUETE = linea.GetValue("PAQUETE").ToString();
                            sp.SUB_PACKNO = linea.GetValue("SUB_PACKNO").ToString();
                            sp.LBNUM = linea.GetValue("LBNUM").ToString();
                            sp.AUSGB = linea.GetValue("AUSGB").ToString();
                            sp.STLVPOS = linea.GetValue("STLVPOS").ToString();
                            sp.EXTSRVNO = linea.GetValue("EXTSRVNO").ToString();
                            sp.MENGE = linea.GetValue("MENGE").ToString();
                            sp.MEINS = linea.GetValue("MEINS").ToString();
                            sp.UEBTO = linea.GetValue("UEBTO").ToString();
                            sp.UEBTK = linea.GetValue("UEBTK").ToString();
                            sp.WITH_LIM = linea.GetValue("WITH_LIM").ToString();
                            sp.SPINF = linea.GetValue("SPINF").ToString();
                            sp.PEINH = linea.GetValue("PEINH").ToString();
                            sp.BRTWR = linea.GetValue("BRTWR").ToString();
                            sp.NETWR = linea.GetValue("NETWR").ToString();
                            sp.FROMPOS = linea.GetValue("FROMPOS").ToString();
                            sp.TOPOS = linea.GetValue("TOPOS").ToString();
                            sp.KTEXT1 = linea.GetValue("KTEXT1").ToString();
                            sp.VRTKZ = linea.GetValue("VRTKZ").ToString();
                            sp.TWRKZ = linea.GetValue("TWRKZ").ToString();
                            sp.PERNR = linea.GetValue("PERNR").ToString();
                            sp.MOLGA = linea.GetValue("MOLGA").ToString();
                            sp.LGART = linea.GetValue("LGART").ToString();
                            sp.LGTXT = linea.GetValue("LGTXT").ToString();
                            sp.STELL = linea.GetValue("STELL").ToString();
                            sp.IFTNR = linea.GetValue("IFTNR").ToString();
                            sp.BUDAT = linea.GetValue("BUDAT").ToString();
                            sp.INSDT = linea.GetValue("INSDT").ToString();
                            sp.PLN_PACKNO = linea.GetValue("PLN_PACKNO").ToString();
                            sp.PLN_INTROW = linea.GetValue("PLN_INTROW").ToString();
                            sp.KNT_PACKNO = linea.GetValue("KNT_PACKNO").ToString();
                            sp.KNT_INTROW = linea.GetValue("KNT_INTROW").ToString();
                            sp.TMP_PACKNO = linea.GetValue("TMP_PACKNO").ToString();
                            sp.TMP_INTROW = linea.GetValue("TMP_INTROW").ToString();
                            sp.STLV_LIM = linea.GetValue("STLV_LIM").ToString();
                            sp.LIMIT_ROW = linea.GetValue("LIMIT_ROW").ToString();
                            sp.ACT_MENGE = linea.GetValue("ACT_MENGE").ToString();
                            sp.ACT_WERT = linea.GetValue("ACT_WERT").ToString();
                            sp.KNT_WERT = linea.GetValue("KNT_WERT").ToString();
                            sp.KNT_MENGE = linea.GetValue("KNT_MENGE").ToString();
                            sp.ZIELWERT = linea.GetValue("ZIELWERT").ToString();
                            sp.UNG_WERT = linea.GetValue("UNG_WERT").ToString();
                            sp.UNG_MENGE = linea.GetValue("UNG_MENGE").ToString();
                            sp.ALT_INTROW = linea.GetValue("ALT_INTROW").ToString();
                            sp.BASIC = linea.GetValue("BASIC").ToString();
                            sp.ALTERNAT = linea.GetValue("ALTERNAT").ToString();
                            sp.BIDDER = linea.GetValue("BIDDER").ToString();
                            sp.SUPPLE = linea.GetValue("SUPPLE").ToString();
                            sp.FREEQTY = linea.GetValue("FREEQTY").ToString();
                            sp.INFORM = linea.GetValue("INFORM").ToString();
                            sp.PAUSCH = linea.GetValue("PAUSCH").ToString();
                            sp.EVENTUAL = linea.GetValue("EVENTUAL").ToString();
                            sp.MWSKZ = linea.GetValue("MWSKZ").ToString();
                            sp.TXJCD = linea.GetValue("TXJCD").ToString();
                            sp.PRS_CHG = linea.GetValue("PRS_CHG").ToString();
                            sp.MATKL = linea.GetValue("MATKL").ToString();
                            sp.TBTWR = linea.GetValue("TBTWR").ToString();
                            sp.NAVNW = linea.GetValue("NAVNW").ToString();
                            sp.BASWR = linea.GetValue("BASWR").ToString();
                            sp.KKNUMV = linea.GetValue("KKNUMV").ToString();
                            sp.IWEIN = linea.GetValue("IWEIN").ToString();
                            sp.INT_WORK = linea.GetValue("INT_WORK").ToString();
                            sp.EXTERNALID = linea.GetValue("EXTERNALID").ToString();
                            sp.KSTAR = linea.GetValue("KSTAR").ToString();
                            sp.ACT_WORK = linea.GetValue("ACT_WORK").ToString();
                            sp.MAPNO = linea.GetValue("MAPNO").ToString();
                            sp.SRVMAPKEY = linea.GetValue("SRVMAPKEY").ToString();
                            sp.TAXTARIFFCODE = linea.GetValue("TAXTARIFFCODE").ToString();
                            sp.SDATE = linea.GetValue("SDATE").ToString();
                            sp.BEGTIME = linea.GetValue("BEGTIME").ToString();
                            sp.ENDTIME = linea.GetValue("ENDTIME").ToString();
                            sp.PERSEXT = linea.GetValue("PERSEXT").ToString();
                            sp.CATSCOUNTE = linea.GetValue("CATSCOUNTE").ToString();
                            sp.STOKZ = linea.GetValue("STOKZ").ToString();
                            sp.BELNR = linea.GetValue("BELNR").ToString();
                            sp.FORMELNR = linea.GetValue("FORMELNR").ToString();
                            sp.FRMVAL1 = linea.GetValue("FRMVAL1").ToString();
                            sp.FRMVAL2 = linea.GetValue("FRMVAL2").ToString();
                            sp.FRMVAL3 = linea.GetValue("FRMVAL3").ToString();
                            sp.FRMVAL4 = linea.GetValue("FRMVAL4").ToString();
                            sp.FRMVAL5 = linea.GetValue("FRMVAL5").ToString();
                            sp.USERF1_NUM = linea.GetValue("USERF1_NUM").ToString();
                            sp.USERF2_NUM = linea.GetValue("USERF2_NUM").ToString();
                            sp.USERF1_TXT = linea.GetValue("USERF1_TXT").ToString();
                            sp.USERF2_TXT = linea.GetValue("USERF2_TXT").ToString();
                            sp.KNOBJ = linea.GetValue("KNOBJ").ToString();
                            sp.CHGTEXT = linea.GetValue("CHGTEXT").ToString();
                            sp.KALNR = linea.GetValue("KALNR").ToString();
                            sp.KLVAR = linea.GetValue("KLVAR").ToString();
                            sp.EXTDES = linea.GetValue("EXTDES").ToString();
                            sp.BOSINTER = linea.GetValue("BOSINTER").ToString();
                            sp.BOSGRP = linea.GetValue("BOSGRP").ToString();
                            sp.BOS_RISK = linea.GetValue("BOS_RISK").ToString();
                            sp.BOS_ECP = linea.GetValue("BOS_ECP").ToString();
                            sp.CHGLTEXT = linea.GetValue("CHGLTEXT").ToString();
                            sp.BOSGRUPPENR = linea.GetValue("BOSGRUPPENR").ToString();
                            sp.BOSLFDNR = linea.GetValue("BOSLFDNR").ToString();
                            sp.BOSDRUKZ = linea.GetValue("BOSDRUKZ").ToString();
                            sp.BOSSUPPLENO = linea.GetValue("BOSSUPPLENO").ToString();
                            sp.BOSSUPPLESTATUS = linea.GetValue("BOSSUPPLESTATUS").ToString();
                            sp.SAPBOQ_OBJTYPE = linea.GetValue("/SAPBOQ/OBJTYPE").ToString();
                            sp.SAPBOQ_SPOSNR = linea.GetValue("/SAPBOQ/SPOSNR").ToString();
                            sp.SAPBOQ_MINTROW = linea.GetValue("/SAPBOQ/MINTROW").ToString();
                            sp.SAPBOQ_QT_REL = linea.GetValue("/SAPBOQ/QT_REL").ToString();
                            sp.SAPBOQ_CK_QTY = linea.GetValue("/SAPBOQ/CK_QTY").ToString();
                            sp.SAPBOQ_M_FRATE = linea.GetValue("/SAPBOQ/M_FRATE").ToString();
                            sp.EXTREFKEY = linea.GetValue("EXTREFKEY").ToString();
                            sp.INV_MENGE = linea.GetValue("INV_MENGE").ToString();
                            sp.PER_SDATE = linea.GetValue("PER_SDATE").ToString();
                            sp.PER_EDATE = linea.GetValue("PER_EDATE").ToString();
                            sp.GL_ACCOUNT = linea.GetValue("GL_ACCOUNT").ToString();
                            sp.VORNR = linea.GetValue("VORNR").ToString();
                            sp.WAERS = linea.GetValue("WAERS").ToString();

                            DALC_PP_Plan.obtenerInstancia().IngresaServiciosPM(connection, sp);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in txt)
                    {
                        TextoPosicionesOrdenesVis txtposvis = new TextoPosicionesOrdenesVis();
                        try
                        {
                            txtposvis.AUFNR = linea.GetValue("AUFNR").ToString();
                            //falta centro
                            DALC_PP_Plan.obtenerInstancia().VaciarTextoPosicionOrdenVis(connection, txtposvis);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in txt)
                    {
                        TextoPosicionesOrdenesVis txtposvis = new TextoPosicionesOrdenesVis();
                        try
                        {
                            txtposvis.AUFNR = linea.GetValue("AUFNR").ToString();
                            txtposvis.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                            txtposvis.VORNR = linea.GetValue("VORNR").ToString();
                            txtposvis.INDICE = linea.GetValue("INDICE").ToString();
                            txtposvis.TDLINE = linea.GetValue("TDLINE").ToString();

                            DALC_PP_Plan.obtenerInstancia().IngresaTextoPosicionOrdenVis(connection, txtposvis);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ocon)
                    {
                        OrdenesControl oc = new OrdenesControl();
                        try
                        {
                            oc.AUFNR = linea.GetValue("AUFNR").ToString();
                            //falta centro
                            DALC_PP_Plan.obtenerInstancia().VaciarOrdenesControl(connection, oc);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ocon)
                    {
                        OrdenesControl oc = new OrdenesControl();
                        try
                        {
                            oc.AUFNR = linea.GetValue("AUFNR").ToString();
                            oc.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                            oc.ERNAM = linea.GetValue("ERNAM").ToString();
                            oc.ERDAT = linea.GetValue("ERDAT").ToString();
                            oc.AENAM = linea.GetValue("AENAM").ToString();
                            oc.AEDAT = linea.GetValue("AEDAT").ToString();
                            DALC_PP_Plan.obtenerInstancia().IngresarOrdenesControl(connection, oc);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in oemp)
                    {
                        OrdenesEmplazamiento oe = new OrdenesEmplazamiento();
                        try
                        {
                            oe.AUFNR = linea.GetValue("AUFNR").ToString();
                            oe.SWERK = linea.GetValue("SWERK").ToString();
                            DALC_PP_Plan.obtenerInstancia().VaciarOrdenesEmplazamiento(connection, oe);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in oemp)
                    {
                        OrdenesEmplazamiento oe = new OrdenesEmplazamiento();
                        try
                        {
                            oe.AUFNR = linea.GetValue("AUFNR").ToString();
                            oe.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                            oe.SWERK = linea.GetValue("SWERK").ToString();
                            oe.STORT = linea.GetValue("STORT").ToString();
                            oe.MSGRP = linea.GetValue("MSGRP").ToString();
                            oe.BEBER = linea.GetValue("BEBER").ToString();
                            oe.ARBPL = linea.GetValue("ARBPL").ToString();
                            oe.ABCKZ = linea.GetValue("ABCKZ").ToString();
                            oe.EQFNR = linea.GetValue("EQFNR").ToString();
                            oe.BUKRS = linea.GetValue("BUKRS").ToString();
                            oe.ANLNR = linea.GetValue("ANLNR").ToString();
                            oe.ANLUN = linea.GetValue("ANLUN").ToString();
                            oe.KOSTL = linea.GetValue("KOSTL").ToString();
                            oe.PROID = linea.GetValue("PROID").ToString();
                            DALC_PP_Plan.obtenerInstancia().IngresarOrdenesEmplazamiento(connection, oe);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in opla)
                    {
                        OrdenesPlanificacion opa = new OrdenesPlanificacion();
                        try
                        {
                            opa.AUFNR = linea.GetValue("AUFNR").ToString();
                            //falta centro
                            DALC_PP_Plan.obtenerInstancia().VaciarOrdenesPlanificacion(connection, opa);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in opla)
                    {
                        OrdenesPlanificacion opa = new OrdenesPlanificacion();
                        try
                        {
                            opa.AUFNR = linea.GetValue("AUFNR").ToString();
                            opa.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                            opa.WARPL = linea.GetValue("WARPL").ToString();
                            opa.WAPOS = linea.GetValue("WAPOS").ToString();
                            opa.PLNTY = linea.GetValue("PLNTY").ToString();
                            opa.PLNNR = linea.GetValue("PLNNR").ToString();
                            opa.PLNAL = linea.GetValue("PLNAL").ToString();
                            DALC_PP_Plan.obtenerInstancia().IngresarOrdenesPlanificacion(connection, opa);
                        }
                        catch (Exception) { return false; }
                    }
                }
                DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZORDENES_PM", "Correcto");
                DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZORDENES_PM", "X", centro);
                return true;
            }
            return true;
        }
        public bool P_ZRELACION_EQUIPOS(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZRELACION_EQUIPOS");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable ta1 = FUNCTION.GetTable("ENS_E");
            IRfcTable ta2 = FUNCTION.GetTable("ZENS_O");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                Relacion rl = new Relacion();
                try
                {
                    rl.EQUNR = linea.GetValue("EQUNR").ToString();
                    //falta centro
                    DALC_Relacion.obtenerInstania().VaciarRelacion(connection, rl);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                Relacion rl = new Relacion();
                try
                {
                    rl.JERARQUIA = linea.GetValue("JERARQUIA").ToString();
                    rl.NIVEL = linea.GetValue("NIVEL").ToString();
                    rl.TPLNR = linea.GetValue("TPLNR").ToString();
                    rl.EQUNR = linea.GetValue("EQUNR").ToString();
                    rl.EQUNR01 = linea.GetValue("EQUNR01").ToString();
                    rl.EQUNR02 = linea.GetValue("EQUNR02").ToString();
                    rl.EQUNR03 = linea.GetValue("EQUNR03").ToString();
                    rl.EQUNR04 = linea.GetValue("EQUNR04").ToString();
                    rl.EQUNR05 = linea.GetValue("EQUNR05").ToString();
                    rl.EQUNR06 = linea.GetValue("EQUNR06").ToString();
                    rl.EQUNR07 = linea.GetValue("EQUNR07").ToString();
                    rl.EQUNR08 = linea.GetValue("EQUNR08").ToString();
                    rl.EQUNR09 = linea.GetValue("EQUNR09").ToString();
                    rl.EQUNR10 = linea.GetValue("EQUNR10").ToString();
                    rl.EQUNR11 = linea.GetValue("EQUNR11").ToString();
                    rl.EQUNR12 = linea.GetValue("EQUNR12").ToString();
                    rl.EQUNR13 = linea.GetValue("EQUNR13").ToString();
                    rl.EQUNR14 = linea.GetValue("EQUNR14").ToString();
                    rl.EQUNR15 = linea.GetValue("EQUNR15").ToString();
                    rl.EQUNR16 = linea.GetValue("EQUNR16").ToString();
                    rl.EQUNR17 = linea.GetValue("EQUNR17").ToString();
                    rl.EQUNR18 = linea.GetValue("EQUNR18").ToString();
                    rl.EQUNR19 = linea.GetValue("EQUNR19").ToString();
                    rl.EQUNR20 = linea.GetValue("EQUNR20").ToString();
                    rl.MATNR = linea.GetValue("MATNR").ToString();
                    rl.CHARG = linea.GetValue("CHARG").ToString();
                    rl.WERKS = linea.GetValue("WERKS").ToString();
                    rl.SERNR = linea.GetValue("SERNR").ToString();
                    rl.LGORT = linea.GetValue("LGORT").ToString();
                    rl.POINT = linea.GetValue("POINT").ToString();
                    rl.MDOCM = linea.GetValue("MDOCM").ToString();
                    rl.RECDV = linea.GetValue("RECDV").ToString();
                    rl.RECDU = linea.GetValue("RECDU").ToString();
                    rl.EQKTX = linea.GetValue("EQKTX").ToString();
                    rl.ICONO = linea.GetValue("ICONO").ToString();
                    rl.RENGLON = linea.GetValue("RENGLON").ToString();
                    rl.NRECDV = linea.GetValue("NRECDV").ToString();
                    rl.READG = linea.GetValue("READG").ToString();
                    rl.NREADG = linea.GetValue("NREADG").ToString();
                    rl.LVORM = linea.GetValue("LVORM").ToString();

                    DALC_Relacion.obtenerInstania().IngresaRelacion(connection, rl);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Relacion2 rl = new Relacion2();
                try
                {
                    rl.EQUNR = linea.GetValue("EQUNR").ToString();
                    rl.IWERK = linea.GetValue("IWERK").ToString();
                    DALC_Relacion.obtenerInstania().VaciarRelacion2(connection, rl);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Relacion2 rl = new Relacion2();
                try
                {
                    rl.JERARQUIA = linea.GetValue("JERARQUIA").ToString();
                    rl.NIVEL = linea.GetValue("NIVEL").ToString();
                    rl.WAPOS = linea.GetValue("WAPOS").ToString();
                    rl.ABNUM = linea.GetValue("ABNUM").ToString();
                    rl.AUFNR = linea.GetValue("AUFNR").ToString();
                    rl.ADDAT = linea.GetValue("ADDAT").ToString();
                    rl.WARPL = linea.GetValue("WARPL").ToString();
                    rl.WSTRA = linea.GetValue("WSTRA").ToString();
                    rl.WPPOS = linea.GetValue("WPPOS").ToString();
                    rl.PSTXT = linea.GetValue("PSTXT").ToString();
                    rl.EQUNR = linea.GetValue("EQUNR").ToString();
                    rl.OBKNR = linea.GetValue("OBKNR").ToString();
                    rl.ERKNZ = linea.GetValue("ERKNZ").ToString();
                    rl.AEKNZ = linea.GetValue("AEKNZ").ToString();
                    rl.ERNAM = linea.GetValue("ERNAM").ToString();
                    rl.ERSDT = linea.GetValue("ERSDT").ToString();
                    rl.AEDAT = linea.GetValue("AEDAT").ToString();
                    rl.AENAM = linea.GetValue("AENAM").ToString();
                    rl.PLNTY = linea.GetValue("PLNTY").ToString();
                    rl.LAUFN = linea.GetValue("LAUFN").ToString();
                    rl.PLNNR = linea.GetValue("PLNNR").ToString();
                    rl.PLNAL = linea.GetValue("PLNAL").ToString();
                    rl.STATUS = linea.GetValue("STATUS").ToString();
                    rl.LTKNZ = linea.GetValue("LTKNZ").ToString();
                    rl.WPGRP = linea.GetValue("WPGRP").ToString();
                    rl.OBJTY = linea.GetValue("OBJTY").ToString();
                    rl.GEWRK = linea.GetValue("GEWRK").ToString();
                    rl.IWERK = linea.GetValue("IWERK").ToString();
                    rl.LANGU = linea.GetValue("LANGU").ToString();
                    rl.ILOAN = linea.GetValue("ILOAN").ToString();
                    rl.ILOAI = linea.GetValue("ILOAI").ToString();
                    rl.SERIALNR = linea.GetValue("SERIALNR").ToString();
                    rl.SERMAT = linea.GetValue("SERMAT").ToString();
                    rl.MATNR = linea.GetValue("MATNR").ToString();
                    rl.CHARG = linea.GetValue("CHARG").ToString();
                    rl.WERKS = linea.GetValue("WERKS").ToString();
                    rl.LGORT = linea.GetValue("LGORT").ToString();
                    rl.SERNR = linea.GetValue("SERNR").ToString();
                    rl.POINT = linea.GetValue("POINT").ToString();
                    rl.MDOCM = linea.GetValue("MDOCM").ToString();
                    rl.RECDV = linea.GetValue("RECDV").ToString();
                    rl.RECDU = linea.GetValue("RECDU").ToString();
                    rl.EQKTX = linea.GetValue("EQKTX").ToString();
                    rl.ICONO = linea.GetValue("ICONO").ToString();
                    rl.RENGLON = linea.GetValue("RENGLON").ToString();
                    rl.L_LINE = linea.GetValue("L_LINE").ToString();
                    rl.GSTRP = linea.GetValue("GSTRP").ToString();

                    DALC_Relacion.obtenerInstania().IngresaRelacion2(connection, rl);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZRELACION_EQUIPOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZRELACION_EQUIPOS", "X", centro);
            return true;
        }
        public bool P_ZMM_RESERVAS_MATERIALES(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZMM_RESERVAS_MATERIALES");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tabResb = FUNCTION.GetTable("IT_RESB");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_ReservasMateriales.ObtenerInstancia().VaciarReservasMateriales(connection, centro);
            //foreach (IRfcStructure linea in tabResb)
            //{
            //    ReservasMateriales rem = new ReservasMateriales();
            //    try
            //    {
            //        rem.RSNUM = linea.GetValue("RSNUM").ToString();
            //        rem.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_ReservasMateriales.ObtenerInstancia().VaciarReservasMateriales(connection, rem);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in tabResb)
            {
                ReservasMateriales rem = new ReservasMateriales();
                try
                {
                    rem.RSNUM = linea.GetValue("RSNUM").ToString();
                    rem.RSPOS = linea.GetValue("RSPOS").ToString();
                    rem.XLOEK = linea.GetValue("XLOEK").ToString();
                    rem.XWAOK = linea.GetValue("XWAOK").ToString();
                    rem.KZEAR = linea.GetValue("KZEAR").ToString();
                    rem.MATNR = linea.GetValue("MATNR").ToString();
                    rem.WERKS = linea.GetValue("WERKS").ToString();
                    rem.LGORT = linea.GetValue("LGORT").ToString();
                    rem.CHARG = linea.GetValue("CHARG").ToString();
                    rem.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    rem.BDTER = linea.GetValue("BDTER").ToString();
                    rem.BDMNG = linea.GetValue("BDMNG").ToString();
                    rem.MEINS = linea.GetValue("MEINS").ToString();
                    rem.SHKZG = linea.GetValue("SHKZG").ToString();
                    rem.FMENG = linea.GetValue("FMENG").ToString();
                    rem.ENMNG = linea.GetValue("ENMNG").ToString();
                    rem.ENWRT = linea.GetValue("ENWRT").ToString();
                    rem.WAERS = linea.GetValue("WAERS").ToString();
                    rem.ERFMG = linea.GetValue("ERFMG").ToString();
                    rem.ERFME = linea.GetValue("ERFME").ToString();
                    rem.KOSTL = linea.GetValue("KOSTL").ToString();
                    rem.AUFNR = linea.GetValue("AUFNR").ToString();
                    rem.BWART = linea.GetValue("BWART").ToString();
                    rem.SAKNR = linea.GetValue("SAKNR").ToString();
                    rem.UMWRK = linea.GetValue("UMWRK").ToString();
                    rem.UMLGO = linea.GetValue("UMLGO").ToString();
                    rem.SGTXT = linea.GetValue("SGTXT").ToString();
                    rem.LMENG = linea.GetValue("LMENG").ToString();
                    rem.STLTY = linea.GetValue("STLTY").ToString();
                    rem.STLNR = linea.GetValue("STLNR").ToString();
                    rem.POTX1 = linea.GetValue("POTX1").ToString();
                    rem.POTX2 = linea.GetValue("POTX2").ToString();
                    rem.UMREZ = linea.GetValue("UMREZ").ToString();
                    rem.UMREN = linea.GetValue("UMREN").ToString();
                    rem.AUFPL = linea.GetValue("AUFPL").ToString();
                    rem.PLNFL = linea.GetValue("PLNFL").ToString();
                    rem.VORNR = linea.GetValue("VORNR").ToString();
                    rem.PEINH = linea.GetValue("PEINH").ToString();
                    rem.AFPOS = linea.GetValue("AFPOS").ToString();
                    rem.MATKL = linea.GetValue("MATKL").ToString();
                    rem.LIFNR = linea.GetValue("LIFNR").ToString();
                    rem.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rem.LVORM = linea.GetValue("LVORM").ToString();
                    rem.UNAME = linea.GetValue("UNAME").ToString();

                    DALC_ReservasMateriales.ObtenerInstancia().IngresaReservasMateriales(connection, rem);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_RESERVAS_MATERIALES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_RESERVAS_MATERIALES", "X", centro);
            return true;
        }
        public bool P_ZQM_PLAN_INSP_CENTRO(string centro)
        {
            //RfcRepository qm = rfcDesti.Repository;
            //IRfcFunction FUNCTION = qm.CreateFunction("ZQM_PLAN_INSP_CENTRO");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable mat = FUNCTION.GetTable("ZPQMAT");
            //IRfcTable ope = FUNCTION.GetTable("ZQMOP");
            //IRfcTable car = FUNCTION.GetTable("ZQMCAR");
            //IRfcTable din = FUNCTION.GetTable("ZDATINSP");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in mat)
            //{
            //    MaterialesQM mt = new MaterialesQM();
            //    try
            //    {
            //        mt.MATNR = linea.GetValue("MATNR").ToString();
            //        mt.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_MaterialesQM.ObtenerIntancia().VaciarTablaMaterialesQM(connection, mt);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in mat)
            //{
            //    MaterialesQM mt = new MaterialesQM();
            //    try
            //    {
            //        mt.MATNR = linea.GetValue("MATNR").ToString();
            //        mt.WERKS = linea.GetValue("WERKS").ToString();
            //        mt.PLNTY = linea.GetValue("PLNTY").ToString();
            //        mt.PLNNR = linea.GetValue("PLNNR").ToString();
            //        mt.PLNAL = linea.GetValue("PLNAL").ToString();
            //        mt.ZKRIZ = linea.GetValue("ZKRIZ").ToString();
            //        mt.ZAEHL = linea.GetValue("ZAEHL").ToString();
            //        mt.SPRAS = linea.GetValue("SPRAS").ToString();
            //        mt.MAKTX = linea.GetValue("MAKTX").ToString();
            //        mt.LOEKZ = linea.GetValue("LOEKZ").ToString();
            //        mt.VERWE = linea.GetValue("VERWE").ToString();
            //        mt.STATU = linea.GetValue("STATU").ToString();

            //        DALC_MaterialesQM.ObtenerIntancia().IngresarMaterialesQM(connection, mt);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ope)
            //{
            //    OperacionesQM op = new OperacionesQM();
            //    try
            //    {
            //        op.MATNR = linea.GetValue("MATNR").ToString();
            //        op.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_OperacionesQM.ObtenerInstancia().VaciarOperacionesQM(connection, op);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ope)
            //{
            //    OperacionesQM op = new OperacionesQM();
            //    try
            //    {
            //        op.MATNR = linea.GetValue("MATNR").ToString();
            //        op.WERKS = linea.GetValue("WERKS").ToString();
            //        op.PLNTY = linea.GetValue("PLNTY").ToString();
            //        op.PLNNR = linea.GetValue("PLNNR").ToString();
            //        op.PLNAL = linea.GetValue("PLNAL").ToString();
            //        op.ZKRIZ = linea.GetValue("ZKRIZ").ToString();
            //        op.ZAEHL = linea.GetValue("ZAEHL").ToString();
            //        op.PLNFL = linea.GetValue("PLNFL").ToString();
            //        op.PLNKN = linea.GetValue("PLNKN").ToString();
            //        op.VORNR = linea.GetValue("VORNR").ToString();
            //        op.STEUS = linea.GetValue("STEUS").ToString();
            //        op.LTXA1 = linea.GetValue("LTXA1").ToString();
            //        op.QPPKTABS = linea.GetValue("QPPKTABS").ToString();
            //        op.MAKTX = linea.GetValue("MAKTX").ToString();
            //        op.RENGLON = linea.GetValue("RENGLON").ToString();

            //        DALC_OperacionesQM.ObtenerInstancia().IngresarOperacionesQM(connection, op);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in car)
            //{
            //    CaracterisitcasQM ca = new CaracterisitcasQM();
            //    try
            //    {
            //        ca.MATNR = linea.GetValue("MATNR").ToString();
            //        ca.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_CaracteristicasQM.obtenerInstancia().VaciarTablasCaracteristicasQM(connection, ca);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in car)
            //{
            //    CaracterisitcasQM ca = new CaracterisitcasQM();
            //    try
            //    {
            //        ca.MATNR = linea.GetValue("MATNR").ToString();
            //        ca.WERKS = linea.GetValue("WERKS").ToString();
            //        ca.PLNTY = linea.GetValue("PLNTY").ToString();
            //        ca.PLNNR = linea.GetValue("PLNNR").ToString();
            //        ca.PLNAL = linea.GetValue("PLNAL").ToString();
            //        ca.VORNR = linea.GetValue("VORNR").ToString();
            //        ca.QPPKTABS = linea.GetValue("QPPKTABS").ToString();
            //        ca.KURZTEXT = linea.GetValue("KURZTEXT").ToString();
            //        ca.LTEXTSPR = linea.GetValue("LTEXTSPR").ToString();
            //        ca.STICHPRVER = linea.GetValue("STICHPRVER").ToString();
            //        ca.PROBEMGEH = linea.GetValue("PROBEMGEH").ToString();
            //        ca.PRUEFEINH = linea.GetValue("PRUEFEINH").ToString();
            //        ca.KATAB1 = linea.GetValue("KATAB1").ToString();
            //        ca.KATALGART1 = linea.GetValue("KATALGART1").ToString();
            //        ca.AUSWMENGE1 = linea.GetValue("AUSWMENGE1").ToString();
            //        ca.AUSWMGWRK1 = linea.GetValue("AUSWMGWRK1").ToString();
            //        ca.STEUERKZ = linea.GetValue("STEUERKZ").ToString();
            //        ca.RENGLON = linea.GetValue("RENGLON").ToString();
            //        ca.MERKNR = linea.GetValue("MERKNR").ToString();
            //        ca.VERWMERKM = linea.GetValue("VERWMERKM").ToString();
            //        ca.CHAR_TYPE = linea.GetValue("CHAR_TYPE").ToString();
            //        ca.PRUEFUMF = linea.GetValue("PRUEFUMF").ToString();
            //        ca.KTX01 = linea.GetValue("KTX01").ToString();
            //        ca.QPMK_ZAEHL = linea.GetValue("QPMK_ZAEHL").ToString();
            //        ca.MASSEINHSW = linea.GetValue("MASSEINHSW").ToString();


            //        DALC_CaracteristicasQM.obtenerInstancia().IngresarCaracteristicasQM(connection, ca);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in din)
            //{
            //    DatosInspeccion insdat = new DatosInspeccion();
            //    try
            //    {
            //        insdat.MATNR = linea.GetValue("MATNR").ToString();
            //        insdat.WERKS = linea.GetValue("WERKS").ToString();

            //        DALC_CaracteristicasQM.obtenerInstancia().VaciarDatosInspeccion(connection, insdat);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in din)
            //{
            //    DatosInspeccion insdat = new DatosInspeccion();
            //    try
            //    {
            //        insdat.MATNR = linea.GetValue("MATNR").ToString();
            //        insdat.WERKS = linea.GetValue("WERKS").ToString();
            //        insdat.ART = linea.GetValue("ART").ToString();
            //        insdat.KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
            //        insdat.KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();
            //        insdat.APA = linea.GetValue("APA").ToString();
            //        insdat.AKTIV = linea.GetValue("AKTIV").ToString();
            //        insdat.CHG = linea.GetValue("CHG").ToString();

            //        DALC_CaracteristicasQM.obtenerInstancia().InsertarDatosInspeccion(connection, insdat);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZQM_PLAN_INSP_CENTRO", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZQM_PLAN_INSP_CENTRO", "X", centro);
            return true;
        }
        public bool P_ZSAM_LIST_MOV_VIS(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZSAM_LIST_MOV_VIS");
            FUNCTION.SetValue("TWERKS", centro);
            IRfcTable ta1 = FUNCTION.GetTable("TMIGO_CAB");
            IRfcTable ta2 = FUNCTION.GetTable("TMIGO_POS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                TMIGO_CAB um = new TMIGO_CAB();
                try
                {
                    um.MBLNR = linea.GetValue("MBLNR").ToString();
                    um.MJAHR = linea.GetValue("MJAHR").ToString();
                    um.BLDAT = linea.GetValue("BLDAT").ToString();
                    um.BUDAT = linea.GetValue("BUDAT").ToString();
                    um.LFSNR = linea.GetValue("LFSNR").ToString();
                    um.FRBNR = linea.GetValue("FRBNR").ToString();
                    um.BKTXT = linea.GetValue("BKTXT").ToString();
                    um.LIFNR = linea.GetValue("LIFNR").ToString();
                    um.NAME1 = linea.GetValue("NAME1").ToString();
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.UMLGO = linea.GetValue("UMLGO").ToString();
                    um.LVORM = linea.GetValue("LVORM").ToString();
                    DALC_ListaMov.ObtenerInstancia().VaciarTSAM_MOV_CABR(connection, um);
                    DALC_ListaMov.ObtenerInstancia().IngresaTMIGO_CAB(connection, um);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in ta1)
            //{
            //    TMIGO_CAB um = new TMIGO_CAB();
            //    try
            //    {
            //        um.MBLNR = linea.GetValue("MBLNR").ToString();
            //        //falta centro
            //        DALC_ListaMov.ObtenerInstancia().VaciarTSAM_MOV_CABR(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta1)
            //{
            //    TMIGO_CAB um = new TMIGO_CAB();
            //    try
            //    {
            //        um.MBLNR = linea.GetValue("MBLNR").ToString();
            //        um.MJAHR = linea.GetValue("MJAHR").ToString();
            //        um.BLDAT = linea.GetValue("BLDAT").ToString();
            //        um.BUDAT = linea.GetValue("BUDAT").ToString();
            //        um.LFSNR = linea.GetValue("LFSNR").ToString();
            //        um.FRBNR = linea.GetValue("FRBNR").ToString();
            //        um.BKTXT = linea.GetValue("BKTXT").ToString();
            //        um.LIFNR = linea.GetValue("LIFNR").ToString();
            //        um.NAME1 = linea.GetValue("NAME1").ToString();
            //        um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        um.UMLGO = linea.GetValue("UMLGO").ToString();
            //        um.LVORM = linea.GetValue("LVORM").ToString();

            //        DALC_ListaMov.ObtenerInstancia().IngresaTMIGO_CAB(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in ta2)
            {
                TMIGO_POS um = new TMIGO_POS();
                try
                {
                    um.MBLNR = linea.GetValue("MBLNR").ToString();
                    um.ZEILE = linea.GetValue("ZEILE").ToString();
                    um.LIFNR = linea.GetValue("LIFNR").ToString();
                    um.CHARG = linea.GetValue("CHARG").ToString();
                    um.BWART = linea.GetValue("BWART").ToString();
                    um.EBELN = linea.GetValue("EBELN").ToString();
                    um.EBELP = linea.GetValue("EBELP").ToString();
                    um.ABLAD = linea.GetValue("ABLAD").ToString();
                    um.WEMPF = linea.GetValue("WEMPF").ToString();
                    um.MATNR = linea.GetValue("MATNR").ToString();
                    um.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                    um.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                    um.ERFMG = linea.GetValue("ERFMG").ToString();
                    um.ERFME = linea.GetValue("ERFME").ToString();
                    um.KOSTL = linea.GetValue("KOSTL").ToString();
                    um.AUFNR = linea.GetValue("AUFNR").ToString();
                    um.BUKRS = linea.GetValue("BUKRS").ToString();
                    um.SAKTO = linea.GetValue("SAKTO").ToString();
                    um.IDNLF = linea.GetValue("IDNLF").ToString();
                    um.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.NAME2 = linea.GetValue("NAME2").ToString();
                    um.LGORT = linea.GetValue("LGORT").ToString();
                    um.LGOBE = linea.GetValue("LGOBE").ToString();
                    um.EMLIF = linea.GetValue("EMLIF").ToString();
                    um.NAME3 = linea.GetValue("NAME3").ToString();
                    um.SHKZG = linea.GetValue("SHKZG").ToString();
                    um.LPROVEDOR = linea.GetValue("LPROVEDOR").ToString();
                    um.MATKL = linea.GetValue("MATKL").ToString();
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.UMLGO = linea.GetValue("UMLGO").ToString();
                    um.LICHA = linea.GetValue("LICHA").ToString();
                    DALC_ListaMov.ObtenerInstancia().VaciarTSAM_MOVR(connection, um);
                    DALC_ListaMov.ObtenerInstancia().IngresaTMIGO_POS(connection, um);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in ta2)
            //{
            //    TMIGO_POS um = new TMIGO_POS();
            //    try
            //    {
            //        um.MBLNR = linea.GetValue("MBLNR").ToString();
            //        um.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_ListaMov.ObtenerInstancia().VaciarTSAM_MOVR(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta2)
            //{
            //    TMIGO_POS um = new TMIGO_POS();
            //    try
            //    {
            //        um.MBLNR = linea.GetValue("MBLNR").ToString();
            //        um.ZEILE = linea.GetValue("ZEILE").ToString();
            //        um.LIFNR = linea.GetValue("LIFNR").ToString();
            //        um.CHARG = linea.GetValue("CHARG").ToString();
            //        um.BWART = linea.GetValue("BWART").ToString();
            //        um.EBELN = linea.GetValue("EBELN").ToString();
            //        um.EBELP = linea.GetValue("EBELP").ToString();
            //        um.ABLAD = linea.GetValue("ABLAD").ToString();
            //        um.WEMPF = linea.GetValue("WEMPF").ToString();
            //        um.MATNR = linea.GetValue("MATNR").ToString();
            //        um.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
            //        um.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
            //        um.ERFMG = linea.GetValue("ERFMG").ToString();
            //        um.ERFME = linea.GetValue("ERFME").ToString();
            //        um.KOSTL = linea.GetValue("KOSTL").ToString();
            //        um.AUFNR = linea.GetValue("AUFNR").ToString();
            //        um.BUKRS = linea.GetValue("BUKRS").ToString();
            //        um.SAKTO = linea.GetValue("SAKTO").ToString();
            //        um.IDNLF = linea.GetValue("IDNLF").ToString();
            //        um.SOBKZ = linea.GetValue("SOBKZ").ToString();
            //        um.WERKS = linea.GetValue("WERKS").ToString();
            //        um.NAME2 = linea.GetValue("NAME2").ToString();
            //        um.LGORT = linea.GetValue("LGORT").ToString();
            //        um.LGOBE = linea.GetValue("LGOBE").ToString();
            //        um.EMLIF = linea.GetValue("EMLIF").ToString();
            //        um.NAME3 = linea.GetValue("NAME3").ToString();
            //        um.SHKZG = linea.GetValue("SHKZG").ToString();
            //        um.LPROVEDOR = linea.GetValue("LPROVEDOR").ToString();
            //        um.MATKL = linea.GetValue("MATKL").ToString();
            //        um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        um.UMLGO = linea.GetValue("UMLGO").ToString();
            //        um.LICHA = linea.GetValue("LICHA").ToString();

            //        DALC_ListaMov.ObtenerInstancia().IngresaTMIGO_POS(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_LIST_MOV_VIS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSAM_LIST_MOV_VIS", "X", centro);
            return true;
        }
        public bool P_ZPROVEEDORES(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZPROVEEDORES");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable pro = FUNCTION.GetTable("PROVEEDOR");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Proveedores.obtenerInstacia().VaciarProveedor(connection);
            //foreach (IRfcStructure linea in pro)
            //{
            //    Proveedores pr = new Proveedores();
            //    try
            //    {
            //        pr.LIFNR = linea.GetValue("LIFNR").ToString();
            //        pr.BUKRS = linea.GetValue("BUKRS").ToString();
            //        DALC_Proveedores.obtenerInstacia().VaciarProveedor(connection, pr);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in pro)
            {
                Proveedores pr = new Proveedores();
                try
                {
                    pr.LIFNR = linea.GetValue("LIFNR").ToString();
                    pr.BUKRS = linea.GetValue("BUKRS").ToString();
                    pr.EKORG = linea.GetValue("EKORG").ToString();
                    pr.ADRC = linea.GetValue("ADRC").ToString();
                    pr.DATE_FROM = linea.GetValue("DATE_FROM").ToString();
                    pr.NATION = linea.GetValue("NATION").ToString();
                    pr.DATE_TO = linea.GetValue("DATE_TO").ToString();
                    pr.TITLE = linea.GetValue("TITLE").ToString();
                    pr.NAME1 = linea.GetValue("NAME1").ToString();
                    pr.NAME2 = linea.GetValue("NAME2").ToString();
                    pr.NAME3 = linea.GetValue("NAME3").ToString();
                    pr.NAME4 = linea.GetValue("NAME4").ToString();
                    pr.CITY1 = linea.GetValue("CITY1").ToString();
                    pr.CITY2 = linea.GetValue("CITY2").ToString();
                    pr.CITY_CODE = linea.GetValue("CITY_CODE").ToString();
                    pr.CITYP_CODE = linea.GetValue("CITYP_CODE").ToString();
                    pr.HOME_CITY = linea.GetValue("HOME_CITY").ToString();
                    pr.STREET = linea.GetValue("STREET").ToString();
                    pr.HOUSE_NUM1 = linea.GetValue("HOUSE_NUM1").ToString();
                    pr.KTOKK = linea.GetValue("KTOKK").ToString();
                    pr.STCD1 = linea.GetValue("STCD1").ToString();
                    pr.ZTERM = linea.GetValue("ZTERM").ToString();
                    pr.AKONT = linea.GetValue("AKONT").ToString();
                    pr.MINBW = linea.GetValue("MINBW").ToString();
                    pr.WAERS = linea.GetValue("WAERS").ToString();
                    pr.INCO1 = linea.GetValue("INCO1").ToString();
                    pr.INCO2 = linea.GetValue("INCO2").ToString();
                    pr.BSTAE = linea.GetValue("BSTAE").ToString();
                    pr.EKGRP = linea.GetValue("EKGRP").ToString();
                    pr.LFABC = linea.GetValue("LFABC").ToString();
                    pr.LOEVMB = linea.GetValue("LOEVMB").ToString();
                    pr.SPERR = linea.GetValue("SPERR").ToString();
                    pr.LOEVM = linea.GetValue("LOEVM").ToString();
                    pr.LVORM = linea.GetValue("LVORM").ToString();

                    DALC_Proveedores.obtenerInstacia().IngresaProveedor(connection, pr);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPROVEEDORES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZPROVEEDORES", "X", centro);
            return true;
        }
        public bool P_ZCLIENTES(string centro)
        {
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZCLIENTES");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable cli = FUNCTION.GetTable("CLIENTES");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in cli)
            //{
            //    Clientes cl = new Clientes();
            //    try
            //    {
            //        cl.KUNNR = linea.GetValue("KUNNR").ToString();
            //        cl.BUKRS = linea.GetValue("BUKRS").ToString();
            //        DALC_Clientes.obtenerInstania().VaciarCliente(connection, cl);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in cli)
            //{
            //    Clientes cl = new Clientes();
            //    try
            //    {
            //        cl.KUNNR = linea.GetValue("KUNNR").ToString();
            //        cl.BUKRS = linea.GetValue("BUKRS").ToString();
            //        cl.VKORG = linea.GetValue("VKORG").ToString();
            //        cl.VTWEG = linea.GetValue("VTWEG").ToString();
            //        cl.SPART = linea.GetValue("SPART").ToString();
            //        cl.ADRC = linea.GetValue("ADRC").ToString();
            //        cl.DATE_FROM = linea.GetValue("DATE_FROM").ToString();
            //        cl.NATION = linea.GetValue("NATION").ToString();
            //        cl.DATE_TO = linea.GetValue("DATE_TO").ToString();
            //        cl.TITLE = linea.GetValue("TITLE").ToString();
            //        cl.NAME1 = linea.GetValue("NAME1").ToString();
            //        cl.NAME2 = linea.GetValue("NAME2").ToString();
            //        cl.NAME3 = linea.GetValue("NAME3").ToString();
            //        cl.NAME4 = linea.GetValue("NAME4").ToString();
            //        cl.CITY1 = linea.GetValue("CITY1").ToString();
            //        cl.CITY2 = linea.GetValue("CITY2").ToString();
            //        cl.CITY_CODE = linea.GetValue("CITY_CODE").ToString();
            //        cl.CITYP_CODE = linea.GetValue("CITYP_CODE").ToString();
            //        cl.HOME_CITY = linea.GetValue("HOME_CITY").ToString();
            //        cl.STREET = linea.GetValue("STREET").ToString();
            //        cl.HOUSE_NUM1 = linea.GetValue("HOUSE_NUM1").ToString();
            //        cl.KTOKK = linea.GetValue("KTOKK").ToString();
            //        cl.STCD1 = linea.GetValue("STCD1").ToString();
            //        cl.ZTERM = linea.GetValue("ZTERM").ToString();
            //        cl.AKONT = linea.GetValue("AKONT").ToString();
            //        cl.WAERS = linea.GetValue("WAERS").ToString();
            //        cl.INCO1 = linea.GetValue("INCO1").ToString();
            //        cl.INCO2 = linea.GetValue("INCO2").ToString();
            //        cl.VKGRP = linea.GetValue("VKGRP").ToString();
            //        cl.LFABC = linea.GetValue("LFABC").ToString();
            //        cl.LOEVMB = linea.GetValue("LOEVMB").ToString();
            //        cl.SPERR = linea.GetValue("SPERR").ToString();
            //        cl.LOEVM = linea.GetValue("LOEVM").ToString();
            //        cl.LVORM = linea.GetValue("LOEVM").ToString();

            //        DALC_Clientes.obtenerInstania().IngresaCliente(connection, cl);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCLIENTES", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCLIENTES", "X", centro);
            return true;
        }
        public bool P_ZORDENES_PMSAM(string centro)
        {
            List<SELECT_lista_clase_orden_MDL_Result> co = DALC_ClaseOrdenesPMSAM.ObtenerInstancia().ObtenerClaseOrden(connection).ToList();
            if (co.Count > 0)
            {
                foreach (SELECT_lista_clase_orden_MDL_Result cla in co)
                {
                    RfcRepository repo = rfcDesti.Repository;
                    IRfcFunction FUNCTION = repo.CreateFunction("ZORDENES_PMSAM");
                    FUNCTION.SetValue("WERKS", centro);
                    FUNCTION.SetValue("AUART", cla.clase_orden);
                    IRfcTable ta1 = FUNCTION.GetTable("ORDENES_PM");
                    IRfcTable ta2 = FUNCTION.GetTable("PM_OPERACIONES");
                    IRfcTable ta3 = FUNCTION.GetTable("EQUIPO");
                    IRfcTable ta4 = FUNCTION.GetTable("PM01");
                    IRfcTable ta5 = FUNCTION.GetTable("PM03_1");
                    IRfcTable ta6 = FUNCTION.GetTable("PM03_2");
                    IRfcTable ta7 = FUNCTION.GetTable("PM03_3");
                    //IRfcTable ta8 = FUNCTION.GetTable("QM01_1");
                    //IRfcTable ta9 = FUNCTION.GetTable("QM01_2");
                    //IRfcTable ta10 = FUNCTION.GetTable("QM01_3");
                    //IRfcTable ta11 = FUNCTION.GetTable("CAB_QM01");
                    //IRfcTable ta12 = FUNCTION.GetTable("ENSAMBLE");
                    //IRfcTable ta13 = FUNCTION.GetTable("CARACTERISTICAS");
                    //IRfcTable ta14 = FUNCTION.GetTable("DEC_CARACTERISTICAS");
                    //IRfcTable ta15 = FUNCTION.GetTable("DEC_LOTEINSPECCION");
                    //IRfcTable ta16 = FUNCTION.GetTable("TEXTOS_CAR");
                    try
                    {
                        FUNCTION.Invoke(rfcDesti);
                    }
                    catch (Exception) { return false; }
                    foreach (IRfcStructure linea in ta1)
                    {
                        ORDENES_PM or = new ORDENES_PM();
                        try
                        {
                            or.AUFNR = linea.GetValue("AUFNR").ToString();
                            or.IWERK = linea.GetValue("IWERK").ToString();
                            DALC_Ordenes.obtenerInstancia().VaciarORDENES_PM(connection, or);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta1)
                    {
                        ORDENES_PM or = new ORDENES_PM();
                        try
                        {
                            or.AUART = linea.GetValue("AUART").ToString();
                            or.KOKRS = linea.GetValue("KOKRS").ToString();
                            or.AUFNR = linea.GetValue("AUFNR").ToString();
                            or.ESTATUS = linea.GetValue("ESTATUS").ToString();
                            or.KTEXT = linea.GetValue("KTEXT").ToString();
                            or.AUTYP = linea.GetValue("AUTYP").ToString();
                            or.IWERK = linea.GetValue("IWERK").ToString();
                            or.LVORM = linea.GetValue("LOEKZ").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaORDENES_PM(connection, or);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta2)
                    {
                        PM_OPERACIONES pm = new PM_OPERACIONES();
                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.WERKS = linea.GetValue("WERKS").ToString();
                            DALC_Ordenes.obtenerInstancia().VaciarPM_OPERACIONES(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta2)
                    {
                        PM_OPERACIONES pm = new PM_OPERACIONES();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.AUFPL = linea.GetValue("AUFPL").ToString();
                            pm.APLZL = linea.GetValue("APLZL").ToString();
                            pm.PLNAL = linea.GetValue("PLNAL").ToString();
                            pm.PLNTY = linea.GetValue("PLNTY").ToString();
                            pm.VINTV = linea.GetValue("VINTV").ToString();
                            pm.PLNNR = linea.GetValue("PLNNR").ToString();
                            pm.ZAEHL = linea.GetValue("ZAEHL").ToString();
                            pm.VORNR = linea.GetValue("VORNR").ToString();
                            pm.STEUS = linea.GetValue("STEUS").ToString();
                            pm.ARBID = linea.GetValue("ARBID").ToString();
                            pm.WERKS = linea.GetValue("WERKS").ToString();
                            pm.LTXA1 = linea.GetValue("LTXA1").ToString();
                            pm.BMSCH = linea.GetValue("BMSCH").ToString();
                            pm.DAUNO = linea.GetValue("DAUNO").ToString();
                            pm.DAUNE = linea.GetValue("DAUNE").ToString();
                            pm.ARBEI = linea.GetValue("ARBEI").ToString();
                            pm.ARBEH = linea.GetValue("ARBEH").ToString();
                            pm.MGVRG = linea.GetValue("MGVRG").ToString();
                            pm.ISM01 = linea.GetValue("ISM01").ToString();
                            pm.ISM02 = linea.GetValue("ISM02").ToString();
                            pm.ISM03 = linea.GetValue("ISM03").ToString();
                            pm.ISM04 = linea.GetValue("ISM04").ToString();
                            pm.ISM05 = linea.GetValue("ISM05").ToString();
                            pm.ISM06 = linea.GetValue("ISM06").ToString();
                            pm.ILE01 = linea.GetValue("ILE01").ToString();
                            pm.ILE02 = linea.GetValue("ILE02").ToString();
                            pm.ILE03 = linea.GetValue("ILE03").ToString();
                            pm.ILE04 = linea.GetValue("ILE04").ToString();
                            pm.ILE05 = linea.GetValue("ILE05").ToString();
                            pm.ILE06 = linea.GetValue("ILE06").ToString();
                            pm.MEINH = linea.GetValue("MEINH").ToString();
                            pm.AUERU = linea.GetValue("AUERU").ToString();
                            pm.BANFN = linea.GetValue("BANFN").ToString();
                            pm.BNFPO = linea.GetValue("BNFPO").ToString();
                            pm.EKORG = linea.GetValue("EKORG").ToString();
                            pm.EKGRP = linea.GetValue("EKGRP").ToString();
                            pm.MATKL = linea.GetValue("MATKL").ToString();
                            pm.LIFNR = linea.GetValue("LIFNR").ToString();
                            pm.PREIS = linea.GetValue("PREIS").ToString();
                            pm.PEINH = linea.GetValue("PEINH").ToString();
                            pm.WAERS = linea.GetValue("WAERS").ToString();
                            pm.SAKTO = linea.GetValue("SAKTO").ToString();
                            pm.AFNAM = linea.GetValue("AFNAM").ToString();
                            pm.RUECK = linea.GetValue("RUECK").ToString();
                            pm.ISTRU = linea.GetValue("ISTRU").ToString();
                            pm.ISTRU2 = linea.GetValue("ISTRU2").ToString();
                            DALC_Ordenes.obtenerInstancia().IngresaPM_OPERACIONES(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta3)
                    {
                        Equipo_O eq = new Equipo_O();
                        try
                        {
                            eq.AUFNR = linea.GetValue("AUFNR").ToString();
                            eq.WERKS = linea.GetValue("WERKS").ToString();
                            DALC_Ordenes.obtenerInstancia().VaciarEquipo_O(connection, eq);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta3)
                    {
                        Equipo_O eq = new Equipo_O();

                        try
                        {
                            eq.EQUNR = linea.GetValue("EQUNR").ToString();
                            eq.MATNR = linea.GetValue("MATNR").ToString();
                            eq.SERNR = linea.GetValue("SERNR").ToString();
                            eq.WERKS = linea.GetValue("WERKS").ToString();
                            eq.LGORT = linea.GetValue("LGORT").ToString();
                            eq.AUFNR = linea.GetValue("AUFNR").ToString();
                            eq.SUPEQUI = linea.GetValue("SUPEQUI").ToString();
                            eq.CHARG = linea.GetValue("CHARG").ToString();
                            eq.MONTADO = linea.GetValue("MONTADO").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaEquipo_O(connection, eq);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta4)
                    {
                        PM01 pm = new PM01();
                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.WERKS = linea.GetValue("WERKS").ToString();
                            DALC_Ordenes.obtenerInstancia().VaciarPM01(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta4)
                    {
                        PM01 pm = new PM01();

                        try
                        {
                            pm.RSNUM = linea.GetValue("RSNUM").ToString();
                            pm.RSPOS = linea.GetValue("RSPOS").ToString();
                            pm.RSART = linea.GetValue("RSART").ToString();
                            pm.BDART = linea.GetValue("BDART").ToString();
                            pm.RSSTA = linea.GetValue("RSSTA").ToString();
                            pm.XLOEK = linea.GetValue("XLOEK").ToString();
                            pm.XWAOK = linea.GetValue("XWAOK").ToString();
                            pm.KZEAR = linea.GetValue("KZEAR").ToString();
                            pm.XFEHL = linea.GetValue("XFEHL").ToString();
                            pm.MATNR = linea.GetValue("MATNR").ToString();
                            pm.WERKS = linea.GetValue("WERKS").ToString();
                            pm.LGORT = linea.GetValue("LGORT").ToString();
                            pm.PRVBE = linea.GetValue("PRVBE").ToString();
                            pm.CHARG = linea.GetValue("CHARG").ToString();
                            pm.PLPLA = linea.GetValue("PLPLA").ToString();
                            pm.SOBKZ = linea.GetValue("SOBKZ").ToString();
                            pm.BDTER = linea.GetValue("BDTER").ToString();
                            pm.BDMNG = linea.GetValue("BDMNG").ToString();
                            pm.MEINS = linea.GetValue("MEINS").ToString();
                            pm.SHKZG = linea.GetValue("SHKZG").ToString();
                            pm.FMENG = linea.GetValue("FMENG").ToString();
                            pm.ENMNG = linea.GetValue("ENMNG").ToString();
                            pm.ENWRT = linea.GetValue("ENWRT").ToString();
                            pm.WAERS = linea.GetValue("WAERS").ToString();
                            pm.ERFMG = linea.GetValue("ERFMG").ToString();
                            pm.ERFME = linea.GetValue("ERFME").ToString();
                            pm.PLNUM = linea.GetValue("PLNUM").ToString();
                            pm.BANFN = linea.GetValue("BANFN").ToString();
                            pm.BNFPO = linea.GetValue("BNFPO").ToString();
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.BAUGR = linea.GetValue("BAUGR").ToString();
                            pm.SERNR = linea.GetValue("SERNR").ToString();
                            pm.KDAUF = linea.GetValue("KDAUF").ToString();
                            pm.KDPOS = linea.GetValue("KDPOS").ToString();
                            pm.KDEIN = linea.GetValue("KDEIN").ToString();
                            pm.PROJN = linea.GetValue("PROJN").ToString();
                            pm.BWART = linea.GetValue("BWART").ToString();
                            pm.SAKNR = linea.GetValue("SAKNR").ToString();
                            pm.GSBER = linea.GetValue("GSBER").ToString();
                            pm.UMWRK = linea.GetValue("UMWRK").ToString();
                            pm.UMLGO = linea.GetValue("UMLGO").ToString();
                            pm.NAFKZ = linea.GetValue("NAFKZ").ToString();
                            pm.NOMAT = linea.GetValue("NOMAT").ToString();
                            pm.NOMNG = linea.GetValue("NOMNG").ToString();
                            pm.POSTP = linea.GetValue("POSTP").ToString();
                            pm.POSNR = linea.GetValue("POSNR").ToString();
                            pm.ROMS1 = linea.GetValue("ROMS1").ToString();
                            pm.ROMS2 = linea.GetValue("ROMS2").ToString();
                            pm.ROMS3 = linea.GetValue("ROMS3").ToString();
                            pm.ROMEI = linea.GetValue("ROMEI").ToString();
                            pm.ROMEN = linea.GetValue("ROMEN").ToString();
                            pm.SGTXT = linea.GetValue("SGTXT").ToString();
                            pm.LMENG = linea.GetValue("LMENG").ToString();
                            pm.ROHPS = linea.GetValue("ROHPS").ToString();
                            pm.RFORM = linea.GetValue("RFORM").ToString();
                            pm.ROANZ = linea.GetValue("ROANZ").ToString();
                            pm.FLMNG = linea.GetValue("FLMNG").ToString();
                            pm.STLTY = linea.GetValue("STLTY").ToString();
                            pm.STLNR = linea.GetValue("STLNR").ToString();
                            pm.STLKN = linea.GetValue("STLKN").ToString();
                            pm.STPOZ = linea.GetValue("STPOZ").ToString();
                            pm.LTXSP = linea.GetValue("LTXSP").ToString();
                            pm.POTX1 = linea.GetValue("POTX1").ToString();
                            pm.POTX2 = linea.GetValue("POTX2").ToString();
                            pm.SANKA = linea.GetValue("SANKA").ToString();
                            pm.ALPOS = linea.GetValue("ALPOS").ToString();
                            pm.EWAHR = linea.GetValue("EWAHR").ToString();
                            pm.AUSCH = linea.GetValue("AUSCH").ToString();
                            pm.AVOAU = linea.GetValue("AVOAU").ToString();
                            pm.NETAU = linea.GetValue("NETAU").ToString();
                            pm.NLFZT = linea.GetValue("NLFZT").ToString();
                            pm.AENNR = linea.GetValue("AENNR").ToString();
                            pm.UMREZ = linea.GetValue("UMREZ").ToString();
                            pm.UMREN = linea.GetValue("UMREN").ToString();
                            pm.SORTF = linea.GetValue("SORTF").ToString();
                            pm.SBTER = linea.GetValue("SBTER").ToString();
                            pm.VERTI = linea.GetValue("VERTI").ToString();
                            pm.SCHGT = linea.GetValue("SCHGT").ToString();
                            pm.UPSKZ = linea.GetValue("UPSKZ").ToString();
                            pm.DBSKZ = linea.GetValue("DBSKZ").ToString();
                            pm.TXTPS = linea.GetValue("TXTPS").ToString();
                            pm.DUMPS = linea.GetValue("DUMPS").ToString();
                            pm.BEIKZ = linea.GetValue("BEIKZ").ToString();
                            pm.ERSKZ = linea.GetValue("ERSKZ").ToString();
                            pm.AUFST = linea.GetValue("AUFST").ToString();
                            pm.AUFWG = linea.GetValue("AUFWG").ToString();
                            pm.BAUST = linea.GetValue("BAUST").ToString();
                            pm.BAUWG = linea.GetValue("BAUWG").ToString();
                            pm.AUFPS = linea.GetValue("AUFPS").ToString();
                            pm.EBELN = linea.GetValue("EBELN").ToString();
                            pm.EBELP = linea.GetValue("EBELP").ToString();
                            pm.EBELE = linea.GetValue("EBELE").ToString();
                            pm.KNTTP = linea.GetValue("KNTTP").ToString();
                            pm.KZVBR = linea.GetValue("KZVBR").ToString();
                            pm.PSPEL = linea.GetValue("PSPEL").ToString();
                            pm.AUFPL = linea.GetValue("AUFPL").ToString();
                            pm.PLNFL = linea.GetValue("PLNFL").ToString();
                            pm.VORNR = linea.GetValue("VORNR").ToString();
                            pm.APLZL = linea.GetValue("APLZL").ToString();
                            pm.OBJNR = linea.GetValue("OBJNR").ToString();
                            pm.FLGAT = linea.GetValue("FLGAT").ToString();
                            pm.GPREIS = linea.GetValue("GPREIS").ToString();
                            pm.FPREIS = linea.GetValue("FPREIS").ToString();
                            pm.PEINH = linea.GetValue("PEINH").ToString();
                            pm.RGEKZ = linea.GetValue("RGEKZ").ToString();
                            pm.EKGRP = linea.GetValue("EKGRP").ToString();
                            pm.ROKME = linea.GetValue("ROKME").ToString();
                            pm.ZUMEI = linea.GetValue("ZUMEI").ToString();
                            pm.ZUMS1 = linea.GetValue("ZUMS1").ToString();
                            pm.ZUMS2 = linea.GetValue("ZUMS2").ToString();
                            pm.ZUMS3 = linea.GetValue("ZUMS3").ToString();
                            pm.ZUDIV = linea.GetValue("ZUDIV").ToString();
                            pm.VMENG = linea.GetValue("VMENG").ToString();
                            pm.PRREG = linea.GetValue("PRREG").ToString();
                            pm.LIFZT = linea.GetValue("LIFZT").ToString();
                            pm.CUOBJ = linea.GetValue("CUOBJ").ToString();
                            pm.KFPOS = linea.GetValue("KFPOS").ToString();
                            pm.REVLV = linea.GetValue("REVLV").ToString();
                            pm.BERKZ = linea.GetValue("BERKZ").ToString();
                            pm.LGNUM = linea.GetValue("LGNUM").ToString();
                            pm.LGTYP = linea.GetValue("LGTYP").ToString();
                            pm.LGPLA = linea.GetValue("LGPLA").ToString();
                            pm.TBMNG = linea.GetValue("TBMNG").ToString();
                            pm.NPTXTKY = linea.GetValue("NPTXTKY").ToString();
                            pm.KBNKZ = linea.GetValue("KBNKZ").ToString();
                            pm.KZKUP = linea.GetValue("KZKUP").ToString();
                            pm.AFPOS = linea.GetValue("AFPOS").ToString();
                            pm.NO_DISP = linea.GetValue("NO_DISP").ToString();
                            pm.BDZTP = linea.GetValue("BDZTP").ToString();
                            pm.ESMNG = linea.GetValue("ESMNG").ToString();
                            pm.ALPGR = linea.GetValue("ALPGR").ToString();
                            pm.ALPRF = linea.GetValue("ALPRF").ToString();
                            pm.ALPST = linea.GetValue("ALPST").ToString();
                            pm.KZAUS = linea.GetValue("KZAUS").ToString();
                            pm.NFEAG = linea.GetValue("NFEAG").ToString();
                            pm.NFPKZ = linea.GetValue("NFPKZ").ToString();
                            pm.NFGRP = linea.GetValue("NFGRP").ToString();
                            pm.NFUML = linea.GetValue("NFUML").ToString();
                            pm.ADRNR = linea.GetValue("ADRNR").ToString();
                            pm.CHOBJ = linea.GetValue("CHOBJ").ToString();
                            pm.SPLKZ = linea.GetValue("SPLKZ").ToString();
                            pm.SPLRV = linea.GetValue("SPLRV").ToString();
                            pm.KNUMH = linea.GetValue("KNUMH").ToString();
                            pm.WEMPF = linea.GetValue("WEMPF").ToString();
                            pm.ABLAD = linea.GetValue("ABLAD").ToString();
                            pm.HKMAT = linea.GetValue("HKMAT").ToString();
                            pm.HRKFT = linea.GetValue("HRKFT").ToString();
                            pm.VORAB = linea.GetValue("VORAB").ToString();
                            pm.MATKL = linea.GetValue("MATKL").ToString();
                            pm.FRUNV = linea.GetValue("FRUNV").ToString();
                            pm.CLAKZ = linea.GetValue("CLAKZ").ToString();
                            pm.INPOS = linea.GetValue("INPOS").ToString();
                            pm.WEBAZ = linea.GetValue("WEBAZ").ToString();
                            pm.LIFNR = linea.GetValue("LIFNR").ToString();
                            pm.FLGEX = linea.GetValue("FLGEX").ToString();
                            pm.FUNCT = linea.GetValue("FUNCT").ToString();
                            pm.GPREIS_2 = linea.GetValue("GPREIS_2").ToString();
                            pm.FPREIS_2 = linea.GetValue("FPREIS_2").ToString();
                            pm.PEINH_2 = linea.GetValue("PEINH_2").ToString();
                            pm.INFNR = linea.GetValue("INFNR").ToString();
                            pm.KZECH = linea.GetValue("KZECH").ToString();
                            pm.KZMPF = linea.GetValue("KZMPF").ToString();
                            pm.STLAL = linea.GetValue("STLAL").ToString();
                            pm.PBDNR = linea.GetValue("PBDNR").ToString();
                            pm.STVKN = linea.GetValue("STVKN").ToString();
                            pm.KTOMA = linea.GetValue("KTOMA").ToString();
                            pm.VRPLA = linea.GetValue("VRPLA").ToString();
                            pm.KZBWS = linea.GetValue("KZBWS").ToString();
                            pm.NLFZV = linea.GetValue("NLFZV").ToString();
                            pm.NLFMV = linea.GetValue("NLFMV").ToString();
                            pm.TECHS = linea.GetValue("TECHS").ToString();
                            pm.OBJTYPE = linea.GetValue("OBJTYPE").ToString();
                            pm.CH_PROC = linea.GetValue("CH_PROC").ToString();
                            pm.FXPRU = linea.GetValue("FXPRU").ToString();
                            pm.UMSOK = linea.GetValue("UMSOK").ToString();
                            pm.VORAB_SM = linea.GetValue("VORAB_SM").ToString();
                            pm.FIPOS = linea.GetValue("FIPOS").ToString();
                            pm.FIPEX = linea.GetValue("FIPEX").ToString();
                            pm.FISTL = linea.GetValue("FISTL").ToString();
                            pm.GEBER = linea.GetValue("GEBER").ToString();
                            pm.GRANT_NBR = linea.GetValue("GRANT_NBR").ToString();
                            pm.FKBER = linea.GetValue("FKBER").ToString();
                            pm.PRIO_URG = linea.GetValue("PRIO_URG").ToString();
                            pm.PRIO_REQ = linea.GetValue("PRIO_REQ").ToString();
                            pm.KBLNR = linea.GetValue("KBLNR").ToString();
                            pm.KBLPOS = linea.GetValue("KBLPOS").ToString();
                            pm.BUDGET_PD = linea.GetValue("BUDGET_PD").ToString();
                            pm.SC_OBJECT_ID = linea.GetValue("SC_OBJECT_ID").ToString();
                            pm.SC_ITM_NO = linea.GetValue("SC_ITM_NO").ToString();
                            pm.SGT_SCAT = linea.GetValue("SGT_SCAT").ToString();
                            pm.SGT_RCAT = linea.GetValue("SGT_RCAT").ToString();
                            pm.FMFGUS_KEY = linea.GetValue("FMFGUS_KEY").ToString();
                            pm.ADVCODE = linea.GetValue("ADVCODE").ToString();
                            pm.STRUC_CODE = linea.GetValue("STRUC_CODE").ToString();
                            pm.STRUC_CLASS = linea.GetValue("STRUC_CLASS").ToString();
                            pm.STRUC_CLASSTYP = linea.GetValue("STRUC_CLASSTYP").ToString();
                            pm.FSH_RALLOC_QTY = linea.GetValue("FSH_RALLOC_QTY").ToString();
                            pm.FSH_ASSIGN = linea.GetValue("FSH_ASSIGN").ToString();
                            pm.MILL_UCDET = linea.GetValue("MILL_UCDET").ToString();
                            pm.WTY_IND = linea.GetValue("WTY_IND").ToString();
                            pm.R_PART_INDICATOR = linea.GetValue("R_PART_INDICATOR").ToString();
                            pm.WTYSC_CLMITEM = linea.GetValue("WTYSC_CLMITEM").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaPM01(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta5)
                    {
                        PM03_1 pm = new PM03_1();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            //falta centro
                            DALC_Ordenes.obtenerInstancia().VaciarPM03_1(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta5)
                    {
                        PM03_1 pm = new PM03_1();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.EXTROW = linea.GetValue("EXTROW").ToString();
                            pm.SRVPOS = linea.GetValue("SRVPOS").ToString();
                            pm.KTEXT1 = linea.GetValue("KTEXT1").ToString();
                            pm.MENGE = linea.GetValue("MENGE").ToString();
                            pm.MEINS = linea.GetValue("MEINS").ToString();
                            pm.NETWR = linea.GetValue("NETWR").ToString();
                            pm.PEINH = linea.GetValue("PEINH").ToString();
                            pm.MATKL = linea.GetValue("MATKL").ToString();
                            pm.KSTAR = linea.GetValue("KSTAR").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaPM03_1(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta6)
                    {
                        PM03_2 pm = new PM03_2();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.WERKS = linea.GetValue("WERKS").ToString();
                            DALC_Ordenes.obtenerInstancia().VaciarPM03_2(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta6)
                    {
                        PM03_2 pm = new PM03_2();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.BANFN = linea.GetValue("BANFN").ToString();
                            pm.BNFPO = linea.GetValue("BNFPO").ToString();
                            pm.EBELN = linea.GetValue("EBELN").ToString();
                            pm.EBELP = linea.GetValue("EBELP").ToString();
                            pm.LOEKZ = linea.GetValue("LOEKZ").ToString();
                            pm.TXZ01 = linea.GetValue("TXZ01").ToString();
                            pm.WERKS = linea.GetValue("WERKS").ToString();
                            pm.LGORT = linea.GetValue("LGORT").ToString();
                            pm.KONNR = linea.GetValue("KONNR").ToString();
                            pm.IDNLF = linea.GetValue("IDNLF").ToString();
                            pm.MENGE = linea.GetValue("MENGE").ToString();
                            pm.MEINS = linea.GetValue("MEINS").ToString();
                            pm.NETWR = linea.GetValue("NETWR").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaPM03_2(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta7)
                    {
                        PM03_3 pm = new PM03_3();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            //falta centro
                            DALC_Ordenes.obtenerInstancia().VaciarPM03_3(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta7)
                    {
                        PM03_3 pm = new PM03_3();

                        try
                        {
                            pm.AUFNR = linea.GetValue("AUFNR").ToString();
                            pm.EBELN = linea.GetValue("EBELN").ToString();
                            pm.EBELP = linea.GetValue("EBELP").ToString();
                            pm.GJAHR = linea.GetValue("GJAHR").ToString();
                            pm.BELNR = linea.GetValue("BELNR").ToString();
                            pm.BUZEI = linea.GetValue("BUZEI").ToString();
                            pm.BEWTP = linea.GetValue("BEWTP").ToString();
                            pm.BWART = linea.GetValue("BWART").ToString();
                            pm.BUDAT = linea.GetValue("BUDAT").ToString();
                            pm.MENGE = linea.GetValue("MENGE").ToString();
                            pm.WRBTR = linea.GetValue("WRBTR").ToString();
                            pm.WAERS = linea.GetValue("WAERS").ToString();
                            pm.ERNAM = linea.GetValue("ERNAM").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaPM03_3(connection, pm);
                        }
                        catch (Exception) { return false; }
                    }
                    /*foreach (IRfcStructure linea in ta8)
                    {
                        QM01_1 qm = new QM01_1();

                        try
                        {
                            qm.AUFNR = linea.GetValue("AUFNR").ToString();
                            DALC_Ordenes.obtenerInstancia().VaciarQM01_1(connection, qm);
                        }
                        catch (Exception) { return false; }
                    }
                    foreach (IRfcStructure linea in ta8)
                    {
                        QM01_1 qm = new QM01_1();

                        try
                        {
                            qm.AUFNR = linea.GetValue("AUFNR").ToString();
                            qm.MERKNR = linea.GetValue("MERKNR").ToString();
                            qm.KURZTEXT = linea.GetValue("KURZTEXT").ToString();
                            qm.MASSEINHSW = linea.GetValue("MASSEINHSW").ToString();
                            qm.ISTSTPUMF = linea.GetValue("ISTSTPUMF").ToString();
                            qm.ISTSTPANZ = linea.GetValue("ISTSTPANZ").ToString();

                            DALC_Ordenes.obtenerInstancia().IngresaQM01_1(connection, qm);
                        }
                        catch (Exception) { return false; }
                    }*/
                    //foreach (IRfcStructure linea in ta9)
                    //{
                    //    QM01_2 qm = new QM01_2();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        DALC_Ordenes.obtenerInstancia().VaciarQM01_2(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta9)
                    //{
                    //    QM01_2 qm = new QM01_2();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        qm.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        qm.MERKNR = linea.GetValue("MERKNR").ToString();
                    //        qm.KURZTEXT = linea.GetValue("KURZTEXT").ToString();
                    //        qm.KTX01 = linea.GetValue("KTX01").ToString();
                    //        qm.KATAB1 = linea.GetValue("KATAB1").ToString();
                    //        qm.ANZFEHLEH = linea.GetValue("ANZFEHLEH").ToString();
                    //        qm.ORIGINAL_INPUT = linea.GetValue("ORIGINAL_INPUT").ToString();
                    //        qm.MBEWERTG = linea.GetValue("MBEWERTG").ToString();
                    //        qm.CODE1 = linea.GetValue("CODE1").ToString();
                    //        qm.MASSEINHSW = linea.GetValue("MASSEINHSW").ToString();
                    //        qm.PRUEFBEMKT = linea.GetValue("PRUEFBEMKT").ToString();
                    //        qm.KATALGART1 = linea.GetValue("KATALGART1").ToString();
                    //        qm.CHAR_TYPE = linea.GetValue("CHAR_TYPE").ToString();
                    //        qm.AUSWMENGE1 = linea.GetValue("AUSWMENGE1").ToString();
                    //        qm.SOLLSTPUMF = linea.GetValue("SOLLSTPUMF").ToString();
                    //        qm.TEORICO = linea.GetValue("TEORICO").ToString();
                    //        qm.TINFERIOR = linea.GetValue("TINFERIOR").ToString();
                    //        qm.TSUPERIOR = linea.GetValue("TSUPERIOR").ToString();
                    //        qm.SOLLWNI = linea.GetValue("SOLLWNI").ToString();
                    //        qm.TOLOBNI = linea.GetValue("TOLOBNI").ToString();
                    //        qm.TOLUNNI = linea.GetValue("TOLUNNI").ToString();
                    //        DALC_Ordenes.obtenerInstancia().IngresaQM01_2(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta10)
                    //{
                    //    QM01_3 qm = new QM01_3();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        //falta centro
                    //        DALC_Ordenes.obtenerInstancia().VaciarQM01_3(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta10)
                    //{
                    //    QM01_3 qm = new QM01_3();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        qm.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        qm.MERKNR = linea.GetValue("MERKNR").ToString();
                    //        qm.DETAILERG = linea.GetValue("DETAILERG").ToString();
                    //        qm.KURZTEXT = linea.GetValue("KURZTEXT").ToString();
                    //        qm.KATAB1 = linea.GetValue("KATAB1").ToString();
                    //        qm.ISTSTPUMF = linea.GetValue("ISTSTPUMF").ToString();
                    //        qm.ORIGINAL_INPUT = linea.GetValue("ORIGINAL_INPUT").ToString();
                    //        qm.CODE1 = linea.GetValue("CODE1").ToString();
                    //        qm.ICBEWERT = linea.GetValue("ICBEWERT").ToString();
                    //        qm.KTX01 = linea.GetValue("KTX01").ToString();
                    //        qm.PRUEFBEMKT = linea.GetValue("PRUEFBEMKT").ToString();
                    //        qm.ERSTELLER = linea.GetValue("ERSTELLER").ToString();
                    //        qm.SOLLSTPUMF = linea.GetValue("SOLLSTPUMF").ToString();
                    //        qm.MBEWERTG = linea.GetValue("MBEWERTG").ToString();
                    //        qm.MASSEINHSW = linea.GetValue("MASSEINHSW").ToString();
                    //        qm.CHAR_TYPE = linea.GetValue("CHAR_TYPE").ToString();
                    //        qm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    //        qm.TEORICO = linea.GetValue("TEORICO").ToString();
                    //        qm.TINFERIOR = linea.GetValue("TINFERIOR").ToString();
                    //        qm.TSUPERIOR = linea.GetValue("TSUPERIOR").ToString();
                    //        qm.SOLLWNI = linea.GetValue("SOLLWNI").ToString();
                    //        qm.TOLOBNI = linea.GetValue("TOLOBNI").ToString();
                    //        qm.TOLUNNI = linea.GetValue("TOLUNNI").ToString();
                    //        DALC_Ordenes.obtenerInstancia().IngresaQM01_3(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta11)
                    //{
                    //    CAB_QM01 qm = new CAB_QM01();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        qm.WERK = linea.GetValue("WERK").ToString();
                    //        DALC_Ordenes.obtenerInstancia().VaciarCAB_QM01(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta11)
                    //{
                    //    CAB_QM01 qm = new CAB_QM01();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        qm.KTEXTLOS = linea.GetValue("KTEXTLOS").ToString();
                    //        qm.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        qm.WERK = linea.GetValue("WERK").ToString();
                    //        qm.ERSTELLER = linea.GetValue("ERSTELLER").ToString();
                    //        qm.ENSTEHDAT = linea.GetValue("ENSTEHDAT").ToString();
                    //        qm.ENTSTEZEIT = linea.GetValue("ENTSTEZEIT").ToString();
                    //        qm.AENDERER = linea.GetValue("AENDERER").ToString();
                    //        qm.AENDERDAT = linea.GetValue("AENDERDAT").ToString();
                    //        qm.AENDERZEIT = linea.GetValue("AENDERZEIT").ToString();


                    //        DALC_Ordenes.obtenerInstancia().IngresaCAB_QM01(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta12)
                    //{
                    //    ENSAMBLE qm = new ENSAMBLE();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        //falta centro
                    //        DALC_Ordenes.obtenerInstancia().VaciarENSAMBLE(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta12)
                    //{
                    //    ENSAMBLE qm = new ENSAMBLE();

                    //    try
                    //    {
                    //        qm.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        qm.NUM_EQUI = linea.GetValue("NUM_EQUI").ToString();
                    //        qm.FECHA_MONT = linea.GetValue("FECHA_MONT").ToString();
                    //        qm.ULTIM_MEDI = linea.GetValue("ULTIM_MEDI").ToString();
                    //        qm.STATUS_MONT = linea.GetValue("STATUS_MONT").ToString();
                    //        qm.VAL_OVER = linea.GetValue("VAL_OVER").ToString();
                    //        qm.FECHA_DESM = linea.GetValue("FECHA_DESM").ToString();

                    //        DALC_Ordenes.obtenerInstancia().IngresaENSAMBLE(connection, qm);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta13)
                    //{
                    //    CaracteristicasLote cl = new CaracteristicasLote();
                    //    try
                    //    {
                    //        cl.MATNR = linea.GetValue("MATNR").ToString();
                    //        cl.WERKS = linea.GetValue("WERKS").ToString();
                    //        cl.CHARG = linea.GetValue("CHARG").ToString();
                    //        DALC_Ordenes.obtenerInstancia().VaciarCaracteristicasLote(connection, cl);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta13)
                    //{
                    //    CaracteristicasLote lc = new CaracteristicasLote();
                    //    try
                    //    {
                    //        lc.MATNR = linea.GetValue("MATNR").ToString();
                    //        lc.WERKS = linea.GetValue("WERKS").ToString();
                    //        lc.CHARG = linea.GetValue("CHARG").ToString();
                    //        lc.CLABS = linea.GetValue("CLABS").ToString();
                    //        lc.NUM_EQUI = linea.GetValue("NUM_EQUI").ToString();
                    //        lc.FECHA_MONT = linea.GetValue("FECHA_MONT").ToString();
                    //        lc.ULTIM_MEDI = linea.GetValue("ULTIM_MEDI").ToString();
                    //        lc.STATUS_MONT = linea.GetValue("STATUS_MONT").ToString();
                    //        lc.VAL_OVER = linea.GetValue("VAL_OVER").ToString();
                    //        lc.FECHA_DESM = linea.GetValue("FECHA_DESM").ToString();
                    //        DALC_Ordenes.obtenerInstancia().IngresarCaracteristicasLote(connection, lc);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta14)
                    //{
                    //    DecisionEmpleoCaracteristicas dc = new DecisionEmpleoCaracteristicas();
                    //    try
                    //    {
                    //        dc.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        DALC_Ordenes.obtenerInstancia().EliminarDecisionEmpleoCarac(connection, dc);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta14)
                    //{
                    //    DecisionEmpleoCaracteristicas dc = new DecisionEmpleoCaracteristicas();
                    //    try
                    //    {
                    //        dc.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        dc.EQUNR = linea.GetValue("EQUNR").ToString();
                    //        dc.VCODE = linea.GetValue("VCODE").ToString();
                    //        dc.VCODEGRP = linea.GetValue("VCODEGRP").ToString();
                    //        dc.VBEWERTUNG = linea.GetValue("VBEWERTUNG").ToString();
                    //        dc.KURZTEXT = linea.GetValue("KURZTEXT").ToString();
                    //        DALC_Ordenes.obtenerInstancia().IngresarDecisionEmpleoCarac(connection, dc);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta15)
                    //{
                    //    DecisionEmpleoLoteInps dl = new DecisionEmpleoLoteInps();
                    //    try
                    //    {
                    //        dl.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        DALC_Ordenes.obtenerInstancia().EliminarDecisionEmpleoLoteIns(connection, dl);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta15)
                    //{
                    //    DecisionEmpleoLoteInps dl = new DecisionEmpleoLoteInps();
                    //    try
                    //    {
                    //        dl.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        dl.VCODE = linea.GetValue("VCODE").ToString();
                    //        dl.VCODEGRP = linea.GetValue("VCODEGRP").ToString();
                    //        dl.VBEWERTUNG = linea.GetValue("VBEWERTUNG").ToString();
                    //        dl.KURZTEXT = linea.GetValue("KURZTEXT").ToString();
                    //        DALC_Ordenes.obtenerInstancia().InsertarDecisionEmpleoLoteIns(connection, dl);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta16)
                    //{
                    //    TextosCaracteristicasOrdenes tc = new TextosCaracteristicasOrdenes();
                    //    try
                    //    {
                    //        tc.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        DALC_Ordenes.obtenerInstancia().VaciarTextosCaracteristicas(connection, tc);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                    //foreach (IRfcStructure linea in ta16)
                    //{
                    //    TextosCaracteristicasOrdenes tc = new TextosCaracteristicasOrdenes();
                    //    try
                    //    {
                    //        tc.AUFNR = linea.GetValue("AUFNR").ToString();
                    //        tc.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    //        tc.MERKNR = linea.GetValue("MERKNR").ToString();
                    //        tc.TDLINE = linea.GetValue("TDLINE").ToString();
                    //        DALC_Ordenes.obtenerInstancia().IngresarTextosCaracteristicas(connection, tc);
                    //    }
                    //    catch (Exception) { return false; }
                    //}
                }
                DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZORDENES_PMSAM", "Correcto");
                DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZORDENES_PMSAM", "X", centro);
                return true;
            }
            return true;
        }
        public bool P_ZPM_MATERIALES_ALMACEN(string centro)
        {
            //RfcRepository mca = rfcDesti.Repository;
            //IRfcFunction FUNCTION = mca.CreateFunction("ZPM_MATERIALES_ALMACEN");
            //FUNCTION.SetValue("CENTRO", centro);
            //IRfcTable mtc = FUNCTION.GetTable("ZMM_MAT");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in mtc)
            //{
            //    Material_Almacen_Centro adm = new Material_Almacen_Centro();
            //    try
            //    {
            //        adm.MATNR = linea.GetValue("MATNR").ToString();
            //        //falta centro
            //        DALC_MaterialAC.ObtenerInstancia().borraMaterialAC(connection, adm);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in mtc)
            //{
            //    Material_Almacen_Centro mcal = new Material_Almacen_Centro();
            //    try
            //    {
            //        mcal.MATNR = linea.GetValue("MATNR").ToString();
            //        mcal.ACTUALIZADO = linea.GetValue("ACTUALIZADO").ToString();
            //        mcal.LVORM = linea.GetValue("LVORM").ToString();

            //        DALC_MaterialAC.ObtenerInstancia().ingresaMaterialAC(connection, mcal);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPM_MATERIALES_ALMACEN", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZPM_MATERIALES_ALMACEN", "X", centro);
            return true;
        }//no existe en sap
        public bool P_ZDAT_SOLPED_SAM(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZDAT_SOLPED_SAM");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", FAntes);
            FUNCTION.SetValue("FECHA_HIGH", FDespues);
            IRfcTable cabSol = FUNCTION.GetTable("SOLPED_CAB");
            IRfcTable posSol = FUNCTION.GetTable("SOLPED_ITEMS");
            IRfcTable serSol = FUNCTION.GetTable("SOLPED_SERVICIOS");
            IRfcTable txtCab = FUNCTION.GetTable("TEXTO_CAB");
            IRfcTable txtPos = FUNCTION.GetTable("TEXTO_POS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure ln in cabSol)
            {
                try
                {
                    string folio_sap = ln.GetValue("FOLIO_SAP").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().BorrarSolPed(connection, folio_sap);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure ln in cabSol)
            //{
            //    SolPed_Vis_Cab j = new SolPed_Vis_Cab();
            //    try
            //    {
            //        j.FOLIO_SAP = ln.GetValue("FOLIO_SAP").ToString();
            //        j.EWERK = ln.GetValue("EWERK").ToString();
            //        DALC_Sol_Ped.ObtenerInstancia().BorrarCabeceraSolPed(connection, j);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure ln in cabSol)
            {
                SolPed_Vis_Cab j = new SolPed_Vis_Cab();
                try
                {
                    j.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    j.FOLIO_SAP = ln.GetValue("FOLIO_SAP").ToString();
                    j.BBSRT = ln.GetValue("BBSRT").ToString();
                    j.TEXTO_CAB = ln.GetValue("TEXTO_CAB").ToString();
                    j.AFNAM = ln.GetValue("AFNAM").ToString();
                    j.EWERK = ln.GetValue("EWERK").ToString();
                    j.BADAT = ln.GetValue("BADAT").ToString();
                    j.LFDAT = ln.GetValue("LFDAT").ToString();
                    j.FRGDT = ln.GetValue("FRGDT").ToString();
                    j.BSART = ln.GetValue("BSART").ToString();
                    j.WERKS = ln.GetValue("WERKS").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaCabeceraSolPed(connection, j);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure ln in posSol)
            //{
            //    SolPed_Pos p = new SolPed_Pos();
            //    try
            //    {
            //        p.FOLIO_SAP = ln.GetValue("FOLIO_SAP").ToString();
            //        p.PLANT = ln.GetValue("PLANT").ToString();
            //        DALC_Sol_Ped.ObtenerInstancia().BorrarPosicionesSolPed(connection, p);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure ln in posSol)
            {
                SolPed_Pos p = new SolPed_Pos();
                try
                {
                    p.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    p.PREQ_ITEM = ln.GetValue("PREQ_ITEM").ToString();
                    p.ITEM_CAT = ln.GetValue("ITEM_CAT").ToString();
                    p.MATERIAL = ln.GetValue("MATERIAL").ToString();
                    p.DATUM = ln.GetValue("DATUM").ToString();
                    p.UZEIT = ln.GetValue("UZEIT").ToString();
                    p.FOLIO_SAP = ln.GetValue("FOLIO_SAP").ToString();
                    p.DOC_TYPE = ln.GetValue("DOC_TYPE").ToString();
                    p.ACCTASSCAT = ln.GetValue("ACCTASSCAT").ToString();
                    p.SHORT_TEXT = ln.GetValue("SHORT_TEXT").ToString();
                    p.QUANTITY = ln.GetValue("QUANTITY").ToString();
                    p.UNIT = ln.GetValue("UNIT").ToString();
                    p.DEL_DATCAT = ln.GetValue("DEL_DATCAT").ToString();
                    p.DELIV_DATE = ln.GetValue("DELIV_DATE").ToString();
                    p.MAT_GRP = ln.GetValue("MAT_GRP").ToString();
                    p.PLANT = ln.GetValue("PLANT").ToString();
                    p.STORE_LOC = ln.GetValue("STORE_LOC").ToString();
                    p.PUR_GROUP = ln.GetValue("PUR_GROUP").ToString();
                    p.PREQ_NAME = ln.GetValue("PREQ_NAME").ToString();
                    p.PREQ_DATE = ln.GetValue("PREQ_DATE").ToString();
                    p.DES_VENDOR = ln.GetValue("DES_VENDOR").ToString();
                    p.LIFNR = ln.GetValue("LIFNR").ToString();
                    p.TABIX = ln.GetValue("TABIX").ToString();
                    p.EPSTP = ln.GetValue("EPSTP").ToString();
                    p.INFNR = ln.GetValue("INFNR").ToString();
                    p.EKORG = ln.GetValue("EKORG").ToString();
                    p.G_L_ACCT = ln.GetValue("G_L_ACCT").ToString();
                    p.COST_CTR = ln.GetValue("COST_CTR").ToString();
                    p.KONNR = ln.GetValue("KONNR").ToString();
                    p.KTPNR = ln.GetValue("KTPNR").ToString();
                    p.VRTKZ = ln.GetValue("VRTKZ").ToString();
                    p.SAKTO = ln.GetValue("SAKTO").ToString();
                    p.AUFNR = ln.GetValue("AUFNR").ToString();
                    p.KOKRS = ln.GetValue("KOKRS").ToString();
                    p.KOSTL = ln.GetValue("KOSTL").ToString();
                    p.TEXTO_CAB = ln.GetValue("TEXTO_CAB").ToString();
                    p.TEXTO_POS = ln.GetValue("TEXTO_POS").ToString();
                    p.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                    p.PROCESADO = ln.GetValue("PROCESADO").ToString();
                    p.ERROR = ln.GetValue("ERROR").ToString();
                    p.PREIS = ln.GetValue("PREIS").ToString();
                    p.WAERS = ln.GetValue("WAERS").ToString();
                    p.FRGGR = ln.GetValue("FRGGR").ToString();
                    p.FRGRL = ln.GetValue("FRGRL").ToString();
                    p.FRGKZ = ln.GetValue("FRGKZ").ToString();
                    p.FRGZU = ln.GetValue("FRGZU").ToString();
                    p.FRGST = ln.GetValue("FRGST").ToString();
                    p.FRGC1 = ln.GetValue("FRGC1").ToString();
                    p.FRGCT = ln.GetValue("FRGCT").ToString();
                    p.LOEKZ = ln.GetValue("LOEKZ").ToString();
                    p.PEDIDO = ln.GetValue("PEDIDO").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaPosicionesSolPed(connection, p);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure ln in serSol)
            //{
            //    SolPed_Ser_Vis s = new SolPed_Ser_Vis();
            //    try
            //    {
            //        s.BANFN = ln.GetValue("BANFN").ToString();
            //        //falta centro
            //        DALC_Sol_Ped.ObtenerInstancia().BorrarServiciosSolPed(connection, s);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure ln in serSol)
            {
                SolPed_Ser_Vis s = new SolPed_Ser_Vis();
                try
                {
                    s.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    s.BANFN = ln.GetValue("BANFN").ToString();
                    s.BNFPO = ln.GetValue("BNFPO").ToString();
                    s.EXTROW = ln.GetValue("EXTROW").ToString();
                    s.SRVPOS = ln.GetValue("SRVPOS").ToString();
                    s.MENGE = ln.GetValue("MENGE").ToString();
                    s.MEINS = ln.GetValue("MEINS").ToString();
                    s.KTEXT1 = ln.GetValue("KTEXT1").ToString();
                    s.TBTWR = ln.GetValue("TBTWR").ToString();
                    s.WAERS = ln.GetValue("WAERS").ToString();
                    s.KOSTL = ln.GetValue("KOSTL").ToString();
                    s.NETWR = ln.GetValue("NETWR").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaServiciosSolPed(connection, s);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in txtCab)
            //{
            //    TextoCabeceraSolped tc = new TextoCabeceraSolped();
            //    try
            //    {
            //        tc.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
            //        DALC_Sol_Ped.ObtenerInstancia().VaciarTextoCSV(connection, tc);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in txtCab)
            {
                TextoCabeceraSolped tc = new TextoCabeceraSolped();
                try
                {
                    tc.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    tc.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    tc.INDICE = linea.GetValue("INDICE").ToString();
                    tc.TDFORMAT = linea.GetValue("TDFORMAT").ToString();
                    tc.TDLINE = linea.GetValue("TDLINE").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().IngresarTextoCSV(connection, tc);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in txtPos)
            //{
            //    TextoPosicionesSolped tp = new TextoPosicionesSolped();
            //    try
            //    {
            //        tp.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
            //        DALC_Sol_Ped.ObtenerInstancia().VaciarTextoPSV(connection, tp);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in txtPos)
            {
                TextoPosicionesSolped tp = new TextoPosicionesSolped();
                try
                {
                    tp.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    tp.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    tp.PREQ_ITEM = linea.GetValue("PREQ_ITEM").ToString();
                    tp.INDICE = linea.GetValue("INDICE").ToString();
                    tp.TDFORMAT = linea.GetValue("TDFORMAT").ToString();
                    tp.TDLINE = linea.GetValue("TDLINE").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().IngresarTextoPSV(connection, tp);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZDAT_SOLPED_SAM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZDAT_SOLPED_SAM", "X", centro);
            return true;
        }
        public bool P_ZHIS_PEDIDOS(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZHIS_PEDIDOS");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable ta1 = FUNCTION.GetTable("PROV_PEDIDOS");
            IRfcTable ta2 = FUNCTION.GetTable("PROV_PEDIDOS_II");
            IRfcTable ta3 = FUNCTION.GetTable("SERVICIOS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                Pedidos_I pd = new Pedidos_I();
                try
                {
                    pd.EBELN = linea.GetValue("EBELN").ToString();
                    pd.EBELP = linea.GetValue("EBELP").ToString();
                    pd.MATNR = linea.GetValue("MATNR").ToString();
                    pd.TXZ01 = linea.GetValue("TXZ01").ToString();
                    pd.MENGE = linea.GetValue("MENGE").ToString();
                    pd.MEINS = linea.GetValue("MEINS").ToString();
                    pd.BEWTP = linea.GetValue("BEWTP").ToString();
                    pd.BWART = linea.GetValue("BWART").ToString();
                    pd.MBLNR = linea.GetValue("MBLNR").ToString();
                    pd.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    pd.BUZEI = linea.GetValue("BUZEI").ToString();
                    pd.CANTIADE = linea.GetValue("CANTIADE").ToString();
                    pd.UM_PED = linea.GetValue("UM_PED").ToString();
                    pd.EINDT = linea.GetValue("EINDT").ToString();
                    pd.BUDAT = linea.GetValue("BUDAT").ToString();

                    DALC_Pedidos.ObtenerInstancia().IngresaPedido_I(connection, pd);
                    DALC_Pedidos.ObtenerInstancia().BorrarPedido_I(connection, pd);

                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in ta1)
            //{
            //    Pedidos_I pedd = new Pedidos_I();
            //    try
            //    {
            //        pedd.EBELN = linea.GetValue("EBELN").ToString();
            //        DALC_Pedidos.ObtenerInstancia().BorrarPedido_I(connection, pedd);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta1)
            //{
            //    Pedidos_I pd = new Pedidos_I();
            //    try
            //    {
            //        pd.EBELN = linea.GetValue("EBELN").ToString();
            //        pd.EBELP = linea.GetValue("EBELP").ToString();
            //        pd.MATNR = linea.GetValue("MATNR").ToString();
            //        pd.TXZ01 = linea.GetValue("TXZ01").ToString();
            //        pd.MENGE = linea.GetValue("MENGE").ToString();
            //        pd.MEINS = linea.GetValue("MEINS").ToString();
            //        pd.BEWTP = linea.GetValue("BEWTP").ToString();
            //        pd.BWART = linea.GetValue("BWART").ToString();
            //        pd.MBLNR = linea.GetValue("MBLNR").ToString();
            //        pd.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        pd.BUZEI = linea.GetValue("BUZEI").ToString();
            //        pd.CANTIADE = linea.GetValue("CANTIADE").ToString();
            //        pd.UM_PED = linea.GetValue("UM_PED").ToString();
            //        pd.EINDT = linea.GetValue("EINDT").ToString();
            //        pd.BUDAT = linea.GetValue("BUDAT").ToString();

            //        DALC_Pedidos.ObtenerInstancia().IngresaPedido_I(connection, pd);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in ta2)
            {
                Pedidos_II pd = new Pedidos_II();
                try
                {
                    pd.BSTYP = linea.GetValue("BSTYP").ToString();
                    pd.EBELN = linea.GetValue("EBELN").ToString();
                    pd.LIFNR = linea.GetValue("LIFNR").ToString();
                    pd.NAME1 = linea.GetValue("NAME1").ToString();
                    pd.BEDAT = linea.GetValue("BEDAT").ToString();
                    pd.G_NAME = linea.GetValue("G_NAME").ToString();
                    pd.EKORG = linea.GetValue("EKORG").ToString();
                    pd.EKOTX = linea.GetValue("EKOTX").ToString();
                    pd.EKGRP = linea.GetValue("EKGRP").ToString();
                    pd.EKNAM = linea.GetValue("EKNAM").ToString();
                    pd.BUKRS = linea.GetValue("BUKRS").ToString();
                    pd.BUTXT = linea.GetValue("BUTXT").ToString();
                    pd.EBELP = linea.GetValue("EBELP").ToString();
                    pd.KNTTP = linea.GetValue("KNTTP").ToString();
                    pd.PSTYP = linea.GetValue("PSTYP").ToString();
                    pd.MATNR = linea.GetValue("MATNR").ToString();
                    pd.TXZ01 = linea.GetValue("TXZ01").ToString();
                    pd.MENGE = linea.GetValue("MENGE").ToString();
                    pd.MEINS = linea.GetValue("MEINS").ToString();
                    pd.EINDT = linea.GetValue("EINDT").ToString();
                    pd.WERKS = linea.GetValue("WERKS").ToString();
                    pd.LGORT = linea.GetValue("LGORT").ToString();
                    pd.CHARG = linea.GetValue("CHARG").ToString();
                    pd.AFNAM = linea.GetValue("AFNAM").ToString();
                    pd.INFNR = linea.GetValue("INFNR").ToString();
                    pd.BANFN = linea.GetValue("BANFN").ToString();
                    pd.BNFPO = linea.GetValue("BNFPO").ToString();
                    pd.KONNR = linea.GetValue("KONNR").ToString();
                    pd.MATKL = linea.GetValue("MATKL").ToString();
                    pd.IDNLF = linea.GetValue("IDNLF").ToString();
                    pd.EAN11 = linea.GetValue("EAN11").ToString();
                    pd.LICHA = linea.GetValue("LICHA").ToString();
                    pd.BSART = linea.GetValue("BSART").ToString();
                    pd.BATXT = linea.GetValue("BATXT").ToString();
                    pd.G_POS = linea.GetValue("G_POS").ToString();
                    pd.PACKNO = linea.GetValue("PACKNO").ToString();
                    pd.SUB_PACKNO = linea.GetValue("SUB_PACKNO").ToString();
                    pd.BWART = linea.GetValue("BWART").ToString();
                    pd.CANTIDADE = linea.GetValue("CANTIDADE").ToString();
                    pd.KOSTL = linea.GetValue("KOSTL").ToString();
                    pd.AUFNR = linea.GetValue("AUFNR").ToString();
                    pd.SAKTO = linea.GetValue("SAKTO").ToString();
                    pd.FRGGR = linea.GetValue("FRGGR").ToString();
                    pd.FRGSX = linea.GetValue("FRGSX").ToString();
                    pd.FRGKE = linea.GetValue("FRGKE").ToString();
                    pd.FRGZU = linea.GetValue("FRGZU").ToString();
                    pd.BEDNR = linea.GetValue("BEDNR").ToString();
                    pd.FRGC1 = linea.GetValue("FRGC1").ToString();
                    pd.FRGCT = linea.GetValue("FRGCT").ToString();
                    pd.LVORM = linea.GetValue("LOEKZ").ToString();
                    DALC_Pedidos.ObtenerInstancia().BorrarPedido_II(connection, pd);
                    DALC_Pedidos.ObtenerInstancia().IngresaPedido_II(connection, pd);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in ta2)
            //{
            //    Pedidos_II pd = new Pedidos_II();
            //    try
            //    {
            //        pd.BSTYP = linea.GetValue("BSTYP").ToString();
            //        pd.EBELN = linea.GetValue("EBELN").ToString();
            //        pd.LIFNR = linea.GetValue("LIFNR").ToString();
            //        pd.NAME1 = linea.GetValue("NAME1").ToString();
            //        pd.BEDAT = linea.GetValue("BEDAT").ToString();
            //        pd.G_NAME = linea.GetValue("G_NAME").ToString();
            //        pd.EKORG = linea.GetValue("EKORG").ToString();
            //        pd.EKOTX = linea.GetValue("EKOTX").ToString();
            //        pd.EKGRP = linea.GetValue("EKGRP").ToString();
            //        pd.EKNAM = linea.GetValue("EKNAM").ToString();
            //        pd.BUKRS = linea.GetValue("BUKRS").ToString();
            //        pd.BUTXT = linea.GetValue("BUTXT").ToString();
            //        pd.EBELP = linea.GetValue("EBELP").ToString();
            //        pd.KNTTP = linea.GetValue("KNTTP").ToString();
            //        pd.PSTYP = linea.GetValue("PSTYP").ToString();
            //        pd.MATNR = linea.GetValue("MATNR").ToString();
            //        pd.TXZ01 = linea.GetValue("TXZ01").ToString();
            //        pd.MENGE = linea.GetValue("MENGE").ToString();
            //        pd.MEINS = linea.GetValue("MEINS").ToString();
            //        pd.EINDT = linea.GetValue("EINDT").ToString();
            //        pd.WERKS = linea.GetValue("WERKS").ToString();
            //        pd.LGORT = linea.GetValue("LGORT").ToString();
            //        pd.CHARG = linea.GetValue("CHARG").ToString();
            //        pd.AFNAM = linea.GetValue("AFNAM").ToString();
            //        pd.INFNR = linea.GetValue("INFNR").ToString();
            //        pd.BANFN = linea.GetValue("BANFN").ToString();
            //        pd.BNFPO = linea.GetValue("BNFPO").ToString();
            //        pd.KONNR = linea.GetValue("KONNR").ToString();
            //        pd.MATKL = linea.GetValue("MATKL").ToString();
            //        pd.IDNLF = linea.GetValue("IDNLF").ToString();
            //        pd.EAN11 = linea.GetValue("EAN11").ToString();
            //        pd.LICHA = linea.GetValue("LICHA").ToString();
            //        pd.BSART = linea.GetValue("BSART").ToString();
            //        pd.BATXT = linea.GetValue("BATXT").ToString();
            //        pd.G_POS = linea.GetValue("G_POS").ToString();
            //        pd.PACKNO = linea.GetValue("PACKNO").ToString();
            //        pd.SUB_PACKNO = linea.GetValue("SUB_PACKNO").ToString();
            //        pd.BWART = linea.GetValue("BWART").ToString();
            //        pd.CANTIDADE = linea.GetValue("CANTIDADE").ToString();
            //        pd.KOSTL = linea.GetValue("KOSTL").ToString();
            //        pd.AUFNR = linea.GetValue("AUFNR").ToString();
            //        pd.SAKTO = linea.GetValue("SAKTO").ToString();
            //        pd.FRGGR = linea.GetValue("FRGGR").ToString();
            //        pd.FRGSX = linea.GetValue("FRGSX").ToString();
            //        pd.FRGKE = linea.GetValue("FRGKE").ToString();
            //        pd.FRGZU = linea.GetValue("FRGZU").ToString();
            //        pd.BEDNR = linea.GetValue("BEDNR").ToString();
            //        pd.FRGC1 = linea.GetValue("FRGC1").ToString();
            //        pd.FRGCT = linea.GetValue("FRGCT").ToString();
            //        pd.LVORM = linea.GetValue("LOEKZ").ToString();

            //        DALC_Pedidos.ObtenerInstancia().IngresaPedido_II(connection, pd);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in ta3)
            {
                Servicio pd = new Servicio();
                try
                {
                    pd.EBELN = linea.GetValue("EBELN").ToString();
                    pd.EBELP = linea.GetValue("EBELP").ToString();
                    pd.EXTROW = linea.GetValue("EXTROW").ToString();
                    pd.SRVPOS = linea.GetValue("SRVPOS").ToString();
                    pd.MENGE = linea.GetValue("MENGE").ToString();
                    pd.MEINS = linea.GetValue("MEINS").ToString();
                    pd.KTEXT1 = linea.GetValue("KTEXT1").ToString();
                    pd.TBTWR = linea.GetValue("TBTWR").ToString();
                    pd.WAERS = linea.GetValue("WAERS").ToString();
                    pd.NETWR = linea.GetValue("NETWR").ToString();
                    pd.ACT_MENGE = linea.GetValue("ACT_MENGE").ToString();
                    pd.AUFNR = linea.GetValue("AUFNR").ToString();
                    pd.WERKS = linea.GetValue("WERKS").ToString();
                    pd.DATUM = linea.GetValue("DATUM").ToString();
                    DALC_Pedidos.ObtenerInstancia().BorrarServicio(connection, pd);
                    DALC_Pedidos.ObtenerInstancia().IngresaServicio(connection, pd);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure linea in ta3)
            //{
            //    Servicio pd = new Servicio();
            //    try
            //    {
            //        pd.EBELN = linea.GetValue("EBELN").ToString();
            //        pd.EBELP = linea.GetValue("EBELP").ToString();
            //        pd.EXTROW = linea.GetValue("EXTROW").ToString();
            //        pd.SRVPOS = linea.GetValue("SRVPOS").ToString();
            //        pd.MENGE = linea.GetValue("MENGE").ToString();
            //        pd.MEINS = linea.GetValue("MEINS").ToString();
            //        pd.KTEXT1 = linea.GetValue("KTEXT1").ToString();
            //        pd.TBTWR = linea.GetValue("TBTWR").ToString();
            //        pd.WAERS = linea.GetValue("WAERS").ToString();
            //        pd.NETWR = linea.GetValue("NETWR").ToString();
            //        pd.ACT_MENGE = linea.GetValue("ACT_MENGE").ToString();
            //        pd.AUFNR = linea.GetValue("AUFNR").ToString();
            //        pd.WERKS = linea.GetValue("WERKS").ToString();
            //        pd.DATUM = linea.GetValue("DATUM").ToString();

            //        DALC_Pedidos.ObtenerInstancia().IngresaServicio(connection, pd);
            //    }
            //    catch (Exception) { return false; }
            //}
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZHIS_PEDIDOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZHIS_PEDIDOS", "X", centro);
            return true;
        }
        public bool P_ZMATERIALES(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZMATERIALES");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable mat = FUNCTION.GetTable("MATERIALES");
            IRfcTable ser = FUNCTION.GetTable("SERVICIOS");
            IRfcTable mac = FUNCTION.GetTable("MATCONV");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Materiales.ObtenerInstancia().BorrarMateriales(connection, centro);
            //foreach (IRfcStructure linea in mat)
            //{
            //    Materiales matt = new Materiales();
            //    try
            //    {
            //        matt.MATNR = linea.GetValue("MATNR").ToString();
            //        matt.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_Materiales.ObtenerInstancia().BorrarMateriales(connection, matt);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in mat)
            {
                Materiales mt = new Materiales();

                try
                {
                    mt.MATNR = linea.GetValue("MATNR").ToString();
                    mt.WERKS = linea.GetValue("WERKS").ToString();
                    mt.MEINS = linea.GetValue("MEINS").ToString();
                    mt.BISMT = linea.GetValue("BISMT").ToString();
                    mt.MATKL = linea.GetValue("MATKL").ToString();
                    mt.MTART = linea.GetValue("MTART").ToString();
                    mt.TRAGR = linea.GetValue("TRAGR").ToString();
                    mt.EKGRP = linea.GetValue("EKGRP").ToString();
                    mt.XCHPF = linea.GetValue("XCHPF").ToString();
                    mt.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                    mt.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                    mt.DISMM = linea.GetValue("DISMM").ToString();
                    mt.MINBE = linea.GetValue("MINBE").ToString();
                    mt.LFRHY = linea.GetValue("LFRHY").ToString();
                    mt.FXHOR = linea.GetValue("FXHOR").ToString();
                    mt.DISPO = linea.GetValue("DISPO").ToString();
                    mt.DISLS = linea.GetValue("DISLS").ToString();
                    mt.BSTMI = linea.GetValue("BSTMI").ToString();
                    mt.BSTMA = linea.GetValue("BSTMA").ToString();
                    mt.MABST = linea.GetValue("MABST").ToString();
                    mt.BESKZ = linea.GetValue("BESKZ").ToString();
                    mt.SOBSL = linea.GetValue("SOBSL").ToString();
                    mt.WEBAZ = linea.GetValue("WEBAZ").ToString();
                    mt.PLIFZ = linea.GetValue("PLIFZ").ToString();
                    mt.EISBE = linea.GetValue("EISBE").ToString();
                    mt.EISLO = linea.GetValue("EISLO").ToString();
                    mt.QMATV = linea.GetValue("QMATV").ToString();
                    mt.QMPUR = linea.GetValue("QMPUR").ToString();
                    mt.SPART = linea.GetValue("SPART").ToString();
                    mt.MTPOS_MARA = linea.GetValue("MTPOS_MARA").ToString();
                    mt.MTVFP = linea.GetValue("MTVFP").ToString();
                    mt.PRCTR = linea.GetValue("PRCTR").ToString();
                    mt.LADGR = linea.GetValue("LADGR").ToString();
                    mt.VRKME = linea.GetValue("VRKME").ToString();
                    mt.KONDM = linea.GetValue("KONDM").ToString();
                    mt.VERSG = linea.GetValue("VERSG").ToString();
                    mt.KTGRM = linea.GetValue("KTGRM").ToString();
                    mt.MTPOS = linea.GetValue("MTPOS").ToString();
                    mt.PRODH = linea.GetValue("PRODH").ToString();
                    mt.VKORG = linea.GetValue("VKORG").ToString();
                    mt.VKGRP = linea.GetValue("VKGRP").ToString();
                    mt.VTWEG = linea.GetValue("VTWEG").ToString();
                    mt.VPRSV = linea.GetValue("VPRSV").ToString();
                    mt.VERPR = linea.GetValue("VERPR").ToString();
                    mt.STPRS = linea.GetValue("STPRS").ToString();
                    mt.PEINH = linea.GetValue("PEINH").ToString();
                    mt.BWTAR = linea.GetValue("BWTAR").ToString();
                    mt.BKLAS = linea.GetValue("BKLAS").ToString();
                    mt.MFRPN = linea.GetValue("MFRPN").ToString();
                    mt.WRKST = linea.GetValue("WRKST").ToString();
                    DALC_Materiales.ObtenerInstancia().IngresaMaterial(connection, mt);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ser)
            {
                Servicios serv = new Servicios();
                try
                {
                    serv.ASNUM = linea.GetValue("ASNUM").ToString();
                    DALC_Materiales.ObtenerInstancia().BorrarServicios(connection, serv);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ser)
            {
                Servicios se = new Servicios();

                try
                {
                    se.ASNUM = linea.GetValue("ASNUM").ToString();
                    se.ASKTX_ES = linea.GetValue("ASKTX_ES").ToString();
                    se.ASKTX_EN = linea.GetValue("ASKTX_EN").ToString();
                    se.MEINS = linea.GetValue("MEINS").ToString();
                    se.BKLAS = linea.GetValue("BKLAS").ToString();
                    se.BKBEZ_ES = linea.GetValue("BKBEZ_ES").ToString();
                    se.BKBEZ_EN = linea.GetValue("BKBEZ_EN").ToString();
                    se.MATKL = linea.GetValue("MATKL").ToString();
                    se.SPART = linea.GetValue("SPART").ToString();
                    se.ASTYP = linea.GetValue("ASTYP").ToString();
                    se.ASTYP_TXT_ES = linea.GetValue("ASTYP_TXT_ES").ToString();
                    se.ASTYP_TXT_EN = linea.GetValue("ASTYP_TXT_EN").ToString();

                    DALC_Materiales.ObtenerInstancia().IngresaServicio(connection, se);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in mac)
            {
                MaterialesConversion matc = new MaterialesConversion();
                try
                {
                    matc.MATNR = linea.GetValue("MATNR").ToString();
                    DALC_Materiales.ObtenerInstancia().EliminarConversiones(connection, matc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in mac)
            {
                MaterialesConversion matc = new MaterialesConversion();
                try
                {
                    matc.MATNR = linea.GetValue("MATNR").ToString();
                    matc.UMREZ = linea.GetValue("UMREZ").ToString();
                    matc.MEINS = linea.GetValue("MEINS").ToString();
                    matc.UMREN = linea.GetValue("UMREN").ToString();
                    matc.BSTME = linea.GetValue("BSTME").ToString();
                    matc.LVORM = linea.GetValue("LVORM").ToString();
                    matc.VRKME = linea.GetValue("VRKME").ToString();
                    DALC_Materiales.ObtenerInstancia().IngresarMaterialesConversion(connection, matc);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMATERIALES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMATERIALES", "X", centro);
            return true;
        }
        public bool P_ZCATALOGOS_PM(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZCATALOGOS_PM");
            IRfcTable med = FUNCTION.GetTable("MEDICION");
            IRfcTable man = FUNCTION.GetTable("MANTENIMIENTO");
            IRfcTable est = FUNCTION.GetTable("ESTATUS_HR");
            IRfcTable uti = FUNCTION.GetTable("UTILIZACION");
            IRfcTable tcat = FUNCTION.GetTable("TCAT_QPGR");

            try
            {
                FUNCTION.Invoke(rfcDesti);
                DALC_Catalogos.obtenerInstancia().VaciarUtilizacionL(connection);
                DALC_Catalogos.obtenerInstancia().VaciarUtilizacion(connection);
                DALC_Catalogos.obtenerInstancia().VaciarMedicion(connection);
                DALC_Catalogos.obtenerInstancia().VaciarMantenimiento(connection);
                DALC_Catalogos.obtenerInstancia().VaciarGrupoCodigos(connection);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in uti)
            {
                UtilizacionL ul = new UtilizacionL();

                try
                {
                    ul.STLAN = linea.GetValue("STLAN").ToString();
                    ul.PMPFE = linea.GetValue("PMPFE").ToString();
                    ul.PMPKO = linea.GetValue("PMPKO").ToString();
                    ul.PMPKA = linea.GetValue("PMPKA").ToString();
                    ul.PMPRV = linea.GetValue("PMPRV").ToString();
                    ul.PMPVS = linea.GetValue("PMPVS").ToString();
                    ul.PMPIN = linea.GetValue("PMPIN").ToString();
                    ul.PMPER = linea.GetValue("PMPER").ToString();
                    ul.ANTXT_ES = linea.GetValue("ANTXT_ES").ToString();
                    ul.ANTXT_EN = linea.GetValue("ANTXT_EN").ToString();

                    DALC_Catalogos.obtenerInstancia().IngresaUtilizacionL(connection, ul);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in est)
            {
                UtilizacionHR uh = new UtilizacionHR();

                try
                {
                    uh.PLNST = linea.GetValue("PLNST").ToString();
                    uh.TXTH_ES = linea.GetValue("TXTH_ES").ToString();
                    uh.TXTH_EN = linea.GetValue("TXTH_EN").ToString();

                    DALC_Catalogos.obtenerInstancia().IngresaUtilizacion(connection, uh);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in med)
            {
                Medicion me = new Medicion();
                try
                {
                    me.MDOCM = linea.GetValue("MDOCM").ToString();
                    me.POINT = linea.GetValue("POINT").ToString();
                    me.IDATE = linea.GetValue("IDATE").ToString();
                    me.ITIME = linea.GetValue("ITIME").ToString();
                    me.INVTS = linea.GetValue("INVTS").ToString();
                    me.CNTRG = linea.GetValue("CNTRG").ToString();
                    me.MDTXT_ES = linea.GetValue("MDTXT_ES").ToString();
                    me.MDTXT_EN = linea.GetValue("MDTXT_EN").ToString();
                    me.KZLTX = linea.GetValue("KZLTX").ToString();
                    me.READR = linea.GetValue("READR").ToString();
                    me.ERDAT = linea.GetValue("ERDAT").ToString();
                    me.ERUHR = linea.GetValue("ERUHR").ToString();
                    me.ERNAM = linea.GetValue("ERNAM").ToString();
                    me.AEDAT = linea.GetValue("AEDAT").ToString();
                    me.AENAM = linea.GetValue("AENAM").ToString();
                    me.LVORM = linea.GetValue("LVORM").ToString();
                    me.GENER = linea.GetValue("GENER").ToString();
                    me.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    me.VORGLFNR = linea.GetValue("VORGLFNR").ToString();
                    me.MERKNR = linea.GetValue("MERKNR").ToString();
                    me.DETAILERG = linea.GetValue("DETAILERG").ToString();
                    me.ROOTD = linea.GetValue("ROOTD").ToString();
                    me.TOLTY = linea.GetValue("TOLTY").ToString();
                    me.TOLID = linea.GetValue("TOLID").ToString();
                    me.WOOBJ = linea.GetValue("WOOBJ").ToString();
                    me.DOCAF = linea.GetValue("DOCAF").ToString();
                    me.READG = linea.GetValue("READG").ToString();
                    me.READGI = linea.GetValue("READGI").ToString();
                    me.RECDV = linea.GetValue("RECDV").ToString();
                    me.RECDVI = linea.GetValue("RECDVI").ToString();
                    me.RECDU = linea.GetValue("RECDU").ToString();
                    me.CNTRR = linea.GetValue("CNTRR").ToString();
                    me.CNTRRI = linea.GetValue("CNTRRI").ToString();
                    me.CDIFF = linea.GetValue("CDIFF").ToString();
                    me.CDIFFI = linea.GetValue("CDIFFI").ToString();
                    me.IDIFF = linea.GetValue("IDIFF").ToString();
                    me.EXCHG = linea.GetValue("EXCHG").ToString();
                    me.TOTEX = linea.GetValue("TOTEX").ToString();
                    me.CODCT = linea.GetValue("CODCT").ToString();
                    me.CODGR = linea.GetValue("CODGR").ToString();
                    me.VLCOD = linea.GetValue("VLCOD").ToString();
                    me.CVERS = linea.GetValue("CVERS").ToString();
                    me.PREST = linea.GetValue("PREST").ToString();
                    me.CANCL = linea.GetValue("CANCL").ToString();
                    me.WOOB1 = linea.GetValue("WOOB1").ToString();
                    me.PROBENR = linea.GetValue("PROBENR").ToString();
                    me.MBEWERTG = linea.GetValue("MBEWERTG").ToString();
                    me.INTVL = linea.GetValue("INTVL").ToString();
                    me.IDAT1 = linea.GetValue("IDAT1").ToString();
                    me.ITIM1 = linea.GetValue("ITIM1").ToString();
                    me.TMSTP_BW = linea.GetValue("TMSTP_BW").ToString();

                    DALC_Catalogos.obtenerInstancia().IngresaMedicion(connection, me);
                }
                catch (Exception) { return false; }
            }

            foreach (IRfcStructure linea in man)
            {
                Mantenimiento mm = new Mantenimiento();

                try
                {
                    mm.WARPL = linea.GetValue("WARPL").ToString();
                    mm.NUMMER = linea.GetValue("NUMMER").ToString();
                    mm.OPERATOR = linea.GetValue("OPERATOR").ToString();
                    mm.ZYKL1 = linea.GetValue("ZYKL1").ToString();
                    mm.ZEIEH = linea.GetValue("ZEIEH").ToString();
                    mm.PAKTEXT_ES = linea.GetValue("PAKTEXT_ES").ToString();
                    mm.PAKTEXT_EN = linea.GetValue("PAKTEXT_EN").ToString();
                    mm.LANGU = linea.GetValue("LANGU").ToString();
                    mm.POINT = linea.GetValue("POINT").ToString();
                    mm.OFFSET = linea.GetValue("OFFSET").ToString();
                    mm.INAKTIV = linea.GetValue("INAKTIV").ToString();
                    mm.NZAEH = linea.GetValue("NZAEH").ToString();
                    mm.ZAEHL = linea.GetValue("ZAEHL").ToString();
                    mm.KTEXTYZK_ES = linea.GetValue("KTEXTYZK_ES").ToString();
                    mm.KTEXTYZK_EN = linea.GetValue("KTEXTYZK_EN").ToString();
                    mm.CYCLESEQIND = linea.GetValue("CYCLESEQIND").ToString();
                    mm.SETREPEATIND = linea.GetValue("SETREPEATIND").ToString();

                    DALC_Catalogos.obtenerInstancia().IngresaMantenimiento(connection, mm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tcat)
            {
                GrupoCodigos gps = new GrupoCodigos();

                try
                {
                    gps.KATALOGART = linea.GetValue("KATALOGART").ToString();
                    gps.CODEGRUPPE = linea.GetValue("CODEGRUPPE").ToString();
                    gps.KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
                    gps.KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();
                    gps.CODE = linea.GetValue("CODE").ToString();
                    gps.KURZTEXT_E = linea.GetValue("KURZTEXT_E").ToString();
                    gps.KURZTEXT_N = linea.GetValue("KURZTEXT_N").ToString();


                    DALC_Catalogos.obtenerInstancia().IngresaGrupoCodigos(connection, gps);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGOS_PM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGOS_PM", "X", centro);
            return true;
        }
        public bool P_ZCATALOGOS_QM(string centro)
        {
            //RfcRepository cqm = rfcDesti.Repository;
            //IRfcFunction FUNCTION = cqm.CreateFunction("ZCATALOGOS_QM");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable ci = FUNCTION.GetTable("CLASE_INFORME");
            //IRfcTable tex = FUNCTION.GetTable("TEXTOS_CODIGOS");
            //IRfcTable cld = FUNCTION.GetTable("CLASES_DEFECTO");
            //IRfcTable ins = FUNCTION.GetTable("INSPECCION_CODIGOS");
            //IRfcTable code = FUNCTION.GetTable("CODIGOS_DEFECTOS");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //    DALC_Catalagos_QM.ObtenerInstancia().VaciarClaseInforme(connection);
            //    DALC_Catalagos_QM.ObtenerInstancia().VaciarTextosCodigos(connection);
            //    DALC_Catalagos_QM.ObtenerInstancia().VaciarClasesDefecto(connection);
            //    DALC_Catalagos_QM.ObtenerInstancia().VaciarInspeccionCodigo(connection);
            //    DALC_Catalagos_QM.ObtenerInstancia().VaciarCodigoDefecto(connection);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in ci)
            //{
            //    ClaseInforme cat = new ClaseInforme();
            //    try
            //    {
            //        cat.FEART = linea.GetValue("FEART").ToString();
            //        cat.KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
            //        cat.KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();
            //        cat.RBNR = linea.GetValue("RBNR").ToString();
            //        cat.RBNRX_ES = linea.GetValue("RBNRX_ES").ToString();
            //        cat.RBNRX_EN = linea.GetValue("RBNRX_EN").ToString();
            //        cat.BWART = linea.GetValue("BWART").ToString();
            //        cat.BTEXT_ES = linea.GetValue("BTEXT_ES").ToString();
            //        cat.BTEXT_EN = linea.GetValue("BTEXT_EN").ToString();

            //        DALC_Catalagos_QM.ObtenerInstancia().IngresaClaseInforme(connection, cat);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in tex)
            //{
            //    Catalogos_PM ct = new Catalogos_PM();
            //    try
            //    {
            //        ct.T_CODEGRUPPE = linea.GetValue("CODEGRUPPE").ToString();
            //        ct.T_CODE = linea.GetValue("CODE").ToString();
            //        ct.T_KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
            //        ct.T_KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString(); ;

            //        DALC_Catalagos_QM.ObtenerInstancia().IngresaTextosCodigos(connection, ct);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in cld)
            //{
            //    Catalogos_PM cd = new Catalogos_PM();
            //    try
            //    {
            //        cd.C_FEHLKLASSE = linea.GetValue("FEHLKLASSE").ToString();
            //        cd.C_KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
            //        cd.C_KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();

            //        DALC_Catalagos_QM.ObtenerInstancia().IngresaClasesDefecto(connection, cd);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ins)
            //{
            //    Catalogos_PM tc = new Catalogos_PM();
            //    try
            //    {
            //        tc.AUSWAHLMGE = linea.GetValue("AUSWAHLMGE").ToString();
            //        tc.IN_CODEGRUPPE = linea.GetValue("CODEGRUPPE").ToString();
            //        tc.IN_CODE = linea.GetValue("CODE").ToString();
            //        tc.IN_KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
            //        tc.IN_KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();
            //        tc.BEWERTUNG = linea.GetValue("BEWERTUNG").ToString();
            //        DALC_Catalagos_QM.ObtenerInstancia().IngresaInspeccionCodigo(connection, tc);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in code)
            //{
            //    CodigosDefecto cod = new CodigosDefecto();
            //    try
            //    {
            //        cod.KATALOGART = linea.GetValue("KATALOGART").ToString();
            //        cod.CODEGRUPPE = linea.GetValue("CODEGRUPPE").ToString();
            //        cod.CODE = linea.GetValue("CODE").ToString();
            //        cod.KURZTEXT = linea.GetValue("KURZTEXT").ToString();

            //        DALC_Catalagos_QM.ObtenerInstancia().IngresaCodigoDefecto(connection, cod);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGOS_QM", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGOS_QM", "X", centro);
            return true;
        }
        public bool P_ZCATALOGOS_MM(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZCATALOGOS_MM");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable clm = FUNCTION.GetTable("CMONEDA");
            IRfcTable alm = FUNCTION.GetTable("ALMACENES");
            IRfcTable org = FUNCTION.GetTable("ORGANIZACION");
            IRfcTable gru = FUNCTION.GetTable("GRUPO");
            IRfcTable soc = FUNCTION.GetTable("SOCIEDADES");
            IRfcTable cen = FUNCTION.GetTable("CENTROBEN");
            IRfcTable cec = FUNCTION.GetTable("CENTROCOSTO");
            IRfcTable orc = FUNCTION.GetTable("ORGANIZACION_VENTAS");
            IRfcTable can = FUNCTION.GetTable("CANAL_DIS");
            IRfcTable sec = FUNCTION.GetTable("SECTOR");
            IRfcTable cld = FUNCTION.GetTable("CLADOC");
            IRfcTable est = FUNCTION.GetTable("ESTTRA");
            IRfcTable dis = FUNCTION.GetTable("DISTRI");
            IRfcTable tip = FUNCTION.GetTable("TIPPOS");
            IRfcTable tii = FUNCTION.GetTable("TIPIMP");
            IRfcTable tim = FUNCTION.GetTable("TIPO_MATERIAL");
            IRfcTable gra = FUNCTION.GetTable("GRUPO_ARTICULOS");
            IRfcTable ift = FUNCTION.GetTable("INFOTIPO");
            IRfcTable cum = FUNCTION.GetTable("CUENTAMAYOR");
            try
            {
                FUNCTION.Invoke(rfcDesti);
                DALC_Catalogo.ObtenerInstancia().VaciarClaveMoneda(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarInfoTipo(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarGrupoArticulos(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarTipoMaterial(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarTipoImp(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarTipoPos(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarDistribucion(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarTratamiento(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarClaseDoc(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarSector(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarCanalDistribucion(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarOrgVentas(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarCentroCosto(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarCentroBeneficio(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarSociedades(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarGrupoCompras(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarOrganizacionCompras(connection);
                DALC_Catalogo.ObtenerInstancia().VaciarAlmacenes(connection, centro);
                DALC_Catalogo.ObtenerInstancia().VaciarCuentaMayor(connection);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in clm)
            {
                ClaveMoneda cm = new ClaveMoneda();
                try
                {
                    cm.WAERS = linea.GetValue("WAERS").ToString();
                    cm.LTEXT_ES = linea.GetValue("LTEXT_ES").ToString();
                    cm.LTEXT_EN = linea.GetValue("LTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaClaveMoneda(connection, cm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ift)
            {
                InfoTipo it = new InfoTipo();
                try
                {
                    it.VALUED = linea.GetValue("VALUED").ToString();
                    it.DDTEXT_ES = linea.GetValue("DDTEXT_ES").ToString();
                    it.DDTEXT_EN = linea.GetValue("DDTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaTipoInfo(connection, it);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in gra)
            {
                GrupoArticulos ga = new GrupoArticulos();
                try
                {
                    ga.MATKL = linea.GetValue("MATKL").ToString();
                    ga.WGBEZ_ES = linea.GetValue("WGBEZ_ES").ToString();
                    ga.WGBEZ_EN = linea.GetValue("WGBEZ_EN").ToString();
                    ga.WGBEZ60_ES = linea.GetValue("WGBEZ60_ES").ToString();
                    ga.WGBEZ60_EN = linea.GetValue("WGBEZ60_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaGrupoArticulos(connection, ga);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tim)
            {
                TipoMaterial tm = new TipoMaterial();
                try
                {
                    tm.MTART = linea.GetValue("MTART").ToString();
                    tm.MTBEZ_ES = linea.GetValue("MTBEZ_ES").ToString();
                    tm.MTBEZ_EN = linea.GetValue("MTBEZ_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaTipoMaterial(connection, tm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tii)
            {
                TipoImp ti = new TipoImp();
                try
                {
                    ti.KNTTP = linea.GetValue("KNTTP").ToString();
                    ti.KNTTX_ES = linea.GetValue("KNTTX_ES").ToString();
                    ti.KNTTX_EN = linea.GetValue("KNTTX_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaTipoImp(connection, ti);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tip)
            {
                TipoPos tp = new TipoPos();
                try
                {
                    tp.PSTYP = linea.GetValue("PSTYP").ToString();
                    tp.EPSTP_ES = linea.GetValue("EPSTP_ES").ToString();
                    tp.PTEXT_ES = linea.GetValue("PTEXT_ES").ToString();
                    tp.EPSTP_EN = linea.GetValue("EPSTP_EN").ToString();
                    tp.PTEXT_EN = linea.GetValue("PTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaTipoPos(connection, tp);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in dis)
            {
                Distribucion ds = new Distribucion();
                try
                {
                    ds.VALUED = linea.GetValue("VALUED").ToString();
                    ds.DDTEXT_ES = linea.GetValue("DDTEXT_ES").ToString();
                    ds.DDTEXT_EN = linea.GetValue("DDTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaDistribucion(connection, ds);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in est)
            {
                EstatusTratamiento et = new EstatusTratamiento();
                try
                {
                    et.VALUED = linea.GetValue("VALUED").ToString();
                    et.DDTEXT_ES = linea.GetValue("DDTEXT_ES").ToString();
                    et.DDTEXT_EN = linea.GetValue("DDTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaTratamiento(connection, et);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in cld)
            {
                ClaseDoc cl = new ClaseDoc();
                try
                {
                    cl.BSART = linea.GetValue("BSART").ToString();
                    cl.BATXT_ES = linea.GetValue("BATXT_ES").ToString();
                    cl.BATXT_EN = linea.GetValue("BATXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaClaseDoc(connection, cl);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in sec)
            {
                Sector se = new Sector();

                try
                {
                    se.SPART = linea.GetValue("SPART").ToString();
                    se.VTEXT_ES = linea.GetValue("VTEXT_ES").ToString();
                    se.VTEXT_EN = linea.GetValue("VTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaSector(connection, se);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in can)
            {
                Canal_Distribucion cd = new Canal_Distribucion();

                try
                {
                    cd.VKORG = linea.GetValue("VKORG").ToString();
                    cd.VTWEG = linea.GetValue("VTWEG").ToString();
                    cd.VTEXT_ES = linea.GetValue("VTEXT_ES").ToString();
                    cd.VTEXT_EN = linea.GetValue("VTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaCanalDistribucion(connection, cd);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in orc)
            {
                Organizacion_Ventas ov = new Organizacion_Ventas();

                try
                {
                    ov.VKORG = linea.GetValue("VKORG").ToString();
                    ov.VTEXT = linea.GetValue("VTEXT").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaOrgVenta(connection, ov);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in cec)
            {
                CentroCoste cc = new CentroCoste();

                try
                {
                    cc.KOKRS = linea.GetValue("KOKRS").ToString();
                    cc.KOSTL = linea.GetValue("KOSTL").ToString();
                    cc.KTEXT_ES = linea.GetValue("KTEXT_ES").ToString();
                    cc.KTEXT_EN = linea.GetValue("KTEXT_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaCentroCosto(connection, cc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in cen)
            {
                CentroBeneficio cb = new CentroBeneficio();
                try
                {
                    cb.BUKRS = linea.GetValue("BUKRS").ToString();
                    cb.KOKRS = linea.GetValue("KOKRS").ToString();
                    cb.PRCTR = linea.GetValue("PRCTR").ToString();
                    cb.KTEXT = linea.GetValue("KTEXT").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaCentroBeneficio(connection, cb);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in soc)
            {
                Sociedades so = new Sociedades();
                try
                {
                    so.BUKRS = linea.GetValue("BUKRS").ToString();
                    so.BUTXT_ES = linea.GetValue("BUTXT_ES").ToString();
                    so.BUTXT_EN = linea.GetValue("BUTXT_EN").ToString();
                    so.ORT01 = linea.GetValue("ORT01").ToString();
                    so.WAERS = linea.GetValue("WAERS").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaSociedades(connection, so);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in gru)
            {
                GrupoCompras gc = new GrupoCompras();
                try
                {
                    gc.EKGRP = linea.GetValue("EKGRP").ToString();
                    gc.EKNAM = linea.GetValue("EKNAM").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaGrupoCompras(connection, gc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in org)
            {
                OrganizacionCompras oc = new OrganizacionCompras();

                try
                {
                    oc.WERKS = linea.GetValue("WERKS").ToString();
                    oc.EKORG = linea.GetValue("EKORG").ToString();
                    oc.EKOTX_ES = linea.GetValue("EKOTX_ES").ToString();
                    oc.EKOTX_EN = linea.GetValue("EKOTX_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaOrganizacionCompras(connection, oc);
                }
                catch (Exception) { }
            }
            foreach (IRfcStructure linea in alm)
            {
                Almacenes al = new Almacenes();

                try
                {
                    al.WERKS = linea.GetValue("WERKS").ToString();
                    al.LGORT = linea.GetValue("LGORT").ToString();
                    al.LGOBE_ES = linea.GetValue("LGOBE_ES").ToString();
                    al.LGOBE_EN = linea.GetValue("LGOBE_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaAlmacenes(connection, al);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in cum)
            {
                CuentaMayor cu = new CuentaMayor();
                try
                {
                    cu.SAKNR = linea.GetValue("SAKNR").ToString();
                    cu.KTOPL = linea.GetValue("KTOPL").ToString();
                    cu.TXT50_ES = linea.GetValue("TXT50_ES").ToString();
                    cu.TXT50_EN = linea.GetValue("TXT50_EN").ToString();

                    DALC_Catalogo.ObtenerInstancia().IngresaCuentaMayor(connection, cu);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGOS_MM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGOS_MM", "X", centro);
            return true;
        }
        public bool P_ZCATALOGO_IW31(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZCATALOGO_IW31");
            FUNCTION.SetValue("WERKS", centro);
            //IRfcTable ta1 = FUNCTION.GetTable("CLASE_ORDEN");
            //IRfcTable ta2 = FUNCTION.GetTable("CENTRO_PLANIFICACION");
            //IRfcTable ta3 = FUNCTION.GetTable("GRUPOS_PLANIFICACION");
            //IRfcTable ta4 = FUNCTION.GetTable("PUESTO_TRABAJO");
            //IRfcTable ta5 = FUNCTION.GetTable("ACTIVIDAD_ORDEN");
            //IRfcTable ta6 = FUNCTION.GetTable("PRIORIDAD");
            //IRfcTable ta7 = FUNCTION.GetTable("CONTROL_OPERACION");
            IRfcTable ta8 = FUNCTION.GetTable("UNIDADES_MEDIDA");
            try
            {
                FUNCTION.Invoke(rfcDesti);
                //DALC_IW31.obtenerInstania().VaciarCLASE_ORDEN(connection);
                //DALC_IW31.obtenerInstania().VaciarCENTRO_PLANIFICACION(connection);
                //DALC_IW31.obtenerInstania().VaciarGRUPOS_PLANIFICACION(connection);
                //DALC_IW31.obtenerInstania().VaciarPUESTO_TRABAJO(connection);
                //DALC_IW31.obtenerInstania().VaciarACTIVIDAD_ORDEN(connection);
                //DALC_IW31.obtenerInstania().VaciarPRIORIDAD(connection);
                //DALC_IW31.obtenerInstania().VaciarCONTROL_OPERACION(connection);
                DALC_IW31.obtenerInstania().VaciarUNIDADES_MEDIDA(connection);
            }
            catch (Exception) { return false; }
            //foreach (IRfcStructure linea in ta1)
            //{
            //    CLASE_ORDEN um = new CLASE_ORDEN();

            //    try
            //    {
            //        um.AUART = linea.GetValue("AUART").ToString();
            //        um.AUTYP = linea.GetValue("AUTYP").ToString();
            //        um.TXT_ES = linea.GetValue("TXT_ES").ToString();
            //        um.TXT_EN = linea.GetValue("TXT_EN").ToString();

            //        DALC_IW31.obtenerInstania().IngresaCLASE_ORDEN(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta2)
            //{
            //    CENTRO_PLANIFICACION um = new CENTRO_PLANIFICACION();

            //    try
            //    {
            //        um.IWERK = linea.GetValue("IWERK").ToString();

            //        DALC_IW31.obtenerInstania().IngresaCENTRO_PLANIFICACION(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta3)
            //{
            //    GRUPOS_PLANIFICACION um = new GRUPOS_PLANIFICACION();

            //    try
            //    {
            //        um.INGRP = linea.GetValue("INGRP").ToString();
            //        um.IWERK = linea.GetValue("IWERK").ToString();

            //        DALC_IW31.obtenerInstania().IngresaGRUPOS_PLANIFICACION(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta4)
            //{
            //    PUESTO_TRABAJO um = new PUESTO_TRABAJO();

            //    try
            //    {
            //        um.VERWE = linea.GetValue("VERWE").ToString();
            //        um.WERKS = linea.GetValue("WERKS").ToString();
            //        um.ARBPL = linea.GetValue("ARBPL").ToString();
            //        um.KTEXT_UP_ES = linea.GetValue("KTEXT_UP_ES").ToString();
            //        um.KTEXT_UP_EN = linea.GetValue("KTEXT_UP_EN").ToString();
            //        um.KTEXT_ES = linea.GetValue("KTEXT_ES").ToString();
            //        um.KTEXT_EN = linea.GetValue("KTEXT_EN").ToString();

            //        DALC_IW31.obtenerInstania().IngresaPUESTO_TRABAJO(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta5)
            //{
            //    ACTIVIDAD_ORDEN um = new ACTIVIDAD_ORDEN();

            //    try
            //    {
            //        um.AUART = linea.GetValue("AUART").ToString();
            //        um.ILART = linea.GetValue("ILART").ToString();
            //        um.ILATX_ES = linea.GetValue("ILATX_ES").ToString();
            //        um.ILATX_EN = linea.GetValue("ILATX_EN").ToString();

            //        DALC_IW31.obtenerInstania().IngresaACTIVIDAD_ORDEN(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta6)
            //{
            //    PRIORIDAD um = new PRIORIDAD();

            //    try
            //    {
            //        um.ARTPR = linea.GetValue("ARTPR").ToString();
            //        um.PRIOK = linea.GetValue("PRIOK").ToString();
            //        um.PRIOKX_ES = linea.GetValue("PRIOKX_ES").ToString();
            //        um.PRIOKX_EN = linea.GetValue("PRIOKX_EN").ToString();

            //        DALC_IW31.obtenerInstania().IngresaPRIORIDAD(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta7)
            //{
            //    CONTROL_OPERACION um = new CONTROL_OPERACION();

            //    try
            //    {
            //        um.STEUS = linea.GetValue("STEUS").ToString();
            //        um.TXT_ES = linea.GetValue("TXT_ES").ToString();
            //        um.TXT_EN = linea.GetValue("TXT_EN").ToString();

            //        DALC_IW31.obtenerInstania().IngresaCONTROL_OPERACION(connection, um);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure linea in ta8)
            {
                UNIDADES_MEDIDA um = new UNIDADES_MEDIDA();
                try
                {
                    um.MSEHI = linea.GetValue("MSEHI").ToString();
                    um.MSEHL_ES = linea.GetValue("MSEHL_ES").ToString();
                    um.MSEHL_EN = linea.GetValue("MSEHL_EN").ToString();
                    um.DECIMALES = linea.GetValue("DECIMALES").ToString();
                    DALC_IW31.obtenerInstania().IngresaUNIDADES_MEDIDA(connection, um);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGO_IW31", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGO_IW31", "X", centro);
            return true;
        }
        public bool P_ZCATALOGOS_IW21(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZCATALOGOS_IW21");
            FUNCTION.SetValue("TIWERK", centro);
            IRfcTable ta1 = FUNCTION.GetTable("GRUPO_PLAN");
            IRfcTable ta2 = FUNCTION.GetTable("RESPONSABLE_TBJ");
            IRfcTable ta3 = FUNCTION.GetTable("CLASE_AVI");

            try
            {
                FUNCTION.Invoke(rfcDesti);
                DALC_GrupoPlan.ObtenerInstancia().VaciarGrupoPlan(connection);
                DALC_GrupoPlan.ObtenerInstancia().VaciarResponsable(connection);
                DALC_GrupoPlan.ObtenerInstancia().VaciarClaseAviso(connection);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                GrupoPlan um = new GrupoPlan();

                try
                {
                    um.IWERK = linea.GetValue("IWERK").ToString();
                    um.INGRP = linea.GetValue("INGRP").ToString();
                    um.INNAM = linea.GetValue("INNAM").ToString();
                    um.INTEL = linea.GetValue("INTEL").ToString();
                    um.AUART_WP = linea.GetValue("AUART_WP").ToString();

                    DALC_GrupoPlan.ObtenerInstancia().IngresaGrupoPlan(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                ResponsableTrab um = new ResponsableTrab();

                try
                {
                    um.VERAN = linea.GetValue("VERAN").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.ARBPL = linea.GetValue("ARBPL").ToString();
                    um.KTEXT_UP = linea.GetValue("KTEXT_UP").ToString();
                    um.KTEXT = linea.GetValue("KTEXT").ToString();
                    um.SPRAS = linea.GetValue("SPRAS").ToString();

                    DALC_GrupoPlan.ObtenerInstancia().IngresaResponsable(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta3)
            {
                ClaseAviso cla = new ClaseAviso();
                try
                {
                    cla.QMART = linea.GetValue("QMART").ToString();

                    DALC_GrupoPlan.ObtenerInstancia().IngresaClaseAviso(connection, cla);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGOS_IW21", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGOS_IW21", "X", centro);
            return true;
        }
        public bool P_ZMM_ALMACENES_IVEND(string centro)
        {
            //RfcRepository mca = rfcDesti.Repository;
            //IRfcFunction FUNCTION = mca.CreateFunction("ZMM_ALMACENES_IVEND");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable almc = FUNCTION.GetTable("ZLGO_IVE");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in almc)
            //{
            //    Almacenes_Ivend almid = new Almacenes_Ivend();
            //    try
            //    {
            //        almid.WERKS = linea.GetValue("WERKS").ToString();
            //        almid.LGORT = linea.GetValue("LGORT").ToString();
            //        almid.IVEND = linea.GetValue("IVEND").ToString();
            //        almid.ILGORT = linea.GetValue("ILGORT").ToString();
            //        almid.LOCATION = linea.GetValue("LOCACION").ToString();
            //        DALC_Almacenes_Ivend.ObtenerInstancia().VaciarAlmacenesIvend(connection, almid);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in almc)
            //{
            //    Almacenes_Ivend almid = new Almacenes_Ivend();
            //    try
            //    {
            //        almid.WERKS = linea.GetValue("WERKS").ToString();
            //        almid.LGORT = linea.GetValue("LGORT").ToString();
            //        almid.IVEND = linea.GetValue("IVEND").ToString();
            //        almid.ILGORT = linea.GetValue("ILGORT").ToString();
            //        almid.LOCATION = linea.GetValue("LOCACION").ToString();

            //        DALC_Almacenes_Ivend.ObtenerInstancia().IngresaAlmacenesIvend(connection, almid);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_ALMACENES_IVEND", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_ALMACENES_IVEND", "X", centro);
            return true;
        }
        public bool P_ZREG_INFO(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREG_INFO");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable inf = FUNCTION.GetTable("INFO");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in inf)
            {
                RegInfo re = new RegInfo();
                try
                {
                    re.INFNR = linea.GetValue("INFNR").ToString();
                    re.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_RegInfo.obtenerInstacia().VaciarRegistro(connection, re);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in inf)
            {
                RegInfo re = new RegInfo();

                try
                {
                    re.INFNR = linea.GetValue("INFNR").ToString();
                    re.EKORG = linea.GetValue("EKORG").ToString();
                    re.ESOKZ = linea.GetValue("ESOKZ").ToString();
                    re.WERKS = linea.GetValue("WERKS").ToString();
                    re.MATNR = linea.GetValue("MATNR").ToString();
                    re.UMREN = linea.GetValue("UMREN").ToString();
                    re.UMREZ = linea.GetValue("UMREZ").ToString();
                    re.LMEIN = linea.GetValue("LMEIN").ToString();
                    re.MEINS = linea.GetValue("MEINS").ToString();
                    re.MATKL = linea.GetValue("MATKL").ToString();
                    re.EKGRP = linea.GetValue("EKGRP").ToString();
                    re.APLFZ = linea.GetValue("APLFZ").ToString();
                    re.NORBM = linea.GetValue("NORBM").ToString();
                    re.UNTTO = linea.GetValue("UNTTO").ToString();
                    re.UEBTO = linea.GetValue("UEBTO").ToString();
                    re.BSTMA = linea.GetValue("BSTMA").ToString();
                    re.MTXNO = linea.GetValue("MTXNO").ToString();
                    re.KZABS = linea.GetValue("KZABS").ToString();
                    re.MWSKZ = linea.GetValue("MWSKZ").ToString();
                    re.NETPR = linea.GetValue("NETPR").ToString();
                    re.WAERS = linea.GetValue("WAERS").ToString();
                    re.PEINH = linea.GetValue("PEINH").ToString();
                    re.BPRME = linea.GetValue("BPRME").ToString();
                    re.NAME1 = linea.GetValue("NAME1").ToString();
                    re.LIFNR = linea.GetValue("LIFNR").ToString();
                    re.IDNLF = linea.GetValue("IDNLF").ToString();
                    re.LVORM = linea.GetValue("LVORM").ToString();

                    DALC_RegInfo.obtenerInstacia().IngresaRegistro(connection, re);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREG_INFO", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREG_INFO", "X", centro);
            return true;
        }
        public bool P_ZSAM_BOMEQUI(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZSAM_BOMEQUI");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tab = FUNCTION.GetTable("PM_BOM");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tab)
            {
                BomEquipo be = new BomEquipo();
                try
                {
                    be.EQUNR = linea.GetValue("EQUNR").ToString();
                    be.STLNR = linea.GetValue("STLNR").ToString();
                    be.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_BomEquipo.obtenerInstania().VaciarBom(connection, be);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tab)
            {
                BomEquipo be = new BomEquipo();

                try
                {
                    be.MANDT = linea.GetValue("MANDT").ToString();
                    be.EQUNR = linea.GetValue("EQUNR").ToString();
                    be.WERKS = linea.GetValue("WERKS").ToString();
                    be.STLAN = linea.GetValue("STLAN").ToString();
                    be.STLTY = linea.GetValue("STLTY").ToString();
                    be.STLNR = linea.GetValue("STLNR").ToString();
                    be.STLAL = linea.GetValue("STLAL").ToString();
                    be.STKOZ = linea.GetValue("STKOZ").ToString();
                    be.STLKN = linea.GetValue("STLKN").ToString();
                    be.STPOZ = linea.GetValue("STPOZ").ToString();
                    be.STASZ = linea.GetValue("STASZ").ToString();
                    be.DATUV = linea.GetValue("DATUV").ToString();
                    be.LKENZ = linea.GetValue("LKENZ").ToString();
                    be.LOEKZ = linea.GetValue("LOEKZ").ToString();
                    be.ANDAT = linea.GetValue("ANDAT").ToString();
                    be.ANNAM = linea.GetValue("ANNAM").ToString();
                    be.IDNRK = linea.GetValue("IDNRK").ToString();
                    be.PSWRK = linea.GetValue("PSWRK").ToString();
                    be.POSTP = linea.GetValue("POSTP").ToString();
                    be.SPOSN = linea.GetValue("SPOSN").ToString();
                    be.SORTP = linea.GetValue("SORTP").ToString();
                    be.KMPME = linea.GetValue("KMPME").ToString();
                    be.KMPMG = linea.GetValue("KMPMG").ToString();
                    be.FMENG = linea.GetValue("FMENG").ToString();

                    DALC_BomEquipo.obtenerInstania().IngresaBom(connection, be);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_BOMEQUI", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSAM_BOMEQUI", "X", centro);
            return true;
        }
        public bool P_ZREP_STATUS_ORD(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_STATUS_ORD");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable tabStatus = FUNCTION.GetTable("STATUS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabStatus)
            {
                StatusOrdenes sto = new StatusOrdenes();
                try
                {
                    sto.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    sto.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_RepStatusOrd.ObtenerInstancia().VaciarStatusOrdenes(connection, sto);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tabStatus)
            {
                StatusOrdenes sto = new StatusOrdenes();

                try
                {
                    sto.FOLIO_ORD = linea.GetValue("FOLIO_ORD").ToString();
                    sto.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    sto.DATUM = linea.GetValue("DATUM").ToString();
                    sto.UZEIT = linea.GetValue("UZEIT").ToString();
                    sto.AUFNR = linea.GetValue("AUFNR").ToString();
                    sto.OPERACION = linea.GetValue("OPERACION").ToString();
                    sto.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    sto.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    sto.MENSAJE = linea.GetValue("MENSAJE").ToString();
                    sto.WERKS = linea.GetValue("WERKS").ToString();

                    DALC_RepStatusOrd.ObtenerInstancia().IngresaStatusOrdenes(connection, sto);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_STATUS_ORD", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_STATUS_ORD", "X", centro);
            return true;
        }
        public bool P_ZREP_ENTRADAS(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_ENTRADAS");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable taCons = FUNCTION.GetTable("POSICION");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in taCons)
            {
                ReporteEntradas rep = new ReporteEntradas();
                try
                {
                    rep.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rep.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReporteEntradas.ObtenerInstancia().VaciarReporteEntradas(connection, rep);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in taCons)
            {
                ReporteEntradas rep = new ReporteEntradas();

                try
                {
                    rep.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rep.EBELN = linea.GetValue("EBELN").ToString();
                    rep.EBELP = linea.GetValue("EBELP").ToString();
                    rep.WERKS = linea.GetValue("WERKS").ToString();
                    rep.SERVICE = linea.GetValue("SERVICE").ToString();
                    rep.QUANTITY = linea.GetValue("QUANTITY").ToString();
                    rep.SHORTTEX = linea.GetValue("SHORTTEX").ToString();
                    rep.GR_PRICE = linea.GetValue("GR_PRICE").ToString();
                    rep.PRICEUNI = linea.GetValue("PRICEUNI").ToString();
                    rep.DATUM = linea.GetValue("DATUM").ToString();
                    rep.UZEIT = linea.GetValue("UZEIT").ToString();
                    rep.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    rep.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    rep.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    rep.ERROR = linea.GetValue("ERROR").ToString();

                    DALC_ReporteEntradas.ObtenerInstancia().IngresaReporteEntradas(connection, rep);
                    DALC_ReporteEntradas.ObtenerInstancia().ActualizaReporteEntradas(connection, rep);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_ENTRADAS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_ENTRADAS", "X", centro);
            return true;
        }
        public bool P_ZREP_RESERVAS(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_RESERVAS");
            FUNCTION.SetValue("TWERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable taCons = FUNCTION.GetTable("TREP_RESERVA_CAB");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in taCons)
            {
                ReporteReservas rep = new ReporteReservas();
                try
                {
                    rep.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rep.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReportesReservas.ObtenerInstancia().VaciarReporteReservas(connection, rep);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in taCons)
            {
                ReporteReservas rep = new ReporteReservas();

                try
                {
                    rep.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rep.TABIX = linea.GetValue("TABIX").ToString();
                    rep.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    rep.DATUM = linea.GetValue("DATUM").ToString();
                    rep.UZEIT = linea.GetValue("UZEIT").ToString();
                    rep.WERKS = linea.GetValue("WERKS").ToString();
                    rep.BWART = linea.GetValue("BWART").ToString();
                    rep.LGORT = linea.GetValue("LGORT").ToString();
                    rep.KOSTL = linea.GetValue("KOSTL").ToString();
                    rep.AUFNR = linea.GetValue("AUFNR").ToString();
                    rep.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    rep.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    rep.ERROR = linea.GetValue("ERROR").ToString();

                    DALC_ReportesReservas.ObtenerInstancia().IngresaReportesReservas(connection, rep);
                    DALC_ReportesReservas.ObtenerInstancia().ActualizaReportesReservas(connection, rep);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_RESERVAS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_RESERVAS", "X", centro);
            return true;
        }
        public bool P_ZREP_EMPLEO(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_EMPLEO");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable dec = FUNCTION.GetTable("DEC_EMPLEO");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in dec)
            {
                ReporteEmpleo rep = new ReporteEmpleo();
                try
                {
                    rep.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rep.WERKS = linea.GetValue("WERKS").ToString();
                    //eliminar
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in dec)
            {
                ReporteEmpleo rem = new ReporteEmpleo();
                try
                {
                    rem.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rem.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
                    rem.WERKS = linea.GetValue("WERKS").ToString();
                    rem.AUFNR = linea.GetValue("AUFNR").ToString();
                    rem.FOLIO_ORD = linea.GetValue("FOLIO_ORD").ToString();
                    rem.DATUM = linea.GetValue("DATUM").ToString();
                    rem.UZEIT = linea.GetValue("UZEIT").ToString();
                    rem.VCODE = linea.GetValue("VCODE").ToString();
                    rem.VCODEGRP = linea.GetValue("VCODEGRP").ToString();
                    rem.TEXTO = linea.GetValue("TEXTO").ToString();
                    rem.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    rem.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    rem.MENSAJE = linea.GetValue("MENSAJE").ToString();
                    rem.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
                    rem.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
                    rem.UNAME = linea.GetValue("UNAME").ToString();
                    //Ingresar
                }
                catch (Exception) { return false; }
            }
            return true;
        }
        public bool P_ZREP_SOLPED(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_SOLPED");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable ta1 = FUNCTION.GetTable("SOLPED");

            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                SolPed_Rep sol = new SolPed_Rep();
                try
                {
                    sol.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    sol.PLANT = linea.GetValue("PLANT").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().VaciarSolpedRep(connection, sol);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                SolPed_Rep sol = new SolPed_Rep();

                try
                {
                    sol.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    sol.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    sol.MATERIAL = linea.GetValue("MATERIAL").ToString();
                    sol.DATUM = linea.GetValue("DATUM").ToString();
                    sol.UZEIT = linea.GetValue("UZEIT").ToString();
                    sol.DOC_TYPE = linea.GetValue("DOC_TYPE").ToString();
                    sol.TEXTO_CAB = linea.GetValue("TEXTO_CAB").ToString();
                    sol.SHORT_TEXT = linea.GetValue("SHORT_TEXT").ToString();
                    sol.QUANTITY = linea.GetValue("QUANTITY").ToString();
                    sol.PLANT = linea.GetValue("PLANT").ToString();
                    sol.PREQ_DATE = linea.GetValue("PREQ_DATE").ToString();
                    sol.DELIV_DATE = linea.GetValue("DELIV_DATE").ToString();
                    sol.PREIS = linea.GetValue("PREIS").ToString();
                    sol.WAERS = linea.GetValue("WAERS").ToString();
                    sol.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    sol.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    sol.ERROR = linea.GetValue("ERROR").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaRepSolPed(connection, sol);
                    DALC_Sol_Ped.ObtenerInstancia().ActualizaRepSolPed(connection, sol);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_SOLPED", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_SOLPED", "X", centro);
            return true;
        }
        public bool P_ZREP_MOVIMIENTOS(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_MOVIMIENTOS");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable ta1 = FUNCTION.GetTable("MOVIMIENTOS");

            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteMovimientos um = new ReporteMovimientos();
                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReporteMovimientos.ObtenerInstancia().VaciarReporteMovimientos(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteMovimientos um = new ReporteMovimientos();

                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.UZEIT = linea.GetValue("UZEIT").ToString();
                    um.DATUM = linea.GetValue("DATUM").ToString();
                    um.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.LGORT = linea.GetValue("LGORT").ToString();
                    um.ANULA = linea.GetValue("ANULA").ToString();
                    um.TEXTO = linea.GetValue("TEXTO").ToString();
                    um.TEXCAB = linea.GetValue("TEXCAB").ToString();
                    um.TIPO = linea.GetValue("TIPO").ToString();
                    um.UMLGO = linea.GetValue("UMLGO").ToString();
                    um.FACTURA = linea.GetValue("FACTURA").ToString();
                    um.PEDIM = linea.GetValue("PEDIM").ToString();
                    um.LFSNR = linea.GetValue("LFSNR").ToString();
                    um.HU = linea.GetValue("HU").ToString();
                    um.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    um.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    um.ERROR = linea.GetValue("ERROR").ToString();
                    um.IBORRADO = linea.GetValue("IBORRADO").ToString();

                    DALC_ReporteMovimientos.ObtenerInstancia().IngresarReporteMovimientos(connection, um);
                    DALC_ReporteMovimientos.ObtenerInstancia().ActualizaReporteMovimientos(connection, um);
                }
                catch (Exception) { return false; }
            }
            List<SELECT_lista_mov_eliminar_MDL_Result> el = DALC_ReporteMovimientos.ObtenerInstancia().ObtenerListaEliminar(connection).ToList();
            foreach (SELECT_lista_mov_eliminar_MDL_Result mol in el)
            {
                DALC_ReporteMovimientos.ObtenerInstancia().EliminarMovimiento(connection, mol.folio_sam);
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_MOVIMIENTOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_MOVIMIENTOS", "X", centro);
            return true;
        }
        public bool P_ZREP_CONSUMOS(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_CONSUMOS");
            FUNCTION.SetValue("WERKS", centro);
            //FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable taCons = FUNCTION.GetTable("CONSUMOS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in taCons)
            {
                Consumos con = new Consumos();
                try
                {
                    con.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    con.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_RepConsumos.ObtenerInstancia().VaciarConsumos(connection, con);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in taCons)
            {
                Consumos con = new Consumos();

                try
                {
                    con.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    con.FOLIO_ORD = linea.GetValue("FOLIO_ORD").ToString();
                    con.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    con.UZEIT = linea.GetValue("UZEIT").ToString();
                    con.DATUM = linea.GetValue("DATUM").ToString();
                    con.AUFNR = linea.GetValue("AUFNR").ToString();
                    con.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    con.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    con.ERROR = linea.GetValue("ERROR").ToString();
                    con.WERKS = linea.GetValue("WERKS").ToString();

                    DALC_RepConsumos.ObtenerInstancia().IngresaConsumos(connection, con);
                    DALC_RepConsumos.ObtenerInstancia().ActualizaConsumos(connection, con);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_CONSUMOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_CONSUMOS", "X", centro);
            return true;
        }
        public bool P_ZREP_AVISOS(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_AVISOS");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable ta1 = FUNCTION.GetTable("AVISO");

            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteAviso um = new ReporteAviso();
                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.IWERK = linea.GetValue("IWERK").ToString();
                    DALC_ReporteAvisos.ObtenerInstancia().VaciarReporteAviso(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteAviso um = new ReporteAviso();

                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.UZEIT = linea.GetValue("UZEIT").ToString();
                    um.DATUM = linea.GetValue("DATUM").ToString();
                    um.IWERK = linea.GetValue("IWERK").ToString();
                    um.QMART = linea.GetValue("QMART").ToString();
                    um.QMTXT = linea.GetValue("QMTXT").ToString();
                    um.EQUNR = linea.GetValue("EQUNR").ToString();
                    um.QMGRP = linea.GetValue("QMGRP").ToString();
                    um.QMCOD = linea.GetValue("QMCOD").ToString();
                    um.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    um.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    um.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    um.ERROR = linea.GetValue("ERROR").ToString();

                    DALC_ReporteAvisos.ObtenerInstancia().IngresarReporteAviso(connection, um);
                    DALC_ReporteAvisos.ObtenerInstancia().ActualizaReporteAviso(connection, um);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_AVISOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_AVISOS", "X", centro);
            return true;
        }
        public bool P_ZREP_NOTIFICACIONES(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_NOTIFICACIONES");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable ta1 = FUNCTION.GetTable("NOTIFICACIONES");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteNotificaciones um = new ReporteNotificaciones();
                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReporteNotificaciones.ObtenerInstancia().VaciarReporteNotificaciones(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteNotificaciones um = new ReporteNotificaciones();

                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.VORNR = linea.GetValue("VORNR").ToString();
                    um.RMZHL = linea.GetValue("RMZHL").ToString();
                    um.AUFNR = linea.GetValue("AUFNR").ToString();
                    um.UZEIT = linea.GetValue("UZEIT").ToString();
                    um.DATUM = linea.GetValue("DATUM").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.PERNR = linea.GetValue("PERNR").ToString();
                    um.ISMNW_2 = linea.GetValue("ISMNW_2").ToString();
                    um.ISMNU = linea.GetValue("ISMNU").ToString();
                    um.IDAUR = linea.GetValue("IDAUR").ToString();
                    um.IDAUE = linea.GetValue("IDAUE").ToString();
                    um.AUERU = linea.GetValue("AUERU").ToString();
                    um.LTXA1 = linea.GetValue("LTXA1").ToString();
                    um.ARBEH = linea.GetValue("ARBEH").ToString();
                    um.DAUNE = linea.GetValue("DAUNE").ToString();
                    um.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    um.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    um.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    um.ERROR = linea.GetValue("ERROR").ToString();
                    um.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
                    um.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();

                    DALC_ReporteNotificaciones.ObtenerInstancia().IngresarReporteNotificaciones(connection, um);
                    DALC_ReporteNotificaciones.ObtenerInstancia().ActualizaReporteNotificaciones(connection, um);

                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_NOTIFICACIONES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_NOTIFICACIONES", "X", centro);
            return true;
        }
        public bool P_ZREP_ORDENES(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_ORDENES");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable ta1 = FUNCTION.GetTable("ORDEN");

            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteOrden um = new ReporteOrden();
                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.PPLANT = linea.GetValue("PPLANT").ToString();
                    DALC_ReporteOrden.ObtenerInstancia().VaciarReporteOrden(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteOrden um = new ReporteOrden();

                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.AUFNR = linea.GetValue("AUFNR").ToString();
                    um.FUNC_LOC = linea.GetValue("FUNC_LOC").ToString();
                    um.UZEIT = linea.GetValue("UZEIT").ToString();
                    um.DATUM = linea.GetValue("DATUM").ToString();
                    um.TABIX = linea.GetValue("TABIX").ToString();
                    um.PPLANT = linea.GetValue("PPLANT").ToString();
                    um.ORDTYPE = linea.GetValue("ORDTYPE").ToString();
                    um.MNWKCTR = linea.GetValue("MNWKCTR").ToString();
                    um.SHORTTXT = linea.GetValue("SHORTTXT").ToString();
                    um.EQUIP = linea.GetValue("EQUIP").ToString();
                    um.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    um.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    um.ERROR = linea.GetValue("ERROR").ToString();

                    DALC_ReporteOrden.ObtenerInstancia().IngresarReporteOrden(connection, um);
                    DALC_ReporteOrden.ObtenerInstancia().ActualizaReporteOrden(connection, um);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_ORDENES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_ORDENES", "X", centro);
            return true;
        }
        public bool P_ZMM_STOCK_TRANSFERENCIA(string centro)
        {
            RfcRepository mca = rfcDesti.Repository;
            IRfcFunction FUNCTION = mca.CreateFunction("ZMM_STOCK_TRANSFERENCIA");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable almc = FUNCTION.GetTable("ZMM_STRA");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure linea in almc)
            {
                StockTransferencia almid = new StockTransferencia();
                try
                {
                    almid.WERKS = linea.GetValue("WERKS").ToString();
                    almid.MATNR = linea.GetValue("MATNR").ToString();
                    almid.UMLMC = linea.GetValue("UMLMC").ToString();
                    almid.XCHPF = linea.GetValue("XCHPF").ToString();
                    almid.MEINS = linea.GetValue("MEINS").ToString();
                    almid.MAKTX = linea.GetValue("MAKTX").ToString();
                    almid.MAKTX_E = linea.GetValue("MAKTX_E").ToString();
                    almid.LVORM = linea.GetValue("LVORM").ToString();

                    List<SELECT_Validar_existe_material_MDL_Result> valida = DALC_StockTransferencia.obtenerInstancia().ValidarMaterial(connection, almid.WERKS, almid.MATNR).ToList();
                    if (valida.Count > 0)
                    {
                        string operacion = "";
                        operacion = almid.UMLMC.ToString().Substring(0, 1);
                        if (operacion.Equals("-"))
                        {
                            decimal sap, sam, resta;
                            operacion = almid.UMLMC.Replace("-", "");
                            sap = Convert.ToDecimal(operacion.ToString());
                            foreach (SELECT_Validar_existe_material_MDL_Result va in valida)
                            {
                                sam = Convert.ToDecimal(va.stock_traslado.ToString());
                                resta = sam - sap;
                                string stock = Convert.ToString(resta);
                                DALC_StockTransferencia.obtenerInstancia().ActulizarMaterialStockTransferencia(connection, almid.WERKS, almid.MATNR, stock);
                            }
                        }
                        else
                        {
                            decimal sap, sam, suma;
                            sap = Convert.ToDecimal(almid.UMLMC.ToString());
                            foreach (SELECT_Validar_existe_material_MDL_Result va in valida)
                            {
                                sam = Convert.ToDecimal(va.stock_traslado.ToString());
                                suma = sap + sam;
                                string stock = Convert.ToString(suma);
                                DALC_StockTransferencia.obtenerInstancia().ActulizarMaterialStockTransferencia(connection, almid.WERKS, almid.MATNR, stock);
                            }
                        }
                    }
                    else
                    {
                        DALC_StockTransferencia.obtenerInstancia().IngresarStockTransferencia(connection, almid);
                    }
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_STOCK_TRANSFERENCIA", "Correcto");
            return true;
        }
        //public bool P_ZMM_STOCK_TRANSFERENCIA(string centro)
        //{
        //    RfcRepository mca = rfcDesti.Repository;
        //    IRfcFunction FUNCTION = mca.CreateFunction("ZMM_STOCK_TRANSFERENCIA");
        //    FUNCTION.SetValue("WERKS", centro);
        //    IRfcTable almc = FUNCTION.GetTable("ZMM_STRA");
        //    try
        //    {
        //        FUNCTION.Invoke(rfcDesti);
        //    }
        //    catch (Exception) { }
        //    foreach (IRfcStructure linea in almc)
        //    {
        //        StockTransferencia almid = new StockTransferencia();
        //        try
        //        {
        //            almid.WERKS = linea.GetValue("WERKS").ToString();
        //            almid.MATNR = linea.GetValue("MATNR").ToString();
        //            almid.UMLMC = linea.GetValue("UMLMC").ToString();
        //            almid.XCHPF = linea.GetValue("XCHPF").ToString();
        //            almid.MEINS = linea.GetValue("MEINS").ToString();
        //            almid.MAKTX = linea.GetValue("MAKTX").ToString();
        //            almid.MAKTX_E = linea.GetValue("MAKTX_E").ToString();
        //            almid.LVORM = linea.GetValue("LVORM").ToString();

        //            List<SELECT_Validar_existe_material_MDL_Result> valida = DALC_StockTransferencia.obtenerInstancia().ValidarMaterial(connection, almid.WERKS, almid.MATNR).ToList();
        //            if (valida.Count > 0)
        //            {
        //                string operacion = "";
        //                operacion = almid.UMLMC.ToString().Substring(0, 1);
        //                if (operacion.Equals("-"))
        //                {
        //                    decimal sap, sam, resta;
        //                    operacion = almid.UMLMC.Replace("-", "");
        //                    sap = Convert.ToDecimal(operacion.ToString());
        //                    foreach (SELECT_Validar_existe_material_MDL_Result va in valida)
        //                    {
        //                        sam = Convert.ToDecimal(va.stock_traslado.ToString());
        //                        resta = sam - sap;
        //                        string stock = Convert.ToString(resta);
        //                        DALC_StockTransferencia.obtenerInstancia().ActulizarMaterialStockTransferencia(connection, almid.WERKS, almid.MATNR, stock);
        //                    }
        //                }
        //                else
        //                {
        //                    decimal sap, sam, suma;
        //                    sap = Convert.ToDecimal(almid.UMLMC.ToString());
        //                    foreach (SELECT_Validar_existe_material_MDL_Result va in valida)
        //                    {
        //                        sam = Convert.ToDecimal(va.stock_traslado.ToString());
        //                        suma = sap + sam;
        //                        string stock = Convert.ToString(suma);
        //                        DALC_StockTransferencia.obtenerInstancia().ActulizarMaterialStockTransferencia(connection, almid.WERKS, almid.MATNR, stock);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                DALC_StockTransferencia.obtenerInstancia().IngresarStockTransferencia(connection, almid);
        //            }
        //        }
        //        catch (Exception) { return false; }
        //    }
        //    DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_STOCK_TRANSFERENCIA", "Correcto");
        //    return true;
        //}
        public bool P_ZREPORTE_SOLPE(string centro)
        {
            RfcRepository rep = rfcDesti.Repository;
            IRfcFunction FUNCTION = rep.CreateFunction("ZREPORTE_SOLPED");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable repsol = FUNCTION.GetTable("SOLPE1");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure linea in repsol)
            {
                RepoSolpe reps = new RepoSolpe();
                try
                {
                    reps.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    //reps.BNFPO = linea.GetValue("BNFPO").ToString();
                    reps.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_RepoSolpe.ObtenerInstancia().VaciarRepoSolpe(connection, reps);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in repsol)
            {
                RepoSolpe reps = new RepoSolpe();
                try
                {
                    reps.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    reps.BNFPO = linea.GetValue("BNFPO").ToString();
                    reps.BANFN = linea.GetValue("BANFN").ToString();
                    reps.BADAT = linea.GetValue("BADAT").ToString();
                    reps.PSTYP = linea.GetValue("PSTYP").ToString();
                    reps.KNTTP = linea.GetValue("KNTTP").ToString();
                    reps.MATNR = linea.GetValue("MATNR").ToString();
                    reps.TXZ01 = linea.GetValue("TXZ01").ToString();
                    reps.MENGE = linea.GetValue("MENGE").ToString();
                    reps.MEINS = linea.GetValue("MEINS").ToString();
                    reps.WERKS = linea.GetValue("WERKS").ToString();
                    reps.LGORT = linea.GetValue("LGORT").ToString();
                    reps.AFNAM = linea.GetValue("AFNAM").ToString();
                    reps.FRGDT = linea.GetValue("FRGDT").ToString();
                    reps.FRGKZ = linea.GetValue("FRGKZ").ToString();
                    reps.FRGCT = linea.GetValue("FRGCT").ToString();
                    reps.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    reps.ERROR = linea.GetValue("ERROR").ToString();
                    reps.EBELN = linea.GetValue("EBELN").ToString();
                    reps.UDATE = linea.GetValue("UDATE").ToString();
                    reps.FRGKE = linea.GetValue("FRGKE").ToString();

                    DALC_RepoSolpe.ObtenerInstancia().IngresarSolpe(connection, reps);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREPORTE_SOLPED", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREPORTE_SOLPED", "X", centro);
            return true;
        }
        public string ObtenerUltimoRegistroContador()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_ultimo_registro_contador_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalContadores(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_contadores_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public void P_ZPM_CONTADOR_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroContador();
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            int id = 0;
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_contadores_datos_id_MDL_Result> datos = DALC_Contadores.ObtenerInstancia().ObtenerDatosIdContador(connection, id).ToList();
            foreach (SELECT_contadores_datos_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_contador_valida_hora_MDL_Result> validahor = DALC_Contadores.ObtenerInstancia().ObtenerValidacionHoraContador(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_contador_menos_MDL_Result> f = DALC_Contadores.ObtenerInstancia().ObtenerFolioMenosContador(connection, id).ToList();
                        foreach (SELEC_fol_contador_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_contadores_MDL_Result> movs = DALC_Contadores.ObtenerInstancia().ObtenerTodoFolioContadores(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_contadores_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosContadores(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_contadores_MDL_Result> movs = DALC_Contadores.ObtenerInstancia().ObtenerTodoFolioContadores(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_contadores_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosContadores(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_contadores_MDL_Result> movs = DALC_Contadores.ObtenerInstancia().ObtenerTodoFolioContadores(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_contadores_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosContadores(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosContadores(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_contadores_crea_Folio_MDL_Result> Cont = DALC_Contadores.ObtenerInstancia().ObtenerContadoresFolio(connection, folis).ToList();
            int num = Cont.Count;
            string total = ObtenerTotalContadores(folis);
            if (total.Equals(Cont.Count.ToString()))
            {
                RfcRepository cn = rfcDesti.Repository;
                IRfcFunction FUNCTION = cn.CreateFunction("ZPM_CONTADOR_SAM_VS_SAP_BD");
                IRfcTable contads = FUNCTION.GetTable("CONTADOR_EQUI");
                IRfcTable Rcontads = FUNCTION.GetTable("RCONTADOR_EQUI");
                FUNCTION.SetValue("CAB", num);

                if (Cont.Count > 0)
                {
                    foreach (SELECT_contadores_crea_Folio_MDL_Result ct in Cont)
                    {
                        try
                        {
                            contads.Append();
                            contads.SetValue("FOLIO_SAM", ct.folio_sam);
                            contads.SetValue("VALOR_CNT", ct.valor_contador);
                            contads.SetValue("FOLIO_SAP", ct.folio_sap);
                            contads.SetValue("DATUM", ct.fecha);
                            contads.SetValue("UZEIT", ct.hora_dia);
                            contads.SetValue("NIVEL", ct.nivel);
                            contads.SetValue("ICONO", ct.icono);
                            contads.SetValue("EQUNR", ct.num_equipo);
                            contads.SetValue("EQKTX", ct.denominacion_objeto_tecnico);
                            contads.SetValue("POINT", ct.punto_medida);
                            contads.SetValue("MDOCM", ct.doc_medicion);
                            contads.SetValue("NRECDV", ct.cantidad1);
                            contads.SetValue("NREADG", ct.cantidad2);
                            contads.SetValue("RECDU", ct.unidad_medida_entrada_doc);
                            contads.SetValue("TPLNR", ct.ubicacion_tecnica);
                            contads.SetValue("MATNR", ct.num_material);
                            contads.SetValue("WERKS", ct.centro);
                            contads.SetValue("SERNR", ct.num_serie);
                            contads.SetValue("LGORT", ct.almacen);
                            contads.SetValue("CHARG", ct.num_lote);
                            contads.SetValue("RECIBIDO", ct.recibido);
                            contads.SetValue("PROCESADO", ct.procesado);
                            contads.SetValue("ERROR", ct.error);
                            contads.SetValue("UNAME", ct.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }

                foreach (IRfcStructure ln in Rcontads)
                {
                    Contadores cos = new Contadores();
                    try
                    {
                        cos.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        cos.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_Contadores.ObtenerInstancia().ActulizarContadores(connection, cos);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroNotificaciones()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_notificaciones_ultimo_registro_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalNotificaciones(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_notificaciones_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public void P_ZPM_NOTIFICACION_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroNotificaciones();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_notificaciones_datos_id_MDL_Result> datos = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerDatosIdNotificaciones(connection, id).ToList();
            foreach (SELECT_notificaciones_datos_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_notificaciones_valida_hora_MDL_Result> validahor = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerValidacionHoraNotificaciones(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_notificaciones_menos_MDL_Result> f = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerFolioMenosNotificaciones(connection, id).ToList();
                        foreach (SELEC_fol_notificaciones_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_notificaciones_MDL_Result> movs = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerTodoFolioNotificaciones(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_notificaciones_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosNotificaciones(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_notificaciones_MDL_Result> movs = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerTodoFolioNotificaciones(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_notificaciones_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosNotificaciones(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_notificaciones_MDL_Result> movs = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerTodoFolioNotificaciones(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_notificaciones_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosNotificaciones(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosNotificaciones(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_notificaciones_crea_Folios_MDL_Result> cabNotifi = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerCabFol(connection, folis).ToList();
            List<SELECT_posiciones_notificaciones_crea_Folios_MDL_Result> posNotifi = DALC_NotificacionesSAM.ObtenerInstancia().ObtenerPosFol(connection, folis).ToList();
            int cabs = cabNotifi.Count;
            int poss = posNotifi.Count;
            string total = ObtenerTotalNotificaciones(folis);
            if (total.Equals(posNotifi.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZPM_NOTIFICACION_SAM_VS_SAP_BD");
                IRfcTable notCab = FUNCTION.GetTable("TNOTIFICA_CAB");
                IRfcTable RnotCab = FUNCTION.GetTable("RNOTIFICA_CAB");
                IRfcTable notPos = FUNCTION.GetTable("TNOTIFICA");
                IRfcTable RnotPos = FUNCTION.GetTable("RNOTIFICA");
                FUNCTION.SetValue("CAB", cabs);
                FUNCTION.SetValue("POS", poss);

                if (cabNotifi.Count > 0)
                {
                    foreach (SELECT_cabecera_notificaciones_crea_Folios_MDL_Result nc in cabNotifi)
                    {
                        try
                        {
                            notCab.Append();
                            notCab.SetValue("FOLIO_SAM", nc.folio_sam);
                            notCab.SetValue("FOLIO_ORD", nc.folio_orden_sam);
                            notCab.SetValue("AUFNR", nc.folio_orden_sap);
                            notCab.SetValue("WERKS", nc.centro);
                            notCab.SetValue("VORNR", nc.num_operacion);
                            notCab.SetValue("RMZHL", nc.contador_notificacion);
                            notCab.SetValue("UZEIT", nc.hora_dia);
                            notCab.SetValue("DATUM", nc.fecha);
                            notCab.SetValue("RECIBIDO", nc.recibido);
                            notCab.SetValue("PROCESADO", nc.procesado);
                            notCab.SetValue("ERROR", nc.error);
                            //notCab.SetValue("FECHA_RECIBIDO", nc.fecha_recibido);
                            //notCab.SetValue("HORA_RECIBIDO ", nc.hora_recibido);
                            notCab.SetValue("FECHA_CONTABLE", nc.fecha_contable);
                            notCab.SetValue("UNAME", nc.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                if (posNotifi.Count > 0)
                {
                    foreach (SELECT_posiciones_notificaciones_crea_Folios_MDL_Result pn in posNotifi)
                    {
                        try
                        {
                            notPos.Append();
                            notPos.SetValue("FOLIO_SAM", pn.folio_sam);
                            notPos.SetValue("FOLIO_ORD", pn.folio_orden_sam);
                            notPos.SetValue("VORNR", pn.num_operacion);
                            notPos.SetValue("RMZHL", pn.contador_notificacion);
                            notPos.SetValue("AUFNR", pn.folio_orden_sap);
                            notPos.SetValue("UZEIT", pn.hora_dia);
                            notPos.SetValue("DATUM", pn.fecha);
                            notPos.SetValue("WERKS", pn.centro);
                            notPos.SetValue("PERNR", pn.num_personal);
                            notPos.SetValue("ISMNW_2", "0");
                            notPos.SetValue("ISMNU", pn.unidad_trabajo);
                            notPos.SetValue("IDAUR", pn.duracion_real_notificacion);
                            notPos.SetValue("IDAUE", pn.unidad_duracion_real);
                            notPos.SetValue("AUERU", pn.notificacion_parcial_final);
                            notPos.SetValue("LTXA1", pn.texto_notificacion);
                            notPos.SetValue("ARBEH", pn.unidad_trabajo2);
                            notPos.SetValue("DAUNE", pn.unidad_duracion_normal);
                            notPos.SetValue("FOLIO_SAP", pn.folio_sap);
                            notPos.SetValue("RECIBIDO", pn.recibido);
                            notPos.SetValue("PROCESADO", pn.procesado);
                            notPos.SetValue("ERROR", pn.error);
                            notPos.SetValue("FECHA_RECIBIDO", pn.fecha_recibido);
                            notPos.SetValue("HORA_RECIBIDO", pn.hora_recibido);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }

                foreach (IRfcStructure ln in RnotCab)
                {
                    CabNotificacionesCrea cabnotificaciones = new CabNotificacionesCrea();
                    try
                    {
                        cabnotificaciones.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        cabnotificaciones.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_NotificacionesSAM.ObtenerInstancia().ActualizaCabNotificacionesCrea(connection, cabnotificaciones);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure ln in RnotPos)
                {
                    PosNotificacionesCrea posnotifi = new PosNotificacionesCrea();
                    try
                    {
                        posnotifi.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        posnotifi.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_NotificacionesSAM.ObtenerInstancia().ActualizaPosNotificacionesCrea(connection, posnotifi);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroAvisos()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_ultimo_aviso_registro_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalQmAvisos(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_qm_avisos_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public string ObtenerTotalTextosAvisos(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_texto_avisos_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public void P_ZGUARDA_AVISOS()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroAvisos();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_avisos_datos_id_MDL_Result> datos = DALC_GuardaAvisos.ObtenerInstancia().ObtenerDatosIdAvisos(connection, id).ToList();
            foreach (SELECT_avisos_datos_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_avisos_valida_hora_MDL_Result> validahor = DALC_GuardaAvisos.ObtenerInstancia().ObtenerValidacionHoraAvisos(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_avisos_menos_MDL_Result> f = DALC_GuardaAvisos.ObtenerInstancia().ObtenerFolioMenosAvisos(connection, id).ToList();
                        foreach (SELEC_fol_avisos_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_avisos_MDL_Result> movs = DALC_GuardaAvisos.ObtenerInstancia().ObtenerTodoFolioAvisos(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_avisos_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosAvisos(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_avisos_MDL_Result> movs = DALC_GuardaAvisos.ObtenerInstancia().ObtenerTodoFolioAvisos(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_avisos_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosAvisos(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_avisos_MDL_Result> movs = DALC_GuardaAvisos.ObtenerInstancia().ObtenerTodoFolioAvisos(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_avisos_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosAvisos(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosAvisos(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_avisos_crea_Folio_MDL_Result> cabA = DALC_GuardaAvisos.ObtenerInstancia().ObtenerCabAvisosFol(connection, folis).ToList();
            List<SELECT_qmm_avisos_crea_Folio_MDL_Result> qmA = DALC_GuardaAvisos.ObtenerInstancia().ObtenerQmFol(connection, folis).ToList();
            List<SELECT_texto_avisos_crea_Folio_MDL_Result> texA = DALC_GuardaAvisos.ObtenerInstancia().ObtenerTextoFol(connection, folis).ToList();
            int cA = cabA.Count;
            int QA = qmA.Count;
            int TA = texA.Count;
            string totqm = ObtenerTotalQmAvisos(folis);
            string tottx = ObtenerTotalTextosAvisos(folis);
            if (totqm.Equals(qmA.Count.ToString()) && tottx.Equals(texA.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZGUARDA_AVISOS_SAP_VS_SAM");
                IRfcTable samqm = FUNCTION.GetTable("TSAM_QMMA");
                IRfcTable samtex = FUNCTION.GetTable("TSAM_TEXTOS");
                IRfcTable avicab = FUNCTION.GetTable("TSAM_AVISOS_CAB");
                IRfcTable Ravicab = FUNCTION.GetTable("RSAM_AVISOS_CAB");
                FUNCTION.SetValue("CAB", cA);
                FUNCTION.SetValue("POS", QA);
                FUNCTION.SetValue("TXT", TA);

                if (cabA.Count > 0)
                {
                    foreach (SELECT_cabecera_avisos_crea_Folio_MDL_Result ca in cabA)
                    {
                        try
                        {
                            avicab.Append();
                            avicab.SetValue("FOLIO_SAM", ca.folio_sam);
                            avicab.SetValue("UZEIT", ca.hora_dia);
                            avicab.SetValue("DATUM", ca.fecha);
                            avicab.SetValue("IWERK", ca.centro_planificacion_mante);
                            avicab.SetValue("QMART", ca.clase_aviso);
                            avicab.SetValue("QMTXT", ca.texto_breve);
                            avicab.SetValue("EQUNR", ca.num_equipo);
                            avicab.SetValue("QMGRP", ca.grupo_codigo_codificacion);
                            avicab.SetValue("QMCOD", ca.codificacion);
                            avicab.SetValue("FOLIO_SAP", ca.folio_sap);
                            avicab.SetValue("RECIBIDO", ca.recibido);
                            avicab.SetValue("PROCESADO", ca.procesado);
                            avicab.SetValue("ERROR", ca.error);
                            //avicab.SetValue("FECHA_RECIBIDO", ca.fecha);
                            //avicab.SetValue("HORA_RECIBIDO", ca.HORA_RECIBIDO);
                            // avicab.SetValue("FLAG", ca.FLAG);
                            avicab.SetValue("MODIFICA", ca.modificado);
                            avicab.SetValue("STTXT", ca.status_sistema_aviso_conjunto);
                            avicab.SetValue("BAUTL", ca.conjunto);
                            avicab.SetValue("TPLNR", ca.ubicacion_tecnica);
                            avicab.SetValue("INGRP", ca.grupo_planificador_servicio_cliente_mante);
                            avicab.SetValue("GEWRK", ca.puesto_trabajo_responsable_medida_mante);
                            avicab.SetValue("SWERK", ca.centro);
                            avicab.SetValue("I_PARNR", ca.interlocutor);
                            avicab.SetValue("NAME_LIST", ca.nombre_visualizaciones_lista);
                            avicab.SetValue("PARNR_VERA", ca.interlocutor_vera);
                            avicab.SetValue("NAME_VERA", ca.nombre_visualizaciones_lista_vera);
                            avicab.SetValue("QMNAM", ca.nombre_autor_aviso);
                            avicab.SetValue("HEADKTXT", ca.texto_breve2);
                            avicab.SetValue("ORDEN_SAM", ca.num_orden);
                            avicab.SetValue("ESTATUS", ca.status_concluido);
                            avicab.SetValue("UNAME", ca.usuario);
                            avicab.SetValue("FECHA_MOD", ca.fecha_ultima_modificacion);
                            avicab.SetValue("HORA_MOD", ca.hora_ultima_modificacion);
                        }
                        catch (Exception) { }
                    }
                }
                if (qmA.Count > 0)
                {
                    foreach (SELECT_qmm_avisos_crea_Folio_MDL_Result qm in qmA)
                    {
                        try
                        {
                            samqm.Append();
                            samqm.SetValue("FOLIO_SAM", qm.folio_sam);
                            samqm.SetValue("QMNUM", qm.num_notificacion);
                            samqm.SetValue("MANUM", qm.num_actual_actividad);
                            samqm.SetValue("UZEIT", qm.hora_dia);
                            samqm.SetValue("DATUM", qm.fecha);
                            // samqm.SetValue("TABIX", qm.TABIX);
                            samqm.SetValue("FENUM", qm.num_posicion_registro_posicion);
                            samqm.SetValue("URNUM", qm.num_correlativo_causa);
                            samqm.SetValue("MNKAT", qm.clase_catalogo_actividades);
                            samqm.SetValue("MNGRP", qm.grupo_codigo_acciones);
                            samqm.SetValue("MNCOD", qm.codigo_actividad);
                            samqm.SetValue("MNVER", qm.num_version);
                            samqm.SetValue("MATXT", qm.texto_accion);
                            samqm.SetValue("ERNAM", qm.nombre_responsable_anadio_objeto);
                            samqm.SetValue("ERDAT", qm.fecha_creacion_registro);
                            samqm.SetValue("AENAM", qm.nombre_responsable_modifico_objeto);
                            samqm.SetValue("AEDAT", qm.fecha_ultima_modificacion);
                            samqm.SetValue("MAKLS", qm.clase_medidas);
                            samqm.SetValue("KLAKZ", qm.indicador_existe_clasificacion);
                            samqm.SetValue("PSTER", qm.fecha_inicio);
                            samqm.SetValue("PETER", qm.fecha_fin);
                            samqm.SetValue("INDTX", qm.existe_texto_explicativo_objeto);
                            samqm.SetValue("KZMLA", qm.indicador_segmento_texto_idioma_director);
                            samqm.SetValue("MNGFA", qm.factor_cantidad_acciones);
                            samqm.SetValue("PSTUR", qm.hora_inicio_accion);
                            samqm.SetValue("PETUR", qm.hora_fin_accion);
                            samqm.SetValue("ERZEIT", qm.hora_agrego_registro);
                            samqm.SetValue("AEZEIT", qm.hora_modificacion);
                            samqm.SetValue("KZLOESCH", qm.indicador_borrar_registro_datos);
                            samqm.SetValue("QMANUM", qm.num_claseificacion_actividad);
                            samqm.SetValue("AUTKZ", qm.registro_datos_creado_med_funmod);
                            samqm.SetValue("KZACTIONBOX", qm.creado_ayuda_barra_actividades);
                            samqm.SetValue("FUNKTION", qm.clave_funcion_barra_actividad);
                            samqm.SetValue("KURZTEXT_ES", qm.texto_breve_codigo_ES);
                            samqm.SetValue("KURZTEXT_EN", qm.texto_breve_codigo_EN);
                        }
                        catch (Exception) { }
                    }
                }
                if (texA.Count > 0)
                {
                    foreach (SELECT_texto_avisos_crea_Folio_MDL_Result tx in texA)
                    {
                        try
                        {
                            samtex.Append();
                            samtex.SetValue("FOLIO_SAM", tx.folio_sam);
                            samtex.SetValue("TDFORMAT", tx.formato);
                            samtex.SetValue("TDLINE", tx.linea_texto);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                if (FUNCTION.GetValue("MENSAJE").ToString().Trim().Equals("Correcto"))
                {
                    ZAVISO_SAM(FUNCTION.GetValue("FOLIO_S").ToString().Trim());
                    DALC_GuardaAvisos.ObtenerInstancia().EliminarRegistroAvisos(connection, folio_sam);
                }
                else
                {
                    DALC_GuardaAvisos.ObtenerInstancia().ActualizarAvisoCreacion(connection, folio_sam, FUNCTION.GetValue("MENSAJE").ToString().Trim());
                }
            }
        }
        //public void EnviarDatosAvisos(string folio_sam)
        //{
        //    Thread.Sleep(10000);
        //    string folis = folio_sam;
        //    List<SELECT_cabecera_avisos_crea_Folio_MDL_Result> cabA = DALC_GuardaAvisos.ObtenerInstancia().ObtenerCabAvisosFol(connection, folis).ToList();
        //    List<SELECT_qmm_avisos_crea_Folio_MDL_Result> qmA = DALC_GuardaAvisos.ObtenerInstancia().ObtenerQmFol(connection, folis).ToList();
        //    List<SELECT_texto_avisos_crea_Folio_MDL_Result> texA = DALC_GuardaAvisos.ObtenerInstancia().ObtenerTextoFol(connection, folis).ToList();
        //    int cA = cabA.Count;
        //    int QA = qmA.Count;
        //    int TA = texA.Count;
        //    string totqm = ObtenerTotalQmAvisos(folis);
        //    string tottx = ObtenerTotalTextosAvisos(folis);
        //    if (totqm.Equals(qmA.Count.ToString()) && tottx.Equals(texA.Count.ToString()))
        //    {
        //        RfcRepository repo = rfcDesti.Repository;
        //        IRfcFunction FUNCTION = repo.CreateFunction("ZGUARDA_AVISOS");
        //        IRfcTable samqm = FUNCTION.GetTable("TSAM_QMMA");
        //        IRfcTable samtex = FUNCTION.GetTable("TSAM_TEXTOS");
        //        IRfcTable avicab = FUNCTION.GetTable("TSAM_AVISOS_CAB");
        //        IRfcTable Ravicab = FUNCTION.GetTable("RSAM_AVISOS_CAB");
        //        FUNCTION.SetValue("CAB", cA);
        //        FUNCTION.SetValue("POS", QA);
        //        FUNCTION.SetValue("TXT", TA);

        //        if (cabA.Count > 0)
        //        {
        //            foreach (SELECT_cabecera_avisos_crea_Folio_MDL_Result ca in cabA)
        //            {
        //                try
        //                {
        //                    avicab.Append();
        //                    avicab.SetValue("FOLIO_SAM", ca.folio_sam);
        //                    avicab.SetValue("UZEIT", ca.hora_dia);
        //                    avicab.SetValue("DATUM", ca.fecha);
        //                    avicab.SetValue("IWERK", ca.centro_planificacion_mante);
        //                    avicab.SetValue("QMART", ca.clase_aviso);
        //                    avicab.SetValue("QMTXT", ca.texto_breve);
        //                    avicab.SetValue("EQUNR", ca.num_equipo);
        //                    avicab.SetValue("QMGRP", ca.grupo_codigo_codificacion);
        //                    avicab.SetValue("QMCOD", ca.codificacion);
        //                    avicab.SetValue("FOLIO_SAP", ca.folio_sap);
        //                    avicab.SetValue("RECIBIDO", ca.recibido);
        //                    avicab.SetValue("PROCESADO", ca.procesado);
        //                    avicab.SetValue("ERROR", ca.error);
        //                    //avicab.SetValue("FECHA_RECIBIDO", ca.fecha);
        //                    //avicab.SetValue("HORA_RECIBIDO", ca.HORA_RECIBIDO);
        //                    // avicab.SetValue("FLAG", ca.FLAG);
        //                    avicab.SetValue("MODIFICA", ca.modificado);
        //                    avicab.SetValue("STTXT", ca.status_sistema_aviso_conjunto);
        //                    avicab.SetValue("BAUTL", ca.conjunto);
        //                    avicab.SetValue("TPLNR", ca.ubicacion_tecnica);
        //                    avicab.SetValue("INGRP", ca.grupo_planificador_servicio_cliente_mante);
        //                    avicab.SetValue("GEWRK", ca.puesto_trabajo_responsable_medida_mante);
        //                    avicab.SetValue("SWERK", ca.centro);
        //                    avicab.SetValue("I_PARNR", ca.interlocutor);
        //                    avicab.SetValue("NAME_LIST", ca.nombre_visualizaciones_lista);
        //                    avicab.SetValue("PARNR_VERA", ca.interlocutor_vera);
        //                    avicab.SetValue("NAME_VERA", ca.nombre_visualizaciones_lista_vera);
        //                    avicab.SetValue("QMNAM", ca.nombre_autor_aviso);
        //                    avicab.SetValue("HEADKTXT", ca.texto_breve2);
        //                    avicab.SetValue("ORDEN_SAM", ca.num_orden);
        //                    avicab.SetValue("ESTATUS", ca.status_concluido);
        //                    avicab.SetValue("UNAME", ca.usuario);
        //                    avicab.SetValue("FECHA_MOD", ca.fecha_ultima_modificacion);
        //                    avicab.SetValue("HORA_MOD", ca.hora_ultima_modificacion);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        if (qmA.Count > 0)
        //        {
        //            foreach (SELECT_qmm_avisos_crea_Folio_MDL_Result qm in qmA)
        //            {
        //                try
        //                {
        //                    samqm.Append();
        //                    samqm.SetValue("FOLIO_SAM", qm.folio_sam);
        //                    samqm.SetValue("QMNUM", qm.num_notificacion);
        //                    samqm.SetValue("MANUM", qm.num_actual_actividad);
        //                    samqm.SetValue("UZEIT", qm.hora_dia);
        //                    samqm.SetValue("DATUM", qm.fecha);
        //                    // samqm.SetValue("TABIX", qm.TABIX);
        //                    samqm.SetValue("FENUM", qm.num_posicion_registro_posicion);
        //                    samqm.SetValue("URNUM", qm.num_correlativo_causa);
        //                    samqm.SetValue("MNKAT", qm.clase_catalogo_actividades);
        //                    samqm.SetValue("MNGRP", qm.grupo_codigo_acciones);
        //                    samqm.SetValue("MNCOD", qm.codigo_actividad);
        //                    samqm.SetValue("MNVER", qm.num_version);
        //                    samqm.SetValue("MATXT", qm.texto_accion);
        //                    samqm.SetValue("ERNAM", qm.nombre_responsable_anadio_objeto);
        //                    samqm.SetValue("ERDAT", qm.fecha_creacion_registro);
        //                    samqm.SetValue("AENAM", qm.nombre_responsable_modifico_objeto);
        //                    samqm.SetValue("AEDAT", qm.fecha_ultima_modificacion);
        //                    samqm.SetValue("MAKLS", qm.clase_medidas);
        //                    samqm.SetValue("KLAKZ", qm.indicador_existe_clasificacion);
        //                    samqm.SetValue("PSTER", qm.fecha_inicio);
        //                    samqm.SetValue("PETER", qm.fecha_fin);
        //                    samqm.SetValue("INDTX", qm.existe_texto_explicativo_objeto);
        //                    samqm.SetValue("KZMLA", qm.indicador_segmento_texto_idioma_director);
        //                    samqm.SetValue("MNGFA", qm.factor_cantidad_acciones);
        //                    samqm.SetValue("PSTUR", qm.hora_inicio_accion);
        //                    samqm.SetValue("PETUR", qm.hora_fin_accion);
        //                    samqm.SetValue("ERZEIT", qm.hora_agrego_registro);
        //                    samqm.SetValue("AEZEIT", qm.hora_modificacion);
        //                    samqm.SetValue("KZLOESCH", qm.indicador_borrar_registro_datos);
        //                    samqm.SetValue("QMANUM", qm.num_claseificacion_actividad);
        //                    samqm.SetValue("AUTKZ", qm.registro_datos_creado_med_funmod);
        //                    samqm.SetValue("KZACTIONBOX", qm.creado_ayuda_barra_actividades);
        //                    samqm.SetValue("FUNKTION", qm.clave_funcion_barra_actividad);
        //                    samqm.SetValue("KURZTEXT_ES", qm.texto_breve_codigo_ES);
        //                    samqm.SetValue("KURZTEXT_EN", qm.texto_breve_codigo_EN);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        if (texA.Count > 0)
        //        {
        //            foreach (SELECT_texto_avisos_crea_Folio_MDL_Result tx in texA)
        //            {
        //                try
        //                {
        //                    samtex.Append();
        //                    samtex.SetValue("FOLIO_SAM", tx.folio_sam);
        //                    samtex.SetValue("TDFORMAT", tx.formato);
        //                    samtex.SetValue("TDLINE", tx.linea_texto);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        try
        //        {
        //            FUNCTION.Invoke(rfcDesti);
        //        }
        //        catch (Exception) { }

        //        foreach (IRfcStructure ln in Ravicab)
        //        {
        //            CabAvisosCrea actCabAvi = new CabAvisosCrea();
        //            try
        //            {
        //                actCabAvi.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
        //                actCabAvi.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
        //                DALC_GuardaAvisos.ObtenerInstancia().ActualizaCabAvisosCrea(connection, actCabAvi);
        //            }
        //            catch (Exception) { }
        //        }
        //    }
        //}
        public string ObtenerUltimoRegistroSolped()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_solped_ultimo_reg_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotal(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_solped_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public string ObtenerTotalServiciosSolped(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_servicios_solped_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public string ObtenerTotalTextoCab(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_textocab_solped_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public string ObtenerTotalTextoPos(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_textopos_solped_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public void P_ZGUARDA_SOLPED_SAM()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 1;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroSolped();
            Thread.Sleep(2000);
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_solped_id_MDL_Result> datos = DALC_Sol_Ped.ObtenerInstancia().ObtenerDatosIdSolped(connection, id).ToList();
            foreach (SELEC_datos_solped_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_solped_valida_hora_MDL_Result> validahor = DALC_Sol_Ped.ObtenerInstancia().ObtenerValidacionHoraSolped(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        string ese = folio_sam.Replace("S", "0");
                        string pe = ese.Replace("P", "0");
                        int foli = Convert.ToInt32(pe.ToString()) - 1;
                        folio_sam = "SP" + foli.ToString();
                        //List<SELEC_fol_solped_menos_MDL_Result> f = DALC_Sol_Ped.ObtenerInstancia().ObtenerFolioMenosSolped(connection, id).ToList();
                        //foreach (SELEC_fol_solped_menos_MDL_Result fo in f)
                        //{
                        //folio_sam = fo.folio_sam;
                        List<SELECT_lista_folios_solped_MDL_Result> movs = DALC_Sol_Ped.ObtenerInstancia().ObtenerTodoFolioSolped(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_solped_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosSolped(folio_sam);
                        }
                        //}
                    }
                    else
                    {
                        List<SELECT_lista_folios_solped_MDL_Result> movs = DALC_Sol_Ped.ObtenerInstancia().ObtenerTodoFolioSolped(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_solped_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosSolped(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_solped_MDL_Result> movs = DALC_Sol_Ped.ObtenerInstancia().ObtenerTodoFolioSolped(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_solped_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosSolped(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosSolped(string folio_sam)
        {
            //Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_solped_crea_Fol_MDL_Result> cab = DALC_Sol_Ped.ObtenerInstancia().ObtenerCabecera(connection, folis).ToList();
            List<SELECT_solped_servicios_crea_Fol_MDL_Result> servicios = DALC_Sol_Ped.ObtenerInstancia().ObtenerServicios(connection, folis).ToList();
            List<SELECT_textos_cabecera_solped_Fol_MDL_Result> txtcab = DALC_Sol_Ped.ObtenerInstancia().ObtenerTextoCabecera(connection, folis).ToList();
            List<SELECT_textos_posiciones_solped_Fol_MDL_Result> txtposi = DALC_Sol_Ped.ObtenerInstancia().ObtenerTextoPosicion(connection, folis).ToList();
            //Thread.Sleep(2000);
            string tota = ObtenerTotal(folis);
            string tots = ObtenerTotalServiciosSolped(folis);
            string txtc = ObtenerTotalTextoCab(folis);
            string txtp = ObtenerTotalTextoPos(folis);
            int cabs = cab.Count;
            int sers = servicios.Count;
            int txtC = txtcab.Count;
            int txtP = txtposi.Count;
            if (tota.Equals(cab.Count.ToString()) && tots.Equals(servicios.Count.ToString()) && txtc.Equals(txtcab.Count.ToString()) && txtp.Equals(txtposi.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCION = repo.CreateFunction("ZSOLPED_SAM_VS_SAP");
                IRfcTable tabla = FUNCION.GetTable("SOLPED");
                IRfcTable tabsr = FUNCION.GetTable("RSOLPED");
                IRfcTable tabls = FUNCION.GetTable("SERVICIOS");
                IRfcTable tablr = FUNCION.GetTable("RSERVICIOS");
                IRfcTable txtca = FUNCION.GetTable("TXT_CAB");
                IRfcTable txtpos = FUNCION.GetTable("TXT_POS");
                FUNCION.SetValue("CAB", cabs);
                FUNCION.SetValue("SER", sers);
                FUNCION.SetValue("CABTXT", txtC);
                FUNCION.SetValue("POSTXT", txtP);
                String m = FUNCION.GetValue("MENSAJE").ToString();

                if (cab.Count > 0)
                {
                    foreach (SELECT_solped_crea_Fol_MDL_Result so in cab)
                    {
                        try
                        {
                            tabla.Append();
                            tabla.SetValue("FOLIO_SAM", so.folio_sam);
                            tabla.SetValue("PREQ_ITEM", so.num_posicion_solped);
                            tabla.SetValue("ITEM_CAT", so.tipo_posicion_doc_compras);
                            tabla.SetValue("MATERIAL", so.num_material);
                            tabla.SetValue("DATUM", so.fecha);
                            tabla.SetValue("UZEIT", so.hora_dia);
                            tabla.SetValue("FOLIO_SAP", so.num_solped);
                            tabla.SetValue("DOC_TYPE", so.clase_documento_solped);
                            tabla.SetValue("ACCTASSCAT", so.tipo_imputacion);
                            tabla.SetValue("SHORT_TEXT", so.texto_breve);
                            tabla.SetValue("QUANTITY", so.cantidad_solped);
                            tabla.SetValue("UNIT", so.unidad_medida_solped);
                            tabla.SetValue("DEL_DATCAT", so.tipo_fecha);
                            tabla.SetValue("DELIV_DATE", so.fecha_entraga_posicion);
                            tabla.SetValue("MAT_GRP", so.grupo_articulos);
                            tabla.SetValue("PLANT", so.centro);
                            tabla.SetValue("STORE_LOC", so.almacen);
                            tabla.SetValue("PUR_GROUP", so.grupo_compras);
                            tabla.SetValue("PREQ_NAME", so.solicitante);
                            tabla.SetValue("PREQ_DATE", so.fecha_solicitud);
                            tabla.SetValue("DES_VENDOR", so.proveedor_deseado);
                            tabla.SetValue("LIFNR", so.num_cuenta_proveedor);
                            tabla.SetValue("TABIX", "0");
                            tabla.SetValue("EPSTP", so.tipo_posicion_doc_compras2);
                            tabla.SetValue("INFNR", so.num_registro_info_compras);
                            tabla.SetValue("EKORG", so.organizacion_compras);
                            tabla.SetValue("G_L_ACCT", so.num_cuenta_mayor);
                            tabla.SetValue("COST_CTR", so.centro_coste);
                            tabla.SetValue("KONNR", so.num_contrato_superior);
                            tabla.SetValue("KTPNR", so.num_posicion_contrato_superior);
                            tabla.SetValue("VRTKZ", so.indicador_distribucion_imputacion_multiple);
                            tabla.SetValue("SAKTO", so.num_cuenta_mayor2);
                            tabla.SetValue("AUFNR", so.num_orden);
                            tabla.SetValue("KOKRS", so.sociedad_co);
                            tabla.SetValue("KOSTL", so.centro_coste2);
                            tabla.SetValue("TEXTO_CAB", so.texto_cabecera);
                            tabla.SetValue("TEXTO_POS", so.texto_posicion);
                            tabla.SetValue("RECIBIDO", so.recibido);
                            tabla.SetValue("PROCESADO", so.procesado);
                            tabla.SetValue("MODIFICADO", so.modificado);
                            tabla.SetValue("ERROR", so.error);
                            tabla.SetValue("PREIS", so.precio_solped);
                            tabla.SetValue("WAERS", so.clave_moneda);
                            tabla.SetValue("UNAME", so.usuario);
                            tabla.SetValue("FECHA_MOD", so.fecha_ultima_modificacion);
                            tabla.SetValue("HORA_MOD", so.hora_ultima_modificacion);
                        }
                        catch (Exception) { }
                    }
                }
                if (servicios.Count > 0)
                {
                    foreach (SELECT_solped_servicios_crea_Fol_MDL_Result so in servicios)
                    {
                        try
                        {
                            tabls.Append();
                            tabls.SetValue("FOLIO_SAM", so.folio_sam);
                            tabls.SetValue("PREQ_ITEM", so.num_posicion_solped);
                            tabls.SetValue("NUM_SER", so.num_posicion_solped2);
                            tabls.SetValue("SERVICE", so.num_servicio);
                            tabls.SetValue("DATUM", so.fecha);
                            tabls.SetValue("UZEIT", so.hora_dia);
                            tabls.SetValue("SHORT_TEXT", so.texto_breve);
                            tabls.SetValue("QUANTITY", so.cantidad);
                            tabls.SetValue("BASE_UOM", so.unidad_medida_base);
                            tabls.SetValue("PRICE_UNIT", so.cantidad_base);
                            tabls.SetValue("GR_PRICE", so.precio_bruto);
                            tabls.SetValue("MATL_GROUP", so.grupo_articulos);
                            tabls.SetValue("G_L_ACCT", so.num_cuenta_mayor);
                            tabls.SetValue("COST_CTR", so.centro_coste);
                            tabls.SetValue("AUFNR", so.num_orden);
                            tabls.SetValue("FOLIO_SAP", so.num_solped);
                            tabls.SetValue("RECIBIDO", so.recibido);
                            tabls.SetValue("PROCESADO", so.procesado);
                            tabls.SetValue("MODIFICADO", so.modificado);
                            tabls.SetValue("ERROR", so.error);
                        }
                        catch (Exception) { }
                    }
                }
                if (txtcab.Count > 0)
                {
                    foreach (SELECT_textos_cabecera_solped_Fol_MDL_Result tca in txtcab)
                    {
                        try
                        {
                            txtca.Append();
                            txtca.SetValue("FOLIO_SAM", tca.folio_sam);
                            txtca.SetValue("INDICE", tca.indice);
                            txtca.SetValue("TDFORMAT", tca.columna_formato);
                            txtca.SetValue("TDLINE", tca.linea_texto);
                        }
                        catch (Exception) { }
                    }
                }
                if (txtposi.Count > 0)
                {
                    foreach (SELECT_textos_posiciones_solped_Fol_MDL_Result tpo in txtposi)
                    {
                        try
                        {
                            txtpos.Append();
                            txtpos.SetValue("FOLIO_SAM", tpo.folio_sam);
                            txtpos.SetValue("PREQ_ITEM", tpo.num_posicion_solped);
                            txtpos.SetValue("INDICE", tpo.indice);
                            txtpos.SetValue("TDFORMAT", tpo.columna_formato);
                            txtpos.SetValue("TDLINE", tpo.linea_texto);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                if (FUNCION.GetValue("MENSAJE2").ToString().Trim().Equals("Correcto"))
                {
                    ZSOLPED_SAM(FUNCION.GetValue("FOLIO").ToString().Trim());
                    DALC_Sol_Ped.ObtenerInstancia().EliminarRegistroCrea(connection, folio_sam);
                }
                else
                {
                    foreach (SELECT_solped_crea_Fol_MDL_Result s in cab)
                    {
                        SolPed_Crea so = new SolPed_Crea();
                        so.FOLIO_SAM = folio_sam;
                        so.PREQ_ITEM = s.num_posicion_solped;
                        so.RECIBIDO = "X";
                        so.PROCESADO = "X";
                        so.ERROR = FUNCION.GetValue("MENSAJE2").ToString().Trim();
                        DALC_Sol_Ped.ObtenerInstancia().ActualizaSolpedCrea(connection, so);
                    }
                }
            }
        }
        //public void EnviarDatosSolped(string folio_sam)
        //{
        //    Thread.Sleep(10000);
        //    string folis = folio_sam;
        //    List<SELECT_solped_crea_Fol_MDL_Result> cab = DALC_Sol_Ped.ObtenerInstancia().ObtenerCabecera(connection, folis).ToList();
        //    List<SELECT_solped_servicios_crea_Fol_MDL_Result> servicios = DALC_Sol_Ped.ObtenerInstancia().ObtenerServicios(connection, folis).ToList();
        //    List<SELECT_textos_cabecera_solped_Fol_MDL_Result> txtcab = DALC_Sol_Ped.ObtenerInstancia().ObtenerTextoCabecera(connection, folis).ToList();
        //    List<SELECT_textos_posiciones_solped_Fol_MDL_Result> txtposi = DALC_Sol_Ped.ObtenerInstancia().ObtenerTextoPosicion(connection, folis).ToList();
        //    Thread.Sleep(2000);
        //    string tota = ObtenerTotal(folis);
        //    string tots = ObtenerTotalServiciosSolped(folis);
        //    string txtc = ObtenerTotalTextoCab(folis);
        //    string txtp = ObtenerTotalTextoPos(folis);
        //    int cabs = cab.Count;
        //    int sers = servicios.Count;
        //    int txtC = txtcab.Count;
        //    int txtP = txtposi.Count;
        //    if (tota.Equals(cab.Count.ToString()) && tots.Equals(servicios.Count.ToString()) && txtc.Equals(txtcab.Count.ToString()) && txtp.Equals(txtposi.Count.ToString()))
        //    {
        //        RfcRepository repo = rfcDesti.Repository;
        //        IRfcFunction FUNCION = repo.CreateFunction("ZGUARDA_SOLPED_SAM");
        //        IRfcTable tabla = FUNCION.GetTable("SOLPED");
        //        IRfcTable tabsr = FUNCION.GetTable("RSOLPED");
        //        IRfcTable tabls = FUNCION.GetTable("SERVICIOS");
        //        IRfcTable tablr = FUNCION.GetTable("RSERVICIOS");
        //        IRfcTable txtca = FUNCION.GetTable("TXT_CAB");
        //        IRfcTable txtpos = FUNCION.GetTable("TXT_POS");
        //        FUNCION.SetValue("CAB", cabs);
        //        FUNCION.SetValue("SER", sers);
        //        FUNCION.SetValue("CABTXT", txtC);
        //        FUNCION.SetValue("POSTXT", txtP);
        //        String m = FUNCION.GetValue("MENSAJE").ToString();

        //        if (cab.Count > 0)
        //        {
        //            foreach (SELECT_solped_crea_Fol_MDL_Result so in cab)
        //            {
        //                try
        //                {
        //                    tabla.Append();
        //                    tabla.SetValue("FOLIO_SAM", so.folio_sam);
        //                    tabla.SetValue("PREQ_ITEM", so.num_posicion_solped);
        //                    tabla.SetValue("ITEM_CAT", so.tipo_posicion_doc_compras);
        //                    tabla.SetValue("MATERIAL", so.num_material);
        //                    tabla.SetValue("DATUM", so.fecha);
        //                    tabla.SetValue("UZEIT", so.hora_dia);
        //                    tabla.SetValue("FOLIO_SAP", so.num_solped);
        //                    tabla.SetValue("DOC_TYPE", so.clase_documento_solped);
        //                    tabla.SetValue("ACCTASSCAT", so.tipo_imputacion);
        //                    tabla.SetValue("SHORT_TEXT", so.texto_breve);
        //                    tabla.SetValue("QUANTITY", so.cantidad_solped);
        //                    tabla.SetValue("UNIT", so.unidad_medida_solped);
        //                    tabla.SetValue("DEL_DATCAT", so.tipo_fecha);
        //                    tabla.SetValue("DELIV_DATE", so.fecha_entraga_posicion);
        //                    tabla.SetValue("MAT_GRP", so.grupo_articulos);
        //                    tabla.SetValue("PLANT", so.centro);
        //                    tabla.SetValue("STORE_LOC", so.almacen);
        //                    tabla.SetValue("PUR_GROUP", so.grupo_compras);
        //                    tabla.SetValue("PREQ_NAME", so.solicitante);
        //                    tabla.SetValue("PREQ_DATE", so.fecha_solicitud);
        //                    tabla.SetValue("DES_VENDOR", so.proveedor_deseado);
        //                    tabla.SetValue("LIFNR", so.num_cuenta_proveedor);
        //                    tabla.SetValue("TABIX", "0");
        //                    tabla.SetValue("EPSTP", so.tipo_posicion_doc_compras2);
        //                    tabla.SetValue("INFNR", so.num_registro_info_compras);
        //                    tabla.SetValue("EKORG", so.organizacion_compras);
        //                    tabla.SetValue("G_L_ACCT", so.num_cuenta_mayor);
        //                    tabla.SetValue("COST_CTR", so.centro_coste);
        //                    tabla.SetValue("KONNR", so.num_contrato_superior);
        //                    tabla.SetValue("KTPNR", so.num_posicion_contrato_superior);
        //                    tabla.SetValue("VRTKZ", so.indicador_distribucion_imputacion_multiple);
        //                    tabla.SetValue("SAKTO", so.num_cuenta_mayor2);
        //                    tabla.SetValue("AUFNR", so.num_orden);
        //                    tabla.SetValue("KOKRS", so.sociedad_co);
        //                    tabla.SetValue("KOSTL", so.centro_coste2);
        //                    tabla.SetValue("TEXTO_CAB", so.texto_cabecera);
        //                    tabla.SetValue("TEXTO_POS", so.texto_posicion);
        //                    tabla.SetValue("RECIBIDO", so.recibido);
        //                    tabla.SetValue("PROCESADO", so.procesado);
        //                    tabla.SetValue("MODIFICADO", so.modificado);
        //                    tabla.SetValue("ERROR", so.error);
        //                    tabla.SetValue("PREIS", so.precio_solped);
        //                    tabla.SetValue("WAERS", so.clave_moneda);
        //                    tabla.SetValue("UNAME", so.usuario);
        //                    tabla.SetValue("FECHA_MOD", so.fecha_ultima_modificacion);
        //                    tabla.SetValue("HORA_MOD", so.hora_ultima_modificacion);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        if (servicios.Count > 0)
        //        {
        //            foreach (SELECT_solped_servicios_crea_Fol_MDL_Result so in servicios)
        //            {
        //                try
        //                {
        //                    tabls.Append();
        //                    tabls.SetValue("FOLIO_SAM", so.folio_sam);
        //                    tabls.SetValue("PREQ_ITEM", so.num_posicion_solped);
        //                    tabls.SetValue("NUM_SER", so.num_posicion_solped2);
        //                    tabls.SetValue("SERVICE", so.num_servicio);
        //                    tabls.SetValue("DATUM", so.fecha);
        //                    tabls.SetValue("UZEIT", so.hora_dia);
        //                    tabls.SetValue("SHORT_TEXT", so.texto_breve);
        //                    tabls.SetValue("QUANTITY", so.cantidad);
        //                    tabls.SetValue("BASE_UOM", so.unidad_medida_base);
        //                    tabls.SetValue("PRICE_UNIT", so.cantidad_base);
        //                    tabls.SetValue("GR_PRICE", so.precio_bruto);
        //                    tabls.SetValue("MATL_GROUP", so.grupo_articulos);
        //                    tabls.SetValue("G_L_ACCT", so.num_cuenta_mayor);
        //                    tabls.SetValue("COST_CTR", so.centro_coste);
        //                    tabls.SetValue("AUFNR", so.num_orden);
        //                    tabls.SetValue("FOLIO_SAP", so.num_solped);
        //                    tabls.SetValue("RECIBIDO", so.recibido);
        //                    tabls.SetValue("PROCESADO", so.procesado);
        //                    tabls.SetValue("MODIFICADO", so.modificado);
        //                    tabls.SetValue("ERROR", so.error);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        if (txtcab.Count > 0)
        //        {
        //            foreach (SELECT_textos_cabecera_solped_Fol_MDL_Result tca in txtcab)
        //            {
        //                try
        //                {
        //                    txtca.Append();
        //                    txtca.SetValue("FOLIO_SAM", tca.folio_sam);
        //                    txtca.SetValue("INDICE", tca.indice);
        //                    txtca.SetValue("TDFORMAT", tca.columna_formato);
        //                    txtca.SetValue("TDLINE", tca.linea_texto);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        if (txtposi.Count > 0)
        //        {
        //            foreach (SELECT_textos_posiciones_solped_Fol_MDL_Result tpo in txtposi)
        //            {
        //                try
        //                {
        //                    txtpos.Append();
        //                    txtpos.SetValue("FOLIO_SAM", tpo.folio_sam);
        //                    txtpos.SetValue("PREQ_ITEM", tpo.num_posicion_solped);
        //                    txtpos.SetValue("INDICE", tpo.indice);
        //                    txtpos.SetValue("TDFORMAT", tpo.columna_formato);
        //                    txtpos.SetValue("TDLINE", tpo.linea_texto);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        try
        //        {
        //            FUNCION.Invoke(rfcDesti);
        //        }
        //        catch (Exception) { }

        //        foreach (IRfcStructure ln in tabsr)
        //        {
        //            SolPed_Crea s = new SolPed_Crea();
        //            try
        //            {
        //                s.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
        //                s.PREQ_ITEM = ln.GetValue("PREQ_ITEM").ToString();
        //                s.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
        //                DALC_Sol_Ped.ObtenerInstancia().ActualizaSolpedCrea(connection, s);
        //            }
        //            catch (Exception) { }
        //        }
        //        foreach (IRfcStructure ln in tablr)
        //        {
        //            SolPed_Serv s = new SolPed_Serv();
        //            try
        //            {
        //                s.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
        //                s.PREQ_ITEM = ln.GetValue("PREQ_ITEM").ToString();
        //                s.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
        //                DALC_Sol_Ped.ObtenerInstancia().ActualizaSolpedServ(connection, s);
        //            }
        //            catch (Exception) { }
        //        }
        //    }
        //}
        public string ObtenerUltimoRegistroConsumos()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_consumo_ultimo_registro_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalPosConsumos(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_posiciones_consumos_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public void P_ZPM_CONSUMOS_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroConsumos();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_consumos_datos_id_MDL_Result> datos = DALC_ConsumosSAM.ObtenerInstancia().ObtenerDatosIdConsumo(connection, id).ToList();
            foreach (SELECT_consumos_datos_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_consumos_valida_hora_MDL_Result> validahor = DALC_ConsumosSAM.ObtenerInstancia().ObtenerValidacionHoraConsumo(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_consumos_menos_MDL_Result> f = DALC_ConsumosSAM.ObtenerInstancia().ObtenerFolioMenosConsumo(connection, id).ToList();
                        foreach (SELEC_fol_consumos_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_consumos_MDL_Result> movs = DALC_ConsumosSAM.ObtenerInstancia().ObtenerTodoFolioConsumo(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_consumos_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosConsumos(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_consumos_MDL_Result> movs = DALC_ConsumosSAM.ObtenerInstancia().ObtenerTodoFolioConsumo(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_consumos_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosConsumos(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_consumos_MDL_Result> movs = DALC_ConsumosSAM.ObtenerInstancia().ObtenerTodoFolioConsumo(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_consumos_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosConsumos(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosConsumos(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_consumos_crea_Folio_MDL_Result> cabCons = DALC_ConsumosSAM.ObtenerInstancia().ObtenerCabConsumoFol(connection, folis).ToList();
            List<SELECT_posiciones_consumos_crea_Folio_MDL_Result> posCons = DALC_ConsumosSAM.ObtenerInstancia().ObtenerPosicionesConsumosFol(connection, folis).ToList();
            int CAB = cabCons.Count;
            int POS = posCons.Count;
            string total = ObtenerTotalPosConsumos(folis);
            if (total.Equals(posCons.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZPM_CONSUMOS_SAM_VS_SAP_BD");
                IRfcTable movCab = FUNCTION.GetTable("TSAM_MOV_CAB");
                IRfcTable RmovCab = FUNCTION.GetTable("RSAM_MOV_CAB");
                IRfcTable samMov = FUNCTION.GetTable("TSAM_MOV");
                IRfcTable RsamMov = FUNCTION.GetTable("RSAM_MOV");
                FUNCTION.SetValue("CAB", CAB);
                FUNCTION.SetValue("POS", POS);

                if (cabCons.Count > 0)
                {
                    foreach (SELECT_cabecera_consumos_crea_Folio_MDL_Result cc in cabCons)
                    {
                        try
                        {
                            movCab.Append();
                            movCab.SetValue("FOLIO_SAM", cc.folio_sam);
                            movCab.SetValue("FOLIO_ORD", cc.folio_orden_sam);
                            movCab.SetValue("AUFNR", cc.folio_orden_sap);
                            movCab.SetValue("UZEIT", cc.hora_dia);
                            movCab.SetValue("DATUM", cc.fecha);
                            movCab.SetValue("FOLIO_SAP", cc.folio_sap);
                            movCab.SetValue("WERKS", cc.centro);
                            movCab.SetValue("LGORT", cc.almacen);
                            movCab.SetValue("ANULA", cc.anula);
                            movCab.SetValue("TEXTO", cc.texto);
                            movCab.SetValue("TEXCAB", cc.texto_cab);
                            movCab.SetValue("TIPO", cc.tipo);
                            movCab.SetValue("UMLGO", cc.almacen_receptor_emisor);
                            movCab.SetValue("FACTURA", cc.factura);
                            movCab.SetValue("PEDIM", cc.campo_usuario_cluster_pc_nacional);
                            movCab.SetValue("LFSNR", cc.num_nota_entrega_externa);
                            movCab.SetValue("HU", cc.num_unidad_almacen);
                            movCab.SetValue("RECIBIDO", cc.recibido);
                            movCab.SetValue("PROCESADO", cc.procesado);
                            movCab.SetValue("ERROR", cc.error);
                            movCab.SetValue("FECHA_RECIBIDO", cc.fecha_recibido);
                            movCab.SetValue("HORA_RECIBIDO", cc.hora_recibido);
                            movCab.SetValue("UNAME", cc.usuario);
                            movCab.SetValue("FECHA_CONTABLE", cc.fecha_contable);
                        }
                        catch (Exception) { }
                    }
                }
                if (posCons.Count > 0)
                {
                    foreach (SELECT_posiciones_consumos_crea_Folio_MDL_Result pp in posCons)
                    {
                        try
                        {
                            samMov.Append();
                            samMov.SetValue("FOLIO_SAM", pp.folio_sam);
                            samMov.SetValue("FOLIO_ORD", pp.folio_orden_sam);
                            samMov.SetValue("AUFNR", pp.folio_orden_sap);
                            samMov.SetValue("UZEIT", pp.hora_dia);
                            samMov.SetValue("DATUM", pp.fecha);
                            samMov.SetValue("TABIX", pp.indice_registro_no_valido);
                            samMov.SetValue("FOLIO_SAP", pp.folio_sap);
                            samMov.SetValue("BWART", pp.clase_movimiento);
                            samMov.SetValue("ACTIVO", pp.activo);
                            samMov.SetValue("CHARG", pp.lote);
                            samMov.SetValue("CMEINS", pp.unidad_medida_base);
                            samMov.SetValue("CMENGE", pp.cantidad);
                            samMov.SetValue("ENMNG", pp.cantidad_tomada);
                            samMov.SetValue("ERFME", pp.unidad_medida_entrada);
                            //samMov.SetValue("ERFMG", pp.ERFMG);
                            samMov.SetValue("IDNRK", pp.componente_lista_materiales);
                            //samMov.SetValue("LCMENGE", pp.LCMENGE);
                            samMov.SetValue("LGORT", pp.almacen);
                            samMov.SetValue("LMEINS", pp.unidad_medida_base2);
                            //samMov.SetValue("LMENGE", pp.LMENGE);
                            samMov.SetValue("LTMEINS", pp.unidad_medida_base3);
                            //samMov.SetValue("LTMENGE", pp.LTMENGE);
                            samMov.SetValue("MAKTX", pp.texto_breve_material);
                            samMov.SetValue("MATNR", pp.num_material);
                            //samMov.SetValue("MENGE", pp.MENGE);
                            samMov.SetValue("RENGLON", pp.renglon);
                            samMov.SetValue("RSNUM", pp.num_reserva);
                            samMov.SetValue("RSPOS", pp.num_posicion_reserva);
                            samMov.SetValue("TMEINS", pp.unidad_medida_base4);
                            //samMov.SetValue("TMENGE", pp.TMENGE);
                            samMov.SetValue("UMLGO", pp.almacen_receptor);
                            samMov.SetValue("URSNUM", pp.num_reserva2);
                            samMov.SetValue("WERKS", pp.centro);
                            samMov.SetValue("EBELN", pp.num_doc_compras);
                            samMov.SetValue("EBELP", pp.num_posicion_doc_compras);
                            samMov.SetValue("SOBKZ", pp.indicador_stock_especial);
                            samMov.SetValue("LIFNR", pp.proveedor);
                            samMov.SetValue("UMWRK", pp.centro_receptor);
                            samMov.SetValue("KOSTL", pp.centro_coste);
                            samMov.SetValue("LFSNR", pp.num_nota_entrega_externa);
                            samMov.SetValue("FRBNR", pp.num_carta_porte_entrada_mercancias);
                            samMov.SetValue("EAN11_BME", pp.num_articulo_europeo_unidad_medida_pedido);
                            samMov.SetValue("EAN11_KON", pp.control_num_articulo_europeo);
                            samMov.SetValue("EANME", pp.unidad_medida_base5);
                            samMov.SetValue("LICHA", pp.num_lote_proveedor);
                            samMov.SetValue("SAKTO", pp.clase_coste);
                            samMov.SetValue("RECIBIDO", pp.recibido);
                            samMov.SetValue("PROCESADO", pp.procesado);
                            samMov.SetValue("ERROR", pp.error);
                            samMov.SetValue("FECHA_RECIBIDO", pp.fecha_recibido);
                            samMov.SetValue("HORA_RECIBIDO", pp.hora_recibido);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure ln in RmovCab)
                {
                    CabConsumosCrea cabconsumos = new CabConsumosCrea();
                    try
                    {
                        cabconsumos.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        cabconsumos.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_ConsumosSAM.ObtenerInstancia().ActualizaCabConsumosCrea(connection, cabconsumos);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure ln in RsamMov)
                {
                    PosConsumosCrea posconsumos = new PosConsumosCrea();
                    try
                    {
                        posconsumos.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        posconsumos.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_ConsumosSAM.ObtenerInstancia().ActualizaPosConsumosCrea(connection, posconsumos);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroEntrada()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_entradas_ultimo_reg_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalEntrada(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_entradas_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public string ObtenerTotalTxtEntradas(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_texto_entradas_fol_MDL(folio_sam, obj);
            string total = "";
            total = obj.Value.ToString();
            return total;
        }
        public void P_ZMM_ENTRADAS_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroEntrada();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_entrada_id_MDL_Result> datos = DALC_MMEntradasSam.ObtenerInstancia().ObtenerDatosIdEntrada(connection, id).ToList();
            foreach (SELEC_datos_entrada_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_entrada_valida_hora_MDL_Result> validahor = DALC_MMEntradasSam.ObtenerInstancia().ObtenerValidacionHoraEntrada(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_entrada_menos_MDL_Result> f = DALC_MMEntradasSam.ObtenerInstancia().ObtenerFolioMenosEntrada(connection, id).ToList();
                        foreach (SELEC_fol_entrada_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_entrada_MDL_Result> movs = DALC_MMEntradasSam.ObtenerInstancia().ObtenerTodoFolioEntrada(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_entrada_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosEntrada(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_entrada_MDL_Result> movs = DALC_MMEntradasSam.ObtenerInstancia().ObtenerTodoFolioEntrada(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_entrada_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosEntrada(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_entrada_MDL_Result> movs = DALC_MMEntradasSam.ObtenerInstancia().ObtenerTodoFolioEntrada(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_entrada_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosEntrada(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosEntrada(string folio_sam)
        {
            //Thread.Sleep(10000);
            string folis = folio_sam, doc_compras = "";
            List<SELECT_entrada_servicios_crea_Todo_MDL_Result> mov = DALC_MMEntradasSam.ObtenerInstancia().ObtenerEntradas(connection, folis).ToList();
            List<SELECT_textos_entrada_servicios_Fol_MDL_Result> texn = DALC_MMEntradasSam.ObtenerInstancia().ObtenerTextosEn(connection, folis).ToList();
            int cbs = mov.Count;
            string toten = ObtenerTotalEntrada(folis);
            string totxt = ObtenerTotalTxtEntradas(folis);
            if (toten.Equals(mov.Count.ToString()) && totxt.Equals(texn.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZMM_ENTRADAS_SAM_VS_SAP_BD_II");
                IRfcTable entra = FUNCTION.GetTable("TENTRADA");
                IRfcTable Rentra = FUNCTION.GetTable("RENTRADA");
                IRfcTable Text = FUNCTION.GetTable("TXTLIBRE");
                FUNCTION.SetValue("CAB", cbs);
                if (mov.Count > 0)
                {
                    foreach (SELECT_entrada_servicios_crea_Todo_MDL_Result en in mov)
                    {
                        try
                        {
                            entra.Append();
                            entra.SetValue("FOLIO_SAM", en.folio_sam);
                            doc_compras = en.num_doc_compras;
                            entra.SetValue("EBELN", en.num_doc_compras);
                            entra.SetValue("EBELP", en.num_posicion_doc_compras);
                            entra.SetValue("TABIX", en.indice_registro_no_valido);
                            entra.SetValue("WERKS", en.centro);
                            entra.SetValue("SERVICE", en.num_servicio);
                            entra.SetValue("QUANTITY", en.cantidad);
                            entra.SetValue("SHORTTEX", en.texto_breve);
                            entra.SetValue("GR_PRICE", en.precio_bruto);
                            entra.SetValue("PRICEUNI", en.cantidad_base);
                            entra.SetValue("REF_DOC_NO", en.texto_referencia);
                            entra.SetValue("DATUM", en.fecha);
                            entra.SetValue("BKTXT", en.texto_documento);
                            entra.SetValue("UZEIT", en.hora_dia);
                            entra.SetValue("FOLIO_SAP", en.folio_sap);
                            entra.SetValue("RECIBIDO", en.recibido);
                            entra.SetValue("PROCESADO", en.procesado);
                            entra.SetValue("ERROR", en.error);
                            entra.SetValue("EXTROW", en.posicion_servicio);
                            entra.SetValue("UNAME", en.usuario);
                            entra.SetValue("FECHA_CONTABLE", en.fecha_contable);
                            entra.SetValue("PWWE", en.nota_calidad_prestacion);
                            entra.SetValue("PWFR", en.nota_cumplim_prestacion);
                        }
                        catch (Exception) { }
                    }
                }
                if (texn.Count > 0)
                {
                    foreach (SELECT_textos_entrada_servicios_Fol_MDL_Result txt in texn)
                    {
                        try
                        {
                            Text.Append();
                            Text.SetValue("FOLIO_SAM", txt.folio_sam);
                            Text.SetValue("INDICE", txt.contador_posicion);
                            Text.SetValue("TDFORMAT", txt.formato);
                            Text.SetValue("TDLINE", txt.texto);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                if (FUNCTION.GetValue("ERROR").ToString().Trim().Equals("Correcto"))
                {
                    ZHIS_PEDIDOS_MAT(doc_compras);
                    DALC_MMEntradasSam.ObtenerInstancia().ElimanarRegistroCreacion(connection, folio_sam);
                }
                else
                {
                    DALC_MMEntradasSam.ObtenerInstancia().ActualizarResultado(connection, folio_sam, FUNCTION.GetValue("SHET_NO").ToString().Trim(), FUNCTION.GetValue("ERROR").ToString().Trim());
                }
            }
        }
        public bool ValidacionMov()
        {
            List<SELECT_movimientos_cabecera_crea_MDL_Result> fl = DALC_MM_MovMat.ObtenerInstancia().ObtenerListaFolios(connection).ToList();
            if (fl.Count > 0)
            {
                return true;
            }
            else { return false; }
        }
        public string ObtenerUltimoRegistro()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_mov_ultimo_reg_res_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalMovimientos(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_movimientos_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public bool P_ZMM_MOVMAT_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistro();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_mov_id_MDL_Result> datos = DALC_MM_MovMat.ObtenerInstancia().ObtenerDatosId(connection, id).ToList();
            if (datos.Count > 0)
            {
                foreach (SELEC_datos_mov_id_MDL_Result da in datos)
                {
                    folio_sam = da.folio_sam;
                    fechafol = da.fecha;
                    if (fechafol.Equals(fecha_Actual))
                    {
                        List<SELECT_mov_valida_hora_MDL_Result> validahor = DALC_MM_MovMat.ObtenerInstancia().ObtenerValidacionHora(connection, id, hor).ToList();
                        if (validahor.Count <= 0)
                        {
                            id = id - 1;
                            List<SELEC_fol_mov_menos_MDL_Result> f = DALC_MM_MovMat.ObtenerInstancia().ObtenerFolioMenos(connection, id).ToList();
                            if (f.Count > 0)
                            {
                                foreach (SELEC_fol_mov_menos_MDL_Result fo in f)
                                {
                                    folio_sam = fo.folio_sam;
                                    List<SELECT_lista_folios_mov_MDL_Result> movs = DALC_MM_MovMat.ObtenerInstancia().ObtenerTodoFolio(connection, folio_sam).ToList();
                                    foreach (SELECT_lista_folios_mov_MDL_Result m in movs)
                                    {
                                        folio_sam = m.folio_sam;
                                        MandarDatosMovimientos(folio_sam);
                                    }
                                }
                                return true;
                            }
                            else { return false; }
                        }
                        else
                        {
                            List<SELECT_lista_folios_mov_MDL_Result> movs = DALC_MM_MovMat.ObtenerInstancia().ObtenerTodoFolio(connection, folio_sam).ToList();
                            if (movs.Count > 0)
                            {
                                foreach (SELECT_lista_folios_mov_MDL_Result m in movs)
                                {
                                    folio_sam = m.folio_sam;
                                    MandarDatosMovimientos(folio_sam);
                                }
                                return true;
                            }
                            else { return false; }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_mov_MDL_Result> movs = DALC_MM_MovMat.ObtenerInstancia().ObtenerTodoFolio(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_mov_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            MandarDatosMovimientos(folio_sam);
                        }
                        return true;
                    }
                }
                return true;
            }
            else { return false; }
        }
        public void MandarDatosMovimientos(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_movimientos_cabecera_crea_Fol_MDL_Result> moc = DALC_MM_MovMat.ObtenerInstancia().ObtenerCabeceraMov(connection, folis).ToList();
            List<SELECT_movimientos_detalle_crea_Fol_MDL_Result> mov = DALC_MM_MovMat.ObtenerInstancia().ObtenerDetallesMov(connection, folis).ToList();
            int POSS = mov.Count;
            int CABS = moc.Count;
            string total = ObtenerTotalMovimientos(folis);
            if (total.Equals(mov.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCION = repo.CreateFunction("ZMM_MOVMAT_SAM_VS_SAP_BD");
                IRfcTable smov = FUNCION.GetTable("TSAM_MOV");
                IRfcTable movcab = FUNCION.GetTable("TSAM_MOV_CAB");
                IRfcTable Rmovcab = FUNCION.GetTable("RSAM_MOV_CAB");
                IRfcTable Rsamov = FUNCION.GetTable("RSAM_MOV");
                FUNCION.SetValue("CAB", CABS);
                FUNCION.SetValue("POS", POSS);

                if (mov.Count > 0)
                {
                    foreach (SELECT_movimientos_detalle_crea_Fol_MDL_Result mo in mov)
                    {
                        try
                        {
                            smov.Append();
                            smov.SetValue("FOLIO_SAM", mo.folio_sam);
                            smov.SetValue("UZEIT", mo.hora_dia);
                            smov.SetValue("DATUM", mo.fecha);
                            smov.SetValue("TABIX", mo.indice_registro_no_valido);
                            smov.SetValue("FOLIO_SAP", mo.num_doc_material);
                            smov.SetValue("BWART", mo.clase_movimiento);
                            smov.SetValue("ACTIVO", mo.indicador_posicion);
                            smov.SetValue("AUFNR", mo.num_orden);
                            smov.SetValue("CHARG", mo.num_lote);
                            smov.SetValue("CMEINS", mo.unidad_medida_base);
                            //smov.SetValue("CMENGE", mo.cantidad);
                            //smov.SetValue("ENMNG", mo.cantidad_toomada);
                            smov.SetValue("ERFME", mo.unidad_medida_entrada);
                            //smov.SetValue("ERFMG", mo.cantidad_unidad_medida_entrada);
                            smov.SetValue("IDNRK", mo.componente_lista_material);
                            smov.SetValue("LCMENGE", mo.cantidad1);
                            smov.SetValue("LGORT", mo.almacen);
                            smov.SetValue("LMEINS", mo.unidad_medida_base);
                            //smov.SetValue("LMENGE", mo.cantidad2);
                            smov.SetValue("LTMEINS", mo.unidad_medida_base2);
                            //smov.SetValue("LTMENGE", mo.cantidad3);
                            smov.SetValue("MAKTX", mo.texto_breve_material);
                            smov.SetValue("MATNR", mo.num_material);
                            //smov.SetValue("MENGE", mo.cantidad4);
                            smov.SetValue("RENGLON", mo.indicador_posicion2);
                            if (mo.num_reserva_necesidad_secundaria == "")
                            {
                                smov.SetValue("RSNUM", mo.num_reserva_necesidad_secundaria);
                            }
                            else
                            {
                                string re = mo.num_reserva_necesidad_secundaria.Substring(0, 2);
                                if (re == "RE")
                                {
                                    smov.SetValue("ZFOLIO_RES", mo.num_reserva_necesidad_secundaria);
                                }
                                else
                                {
                                    smov.SetValue("RSNUM", mo.num_reserva_necesidad_secundaria);
                                }
                            }

                            smov.SetValue("RSPOS", mo.num_posicion_reserva);
                            smov.SetValue("TMEINS", mo.unidad_medidabase3);
                            //smov.SetValue("TMENGE", mo.cantidad5);
                            smov.SetValue("UMLGO", mo.almacen_receptor);
                            smov.SetValue("URSNUM", mo.num_reserva_secund);
                            smov.SetValue("WERKS", mo.centro);
                            smov.SetValue("EBELN", mo.num_doc_compras);
                            smov.SetValue("EBELP", mo.num_posicion_doc_compras);
                            smov.SetValue("SOBKZ", mo.indicador_stock_especial);
                            smov.SetValue("LIFNR", mo.num_cuenta_proveedor);
                            smov.SetValue("UMWRK", mo.centro_receptor);
                            smov.SetValue("KOSTL", mo.centro_coste);
                            smov.SetValue("LFSNR", mo.num_nota_entrega_externa);
                            smov.SetValue("FRBNR", mo.num_carta_porte_entrada_merca);
                            smov.SetValue("EAN11_BME", mo.num_articulo_europeo_um_pedido);
                            smov.SetValue("EAN11_KON", mo.control_num_articulo_europeo);
                            smov.SetValue("EANME", mo.um_base);
                            smov.SetValue("LICHA", mo.num_lote_proveedor);
                            smov.SetValue("SAKTO", mo.clase_coste);
                            smov.SetValue("RECIBIDO", mo.recibido);
                            smov.SetValue("PROCESADO", mo.procesado);
                            smov.SetValue("ERROR", mo.error);
                            smov.SetValue("SPE_CRM_REF_SO", mo.folio_numdocmaterial);
                            smov.SetValue("SPE_CRM_REF_ITEM", mo.posicion_doc_material);
                        }
                        catch (Exception) { }
                    }
                }
                if (moc.Count > 0)
                {
                    foreach (SELECT_movimientos_cabecera_crea_Fol_MDL_Result moo in moc)
                    {
                        try
                        {
                            movcab.Append();
                            movcab.SetValue("FOLIO_SAM", moo.folio_sam);
                            movcab.SetValue("UZEIT", moo.hora_dia);
                            movcab.SetValue("DATUM", moo.fecha);
                            movcab.SetValue("FOLIO_SAP", moo.num_doc_material);
                            movcab.SetValue("WERKS", moo.centro);
                            movcab.SetValue("LGORT", moo.almacen);
                            movcab.SetValue("ANULA", moo.anula);
                            movcab.SetValue("TEXTO", moo.texto);
                            movcab.SetValue("TEXCAB", moo.texto_cabecera);
                            movcab.SetValue("TIPO", moo.tipo);
                            movcab.SetValue("UMLGO", moo.almacen_receptor);
                            movcab.SetValue("FACTURA", moo.factura);
                            movcab.SetValue("PEDIM", moo.campo_usuario_cluster_pcnacional);
                            movcab.SetValue("LFSNR", moo.num_nota_entrega_externa);
                            movcab.SetValue("HU", moo.num_unidad_almacen);
                            movcab.SetValue("RECIBIDO", moo.recibido);
                            movcab.SetValue("PROCESADO", moo.procesado);
                            movcab.SetValue("ERROR", moo.error);
                            movcab.SetValue("UNAME", moo.usuario);
                            movcab.SetValue("FECHA_CONTABLE", moo.fecha_contable);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCION.Invoke(rfcDesti);
                }
                catch (Exception) { }

                foreach (IRfcStructure ln in Rmovcab)
                {
                    Mov_cabecera_crea ms = new Mov_cabecera_crea();
                    try
                    {
                        ms.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        ms.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_MM_MovMat.ObtenerInstancia().ActualizaMov_cabecera_crea(connection, ms);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure ln in Rsamov)
                {
                    Mov_detalle_crea d = new Mov_detalle_crea();
                    try
                    {
                        d.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        d.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_MM_MovMat.ObtenerInstancia().ActualizaMov_detalles_crea(connection, d);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroReserva()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_reservas_ultimo_reg_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalReservas(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_reservas_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZMM_RESERVAS_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 1;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroReserva();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_reserva_id_MDL_Result> datos = DALC_Reservas.ObtenerInstancia().ObtenerDatosIdReserva(connection, id).ToList();
            foreach (SELEC_datos_reserva_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_reservas_valida_hora_MDL_Result> validahor = DALC_Reservas.ObtenerInstancia().ObtenerValidacionHoraReserva(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_reserva_menos_MDL_Result> f = DALC_Reservas.ObtenerInstancia().ObtenerFolioMenosReserva(connection, id).ToList();
                        foreach (SELEC_fol_reserva_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_reservas_MDL_Result> movs = DALC_Reservas.ObtenerInstancia().ObtenerTodoFolioReserva(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_reservas_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosReservas(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_reservas_MDL_Result> movs = DALC_Reservas.ObtenerInstancia().ObtenerTodoFolioReserva(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_reservas_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosReservas(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_reservas_MDL_Result> movs = DALC_Reservas.ObtenerInstancia().ObtenerTodoFolioReserva(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_reservas_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosReservas(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosReservas(string folio_sam)
        {
            //Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_reserva_cabecera_crea_Fol_MDL_Result> re = DALC_Reservas.ObtenerInstancia().ObtenerReservaCab(connection, folis).ToList();
            List<SELECT_reserva_posiciones_crea_Fol_MDL_Result> rv = DALC_Reservas.ObtenerInstancia().ObtenerReservaPos(connection, folis).ToList();
            int cass = re.Count;
            int RP = rv.Count;
            string total = ObtenerTotalReservas(folis);
            if (total.Equals(rv.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCION = repo.CreateFunction("ZMM_RESERVAS_SAM_VS_SAP_BD_II");
                IRfcTable tabla = FUNCION.GetTable("TRESERVA_CAB");
                IRfcTable tabsr = FUNCION.GetTable("TRESERVA");
                IRfcTable tabls = FUNCION.GetTable("RRESERVA_CAB");
                IRfcTable tablr = FUNCION.GetTable("RRESERVA");
                FUNCION.SetValue("CAB", cass);
                FUNCION.SetValue("POS", RP);

                if (rv.Count > 0)
                {
                    foreach (SELECT_reserva_posiciones_crea_Fol_MDL_Result rp in rv)
                    {
                        tabsr.Append();
                        tabsr.SetValue("FOLIO_SAM", rp.folio_sam);
                        tabsr.SetValue("RSNUM", rp.posicion_reserva);
                        tabsr.SetValue("MATNR", rp.num_material);
                        tabsr.SetValue("WERKS", rp.centro);
                        tabsr.SetValue("LGORT", rp.almacen);
                        tabsr.SetValue("BDMNG", rp.cantidad_necesaria);
                        tabsr.SetValue("MEINS", rp.unidad_medida_base);
                        tabsr.SetValue("KOSTL", rp.centro_coste);
                        tabsr.SetValue("AUFNR", rp.num_orden);
                        tabsr.SetValue("BWART", rp.clase_movimiento);
                        tabsr.SetValue("SGTXT", rp.texto_posicion);
                        tabsr.SetValue("RECIBIDO", rp.recibido);
                        tabsr.SetValue("PROCESADO", rp.procesado);
                        tabsr.SetValue("ERROR", rp.error);
                        tabsr.SetValue("UMLGO", rp.almacen_destino);
                    }
                }
                if (re.Count > 0)
                {
                    foreach (SELECT_reserva_cabecera_crea_Fol_MDL_Result rc in re)
                    {
                        try
                        {
                            tabla.Append();
                            tabla.SetValue("FOLIO_SAM", rc.folio_sam);
                            tabla.SetValue("FOLIO_SAP", rc.folio_sap);
                            tabla.SetValue("DATUM", rc.fecha);
                            tabla.SetValue("UZEIT", rc.hora_dia);
                            tabla.SetValue("WERKS", rc.centro);
                            tabla.SetValue("BWART", rc.clase_movimiento);
                            tabla.SetValue("LGORT", rc.almacen);
                            tabla.SetValue("KOSTL", rc.centro_coste);
                            tabla.SetValue("AUFNR", rc.num_orden);
                            tabla.SetValue("RECIBIDO", rc.recibido);
                            tabla.SetValue("PROCESADO", rc.procesado);
                            tabla.SetValue("ERROR", rc.error);
                            tabla.SetValue("UMLGO", rc.almacen_destino);
                            tabla.SetValue("UNAME", rc.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                if (FUNCION.GetValue("MENSAJE_R").ToString().Trim().Equals("Correcto"))
                {
                    ZRESERVAS_SAM(FUNCION.GetValue("FOLIO_SAP").ToString().Trim());
                    DALC_Reservas.ObtenerInstancia().ElimanarRegistroCreacion(connection, folio_sam);
                }
                else
                {
                    DALC_Reservas.ObtenerInstancia().ActualizarResultado(connection, folio_sam, FUNCION.GetValue("FOLIO_SAP").ToString().Trim(), FUNCION.GetValue("MENSAJE_R").ToString().Trim());
                }
            }
        }
        //public void EnviarDatosReservas(string folio_sam)
        //{
        //    Thread.Sleep(10000);
        //    string folis = folio_sam;
        //    List<SELECT_reserva_cabecera_crea_Fol_MDL_Result> re = DALC_Reservas.ObtenerInstancia().ObtenerReservaCab(connection, folis).ToList();
        //    List<SELECT_reserva_posiciones_crea_Fol_MDL_Result> rv = DALC_Reservas.ObtenerInstancia().ObtenerReservaPos(connection, folis).ToList();
        //    int cass = re.Count;
        //    int RP = rv.Count;
        //    string total = ObtenerTotalReservas(folis);
        //    if (total.Equals(rv.Count.ToString()))
        //    {
        //        RfcRepository repo = rfcDesti.Repository;
        //        IRfcFunction FUNCION = repo.CreateFunction("ZMM_RESERVAS_SAM_VS_SAP_BD");
        //        IRfcTable tabla = FUNCION.GetTable("TRESERVA_CAB");
        //        IRfcTable tabsr = FUNCION.GetTable("TRESERVA");
        //        IRfcTable tabls = FUNCION.GetTable("RRESERVA_CAB");
        //        IRfcTable tablr = FUNCION.GetTable("RRESERVA");
        //        FUNCION.SetValue("CAB", cass);
        //        FUNCION.SetValue("POS", RP);

        //        if (rv.Count > 0)
        //        {
        //            foreach (SELECT_reserva_posiciones_crea_Fol_MDL_Result rp in rv)
        //            {
        //                tabsr.Append();
        //                tabsr.SetValue("FOLIO_SAM", rp.folio_sam);
        //                tabsr.SetValue("RSNUM", rp.posicion_reserva);
        //                tabsr.SetValue("MATNR", rp.num_material);
        //                tabsr.SetValue("WERKS", rp.centro);
        //                tabsr.SetValue("LGORT", rp.almacen);
        //                tabsr.SetValue("BDMNG", rp.cantidad_necesaria);
        //                tabsr.SetValue("MEINS", rp.unidad_medida_base);
        //                tabsr.SetValue("KOSTL", rp.centro_coste);
        //                tabsr.SetValue("AUFNR", rp.num_orden);
        //                tabsr.SetValue("BWART", rp.clase_movimiento);
        //                tabsr.SetValue("SGTXT", rp.texto_posicion);
        //                tabsr.SetValue("RECIBIDO", rp.recibido);
        //                tabsr.SetValue("PROCESADO", rp.procesado);
        //                tabsr.SetValue("ERROR", rp.error);
        //                tabsr.SetValue("UMLGO", rp.almacen_destino);
        //            }
        //        }
        //        if (re.Count > 0)
        //        {
        //            foreach (SELECT_reserva_cabecera_crea_Fol_MDL_Result rc in re)
        //            {
        //                try
        //                {
        //                    tabla.Append();
        //                    tabla.SetValue("FOLIO_SAM", rc.folio_sam);
        //                    tabla.SetValue("FOLIO_SAP", rc.folio_sap);
        //                    tabla.SetValue("DATUM", rc.fecha);
        //                    tabla.SetValue("UZEIT", rc.hora_dia);
        //                    tabla.SetValue("WERKS", rc.centro);
        //                    tabla.SetValue("BWART", rc.clase_movimiento);
        //                    tabla.SetValue("LGORT", rc.almacen);
        //                    tabla.SetValue("KOSTL", rc.centro_coste);
        //                    tabla.SetValue("AUFNR", rc.num_orden);
        //                    tabla.SetValue("RECIBIDO", rc.recibido);
        //                    tabla.SetValue("PROCESADO", rc.procesado);
        //                    tabla.SetValue("ERROR", rc.error);
        //                    tabla.SetValue("UMLGO", rc.almacen_destino);
        //                    tabla.SetValue("UNAME", rc.usuario);
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //        try
        //        {
        //            FUNCION.Invoke(rfcDesti);
        //        }
        //        catch (Exception) { }
        //        foreach (IRfcStructure ln in tabls)
        //        {
        //            ReservaCab s = new ReservaCab();
        //            try
        //            {
        //                s.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
        //                s.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
        //                DALC_Reservas.ObtenerInstancia().ActualizaReservaCab(connection, s);
        //            }
        //            catch (Exception) { }
        //        }
        //        foreach (IRfcStructure ln in tablr)
        //        {
        //            ReservaPos s = new ReservaPos();
        //            try
        //            {
        //                s.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
        //                s.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
        //                DALC_Reservas.ObtenerInstancia().ActualizaReservaPos(connection, s);
        //            }
        //            catch (Exception) { }
        //        }
        //    }
        //}
        public string ObtenerUltimoRegistroOrden()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_orden_ultimo_registro_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalOperaciones(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_operaciones_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public string ObtenerTotalServiciosOrdenes(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_servicios_ordenes_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public string ObtenerTotalMaterialesOrdenes(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_materiales_ordenes_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public string ObtenerTotalTextosOrdenes(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_textos_ordenes_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZPM_ORDENESPM_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroOrden();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_ordenes_datos_id_MDL_Result> datos = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerDatosIdOrden(connection, id).ToList();
            foreach (SELECT_ordenes_datos_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_ordenes_valida_hora_MDL_Result> validahor = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerValidacionHoraOrden(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_ordenes_menos_MDL_Result> f = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerFolioMenosOrden(connection, id).ToList();
                        foreach (SELEC_fol_ordenes_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_ordenes_MDL_Result> movs = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerTodoFolioOrden(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_ordenes_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosOrdenes(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_ordenes_MDL_Result> movs = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerTodoFolioOrden(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_ordenes_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosOrdenes(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_ordenes_MDL_Result> movs = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerTodoFolioOrden(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_ordenes_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosOrdenes(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosOrdenes(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_operaciones_ordenes_crea_Folio_MDL_Result> mov = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerOperacionesFolio(connection, folis).ToList();
            List<SELECT_servicios_ordenes_crea_Folio_MDL_Result> ser = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerServiciosFolio(connection, folis).ToList();
            List<SELECT_materiales_ordenes_crea_Folio_MDL_Result> mat = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerMaterialesFolio(connection, folis).ToList();
            List<SELECT_cabecera_ordenes_crea_Folio_MDL_Result> cab = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerCabFolio(connection, folis).ToList();
            List<SELECT_texto_posicion_ordenes_crea_Folio_MDL_Result> txt = DALC_OrdenesPMBD.ObtenerInstancia().ObtenerTextoPosFolio(connection, folis).ToList();
            int Ca = cab.Count;
            int Op = mov.Count;
            int Se = ser.Count;
            int Ma = mat.Count;
            string totop = ObtenerTotalOperaciones(folis);
            string totse = ObtenerTotalServiciosOrdenes(folis);
            string totma = ObtenerTotalMaterialesOrdenes(folis);
            string tottx = ObtenerTotalTextosOrdenes(folis);
            if (totop.Equals(mov.Count.ToString()) && totse.Equals(ser.Count.ToString()) && totma.Equals(mat.Count.ToString()) && tottx.Equals(txt.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZPM_ORDENESPM_SAM_VS_SAP_BD");
                IRfcTable pmop = FUNCTION.GetTable("TSAM_PM_OP");
                IRfcTable esll = FUNCTION.GetTable("TSAM_ESLL");
                IRfcTable alm = FUNCTION.GetTable("TSAM_MATERIALES");
                IRfcTable ord = FUNCTION.GetTable("TSAM_ORDEN_CAB");
                IRfcTable Rord = FUNCTION.GetTable("RSAM_ORDEN_CAB");
                IRfcTable texPos = FUNCTION.GetTable("TXT_POS");
                FUNCTION.SetValue("CAB", Ca);
                FUNCTION.SetValue("OPE", Op);
                FUNCTION.SetValue("SER", Se);
                FUNCTION.SetValue("MAT", Ma);
                if (mov.Count > 0)
                {
                    foreach (SELECT_operaciones_ordenes_crea_Folio_MDL_Result mo in mov)
                    {
                        try
                        {
                            pmop.Append();
                            pmop.SetValue("FOLIO_SAM", mo.folio_sam);
                            pmop.SetValue("AUFNR", mo.num_orden);
                            pmop.SetValue("AUFPL", mo.num_hoja_ruta_operaciones_orden);
                            pmop.SetValue("APLZL", mo.contador_general_orden);
                            pmop.SetValue("UZEIT", mo.hora_dia);
                            pmop.SetValue("DATUM", mo.fecha);
                            //pmop.SetValue("TABIX", mo.indice_registro_no_valido);
                            pmop.SetValue("PLNAL", mo.contador_grupo_hoja_ruta);
                            pmop.SetValue("PLNTY", mo.tipo_grupo_hoja_ruta);
                            //pmop.SetValue("VINTV", mo.incremento_operaciones_referenciadas);
                            pmop.SetValue("PLNNR", mo.clave_grupo_hoja_ruta);
                            pmop.SetValue("ZAEHL", mo.contador_interno);
                            pmop.SetValue("VORNR", mo.num_operacion);
                            pmop.SetValue("STEUS", mo.clave_control);
                            pmop.SetValue("ARBID", mo.id_objeto_recurso);
                            pmop.SetValue("WERKS", mo.centro);
                            pmop.SetValue("LTXA1", mo.texto_breve_operacion);
                            pmop.SetValue("BMSCH", mo.cantidad_base);
                            pmop.SetValue("DAUNO", mo.duracion_operacion);
                            pmop.SetValue("DAUNE", mo.unidad_duracion_normal);
                            //pmop.SetValue("ARBEI", mo.trabajo_operacion); //no mandar vacio
                            pmop.SetValue("ARBEH", mo.unidad_trabajo);
                            pmop.SetValue("MGVRG", mo.cantidad_operacion);
                            //pmop.SetValue("ISM01", mo.actividad_ya_notificada1);//no mandar vacio
                            //pmop.SetValue("ISM02", mo.actividad_ya_notificada2);
                            //pmop.SetValue("ISM03", mo.actividad_ya_notificada3);
                            //pmop.SetValue("ISM04", mo.actividad_ya_notificada4);
                            //pmop.SetValue("ISM05", mo.actividad_ya_notificada5);
                            //pmop.SetValue("ISM06", mo.actividad_ya_notificada6);
                            pmop.SetValue("ILE01", mo.unidad_medida_actividad_notificar1);
                            pmop.SetValue("ILE02", mo.unidad_medida_actividad_notificar2);
                            pmop.SetValue("ILE03", mo.unidad_medida_actividad_notificar3);
                            pmop.SetValue("ILE04", mo.unidad_medida_actividad_notificar4);
                            pmop.SetValue("ILE05", mo.unidad_medida_actividad_notificar5);
                            pmop.SetValue("ILE06", mo.unidad_medida_actividad_notificar6);
                            pmop.SetValue("MEINH", mo.unidad_medida_operacion);
                            pmop.SetValue("AUERU", mo.indicador_valor_predeterminado_trabajo_relevante);
                            pmop.SetValue("BANFN", mo.num_solped);
                            pmop.SetValue("BNFPO", mo.num_posicion_solped__orden);
                            pmop.SetValue("EKORG", mo.organizacion_compras);
                            pmop.SetValue("EKGRP", mo.grupo_compras_actividad_trabajo_externa);
                            pmop.SetValue("MATKL", mo.grupo_articulos);
                            pmop.SetValue("LIFNR", mo.num_cuenta_proveedor);
                            pmop.SetValue("PREIS", mo.precio);
                            pmop.SetValue("PEINH", mo.cantidad_base2);
                            pmop.SetValue("WAERS", mo.clave_moneda);
                            pmop.SetValue("SAKTO", mo.clase_coste);
                            pmop.SetValue("AFNAM", mo.solicitante);
                            pmop.SetValue("RUECK", mo.num_notificacion_operacion);
                        }
                        catch (Exception) { }
                    }
                }

                if (ser.Count > 0)
                {
                    foreach (SELECT_servicios_ordenes_crea_Folio_MDL_Result se in ser)
                    {
                        try
                        {
                            esll.Append();
                            esll.SetValue("FOLIO_SAM", se.folio_sam);
                            esll.SetValue("VORNR", se.num_operacion);
                            esll.SetValue("PACKNO", se.num_paquete);
                            esll.SetValue("INTROW", se.num_linea1);
                            esll.SetValue("EXTROW", se.num_linea2);
                            esll.SetValue("DEL", se.indicador_borrado);
                            esll.SetValue("SRVPOS", se.num_servicio);
                            //esll.SetValue("RANG", se.nivel_jerarquico_grupo);
                            esll.SetValue("EXTGROUP", se.nivel_estructura);
                            esll.SetValue("PAQUETE", se.asignacion_servicio);
                            esll.SetValue("SUB_PACKNO", se.num_subpaquete);
                            esll.SetValue("LBNUM", se.denominacion_ambito_servicio);
                            esll.SetValue("AUSGB", se.edicion_ambito_servicio);
                            esll.SetValue("STLVPOS", se.posicion_catalogo_prestaciones_estandar);
                            esll.SetValue("EXTSRVNO", se.num_servicio_proveedor);
                            esll.SetValue("MENGE", se.cantidad_con_signo);
                            esll.SetValue("MEINS", se.unidad_medida_base);
                            //esll.SetValue("UEBTO", se.tolerancia_sobrecumplimiento);
                            esll.SetValue("UEBTK", se.sobrecumplimiento_ilimitado);
                            esll.SetValue("WITH_LIM", se.busqueda_limites);
                            esll.SetValue("SPINF", se.actualizar_condiciones);
                            //esll.SetValue("PEINH", se.cantidad_base);
                            //esll.SetValue("BRTWR", se.precio_bruto);
                            //esll.SetValue("NETWR", se.valor_neto_posicion);
                            esll.SetValue("FROMPOS", se.limite_inferior);
                            esll.SetValue("TOPOS", se.limite_superior);
                            esll.SetValue("KTEXT1", se.texto_breve);
                            esll.SetValue("VRTKZ", se.indicador_distribucion_imputacion_multiple);
                            esll.SetValue("TWRKZ", se.indicador_factura_parcial);
                            esll.SetValue("PERNR", se.num_personal);
                            esll.SetValue("MOLGA", se.agrupacion_paises);
                            esll.SetValue("LGART", se.cc_nomina);
                            esll.SetValue("LGTXT", se.texto_explicativo_cc_nomina);
                            esll.SetValue("STELL", se.funcion);
                            esll.SetValue("IFTNR", se.num_actual_tablas_interfaces);
                            esll.SetValue("BUDAT", se.fecha_contabilizacion_doc);
                            esll.SetValue("INSDT", se.fecha_activado_registro_tabla);
                            esll.SetValue("PLN_PACKNO", se.num_paquete_original);
                            esll.SetValue("PLN_INTROW", se.entrada_linea_paquete_plan);
                            esll.SetValue("KNT_PACKNO", se.entrada_no_planif_pedido);
                            esll.SetValue("KNT_INTROW", se.entrada_no_planif_contr);
                            esll.SetValue("TMP_PACKNO", se.entrada_servicio_no_planificado_modelo);
                            esll.SetValue("TMP_INTROW", se.entrada_servicio_no_planificado_cp_modelo);
                            esll.SetValue("STLV_LIM", se.linea_servicio_refiere_limites_cpe);
                            esll.SetValue("LIMIT_ROW", se.entrada_no_plan_linea_limite);
                            //esll.SetValue("ACT_MENGE", se.pedido_cantidad_entrada);
                            //esll.SetValue("ACT_WERT", se.valor_registrado);
                            //esll.SetValue("KNT_WERT", se.pedido_abierto_valor_ordenado);
                            //esll.SetValue("KNT_MENGE", se.pedido_abierto_cantidad_ordenada);
                            //esll.SetValue("ZIELWERT", se.valor_previsto);
                            //esll.SetValue("UNG_WERT", se.pedido_abierto_valor_llamado_no_planif);
                            //esll.SetValue("UNG_MENGE", se.pedido_abierto_cantidad_ordenada_forma_no_plan);
                            esll.SetValue("ALT_INTROW", se.alternativa_nota_posicion_base);
                            esll.SetValue("BASIC", se.linea_base);
                            esll.SetValue("ALTERNAT", se.linea_alternativa);
                            esll.SetValue("BIDDER", se.linea_licitante);
                            esll.SetValue("SUPPLE", se.linea_suplementaria);
                            esll.SetValue("FREEQTY", se.linea_cantidad_libre);
                            esll.SetValue("INFORM", se.linea_informativa);
                            esll.SetValue("PAUSCH", se.linea_global);
                            esll.SetValue("EVENTUAL", se.linea_reserva);
                            esll.SetValue("MWSKZ", se.indicador_iva);
                            esll.SetValue("TXJCD", se.domicilio_fiscal);
                            esll.SetValue("PRS_CHG", se.modificacion_precio_hoja_entrada);
                            esll.SetValue("MATKL", se.grupo_articulos);
                            //esll.SetValue("TBTWR", se.precio_bruto2);
                            //esll.SetValue("NAVNW", se.iva_soportado_no_deducible);
                            //esll.SetValue("BASWR", se.importe_base_impuesto);
                            esll.SetValue("KKNUMV", se.num_condicion_doc);
                            esll.SetValue("IWEIN", se.unidad_trabajo);
                            //esll.SetValue("INT_WORK", se.trabajo_interno);
                            esll.SetValue("EXTERNALID", se.clave_referencia_srm);
                            esll.SetValue("KSTAR", se.clase_coste);
                            //esll.SetValue("ACT_WORK", se.trabajo_interno2);
                            esll.SetValue("MAPNO", se.cpo_asignacion);
                            esll.SetValue("SRVMAPKEY", se.clave_posicion_mensaje_esoa);
                            esll.SetValue("TAXTARIFFCODE", se.tax_tariff_code);
                            esll.SetValue("SDATE", se.fecha);
                            esll.SetValue("BEGTIME", se.hora_inicio);
                            esll.SetValue("ENDTIME", se.hora_final);
                            esll.SetValue("PERSEXT", se.num_externo_personal);
                            esll.SetValue("CATSCOUNTE", se.contador_registro_entrada_tiempos);
                            esll.SetValue("STOKZ", se.indicador_doc_anulado);
                            esll.SetValue("BELNR", se.num_doc);
                            esll.SetValue("FORMELNR", se.num_formula);
                            //esll.SetValue("FRMVAL1", se.valor_formula1);
                            //esll.SetValue("FRMVAL2", se.valor_formula2);
                            //esll.SetValue("FRMVAL3", se.valor_formula3);
                            //esll.SetValue("FRMVAL4", se.valor_formula4);
                            //esll.SetValue("FRMVAL5", se.valor_formula5);
                            esll.SetValue("USERF1_NUM", se.campo_personalizado1);
                            //esll.SetValue("USERF2_NUM", se.campo_personalizado2);
                            esll.SetValue("USERF1_TXT", se.campo_personalizado3);
                            esll.SetValue("USERF2_TXT", se.campo_personalizado4);
                            esll.SetValue("KNOBJ", se.num_objeto_modulo_relaciones_asignado);
                            esll.SetValue("CHGTEXT", se.modificacion_texto_breve_permitida);
                            esll.SetValue("KALNR", se.num_calculo_coste);
                            esll.SetValue("KLVAR", se.variante_cal_coste);
                            esll.SetValue("EXTDES", se.identificacion_lines);
                            esll.SetValue("BOSINTER", se.linea_interna);
                            esll.SetValue("BOSGRP", se.grupo_subcontratista);
                            esll.SetValue("BOS_RISK", se.linea_riesgo);
                            esll.SetValue("BOS_ECP", se.casilla_seleccion);
                            esll.SetValue("CHGLTEXT", se.modificacion_texto_explicativo);
                            esll.SetValue("BOSGRUPPENR", se.num_grupo_numero_asignacion_ejecucion);
                            esll.SetValue("BOSLFDNR", se.num_actual_num_asignacion_ejecucion);
                            esll.SetValue("BOSDRUKZ", se.indicador_immpresion);
                            esll.SetValue("BOSSUPPLENO", se.num_suplementario);
                            esll.SetValue("BOSSUPPLESTATUS", se.status_suplemento);
                            esll.SetValue("/SAPBOQ/OBJTYPE", se.tipo_objeto_linea_catalogo_servicios);
                            esll.SetValue("/SAPBOQ/SPOSNR", se.num_subposicion);
                            esll.SetValue("/SAPBOQ/MINTROW", se.num_linea);
                            esll.SetValue("/SAPBOQ/QT_REL", se.entrada_servicio_permitida);
                            esll.SetValue("/SAPBOQ/CK_QTY", se.tamano_lote_calculo_costes);
                            esll.SetValue("/SAPBOQ/M_FRATE", se.precio_global_posicion_principal_definida);
                            esll.SetValue("EXTREFKEY", se.clave_referencia_externa_prestacion);
                            //esll.SetValue("INV_MENGE", se.pedido_cantidad_registrada_factura);
                            esll.SetValue("PER_SDATE", se.fecha_inicio_periodo_prestacion_servicios);
                            esll.SetValue("PER_EDATE", se.fecha_fin_periodo_prestacion_servicios);
                            esll.SetValue("GL_ACCOUNT", se.num_cuenta_mayor);
                        }
                        catch (Exception) { }
                    }
                }
                if (mat.Count > 0)
                {
                    foreach (SELECT_materiales_ordenes_crea_Folio_MDL_Result ma in mat)
                    {
                        try
                        {
                            alm.Append();
                            alm.SetValue("FOLIO_SAM", ma.folio_sam);
                            alm.SetValue("UZEIT", ma.hora_dia);
                            alm.SetValue("DATUM", ma.fecha);
                            //alm.SetValue("TABIX", ma.indice_registro_no_valido);
                            alm.SetValue("RESERV_NO", ma.num_reserva);
                            alm.SetValue("RES_ITEM", ma.num_posicion_reserva);
                            alm.SetValue("RES_TYPE", ma.clase_registro);
                            alm.SetValue("MOVEMENT", ma.movimiento_mercancia_permitido_reserva);
                            alm.SetValue("WITHDRAWN", ma.salida_final_reserva);
                            alm.SetValue("MATERIAL", ma.num_material);
                            alm.SetValue("PLANT", ma.centro);
                            alm.SetValue("STGE_LOC", ma.almacen);
                            alm.SetValue("BATCH", ma.lote);
                            alm.SetValue("FIXED_QUAN", ma.cantidad_fija);
                            alm.SetValue("CURRENCY", ma.clave_moneda);
                            alm.SetValue("CURRENCY_ISO", ma.codigo_iso_moneda);
                            alm.SetValue("SALES_ORD", ma.num_pedido_cliente);
                            alm.SetValue("S_ORD_ITEM", ma.num_posicion_pedido_cliente);
                            alm.SetValue("GL_ACCOUNT", ma.num_cuenta_mayor);
                            alm.SetValue("ORIGINAL_QUANTITY", ma.cantidad_emplear);
                            alm.SetValue("ITEM_CAT", ma.tipo_posicion);
                            alm.SetValue("ITEM_NUMBER", ma.num_posicion_lista_materiales);
                            alm.SetValue("ITEM_TEXT1", ma.texto_posicion_lista_materiales);
                            alm.SetValue("COST_RELEVANT", ma.indicador_relevancia_calculo_coste);
                            //alm.SetValue("USAGE_PROB", ma.posicion_alternativa_probabilidad_empleo);
                            alm.SetValue("SORT_STRING", ma.concepto_clas);
                            alm.SetValue("BULK_MAT", ma.indicador_material_granel);
                            alm.SetValue("MAT_PROVISION", ma.indicador_pieza_facilitada);
                            alm.SetValue("WBS_ELEM", ma.elemento_pep);
                            alm.SetValue("ACTIVITY", ma.num_operacion);
                            //alm.SetValue("PRICE", ma.precio_moneda_componente);
                            //alm.SetValue("PRICE_UNIT", ma.cantidad_base);
                            alm.SetValue("BACKFLUSH", ma.indicador_toma_retroactiva);
                            alm.SetValue("PUR_GROUP", ma.grupo_compras);
                            //alm.SetValue("DELIVERY_DAYS", ma.plazo_entrega_dias);
                            alm.SetValue("GR_RCPT", ma.destinatario_mercancias);
                            alm.SetValue("UNLOAD_PT", ma.puesto_descarga);
                            alm.SetValue("MATL_GROUP", ma.grupo_articulos);
                            //alm.SetValue("GR_PR_TIME", ma.timepo_tratamiento_entrada_mercancia_dias);
                            alm.SetValue("VENDOR_NO", ma.num_cuenta_proveedor);
                            alm.SetValue("INFO_REC", ma.num_registro_info_compras);
                            //alm.SetValue("LEAD_TIME_OFFSET_OPR", ma.decalaje_operacion);
                            alm.SetValue("LEAD_TIME_OFFSET_OPR_UNIT", ma.unidad_decalaje_operacion);
                            alm.SetValue("LEAD_TIME_OFFSET_OPR_UNIT_ISO", ma.codigo_iso_unidad_medida);
                            alm.SetValue("PREQ_NAME", ma.solicitante);
                            alm.SetValue("TRACKINGNO", ma.num_necesidad);
                            alm.SetValue("PURCH_ORG", ma.organizacion_compras);
                            alm.SetValue("MATL_DESC", ma.texto_breve_material);
                            //alm.SetValue("REQUIREMENT_QUANTITY", ma.cantidad_necesaria_componente2);
                            alm.SetValue("REQUIREMENT_QUANTITY_UNIT", ma.unidad_medida_base);
                            alm.SetValue("REQUIREMENT_QUANTITY_UNIT_ISO", ma.codigo_iso_unidad_de_medida);
                            alm.SetValue("AGREEMENT", ma.num_contrato_superior);
                            alm.SetValue("AGMT_ITEM", ma.num_posicion_contrato_superior);
                            alm.SetValue("RELATIONSHIP_TYPE", ma.clase_relacion_ordenacion);
                            alm.SetValue("RELATIONSHIP_UNIT", ma.unidad_intervalo_relaciones_ordenacion);
                            alm.SetValue("RELATIONSHIP_UNIT_ISO", ma.codigo_iso_unidad_medida2);
                            //alm.SetValue("RELATIONSHIP_INTERVAL", ma.intervalo_relaciones_ordenacion);
                            alm.SetValue("MRP_RELEVANT", ma.efectividad_reserva);
                            alm.SetValue("DIR_PROCUR", ma.indicador_aprovisionamiento_directo);
                            alm.SetValue("SPECIAL_STOCK", ma.indicador_stock_especial_visualizacion_dialogo);
                            //alm.SetValue("VSI_SIZE1", ma.dimension_bruta1);
                            alm.SetValue("VSI_SIZE_UNIT", ma.unidad_dimension_brutas);
                            alm.SetValue("VSI_SIZE_UNIT_ISO", ma.codigo_unidad_medida);
                            alm.SetValue("VSI_FORMULA", ma.clave_formula);
                            //alm.SetValue("VSI_SIZE2", ma.dimension_bruta2);
                            //alm.SetValue("VSI_NO", ma.cantidad_piezas_bruto);
                            //alm.SetValue("VSI_SIZE3", ma.dimension_bruta3);
                            //alm.SetValue("VSI_QTY", ma.ctd_piezas_bruto);
                            alm.SetValue("VAR_SIZE_COMP_MEASURE_UNIT", ma.unidad_medida_componente_pieza_bruto);
                            alm.SetValue("VAR_SIZE_COMP_MEASURE_UNIT_ISO", ma.codigo_iso_p_unidad_medida);
                            alm.SetValue("MATERIAL_EXTERNAL", ma.num_largo_material);
                            alm.SetValue("MATERIAL_GUID", ma.uid_externo_campo_material);
                            alm.SetValue("MATERIAL_VERSION", ma.num_version_campo_material);
                            alm.SetValue("REQ_DATE", ma.fecha_necesidad_componente);
                            alm.SetValue("REQ_TIME", ma.fecha_necesidad_cantidad_reserva);
                            alm.SetValue("MANUAL_REQUIREMENTS_DATE", ma.in_actualizacion_manual_fecha_necesidad);
                            alm.SetValue("MATERIAL_LONG", ma.num_material_long);
                        }
                        catch (Exception) { }
                    }
                }
                if (cab.Count > 0)
                {
                    foreach (SELECT_cabecera_ordenes_crea_Folio_MDL_Result ca in cab)
                    {
                        try
                        {
                            ord.Append();
                            ord.SetValue("FOLIO_SAM", ca.folio_sam);
                            ord.SetValue("AUFNR", ca.num_orden);
                            ord.SetValue("FUNC_LOC", ca.ubicacion_tecnica);
                            ord.SetValue("UZEIT", ca.hora_dia);
                            ord.SetValue("DATUM", ca.fecha);
                            //ord.SetValue("TABIX", ca.indice_registro_no_valido);
                            ord.SetValue("PPLANT", ca.centro_planificacion_mantenimiento);
                            ord.SetValue("ORDTYPE", ca.clase_orden);
                            ord.SetValue("MNWKCTR", ca.puesto_trabajo_responsable_medidas_mante);
                            ord.SetValue("SHORTTXT", ca.texto_breve);
                            ord.SetValue("EQUIP", ca.num_equipo);
                            ord.SetValue("START_DATE", ca.fecha_inicio_extrema);
                            ord.SetValue("FINISH_DATE", ca.fecha_fin_extrema);
                            ord.SetValue("FOLIO_SAP", ca.folio_sap);
                            ord.SetValue("RECIBIDO", ca.recibido);
                            ord.SetValue("PROCESADO", ca.procesado);
                            ord.SetValue("ERROR", ca.error);
                            ord.SetValue("MODIFICA", ca.modificado);
                            ord.SetValue("UNAME", ca.usuario);
                            ord.SetValue("FECHA_MOD", ca.fecha_ultima_modificacion);
                            ord.SetValue("HORA_MOD", ca.hora_ultima_modificacion);
                        }
                        catch (Exception) { }
                    }
                }
                if (txt.Count > 0)
                {
                    foreach (SELECT_texto_posicion_ordenes_crea_Folio_MDL_Result tx in txt)
                    {
                        try
                        {
                            texPos.Append();
                            texPos.SetValue("FOLIO_SAM", tx.folio_sam);
                            texPos.SetValue("VORNR", tx.num_operacion);
                            texPos.SetValue("INDICE", tx.indice);
                            texPos.SetValue("TDFORMAT", tx.formato);
                            texPos.SetValue("TDLINE", tx.texto);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure ln in Rord)
                {
                    CabOrdenesCrea c = new CabOrdenesCrea();
                    try
                    {
                        c.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        c.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_OrdenesPMBD.ObtenerInstancia().ActualizaCabOrdenesCrea(connection, c);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroEstatus()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_statusnot_ultimo_registro_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalStatusNot(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_status_noti_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZPM_STATUSORDEN_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroEstatus();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_statusnot_datos_id_MDL_Result> datos = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerDatosIdEstatus(connection, id).ToList();
            foreach (SELECT_statusnot_datos_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha_sistema;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_statusnot_valida_hora_MDL_Result> validahor = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerValidacionHoraEstatus(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_estatusnot_menos_MDL_Result> f = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerFolioMenosEstatus(connection, id).ToList();
                        foreach (SELEC_fol_estatusnot_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_statusnot_MDL_Result> movs = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerTodoFolioEstatus(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_statusnot_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosEstatus(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_statusnot_MDL_Result> movs = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerTodoFolioEstatus(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_statusnot_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosEstatus(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_statusnot_MDL_Result> movs = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerTodoFolioEstatus(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_statusnot_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosEstatus(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosEstatus(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_status_notificaciones_Folio_MDL_Result> staN = DALC_StatusNotificaciones.ObtenerInstancia().ObtenerEstatusNotFol(connection, folis).ToList();
            int CA = staN.Count;
            string total = ObtenerTotalStatusNot(folis);
            if (total.Equals(staN.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZPM_STATUSORDEN_SAM_VS_SAP_BD");
                IRfcTable statusO = FUNCTION.GetTable("TSAM_STATUS_ORD");
                IRfcTable RstatusO = FUNCTION.GetTable("RSAM_STATUS_ORD");
                FUNCTION.SetValue("CAB", CA);
                if (staN.Count > 0)
                {
                    foreach (SELECT_status_notificaciones_Folio_MDL_Result st in staN)
                    {
                        try
                        {
                            statusO.Append();
                            statusO.SetValue("FOLIO_SAM", st.folio_sam);
                            statusO.SetValue("FOLIO_ORD", st.folio_orden_sam);
                            statusO.SetValue("DATUM", st.fecha_sistema);
                            statusO.SetValue("UZEIT", st.hora_sistema);
                            statusO.SetValue("AUFNR", st.folio_sap_orden);
                            statusO.SetValue("WERKS", st.centro);
                            statusO.SetValue("OPERACION", st.operacion_sam);
                            statusO.SetValue("RECIBIDO", st.recibido);
                            statusO.SetValue("PROCESADO", st.procesado);
                            statusO.SetValue("MENSAJE", st.error);
                            statusO.SetValue("FDATUM", st.fecha_fin);
                            statusO.SetValue("FUZEIT", st.hora_fin);
                            statusO.SetValue("NEWCHARG", st.lote);
                            statusO.SetValue("FECHA_RECIBIDO", st.fecha_recibido);
                            statusO.SetValue("HORA_RECIBIDO", st.hora_recibido);
                            statusO.SetValue("UNAME", st.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure ln in RstatusO)
                {
                    StatusNotificaciones status = new StatusNotificaciones();
                    try
                    {
                        status.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        status.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_StatusNotificaciones.ObtenerInstancia().ActualizaStatusNotificaciones(connection, status);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroDefectos()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_defectos_ultimo_registro_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalDefectosPos(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_defectos_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public string ObtenerTotalTextosDefectos(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_textos_defectos_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZMM_MASIVO_DEFECTOS101()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroDefectos();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_defectos_datos_id_MDL_Result> datos = DALC_QMDefectos.ObtenerInstancia().ObtenerDatosIdDefectos(connection, id).ToList();
            foreach (SELECT_defectos_datos_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_defectos_valida_hora_MDL_Result> validahor = DALC_QMDefectos.ObtenerInstancia().ObtenerValidacionHoraDefectos(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_defectos_menos_MDL_Result> f = DALC_QMDefectos.ObtenerInstancia().ObtenerFolioMenosDefectos(connection, id).ToList();
                        foreach (SELEC_fol_defectos_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_defectos_MDL_Result> movs = DALC_QMDefectos.ObtenerInstancia().ObtenerTodoFolioDefectos(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_defectos_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosDefectos(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_defectos_MDL_Result> movs = DALC_QMDefectos.ObtenerInstancia().ObtenerTodoFolioDefectos(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_defectos_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosDefectos(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_defectos_MDL_Result> movs = DALC_QMDefectos.ObtenerInstancia().ObtenerTodoFolioDefectos(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_defectos_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosDefectos(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosDefectos(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_defectos_crea_Folio_MDL_Result> cabe = DALC_QMDefectos.ObtenerInstancia().ObtenerCabeceraFolio(connection, folis).ToList();
            int CAB = cabe.Count;
            List<SELECT_posiciones_defectos_crea_Folio_MDL_Result> posi = DALC_QMDefectos.ObtenerInstancia().ObtenerPosicionesDefectosFolio(connection, folis).ToList();
            int POS = posi.Count;
            List<SELECT_textos_defectos_crea_Folio_MDL_Result> texs = DALC_QMDefectos.ObtenerInstancia().ObtenerTextosDeFolios(connection, folis).ToList();
            string totpos = ObtenerTotalDefectosPos(folis);
            string tottxt = ObtenerTotalTextosDefectos(folis);
            if (totpos.Equals(posi.Count.ToString()) && tottxt.Equals(texs.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZMM_MASIVO_DEFECTOS101");
                IRfcTable cabsap = FUNCTION.GetTable("CAB_DEFECTOSQM");
                IRfcTable possap = FUNCTION.GetTable("POS_DEFECTOSQM");
                IRfcTable txtde = FUNCTION.GetTable("TXT_POS");
                IRfcTable rescab = FUNCTION.GetTable("RCAB_DEFECTOSQM");
                IRfcTable respos = FUNCTION.GetTable("RPOS_DEFECTOSQM");
                FUNCTION.SetValue("CAB", CAB);
                FUNCTION.SetValue("POS", POS);
                if (cabe.Count > 0)
                {
                    foreach (SELECT_cabecera_defectos_crea_Folio_MDL_Result ca in cabe)
                    {
                        try
                        {
                            cabsap.Append();
                            cabsap.SetValue("FOLIO_SAM", ca.folio_sam);
                            cabsap.SetValue("PRUEFLOS", ca.num_lote_inspeccion);
                            cabsap.SetValue("WERKS", ca.centro);
                            cabsap.SetValue("DATUM", ca.fecha);
                            cabsap.SetValue("MATNR", ca.num_material);
                            cabsap.SetValue("UZEIT", ca.hora_dia);
                            cabsap.SetValue("EBELN", ca.num_doc_compras);
                            cabsap.SetValue("FOLIO_MOV", ca.folio_sam_movimiento);
                            cabsap.SetValue("MBLNR", ca.num_doc_material);
                            cabsap.SetValue("FEART", ca.clase_informe_entrada_defecto);
                            cabsap.SetValue("QKURZTEXT", ca.texto_breve);
                            cabsap.SetValue("RBNR", ca.perfil_catalogo);
                            cabsap.SetValue("RBNRX", ca.texto_esquema_informe);
                            cabsap.SetValue("RECIBIDO", ca.recibido);
                            cabsap.SetValue("PROCESADO", ca.procesado);
                            cabsap.SetValue("MENSAJE", ca.error);
                            cabsap.SetValue("FECHA_RECIBIDO", ca.fecha_recibido);
                            cabsap.SetValue("HORA_RECIBIDO", ca.hora_recibido);
                            cabsap.SetValue("PRUEFER", ca.nombre_inspector);
                            cabsap.SetValue("UNAME", ca.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                if (posi.Count > 0)
                {
                    foreach (SELECT_posiciones_defectos_crea_Folio_MDL_Result po in posi)
                    {
                        try
                        {
                            possap.Append();
                            possap.SetValue("FOLIO_SAM", po.folio_sam);
                            possap.SetValue("POSICION", po.num_posicion_registro);
                            possap.SetValue("PRUEFLOS", po.num_lote_inspeccion);
                            possap.SetValue("DATUM", po.fecha);
                            possap.SetValue("UZEIT", po.hora_dia);
                            possap.SetValue("FEGRP", po.grupo_codigos_problema);
                            possap.SetValue("FECOD", po.codigo_defecto);
                            possap.SetValue("TXTCDGR", po.texto_breve_codigo_averia_problemas);
                            possap.SetValue("ANZFEHLER", po.cantidad_defectos_observados);
                            possap.SetValue("FEQKLAS", po.clase_defecto);
                            possap.SetValue("OTGRP", po.grupo_codigos_parte_objeto);
                            possap.SetValue("OTEIL", po.codigo_ubicacion_defecto);
                            possap.SetValue("TXTCDOT", po.texto_breve_codigo_objeto);
                            possap.SetValue("FETXT", po.defectos);
                            possap.SetValue("RECIBIDO", po.recibido);
                            possap.SetValue("PROCESADO", po.procesado);
                            possap.SetValue("MENSAJE", po.error);
                            possap.SetValue("FECHA_RECIBIDO", po.fecha_recibido);
                            possap.SetValue("HORA_RECIBIDO", po.hora_recibido);
                        }
                        catch (Exception) { }
                    }
                }
                if (texs.Count > 0)
                {
                    foreach (SELECT_textos_defectos_crea_Folio_MDL_Result tdc in texs)
                    {
                        try
                        {
                            txtde.Append();
                            txtde.SetValue("FOLIO_SAM", tdc.folio_sam_defecto);
                            txtde.SetValue("FENUM", tdc.num_posicion_defecto);
                            txtde.SetValue("RENGLON", tdc.renglon);
                            txtde.SetValue("TDFORMAT", tdc.formato);
                            txtde.SetValue("TDLINE", tdc.texto);
                        }
                        catch (Exception ex) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure ln in rescab)
                {
                    Cabecera_defectos cabs = new Cabecera_defectos();
                    try
                    {
                        cabs.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        cabs.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_QMDefectos.ObtenerInstancia().ActulizarCabeceraDefectos(connection, cabs);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure ln in respos)
                {
                    Posiciones_defectos posc = new Posiciones_defectos();
                    try
                    {
                        posc.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        posc.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_QMDefectos.ObtenerInstancia().ActulizarPosicionesCrea(connection, posc);
                    }
                    catch (Exception) { }
                }
            }
        }
        public bool P_ZINV_SAM(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZINV_SAM");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tab = FUNCTION.GetTable("INVENTARIO_SAM");

            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tab)
            {
                Inventarios ins = new Inventarios();
                try
                {
                    ins.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_Inventarios.obtenerInstania().VaciarInventario(connection, ins);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tab)
            {
                Inventarios ins = new Inventarios();
                try
                {
                    ins.MATNR = linea.GetValue("MATNR").ToString();
                    ins.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                    ins.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                    ins.MATKL = linea.GetValue("MATKL").ToString();
                    ins.WERKS = linea.GetValue("WERKS").ToString();
                    ins.MEINS = linea.GetValue("MEINS").ToString();
                    ins.LGORT = linea.GetValue("LGORT").ToString();
                    ins.LGOBE = linea.GetValue("LGOBE").ToString();
                    ins.CLABS = linea.GetValue("CLABS").ToString();
                    ins.CINSM = linea.GetValue("CINSM").ToString();
                    ins.CSPEM = linea.GetValue("CSPEM").ToString();
                    ins.CUMLM = linea.GetValue("CUMLM").ToString();
                    ins.CHARG = linea.GetValue("CHARG").ToString();
                    ins.MTART = linea.GetValue("MTART").ToString();
                    ins.SERNR = linea.GetValue("SERNR").ToString();
                    ins.XCHPF = linea.GetValue("XCHPF").ToString();

                    DALC_Inventarios.obtenerInstania().IngresarInventarios(connection, ins);
                }
                catch (Exception) { return false; }
            }
            DALC_Inventarios.obtenerInstania().EliminarDatosRepetidos(connection);
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZINV_SAM", "Correcto");
            return true;
        }
        public bool P_ZREP_CONTADOR_EQUIPO(string centro)
        {
            String Nfecha = fechaN.Substring(0, 4) + fechaN.Substring(4, 2) + fechaN.Substring(6, 2);
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_CONTADOR_EQUIPO");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("FECHA_LOW", Nfecha);
            IRfcTable ta1 = FUNCTION.GetTable("CONTADOR");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteContadores um = new ReporteContadores();
                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReporteContadores.ObtenerInstancia().VaciarReporteContadores(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                ReporteContadores um = new ReporteContadores();

                try
                {
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.VALOR_CNT = linea.GetValue("VALOR_CNT").ToString();
                    um.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    um.DATUM = linea.GetValue("DATUM").ToString();
                    um.UZEIT = linea.GetValue("UZEIT").ToString();
                    um.NIVEL = linea.GetValue("NIVEL").ToString();
                    um.ICONO = linea.GetValue("ICONO").ToString();
                    um.EQUNR = linea.GetValue("EQUNR").ToString();
                    um.EQKTX = linea.GetValue("EQKTX").ToString();
                    um.POINT = linea.GetValue("POINT").ToString();
                    um.MDOCM = linea.GetValue("MDOCM").ToString();
                    um.NRECDV = linea.GetValue("NRECDV").ToString();
                    um.NREADG = linea.GetValue("NREADG").ToString();
                    um.RECDU = linea.GetValue("RECDU").ToString();
                    um.TPLNR = linea.GetValue("TPLNR").ToString();
                    um.MATNR = linea.GetValue("MATNR").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.SERNR = linea.GetValue("SERNR").ToString();
                    um.LGORT = linea.GetValue("LGORT").ToString();
                    um.CHARG = linea.GetValue("CHARG").ToString();
                    um.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    um.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    um.ERROR = linea.GetValue("ERROR").ToString();
                    um.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
                    um.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
                    um.UNAME = linea.GetValue("UNAME").ToString();

                    DALC_ReporteContadores.ObtenerInstancia().IngresarReporteContadores(connection, um);
                    DALC_ReporteContadores.ObtenerInstancia().ActulizarReporteContadores(connection, um);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_CONTADOR_EQUIPO", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_CONTADOR_EQUIPO", "X", centro);
            return true;
        }
        public void P_ZPM_CIERRA_AVISOS_SAP_VS_SAM()
        {
            List<SELECT_cierre_ligue_avisos_MDL_Result> cieav = DALC_CierreAvisosLigue.ObtenerInstancia().ObtenerCierreFolio(connection).ToList();
            int CA = cieav.Count;
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZPM_CIERRA_AVISOS_SAP_VS_SAM");
            IRfcTable ta1 = FUNCTION.GetTable("CIERRA");
            IRfcTable ta2 = FUNCTION.GetTable("RCIERRA");
            FUNCTION.SetValue("CAB", CA);

            if (cieav.Count > 0)
            {
                foreach (SELECT_cierre_ligue_avisos_MDL_Result st in cieav)
                {
                    try
                    {
                        ta1.Append();
                        ta1.SetValue("FOLIO_SAM", st.folio_sam_cl);
                        ta1.SetValue("FOLIO_SAP", st.folio_aviso);
                        ta1.SetValue("AUFNR", st.folio_sam_orden);
                        ta1.SetValue("DATUM", st.fecha);
                        ta1.SetValue("UZEIT", st.hora_dia);
                        ta1.SetValue("FECHA_RECIBIDO", st.fecha_recibido);
                        ta1.SetValue("HORA_RECIBIDO", st.hora_recibido);
                        ta1.SetValue("STATUS", st.estatus);
                        ta1.SetValue("ESTATUS", st.indicador_cerrado_aviso);
                        ta1.SetValue("RECIBIDO", st.recibido);
                        ta1.SetValue("PROCESADO", st.procesado);
                        ta1.SetValue("ERROR", st.error);
                        ta1.SetValue("UNAME", st.usuario);
                    }
                    catch (Exception) { }
                }
            }
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure ln in ta2)
            {
                CierreAvisosLigue avicie = new CierreAvisosLigue();
                try
                {
                    avicie.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    avicie.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                    DALC_CierreAvisosLigue.ObtenerInstancia().ActulizaCierreAvisosLigue(connection, avicie);
                }
                catch (Exception) { }
            }
        }
        public string ObtenerUltimoRegistroLoteMM()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_lotemov_ultimo_reg_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalLoteMM(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_lotesmm_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZMM_LOTE_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroLoteMM();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_lotemov_id_MDL_Result> datos = DALC_CalidadMM.ObtenerInstancia().ObtenerDatosIdLoteMM(connection, id).ToList();
            foreach (SELEC_datos_lotemov_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_lotemov_valida_hora_MDL_Result> validahor = DALC_CalidadMM.ObtenerInstancia().ObtenerValidacionHoraLoteMM(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_lotemov_menos_MDL_Result> f = DALC_CalidadMM.ObtenerInstancia().ObtenerFolioMenosLoteMM(connection, id).ToList();
                        foreach (SELEC_fol_lotemov_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_lotemov_MDL_Result> movs = DALC_CalidadMM.ObtenerInstancia().ObtenerTodoFolioLoteMM(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_lotemov_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosLoteMM(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_lotemov_MDL_Result> movs = DALC_CalidadMM.ObtenerInstancia().ObtenerTodoFolioLoteMM(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_lotemov_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosLoteMM(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_lotemov_MDL_Result> movs = DALC_CalidadMM.ObtenerInstancia().ObtenerTodoFolioLoteMM(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_lotemov_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosLoteMM(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosLoteMM(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_lotes_inspeccion_movimientos_crea_Folio_MDL_Result> cab = DALC_CalidadMM.ObtenerInstancia().ObtenerCabLotInsFolio(connection, folis).ToList();
            int CA = cab.Count;
            List<SELECT_posiciones_lotes_inspeccion_movimientos_crea_Folio_MDL_Result> pos = DALC_CalidadMM.ObtenerInstancia().ObtenerPosLotFol(connection, folis).ToList();
            int POS = pos.Count;
            string total = ObtenerTotalLoteMM(folis);
            if (total.Equals(pos.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZMM_LOTE_SAM_VS_SAP_BD");
                IRfcTable ta1 = FUNCTION.GetTable("LOTE_INSP_CAB");
                IRfcTable ta2 = FUNCTION.GetTable("LOTE_INSP");
                IRfcTable rta2 = FUNCTION.GetTable("RLOTE_INSP");
                FUNCTION.SetValue("CAB", CA);
                FUNCTION.SetValue("POS", POS);

                if (cab.Count > 0)
                {
                    foreach (SELECT_cabecera_lotes_inspeccion_movimientos_crea_Folio_MDL_Result cb in cab)
                    {
                        try
                        {
                            ta1.Append();
                            ta1.SetValue("FOLIO_SAM", cb.folio_sam);
                            ta1.SetValue("PRUEFLOS", cb.num_lote_inspeccion);
                            ta1.SetValue("DATUM", cb.fecha);
                            ta1.SetValue("UZEIT", cb.hora_dia);
                            ta1.SetValue("MATNR", cb.material);
                            ta1.SetValue("MAKTX", cb.texto_breve_material);
                            ta1.SetValue("EBELN", cb.num_doc_compras);
                            ta1.SetValue("FOLIO_MOV", cb.folio_sam_mov);
                            ta1.SetValue("MBLNR", cb.num_doc_material);
                            ta1.SetValue("WERKS", cb.centro);
                            ta1.SetValue("ERSTELLER", cb.creador_registro_datos);
                            ta1.SetValue("RECIBIDO", cb.recibido);
                            ta1.SetValue("PROCESADO", cb.procesado);
                            ta1.SetValue("MENSAJE", cb.error);
                            ta1.SetValue("FECHA_RECIBIDO", cb.fecha_recibido);
                            ta1.SetValue("PRUEFER", cb.nombre_inspector);
                            ta1.SetValue("UNAME", cb.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                if (pos.Count > 0)
                {
                    foreach (SELECT_posiciones_lotes_inspeccion_movimientos_crea_Folio_MDL_Result posm in pos)
                    {
                        try
                        {
                            ta2.Append();
                            decimal val = Convert.ToDecimal(posm.tamano_muestra_inspeccionar_carac);
                            int vl = Convert.ToInt32(val);
                            int val3 = Convert.ToInt32(posm.num_unidad_muestreo_defectuosa);
                            ta2.SetValue("FOLIO_SAM", posm.folio_sam);
                            ta2.SetValue("MERKNR", posm.num_caracteristica_inspeccion);
                            ta2.SetValue("PRUEFLOS", posm.num_lote_inspeccion);
                            ta2.SetValue("DATUM", posm.fecha);
                            ta2.SetValue("UZEIT", posm.hora_dia);
                            ta2.SetValue("KURZTEXT", posm.texto_breve_carac_inspeccion);
                            ta2.SetValue("KTX01", posm.descripcion_breve_conjunto_seleccion);
                            ta2.SetValue("KATAB1", posm.entrada_catalogo_conjunto_seleccion);
                            ta2.SetValue("PRUEFUMF", vl);
                            ta2.SetValue("ANZFEHLEH", val3);
                            ta2.SetValue("ORIGINAL_INPUT", posm.valor_original_anterior_tratamiento_entradas);
                            ta2.SetValue("MBEWERTG", posm.valoracion_resultado_inspeccion);
                            ta2.SetValue("CODE1", posm.codigo);
                            ta2.SetValue("MASSEINHSW", posm.uni_medida_graban_dat_cuantitativos);
                            ta2.SetValue("PRUEFBEMKT", posm.texto_breve);
                            ta2.SetValue("KATALGART1", posm.catalogo);
                            ta2.SetValue("ERSTELLER", posm.creador_registro_datos);
                            ta2.SetValue("RECIBIDO", posm.recibido);
                            ta2.SetValue("PROCESADO", posm.procesado);
                            ta2.SetValue("MENSAJE", posm.error);
                            ta2.SetValue("FECHA_RECIBIDO", posm.fecha_recibido);
                            ta2.SetValue("HORA_RECIBIDO", posm.hora_recibido);
                            ta2.SetValue("CHAR_TYPE", posm.tipo_caracteristica);
                            ta2.SetValue("GRUPPE1", posm.grupo_codigos);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure ln in ta1)
                {
                    CalidadMovimientosMateriales camov = new CalidadMovimientosMateriales();
                    try
                    {
                        camov.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        camov.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_CalidadMM.ObtenerInstancia().ActulizaCalidadMovMat(connection, camov);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure ln in rta2)
                {
                    CalidadMMPos pomov = new CalidadMMPos();
                    try
                    {
                        pomov.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        pomov.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_CalidadMM.ObtenerInstancia().ActualizaCalidadPosMov(connection, pomov);
                    }
                    catch (Exception) { }
                }
            }
        }
        public string ObtenerUltimoRegistroLotePM()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_lotepm_ultimo_registro_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalLotePM(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_poslotePM_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public string ObtenerTotalDeEm2(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_decision2_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZQM_LOTEZIW41_SAM_VS_SAP_BD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroLotePM();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_lotepm_datos_id_MDL_Result> datos = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerDatosIdLotePM(connection, id).ToList();
            foreach (SELECT_lotepm_datos_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_lotepm_valida_hora_MDL_Result> validahor = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerValidacionHoraLotePM(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_lotepm_menos_MDL_Result> f = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerFolioMenosLotePM(connection, id).ToList();
                        foreach (SELEC_fol_lotepm_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_lotepm_MDL_Result> movs = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerTodoFolioLotePM(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_lotepm_MDL_Result m in movs)
                            {
                                folio_sam = m.folio_sam;
                                EnviarDatosLotePM(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_lotepm_MDL_Result> movs = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerTodoFolioLotePM(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_lotepm_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosLotePM(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_lotepm_MDL_Result> movs = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerTodoFolioLotePM(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_lotepm_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosLotePM(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosLotePM(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_lotes_inspeccion_notificaciones_crea_Folio_MDL_Result> cab = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerCabLotInsFolio(connection, folis).ToList();
            int CA = cab.Count;
            List<SELECT_posiciones_lotes_inspeccion_notificaciones_crea_F_MDL_Result> pos = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerPosLotInsFolio(connection, folis).ToList();
            int POS = pos.Count;
            List<SELECT_decision_empleo_lote_crea2_Folio_MDL_Result> des = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerDecisionesEmploeFolio(connection, folis).ToList();
            int DES = des.Count;
            List<SELECT_textos_decision_empleo_lote_crea2_Folio_MDL_Result> txt = DALC_CalidadNotificaciones.ObtenerInstancia().ObtenerTextosResultados(connection, folis).ToList();
            string totpos = ObtenerTotalLotePM(folis);
            string totde2 = ObtenerTotalDeEm2(folis);
            if (totpos.Equals(pos.Count.ToString()) && totde2.Equals(des.Count.ToString()))
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZQM_LOTEZIW41_SAM_VS_SAP_BD");
                IRfcTable ta1 = FUNCTION.GetTable("LOTE_ZIW41_CAB");
                IRfcTable ta2 = FUNCTION.GetTable("LOTE_ZIW41");
                IRfcTable ta3 = FUNCTION.GetTable("DECISION_EMPLEO");
                IRfcTable ta4 = FUNCTION.GetTable("TXT_CARACTERISTICA");
                IRfcTable Rta1 = FUNCTION.GetTable("RLOTE_ZIW41_CAB");
                IRfcTable Rta2 = FUNCTION.GetTable("RLOTE_ZIW41");
                IRfcTable Rta3 = FUNCTION.GetTable("RDECISION_EMPLEO");
                FUNCTION.SetValue("CAB", CA);
                FUNCTION.SetValue("POS", POS);
                FUNCTION.SetValue("DEC", DES);
                if (cab.Count > 0)
                {
                    foreach (SELECT_cabecera_lotes_inspeccion_notificaciones_crea_Folio_MDL_Result cb in cab)
                    {
                        try
                        {
                            ta1.Append();
                            ta1.SetValue("FOLIO_SAM", cb.folio_sam);
                            ta1.SetValue("PRUEFLOS", cb.num_lote_inspeccion);
                            ta1.SetValue("DATUM", cb.fecha);
                            ta1.SetValue("UZEIT", cb.hora_dia);
                            ta1.SetValue("AUFNR", cb.num_orden);
                            ta1.SetValue("VORNR", cb.num_operacion);
                            ta1.SetValue("KTEXTLOS", cb.texto_breve_operacion);
                            ta1.SetValue("WERK", cb.centro);
                            ta1.SetValue("ERSTELLER", cb.creador_registro_datos);
                            ta1.SetValue("ENSTEHDAT", cb.fecha_creacion_lote);
                            ta1.SetValue("ENTSTEZEIT", cb.hora_creacion_lote);
                            ta1.SetValue("AENDERER", cb.ultimo_modificador_registro_datos);
                            ta1.SetValue("AENDERDAT", cb.fecha_modificacion_registro_datos);
                            ta1.SetValue("AENDERZEIT", cb.hora_modificacion_lote);
                            ta1.SetValue("RECIBIDO", cb.recibido);
                            ta1.SetValue("PROCESADO", cb.procesado);
                            ta1.SetValue("MENSAJE", cb.error);
                            ta1.SetValue("FECHA_RECIBIDO", cb.fecha_recibido);
                            ta1.SetValue("HORA_RECIBIDO", cb.hora_recibido);
                            ta1.SetValue("PRUEFER", cb.nombre_inspector);
                            ta1.SetValue("UNAME", cb.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                if (pos.Count > 0)
                {
                    foreach (SELECT_posiciones_lotes_inspeccion_notificaciones_crea_F_MDL_Result posm in pos)
                    {
                        try
                        {
                            ta2.Append();
                            ta2.SetValue("FOLIO_SAM", posm.folio_sam);
                            ta2.SetValue("PRUEFLOS", posm.num_lote_inspeccion);
                            ta2.SetValue("MERKNR", posm.num_caracteristica_inspeccion);
                            ta2.SetValue("DATUM", posm.fecha);
                            ta2.SetValue("UZEIT", posm.hora_dia);
                            ta2.SetValue("KURZTEXT", posm.texto_breve_caracteristica_inspeccion);
                            ta2.SetValue("KTX01", posm.descripcion_breve_conjunto_seleccion);
                            ta2.SetValue("KATAB1", posm.entrada_catalogo_conjunto_seleccion);
                            ta2.SetValue("ANZFEHLEH", posm.num_unidades_muestreo_defectuoso);
                            ta2.SetValue("ORIGINAL_INPUT", posm.valor_original_anterior_tratamiento_entradas);
                            ta2.SetValue("MBEWERTG", posm.valoracion_resultado_inspeccion);
                            ta2.SetValue("CODE1", posm.codigo);
                            ta2.SetValue("MASSEINHSW", posm.unidad_medida_graban_cuantitativos);
                            ta2.SetValue("PRUEFBEMKT", posm.texto_breve);
                            ta2.SetValue("KATALGART1", posm.catalogo);
                            ta2.SetValue("CHAR_TYPE", posm.tipo_caracteristica);
                            ta2.SetValue("AUSWMENGE1", posm.grupo_codigos_conjunto_seleccion_asignado);
                            ta2.SetValue("RECIBIDO", posm.recibido);
                            ta2.SetValue("PROCESADO", posm.procesado);
                            ta2.SetValue("MENSAJE", posm.error);
                            ta2.SetValue("FECHA_RECIBIDO", posm.fecha_recibido);
                            ta2.SetValue("HORA_RECIBIDO", posm.hora_recibido);
                            ta2.SetValue("GRUPPE1", posm.grupo_codigos);
                            ta2.SetValue("SOLLSTPUMF", posm.tamano_muestra_inps_carac);
                        }
                        catch (Exception) { }
                    }
                }
                if (des.Count > 0)
                {
                    foreach (SELECT_decision_empleo_lote_crea2_Folio_MDL_Result dc in des)
                    {
                        try
                        {
                            ta3.Append();
                            ta3.SetValue("FOLIO_SAM", dc.folio_sam);
                            ta3.SetValue("PRUEFLOS", dc.num_lote_inspeccion);
                            ta3.SetValue("WERKS", dc.centro);
                            ta3.SetValue("AUFNR", dc.num_orden);
                            ta3.SetValue("FOLIO_ORD", dc.folio_sam_orden);
                            ta3.SetValue("DATUM", dc.fecha);
                            ta3.SetValue("UZEIT", dc.hora_dia);
                            ta3.SetValue("VCODE", dc.codigo_decision_empleo);
                            ta3.SetValue("VCODEGRP", dc.grupo_codigo_decision_empleo);
                            ta3.SetValue("TEXTO", dc.texto_mensaje);
                            ta3.SetValue("RECIBIDO", dc.recibido);
                            ta3.SetValue("PROCESADO", dc.procesado);
                            ta3.SetValue("MENSAJE", dc.error);
                            ta3.SetValue("FECHA_RECIBIDO", dc.fecha_recibido);
                            ta3.SetValue("HORA_RECIBIDO", dc.hora_recibido);
                            ta3.SetValue("UNAME", dc.usuario);
                        }
                        catch (Exception) { }
                    }
                }
                if (txt.Count > 0)
                {
                    foreach (SELECT_textos_decision_empleo_lote_crea2_Folio_MDL_Result text in txt)
                    {
                        try
                        {
                            ta4.Append();
                            ta4.SetValue("FOLIO_SAM", text.folio_sam);
                            ta4.SetValue("PRUEFLOS", text.num_lote_inspeccion);
                            ta4.SetValue("MERKNR", text.num_caracteristica_inspeccion);
                            ta4.SetValue("RENGLON", text.renglon);
                            ta4.SetValue("TDFORMAT", text.formato);
                            ta4.SetValue("TDLINE", text.texto);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure ln in Rta1)
                {
                    CalidadCabNot canot = new CalidadCabNot();
                    try
                    {
                        canot.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        canot.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_CalidadNotificaciones.ObtenerInstancia().ActulizarCabLotInsNot(connection, canot);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure ln in Rta2)
                {
                    CalidadPosNot ponot = new CalidadPosNot();
                    try
                    {
                        ponot.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                        ponot.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                        DALC_CalidadNotificaciones.ObtenerInstancia().ActulizaPosLotInsNot(connection, ponot);
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure linea in Rta3)
                {
                    DecisionEmpleo de = new DecisionEmpleo();
                    try
                    {
                        de.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        de.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                        DALC_CalidadNotificaciones.ObtenerInstancia().ActualizarDecisionEmpleo2(connection, de);
                    }
                    catch (Exception) { }
                }
            }
        }
        public void P_ZSAM_GUARDA_SOLPED_CANCEL()
        {
            List<SELECT_cancela_solped_MDL_Result> can = DALC_Sol_Ped.ObtenerInstancia().ObtenerCancelacionesSolPed(connection).ToList();
            int CA = can.Count;
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZSAM_GUARDA_SOLPED_CANCEL");
            IRfcTable ta1 = FUNCTION.GetTable("TBORRA_SOLPED");
            IRfcTable taR = FUNCTION.GetTable("RBORRA_SOLPED");
            FUNCTION.SetValue("CAB", CA);
            if (can.Count > 0)
            {
                foreach (SELECT_cancela_solped_MDL_Result ca in can)
                {
                    try
                    {
                        ta1.Append();
                        ta1.SetValue("PREQ_ITEM", ca.num_posicion_solped);
                        ta1.SetValue("FOLIO_SAM", ca.folio_sam);
                        ta1.SetValue("FECHA_M", ca.fecha);
                        ta1.SetValue("HORA_M", ca.hora);
                        ta1.SetValue("FOLIO_SAP", ca.folio_sap);
                        ta1.SetValue("DELETE_IND", ca.indicador_borrado);
                        ta1.SetValue("CLOSED", ca.conluido);
                        ta1.SetValue("RECIBIDO", ca.recibido);
                        ta1.SetValue("PROCESADO", ca.procesado);
                        ta1.SetValue("MESSAGE", ca.error);
                        ta1.SetValue("FECHA_R", ca.fecha_sap);
                        ta1.SetValue("HORA_R", ca.hora_sap);
                    }
                    catch (Exception) { }
                }
            }
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure linea in taR)
            {
                SolPedCancela spc = new SolPedCancela();
                try
                {
                    spc.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    spc.PREQ_ITEM = linea.GetValue("PREQ_ITEM").ToString();
                    spc.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().ActulizaCancelacion(connection, spc);
                }
                catch (Exception) { }
            }
        }
        public bool P_ZVALIDA_SOLPED(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZVALIDA_SOLPED");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tabCecos = FUNCTION.GetTable("CECOS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabCecos)
            {
                Cecos ce = new Cecos();
                try
                {
                    ce.BUKRS = linea.GetValue("BUKRS").ToString();
                    DALC_ValidaSolped.ObtenerInstancia().VaciarCecos(connection, ce);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tabCecos)
            {
                Cecos ce = new Cecos();

                try
                {
                    ce.BUKRS = linea.GetValue("BUKRS").ToString();
                    ce.KSTAR = linea.GetValue("KSTAR").ToString();
                    ce.LTEXT = linea.GetValue("LTEXT").ToString();
                    ce.KOSTL = linea.GetValue("KOSTL").ToString();
                    ce.KTEXT_ES = linea.GetValue("KTEXT_ES").ToString();

                    DALC_ValidaSolped.ObtenerInstancia().IngresaCecos(connection, ce);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZVALIDA_SOLPED", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZVALIDA_SOLPED", "X", centro);
            return true;
        }
        public bool P_ZTIPO_SOLPED(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZTIPO_SOLPED");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tabCecos = FUNCTION.GetTable("ZTIPOS_SOLPED");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabCecos)
            {
                Tipos_Solped ts = new Tipos_Solped();
                try
                {
                    ts.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_Tipos_Solped.ObtenerInstancia().VaciarTiposSolped(connection, ts);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tabCecos)
            {
                Tipos_Solped ts = new Tipos_Solped();

                try
                {
                    ts.WERKS = linea.GetValue("WERKS").ToString();
                    ts.BSART = linea.GetValue("BSART").ToString();
                    ts.MTART = linea.GetValue("MTART").ToString();
                    ts.BATXT_ES = linea.GetValue("BATXT_ES").ToString();
                    ts.BATXT_EN = linea.GetValue("BATXT_EN").ToString();

                    DALC_Tipos_Solped.ObtenerInstancia().IngresaTiposSolped(connection, ts);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZTIPO_SOLPED", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZTIPO_SOLPED", "X", centro);
            return true;
        }
        public string ValidacionPedidoPos(string pedido, string pos)
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.VALIDACION_PEDIDO_POSICION_MDL(pedido, pos, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public bool P_ZSAM_TOLERANCIA(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZSAM_TOLERANCIA");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tabtol = FUNCTION.GetTable("TOLERANCIA");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabtol)
            {
                string pedido = "", pos = "";
                pedido = linea.GetValue("EBELN").ToString();
                pos = linea.GetValue("EBELP").ToString();
                string val = ValidacionPedidoPos(pedido, pos);
                if (val.Equals(""))
                {
                    Tolerancia tol = new Tolerancia();
                    try
                    {
                        tol.WERKS = linea.GetValue("WERKS").ToString();
                        tol.EBELN = linea.GetValue("EBELN").ToString();
                        tol.EBELP = linea.GetValue("EBELP").ToString();
                        int tole = Convert.ToInt32(linea.GetValue("TOLERANCIA").ToString());
                        tol.TOLERANCIA = Convert.ToString(tole);
                        DALC_Tolerancia.ObtenerInstancia().IngresarToleranciaPedido(connection, tol);
                    }
                    catch (Exception) { return false; }
                }
                else
                {
                    Tolerancia tol = new Tolerancia();
                    try
                    {
                        tol.WERKS = linea.GetValue("WERKS").ToString();
                        tol.EBELN = linea.GetValue("EBELN").ToString();
                        tol.EBELP = linea.GetValue("EBELP").ToString();
                        int tole = Convert.ToInt32(linea.GetValue("TOLERANCIA").ToString());
                        tol.TOLERANCIA = Convert.ToString(tole);
                        DALC_Tolerancia.ObtenerInstancia().ActualizarToleranciaPedido(connection, tol);
                    }
                    catch (Exception) { return false; }
                }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_TOLERANCIA", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSAM_TOLERANCIA", "X", centro);
            return true;
        }
        public string ValidacionTipoMov(string id_mov)
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.VALIDACION_TIPO_MOVI_MDL(id_mov, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public bool P_ZMM_CLM(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZMM_CLM");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tabla = FUNCTION.GetTable("SAM_CLMT");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabla)
            {
                string id_tm = "";
                id_tm = linea.GetValue("BWART").ToString();
                string val = ValidacionTipoMov(id_tm);
                if (val.Equals(""))
                {
                    TiposMovimientos tp = new TiposMovimientos();
                    try
                    {
                        tp.BWART = linea.GetValue("BWART").ToString();
                        tp.BTEXT = linea.GetValue("BTEXT").ToString();
                        DALC_TipoMovi.ObtenerInstancia().IngresarTipoMov(connection, tp);
                    }
                    catch (Exception) { return false; }
                }
                else
                {
                    TiposMovimientos tp = new TiposMovimientos();
                    try
                    {
                        tp.BWART = linea.GetValue("BWART").ToString();
                        tp.BTEXT = linea.GetValue("BTEXT").ToString();
                        DALC_TipoMovi.ObtenerInstancia().ActualizarTipoMov(connection, tp);
                    }
                    catch (Exception) { return false; }
                }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_CLM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_CLM", "X", centro);
            return true;
        }
        public bool P_ZREP_LOTESINS_MM(string centro)
        {
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZREP_LOTESINS_MM");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable replot = FUNCTION.GetTable("LOTESINSP_MM");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in replot)
            //{
            //    ReporteLotesInsp rl = new ReporteLotesInsp();
            //    try
            //    {
            //        rl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        DALC_ReporteLostesInspeccion.ObtenerInstancia().VaciarReporteLotesInspeccion(connection, rl);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in replot)
            //{
            //    ReporteLotesInsp rl = new ReporteLotesInsp();
            //    try
            //    {
            //        rl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        rl.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
            //        rl.DATUM = linea.GetValue("DATUM").ToString();
            //        rl.UZEIT = linea.GetValue("UZEIT").ToString();
            //        rl.MATNR = linea.GetValue("MATNR").ToString();
            //        rl.MAKTX = linea.GetValue("MAKTX").ToString();
            //        rl.EBELN = linea.GetValue("EBELN").ToString();
            //        rl.FOLIO_MOV = linea.GetValue("FOLIO_MOV").ToString();
            //        rl.MBLNR = linea.GetValue("MBLNR").ToString();
            //        rl.WERKS = linea.GetValue("WERKS").ToString();
            //        rl.ERSTELLER = linea.GetValue("ERSTELLER").ToString();
            //        rl.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
            //        rl.PROCESADO = linea.GetValue("PROCESADO").ToString();
            //        rl.MENSAJE = linea.GetValue("MENSAJE").ToString();
            //        rl.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
            //        rl.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
            //        rl.PRUEFER = linea.GetValue("PRUEFER").ToString();
            //        rl.UNAME = linea.GetValue("UNAME").ToString();

            //        DALC_ReporteLostesInspeccion.ObtenerInstancia().InsertReporteLotesInspeccion(connection, rl);
            //        DALC_ReporteLostesInspeccion.ObtenerInstancia().ActualizarReporteLotesInspeccion(connection, rl);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_LOTESINS_MM", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_LOTESINS_MM", "X", centro);
            return true;
        }
        public bool P_ZREP_LOTESINS_PM(string centro)
        {
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZREP_LOTESINS_PM");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable replot = FUNCTION.GetTable("LOTESINSP_PM");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in replot)
            //{
            //    ReporteLotesPM rl = new ReporteLotesPM();
            //    try
            //    {
            //        rl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        DALC_ReporteLotesPM.ObtenerInstancia().VaciarReporteLotesPM(connection, rl);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in replot)
            //{
            //    ReporteLotesPM rl = new ReporteLotesPM();
            //    try
            //    {
            //        rl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        rl.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
            //        rl.DATUM = linea.GetValue("DATUM").ToString();
            //        rl.UZEIT = linea.GetValue("UZEIT").ToString();
            //        rl.AUFNR = linea.GetValue("AUFNR").ToString();
            //        rl.VORNR = linea.GetValue("VORNR").ToString();
            //        rl.KTEXTLOS = linea.GetValue("KTEXTLOS").ToString();
            //        rl.WERK = linea.GetValue("WERK").ToString();
            //        rl.ERSTELLER = linea.GetValue("ERSTELLER").ToString();
            //        rl.ENSTEHDAT = linea.GetValue("ENSTEHDAT").ToString();
            //        rl.ENTSTEZEIT = linea.GetValue("ENTSTEZEIT").ToString();
            //        rl.AENDERER = linea.GetValue("AENDERER").ToString();
            //        rl.AENDERDAT = linea.GetValue("AENDERDAT").ToString();
            //        rl.AENDERZEIT = linea.GetValue("AENDERZEIT").ToString();
            //        rl.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
            //        rl.PROCESADO = linea.GetValue("PROCESADO").ToString();
            //        rl.MENSAJE = linea.GetValue("MENSAJE").ToString();
            //        rl.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
            //        rl.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
            //        rl.PRUEFER = linea.GetValue("PRUEFER").ToString();
            //        rl.UNAME = linea.GetValue("UNAME").ToString();
            //        rl.FOLIO_ORD = linea.GetValue("FOLIO_ORD").ToString();
            //        DALC_ReporteLotesPM.ObtenerInstancia().IngresarLotesPM(connection, rl);
            //        DALC_ReporteLotesPM.ObtenerInstancia().ActualizarLotesPMcrea(connection, rl);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_LOTESINS_PM", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_LOTESINS_PM", "X", centro);
            return true;
        }
        public bool P_ZREP_ACT_AVISO(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_ACT_AVISO");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tab1 = FUNCTION.GetTable("ACT_AVISO_CAB");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tab1)
            {
                ReporteAvisosActividad ac = new ReporteAvisosActividad();
                try
                {
                    ac.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    DALC_ReporteAvisosActividades.ObtenerInstancia().VaciarReporteAvisosActividades(connection, ac);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tab1)
            {
                ReporteAvisosActividad ac = new ReporteAvisosActividad();
                try
                {
                    ac.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    ac.QMNUM = linea.GetValue("QMNUM").ToString();
                    ac.FECHA = linea.GetValue("FECHA").ToString();
                    ac.HORA = linea.GetValue("HORA").ToString();
                    ac.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    ac.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    ac.MENSAJE = linea.GetValue("MENSAJE").ToString();
                    ac.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
                    ac.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
                    ac.UNAME = linea.GetValue("UNAME").ToString();
                    ac.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReporteAvisosActividades.ObtenerInstancia().IngresarReporteAvisosActividades(connection, ac);
                    DALC_ReporteAvisosActividades.ObtenerInstancia().ActualizarReporteAvisosActividades(connection, ac);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_ACT_AVISO", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_ACT_AVISO", "X", centro);
            return true;
        }
        public bool P_ZREP_DMSMASIVO(string centro)
        {
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZREP_DMSMASIVO");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable tab1 = FUNCTION.GetTable("DMS_MASIVO");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in tab1)
            //{
            //    ReporteDMS rdms = new ReporteDMS();
            //    try
            //    {
            //        rdms.FOLIO_DMS = linea.GetValue("FOLIO_DMS").ToString();
            //        DALC_ReporteDMS.ObtenerInstancia().VaciarReporteDMS(connection, rdms);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in tab1)
            //{
            //    ReporteDMS rdms = new ReporteDMS();
            //    try
            //    {
            //        rdms.FOLIO_DMS = linea.GetValue("FOLIO_DMS").ToString();
            //        rdms.TABIX = linea.GetValue("TABIX").ToString();
            //        rdms.DOCNUMBER = linea.GetValue("DOCNUMBER").ToString();
            //        rdms.DATUM = linea.GetValue("DATUM").ToString();
            //        rdms.UZEIT = linea.GetValue("UZEIT").ToString();
            //        rdms.WSAPPLICATION = linea.GetValue("WSAPPLICATION").ToString();
            //        rdms.OBJECTTYPE = linea.GetValue("OBJECTTYPE").ToString();
            //        rdms.DOCUMENTTYPE = linea.GetValue("DOCUMENTTYPE").ToString();
            //        rdms.DESCRIPTION = linea.GetValue("DESCRIPTION").ToString();
            //        rdms.UNAME = linea.GetValue("UNAME").ToString();
            //        rdms.DOCFILE = linea.GetValue("DOCFILE").ToString();
            //        rdms.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
            //        rdms.PROCESADO = linea.GetValue("PROCESADO").ToString();
            //        rdms.MENSAJE = linea.GetValue("MENSAJE").ToString();
            //        rdms.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
            //        rdms.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
            //        rdms.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_ReporteDMS.ObtenerInstancia().IngresarReporteDMS(connection, rdms);
            //        DALC_ReporteDMS.ObtenerInstancia().ActualizarReporteDMS(connection, rdms);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_DMSMASIVO", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_DMSMASIVO", "X", centro);
            return true;
        }
        public bool P_ZREP_ACTUALIZA(string centro)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZREP_ACTUALIZA");
            FUNCTION.SetValue("WERKS", centro);
            IRfcTable tab1 = FUNCTION.GetTable("ACTUALIZA_CAB");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tab1)
            {
                ReporteActualizaAvisoCalidad rac = new ReporteActualizaAvisoCalidad();
                try
                {
                    rac.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    DALC_ReporteAvisosCalidad.ObtenerInstancia().VaciarTReporteAvisosCalidad(connection, rac);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in tab1)
            {
                ReporteActualizaAvisoCalidad rac = new ReporteActualizaAvisoCalidad();
                try
                {
                    rac.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rac.NOTIF_NO = linea.GetValue("NOTIF_NO").ToString();
                    rac.TASK_KEY = linea.GetValue("TASK_KEY").ToString();
                    rac.TASK_CODE = linea.GetValue("TASK_CODE").ToString();
                    rac.UNAME = linea.GetValue("UNAME").ToString();
                    rac.DATUM = linea.GetValue("DATUM").ToString();
                    rac.UZEIT = linea.GetValue("UZEIT").ToString();
                    rac.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    rac.PROCESADO = linea.GetValue("PROCESADO").ToString();
                    rac.MENSAJE = linea.GetValue("MENSAJE").ToString();
                    rac.HORA_RECIBIDO = linea.GetValue("HORA_RECIBIDO").ToString();
                    rac.FECHA_RECIBIDO = linea.GetValue("FECHA_RECIBIDO").ToString();
                    rac.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_ReporteAvisosCalidad.ObtenerInstancia().IngresarReporteAvisosCalidad(connection, rac);
                    DALC_ReporteAvisosCalidad.ObtenerInstancia().ActualizaReporteAvisosCalidad(connection, rac);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_ACTUALIZA", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_ACTUALIZA", "X", centro);
            return true;
        }
        public bool P_ZREP_DEFECTOS_MM(string centro)
        {
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZREP_DEFECTOS_MM");
            //FUNCTION.SetValue("WERKS", centro);
            //IRfcTable repdef = FUNCTION.GetTable("DEFECTOS_MM");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in repdef)
            //{
            //    ReporteDefectos rd = new ReporteDefectos();
            //    try
            //    {
            //        rd.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        rd.WERKS = linea.GetValue("WERKS").ToString();
            //        DALC_ReporteDefectos.ObtenerInstancia().VaciarReporteDefectos(connection, rd);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in repdef)
            //{
            //    ReporteDefectos red = new ReporteDefectos();
            //    try
            //    {
            //        red.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        red.PRUEFLOS = linea.GetValue("PRUEFLOS").ToString();
            //        red.WERKS = linea.GetValue("WERKS").ToString();
            //        red.DATUM = linea.GetValue("DATUM").ToString();
            //        red.MATNR = linea.GetValue("MATNR").ToString();
            //        red.UZEIT = linea.GetValue("UZEIT").ToString();
            //        red.EBELN = linea.GetValue("EBELN").ToString();
            //        red.FOLIO_MOV = linea.GetValue("FOLIO_MOV").ToString();
            //        red.MBLNR = linea.GetValue("MBLNR").ToString();
            //        red.FEART = linea.GetValue("FEART").ToString();
            //        red.QKURZTEXT = linea.GetValue("QKURZTEXT").ToString();
            //        red.RBNR = linea.GetValue("RBNR").ToString();
            //        red.RBNRX = linea.GetValue("RBNRX").ToString();
            //        red.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
            //        red.PROCESADO = linea.GetValue("PROCESADO").ToString();
            //        red.MENSAJE = linea.GetValue("PROCESADO").ToString();
            //        red.FECHA_RECIBIDO = linea.GetValue("PROCESADO").ToString();
            //        red.HORA_RECIBIDO = linea.GetValue("PROCESADO").ToString();
            //        red.PRUEFER = linea.GetValue("PROCESADO").ToString();
            //        red.UNAME = linea.GetValue("PROCESADO").ToString();

            //        DALC_ReporteDefectos.ObtenerInstancia().InsertarReporteDefecto(connection, red);
            //        DALC_ReporteDefectos.ObtenerInstancia().ActulizarDefectos(connection, red);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZREP_DEFECTOS_MM", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZREP_DEFECTOS_MM", "X", centro);
            return true;
        }
        public bool P_ZEXTRAE_ESTATUS()
        {
            bool dato = false;
            List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
            foreach (centro_MDL_Result ce in cc)
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZEXTRAE_ESTATUS");
                FUNCTION.SetValue("WERKS", ce.centro);
                IRfcTable est = FUNCTION.GetTable("ESTATUS");
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception exc) { return dato; }
                foreach (IRfcStructure linea in est)
                {
                    EstatusCreacion et = new EstatusCreacion();
                    try
                    {
                        et.ESTATUS = linea.GetValue("ESTATUS").ToString();
                        if (et.ESTATUS == "X")
                        {
                            Thread.Sleep(2000);
                            P_ZREP_MOVIMIENTOS(ce.centro);
                            P_ZSAM_LIST_MOV_VIS(ce.centro);
                            dato = true;
                        }
                        else { dato = false; }
                    }
                    catch (Exception exc) { return dato; }
                }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZEXTRAE_ESTATUS", "Correcto");
            return dato;
        }
        public void P_ZBORRA_ESTATUS()
        {
            List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
            foreach (centro_MDL_Result ce in cc)
            {
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCTION = repo.CreateFunction("ZBORRA_ESTATUS");
                FUNCTION.SetValue("WERKS", ce.centro);
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZBORRA_ESTATUS", "Correcto");
        }
        public bool VerificarErrores()
        {
            List<SELECT_Errores_Movimientos_MDL_Result> err = DALC_ReporteMovimientos.ObtenerInstancia().ObtenerErrores(connection).ToList();
            if (err.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void P_ZQM_DECISION_MASIVO()
        {
            //List<SELECT_decision_empleo_lote_crea_MDL_Result> can = DALC_DecisionEmpleo.ObtenerInstancia().ObtenerDecisionesEmpleo(connection).ToList();
            //int CA = can.Count;
            //List<SELECT_textos_decision_empleo_MDL_Result> txt = DALC_DecisionEmpleo.ObtenerInstancia().ObtenerTextosDecisionEmpleo(connection).ToList();
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZQM_DECISION_MASIVO");
            //IRfcTable ta1 = FUNCTION.GetTable("DECISION_EMPLEO");
            //IRfcTable ta2 = FUNCTION.GetTable("TXT_EMPLEO");
            //IRfcTable taR = FUNCTION.GetTable("RDECISION_EMPLEO");
            //FUNCTION.SetValue("CAB", CA);
            //if (can.Count > 0)
            //{
            //    foreach (SELECT_decision_empleo_lote_crea_MDL_Result ca in can)
            //    {
            //        try
            //        {
            //            ta1.Append();
            //            ta1.SetValue("FOLIO_SAM", ca.folio_sam);
            //            ta1.SetValue("PRUEFLOS", ca.num_lote_inspeccion);
            //            ta1.SetValue("WERKS", ca.centro);
            //            ta1.SetValue("AUFNR", ca.num_orden);
            //            ta1.SetValue("FOLIO_ORD", ca.folio_sam_orden);
            //            ta1.SetValue("DATUM", ca.fecha);
            //            ta1.SetValue("UZEIT", ca.hora_dia);
            //            ta1.SetValue("VCODE", ca.codigo_decision_empleo);
            //            ta1.SetValue("VCODEGRP", ca.grupo_codigo_decision_empleo);
            //            ta1.SetValue("TEXTO", ca.texto_mensaje);
            //            ta1.SetValue("RECIBIDO", ca.recibido);
            //            ta1.SetValue("PROCESADO", ca.procesado);
            //            ta1.SetValue("MENSAJE", ca.error);
            //            ta1.SetValue("FECHA_RECIBIDO", ca.fecha_recibido);
            //            ta1.SetValue("HORA_RECIBIDO", ca.hora_recibido);
            //            ta1.SetValue("UNAME", ca.usuario);
            //        }
            //        catch (Exception) { }
            //    }
            //}
            //if (txt.Count > 0)
            //{
            //    foreach (SELECT_textos_decision_empleo_MDL_Result text in txt)
            //    {
            //        try
            //        {
            //            ta2.Append();
            //            ta2.SetValue("FOLIO_SAM", text.folio_sam);
            //            ta2.SetValue("RENGLON", text.renglon);
            //            ta2.SetValue("TDFORMAT", text.formato);
            //            ta2.SetValue("TDLINE", text.texto);
            //        }
            //        catch (Exception) { }
            //    }
            //}
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { }
            //foreach (IRfcStructure linea in taR)
            //{
            //    DecisionEmpleo dec = new DecisionEmpleo();
            //    try
            //    {
            //        dec.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
            //        dec.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
            //DALC_DecisionEmpleo.ObtenerInstancia().ActualizarDecisionEmpleo(connection, dec);
            //    }
            //    catch (Exception) { }
            //}
        }
        public bool P_ZQM_NOTIFICA(string centro)
        {
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZQM_NOTIFICA");
            //FUNCTION.SetValue("CENTRO", centro);
            //IRfcTable ta1 = FUNCTION.GetTable("NOTIFTASK");
            //IRfcTable ta2 = FUNCTION.GetTable("TEXTOS");
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { return false; }
            //foreach (IRfcStructure linea in ta1)
            //{
            //    AvisosCalidad ac = new AvisosCalidad();
            //    try
            //    {
            //        ac.NOTIF_NO = linea.GetValue("NOTIF_NO").ToString();
            //        DALC_AvisosCalidad.ObtenerInstancia().DeleteAvisosCalidad(connection, ac);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta1)
            //{
            //    AvisosCalidad ac = new AvisosCalidad();
            //    try
            //    {
            //        ac.NOTIF_NO = linea.GetValue("NOTIF_NO").ToString();
            //        ac.TASK_KEY = linea.GetValue("TASK_KEY").ToString();
            //        ac.TASK_CAT_TYP = linea.GetValue("TASK_CAT_TYP").ToString();
            //        ac.TASK_CODEGRP = linea.GetValue("TASK_CODEGRP").ToString();
            //        ac.TASK_CODE = linea.GetValue("TASK_CODE").ToString();
            //        ac.TASK_TEXT = linea.GetValue("TASK_TEXT").ToString();
            //        ac.CREATED_BY = linea.GetValue("CREATED_BY").ToString();
            //        ac.CREATED_DATE = linea.GetValue("CREATED_DATE").ToString();
            //        ac.CHANGED_BY = linea.GetValue("CHANGED_BY").ToString();
            //        ac.CHANGED_DATE = linea.GetValue("CHANGED_DATE").ToString();
            //        ac.PLND_START_DATE = linea.GetValue("PLND_START_DATE").ToString();
            //        ac.PLND_END_DATE = linea.GetValue("PLND_END_DATE").ToString();
            //        ac.OBJECT_NO = linea.GetValue("OBJECT_NO").ToString();
            //        ac.LONG_TEXT = linea.GetValue("LONG_TEXT").ToString();
            //        ac.PRILANG = linea.GetValue("PRILANG").ToString();
            //        ac.PLND_START_TIME = linea.GetValue("PLND_START_TIME").ToString();
            //        ac.PLND_END_TIME = linea.GetValue("PLND_END_TIME").ToString();
            //        ac.CARRIED_OUT_BY = linea.GetValue("CARRIED_OUT_BY").ToString();
            //        ac.CARRIED_OUT_DATE = linea.GetValue("CARRIED_OUT_DATE").ToString();
            //        ac.CARRIED_OUT_TIME = linea.GetValue("CARRIED_OUT_TIME").ToString();
            //        ac.RESUBMIT_DATE = linea.GetValue("RESUBMIT_DATE").ToString();
            //        ac.ITEM_KEY = linea.GetValue("ITEM_KEY").ToString();
            //        ac.CREATED_TIME = linea.GetValue("CREATED_TIME").ToString();
            //        ac.CHANGED_TIME = linea.GetValue("CHANGED_TIME").ToString();
            //        ac.PARTN_ROLE = linea.GetValue("PARTN_ROLE").ToString();
            //        ac.PARTNER = linea.GetValue("PARTNER").ToString();
            //        ac.DELETE_FLAG = linea.GetValue("DELETE_FLAG").ToString();
            //        ac.TASK_SORT_NO = linea.GetValue("TASK_SORT_NO").ToString();
            //        ac.TXT_TASKGRP = linea.GetValue("TXT_TASKGRP").ToString();
            //        ac.TXT_TASKCD = linea.GetValue("TXT_TASKCD").ToString();
            //        ac.STATUS = linea.GetValue("STATUS").ToString();
            //        ac.USERSTATUS_FLAG = linea.GetValue("USERSTATUS_FLAG").ToString();
            //        ac.USERSTATUS = linea.GetValue("USERSTATUS").ToString();
            //        DALC_AvisosCalidad.ObtenerInstancia().InsertAvisosCalidad(connection, ac);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta2)
            //{
            //    TextosAvisosCalidad tac = new TextosAvisosCalidad();
            //    try
            //    {
            //        tac.NOTIF_NO = linea.GetValue("NOTIF_NO").ToString();
            //        DALC_AvisosCalidad.ObtenerInstancia().DeleteTextosAvisoCalidad(connection, tac);
            //    }
            //    catch (Exception) { return false; }
            //}
            //foreach (IRfcStructure linea in ta2)
            //{
            //    TextosAvisosCalidad tac = new TextosAvisosCalidad();
            //    try
            //    {
            //        tac.NOTIF_NO = linea.GetValue("NOTIF_NO").ToString();
            //        tac.TASK_KEY = linea.GetValue("TASK_KEY").ToString();
            //        tac.TASK_SORT_NO = linea.GetValue("TASK_SORT_NO").ToString();
            //        tac.LINEA = linea.GetValue("LINEA").ToString();
            //        tac.TEXTO = linea.GetValue("TEXTO").ToString();
            //        tac.FOIO_SAM = linea.GetValue("FOIO_SAM").ToString();
            //        DALC_AvisosCalidad.ObtenerInstancia().InsertTextosAvisosCalidad(connection, tac);
            //    }
            //    catch (Exception) { return false; }
            //}
            //DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZQM_NOTIFICA", "Correcto");
            //DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZQM_NOTIFICA", "X", centro);
            return true;
        }
        public string ObtenerUltimoRegistroTxtAviCal()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_ultimo_registro_txtavical_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public string ObtenerTotalCab(string folio_sam)
        {
            ObjectParameter objpar = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_postxtavical_fol_MDL(folio_sam, objpar);
            string total = "";
            total = objpar.Value.ToString();
            return total;
        }
        public void P_ZGUARDA_ACTUALIZA()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroTxtAviCal();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_txtavical_datos_id_MDL_Result> datos = DALC_ModificaAvisosGuarda.ObtenerInstancia().ObtenerDatosIdAvisoModif(connection, id).ToList();
            foreach (SELECT_txtavical_datos_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_sam;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_txtavical_valida_hora_MDL_Result> validahor = DALC_ModificaAvisosGuarda.ObtenerInstancia().ObtenerHoraAvisoMod(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELEC_fol_txtavical_menos_MDL_Result> f = DALC_ModificaAvisosGuarda.ObtenerInstancia().ObtenerFolioMenosAviMod(connection, id).ToList();
                        foreach (SELEC_fol_txtavical_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_sam;
                            List<SELECT_lista_folios_txtavical_MDL_Result> avm = DALC_ModificaAvisosGuarda.ObtenerInstancia().ObtenerListaFolios(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_txtavical_MDL_Result a in avm)
                            {
                                folio_sam = a.folio_sam;
                                EnviarAvisosCalidadTextos(folio_sam);
                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_txtavical_MDL_Result> avm = DALC_ModificaAvisosGuarda.ObtenerInstancia().ObtenerListaFolios(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_txtavical_MDL_Result a in avm)
                        {
                            folio_sam = a.folio_sam;
                            EnviarAvisosCalidadTextos(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_txtavical_MDL_Result> avm = DALC_ModificaAvisosGuarda.ObtenerInstancia().ObtenerListaFolios(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_txtavical_MDL_Result a in avm)
                    {
                        folio_sam = a.folio_sam;
                        EnviarAvisosCalidadTextos(folio_sam);
                    }
                }
            }
        }
        public void EnviarAvisosCalidadTextos(string folio_sam)
        {
            Thread.Sleep(10000);
            string folis = folio_sam;
            List<SELECT_cabecera_avisos_textos_calidad_crea_Folio_MDL_Result> cab = DALC_AvisosCalidad_Crea.ObtenerInstancia().ObtenerCabeceraTextosAvisosCalidad(connection, folis).ToList();
            int CA = cab.Count;
            List<SELECT_posiciones_avisos_textos_calidad_crea_Folio_MDL_Result> pos = DALC_AvisosCalidad_Crea.ObtenerInstancia().ObtenerPosicionesAvisosCalidad(connection, folis).ToList();
            int PO = pos.Count;
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZGUARDA_ACTUALIZA");
            IRfcTable ta1 = FUNCTION.GetTable("ACTUALIZA_CAB");
            IRfcTable ta2 = FUNCTION.GetTable("ACTUALIZA_TXT");
            IRfcTable ta1R = FUNCTION.GetTable("RACTUALIZA_CAB");
            IRfcTable ta2R = FUNCTION.GetTable("RACTUALIZA_TXT");
            FUNCTION.SetValue("CAB", CA);
            FUNCTION.SetValue("POS", PO);
            if (cab.Count > 0)
            {
                foreach (SELECT_cabecera_avisos_textos_calidad_crea_Folio_MDL_Result ca in cab)
                {
                    try
                    {
                        ta1.Append();
                        ta1.SetValue("FOLIO_SAM", ca.folio_sam);
                        ta1.SetValue("NOTIF_NO", ca.num_notificacion);
                        ta1.SetValue("TASK_KEY", ca.numero_correlativo_medida_mante);
                        ta1.SetValue("TASK_CODE", ca.codigo_transaccion);
                        ta1.SetValue("UNAME", ca.usuario);
                        ta1.SetValue("DATUM", ca.fecha);
                        ta1.SetValue("UZEIT", ca.hora_dia);
                        ta1.SetValue("RECIBIDO", ca.recibido);
                        ta1.SetValue("PROCESADO", ca.procesado);
                        ta1.SetValue("MENSAJE", ca.error);
                        ta1.SetValue("HORA_RECIBIDO", ca.hora_recibido);
                        ta1.SetValue("FECHA_RECIBIDO", ca.fecha_recibido);
                    }
                    catch (Exception) { }
                }
            }
            if (pos.Count > 0)
            {
                foreach (SELECT_posiciones_avisos_textos_calidad_crea_Folio_MDL_Result po in pos)
                {
                    try
                    {
                        ta2.Append();
                        ta2.SetValue("FOLIO_SAM", po.folio_sam);
                        ta2.SetValue("LINEA", po.linea);
                        ta2.SetValue("NOTIF_NO", po.num_notificacion);
                        ta2.SetValue("TASK_KEY", po.num_correlativo_medida_mante);
                        ta2.SetValue("TASK_SORT_NO", po.num_clasificacion_medida);
                        ta2.SetValue("TEXTO", po.linea_texto);
                        ta2.SetValue("DATUM", po.fecha);
                        ta2.SetValue("UZEIT", po.hora_dia);
                        ta2.SetValue("RECIBIDO", po.recibido);
                        ta2.SetValue("PROCESADO", po.procesado);
                        ta2.SetValue("UNAME", po.usuario);
                        ta2.SetValue("FECHA_RECIBIDO", po.fecha_recibido);
                        ta2.SetValue("HORA_RECIBIDO", po.hora_recibido);
                        ta2.SetValue("MENSAJE", po.error);
                    }
                    catch (Exception) { }
                }
            }
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure ln in ta1R)
            {
                TextoAvisosCalidadCrea txtca = new TextoAvisosCalidadCrea();
                try
                {
                    txtca.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    txtca.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                    DALC_AvisosCalidad_Crea.ObtenerInstancia().ActualizarCabTextoAvi(connection, txtca);
                }
                catch (Exception) { }
            }
            foreach (IRfcStructure linea in ta2R)
            {
                TextoPosAvisosCalidad txtpos = new TextoPosAvisosCalidad();
                try
                {
                    txtpos.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    txtpos.RECIBIDO = linea.GetValue("RECIBIDO").ToString();
                    DALC_AvisosCalidad_Crea.ObtenerInstancia().ActualicarPosTextoAvi(connection, txtpos);
                }
                catch (Exception) { }
            }
        }
        public string ObtenerUltimoResultadoAvMod()
        {
            ObjectParameter objpar = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_ultimo_registro_txtavical_MDL(objpar);
            string val = "";
            val = objpar.Value.ToString();
            return val;
        }
        public void P_ZGUARDA_ACT_AVISOS()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 2;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoResultadoAvMod();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELECT_cabecera_modificacion_avisos_sap_crea_datos_id_MDL_Result> datos = DALC_ModAvisosPM.ObtenerInstancia().ObtenerDatosIdModAviSAP(connection, id).ToList();
            foreach (SELECT_cabecera_modificacion_avisos_sap_crea_datos_id_MDL_Result da in datos)
            {
                folio_sam = da.folio_aviso;
                fechafol = da.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_cabecera_modificacion_avisos_sap_crea_valida_hora_MDL_Result> validahor = DALC_ModAvisosPM.ObtenerInstancia().ObtenerHoraAvisoModSAP(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        id = id - 1;
                        List<SELECT_fol_cabecera_modificacion_avisos_sap_crea_menos_MDL_Result> f = DALC_ModAvisosPM.ObtenerInstancia().ObtenerFolioMenosAviModSAP(connection, id).ToList();
                        foreach (SELECT_fol_cabecera_modificacion_avisos_sap_crea_menos_MDL_Result fo in f)
                        {
                            folio_sam = fo.folio_aviso;
                            List<SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result> avm = DALC_ModAvisosPM.ObtenerInstancia().ObtenerListaFoliosAviSAP(connection, folio_sam).ToList();
                            foreach (SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result a in avm)
                            {
                                folio_sam = a.folio_aviso;

                            }
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result> avm = DALC_ModAvisosPM.ObtenerInstancia().ObtenerListaFoliosAviSAP(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result a in avm)
                        {
                            folio_sam = a.folio_aviso;

                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result> avm = DALC_ModAvisosPM.ObtenerInstancia().ObtenerListaFoliosAviSAP(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_cabecera_modificacion_avisos_sap_crea_MDL_Result a in avm)
                    {
                        folio_sam = a.folio_aviso;

                    }
                }
            }
        }
        public void EnviarAvisosSAPModificados(string folio_sam)
        {
            //Thread.Sleep(10000);
            //string folis = folio_sam;
            //List<SELECT_cabecera_modificacion_avisos_sap_crea_Folio_MDL_Result> cab = DALC_ModAvisosPM.ObtenerInstancia().ObtenerDatosCabAvisoSAPFol(connection, folis).ToList();
            //int CAB = cab.Count();
            //List<SELECT_posiciones_modificacion_avisos_sap_crea_Folio_MDL_Result> pos = DALC_ModAvisosPM.ObtenerInstancia().ObtenerDatosPosAviSAPFol(connection, folis).ToList();
            //int POS = pos.Count();
            //RfcRepository repo = rfcDesti.Repository;
            //IRfcFunction FUNCTION = repo.CreateFunction("ZGUARDA_ACT_AVISOS");
            //IRfcTable ta1 = FUNCTION.GetTable("ACT_AVISO_CAB");
            //IRfcTable ta2 = FUNCTION.GetTable("ACT_AVISO");
            //IRfcTable ta1R = FUNCTION.GetTable("RACT_AVISO_CAB");
            //IRfcTable ta2R = FUNCTION.GetTable("RACT_AVISO");
            //FUNCTION.SetValue("CAB", CAB);
            //FUNCTION.SetValue("POS", POS);
            //if (cab.Count > 0)
            //{
            //    foreach (SELECT_cabecera_modificacion_avisos_sap_crea_Folio_MDL_Result ca in cab)
            //    {
            //        try
            //        {
            //            ta1.Append();
            //            ta1.SetValue("FOLIO_SAM", ca.folio_aviso);
            //            ta1.SetValue("QMNUM", ca.num_notificacion);
            //            ta1.SetValue("FECHA", ca.fecha);
            //            ta1.SetValue("HORA", ca.hora_dia);
            //            ta1.SetValue("RECIBIDO", ca.recibido);
            //            ta1.SetValue("PROCESADO", ca.procesado);
            //            ta1.SetValue("MENSAJE", ca.error);
            //            ta1.SetValue("HORA_RECIBIDO", ca.hora_recibido);
            //            ta1.SetValue("FECHA_RECIBIDO", ca.fecha_recibido);
            //            ta1.SetValue("UNAME", ca.usuario);
            //        }
            //        catch (Exception) { }
            //    }
            //}
            //if (pos.Count > 0)
            //{
            //    foreach (SELECT_posiciones_modificacion_avisos_sap_crea_Folio_MDL_Result po in pos)
            //    {
            //        try
            //        {
            //            ta2.Append();
            //            ta2.SetValue("FOLIO_SAM", po.folio_aviso);
            //            ta2.SetValue("QMNUM", po.num_notificacion);
            //            ta2.SetValue("RENGLON", po.renglon);
            //            ta2.SetValue("FECHA", po.fecha);
            //            ta2.SetValue("HORA", po.hora_dia);
            //            ta2.SetValue("REFOBJECTKEY", po.clave_objeto);
            //            ta2.SetValue("ACT_KEY", po.num_actual_actividad);
            //            ta2.SetValue("ACT_SORT_NO", po.num_clasificacion_actividad);
            //            ta2.SetValue("ACTTEXT", po.texto_accion);
            //            ta2.SetValue("ACT_CODEGRP", po.grupo_codigo_acciones);
            //            ta2.SetValue("ACT_CODE", po.codigo_actividad);
            //            ta2.SetValue("START_DATE", po.fecha_inicio);
            //            ta2.SetValue("START_TIME", po.hora_inicio_accion);
            //            ta2.SetValue("END_DATE", po.fecha_fin);
            //            ta2.SetValue("END_TIME", po.hora_fin_accion);
            //            ta2.SetValue("ITEM_SORT_NO", po.num_clasificacion_x_posicion);
            //            ta2.SetValue("RECIBIDO", po.recibido);
            //            ta2.SetValue("PROCESADO", po.procesado);
            //            ta2.SetValue("MENSAJE", po.error);
            //            ta2.SetValue("HORA_RECIBIDO", po.hora_recibido);
            //            ta2.SetValue("FECHA_RECIBIDO", po.fecha_recibido);
            //            ta2.SetValue("UNAME", po.usuario);
            //        }
            //        catch (Exception) { }
            //    }
            //}
            //try
            //{
            //    FUNCTION.Invoke(rfcDesti);
            //}
            //catch (Exception) { }
            //foreach (IRfcStructure ln in ta1R)
            //{
            //    CanAvisosSAPModificaciones cabe = new CanAvisosSAPModificaciones();
            //    try
            //    {
            //        cabe.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
            //        cabe.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
            //        DALC_ModAvisosPM.ObtenerInstancia().ActualizarCabAviModSAP(connection, cabe);
            //    }
            //    catch (Exception) { }
            //}
            //foreach (IRfcStructure ln in ta1R)
            //{
            //    PosAvisosSAPModificaciones posi = new PosAvisosSAPModificaciones();
            //    try
            //    {
            //        posi.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
            //        posi.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
            //        DALC_ModAvisosPM.ObtenerInstancia().ActualizarPosAviModSAP(connection, posi);
            //    }
            //    catch (Exception) { }
            //}
        }
        /************************ PEDIDOS SD *****************************/
        public string ObtenerUltimoRegistroPedidos()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_pedido_sd_ultimo_reg_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalPedidos(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_pedido_sd_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public void ZCREA_PEDIDO_SD()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 1;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroPedidos();
            Thread.Sleep(2000);
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_pedido_sd_id_MDL_Result> datos = DALC_PedidosSD.ObtenerInstancia().ObtenerDatosIdPedido(connection, id).ToList();
            foreach (SELEC_datos_pedido_sd_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha_creacion;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_pedido_sd_valida_hora_MDL_Result> validahor = DALC_PedidosSD.ObtenerInstancia().ObtenerValidacionHoraPedido(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        string ese = folio_sam.Replace("P", "0");
                        string pe = ese.Replace("V", "0");
                        int foli = Convert.ToInt32(pe.ToString()) - 1;
                        folio_sam = "PV" + foli.ToString();
                        //List<SELEC_fol_solped_menos_MDL_Result> f = DALC_Sol_Ped.ObtenerInstancia().ObtenerFolioMenosSolped(connection, id).ToList();
                        //foreach (SELEC_fol_solped_menos_MDL_Result fo in f)
                        //{
                        //folio_sam = fo.folio_sam;
                        List<SELECT_lista_folios_pedido_sd_MDL_Result> movs = DALC_PedidosSD.ObtenerInstancia().ObtenerTodoFolioPedido(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_pedido_sd_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosPedidoSD(folio_sam);
                        }
                        //}
                    }
                    else
                    {
                        List<SELECT_lista_folios_pedido_sd_MDL_Result> movs = DALC_PedidosSD.ObtenerInstancia().ObtenerTodoFolioPedido(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_pedido_sd_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosPedidoSD(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_pedido_sd_MDL_Result> movs = DALC_PedidosSD.ObtenerInstancia().ObtenerTodoFolioPedido(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_pedido_sd_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosPedidoSD(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosPedidoSD(string folio_sam)
        {
            string folio_doc = "";
            List<SELECT_cabecera_pedidos_sd_crea_Fol_MDL1_Result> cab = DALC_PedidosSD.ObtenerInstancia().ObtenerCabeceraPedidos(connection, folio_sam).ToList();
            List<SELECT_posiciones_pedido_sd_crea_Fol_MDL_Result> pos = DALC_PedidosSD.ObtenerInstancia().ObtenerPosicionesPedidos(connection, folio_sam).ToList();
            List<SELECT_cantidades_pedido_sd_crea_Fol_MDL_Result> can = DALC_PedidosSD.ObtenerInstancia().ObtenerCantidadesPedidos(connection, folio_sam).ToList();
            List<SELECT_clientes_pedido_sd_crea_Fol_MDL_Result> cli = DALC_PedidosSD.ObtenerInstancia().ObtenerClientesPedidos(connection, folio_sam).ToList();
            List<SELECT_textos_pedidos_cabecera_sd_crea_Fol_MDL_Result> txtc = DALC_PedidosSD.ObtenerInstancia().ObtenerTextosCabPedidos(connection, folio_sam).ToList();
            List<SELECT_textos_pedidos_posiciones_sd_crea_Fol_MDL_Result> txtp = DALC_PedidosSD.ObtenerInstancia().ObtenerTextosPosPedidos(connection, folio_sam).ToList();
            Thread.Sleep(2000);
            string tota = ObtenerTotalPedidos(folio_sam);
            //Agregar las demas tablas
            int cabs = cab.Count;
            if (tota.Equals(cab.Count.ToString()))
            {
                RfcRepository rst = rfcDesti.Repository;
                IRfcFunction FUNCION = rst.CreateFunction("ZSD_CREA_PEDIDO");
                IRfcTable ca = FUNCION.GetTable("CABECERA");
                IRfcTable cl = FUNCION.GetTable("CLIENTES");
                IRfcTable po = FUNCION.GetTable("MATERIALES");
                IRfcTable cd = FUNCION.GetTable("CANTIDADES");
                IRfcTable tc = FUNCION.GetTable("TEXTOS_CAB");
                IRfcTable tp = FUNCION.GetTable("TEXTOS_POS");
                IRfcTable res = FUNCION.GetTable("RET");
                FUNCION.SetValue("CABN", cab.Count);
                FUNCION.SetValue("CLIN", cli.Count);
                FUNCION.SetValue("MATN", pos.Count);
                FUNCION.SetValue("CANN", can.Count);
                foreach (SELECT_cabecera_pedidos_sd_crea_Fol_MDL1_Result c in cab)
                {
                    try
                    {
                        ca.Append();
                        ca.SetValue("FOLIO_SAM", c.folio_sam);
                        ca.SetValue("DOC_TYPE", c.clase_documento);
                        ca.SetValue("SALES_ORG", c.organizacion_ventas);
                        ca.SetValue("DISTR_CHAN", c.canal_distribucion);
                        ca.SetValue("DIVISION", c.sector);
                        ca.SetValue("SALES_GRP", c.grupo_vendedores);
                        ca.SetValue("SALES_OFF", c.oficina_ventas);
                        ca.SetValue("REQ_DATE_H", c.fecha_entrega);
                        ca.SetValue("PRICE_DATE", c.fecha_precio);
                        ca.SetValue("PURCH_NO_C", c.referencia_cliente);
                        ca.SetValue("UNAME", c.usuario);
                        ca.SetValue("FECHA_SAM", c.fecha_creacion);
                        ca.SetValue("HORA_SAM", c.hora_creacion);
                        ca.SetValue("RECIBIDO", c.recibido);
                        ca.SetValue("PROCESADO", c.procesado);
                        ca.SetValue("ERROR", c.error);
                        ca.SetValue("FECHA_RECIBIDO", c.fecha_recibido);
                        ca.SetValue("HORA_RECIBIDO", c.hora_recibido);
                        ca.SetValue("PRICE_LIST", c.lista_precio);
                    }
                    catch (Exception) { }
                }
                foreach (SELECT_posiciones_pedido_sd_crea_Fol_MDL_Result p in pos)
                {
                    try
                    {
                        po.Append();
                        po.SetValue("FOLIO_SAM", p.folio_sam);
                        po.SetValue("ITM_NUMBER", p.posicion);
                        po.SetValue("MATERIAL", p.material);
                        po.SetValue("TARGET_QU", p.unidad_medida);
                        po.SetValue("FECHA_RECIBIDO", p.fecha_recibido);
                        po.SetValue("HORA_RECIBIDO", p.hora_recibido);
                        po.SetValue("RECIBIDO", p.recibido);
                        po.SetValue("PROCESADO", p.procesado);
                        po.SetValue("ERROR", p.error);
                        po.SetValue("UNAME", p.usuario);
                        po.SetValue("FECHA_SAM", p.fecha_creacion);
                        po.SetValue("HORA_SAM", p.hora_creacion);
                    }
                    catch (Exception) { }
                }
                foreach (SELECT_cantidades_pedido_sd_crea_Fol_MDL_Result c in can)
                {
                    try
                    {
                        cd.Append();
                        cd.SetValue("FOLIO_SAM", c.folio_sam);
                        cd.SetValue("ITM_NUMBER", c.posicion_documento);
                        cd.SetValue("REQ_DATE", c.fecha_entrega);
                        cd.SetValue("REQ_QTY", c.cantidad);
                        cd.SetValue("FECHA_RECIBIDO", c.fecha_recibido);
                        cd.SetValue("HORA_RECIBIDO", c.hora_recibido);
                        cd.SetValue("RECIBIDO", c.recibido);
                        cd.SetValue("PROCESADO", c.procesado);
                        cd.SetValue("ERROR", c.error);
                        cd.SetValue("UNAME", c.usuario);
                        cd.SetValue("FECHA_SAM", c.fecha_creacion);
                        cd.SetValue("HORA_SAM", c.hora_creacion);
                    }
                    catch (Exception) { }
                }
                foreach (SELECT_clientes_pedido_sd_crea_Fol_MDL_Result ci in cli)
                {
                    try
                    {
                        cl.Append();
                        cl.SetValue("FOLIO_SAM", ci.folio_sam);
                        cl.SetValue("PARTN_ROLE", ci.funcion_interlocutor);
                        cl.SetValue("PARTN_NUMB", ci.numero_deudor);
                        cl.SetValue("FECHA_RECIBIDO", ci.fecha_recibido);
                        cl.SetValue("HORA_RECIBIDO", ci.hora_recibido);
                        cl.SetValue("RECIBIDO", ci.recibido);
                        cl.SetValue("PROCESADO", ci.procesado);
                        cl.SetValue("ERROR", ci.error);
                        cl.SetValue("UNAME", ci.usuario);
                        cl.SetValue("FECHA_SAM", ci.fecha_creacion);
                        cl.SetValue("HORA_SAM", ci.hora_creacion);
                    }
                    catch (Exception) { }
                }
                foreach (SELECT_textos_pedidos_cabecera_sd_crea_Fol_MDL_Result txc in txtc)
                {
                    try
                    {
                        tc.Append();
                        tc.SetValue("FOLIO_SAM", txc.folio_sam);
                        tc.SetValue("INDICE", txc.linea);
                        tc.SetValue("TDLINE", txc.texto);
                        tc.SetValue("UNAME", txc.usuario);
                    }
                    catch (Exception) { }
                }
                foreach (SELECT_textos_pedidos_posiciones_sd_crea_Fol_MDL_Result txp in txtp)
                {
                    try
                    {
                        tp.Append();
                        tp.SetValue("FOLIO_SAM", txp.folio_sam);
                        tp.SetValue("POSNR", txp.posicion);
                        tp.SetValue("INDICE", txp.linea);
                        tp.SetValue("TDLINE", txp.texto);
                        tp.SetValue("UNAME", txp.usuario);
                    }
                    catch (Exception) { }
                }
                try
                {
                    FUNCION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure r in ca)
                {
                    CabeceraPedidosSD c = new CabeceraPedidosSD();
                    try
                    {
                        c.FOLIO_SAM = r.GetValue("FOLIO_SAM").ToString();
                        c.VBELN = r.GetValue("VBELN").ToString();
                        folio_doc = r.GetValue("VBELN").ToString();
                        c.RECIBIDO = r.GetValue("RECIBIDO").ToString();
                        c.PROCESADO = r.GetValue("PROCESADO").ToString();
                        c.ERROR = r.GetValue("ERROR").ToString();
                        DALC_PedidosSD.ObtenerInstancia().ActualizaPedidoCrea(connection, c);
                    }
                    catch (Exception) { }
                }
                //foreach (IRfcStructure ret in res)
                //{
                //    string me = ret.GetValue("MESSAGE").ToString();
                //}
                //foreach (IRfcStructure r in res)
                //{
                //    string mensaje = r.GetValue("MESSAGE").ToString();
                //}
                //string respuesta = FUNCION.GetValue("DOC").ToString();
                //string men = FUNCION.GetValue("MENSAJE").ToString();
                Thread.Sleep(5000);
                if (folio_doc != "")
                {
                    ZSD_HIS_PEDIDOS_DOC(folio_doc);
                }
                //ZSD_HIS_PEDIDOS();
            }
        }
        public bool ZSD_HIS_PEDIDOS()
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_HIS_PEDIDOS");
            IRfcTable CABECERA = FUNCTION.GetTable("CABECERA");
            IRfcTable POSICIONES = FUNCTION.GetTable("POSICIONES");
            IRfcTable EXPEDICION = FUNCTION.GetTable("EXPEDICION");
            IRfcTable CONDICIONES = FUNCTION.GetTable("CONDICIONES");
            IRfcTable REPARTOS = FUNCTION.GetTable("REPARTOS");
            IRfcTable TEXTOS_CAB = FUNCTION.GetTable("TEXTOS_CAB");
            IRfcTable TEXTOS_POS = FUNCTION.GetTable("TEXTOS_POS");
            IRfcTable INFORECORDS = FUNCTION.GetTable("INFORECORDS");
            IRfcTable INTERLOCUTORES = FUNCTION.GetTable("INTERLOCUTORES");
            FUNCTION.SetValue("WERKS", "1000");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_PedidosSD.ObtenerInstancia().VaciarTablasPedidosSD(connection);
            foreach (IRfcStructure cab in CABECERA)
            {
                Cabecera_Pedidos_SD ca = new Cabecera_Pedidos_SD();
                try
                {
                    ca.AUART = cab.GetValue("AUART").ToString();
                    ca.VBELN = cab.GetValue("VBELN").ToString();
                    ca.XBLNR = cab.GetValue("XBLNR").ToString();
                    ca.KUNNR = cab.GetValue("KUNNR").ToString();
                    ca.TXTPA = cab.GetValue("TXTPA").ToString();
                    ca.KUNWE = cab.GetValue("KUNWE").ToString();
                    ca.TXTPD = cab.GetValue("TXTPD").ToString();
                    ca.BSTNK = cab.GetValue("BSTNK").ToString();
                    ca.BSTDK = cab.GetValue("BSTDK").ToString();
                    ca.VKORG = cab.GetValue("VKORG").ToString();
                    ca.VTWEG = cab.GetValue("VTWEG").ToString();
                    ca.SPART = cab.GetValue("SPART").ToString();
                    ca.TXT_VTRBER = cab.GetValue("TXT_VTRBER").ToString();
                    ca.VKBUR = cab.GetValue("VKBUR").ToString();
                    ca.BEZEI = cab.GetValue("BEZEI").ToString();
                    ca.VKGRP = cab.GetValue("VKGRP").ToString();
                    ca.BEZEIGV = cab.GetValue("BEZEIGV").ToString();
                    ca.AUDAT = cab.GetValue("AUDAT").ToString();
                    ca.NETWR = cab.GetValue("NETWR").ToString();
                    ca.WAERK = cab.GetValue("WAERK").ToString();
                    ca.ERNAM = cab.GetValue("ERNAM").ToString();
                    ca.AUGRU = cab.GetValue("AUGRU").ToString();
                    ca.PRSDT = cab.GetValue("PRSDT").ToString();
                    ca.VDATU = cab.GetValue("VDATU").ToString();
                    ca.KNUMV = cab.GetValue("KNUMV").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarCabeceraPedidos(connection, ca);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure pos in POSICIONES)
            {
                Posiciones_Pedidos_SD po = new Posiciones_Pedidos_SD();
                try
                {
                    po.VBELN = pos.GetValue("VBELN").ToString();
                    po.XBLNR = pos.GetValue("XBLNR").ToString();
                    po.POSNR = pos.GetValue("POSNR").ToString();
                    po.PRGRS = pos.GetValue("PRGRS").ToString();
                    po.EDATUM = pos.GetValue("EDATUM").ToString();
                    po.MATNR = pos.GetValue("MATNR").ToString();
                    po.KWMENG = pos.GetValue("KWMENG").ToString();
                    po.VRKME = pos.GetValue("VRKME").ToString();
                    po.EPMEH = pos.GetValue("EPMEH").ToString();
                    po.ARKTX = pos.GetValue("ARKTX").ToString();
                    po.KDMAT = pos.GetValue("KDMAT").ToString();
                    po.PSTYV = pos.GetValue("PSTYV").ToString();
                    po.PROFL = pos.GetValue("PROFL").ToString();
                    po.UEPOS = pos.GetValue("UEPOS").ToString();
                    po.KPRGBZ = pos.GetValue("KPRGBZ").ToString();
                    po.ETDAT = pos.GetValue("ETDAT").ToString();
                    po.GBSTA_BEZ = pos.GetValue("GBSTA_BEZ").ToString();
                    po.ABGRU = pos.GetValue("ABGRU").ToString();
                    po.BEZEIM = pos.GetValue("BEZEIM").ToString();
                    po.LFSTA_BEZ = pos.GetValue("LFSTA_BEZ").ToString();
                    po.LFGSA_BEZ = pos.GetValue("LFGSA_BEZ").ToString();
                    po.MGAME = pos.GetValue("MGAME").ToString();
                    po.NETWR = pos.GetValue("NETWR").ToString();
                    po.WAERK = pos.GetValue("WAERK").ToString();
                    po.MWSBP = pos.GetValue("MWSBP").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarPosicionesPedidos(connection, po);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure exp in EXPEDICION)
            {
                Expedicion_Pedidos_SD ex = new Expedicion_Pedidos_SD();
                try
                {
                    ex.VBELN = exp.GetValue("VBELN").ToString();
                    ex.POSNR = exp.GetValue("POSNR").ToString();
                    ex.WERKS = exp.GetValue("WERKS").ToString();
                    ex.NAME1 = exp.GetValue("NAME1").ToString();
                    ex.LGORT = exp.GetValue("LGORT").ToString();
                    ex.LGOBE = exp.GetValue("LGOBE").ToString();
                    ex.VSTEL = exp.GetValue("VSTEL").ToString();
                    ex.VTEXT = exp.GetValue("VTEXT").ToString();
                    ex.ROUTE = exp.GetValue("ROUTE").ToString();
                    ex.BEZEIPX = exp.GetValue("BEZEIPX").ToString();
                    ex.LPRIO = exp.GetValue("LPRIO").ToString();
                    ex.BEZEIPE = exp.GetValue("BEZEIPE").ToString();
                    ex.NTGEW = exp.GetValue("NTGEW").ToString();
                    ex.GEWEI = exp.GetValue("GEWEI").ToString();
                    ex.BRGEW = exp.GetValue("BRGEW").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarExpedicionPedidos(connection, ex);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure con in CONDICIONES)
            {
                Condiciones_Pedidos_SD co = new Condiciones_Pedidos_SD();
                try
                {
                    co.KNUMV = con.GetValue("KNUMV").ToString();
                    co.KPOSN = con.GetValue("KPOSN").ToString();
                    co.STUNR = con.GetValue("STUNR").ToString();
                    co.KAPPL = con.GetValue("KAPPL").ToString();
                    co.KINAK = con.GetValue("KINAK").ToString();
                    co.KSCHL = con.GetValue("KSCHL").ToString();
                    co.VTEXT = con.GetValue("VTEXT").ToString();
                    co.KBETR = con.GetValue("KBETR").ToString();
                    co.WAERS = con.GetValue("WAERS").ToString();
                    co.KPEIN = con.GetValue("KPEIN").ToString();
                    co.KMEIN = con.GetValue("KMEIN").ToString();
                    co.KWERT = con.GetValue("KWERT").ToString();
                    co.KUMZA = con.GetValue("KUMZA").ToString();
                    co.MEINS = con.GetValue("MEINS").ToString();
                    co.KUMNE = con.GetValue("KUMNE").ToString();
                    co.KMEI1 = con.GetValue("KMEI1").ToString();
                    co.KWERT_K = con.GetValue("KWERT_K").ToString();
                    co.KWAEH = con.GetValue("KWAEH").ToString();
                    co.KSTAT = con.GetValue("KSTAT").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarCondicionesPedidos(connection, co);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure rep in REPARTOS)
            {
                Repartos_Pedidos_SD re = new Repartos_Pedidos_SD();
                try
                {
                    re.VBELN = rep.GetValue("VBELN").ToString();
                    re.POSNR = rep.GetValue("POSNR").ToString();
                    re.DELCO = rep.GetValue("DELCO").ToString();
                    re.KWMENG = rep.GetValue("KWMENG").ToString();
                    re.VRKME = rep.GetValue("VRKME").ToString();
                    re.LSMENG = rep.GetValue("LSMENG").ToString();
                    re.PRGRS = rep.GetValue("PRGRS").ToString();
                    re.EDATU = rep.GetValue("EDATU").ToString();
                    re.WMENG = rep.GetValue("WMENG").ToString();
                    re.CMENG = rep.GetValue("CMENG").ToString();
                    re.BMENG = rep.GetValue("BMENG").ToString();
                    re.VRKME_B = rep.GetValue("VRKME_B").ToString();
                    re.LIFSP = rep.GetValue("LIFSP").ToString();
                    re.ETTYP = rep.GetValue("ETTYP").ToString();
                    re.BANFN = rep.GetValue("BANFN").ToString();
                    re.BNFPO = rep.GetValue("BNFPO").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarRepartosPedidos(connection, re);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure txtcab in TEXTOS_CAB)
            {
                Textos_Cabecera_Pedidos_SD_Vis txtc = new Textos_Cabecera_Pedidos_SD_Vis();
                try
                {
                    txtc.FOLIO_SAM = txtcab.GetValue("FOLIO_SAM").ToString();
                    txtc.VBELN = txtcab.GetValue("VBELN").ToString();
                    txtc.INDICE = txtcab.GetValue("INDICE").ToString();
                    txtc.TDLINE = txtcab.GetValue("TDLINE").ToString();
                    txtc.UNAME = txtcab.GetValue("UNAME").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarTextosCabPedidos(connection, txtc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure txtpos in TEXTOS_POS)
            {
                Textos_Posiciones_Pedidos_SD_Vis txtp = new Textos_Posiciones_Pedidos_SD_Vis();
                try
                {
                    txtp.FOLIO_SAM = txtpos.GetValue("FOLIO_SAM").ToString();
                    txtp.VBELN = txtpos.GetValue("VBELN").ToString();
                    txtp.POSNR = txtpos.GetValue("POSNR").ToString();
                    txtp.INDICE = txtpos.GetValue("INDICE").ToString();
                    txtp.TDLINE = txtpos.GetValue("TDLINE").ToString();
                    txtp.UNAME = txtpos.GetValue("UNAME").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarTextosPosPedidos(connection, txtp);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure info in INFORECORDS)
            {
                Inforecords inf = new Inforecords();
                try
                {
                    inf.VKORG = info.GetValue("VKORG").ToString();
                    inf.VTWEG = info.GetValue("VTWEG").ToString();
                    inf.KUNNR = info.GetValue("KUNNR").ToString();
                    inf.MATNR = info.GetValue("MATNR").ToString();
                    inf.POSTX = info.GetValue("POSTX").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarInforecords(connection, inf);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure i in INTERLOCUTORES)
            {
                Interlocutores_pedidos_vis inter = new Interlocutores_pedidos_vis();
                try
                {
                    inter.VBELN = i.GetValue("VBELN").ToString();
                    inter.XBLNR = i.GetValue("XBLNR").ToString();
                    inter.POSNR = i.GetValue("POSNR").ToString();
                    inter.PARVW = i.GetValue("PARVW").ToString();
                    inter.KUNNR = i.GetValue("KUNNR").ToString();
                    inter.XCPDK = i.GetValue("XCPDK").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarInterlocutores(connection, inter);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSD_HIS_PEDIDOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSD_HIS_PEDIDOS", "X", "1000");
            return true;
        }
        public bool ZSD_MATERIALES(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_MATERIALES");
            IRfcTable MATERIALES = FUNCTION.GetTable("MATERIALES");
            IRfcTable TEXTOSMAT = FUNCTION.GetTable("TEXTOS_COMERCIALES");
            //FUNCTION.SetValue("WERKS", "1000");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_MaterialesSD.ObtenerInstancia().VaciarMateriales(connection);
            foreach (IRfcStructure mate in MATERIALES)
            {
                MaterialesSD mat = new MaterialesSD();
                try
                {
                    mat.MATNR = mate.GetValue("MATNR").ToString();
                    mat.WERKS = mate.GetValue("WERKS").ToString();
                    mat.MEINS = mate.GetValue("MEINS").ToString();
                    mat.BISMT = mate.GetValue("BISMT").ToString();
                    mat.MATKL = mate.GetValue("MATKL").ToString();
                    mat.MTART = mate.GetValue("MTART").ToString();
                    mat.EKGRP = mate.GetValue("EKGRP").ToString();
                    mat.XCHPF = mate.GetValue("XCHPF").ToString();
                    mat.MAKTX_ES = mate.GetValue("MAKTX_ES").ToString();
                    mat.MAKTX_EN = mate.GetValue("MAKTX_EN").ToString();
                    mat.DISMM = mate.GetValue("DISMM").ToString();
                    mat.MINBE = mate.GetValue("MINBE").ToString();
                    mat.LFRHY = mate.GetValue("LFRHY").ToString();
                    mat.FXHOR = mate.GetValue("FXHOR").ToString();
                    mat.DISPO = mate.GetValue("DISPO").ToString();
                    mat.DISLS = mate.GetValue("DISLS").ToString();
                    mat.BSTMI = mate.GetValue("BSTMI").ToString();
                    mat.BSTMA = mate.GetValue("BSTMA").ToString();
                    mat.MABST = mate.GetValue("MABST").ToString();
                    mat.BESKZ = mate.GetValue("BESKZ").ToString();
                    mat.SOBSL = mate.GetValue("SOBSL").ToString();
                    mat.WEBAZ = mate.GetValue("WEBAZ").ToString();
                    mat.PLIFZ = mate.GetValue("PLIFZ").ToString();
                    mat.EISBE = mate.GetValue("EISBE").ToString();
                    mat.EISLO = mate.GetValue("EISLO").ToString();
                    mat.QMATV = mate.GetValue("QMATV").ToString();
                    mat.QMPUR = mate.GetValue("QMPUR").ToString();
                    mat.SPART = mate.GetValue("SPART").ToString();
                    mat.MTPOS_MARA = mate.GetValue("MTPOS_MARA").ToString();
                    mat.MTVFP = mate.GetValue("MTVFP").ToString();
                    mat.PRCTR = mate.GetValue("PRCTR").ToString();
                    mat.LADGR = mate.GetValue("LADGR").ToString();
                    mat.VRKME = mate.GetValue("VRKME").ToString();
                    mat.KONDM = mate.GetValue("KONDM").ToString();
                    mat.VERSG = mate.GetValue("VERSG").ToString();
                    mat.KTGRM = mate.GetValue("KTGRM").ToString();
                    mat.MTPOS = mate.GetValue("MTPOS").ToString();
                    mat.PRODH = mate.GetValue("PRODH").ToString();
                    mat.VKORG = mate.GetValue("VKORG").ToString();
                    mat.VKGRP = mate.GetValue("VKGRP").ToString();
                    mat.VTWEG = mate.GetValue("VTWEG").ToString();
                    mat.VPRSV = mate.GetValue("VPRSV").ToString();
                    mat.VERPR = mate.GetValue("VERPR").ToString();
                    mat.STPRS = mate.GetValue("STPRS").ToString();
                    mat.PEINH = mate.GetValue("PEINH").ToString();
                    mat.BWTAR = mate.GetValue("BWTAR").ToString();
                    mat.BKLAS = mate.GetValue("BKLAS").ToString();
                    mat.TRAGR = mate.GetValue("TRAGR").ToString();
                    mat.LOEKZ = mate.GetValue("LOEKZ").ToString();
                    mat.LOEKZC = mate.GetValue("LOEKZC").ToString();
                    mat.MFRPN = mate.GetValue("MFRPN").ToString();
                    mat.WRKST = mate.GetValue("WRKST").ToString();
                    DALC_MaterialesSD.ObtenerInstancia().InsertarMaterialesVentas(connection, mat);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure tex in TEXTOSMAT)
            {
                TextoComercialMaterialesSD txt = new TextoComercialMaterialesSD();
                try
                {
                    txt.MATNR = tex.GetValue("MATNR").ToString();
                    txt.VKORG = tex.GetValue("VKORG").ToString();
                    txt.SPART = tex.GetValue("SPART").ToString();
                    txt.INDICE = tex.GetValue("INDICE").ToString();
                    txt.TDFORMAT = tex.GetValue("TDFORMAT").ToString();
                    txt.TDLINE = tex.GetValue("TDLINE").ToString();
                    DALC_MaterialesSD.ObtenerInstancia().InsertarTextosCom(connection, txt);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSD_MATERIALES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSD_MATERIALES", "X", centro);

            return true;
        }
        public bool ZSD_CLIENTES(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZCLIENTES");
            IRfcTable CLIENTES = FUNCTION.GetTable("CLIENTES");
            IRfcTable RELACION = FUNCTION.GetTable("RELACION");
            IRfcTable tab3 = FUNCTION.GetTable("VALIDACION");
            FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Clientes.obtenerInstania().VaciarRelacionInterlocutores(connection);
            DALC_Clientes.obtenerInstania().VaciarValidacion(connection);
            foreach (IRfcStructure cl in CLIENTES)
            {
                Clientes c = new Clientes();
                try
                {
                    c.KUNNR = cl.GetValue("KUNNR").ToString();
                    c.BUKRS = cl.GetValue("BUKRS").ToString();
                    DALC_Clientes.obtenerInstania().VaciarCliente(connection, c);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure cls in CLIENTES)
            {
                Clientes cl = new Clientes();
                try
                {
                    cl.KUNNR = cls.GetValue("KUNNR").ToString();
                    cl.BUKRS = cls.GetValue("BUKRS").ToString();
                    cl.VKORG = cls.GetValue("VKORG").ToString();
                    cl.VTWEG = cls.GetValue("VTWEG").ToString();
                    cl.SPART = cls.GetValue("SPART").ToString();
                    cl.ADRC = cls.GetValue("ADRC").ToString();
                    cl.DATE_FROM = cls.GetValue("DATE_FROM").ToString();
                    cl.NATION = cls.GetValue("NATION").ToString();
                    cl.DATE_TO = cls.GetValue("DATE_TO").ToString();
                    cl.TITLE = cls.GetValue("TITLE").ToString();
                    cl.NAME1 = cls.GetValue("NAME1").ToString();
                    cl.NAME2 = cls.GetValue("NAME2").ToString();
                    cl.NAME3 = cls.GetValue("NAME3").ToString();
                    cl.NAME4 = cls.GetValue("NAME4").ToString();
                    cl.CITY1 = cls.GetValue("CITY1").ToString();
                    cl.CITY2 = cls.GetValue("CITY2").ToString();
                    cl.CITY_CODE = cls.GetValue("CITY_CODE").ToString();
                    cl.CITYP_CODE = cls.GetValue("CITYP_CODE").ToString();
                    cl.HOME_CITY = cls.GetValue("HOME_CITY").ToString();
                    cl.STREET = cls.GetValue("STREET").ToString();
                    cl.HOUSE_NUM1 = cls.GetValue("HOUSE_NUM1").ToString();
                    cl.KTOKK = cls.GetValue("KTOKK").ToString();
                    cl.STCD1 = cls.GetValue("STCD1").ToString();
                    cl.ZTERM = cls.GetValue("ZTERM").ToString();
                    cl.AKONT = cls.GetValue("AKONT").ToString();
                    cl.WAERS = cls.GetValue("WAERS").ToString();
                    cl.INCO1 = cls.GetValue("INCO1").ToString();
                    cl.INCO2 = cls.GetValue("INCO2").ToString();
                    cl.VKGRP = cls.GetValue("VKGRP").ToString();
                    cl.LFABC = cls.GetValue("LFABC").ToString();
                    cl.LOEVMB = cls.GetValue("LOEVMB").ToString();
                    cl.SPERR = cls.GetValue("SPERR").ToString();
                    cl.LOEVM = cls.GetValue("LOEVM").ToString();
                    DALC_Clientes.obtenerInstania().InsertarClientes(connection, cl);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure re in RELACION)
            {
                RelacionInterlocutores ri = new RelacionInterlocutores();
                try
                {
                    ri.KUNNR = re.GetValue("KUNNR").ToString();
                    ri.VKORG = re.GetValue("VKORG").ToString();
                    ri.VTWEG = re.GetValue("VTWEG").ToString();
                    ri.SPART = re.GetValue("SPART").ToString();
                    ri.PARVW = re.GetValue("PARVW").ToString();
                    ri.KUNN2 = re.GetValue("KUNN2").ToString();
                    DALC_Clientes.obtenerInstania().InsertarRelacion(connection, ri);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure v in tab3)
            {
                Validacion va = new Validacion();
                try
                {
                    va.KSCHL = v.GetValue("KSCHL").ToString();
                    va.VKORG = v.GetValue("VKORG").ToString();
                    va.VTWEG = v.GetValue("VTWEG").ToString();
                    va.VKGRP = v.GetValue("VKGRP").ToString();
                    va.PLTYP = v.GetValue("PLTYP").ToString();
                    va.KUNNR = v.GetValue("KUNNR").ToString();
                    va.MATNR = v.GetValue("MATNR").ToString();
                    va.KFRST = v.GetValue("KFRST").ToString();
                    va.DATBI = v.GetValue("DATBI").ToString();
                    va.DATAB = v.GetValue("DATAB").ToString();
                    va.KBSTAT = v.GetValue("KBSTAT").ToString();
                    va.KNUMH = v.GetValue("KNUMH").ToString();
                    DALC_Clientes.obtenerInstania().InsertarValidacion(connection, va);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCLIENTES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCLIENTES", "X", centro);
            return true;
        }
        public bool ZSD_INTERLOCUTORES(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_INTERLOCUTORES");
            IRfcTable INTERLOCUTORES = FUNCTION.GetTable("INTERLOCUTORES");
            IRfcTable SOCIEDADES = FUNCTION.GetTable("SOCIEDADES");
            IRfcTable ORG_VENTAS = FUNCTION.GetTable("ORG_VENTAS");
            FUNCTION.SetValue("WERKS", "1000");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Interlocutores.ObtenerInstancia().VaciarTablasInterlocutores(connection);
            foreach (IRfcStructure i in INTERLOCUTORES)
            {
                Interlocutores inter = new Interlocutores();
                try
                {
                    inter.KUNNR = i.GetValue("KUNNR").ToString();
                    inter.VKORG = i.GetValue("VKORG").ToString();
                    inter.VTWEG = i.GetValue("VTWEG").ToString();
                    inter.SPART = i.GetValue("SPART").ToString();
                    inter.PARVW = i.GetValue("PARVW").ToString();
                    inter.PARZA = i.GetValue("PARZA").ToString();
                    inter.KUNN2 = i.GetValue("KUNN2").ToString();
                    inter.LIFNR = i.GetValue("LIFNR").ToString();
                    inter.PERNR = i.GetValue("PERNR").ToString();
                    inter.PARNR = i.GetValue("PARNR").ToString();
                    inter.KNREF = i.GetValue("KNREF").ToString();
                    inter.DEFPA = i.GetValue("DEFPA").ToString();
                    DALC_Interlocutores.ObtenerInstancia().InsertarInterlocutores(connection, inter);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure s in SOCIEDADES)
            {
                Sociedades soc = new Sociedades();
                try
                {
                    soc.BUKRS = s.GetValue("BUKRS").ToString();
                    soc.BUTXT_ES = s.GetValue("BUTXT_ES").ToString();
                    soc.BUTXT_EN = s.GetValue("BUTXT_EN").ToString();
                    soc.ORT01 = s.GetValue("ORT01").ToString();
                    soc.WAERS = s.GetValue("WAERS").ToString();
                    DALC_Interlocutores.ObtenerInstancia().InsertarSociedades(connection, soc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure o in ORG_VENTAS)
            {
                Org_ventas org = new Org_ventas();
                try
                {
                    org.VKORG = o.GetValue("VKORG").ToString();
                    org.VTEXT = o.GetValue("VTEXT").ToString();
                    DALC_Interlocutores.ObtenerInstancia().InsertarOrgVentas(connection, org);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSD_INTERLOCUTORES", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSD_INTERLOCUTORES", "X", centro);
            return true;
        }
        public bool ZSD_FLUJO_DOC(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_FLUJO_DOC");
            IRfcTable REP = FUNCTION.GetTable("REP");
            FUNCTION.SetValue("F_INI", Convert.ToDateTime("01-01-2018"));
            FUNCTION.SetValue("F_FIN", Convert.ToDateTime("31-12-2018"));
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Flujo.ObtenerInstancia().VaciarFlujo(connection);
            foreach (IRfcStructure f in REP)
            {
                FlujoSD fu = new FlujoSD();
                try
                {
                    fu.VBELN = f.GetValue("VBELN").ToString();
                    fu.POSNR = f.GetValue("POSNR").ToString();
                    fu.ERDAT = f.GetValue("ERDAT").ToString();
                    fu.WAERK = f.GetValue("WAERK").ToString();
                    fu.VKORG = f.GetValue("VKORG").ToString();
                    fu.VTWEG = f.GetValue("VTWEG").ToString();
                    fu.SPART = f.GetValue("SPART").ToString();
                    fu.VKGRP = f.GetValue("VKGRP").ToString();
                    fu.VKBUR = f.GetValue("VKBUR").ToString();
                    fu.AUART = f.GetValue("AUART").ToString();
                    fu.KUNNR = f.GetValue("KUNNR").ToString();
                    fu.KOSTL = f.GetValue("KOSTL").ToString();
                    fu.VGTYP = f.GetValue("VGTYP").ToString();
                    fu.XBLNR = f.GetValue("XBLNR").ToString();
                    fu.ZUONR = f.GetValue("ZUONR").ToString();
                    fu.MATNR = f.GetValue("MATNR").ToString();
                    fu.MATWA = f.GetValue("MATWA").ToString();
                    fu.PMATN = f.GetValue("PMATN").ToString();
                    fu.CHARG = f.GetValue("CHARG").ToString();
                    fu.MATKL = f.GetValue("MATKL").ToString();
                    fu.ARKTX = f.GetValue("ARKTX").ToString();
                    fu.PRODH = f.GetValue("PRODH").ToString();
                    fu.ZMENG = f.GetValue("ZMENG").ToString();
                    fu.ZIEME = f.GetValue("ZIEME").ToString();
                    fu.NETWR = f.GetValue("NETWR").ToString();
                    fu.WERKS = f.GetValue("WERKS").ToString();
                    fu.LGORT = f.GetValue("LGORT").ToString();
                    fu.ROUTE = f.GetValue("ROUTE").ToString();
                    fu.AUFNR = f.GetValue("AUFNR").ToString();
                    fu.VBELV_LI = f.GetValue("VBELV_LI").ToString();
                    fu.POSNR_LI = f.GetValue("POSNR_LI").ToString();
                    fu.RFMNG = f.GetValue("RFMNG").ToString();
                    fu.MEINS = f.GetValue("MEINS").ToString();
                    fu.VBELV_8 = f.GetValue("VBELV_8").ToString();
                    fu.POSNR_8 = f.GetValue("POSNR_8").ToString();
                    fu.RFMNG_8 = f.GetValue("RFMNG_8").ToString();
                    fu.MEINS_8 = f.GetValue("MEINS_8").ToString();
                    fu.VBELV_R = f.GetValue("VBELV_R").ToString();
                    fu.POSNR_R = f.GetValue("POSNR_R").ToString();
                    fu.RFMNG_R = f.GetValue("RFMNG_R").ToString();
                    fu.MEINS_R = f.GetValue("MEINS_R").ToString();
                    fu.VBELV_M = f.GetValue("VBELV_M").ToString();
                    fu.POSNR_M = f.GetValue("POSNR_M").ToString();
                    fu.RFMNG_M = f.GetValue("RFMNG_M").ToString();
                    fu.MEINS_M = f.GetValue("MEINS_M").ToString();
                    fu.KUWEV_KUNNR = f.GetValue("KUWEV_KUNNR").ToString();
                    fu.LIKP_BTGEW = f.GetValue("LIKP_BTGEW").ToString();
                    fu.LIKP_LDDAT = f.GetValue("LIKP_LDDAT").ToString();
                    fu.LIKP_LIFEX = f.GetValue("LIKP_LIFEX").ToString();
                    fu.LIKP_NTGEW = f.GetValue("LIKP_NTGEW").ToString();
                    fu.LIKP_TDDAT = f.GetValue("LIKP_TDDAT").ToString();
                    fu.LIKP_VSTEL = f.GetValue("LIKP_VSTEL").ToString();
                    fu.LIPS_CHARG = f.GetValue("LIPS_CHARG").ToString();
                    fu.LIPS_GEWEI = f.GetValue("LIPS_GEWEI").ToString();
                    fu.LIPSD_G_LFIMG = f.GetValue("LIPSD_G_LFIMG").ToString();
                    fu.TVROT_BEZEI = f.GetValue("TVROT_BEZEI").ToString();
                    fu.VBAK_ERNAM = f.GetValue("VBAK_ERNAM").ToString();
                    fu.VBKD_BSTKD = f.GetValue("VBKD_BSTKD").ToString();
                    fu.VFKK_FKART = f.GetValue("VFKK_FKART").ToString();
                    fu.VFKK_FKNUM = f.GetValue("VFKK_FKNUM").ToString();
                    fu.VFKK_STABR = f.GetValue("VFKK_STABR").ToString();
                    fu.VFKP_NETWR = f.GetValue("VFKP_NETWR").ToString();
                    fu.VFKP_EBELN = f.GetValue("VFKP_EBELN").ToString();
                    fu.VTTK_ADD01 = f.GetValue("VTTK_ADD01").ToString();
                    fu.VTTK_ADD02 = f.GetValue("VTTK_ADD02").ToString();
                    fu.VTTK_ADD03 = f.GetValue("VTTK_ADD03").ToString();
                    fu.VTTK_ADD04 = f.GetValue("VTTK_ADD04").ToString();
                    fu.VTTK_BFART = f.GetValue("VTTK_BFART").ToString();
                    fu.VTTK_EXTI1 = f.GetValue("VTTK_EXTI1").ToString();
                    fu.VTTK_EXTI2 = f.GetValue("VTTK_EXTI2").ToString();
                    fu.VTTK_ROUTE = f.GetValue("VTTK_ROUTE").ToString();
                    fu.VTTK_SHTYP = f.GetValue("VTTK_SHTYP").ToString();
                    fu.VTTK_SIGNI = f.GetValue("VTTK_SIGNI").ToString();
                    fu.VTTK_STTRG = f.GetValue("VTTK_STTRG").ToString();
                    fu.VTTK_TDLNR = f.GetValue("VTTK_TDLNR").ToString();
                    fu.VTTK_TPBEZ = f.GetValue("VTTK_TPBEZ").ToString();
                    fu.VTTK_VSART = f.GetValue("VTTK_VSART").ToString();
                    fu.ACTUAL_START_DATE = f.GetValue("ACTUAL_START_DATE").ToString();
                    fu.ACTUAL_FINISH_DATE = f.GetValue("ACTUAL_FINISH_DATE").ToString();
                    fu.SYSTEM_STATUS = f.GetValue("SYSTEM_STATUS").ToString();
                    fu.KALAB = f.GetValue("KALAB").ToString();
                    fu.FLAG = f.GetValue("FLAG(1)").ToString();
                    fu.NAME1 = f.GetValue("NAME1").ToString();
                    fu.AUDAT = f.GetValue("AUDAT").ToString();
                    DALC_Flujo.ObtenerInstancia().InsertarFlujo(connection, fu);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSD_FLUJO_DOC", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSD_FLUJO_DOC", "X", centro);
            return true;
        }
        public bool ZSD_AVISOS_VENTA(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_AVISOS_VENTA");
            IRfcTable AVISOS = FUNCTION.GetTable("T_AVISOS_VEND");
            FUNCTION.SetValue("GPVEND", "");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Vendedores.ObtenerInstancia().VaciarTablaAvisosVendedor(connection);
            foreach (IRfcStructure a in AVISOS)
            {
                Avisos_vendedor av = new Avisos_vendedor();
                try
                {
                    av.VKGRP = a.GetValue("VKGRP").ToString();
                    av.BEZEI = a.GetValue("BEZEI").ToString();
                    av.TEXTO1 = a.GetValue("TEXTO1").ToString();
                    av.TEXTO2 = a.GetValue("TEXTO2").ToString();
                    av.TEXTO3 = a.GetValue("TEXTO3").ToString();
                    av.FCREACION = a.GetValue("FCREACION").ToString();
                    av.HCREACION = a.GetValue("HCREACION").ToString();
                    DALC_Vendedores.ObtenerInstancia().InsertarAvisosVendedor(connection, av);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSD_AVISOS_VENTA", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSD_AVISOS_VENTA", "X", centro);
            return true;
        }
        public bool ZMM_DOC_INVENTARIOS(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZMM_DOC_INVENTARIOS");
            IRfcTable CAB = FUNCTION.GetTable("CABECERA");
            IRfcTable POS = FUNCTION.GetTable("POSICIONES");
            FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Documentos_inventario.obtenerInstancia().VaciarDocumentosInventario(connection, centro);
            foreach (IRfcStructure a in CAB)
            {
                Cabecera_documentos_inventario cab = new Cabecera_documentos_inventario();
                try
                {
                    cab.IBLNR = a.GetValue("IBLNR").ToString();
                    cab.FOLIO_SAM = a.GetValue("FOLIO_SAM").ToString();
                    cab.GJAHR = a.GetValue("GJAHR").ToString();
                    cab.VGART = a.GetValue("VGART").ToString();
                    cab.WERKS = a.GetValue("WERKS").ToString();
                    cab.LGORT = a.GetValue("LGORT").ToString();
                    cab.SOBKZ = a.GetValue("SOBKZ").ToString();
                    cab.BLDAT = a.GetValue("BLDAT").ToString();
                    cab.GIDAT = a.GetValue("GIDAT").ToString();
                    cab.ZLDAT = a.GetValue("ZLDAT").ToString();
                    cab.BUDAT = a.GetValue("BUDAT").ToString();
                    cab.MONAT = a.GetValue("MONAT").ToString();
                    cab.USNAM = a.GetValue("USNAM").ToString();
                    DALC_Documentos_inventario.obtenerInstancia().InsertarCabeceraDocumentos(connection, cab);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure p in POS)
            {
                Posiciones_documento_inventario pos = new Posiciones_documento_inventario();
                try
                {
                    pos.IBLNR = p.GetValue("IBLNR").ToString();
                    pos.FOLIO_SAM = p.GetValue("FOLIO_SAM").ToString();
                    pos.ZEILI = p.GetValue("ZEILI").ToString();
                    pos.MATNR = p.GetValue("MATNR").ToString();
                    pos.WERKS = p.GetValue("WERKS").ToString();
                    pos.LGORT = p.GetValue("LGORT").ToString();
                    pos.CHARG = p.GetValue("CHARG").ToString();
                    pos.SOBKZ = p.GetValue("SOBKZ").ToString();
                    pos.BSTAR = p.GetValue("BSTAR").ToString();
                    pos.KDAUF = p.GetValue("KDAUF").ToString();
                    pos.KDPOS = p.GetValue("KDPOS").ToString();
                    pos.KDEIN = p.GetValue("KDEIN").ToString();
                    pos.LIFNR = p.GetValue("LIFNR").ToString();
                    pos.KUNNR = p.GetValue("KUNNR").ToString();
                    pos.MEINS = p.GetValue("MEINS").ToString();
                    DALC_Documentos_inventario.obtenerInstancia().InsertarPosicionesDocumentosInventario(connection, pos);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_DOC_INVENTARIOS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_DOC_INVENTARIOS", "X", centro);
            return true;
        }
        public bool ZCATALOGOS_SD(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZCATALOGOS_SD");
            IRfcTable ORGVEN = FUNCTION.GetTable("ORG_VENTAS");
            IRfcTable CAN = FUNCTION.GetTable("CANAL_DISTRIBUCION");
            IRfcTable SOC = FUNCTION.GetTable("SOCIEDADES");
            IRfcTable CEN = FUNCTION.GetTable("CENTRO_BENEFICIO");
            IRfcTable COS = FUNCTION.GetTable("CENTRO_COSTO");
            IRfcTable CUE = FUNCTION.GetTable("CUENTA_MAYOR");
            IRfcTable SEC = FUNCTION.GetTable("SECTOR");
            IRfcTable CLA = FUNCTION.GetTable("CLASE_PEDIDO");
            IRfcTable OFI = FUNCTION.GetTable("OFICINA_VENTAS");
            IRfcTable GRU = FUNCTION.GetTable("GRUPO_VENDEDORES");
            IRfcTable LIS = FUNCTION.GetTable("LISTA_PRECIO");
            FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_Catalogos_SD.ObtenerInstancia().VaciarTablasCatalogo(connection);
            foreach (IRfcStructure o in ORGVEN)
            {
                Organizacion_Ventas org = new Organizacion_Ventas();
                try
                {
                    org.VKORG = o.GetValue("VKORG").ToString();
                    org.VTEXT = o.GetValue("VTEXT").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarOrgVentas(connection, org);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure c in CAN)
            {
                Canal_Distribucion can = new Canal_Distribucion();
                try
                {
                    can.VKORG = c.GetValue("VKORG").ToString();
                    can.VTWEG = c.GetValue("VTWEG").ToString();
                    can.VTEXT_ES = c.GetValue("VTEXT_ES").ToString();
                    can.VTEXT_EN = c.GetValue("VTEXT_EN").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarCanalD(connection, can);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure s in SOC)
            {
                Sociedades soc = new Sociedades();
                try
                {
                    soc.BUKRS = s.GetValue("BUKRS").ToString();
                    soc.BUTXT_ES = s.GetValue("BUTXT_ES").ToString();
                    soc.BUTXT_EN = s.GetValue("BUTXT_EN").ToString();
                    soc.ORT01 = s.GetValue("ORT01").ToString();
                    soc.WAERS = s.GetValue("WAERS").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarSociedades(connection, soc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure cb in CEN)
            {
                CentroBeneficio cenb = new CentroBeneficio();
                try
                {
                    cenb.BUKRS = cb.GetValue("BUKRS").ToString();
                    cenb.KOKRS = cb.GetValue("KOKRS").ToString();
                    cenb.PRCTR = cb.GetValue("PRCTR").ToString();
                    cenb.KTEXT = cb.GetValue("KTEXT").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarCentroB(connection, cenb);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure cc in COS)
            {
                CentroCoste cco = new CentroCoste();
                try
                {
                    cco.KOKRS = cc.GetValue("KOKRS").ToString();
                    cco.KOSTL = cc.GetValue("KOSTL").ToString();
                    cco.KTEXT_ES = cc.GetValue("KTEXT_ES").ToString();
                    cco.KTEXT_EN = cc.GetValue("KTEXT_EN").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarCentroC(connection, cco);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure cm in CUE)
            {
                CuentaMayor cuen = new CuentaMayor();
                try
                {
                    cuen.SAKNR = cm.GetValue("SAKNR").ToString();
                    cuen.KTOPL = cm.GetValue("KTOPL").ToString();
                    cuen.TXT50_ES = cm.GetValue("TXT50_ES").ToString();
                    cuen.TXT50_EN = cm.GetValue("TXT50_EN").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarCuentaM(connection, cuen);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure se in SEC)
            {
                Sector s = new Sector();
                try
                {
                    s.SPART = se.GetValue("SPART").ToString();
                    s.VTEXT_ES = se.GetValue("VTEXT_ES").ToString();
                    s.VTEXT_EN = se.GetValue("VTEXT_EN").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarSector(connection, s);
                }
                catch (Exception) { return false; }
            }
            //foreach (IRfcStructure cp in CLA)
            //{
            //    ClasePedido c = new ClasePedido();
            //    try
            //    {
            //        c.AUART = cp.GetValue("AUART").ToString();
            //        c.BEZEI = cp.GetValue("BEZEI").ToString();
            //        DALC_Catalogos_SD.ObtenerInstancia().InsertarClaseP(connection, c);
            //    }
            //    catch (Exception) { return false; }
            //}
            foreach (IRfcStructure o in OFI)
            {
                OficinaVentas ofi = new OficinaVentas();
                try
                {
                    ofi.VKBUR = o.GetValue("VKBUR").ToString();
                    ofi.BEZEI = o.GetValue("BEZEI").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarOficinaV(connection, ofi);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure g in GRU)
            {
                GrupoVendedores gv = new GrupoVendedores();
                try
                {
                    gv.VKGRP = g.GetValue("VKGRP").ToString();
                    gv.BEZEI = g.GetValue("BEZEI").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarGrupoV(connection, gv);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure l in LIS)
            {
                ListaPrecios li = new ListaPrecios();
                try
                {
                    li.PLTYP = l.GetValue("PLTYP").ToString();
                    li.PTEXT = l.GetValue("PTEXT").ToString();
                    DALC_Catalogos_SD.ObtenerInstancia().InsertarListaP(connection, li);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGOS_SD", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGOS_SD", "X", centro);
            return true;
        }
        //*****************Conteo de inventario*************************
        public string ObtenerUltimoRegistroDOCCONTEO()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contador_sd_ultimo_reg_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalConteos(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_conteo_sd_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public void ZGUARDACONT_DOC_INV_CREA()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 1;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroDOCCONTEO();
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_contador_sd_id_MDL_Result> datos = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerDatosIdConteo(connection, id).ToList();
            foreach (SELEC_datos_contador_sd_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_conteo_sd_valida_hora_MDL_Result> validahor = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerValidacionHoraConteo(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        string ese = folio_sam.Replace("D", "0");
                        string pe = ese.Replace("I", "0");
                        int foli = Convert.ToInt32(pe.ToString()) - 1;
                        folio_sam = "DI" + foli.ToString();
                        List<SELECT_lista_folios_conteo_sd_MDL_Result> movs = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerTodoFolioConteo(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_conteo_sd_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosConteo(folio_sam);
                        }
                        //}
                    }
                    else
                    {
                        List<SELECT_lista_folios_conteo_sd_MDL_Result> movs = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerTodoFolioConteo(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_conteo_sd_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosConteo(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_conteo_sd_MDL_Result> movs = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerTodoFolioConteo(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_conteo_sd_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosConteo(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosConteo(string folio_sam)
        {
            List<SELECT_cabecera_recuento_inventario_crea_Fol_MDL_Result> cab = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerCabeceraConteo(connection, folio_sam).ToList();
            List<SELECT_posiciones_recuento_inventario_crea_Fol_MDL_Result> pos = DALC_Conteo_Inventario.ObtenerInstancia().ObtenerPosicionesConteo(connection, folio_sam).ToList();
            Thread.Sleep(2000);
            string tota = ObtenerTotalConteos(folio_sam);
            int cabs = cab.Count;
            if (tota.Equals(cab.Count.ToString()))
            {
                RfcRepository rst = rfcDesti.Repository;
                IRfcFunction FUNCION = rst.CreateFunction("ZGUARDACONT_DOC_INV_CREA");
                IRfcTable ca = FUNCION.GetTable("CAB_DOC");
                IRfcTable po = FUNCION.GetTable("POS_DOC");
                IRfcTable rca = FUNCION.GetTable("CAB_DOC_R");
                IRfcTable rpo = FUNCION.GetTable("POS_DOC_R");
                FUNCION.SetValue("COUNT_CAB_DOC", cab.Count);
                FUNCION.SetValue("COUNT_POS_DOC", pos.Count);
                foreach (SELECT_cabecera_recuento_inventario_crea_Fol_MDL_Result c in cab)
                {
                    try
                    {
                        ca.Append();
                        ca.SetValue("FOLIO_OFF", c.folio_sam);
                        ca.SetValue("UZEIT", c.hora_dia);
                        ca.SetValue("DATUM", c.fecha);
                        ca.SetValue("IBLNR_REC", c.num_doc_rec_inventario);
                        ca.SetValue("WERKS", c.centro);
                        ca.SetValue("LGORT", c.almacen);
                        ca.SetValue("LGOBE", c.descripcion_almacen);
                        ca.SetValue("XZAEL", c.num_conteo);
                        ca.SetValue("IBLNRSAM", c.folio_sam_doc_inventario);
                        ca.SetValue("IBLNR", c.num_doc_inventario);
                        ca.SetValue("XUBNAME", c.usuario);
                        ca.SetValue("RECIBIDO", c.recibido);
                        ca.SetValue("PROCESADO", c.procesado);
                        ca.SetValue("ERROR", c.error);
                    }
                    catch (Exception) { }
                }
                foreach (SELECT_posiciones_recuento_inventario_crea_Fol_MDL_Result p in pos)
                {
                    try
                    {
                        po.Append();
                        po.SetValue("FOLIO_OFF", p.folio_sam);
                        po.SetValue("DZEILE", p.posicion);
                        po.SetValue("UZEIT", p.hora_dia);
                        po.SetValue("DATUM", p.fecha);
                        po.SetValue("IBLNR_REC", p.num_doc_rec_inventario);
                        po.SetValue("IBLNRSAM", p.folio_sam_doc_inventario);
                        po.SetValue("IBLNR", p.num_doc_inventario);
                        po.SetValue("WERKS", p.centro);
                        po.SetValue("LGORT", p.almacen);
                        po.SetValue("LGOBE", p.descripcion_almacen);
                        po.SetValue("CHARG", p.lote);
                        po.SetValue("MATKL", p.grupo_articulos);
                        po.SetValue("ETI", p.num_etiqueta);
                        po.SetValue("MATNR", p.material);
                        po.SetValue("MAKTX", p.descripcion_ES);
                        po.SetValue("MENGE", p.cantidad);
                        po.SetValue("CONFIMARCION", p.confirmacion);
                        po.SetValue("XUBNAME", p.usuario);
                        po.SetValue("RECIBIDO", p.recibido);
                        po.SetValue("PROCESADO", p.procesado);
                        po.SetValue("ERROR", p.error);
                        //po.SetValue("MEINS", p.posicion);
                    }
                    catch (Exception) { }
                }
                try
                {
                    FUNCION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure r in rca)
                {
                    Cabecera_Recuento_Inventario c = new Cabecera_Recuento_Inventario();
                    try
                    {
                        c.FOLIO_SAM = r.GetValue("FOLIO_OFF").ToString();
                        c.RECIBIDO = r.GetValue("RECIBIDO").ToString();
                        c.PROCESADO = r.GetValue("PROCESADO").ToString();
                        c.ERROR = r.GetValue("ERROR").ToString();
                        DALC_Conteo_Inventario.ObtenerInstancia().ActualizaConteoCrea(connection, c);
                    }
                    catch (Exception) { }
                }
            }
        }
        public bool ZSD_FLUJO_VENCIMIENTO_DEUDOR(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_FLUJO_VENCIMIENTO_DEUDOR");
            IRfcTable FLUJO = FUNCTION.GetTable("FLUJO");
            //FUNCTION.SetValue("WERKS", "1000");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            DALC_FlujoDeudores.ObtenerInstancia().VaciarTablaFlujo(connection);
            foreach (IRfcStructure fu in FLUJO)
            {
                FlujoDeudores f = new FlujoDeudores();
                try
                {
                    f.KUNNR = fu.GetValue("KUNNR").ToString();
                    f.GJAHR = fu.GetValue("GJAHR").ToString();
                    f.BUDAT = fu.GetValue("BUDAT").ToString();
                    f.WAERS = fu.GetValue("WAERS").ToString();
                    f.MONAT = fu.GetValue("MONAT").ToString();
                    f.DMBTR = fu.GetValue("DMBTR").ToString();
                    f.ZFBDT = fu.GetValue("ZFBDT").ToString();
                    f.ZBD3T = fu.GetValue("ZBD3T").ToString();
                    f.VBELN = fu.GetValue("VBELN").ToString();
                    f.NAME1 = fu.GetValue("NAME1").ToString();
                    f.VKGRP = fu.GetValue("VKGRP").ToString();
                    f.BEZEI = fu.GetValue("BEZEI").ToString();
                    DALC_FlujoDeudores.ObtenerInstancia().IngresarFlujoDeudores(connection, f);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSD_FLUJO_VENCIMIENTO_DEUDOR", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZSD_FLUJO_VENCIMIENTO_DEUDOR", "X", centro);
            return true;
        }
        public bool ZSD_HIS_PEDIDOS_DOC(string documento)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZSD_HIS_PEDIDOS_DOC");
            IRfcTable CAB = FUNCTION.GetTable("CABECERA");
            IRfcTable POS = FUNCTION.GetTable("POSICIONES");
            IRfcTable EXP = FUNCTION.GetTable("EXPEDICION");
            IRfcTable REP = FUNCTION.GetTable("REPARTOS");
            IRfcTable TXC = FUNCTION.GetTable("TEXTOS_CAB");
            IRfcTable TXP = FUNCTION.GetTable("TEXTOS_POS");
            IRfcTable INT = FUNCTION.GetTable("INTERLOCUTORES");
            FUNCTION.SetValue("VBELN", documento);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure cab in CAB)
            {
                Cabecera_Pedidos_SD ca = new Cabecera_Pedidos_SD();
                try
                {
                    ca.AUART = cab.GetValue("AUART").ToString();
                    ca.VBELN = cab.GetValue("VBELN").ToString();
                    ca.XBLNR = cab.GetValue("XBLNR").ToString();
                    ca.KUNNR = cab.GetValue("KUNNR").ToString();
                    ca.TXTPA = cab.GetValue("TXTPA").ToString();
                    ca.KUNWE = cab.GetValue("KUNWE").ToString();
                    ca.TXTPD = cab.GetValue("TXTPD").ToString();
                    ca.BSTNK = cab.GetValue("BSTNK").ToString();
                    ca.BSTDK = cab.GetValue("BSTDK").ToString();
                    ca.VKORG = cab.GetValue("VKORG").ToString();
                    ca.VTWEG = cab.GetValue("VTWEG").ToString();
                    ca.SPART = cab.GetValue("SPART").ToString();
                    ca.TXT_VTRBER = cab.GetValue("TXT_VTRBER").ToString();
                    ca.VKBUR = cab.GetValue("VKBUR").ToString();
                    ca.BEZEI = cab.GetValue("BEZEI").ToString();
                    ca.VKGRP = cab.GetValue("VKGRP").ToString();
                    ca.BEZEIGV = cab.GetValue("BEZEIGV").ToString();
                    ca.AUDAT = cab.GetValue("AUDAT").ToString();
                    ca.NETWR = cab.GetValue("NETWR").ToString();
                    ca.WAERK = cab.GetValue("WAERK").ToString();
                    ca.ERNAM = cab.GetValue("ERNAM").ToString();
                    ca.AUGRU = cab.GetValue("AUGRU").ToString();
                    ca.PRSDT = cab.GetValue("PRSDT").ToString();
                    ca.VDATU = cab.GetValue("VDATU").ToString();
                    ca.KNUMV = cab.GetValue("KNUMV").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarCabeceraPedidos(connection, ca);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure pos in POS)
            {
                Posiciones_Pedidos_SD po = new Posiciones_Pedidos_SD();
                try
                {
                    po.VBELN = pos.GetValue("VBELN").ToString();
                    po.XBLNR = pos.GetValue("XBLNR").ToString();
                    po.POSNR = pos.GetValue("POSNR").ToString();
                    po.PRGRS = pos.GetValue("PRGRS").ToString();
                    po.EDATUM = pos.GetValue("EDATUM").ToString();
                    po.MATNR = pos.GetValue("MATNR").ToString();
                    po.KWMENG = pos.GetValue("KWMENG").ToString();
                    po.VRKME = pos.GetValue("VRKME").ToString();
                    po.EPMEH = pos.GetValue("EPMEH").ToString();
                    po.ARKTX = pos.GetValue("ARKTX").ToString();
                    po.KDMAT = pos.GetValue("KDMAT").ToString();
                    po.PSTYV = pos.GetValue("PSTYV").ToString();
                    po.PROFL = pos.GetValue("PROFL").ToString();
                    po.UEPOS = pos.GetValue("UEPOS").ToString();
                    po.KPRGBZ = pos.GetValue("KPRGBZ").ToString();
                    po.ETDAT = pos.GetValue("ETDAT").ToString();
                    po.GBSTA_BEZ = pos.GetValue("GBSTA_BEZ").ToString();
                    po.ABGRU = pos.GetValue("ABGRU").ToString();
                    po.BEZEIM = pos.GetValue("BEZEIM").ToString();
                    po.LFSTA_BEZ = pos.GetValue("LFSTA_BEZ").ToString();
                    po.LFGSA_BEZ = pos.GetValue("LFGSA_BEZ").ToString();
                    po.MGAME = pos.GetValue("MGAME").ToString();
                    po.NETWR = pos.GetValue("NETWR").ToString();
                    po.WAERK = pos.GetValue("WAERK").ToString();
                    po.MWSBP = pos.GetValue("MWSBP").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarPosicionesPedidos(connection, po);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure exp in EXP)
            {
                Expedicion_Pedidos_SD ex = new Expedicion_Pedidos_SD();
                try
                {
                    ex.VBELN = exp.GetValue("VBELN").ToString();
                    ex.POSNR = exp.GetValue("POSNR").ToString();
                    ex.WERKS = exp.GetValue("WERKS").ToString();
                    ex.NAME1 = exp.GetValue("NAME1").ToString();
                    ex.LGORT = exp.GetValue("LGORT").ToString();
                    ex.LGOBE = exp.GetValue("LGOBE").ToString();
                    ex.VSTEL = exp.GetValue("VSTEL").ToString();
                    ex.VTEXT = exp.GetValue("VTEXT").ToString();
                    ex.ROUTE = exp.GetValue("ROUTE").ToString();
                    ex.BEZEIPX = exp.GetValue("BEZEIPX").ToString();
                    ex.LPRIO = exp.GetValue("LPRIO").ToString();
                    ex.BEZEIPE = exp.GetValue("BEZEIPE").ToString();
                    ex.NTGEW = exp.GetValue("NTGEW").ToString();
                    ex.GEWEI = exp.GetValue("GEWEI").ToString();
                    ex.BRGEW = exp.GetValue("BRGEW").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarExpedicionPedidos(connection, ex);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure rep in REP)
            {
                Repartos_Pedidos_SD re = new Repartos_Pedidos_SD();
                try
                {
                    re.VBELN = rep.GetValue("VBELN").ToString();
                    re.POSNR = rep.GetValue("POSNR").ToString();
                    re.DELCO = rep.GetValue("DELCO").ToString();
                    re.KWMENG = rep.GetValue("KWMENG").ToString();
                    re.VRKME = rep.GetValue("VRKME").ToString();
                    re.LSMENG = rep.GetValue("LSMENG").ToString();
                    re.PRGRS = rep.GetValue("PRGRS").ToString();
                    re.EDATU = rep.GetValue("EDATU").ToString();
                    re.WMENG = rep.GetValue("WMENG").ToString();
                    re.CMENG = rep.GetValue("CMENG").ToString();
                    re.BMENG = rep.GetValue("BMENG").ToString();
                    re.VRKME_B = rep.GetValue("VRKME_B").ToString();
                    re.LIFSP = rep.GetValue("LIFSP").ToString();
                    re.ETTYP = rep.GetValue("ETTYP").ToString();
                    re.BANFN = rep.GetValue("BANFN").ToString();
                    re.BNFPO = rep.GetValue("BNFPO").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarRepartosPedidos(connection, re);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure txtcab in TXC)
            {
                Textos_Cabecera_Pedidos_SD_Vis txtc = new Textos_Cabecera_Pedidos_SD_Vis();
                try
                {
                    txtc.FOLIO_SAM = txtcab.GetValue("FOLIO_SAM").ToString();
                    txtc.VBELN = txtcab.GetValue("VBELN").ToString();
                    txtc.INDICE = txtcab.GetValue("INDICE").ToString();
                    txtc.TDLINE = txtcab.GetValue("TDLINE").ToString();
                    txtc.UNAME = txtcab.GetValue("UNAME").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarTextosCabPedidos(connection, txtc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure txtpos in TXP)
            {
                Textos_Posiciones_Pedidos_SD_Vis txtp = new Textos_Posiciones_Pedidos_SD_Vis();
                try
                {
                    txtp.FOLIO_SAM = txtpos.GetValue("FOLIO_SAM").ToString();
                    txtp.VBELN = txtpos.GetValue("VBELN").ToString();
                    txtp.POSNR = txtpos.GetValue("POSNR").ToString();
                    txtp.INDICE = txtpos.GetValue("INDICE").ToString();
                    txtp.TDLINE = txtpos.GetValue("TDLINE").ToString();
                    txtp.UNAME = txtpos.GetValue("UNAME").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarTextosPosPedidos(connection, txtp);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure i in INT)
            {
                Interlocutores_pedidos_vis inter = new Interlocutores_pedidos_vis();
                try
                {
                    inter.VBELN = i.GetValue("VBELN").ToString();
                    inter.XBLNR = i.GetValue("XBLNR").ToString();
                    inter.POSNR = i.GetValue("POSNR").ToString();
                    inter.PARVW = i.GetValue("PARVW").ToString();
                    inter.KUNNR = i.GetValue("KUNNR").ToString();
                    inter.XCPDK = i.GetValue("XCPDK").ToString();
                    DALC_PedidosSD.ObtenerInstancia().InsertarInterlocutores(connection, inter);
                }
                catch (Exception) { return false; }
            }
            return true;
        }
        public bool ZMATERIALES_PRECIOS()
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZMATERIALES_PRECIOS");
            IRfcTable MAT = FUNCTION.GetTable("MAT");
            //FUNCTION.SetValue("VBELN", documento);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure ma in MAT)
            {
                mat_precios m = new mat_precios();
                try
                {
                    m.material = ma.GetValue("MATNR").ToString();
                    m.antiguo = ma.GetValue("BISMT").ToString();
                    m.centro = ma.GetValue("WERKS").ToString();
                    m.texto_breve = ma.GetValue("MAKTX").ToString();
                    DALC_Mat_Precios.ObtenerInstancia().InsertarPreciosMat(connection, m);
                }
                catch (Exception) { return false; }
            }
            return true;
        }
        /***************************************************************/
        /*************************PP************************************/
        public bool ZIMPRESORAS(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZIMPRESORAS");
            IRfcTable tsi = FUNCTION.GetTable("IMPRESORAS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            if (tsi.Count > 0)
            {
                DALC_sincronizacion.ObtenerInstancia().vaciaImpresoras(connection);
            }
            foreach (IRfcStructure linea in tsi)
            {
                try
                {
                    string pt = linea.GetValue("ARBPL").ToString();
                    string dir = linea.GetValue("DIRECCION").ToString();
                    string tipo = linea.GetValue("TIPO").ToString();
                    DALC_sincronizacion.ObtenerInstancia().IngresaImpresora(connection, pt, dir, tipo);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZIMPRESORAS", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZIMPRESORAS", "X", centro);
            return true;
        }
        public bool ZCATALOGOS_PP(string centro)
        {
            RfcRepository clas = rfcDesti.Repository;
            IRfcFunction FUNCTION = clas.CreateFunction("ZCATALOGOS_PP");
            IRfcTable ClaseOrden = FUNCTION.GetTable("PP_CLOR");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            if (ClaseOrden.Count > 0)
            {
                ACC_ClaseOrdenPP.ObtenerInstancia().TruncaClasOrd(connection);
            }
            foreach (IRfcStructure co in ClaseOrden)
            {
                ClaseOrdenPP cla = new ClaseOrdenPP();
                try
                {
                    cla.WERKS = co.GetValue("WERKS").ToString();
                    cla.PP_AUFART = co.GetValue("PP_AUFART").ToString();
                    ACC_ClaseOrdenPP.ObtenerInstancia().InsertarClasOrd(connection, cla);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZCATALOGOS_PP", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZCATALOGOS_PP", "X", centro);
            return true;
        }
        public bool ZMOTIVOS_RECHAZO(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZMOTIVOS_RECHAZO");
            IRfcTable tsi = FUNCTION.GetTable("MOTIVOS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
                if (tsi.Count > 0)
                {
                    acc_ordenes_pp.ObtenerInstancia().truncarMotivosPP(connection);
                }
            }
            catch (Exception) { return false; }

            foreach (IRfcStructure linea in tsi)
            {
                try
                {
                    string ctr = linea.GetValue("WERKS").ToString();
                    string idx = linea.GetValue("NUMERO").ToString();
                    string mot = linea.GetValue("MOTIVO").ToString();
                    acc_ordenes_pp.ObtenerInstancia().ingresaMotivos(connection, ctr, idx, mot);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMOTIVOS_RECHAZO", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMOTIVOS_RECHAZO", "X", centro);
            return true;
        }
        public bool ZINV_SAM(string centro)
        {
            string mensaje = "Correcto";
            List<VALIDA_MOVI_ERROR_MDL_Result> v = ACC_Inventarios.ObtenerInstancia().ValidarErrores(connection).ToList();
            if (v.Count() <= 0)
            {
                RfcRepository sin = rfcDesti.Repository;
                IRfcFunction FUNCTION = sin.CreateFunction("ZINV_SAM");
                IRfcTable INVENTARIOS = FUNCTION.GetTable("INVENTARIO_SAM");
                IRfcTable INVENTARIOE = FUNCTION.GetTable("INVENTARIO_EE");
                FUNCTION.SetValue("WERKS", centro);
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception)
                {
                    mensaje = "Error";
                    return false;
                }
                ACC_Inventarios.ObtenerInstancia().TruncateInventarioTotal(connection, centro);
                foreach (IRfcStructure linea in INVENTARIOS)
                {
                    Inventarios inv = new Inventarios();
                    try
                    {
                        inv.MATNR = linea.GetValue("MATNR").ToString();
                        inv.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                        inv.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                        inv.WERKS = linea.GetValue("WERKS").ToString();
                        inv.MEINS = linea.GetValue("MEINS").ToString();
                        inv.LGORT = linea.GetValue("LGORT").ToString();
                        inv.LGOBE = linea.GetValue("LGOBE").ToString();
                        inv.CLABS = linea.GetValue("CLABS").ToString();
                        inv.CINSM = linea.GetValue("CINSM").ToString();
                        inv.CSPEM = linea.GetValue("CSPEM").ToString();
                        inv.CUMLM = linea.GetValue("CUMLM").ToString();
                        inv.CHARG = linea.GetValue("CHARG").ToString();
                        inv.MTART = linea.GetValue("MTART").ToString();
                        inv.MATKL = linea.GetValue("MATKL").ToString();
                        inv.SERNR = linea.GetValue("SERNR").ToString();
                        inv.XCHPF = linea.GetValue("XCHPF").ToString();
                        inv.ult_stock_libre = linea.GetValue("CLABS").ToString();
                        inv.cnt_cent_dest = "0";
                        inv.ult_cnt_dest = "0";
                        inv.ult_trans = linea.GetValue("CUMLM").ToString();
                        ACC_Inventarios.ObtenerInstancia().InsertarInvRuta(connection, inv);
                    }
                    catch (Exception)
                    {
                        mensaje = "Error";
                        return false;
                    }
                }
                foreach (IRfcStructure linea in INVENTARIOE)
                {
                    Inventarios inv = new Inventarios();
                    try
                    {
                        inv.MATNR = linea.GetValue("MATNR").ToString();
                        inv.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                        inv.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                        inv.WERKS = linea.GetValue("WERKS").ToString();
                        inv.MEINS = linea.GetValue("MEINS").ToString();
                        inv.LGORT = linea.GetValue("LGORT").ToString();
                        inv.LGOBE = linea.GetValue("LGOBE").ToString();
                        inv.CLABS = linea.GetValue("CLABS").ToString();
                        inv.CINSM = linea.GetValue("CINSM").ToString();
                        inv.CSPEM = linea.GetValue("CSPEM").ToString();
                        inv.CUMLM = linea.GetValue("CUMLM").ToString();
                        inv.CHARG = linea.GetValue("CHARG").ToString();
                        inv.MTART = linea.GetValue("MTART").ToString();
                        inv.MATKL = linea.GetValue("MATKL").ToString();
                        inv.SERNR = linea.GetValue("SERNR").ToString();
                        inv.XCHPF = linea.GetValue("XCHPF").ToString();
                        inv.SOBKZ = linea.GetValue("SOBKZ").ToString();
                        inv.VBELN = linea.GetValue("VBELN").ToString();
                        inv.POSNR = linea.GetValue("POSNR").ToString();
                        ACC_Inventarios.ObtenerInstancia().InsertarInvEE(connection, inv);
                    }
                    catch (Exception)
                    {
                        mensaje = "Error";
                        return false;
                    }
                }
                DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZINV_SAM", mensaje);
                DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZINV_SAM", "X", centro);
            }
            return true;
        }
        public bool ZPP_HOJA_RUTA_SAM(string centro)
        {
            RfcRepository hro = rfcDesti.Repository;
            IRfcFunction FUNCTION = hro.CreateFunction("ZPP_HOJA_RUTA_SAM");
            IRfcTable HOJA_RUTA = FUNCTION.GetTable("HOJA_RUTA");
            FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            ACC_HOJA_RUTA_PP.ObtenerInstancia().EliminarHojasRuta(connection, centro);
            foreach (IRfcStructure hoja in HOJA_RUTA)
            {
                Hoja_RutaPP ho = new Hoja_RutaPP();
                try
                {
                    ho.MATNR = hoja.GetValue("MATNR").ToString();
                    ho.PLNNR = hoja.GetValue("PLNNR").ToString();
                    ho.PLNAL = hoja.GetValue("PLNAL").ToString();
                    ho.ZKRIZ = hoja.GetValue("ZKRIZ").ToString();
                    ho.ZAEHL = hoja.GetValue("ZAEHL").ToString();
                    ho.PLNTY = hoja.GetValue("PLNTY").ToString();
                    ho.PLNKN = hoja.GetValue("PLNKN").ToString();
                    ho.PLNFL = hoja.GetValue("PLNFL").ToString();
                    ho.ZAEHL1 = hoja.GetValue("ZAEHL1").ToString();
                    ho.ZAEHL2 = hoja.GetValue("ZAEHL2").ToString();
                    ho.OBJTY = hoja.GetValue("OBJTY").ToString();
                    ho.OBJID = hoja.GetValue("OBJID").ToString();
                    ho.LOEKZ = hoja.GetValue("LOEKZ").ToString();
                    ho.WERKS = hoja.GetValue("WERKS").ToString();
                    ho.STEUS = hoja.GetValue("STEUS").ToString();
                    ho.ARBID = hoja.GetValue("ARBID").ToString();
                    ho.BMSCH = hoja.GetValue("BMSCH").ToString();
                    ho.DAUNO = hoja.GetValue("DAUNO").ToString();
                    ho.DAUNE = hoja.GetValue("DAUNE").ToString();
                    ho.ARBEI = hoja.GetValue("ARBEI").ToString();
                    ho.ARBEH = hoja.GetValue("ARBEH").ToString();
                    ho.STATU = hoja.GetValue("STATU").ToString();
                    ho.KTEXT = hoja.GetValue("KTEXT").ToString();
                    ho.STLNR = hoja.GetValue("STLNR").ToString();
                    ho.STLAL = hoja.GetValue("STLAL").ToString();
                    ho.VORNR = hoja.GetValue("VORNR").ToString();
                    ho.WERKSI = hoja.GetValue("WERKSI").ToString();
                    ho.LTXA1 = hoja.GetValue("LTXA1").ToString();
                    ho.LTXA2 = hoja.GetValue("LTXA2").ToString();
                    ho.TXTSP = hoja.GetValue("TXTSP").ToString();
                    ho.MEINH = hoja.GetValue("MEINH").ToString();
                    ho.UMREN = hoja.GetValue("UMREN").ToString();
                    ho.UMREZ = hoja.GetValue("UMREZ").ToString();
                    ho.ZMERH = hoja.GetValue("ZMERH").ToString();
                    ho.ZEIER = hoja.GetValue("ZEIER").ToString();
                    ho.LAR01 = hoja.GetValue("LAR01").ToString();
                    ho.VGE01 = hoja.GetValue("VGE01").ToString();
                    ho.VGW01 = hoja.GetValue("VGW01").ToString();
                    ho.LAR02 = hoja.GetValue("LAR02").ToString();
                    ho.VGE02 = hoja.GetValue("VGE02").ToString();
                    ho.VGW02 = hoja.GetValue("VGW02").ToString();
                    ho.LAR03 = hoja.GetValue("LAR03").ToString();
                    ho.VGE03 = hoja.GetValue("VGE03").ToString();
                    ho.VGW03 = hoja.GetValue("VGW03").ToString();
                    ho.LAR04 = hoja.GetValue("LAR04").ToString();
                    ho.VGE04 = hoja.GetValue("VGE04").ToString();
                    ho.VGW04 = hoja.GetValue("VGW04").ToString();
                    ho.LAR05 = hoja.GetValue("LAR05").ToString();
                    ho.VGE05 = hoja.GetValue("VGE05").ToString();
                    ho.VGW05 = hoja.GetValue("VGW05").ToString();
                    ho.LAR06 = hoja.GetValue("LAR06").ToString();
                    ho.VGE06 = hoja.GetValue("VGE06").ToString();
                    ho.VGW06 = hoja.GetValue("VGW06").ToString();
                    ho.LIFNR = hoja.GetValue("LIFNR").ToString();
                    ho.PEINH = hoja.GetValue("PEINH").ToString();
                    ho.SAKTO = hoja.GetValue("SAKTO").ToString();
                    ho.WAERS = hoja.GetValue("WAERS").ToString();
                    ho.INFNR = hoja.GetValue("INFNR").ToString();
                    ho.ESOKZ = hoja.GetValue("ESOKZ").ToString();
                    ho.EKORG = hoja.GetValue("EKORG").ToString();
                    ho.EKGRP = hoja.GetValue("EKGRP").ToString();
                    ho.MATKL = hoja.GetValue("MATKL").ToString();
                    ho.ARBPL = hoja.GetValue("ARBPL").ToString();
                    ho.ANZZL = hoja.GetValue("ANZZL").ToString();
                    ho.ANDAT = hoja.GetValue("ANDAT").ToString();
                    ho.AEDAT = hoja.GetValue("AEDAT").ToString();
                    ho.SRVPOS = hoja.GetValue("SRVPOS").ToString();
                    ho.KTEXT1 = hoja.GetValue("KTEXT1").ToString();
                    ACC_HOJA_RUTA_PP.ObtenerInstancia().InsertarHojasRuta(connection, ho);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPP_HOJA_RUTA_SAM", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZPP_HOJA_RUTA_SAM", "X", centro);
            return true;
        }
        public bool ZPP_SAM_BOMEQUI(string centro)
        {
            RfcRepository boom = rfcDesti.Repository;
            IRfcFunction FUNCTION = boom.CreateFunction("ZPP_SAM_BOMEQUI");
            IRfcTable BOOM_MATE = FUNCTION.GetTable("PM_BOM");
            FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            ACC_BOOM_MATE_PP.ObtenerInstancia().EliminarBoomMaterialesPP(connection, centro);
            foreach (IRfcStructure bm in BOOM_MATE)
            {
                Boom_MatePP ma = new Boom_MatePP();
                try
                {
                    ma.WERKS = bm.GetValue("WERKS").ToString();
                    ma.STLTY = bm.GetValue("STLTY").ToString();
                    ma.STLNR = bm.GetValue("STLNR").ToString();
                    ma.STLAL = bm.GetValue("STLAL").ToString();
                    ma.STKOZ = bm.GetValue("STKOZ").ToString();
                    ma.STLKN = bm.GetValue("STLKN").ToString();
                    ma.STPOZ = bm.GetValue("STPOZ").ToString();
                    ma.STASZ = bm.GetValue("STASZ").ToString();
                    ma.MATNR = bm.GetValue("MATNR").ToString();
                    ma.BMENG = bm.GetValue("BMENG").ToString();
                    ma.DATUV = bm.GetValue("DATUV").ToString();
                    ma.LKENZ = bm.GetValue("LKENZ").ToString();
                    ma.LOEKZ = bm.GetValue("LOEKZ").ToString();
                    ma.ANDAT = bm.GetValue("ANDAT").ToString();
                    ma.ANNAM = bm.GetValue("ANNAM").ToString();
                    ma.IDNRK = bm.GetValue("IDNRK").ToString();
                    ma.PSWRK = bm.GetValue("PSWRK").ToString();
                    ma.POSTP = bm.GetValue("POSTP").ToString();
                    ma.SPOSN = bm.GetValue("SPOSN").ToString();
                    ma.SORTP = bm.GetValue("SORTP").ToString();
                    ma.KMPME = bm.GetValue("KMPME").ToString();
                    ma.KMPMG = bm.GetValue("KMPMG").ToString();
                    ma.FMENG = bm.GetValue("FMENG").ToString();
                    ACC_BOOM_MATE_PP.ObtenerInstancia().InsertarBoomMate(connection, ma);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPP_SAM_BOMEQUI", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZPP_SAM_BOMEQUI", "X", centro);
            return true;
        }
        public bool ZMM_MATERIAL_CENTRO(string centro)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZMM_MATERIAL_CENTRO");
            IRfcTable mat = FUNCTION.GetTable("MATERIALES");
            FUNCTION.SetValue("CENTRO", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
                if (mat.Count > 0)
                {
                    ACC_Inventarios.ObtenerInstancia().BorraMatAlamcenXcentro(connection, centro);
                }
            }
            catch (Exception) { return false; }

            foreach (IRfcStructure linea in mat)
            {
                Materiales_Almacen ma = new Materiales_Almacen();
                try
                {
                    ma.MATNR = linea.GetValue("MATNR").ToString();
                    ma.WERKS = linea.GetValue("WERKS").ToString();
                    ma.LGORT = linea.GetValue("LGORT").ToString();
                    ma.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                    ma.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                    ma.LVORM = linea.GetValue("LVORM").ToString();
                    ACC_Inventarios.ObtenerInstancia().InsertarMaterialAlmacen(connection, ma);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_MATERIAL_CENTRO", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_MATERIAL_CENTRO", "X", centro);
            return true;
        }
        public bool ZMM_STOCK_TRANSFERENCIA(string centro)//TRNASFERENCIA POR CENTRO
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZMM_STOCK_TRANSFERENCIA");
            IRfcTable mat = FUNCTION.GetTable("ZMM_STRA");
            FUNCTION.SetValue("WERKS", centro);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in mat)
            {
                StockTransferencia st = new StockTransferencia();
                try
                {
                    st.WERKS = linea.GetValue("WERKS").ToString();
                    st.MATNR = linea.GetValue("MATNR").ToString();
                    st.UMLMC = linea.GetValue("UMLMC").ToString();
                    st.XCHPF = linea.GetValue("XCHPF").ToString();
                    st.MEINS = linea.GetValue("MEINS").ToString();
                    st.MAKTX = linea.GetValue("MAKTX").ToString();
                    st.MAKTX_E = linea.GetValue("MAKTX_E").ToString();
                    st.LVORM = linea.GetValue("LVORM").ToString();
                    st.LGORT = linea.GetValue("LGORT").ToString();
                    st.UMLGO = linea.GetValue("UMLGO").ToString();
                    st.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    st.KDAUF = linea.GetValue("KDAUF").ToString();
                    st.KDPOS = linea.GetValue("KDPOS").ToString();
                    st.CHARG = linea.GetValue("CHARG").ToString();
                    List<SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result> valida = DALC_StockTransferencia.obtenerInstancia().ValidarMaterialST(connection, st).ToList();
                    if (valida.Count > 0)
                    {
                        string operacion = "";
                        operacion = st.UMLMC.ToString().Substring(0, 1);
                        if (operacion.Equals("-"))
                        {
                            decimal sap, sam, resta;
                            operacion = st.UMLMC.Replace("-", "");
                            sap = Convert.ToDecimal(operacion.ToString());
                            foreach (SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result va in valida)
                            {
                                sam = Convert.ToDecimal(va.stock_traslado.ToString());
                                resta = sam - sap;
                                string stock = Convert.ToString(resta);
                                DALC_StockTransferencia.obtenerInstancia().ActualizarStockTransfer313(connection, st, stock);
                            }
                        }
                        else
                        {
                            decimal sap, sam, suma;
                            sap = Convert.ToDecimal(st.UMLMC.ToString());
                            foreach (SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result va in valida)
                            {
                                sam = Convert.ToDecimal(va.stock_traslado.ToString());
                                suma = sap + sam;
                                string stock = Convert.ToString(suma);
                                DALC_StockTransferencia.obtenerInstancia().ActualizarStockTransfer313(connection, st, stock);
                            }
                        }
                    }
                    else
                    {
                        DALC_StockTransferencia.obtenerInstancia().IngresarStockTransfer(connection, st);
                    }
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_STOCK_TRANSFERENCIA", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMM_STOCK_TRANSFERENCIA", "X", centro);
            return true;
        }

        /******************Envios*******************/
        public string ObtenerUltimoRegistroNotiPP()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_Noti_PP_ultimo_reg_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalNotiPP(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_Noti_PP_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public void ZNOTIFICACIONES_ORDEN()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 1;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroNotiPP();
            Thread.Sleep(2000);
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_Noti_PP_id_MDL_Result> datos = DALC_Notificaciones_PP.ObtenerInstancia().ObtenerDatosIdNoti(connection, id).ToList();
            foreach (SELEC_datos_Noti_PP_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha_recibido;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_Noti_PP_valida_hora_MDL_Result> validahor = DALC_Notificaciones_PP.ObtenerInstancia().ObtenerValidacionHoraNoti(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        string ese = folio_sam.Replace("P", "0");
                        string pe = ese.Replace("P", "0");
                        int foli = Convert.ToInt32(pe.ToString()) - 1;
                        folio_sam = "PP" + foli.ToString();
                        List<SELECT_lista_folios_Noti_PP_MDL_Result> movs = DALC_Notificaciones_PP.ObtenerInstancia().ObtenerTodoFolioNoti(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_Noti_PP_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosNotiPP(folio_sam);
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_Noti_PP_MDL_Result> movs = DALC_Notificaciones_PP.ObtenerInstancia().ObtenerTodoFolioNoti(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_Noti_PP_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosNotiPP(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_Noti_PP_MDL_Result> movs = DALC_Notificaciones_PP.ObtenerInstancia().ObtenerTodoFolioNoti(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_Noti_PP_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosNotiPP(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosNotiPP(string folio_sam)
        {
            List<cab_MovNotGETPP_mdl_Result> cabe = ACC_MovNotPP.ObtenerInstancia().obtenerCabMovNot(connection, folio_sam).ToList();
            List<pos_NotTiempGETPP2_mdl_Result> pos = ACC_NotTiempos.ObtenerInstancia().obtenerPosNotTiempos(connection, folio_sam).ToList();
            List<pos_MovNotGETPP_mdl_Result> posi = ACC_MovNotPP.ObtenerInstancia().obtenerPosMovNot(connection, folio_sam).ToList();
            RfcRepository rst = rfcDesti.Repository;
            IRfcFunction FUNCTION = rst.CreateFunction("ZNOTIFICACIONES_ORDEN");
            IRfcTable tabNT = FUNCTION.GetTable("NOT_TIEMPO");
            IRfcTable tabNC = FUNCTION.GetTable("TSAM_MOV_CAB");
            IRfcTable tabNP = FUNCTION.GetTable("TSAM_MOV");
            FUNCTION.SetValue("CABN", pos.Count);
            FUNCTION.SetValue("CABM", cabe.Count);
            FUNCTION.SetValue("POSM", posi.Count);
            string orden = "";
            if (pos.Count > 0)
            {
                foreach (pos_NotTiempGETPP2_mdl_Result att in pos)
                {
                    tabNT.Append();
                    orden = att.num_orden;
                    tabNT.SetValue("AUFNR", att.num_orden);
                    tabNT.SetValue("VORNR", att.num_operacion);
                    tabNT.SetValue("RUECK", att.num_notificacion);
                    tabNT.SetValue("FOLIO_SAM", att.folio_sam);
                    //tabNT.SetValue("AUART", att.clase_orden);
                    tabNT.SetValue("ARBPL", att.puesto_trabajo);
                    tabNT.SetValue("WERKS", att.centro);
                    tabNT.SetValue("MATNR", att.material);
                    tabNT.SetValue("MATXT", att.texto_breve);
                    tabNT.SetValue("SAM_RUECK", att.num_notificacion_sam);
                    tabNT.SetValue("AUERU", att.notificacion_parcial);
                    tabNT.SetValue("AUSOR", att.compensar);
                    tabNT.SetValue("LMNGA", att.cantidad_buena);
                    tabNT.SetValue("MEINH", att.unidad_medida);
                    tabNT.SetValue("XMNGA", att.rechazo);
                    tabNT.SetValue("RMNGA", att.cantidad_trabajo);
                    tabNT.SetValue("GRUND", att.motivo_desviacion);
                    tabNT.SetValue("ISM01", att.actividad_notificar1);
                    tabNT.SetValue("ILE01", att.unidad_medida_notificar1);
                    tabNT.SetValue("LEK01", att.indicador_actividad1);
                    tabNT.SetValue("ISM02", att.actividad_notificar2);
                    tabNT.SetValue("ILE02", att.unidad_medida_notificar2);
                    tabNT.SetValue("LEK02", att.indicador_actividad2);
                    tabNT.SetValue("ISM03", att.actividad_notificar3);
                    tabNT.SetValue("ILE03", att.unidad_medida_notificar3);
                    tabNT.SetValue("LEK03", att.indicador_actividad3);
                    tabNT.SetValue("ISM04", att.actividad_notificar4);
                    tabNT.SetValue("ILE04", att.unidad_medida_notificar4);
                    tabNT.SetValue("LEK04", att.indicador_actividad4);
                    tabNT.SetValue("ISM05", att.actividad_notificar5);
                    tabNT.SetValue("ILE05", att.unidad_medida_notificar5);
                    tabNT.SetValue("LEK05", att.indicador_actividad5);
                    tabNT.SetValue("ISM06", att.actividad_notificar6);
                    tabNT.SetValue("ILE06", att.unidad_medida_notificar6);
                    tabNT.SetValue("LEK06", att.indicador_actividad6);
                    tabNT.SetValue("PERNR", att.num_personal);
                    tabNT.SetValue("ISDD", att.fecha_inicio);
                    tabNT.SetValue("ISDZ", att.hora_inicio);
                    tabNT.SetValue("IEDD", att.fecha_fin);
                    tabNT.SetValue("IEDZ", att.hora_fin);
                    tabNT.SetValue("BUDAT", att.fecha_contabilizacion);
                    tabNT.SetValue("CONF_TEXT", att.texto_notificacion);
                    tabNT.SetValue("UNAME", att.usuario);
                    tabNT.SetValue("RECIBIDO", att.recibido);
                    tabNT.SetValue("PROCESADO", att.procesado);
                    tabNT.SetValue("FECHA", att.fecha_recibido);
                    tabNT.SetValue("HORA", att.hora_recibido);
                    tabNT.SetValue("MENSAJE", att.mensaje);
                    tabNT.SetValue("MOTIVO", att.motivo);
                }
            }
            if (cabe.Count > 0)
            {
                foreach (cab_MovNotGETPP_mdl_Result ca in cabe)
                {
                    tabNC.Append();
                    tabNC.SetValue("FOLIO_SAM", ca.folio_sam);
                    tabNC.SetValue("AUFNR", ca.num_orden);
                    tabNC.SetValue("VORNR", ca.num_operacion);
                    tabNC.SetValue("MATNR", ca.material);
                    tabNC.SetValue("UZEIT", ca.hora);
                    tabNC.SetValue("DATUM", ca.fecha);
                    tabNC.SetValue("MAKTX", ca.texto_breve);
                    tabNC.SetValue("MBLNR", ca.num_doc_material);
                    tabNC.SetValue("WERKS", ca.centro);
                    tabNC.SetValue("LGORT", ca.almacen);
                    tabNC.SetValue("LMNGA", ca.cantidad_buena);
                    tabNC.SetValue("BWART", ca.clase_mov);
                    tabNC.SetValue("ANULA", ca.indicador1);
                    tabNC.SetValue("TEXTO", ca.texto1);
                    tabNC.SetValue("TEXCAB", ca.texto2);
                    tabNC.SetValue("TIPO", ca.indicador2);
                    tabNC.SetValue("UMLGO", ca.almacen_receptor);
                    tabNC.SetValue("LFSNR", ca.entrega_externa);
                    tabNC.SetValue("RECIBIDO", ca.recibido);
                    tabNC.SetValue("PROCESADO", ca.procesado);
                    tabNC.SetValue("MENSAJE", ca.mensaje);
                    tabNC.SetValue("FECHA_RECIBIDO", ca.fecha_recibido);
                    tabNC.SetValue("HORA_RECIBIDO", ca.hora_recibido);
                    tabNC.SetValue("FECHA_CONTABLE", ca.fecha_contabilizacion);
                    tabNC.SetValue("UNAME", ca.usuario);
                }
            }
            if (posi.Count > 0)
            {
                foreach (pos_MovNotGETPP_mdl_Result po in posi)
                {
                    tabNP.Append();
                    tabNP.SetValue("FOLIO_SAM", po.folio_sam);
                    tabNP.SetValue("AUFNR", po.num_orden);
                    tabNP.SetValue("UZEIT", po.hora);
                    tabNP.SetValue("DATUM", po.fecha);
                    tabNP.SetValue("TABIX", po.ind_reg_inv);
                    tabNP.SetValue("FOLIO_SAP", po.num_doc_mate);
                    tabNP.SetValue("MATNR", po.num_material);
                    tabNP.SetValue("MAKTX", po.txt_breve_mate);
                    tabNP.SetValue("CMENGE", po.cantidad);
                    tabNP.SetValue("CHARG", po.num_lote);
                    tabNP.SetValue("CMEINS", po.um_base);
                    tabNP.SetValue("WERKS", po.centro);
                    tabNP.SetValue("LGORT", po.almacen);
                    tabNP.SetValue("BWART", po.clase_mov);
                    tabNP.SetValue("MENGE", po.cantidad2);
                    tabNP.SetValue("MEINS", po.um_base2);
                    tabNP.SetValue("ACTIVO", po.ind_pos);
                    tabNP.SetValue("ENMNG", po.cnt_tomada);
                    tabNP.SetValue("ERFME", po.um_entrada);
                    tabNP.SetValue("ERFMG", po.cnt_um_entrada);
                    tabNP.SetValue("IDNRK", po.comp_lista_mate);
                    tabNP.SetValue("LCMENGE", po.cantidad3);
                    tabNP.SetValue("LMEINS", po.um_base3);
                    tabNP.SetValue("LMENGE", po.cantidad4);
                    tabNP.SetValue("LTMEINS", po.um_base_4);
                    tabNP.SetValue("LTMENGE", po.cantidad5);
                    tabNP.SetValue("RENGLON", po.ind_pos2);
                    tabNP.SetValue("RSNUM", po.num_reserva);
                    tabNP.SetValue("RSPOS", po.num_posicion_res);
                    tabNP.SetValue("TMEINS", po.um_base5);
                    tabNP.SetValue("TMENGE", po.cantidad6);
                    tabNP.SetValue("UMLGO", po.almacen_rec);
                    tabNP.SetValue("URSNUM", po.num_reserva2);
                    tabNP.SetValue("EBELN", po.num_doc_compra);
                    tabNP.SetValue("EBELP", po.num_posicion_doc);
                    tabNP.SetValue("SOBKZ", po.ind_stock_esp);
                    tabNP.SetValue("LIFNR", po.num_cuenta_prov);
                    tabNP.SetValue("UMWRK", po.centro_emisor);
                    tabNP.SetValue("KOSTL", po.centro_coste);
                    tabNP.SetValue("LFSNR", po.num_nota_ent_max);
                    tabNP.SetValue("FRBNR", po.num_carta_porte);
                    tabNP.SetValue("EAN11_BME", po.num_arts_europ);
                    tabNP.SetValue("EAN11_KON", po.control_num_art);
                    tabNP.SetValue("EANME", po.um_base6);
                    tabNP.SetValue("LICHA", po.num_lote_prov);
                    tabNP.SetValue("SAKTO", po.clase_coste);
                    tabNP.SetValue("RECIBIDO", po.recibido);
                    tabNP.SetValue("PROCESADO", po.procesado);
                    tabNP.SetValue("MENSAJE", po.error);
                    tabNP.SetValue("FECHA_RECIBIDO", po.fecha_recibido);
                    tabNP.SetValue("HORA_RECIBIDO", po.hora_recibido);
                    tabNP.SetValue("POSNR", po.num_pos_lista_mat);
                    tabNP.SetValue("ANCHO", po.ancho);
                    tabNP.SetValue("SPECIAL_STOCK", po.stock_especial);
                    tabNP.SetValue("VORNR", po.num_operacion);
                    //definir proceso de etiqueta
                    if (po.clase_mov.Equals("101"))
                    {
                        try
                        {
                            string s = "";
                            List<getDatosEtiquetaPP2_Result> ee = acc_ordenes_pp.ObtenerInstancia().etiquetaPP_2(connection, po.folio_sam).ToList();
                            List<string> tp = acc_ordenes_pp.ObtenerInstancia().tipoImpresora(connection, ee[0].puesto_trabajo).ToList();

                            if (tp[0].Equals("1"))
                            {
                                s = ACC_Etiquetas.ObtenerInstancia().etiquetaTLP(ee);
                            }
                            else
                            {
                                s = ACC_Etiquetas.ObtenerInstancia().etiquetaGK(ee);
                            }
                            List<string> pt = acc_ordenes_pp.ObtenerInstancia().impresora(connection, ee[0].puesto_trabajo).ToList();
                            RawPrinterHelper.SendStringToPrinter(pt[0], s);
                        }
                        catch (Exception) { }
                    }
                }
            }
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            if (FUNCTION.GetValue("MENSAJE").ToString().Trim().Equals("Correcto"))
            {
                ZORDEN_PP(orden);
                acc_ordenes_pp.ObtenerInstancia().EliminarRegistroNoti(connection, folio_sam);
                foreach (pos_MovNotGETPP_mdl_Result po in posi)
                {
                    ZINV_SAM_MAT(po.num_material);
                }
            }
            else
            {
                acc_ordenes_pp.ObtenerInstancia().ActualizaResultado(connection, folio_sam, FUNCTION.GetValue("MENSAJE").ToString().Trim());
            }
        }
        public bool ZPROCESA_ESTATUS()
        {
            List<status_ordenesGET_mdl_Result> ll = ACC_StatusOrdenes.ObtenerInstancia().obtenerStausOrdnes(connection).ToList();
            if (ll.Count > 0)
            {
                foreach (status_ordenesGET_mdl_Result lml in ll)
                {
                    Thread.Sleep(1000);
                    RfcRepository rst = rfcDesti.Repository;
                    IRfcFunction FUNCION = rst.CreateFunction("ZPROCESA_ESTATUS");
                    IRfcTable tabE = FUNCION.GetTable("TSAM_STATUS_ORD");
                    FUNCION.SetValue("CAB", 1);

                    tabE.Append();
                    tabE.SetValue("FOLIO_SAM", lml.folio_sam);
                    //tabE.SetValue("FOLIO_ORD", lml.folio_orden);
                    tabE.SetValue("DATUM", lml.fecha_serv);
                    tabE.SetValue("UZEIT", lml.hora_serv);
                    tabE.SetValue("AUFNR", lml.num_orden);
                    tabE.SetValue("WERKS", lml.centro);
                    tabE.SetValue("OPERACION", lml.operacion_sam);
                    tabE.SetValue("RECIBIDO", lml.recibido);
                    tabE.SetValue("PROCESADO", lml.procesado);
                    tabE.SetValue("MENSAJE", lml.txt_mensaje);
                    tabE.SetValue("FDATUM", lml.fecha_final);
                    tabE.SetValue("FUZEIT", lml.hora_fin_tardia);
                    tabE.SetValue("NEWCHARG", lml.num_lote);
                    tabE.SetValue("FECHA_RECIBIDO", lml.fecha_recibido);
                    tabE.SetValue("HORA_RECIBIDO", lml.hora_recibido);
                    tabE.SetValue("UNAME", lml.usuario);
                    try
                    {
                        FUNCION.Invoke(rfcDesti);
                    }
                    catch (Exception) { }
                    if (FUNCION.GetValue("MENSAJE").ToString().Trim().Equals("Correcto"))
                    {
                        acc_ordenes_pp.ObtenerInstancia().BorraregistroStatus(connection, lml.folio_sam);
                    }
                    else
                    {
                        acc_ordenes_pp.ObtenerInstancia().updateStatusFolio(connection, lml.folio_sam, FUNCION.GetValue("MENSAJE").ToString().Trim());
                    }
                }
            }
            return true;
        }
        public string ObtenerUltimoRegistroMovMM()
        {
            ObjectParameter obj = new ObjectParameter("id", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_Mov_MM_ultimo_reg_MDL(obj);
            string val = "";
            val = obj.Value.ToString();
            return val;
        }
        public string ObtenerTotalMovMM(string folio_sam)
        {
            ObjectParameter obj = new ObjectParameter("total", typeof(int));
            var context = new samEntities(connection.ToString());
            context.SELECT_contar_Mov_MM_fol_MDL(folio_sam, obj);
            string tot = "";
            tot = obj.Value.ToString();
            return tot;
        }
        public void ZMOV_SAM()
        {
            string folio_sam = "", fechafol = "", hor = "", fecha_Actual = "";
            int hora, minutos, resta;
            DateTime fecha = DateTime.Today;
            fecha_Actual = string.Format("{0:yyyy-MM-dd}", fecha);
            DateTime horass = DateTime.Now;
            hora = Convert.ToInt32(horass.Hour);
            string hour = "";
            if (hora <= 9)
            {
                hour = "0" + hora.ToString();
            }
            else
            {
                hour = hora.ToString();
            }
            minutos = Convert.ToInt32(horass.Minute);
            resta = minutos - 1;
            string tim = "";
            if (resta <= 9)
            {
                tim = "0" + Convert.ToString(resta);
            }
            else
            {
                tim = Convert.ToString(resta);
            }
            hor = hour + ":" + tim + ":00";
            string ids = ObtenerUltimoRegistroMovMM();
            Thread.Sleep(2000);
            int id = 0;
            if (ids.Equals(""))
            {
                ids = "0";
            }
            else { ids = ids; }
            id = Convert.ToInt32(ids.ToString());
            List<SELEC_datos_Mov_MM_id_MDL_Result> datos = ACC_movMM.ObtenerInstancia().ObtenerDatosIdMov(connection, id).ToList();
            foreach (SELEC_datos_Mov_MM_id_MDL_Result di in datos)
            {
                folio_sam = di.folio_sam;
                fechafol = di.fecha;
                if (fechafol.Equals(fecha_Actual))
                {
                    List<SELECT_Mov_MM_valida_hora_MDL_Result> validahor = ACC_movMM.ObtenerInstancia().ObtenerValidacionHoraMov(connection, id, hor).ToList();
                    if (validahor.Count <= 0)
                    {
                        string ese = folio_sam.Replace("M", "0");
                        string pe = ese.Replace("O", "0");
                        int foli = Convert.ToInt32(pe.ToString()) - 1;
                        folio_sam = "MO" + foli.ToString();
                        List<SELECT_lista_folios_Mov_MM_MDL_Result> movs = ACC_movMM.ObtenerInstancia().ObtenerTodoFolioMov(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_Mov_MM_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosMovMM(folio_sam);
                        }
                    }
                    else
                    {
                        List<SELECT_lista_folios_Mov_MM_MDL_Result> movs = ACC_movMM.ObtenerInstancia().ObtenerTodoFolioMov(connection, folio_sam).ToList();
                        foreach (SELECT_lista_folios_Mov_MM_MDL_Result m in movs)
                        {
                            folio_sam = m.folio_sam;
                            EnviarDatosMovMM(folio_sam);
                        }
                    }
                }
                else
                {
                    List<SELECT_lista_folios_Mov_MM_MDL_Result> movs = ACC_movMM.ObtenerInstancia().ObtenerTodoFolioMov(connection, folio_sam).ToList();
                    foreach (SELECT_lista_folios_Mov_MM_MDL_Result m in movs)
                    {
                        folio_sam = m.folio_sam;
                        EnviarDatosMovMM(folio_sam);
                    }
                }
            }
        }
        public void EnviarDatosMovMM(string folio_sam)
        {
            bool flagI = false, docom = false;
            string doc_compras = "";
            List<SELECT_cabMov_MDL_Result> cab = ACC_movMM.ObtenerInstancia().obtenerCabMov(connection, folio_sam).ToList();
            List<SELECT_posMov_MDL_Result> pos = ACC_movMM.ObtenerInstancia().obtenerPosMov(connection, folio_sam).ToList();
            RfcRepository rst = rfcDesti.Repository;
            IRfcFunction FUNCTION = rst.CreateFunction("ZMOV_SAM");
            IRfcTable tabNC = FUNCTION.GetTable("MOV_CAB");
            IRfcTable tabNT = FUNCTION.GetTable("MOV_POS");
            FUNCTION.SetValue("CAB", cab.Count);
            FUNCTION.SetValue("POS", pos.Count);
            if (pos.Count > 0)
            {
                foreach (SELECT_posMov_MDL_Result att in pos)
                {
                    if (att.clase_mov.Equals("313") || att.clase_mov.Equals("315"))
                    {
                        flagI = true;
                    }
                    tabNT.Append();
                    tabNT.SetValue("FOLIO_SAM", att.folio_sam);
                    tabNT.SetValue("TABIX", att.indice);
                    tabNT.SetValue("UZEIT", att.hora);
                    tabNT.SetValue("DATUM", att.fecha);
                    tabNT.SetValue("FOLIO_SAP", att.num_doc_material);
                    tabNT.SetValue("BWART", att.clase_mov);
                    tabNT.SetValue("ACTIVO", att.indicador_posicion);
                    tabNT.SetValue("AUFNR", att.numero_orden);
                    tabNT.SetValue("CHARG", att.lote);
                    tabNT.SetValue("CMEINS", att.unidad_medida);
                    tabNT.SetValue("CMENGE", att.cantidad);
                    tabNT.SetValue("ENMNG", att.cantidad_tomada);
                    tabNT.SetValue("ERFME", att.unidad_entrada);
                    tabNT.SetValue("ERFMG", att.cantidad_entrada);
                    tabNT.SetValue("IDNRK", att.componente_mat);
                    tabNT.SetValue("LCMENGE", att.cantidad1);
                    tabNT.SetValue("LGORT", att.almacen);
                    tabNT.SetValue("LMEINS", att.unidad_base);
                    tabNT.SetValue("LMENGE", att.cantidad2);
                    tabNT.SetValue("LTMEINS", att.unidad_base1);
                    tabNT.SetValue("LTMENGE", att.cantidad3);
                    tabNT.SetValue("MAKTX", att.texto_material);
                    tabNT.SetValue("MATNR", att.numero_material);
                    tabNT.SetValue("MENGE", att.cantidad4);
                    tabNT.SetValue("RENGLON", att.indicador_posicion);
                    tabNT.SetValue("RSNUM", att.num_reserva);
                    tabNT.SetValue("RSPOS", att.num_posicion);
                    tabNT.SetValue("TMEINS", att.unidad_base2);
                    tabNT.SetValue("TMENGE", att.cantidad5);
                    tabNT.SetValue("UMLGO", att.almacen_receptor);
                    tabNT.SetValue("URSNUM", att.numero_reserva);
                    tabNT.SetValue("WERKS", att.centro);
                    tabNT.SetValue("EBELN", att.doc_compras);
                    if (att.doc_compras != "")
                    {
                        doc_compras = att.doc_compras;
                        docom = true;
                    }
                    tabNT.SetValue("EBELP", att.pos_compras);
                    tabNT.SetValue("SOBKZ", att.stock_especial);
                    tabNT.SetValue("LIFNR", att.num_proveedor);
                    tabNT.SetValue("UMWRK", att.centro_receptor);
                    tabNT.SetValue("KOSTL", att.centro_coste);
                    tabNT.SetValue("LFSNR", att.num_nota_entrega);
                    tabNT.SetValue("FRBNR", att.num_carta_porte);
                    tabNT.SetValue("EAN11_BME", att.art_europeo);
                    tabNT.SetValue("EAN11_KON", att.cont_europeo);
                    tabNT.SetValue("EANME", att.unidad_base3);
                    tabNT.SetValue("LICHA", att.lote_proveedor);
                    tabNT.SetValue("SAKTO", att.clase_coste);
                    tabNT.SetValue("RECIBIDO", att.recibido);
                    tabNT.SetValue("PROCESADO", att.procesado);
                    tabNT.SetValue("ERROR", att.error);
                    tabNT.SetValue("SPE_CRM_REF_SO", att.orden_referencia);
                    tabNT.SetValue("SPE_CRM_REF_ITEM", att.pos_referencia);
                    tabNT.SetValue("FECHA_RECIBIDO", att.fecha_recibido);
                    tabNT.SetValue("HORA_RECIBIDO", att.hora_recibido);
                    tabNT.SetValue("ZFOLIO_RES", att.folio_reserva);
                    tabNT.SetValue("VBELN", att.num_pedido);
                    tabNT.SetValue("POSNR", att.pos_pedido);
                }
            }
            if (cab.Count > 0)
            {
                foreach (SELECT_cabMov_MDL_Result ca in cab)
                {
                    tabNC.Append();
                    tabNC.SetValue("FOLIO_SAM", ca.folio_sam);
                    tabNC.SetValue("UZEIT", ca.hora);
                    tabNC.SetValue("DATUM", ca.fecha);
                    tabNC.SetValue("FOLIO_SAP", ca.num_doc_material);
                    tabNC.SetValue("WERKS", ca.centro);
                    tabNC.SetValue("LGORT", ca.almacen);
                    tabNC.SetValue("ANULA", ca.anula);
                    tabNC.SetValue("TEXTO", ca.texto);
                    tabNC.SetValue("TEXCAB", ca.texto_cab);
                    tabNC.SetValue("TIPO", ca.tipo);
                    tabNC.SetValue("UMLGO", ca.almacen_receptor);
                    tabNC.SetValue("FACTURA", ca.factura);
                    tabNC.SetValue("PEDIM", ca.usuario_cluster);
                    tabNC.SetValue("LFSNR", ca.num_nota);
                    tabNC.SetValue("HU", ca.num_unidad_medida);
                    tabNC.SetValue("RECIBIDO", ca.recibido);
                    tabNC.SetValue("PROCESADO", ca.procesado);
                    tabNC.SetValue("ERROR", ca.error);
                    tabNC.SetValue("FECHA_CONTABLE", ca.fecha_contable);
                    tabNC.SetValue("UNAME", ca.usuario);
                }
            }
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            if (FUNCTION.GetValue("MENSAJE").ToString().Trim().Equals("Correcto"))
            {
                ZMOVIMIENTOS_SAM(FUNCTION.GetValue("DOCUMENTO").ToString().Trim());
                if (docom) { ZHIS_PEDIDOS_MAT(doc_compras); }
                ACC_movMM.ObtenerInstancia().ElimanarRegistroCreacion(connection, folio_sam);
                foreach (SELECT_posMov_MDL_Result po in pos)
                {
                    ZINV_SAM_MAT(po.numero_material);
                    if (flagI)
                    {
                        ZMM_STOCK_TRANSFERENCIA_MAT(po.numero_material);
                    }
                }
            }
            else
            {
                ACC_movMM.ObtenerInstancia().ActualizarResultado(connection, folio_sam, FUNCTION.GetValue("DOCUMENTO").ToString().Trim(), FUNCTION.GetValue("MENSAJE").ToString().Trim());
            }
        }
        public void ZINV_SAM_MAT(string material)
        {
            try
            {
                RfcRepository sin = rfcDesti.Repository;
                IRfcFunction FUNCTION = sin.CreateFunction("ZINV_SAM_MAT");
                IRfcTable inv = FUNCTION.GetTable("INVENTARIO_SAM");
                IRfcTable invee = FUNCTION.GetTable("INVENTARIO_EE");
                IRfcTable mat = FUNCTION.GetTable("MATERIALES");

                mat.Append();
                mat.SetValue("MATNR", material);
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { }
                foreach (IRfcStructure linea in inv)
                {
                    Inventario i = new Inventario();
                    try
                    {
                        i.MATNR = linea.GetValue("MATNR").ToString();
                        i.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                        i.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                        i.WERKS = linea.GetValue("WERKS").ToString();
                        i.MEINS = linea.GetValue("MEINS").ToString();
                        i.LGORT = linea.GetValue("LGORT").ToString();
                        i.LGOBE = linea.GetValue("LGOBE").ToString();
                        i.CLABS = linea.GetValue("CLABS").ToString();
                        i.CINSM = linea.GetValue("CINSM").ToString();
                        i.CSPEM = linea.GetValue("CSPEM").ToString();
                        i.CUMLM = linea.GetValue("CUMLM").ToString();
                        i.CHARG = linea.GetValue("CHARG").ToString();
                        i.MTART = linea.GetValue("MTART").ToString();
                        i.MATKL = linea.GetValue("MATKL").ToString();
                        i.SERNR = linea.GetValue("SERNR").ToString();
                        i.XCHPF = linea.GetValue("XCHPF").ToString();
                        /************validar si existe el material****************/
                        List<VALIDA_MATERIAL_INV_MM_MDL_Result> n = ACC_movMM.ObtenerInstancia().ValidarMaterialINMM(connection, i).ToList();
                        if (n.Count > 0)
                        {
                            ACC_movMM.ObtenerInstancia().ActualizaInvMM(connection, i);
                        }
                        else
                        {
                            ACC_movMM.ObtenerInstancia().InsertarMM(connection, i);
                        }
                        /*********************************************************/
                    }
                    catch (Exception) { }
                }
                foreach (IRfcStructure linea in invee)
                {
                    InventarioEE i = new InventarioEE();
                    try
                    {
                        i.MATNR = linea.GetValue("MATNR").ToString();
                        i.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                        i.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                        i.WERKS = linea.GetValue("WERKS").ToString();
                        i.MEINS = linea.GetValue("MEINS").ToString();
                        i.LGORT = linea.GetValue("LGORT").ToString();
                        i.LGOBE = linea.GetValue("LGOBE").ToString();
                        i.CLABS = linea.GetValue("CLABS").ToString();
                        i.CINSM = linea.GetValue("CINSM").ToString();
                        i.CSPEM = linea.GetValue("CSPEM").ToString();
                        i.CUMLM = linea.GetValue("CUMLM").ToString();
                        i.CHARG = linea.GetValue("CHARG").ToString();
                        i.MTART = linea.GetValue("MTART").ToString();
                        i.MATKL = linea.GetValue("MATKL").ToString();
                        i.SERNR = linea.GetValue("SERNR").ToString();
                        i.XCHPF = linea.GetValue("XCHPF").ToString();
                        i.SOBKZ = linea.GetValue("SOBKZ").ToString();
                        i.VBELN = linea.GetValue("VBELN").ToString();
                        i.POSNR = linea.GetValue("POSNR").ToString();
                        List<VALIDA_MATERIAL_INV_EE_MDL_Result> e = ACC_movMM.ObtenerInstancia().ValidarMaterialINEE(connection, i).ToList();
                        if (e.Count > 0)
                        {
                            ACC_movMM.ObtenerInstancia().ActulizarInvEE(connection, i);
                        }
                        else
                        {
                            ACC_movMM.ObtenerInstancia().InsertarEE(connection, i);
                        }
                    }
                    catch (Exception) { }
                }
                DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZINV_SAM_MAT", material);
            }
            catch (Exception) { }
        }
        public void ZMM_STOCK_TRANSFERENCIA_MAT(string material)
        {
            RfcRepository mca = rfcDesti.Repository;
            IRfcFunction FUNCTION = mca.CreateFunction("ZMM_STOCK_MATERIAL");
            FUNCTION.SetValue("MATNR", material);
            IRfcTable almc = FUNCTION.GetTable("ZMM_STRA");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { }
            foreach (IRfcStructure linea in almc)
            {
                StockTransferencia almid = new StockTransferencia();
                try
                {
                    almid.WERKS = linea.GetValue("WERKS").ToString();
                    almid.MATNR = linea.GetValue("MATNR").ToString();
                    almid.UMLMC = linea.GetValue("UMLMC").ToString();
                    almid.XCHPF = linea.GetValue("XCHPF").ToString();
                    almid.MEINS = linea.GetValue("MEINS").ToString();
                    almid.MAKTX = linea.GetValue("MAKTX").ToString();
                    almid.MAKTX_E = linea.GetValue("MAKTX_E").ToString();
                    almid.LVORM = linea.GetValue("LVORM").ToString();
                    almid.LGORT = linea.GetValue("LGORT").ToString();
                    almid.CHARG = linea.GetValue("CHARG").ToString();
                    almid.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    almid.KDAUF = linea.GetValue("KDAUF").ToString();
                    almid.KDPOS = linea.GetValue("KDPOS").ToString();
                    almid.UMLGO = linea.GetValue("UMLGO").ToString();
                    List<SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result> valida = DALC_StockTransferencia.obtenerInstancia().ValidarMaterialST(connection, almid).ToList();
                    if (valida.Count > 0)
                    {
                        string operacion = "";
                        operacion = almid.UMLMC.ToString().Substring(0, 1);
                        if (operacion.Equals("-"))
                        {
                            decimal sap, sam, resta;
                            operacion = almid.UMLMC.Replace("-", "");
                            sap = Convert.ToDecimal(operacion.ToString());
                            foreach (SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result va in valida)
                            {
                                sam = Convert.ToDecimal(va.stock_traslado.ToString());
                                resta = sam - sap;
                                string stock = Convert.ToString(resta);
                                DALC_StockTransferencia.obtenerInstancia().ActualizarStockTransfer313(connection, almid, stock);
                            }
                        }
                        else
                        {
                            decimal sap, sam, suma;
                            sap = Convert.ToDecimal(almid.UMLMC.ToString());
                            foreach (SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result va in valida)
                            {
                                sam = Convert.ToDecimal(va.stock_traslado.ToString());
                                suma = sap + sam;
                                string stock = Convert.ToString(suma);
                                DALC_StockTransferencia.obtenerInstancia().ActualizarStockTransfer313(connection, almid, stock);
                            }
                        }
                    }
                    else
                    {
                        DALC_StockTransferencia.obtenerInstancia().IngresarStockTransfer(connection, almid);
                    }
                }
                catch (Exception) { }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_STOCK_TRANSFERENCIA", material);
        }
        /***************************************************************/
        public bool ZORDEN_PP(string orden)
        {
            RfcRepository sin = rfcDesti.Repository;
            IRfcFunction FUNCTION = sin.CreateFunction("ZORDEN_PP");
            IRfcTable tsi = FUNCTION.GetTable("ORDENES_PM");
            IRfcTable OPERACIONES_PM = FUNCTION.GetTable("OPERACIONES_PM");
            IRfcTable COMPONENTES = FUNCTION.GetTable("COMPONENTES");
            IRfcTable oc = FUNCTION.GetTable("ORDENES_CONTROL");
            IRfcTable tx = FUNCTION.GetTable("RCRCO_TEXT");
            FUNCTION.SetValue("AUFNR", orden);
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tsi)
            {
                Plan_Orden_PP ord = new Plan_Orden_PP();
                try
                {
                    ord.AUFNR = linea.GetValue("AUFNR").ToString();
                    ord.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    ord.WERKS = linea.GetValue("WERKS").ToString();
                    ord.AUART = linea.GetValue("AUART").ToString();
                    ord.ESTATUS = linea.GetValue("ESTATUS").ToString();
                    ord.KTEXT = linea.GetValue("KTEXT").ToString();
                    ord.VAPLZ = linea.GetValue("VAPLZ").ToString();
                    ord.WAWRK = linea.GetValue("WAWRK").ToString();
                    ord.QMNUM = linea.GetValue("QMNUM").ToString();
                    ord.USER4 = linea.GetValue("USER4").ToString();
                    ord.ILART = linea.GetValue("ILART").ToString();
                    ord.ANLZU = linea.GetValue("ANLZU").ToString();
                    ord.GSTRP = linea.GetValue("GSTRP").ToString();
                    ord.GLTRP = linea.GetValue("GLTRP").ToString();
                    ord.PRIOK = linea.GetValue("PRIOK").ToString();
                    ord.REVNR = linea.GetValue("REVNR").ToString();
                    ord.AUFPL = Convert.ToInt32(linea.GetValue("AUFPL").ToString()).ToString();
                    ord.TPLNR = linea.GetValue("TPLNR").ToString();
                    ord.PLTXT = linea.GetValue("PLTXT").ToString();
                    ord.MATNR = linea.GetValue("MATNR").ToString();
                    ord.MAKTX = linea.GetValue("MAKTX").ToString();
                    ord.EQKTX = linea.GetValue("EQKTX").ToString();
                    ord.BAUTL = linea.GetValue("BAUTL").ToString();
                    ord.LVORM = linea.GetValue("LVORM").ToString();
                    ord.ERDAT = linea.GetValue("ERDAT").ToString();
                    ord.AEDAT = linea.GetValue("AEDAT").ToString();
                    ord.RMZHL = linea.GetValue("RMZHL").ToString();
                    ord.GAMNG = linea.GetValue("GAMNG").ToString();
                    ord.UEBTK = linea.GetValue("UEBTK").ToString();
                    ord.EQUNR = linea.GetValue("EQUNR").ToString();
                    ord.KUNNR = linea.GetValue("KUNNR").ToString();
                    ord.NAME1 = linea.GetValue("NAME1").ToString();
                    ord.TEXTO = linea.GetValue("TEXTO").ToString();
                    ord.NOTIFICADO = linea.GetValue("NOTIFICADO").ToString();
                    ord.RESTANTE = linea.GetValue("RESTANTE").ToString();
                    ord.AUSP1 = linea.GetValue("AUSP1").ToString();
                    ord.SALES_ORDER = linea.GetValue("SALES_ORDER").ToString();
                    ord.SALES_ORDER_ITEM = linea.GetValue("SALES_ORDER_ITEM").ToString();
                    ord.TCLIENTE = linea.GetValue("TCLIENTE").ToString();
                    ord.ROUTE = linea.GetValue("ROUTE").ToString();
                    ord.STOCK = linea.GetValue("STOCK").ToString();
                    acc_ordenes_pp.ObtenerInstancia().ActualizarOrden(connection, ord);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in OPERACIONES_PM)
            {
                Operaciones_de_ordenesPM opm = new Operaciones_de_ordenesPM();
                try
                {
                    opm.AUFNR = linea.GetValue("AUFNR").ToString();
                    opm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    opm.VORNR = linea.GetValue("VORNR").ToString();
                    opm.UVORN = linea.GetValue("UVORN").ToString();
                    opm.ARBPL = linea.GetValue("ARBPL").ToString();
                    opm.WERKS = linea.GetValue("WERKS").ToString();
                    opm.STEUS = linea.GetValue("STEUS").ToString();
                    opm.KTSCH = linea.GetValue("KTSCH").ToString();
                    opm.ANLZU = linea.GetValue("ANLZU").ToString();
                    opm.LTXA1 = linea.GetValue("LTXA1").ToString();
                    opm.ISMNW = linea.GetValue("ISMNW").ToString();
                    opm.ARBEI = linea.GetValue("ARBEI").ToString();
                    opm.ARBEH = linea.GetValue("ARBEH").ToString();
                    opm.ANZZL = linea.GetValue("ANZZL").ToString();
                    opm.DAUNO = linea.GetValue("DAUNO").ToString();
                    opm.DAUNE = linea.GetValue("DAUNE").ToString();
                    opm.INDET = linea.GetValue("INDET").ToString();
                    opm.LARNT = linea.GetValue("LARNT").ToString();
                    opm.WEMPF = linea.GetValue("WEMPF").ToString();
                    opm.ABLAD = linea.GetValue("ABLAD").ToString();
                    opm.AUFKT = linea.GetValue("AUFKT").ToString();
                    opm.FLG_KMP = linea.GetValue("FLG_KMP").ToString();
                    opm.FLG_FHM = linea.GetValue("FLG_FHM").ToString();
                    opm.TPLNR = linea.GetValue("TPLNR").ToString();
                    opm.MATNR = linea.GetValue("MATNR").ToString();
                    opm.QMNUM = linea.GetValue("QMNUM").ToString();
                    opm.NPLDA = linea.GetValue("NPLDA").ToString();
                    opm.AUDISP = linea.GetValue("AUDISP").ToString();
                    opm.FSAVZ = linea.GetValue("FSAVZ").ToString();
                    opm.FSAVD = linea.GetValue("FSAVD").ToString();
                    opm.FSEDD = linea.GetValue("FSEDD").ToString();
                    opm.FSEDZ = linea.GetValue("FSEDZ").ToString();
                    opm.EQUNR = linea.GetValue("EQUNR").ToString();
                    opm.MEINS = linea.GetValue("MEINS").ToString();
                    opm.MAKTX = linea.GetValue("MAKTX").ToString();
                    opm.ESPECIFICACION = linea.GetValue("ESPECIFICACION").ToString();
                    opm.OBSERVACIONES = linea.GetValue("OBSERVACIONES").ToString();
                    opm.VGW01 = linea.GetValue("VGW01").ToString();
                    opm.VGE01 = linea.GetValue("VGE01").ToString();
                    opm.SYSTEM_STATUS = linea.GetValue("SYSTEM_STATUS").ToString();
                    acc_ordenes_pp.ObtenerInstancia().ActulizarOperaciones(connection, opm);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in COMPONENTES)
            {
                Componentes_de_ordenesPM copm = new Componentes_de_ordenesPM();
                try
                {
                    copm.AUFNR = linea.GetValue("AUFNR").ToString();
                    copm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    copm.POSNR = linea.GetValue("POSNR").ToString();
                    copm.MATNR = linea.GetValue("MATNR").ToString();
                    copm.MATXT = linea.GetValue("MATXT").ToString();
                    copm.MENGE = linea.GetValue("MENGE").ToString();
                    copm.EINHEIT = linea.GetValue("EINHEIT").ToString();
                    copm.POSTP = linea.GetValue("POSTP").ToString();
                    copm.SOBKZ_D = linea.GetValue("SOBKZ_D").ToString();
                    copm.LGORT = linea.GetValue("LGORT").ToString();
                    copm.WERKS = linea.GetValue("WERKS").ToString();
                    copm.VORNR = linea.GetValue("VORNR").ToString();
                    copm.CHARG = linea.GetValue("CHARG").ToString();
                    copm.WEMPF = linea.GetValue("WEMPF").ToString();
                    copm.ABLAD = linea.GetValue("ABLAD").ToString();
                    copm.XLOEK = linea.GetValue("XLOEK").ToString();
                    copm.SCHGT = linea.GetValue("SCHGT").ToString();
                    copm.RGEKZ = linea.GetValue("RGEKZ").ToString();
                    copm.AUDISP = linea.GetValue("AUDISP").ToString();
                    copm.BWART = linea.GetValue("BWART").ToString();
                    copm.SPECIAL_STOCK = linea.GetValue("SPECIAL_STOCK").ToString();
                    acc_ordenes_pp.ObtenerInstancia().ActualizarComponentes(connection, copm);
                }
                catch (Exception) { return false; }
            }

            foreach (IRfcStructure linea in oc)
            {
                ORDENES_CONTROL octrl = new ORDENES_CONTROL();
                try
                {
                    octrl.AUFNR = linea.GetValue("AUFNR").ToString();
                    octrl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    octrl.ERNAM = linea.GetValue("ERNAM").ToString();
                    octrl.ERDAT = linea.GetValue("ERDAT").ToString();
                    octrl.AENAM = linea.GetValue("AENAM").ToString();
                    octrl.AEDAT = linea.GetValue("AEDAT").ToString();
                    octrl.WERKS = linea.GetValue("WERKS").ToString();
                    acc_ordenes_pp.ObtenerInstancia().ActualizarControl(connection, octrl);
                }
                catch (Exception) { return false; }
            }

            foreach (IRfcStructure linea in tx)
            {
                textos_actividades txt = new textos_actividades();
                try
                {
                    txt.ARBPL = linea.GetValue("ARBPL").ToString();
                    txt.TEXT1 = linea.GetValue("TEXT1").ToString();
                    txt.NOTX1 = linea.GetValue("NOTX1").ToString();
                    txt.TEXT2 = linea.GetValue("TEXT2").ToString();
                    txt.NOTX2 = linea.GetValue("NOTX2").ToString();
                    txt.TEXT3 = linea.GetValue("TEXT3").ToString();
                    txt.NOTX3 = linea.GetValue("NOTX3").ToString();
                    txt.TEXT4 = linea.GetValue("TEXT4").ToString();
                    txt.NOTX4 = linea.GetValue("NOTX4").ToString();
                    txt.TEXT5 = linea.GetValue("TEXT5").ToString();
                    txt.NOTX5 = linea.GetValue("NOTX5").ToString();
                    txt.TEXT6 = linea.GetValue("TEXT6").ToString();
                    txt.NOTX6 = linea.GetValue("NOTX6").ToString();
                    txt.WERKS = linea.GetValue("WERKS").ToString();
                    acc_ordenes_pp.ObtenerInstancia().ActualizarActividades(connection, txt);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZORDENES_PP", orden);
            return true;
        }
        public bool ZORDENES_DELTA(string centro)
        {
            List<SELECT_Clase_Orden_MDL_Result> cl = ACC_ClaseOrdenPP.ObtenerInstancia().claseOrden(connection, centro).ToList();
            foreach (SELECT_Clase_Orden_MDL_Result c in cl)
            {
                RfcRepository sin = rfcDesti.Repository;
                IRfcFunction FUNCTION = sin.CreateFunction("ZORDENES_PP");
                IRfcTable tsi = FUNCTION.GetTable("ORDENES_PM");
                IRfcTable OPERACIONES_PM = FUNCTION.GetTable("OPERACIONES_PM");
                IRfcTable COMPONENTES = FUNCTION.GetTable("COMPONENTES");
                IRfcTable oc = FUNCTION.GetTable("ORDENES_CONTROL");
                IRfcTable tx = FUNCTION.GetTable("RCRCO_TEXT");
                FUNCTION.SetValue("PWERK", centro);
                FUNCTION.SetValue("AUART", c.clase_orden);
                FUNCTION.SetValue("DELTA", "X");
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception)
                {
                    return false;
                }
                foreach (IRfcStructure linea in tsi)
                {
                    string orden = "";
                    try
                    {
                        orden = linea.GetValue("AUFNR").ToString();
                        acc_ordenes_pp.ObtenerInstancia().EliminarOrdenDelta(connection, orden);
                    }
                    catch (Exception) { return false; }
                    Plan_Orden_PP ord = new Plan_Orden_PP();
                    try
                    {
                        ord.AUFNR = linea.GetValue("AUFNR").ToString();
                        ord.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        ord.WERKS = linea.GetValue("WERKS").ToString();
                        ord.AUART = linea.GetValue("AUART").ToString();
                        ord.ESTATUS = linea.GetValue("ESTATUS").ToString();
                        ord.KTEXT = linea.GetValue("KTEXT").ToString();
                        ord.VAPLZ = linea.GetValue("VAPLZ").ToString();
                        ord.WAWRK = linea.GetValue("WAWRK").ToString();
                        ord.QMNUM = linea.GetValue("QMNUM").ToString();
                        ord.USER4 = linea.GetValue("USER4").ToString();
                        ord.ILART = linea.GetValue("ILART").ToString();
                        ord.ANLZU = linea.GetValue("ANLZU").ToString();
                        ord.GSTRP = linea.GetValue("GSTRP").ToString();
                        ord.GLTRP = linea.GetValue("GLTRP").ToString();
                        ord.PRIOK = linea.GetValue("PRIOK").ToString();
                        ord.REVNR = linea.GetValue("REVNR").ToString();
                        ord.AUFPL = Convert.ToInt32(linea.GetValue("AUFPL").ToString()).ToString();
                        ord.TPLNR = linea.GetValue("TPLNR").ToString();
                        ord.PLTXT = linea.GetValue("PLTXT").ToString();
                        ord.MATNR = linea.GetValue("MATNR").ToString();
                        ord.MAKTX = linea.GetValue("MAKTX").ToString();
                        ord.EQKTX = linea.GetValue("EQKTX").ToString();
                        ord.BAUTL = linea.GetValue("BAUTL").ToString();
                        ord.LVORM = linea.GetValue("LVORM").ToString();
                        ord.ERDAT = linea.GetValue("ERDAT").ToString();
                        ord.AEDAT = linea.GetValue("AEDAT").ToString();
                        ord.RMZHL = linea.GetValue("RMZHL").ToString();
                        ord.GAMNG = linea.GetValue("GAMNG").ToString();
                        ord.UEBTK = linea.GetValue("UEBTK").ToString();
                        ord.EQUNR = linea.GetValue("EQUNR").ToString();
                        ord.KUNNR = linea.GetValue("KUNNR").ToString();
                        ord.NAME1 = linea.GetValue("NAME1").ToString();
                        ord.TEXTO = linea.GetValue("TEXTO").ToString();
                        ord.NOTIFICADO = linea.GetValue("NOTIFICADO").ToString();
                        ord.RESTANTE = linea.GetValue("RESTANTE").ToString();
                        ord.AUSP1 = linea.GetValue("AUSP1").ToString();
                        ord.SALES_ORDER = linea.GetValue("SALES_ORDER").ToString();
                        ord.SALES_ORDER_ITEM = linea.GetValue("SALES_ORDER_ITEM").ToString();
                        ord.TCLIENTE = linea.GetValue("TCLIENTE").ToString();
                        ord.ROUTE = linea.GetValue("ROUTE").ToString();
                        ord.STOCK = linea.GetValue("STOCK").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarProductionPlan(connection, ord);
                    }
                    catch (Exception) { return false; }
                }
                foreach (IRfcStructure linea in OPERACIONES_PM)
                {
                    Operaciones_de_ordenesPM opm = new Operaciones_de_ordenesPM();
                    try
                    {
                        opm.AUFNR = linea.GetValue("AUFNR").ToString();
                        opm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        opm.VORNR = linea.GetValue("VORNR").ToString();
                        opm.UVORN = linea.GetValue("UVORN").ToString();
                        opm.ARBPL = linea.GetValue("ARBPL").ToString();
                        opm.WERKS = linea.GetValue("WERKS").ToString();
                        opm.STEUS = linea.GetValue("STEUS").ToString();
                        opm.KTSCH = linea.GetValue("KTSCH").ToString();
                        opm.ANLZU = linea.GetValue("ANLZU").ToString();
                        opm.LTXA1 = linea.GetValue("LTXA1").ToString();
                        opm.ISMNW = linea.GetValue("ISMNW").ToString();
                        opm.ARBEI = linea.GetValue("ARBEI").ToString();
                        opm.ARBEH = linea.GetValue("ARBEH").ToString();
                        opm.ANZZL = linea.GetValue("ANZZL").ToString();
                        opm.DAUNO = linea.GetValue("DAUNO").ToString();
                        opm.DAUNE = linea.GetValue("DAUNE").ToString();
                        opm.INDET = linea.GetValue("INDET").ToString();
                        opm.LARNT = linea.GetValue("LARNT").ToString();
                        opm.WEMPF = linea.GetValue("WEMPF").ToString();
                        opm.ABLAD = linea.GetValue("ABLAD").ToString();
                        opm.AUFKT = linea.GetValue("AUFKT").ToString();
                        opm.FLG_KMP = linea.GetValue("FLG_KMP").ToString();
                        opm.FLG_FHM = linea.GetValue("FLG_FHM").ToString();
                        opm.TPLNR = linea.GetValue("TPLNR").ToString();
                        opm.MATNR = linea.GetValue("MATNR").ToString();
                        opm.QMNUM = linea.GetValue("QMNUM").ToString();
                        opm.NPLDA = linea.GetValue("NPLDA").ToString();
                        opm.AUDISP = linea.GetValue("AUDISP").ToString();
                        opm.FSAVZ = linea.GetValue("FSAVZ").ToString();
                        opm.FSAVD = linea.GetValue("FSAVD").ToString();
                        opm.FSEDD = linea.GetValue("FSEDD").ToString();
                        opm.FSEDZ = linea.GetValue("FSEDZ").ToString();
                        opm.EQUNR = linea.GetValue("EQUNR").ToString();
                        opm.MEINS = linea.GetValue("MEINS").ToString();
                        opm.MAKTX = linea.GetValue("MAKTX").ToString();
                        opm.ESPECIFICACION = linea.GetValue("ESPECIFICACION").ToString();
                        opm.OBSERVACIONES = linea.GetValue("OBSERVACIONES").ToString();
                        opm.VGW01 = linea.GetValue("VGW01").ToString();
                        opm.VGE01 = linea.GetValue("VGE01").ToString();
                        opm.SYSTEM_STATUS = linea.GetValue("SYSTEM_STATUS").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarOperaciones_de_ordenesPM(connection, opm);
                    }
                    catch (Exception) { return false; }
                }
                foreach (IRfcStructure linea in COMPONENTES)
                {
                    Componentes_de_ordenesPM copm = new Componentes_de_ordenesPM();
                    try
                    {
                        copm.AUFNR = linea.GetValue("AUFNR").ToString();
                        copm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        copm.POSNR = linea.GetValue("POSNR").ToString();
                        copm.MATNR = linea.GetValue("MATNR").ToString();
                        copm.MATXT = linea.GetValue("MATXT").ToString();
                        copm.MENGE = linea.GetValue("MENGE").ToString();
                        copm.EINHEIT = linea.GetValue("EINHEIT").ToString();
                        copm.POSTP = linea.GetValue("POSTP").ToString();
                        copm.SOBKZ_D = linea.GetValue("SOBKZ_D").ToString();
                        copm.LGORT = linea.GetValue("LGORT").ToString();
                        copm.WERKS = linea.GetValue("WERKS").ToString();
                        copm.VORNR = linea.GetValue("VORNR").ToString();
                        copm.CHARG = linea.GetValue("CHARG").ToString();
                        copm.WEMPF = linea.GetValue("WEMPF").ToString();
                        copm.ABLAD = linea.GetValue("ABLAD").ToString();
                        copm.XLOEK = linea.GetValue("XLOEK").ToString();
                        copm.SCHGT = linea.GetValue("SCHGT").ToString();
                        copm.RGEKZ = linea.GetValue("RGEKZ").ToString();
                        copm.AUDISP = linea.GetValue("AUDISP").ToString();
                        copm.BWART = linea.GetValue("BWART").ToString();
                        copm.SPECIAL_STOCK = linea.GetValue("SPECIAL_STOCK").ToString();
                        copm.ZREST = linea.GetValue("ZREST").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarComponentes_de_ordenesPM(connection, copm);
                    }
                    catch (Exception) { return false; }
                }

                foreach (IRfcStructure linea in oc)
                {
                    ORDENES_CONTROL octrl = new ORDENES_CONTROL();
                    try
                    {
                        octrl.AUFNR = linea.GetValue("AUFNR").ToString();
                        octrl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        octrl.ERNAM = linea.GetValue("ERNAM").ToString();
                        octrl.ERDAT = linea.GetValue("ERDAT").ToString();
                        octrl.AENAM = linea.GetValue("AENAM").ToString();
                        octrl.AEDAT = linea.GetValue("AEDAT").ToString();
                        octrl.WERKS = linea.GetValue("WERKS").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarOrdenes_control(connection, octrl);
                    }
                    catch (Exception) { return false; }
                }

                foreach (IRfcStructure linea in tx)
                {
                    textos_actividades txt = new textos_actividades();
                    try
                    {
                        txt.ARBPL = linea.GetValue("ARBPL").ToString();
                        txt.TEXT1 = linea.GetValue("TEXT1").ToString();
                        txt.NOTX1 = linea.GetValue("NOTX1").ToString();
                        txt.TEXT2 = linea.GetValue("TEXT2").ToString();
                        txt.NOTX2 = linea.GetValue("NOTX2").ToString();
                        txt.TEXT3 = linea.GetValue("TEXT3").ToString();
                        txt.NOTX3 = linea.GetValue("NOTX3").ToString();
                        txt.TEXT4 = linea.GetValue("TEXT4").ToString();
                        txt.NOTX4 = linea.GetValue("NOTX4").ToString();
                        txt.TEXT5 = linea.GetValue("TEXT5").ToString();
                        txt.NOTX5 = linea.GetValue("NOTX5").ToString();
                        txt.TEXT6 = linea.GetValue("TEXT6").ToString();
                        txt.NOTX6 = linea.GetValue("NOTX6").ToString();
                        txt.WERKS = linea.GetValue("WERKS").ToString();
                        acc_ordenes_pp.ObtenerInstancia().Insertartexto_actividades(connection, txt);
                    }
                    catch (Exception) { return false; }
                }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZORDENES_DELTA", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZORDENES_DELTA", "X", centro);
            return true;
        }
        public bool ZORDENES_PP(string centro)
        {
            List<SELECT_Clase_Orden_MDL_Result> cl = ACC_ClaseOrdenPP.ObtenerInstancia().claseOrden(connection, centro).ToList();
            foreach (SELECT_Clase_Orden_MDL_Result c in cl)
            {
                RfcRepository sin = rfcDesti.Repository;
                IRfcFunction FUNCTION = sin.CreateFunction("ZORDENES_PP");
                IRfcTable tsi = FUNCTION.GetTable("ORDENES_PM");
                IRfcTable OPERACIONES_PM = FUNCTION.GetTable("OPERACIONES_PM");
                IRfcTable COMPONENTES = FUNCTION.GetTable("COMPONENTES");
                IRfcTable oc = FUNCTION.GetTable("ORDENES_CONTROL");
                IRfcTable tx = FUNCTION.GetTable("RCRCO_TEXT");
                FUNCTION.SetValue("PWERK", centro);
                FUNCTION.SetValue("AUART", c.clase_orden);
                FUNCTION.SetValue("DELTA", "");
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception) { return false; }
                ACC_ClaseOrdenPP.ObtenerInstancia().DeleteOrdenesPP(connection, centro);
                foreach (IRfcStructure linea in tsi)
                {
                    Plan_Orden_PP ord = new Plan_Orden_PP();
                    try
                    {
                        ord.AUFNR = linea.GetValue("AUFNR").ToString();
                        ord.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        ord.WERKS = linea.GetValue("WERKS").ToString();
                        ord.AUART = linea.GetValue("AUART").ToString();
                        ord.ESTATUS = linea.GetValue("ESTATUS").ToString();
                        ord.KTEXT = linea.GetValue("KTEXT").ToString();
                        ord.VAPLZ = linea.GetValue("VAPLZ").ToString();
                        ord.WAWRK = linea.GetValue("WAWRK").ToString();
                        ord.QMNUM = linea.GetValue("QMNUM").ToString();
                        ord.USER4 = linea.GetValue("USER4").ToString();
                        ord.ILART = linea.GetValue("ILART").ToString();
                        ord.ANLZU = linea.GetValue("ANLZU").ToString();
                        ord.GSTRP = linea.GetValue("GSTRP").ToString();
                        ord.GLTRP = linea.GetValue("GLTRP").ToString();
                        ord.PRIOK = linea.GetValue("PRIOK").ToString();
                        ord.REVNR = linea.GetValue("REVNR").ToString();
                        ord.AUFPL = Convert.ToInt32(linea.GetValue("AUFPL").ToString()).ToString();
                        ord.TPLNR = linea.GetValue("TPLNR").ToString();
                        ord.PLTXT = linea.GetValue("PLTXT").ToString();
                        ord.MATNR = linea.GetValue("MATNR").ToString();
                        ord.MAKTX = linea.GetValue("MAKTX").ToString();
                        ord.EQKTX = linea.GetValue("EQKTX").ToString();
                        ord.BAUTL = linea.GetValue("BAUTL").ToString();
                        ord.LVORM = linea.GetValue("LVORM").ToString();
                        ord.ERDAT = linea.GetValue("ERDAT").ToString();
                        ord.AEDAT = linea.GetValue("AEDAT").ToString();
                        ord.RMZHL = linea.GetValue("RMZHL").ToString();
                        ord.GAMNG = linea.GetValue("GAMNG").ToString();
                        ord.UEBTK = linea.GetValue("UEBTK").ToString();
                        ord.EQUNR = linea.GetValue("EQUNR").ToString();
                        ord.KUNNR = linea.GetValue("KUNNR").ToString();
                        ord.NAME1 = linea.GetValue("NAME1").ToString();
                        ord.TEXTO = linea.GetValue("TEXTO").ToString();
                        ord.NOTIFICADO = linea.GetValue("NOTIFICADO").ToString();
                        ord.RESTANTE = linea.GetValue("RESTANTE").ToString();
                        ord.AUSP1 = linea.GetValue("AUSP1").ToString();
                        ord.SALES_ORDER = linea.GetValue("SALES_ORDER").ToString();
                        ord.SALES_ORDER_ITEM = linea.GetValue("SALES_ORDER_ITEM").ToString();
                        ord.TCLIENTE = linea.GetValue("TCLIENTE").ToString();
                        ord.ROUTE = linea.GetValue("ROUTE").ToString();
                        ord.STOCK = linea.GetValue("STOCK").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarProductionPlan(connection, ord);
                    }
                    catch (Exception) { return false; }
                }
                foreach (IRfcStructure linea in OPERACIONES_PM)
                {
                    Operaciones_de_ordenesPM opm = new Operaciones_de_ordenesPM();
                    try
                    {
                        opm.AUFNR = linea.GetValue("AUFNR").ToString();
                        opm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        opm.VORNR = linea.GetValue("VORNR").ToString();
                        opm.UVORN = linea.GetValue("UVORN").ToString();
                        opm.ARBPL = linea.GetValue("ARBPL").ToString();
                        opm.WERKS = linea.GetValue("WERKS").ToString();
                        opm.STEUS = linea.GetValue("STEUS").ToString();
                        opm.KTSCH = linea.GetValue("KTSCH").ToString();
                        opm.ANLZU = linea.GetValue("ANLZU").ToString();
                        opm.LTXA1 = linea.GetValue("LTXA1").ToString();
                        opm.ISMNW = linea.GetValue("ISMNW").ToString();
                        opm.ARBEI = linea.GetValue("ARBEI").ToString();
                        opm.ARBEH = linea.GetValue("ARBEH").ToString();
                        opm.ANZZL = linea.GetValue("ANZZL").ToString();
                        opm.DAUNO = linea.GetValue("DAUNO").ToString();
                        opm.DAUNE = linea.GetValue("DAUNE").ToString();
                        opm.INDET = linea.GetValue("INDET").ToString();
                        opm.LARNT = linea.GetValue("LARNT").ToString();
                        opm.WEMPF = linea.GetValue("WEMPF").ToString();
                        opm.ABLAD = linea.GetValue("ABLAD").ToString();
                        opm.AUFKT = linea.GetValue("AUFKT").ToString();
                        opm.FLG_KMP = linea.GetValue("FLG_KMP").ToString();
                        opm.FLG_FHM = linea.GetValue("FLG_FHM").ToString();
                        opm.TPLNR = linea.GetValue("TPLNR").ToString();
                        opm.MATNR = linea.GetValue("MATNR").ToString();
                        opm.QMNUM = linea.GetValue("QMNUM").ToString();
                        opm.NPLDA = linea.GetValue("NPLDA").ToString();
                        opm.AUDISP = linea.GetValue("AUDISP").ToString();
                        opm.FSAVZ = linea.GetValue("FSAVZ").ToString();
                        opm.FSAVD = linea.GetValue("FSAVD").ToString();
                        opm.FSEDD = linea.GetValue("FSEDD").ToString();
                        opm.FSEDZ = linea.GetValue("FSEDZ").ToString();
                        opm.EQUNR = linea.GetValue("EQUNR").ToString();
                        opm.MEINS = linea.GetValue("MEINS").ToString();
                        opm.MAKTX = linea.GetValue("MAKTX").ToString();
                        opm.ESPECIFICACION = linea.GetValue("ESPECIFICACION").ToString();
                        opm.OBSERVACIONES = linea.GetValue("OBSERVACIONES").ToString();
                        opm.VGW01 = linea.GetValue("VGW01").ToString();
                        opm.VGE01 = linea.GetValue("VGE01").ToString();
                        opm.SYSTEM_STATUS = linea.GetValue("SYSTEM_STATUS").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarOperaciones_de_ordenesPM(connection, opm);
                    }
                    catch (Exception) { return false; }
                }
                foreach (IRfcStructure linea in COMPONENTES)
                {
                    Componentes_de_ordenesPM copm = new Componentes_de_ordenesPM();
                    try
                    {
                        copm.AUFNR = linea.GetValue("AUFNR").ToString();
                        copm.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        copm.POSNR = linea.GetValue("POSNR").ToString();
                        copm.MATNR = linea.GetValue("MATNR").ToString();
                        copm.MATXT = linea.GetValue("MATXT").ToString();
                        copm.MENGE = linea.GetValue("MENGE").ToString();
                        copm.EINHEIT = linea.GetValue("EINHEIT").ToString();
                        copm.POSTP = linea.GetValue("POSTP").ToString();
                        copm.SOBKZ_D = linea.GetValue("SOBKZ_D").ToString();
                        copm.LGORT = linea.GetValue("LGORT").ToString();
                        copm.WERKS = linea.GetValue("WERKS").ToString();
                        copm.VORNR = linea.GetValue("VORNR").ToString();
                        copm.CHARG = linea.GetValue("CHARG").ToString();
                        copm.WEMPF = linea.GetValue("WEMPF").ToString();
                        copm.ABLAD = linea.GetValue("ABLAD").ToString();
                        copm.XLOEK = linea.GetValue("XLOEK").ToString();
                        copm.SCHGT = linea.GetValue("SCHGT").ToString();
                        copm.RGEKZ = linea.GetValue("RGEKZ").ToString();
                        copm.AUDISP = linea.GetValue("AUDISP").ToString();
                        copm.BWART = linea.GetValue("BWART").ToString();
                        copm.SPECIAL_STOCK = linea.GetValue("SPECIAL_STOCK").ToString();
                        copm.ZREST = linea.GetValue("ZREST").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarComponentes_de_ordenesPM(connection, copm);
                    }
                    catch (Exception) { return false; }
                }

                foreach (IRfcStructure linea in oc)
                {
                    ORDENES_CONTROL octrl = new ORDENES_CONTROL();
                    try
                    {
                        octrl.AUFNR = linea.GetValue("AUFNR").ToString();
                        octrl.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                        octrl.ERNAM = linea.GetValue("ERNAM").ToString();
                        octrl.ERDAT = linea.GetValue("ERDAT").ToString();
                        octrl.AENAM = linea.GetValue("AENAM").ToString();
                        octrl.AEDAT = linea.GetValue("AEDAT").ToString();
                        octrl.WERKS = linea.GetValue("WERKS").ToString();
                        acc_ordenes_pp.ObtenerInstancia().InsertarOrdenes_control(connection, octrl);
                    }
                    catch (Exception) { return false; }
                }

                foreach (IRfcStructure linea in tx)
                {
                    textos_actividades txt = new textos_actividades();
                    try
                    {
                        txt.ARBPL = linea.GetValue("ARBPL").ToString();
                        txt.TEXT1 = linea.GetValue("TEXT1").ToString();
                        txt.NOTX1 = linea.GetValue("NOTX1").ToString();
                        txt.TEXT2 = linea.GetValue("TEXT2").ToString();
                        txt.NOTX2 = linea.GetValue("NOTX2").ToString();
                        txt.TEXT3 = linea.GetValue("TEXT3").ToString();
                        txt.NOTX3 = linea.GetValue("NOTX3").ToString();
                        txt.TEXT4 = linea.GetValue("TEXT4").ToString();
                        txt.NOTX4 = linea.GetValue("NOTX4").ToString();
                        txt.TEXT5 = linea.GetValue("TEXT5").ToString();
                        txt.NOTX5 = linea.GetValue("NOTX5").ToString();
                        txt.TEXT6 = linea.GetValue("TEXT6").ToString();
                        txt.NOTX6 = linea.GetValue("NOTX6").ToString();
                        txt.WERKS = linea.GetValue("WERKS").ToString();
                        acc_ordenes_pp.ObtenerInstancia().Insertartexto_actividades(connection, txt);
                    }
                    catch (Exception) { return false; }
                }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZORDENES_PP", "Correcto");
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZORDENES_PP", "X", centro);
            return true;
        }
        public bool ZMOVIMIENTOS_SAM(string doc)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZMOVIMIENTO_SAM");
            FUNCTION.SetValue("MBLNR", doc);
            IRfcTable ta1 = FUNCTION.GetTable("TMIGO_CAB");
            IRfcTable ta2 = FUNCTION.GetTable("TMIGO_POS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                TMIGO_CAB um = new TMIGO_CAB();
                try
                {
                    um.MBLNR = linea.GetValue("MBLNR").ToString();
                    um.MJAHR = linea.GetValue("MJAHR").ToString();
                    um.BLDAT = linea.GetValue("BLDAT").ToString();
                    um.BUDAT = linea.GetValue("BUDAT").ToString();
                    um.LFSNR = linea.GetValue("LFSNR").ToString();
                    um.FRBNR = linea.GetValue("FRBNR").ToString();
                    um.BKTXT = linea.GetValue("BKTXT").ToString();
                    um.LIFNR = linea.GetValue("LIFNR").ToString();
                    um.NAME1 = linea.GetValue("NAME1").ToString();
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.UMLGO = linea.GetValue("UMLGO").ToString();
                    um.LVORM = linea.GetValue("LVORM").ToString();
                    DALC_ListaMov.ObtenerInstancia().IngresaTMIGO_CAB(connection, um);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                TMIGO_POS um = new TMIGO_POS();
                try
                {
                    um.MBLNR = linea.GetValue("MBLNR").ToString();
                    um.ZEILE = linea.GetValue("ZEILE").ToString();
                    um.LIFNR = linea.GetValue("LIFNR").ToString();
                    um.CHARG = linea.GetValue("CHARG").ToString();
                    um.BWART = linea.GetValue("BWART").ToString();
                    um.EBELN = linea.GetValue("EBELN").ToString();
                    um.EBELP = linea.GetValue("EBELP").ToString();
                    um.ABLAD = linea.GetValue("ABLAD").ToString();
                    um.WEMPF = linea.GetValue("WEMPF").ToString();
                    um.MATNR = linea.GetValue("MATNR").ToString();
                    um.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                    um.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                    um.ERFMG = linea.GetValue("ERFMG").ToString();
                    um.ERFME = linea.GetValue("ERFME").ToString();
                    um.KOSTL = linea.GetValue("KOSTL").ToString();
                    um.AUFNR = linea.GetValue("AUFNR").ToString();
                    um.BUKRS = linea.GetValue("BUKRS").ToString();
                    um.SAKTO = linea.GetValue("SAKTO").ToString();
                    um.IDNLF = linea.GetValue("IDNLF").ToString();
                    um.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    um.WERKS = linea.GetValue("WERKS").ToString();
                    um.NAME2 = linea.GetValue("NAME2").ToString();
                    um.LGORT = linea.GetValue("LGORT").ToString();
                    um.LGOBE = linea.GetValue("LGOBE").ToString();
                    um.EMLIF = linea.GetValue("EMLIF").ToString();
                    um.NAME3 = linea.GetValue("NAME3").ToString();
                    um.SHKZG = linea.GetValue("SHKZG").ToString();
                    um.LPROVEDOR = linea.GetValue("LPROVEDOR").ToString();
                    um.MATKL = linea.GetValue("MATKL").ToString();
                    um.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    um.UMLGO = linea.GetValue("UMLGO").ToString();
                    um.LICHA = linea.GetValue("LICHA").ToString();
                    DALC_ListaMov.ObtenerInstancia().IngresaTMIGO_POS(connection, um);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_LIST_MOV_VIS", doc);
            return true;
        }
        public bool ZMATERIALES_PP(string centro) //Operación
        {
            RfcRepository boom = rfcDesti.Repository;
            IRfcFunction FUNCTION = boom.CreateFunction("ZMATERIALES_PP");
            IRfcTable MATE = FUNCTION.GetTable("MATERIALES");
            FUNCTION.SetValue("WERKS", centro);
            FUNCTION.SetValue("DELTA", "X");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure bm in MATE)
            {
                Materiales m = new Materiales();
                try
                {
                    m.MATNR = bm.GetValue("MATNR").ToString();
                    m.WERKS = bm.GetValue("WERKS").ToString();
                    m.MEINS = bm.GetValue("MEINS").ToString();
                    m.MATKL = bm.GetValue("MATKL").ToString();
                    m.MTART = bm.GetValue("MTART").ToString();
                    m.XCHPF = bm.GetValue("XCHPF").ToString();
                    m.MAKTX_ES = bm.GetValue("MAKTX_ES").ToString();
                    m.MAKTX_EN = bm.GetValue("MAKTX_EN").ToString();
                    List<VALIDA_MATERIAL_MDL_Result> mat = ACC_Materiales.ObtenerInstancia().ValidarMaterial(connection, m).ToList();
                    if (mat.Count > 0)
                    {
                        ACC_Materiales.ObtenerInstancia().ActualizaMaterial(connection, m);
                    }
                    else
                    {
                        ACC_Materiales.ObtenerInstancia().insertMat(connection, m);
                    }
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZMATERIALES_PP", "X", centro);
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMATERIALES_PP", "Correcto");
            return true;
        }
        /***************************************************************/
        public bool ZSOLPED_SAM(string solped)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZSOLPED_SAM");
            FUNCTION.SetValue("BANFN", solped);
            IRfcTable cabSol = FUNCTION.GetTable("SOLPED_CAB");
            IRfcTable posSol = FUNCTION.GetTable("SOLPED_ITEMS");
            IRfcTable serSol = FUNCTION.GetTable("SOLPED_SERVICIOS");
            IRfcTable txtCab = FUNCTION.GetTable("TEXTO_CAB");
            IRfcTable txtPos = FUNCTION.GetTable("TEXTO_POS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure ln in cabSol)
            {
                SolPed_Vis_Cab j = new SolPed_Vis_Cab();
                try
                {
                    j.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    j.FOLIO_SAP = ln.GetValue("FOLIO_SAP").ToString();
                    j.BBSRT = ln.GetValue("BBSRT").ToString();
                    j.TEXTO_CAB = ln.GetValue("TEXTO_CAB").ToString();
                    j.AFNAM = ln.GetValue("AFNAM").ToString();
                    j.EWERK = ln.GetValue("EWERK").ToString();
                    j.BADAT = ln.GetValue("BADAT").ToString();
                    j.LFDAT = ln.GetValue("LFDAT").ToString();
                    j.FRGDT = ln.GetValue("FRGDT").ToString();
                    j.BSART = ln.GetValue("BSART").ToString();
                    j.WERKS = ln.GetValue("WERKS").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaCabeceraSolPed(connection, j);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure ln in posSol)
            {
                SolPed_Pos p = new SolPed_Pos();
                try
                {
                    p.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    p.PREQ_ITEM = ln.GetValue("PREQ_ITEM").ToString();
                    p.ITEM_CAT = ln.GetValue("ITEM_CAT").ToString();
                    p.MATERIAL = ln.GetValue("MATERIAL").ToString();
                    p.DATUM = ln.GetValue("DATUM").ToString();
                    p.UZEIT = ln.GetValue("UZEIT").ToString();
                    p.FOLIO_SAP = ln.GetValue("FOLIO_SAP").ToString();
                    p.DOC_TYPE = ln.GetValue("DOC_TYPE").ToString();
                    p.ACCTASSCAT = ln.GetValue("ACCTASSCAT").ToString();
                    p.SHORT_TEXT = ln.GetValue("SHORT_TEXT").ToString();
                    p.QUANTITY = ln.GetValue("QUANTITY").ToString();
                    p.UNIT = ln.GetValue("UNIT").ToString();
                    p.DEL_DATCAT = ln.GetValue("DEL_DATCAT").ToString();
                    p.DELIV_DATE = ln.GetValue("DELIV_DATE").ToString();
                    p.MAT_GRP = ln.GetValue("MAT_GRP").ToString();
                    p.PLANT = ln.GetValue("PLANT").ToString();
                    p.STORE_LOC = ln.GetValue("STORE_LOC").ToString();
                    p.PUR_GROUP = ln.GetValue("PUR_GROUP").ToString();
                    p.PREQ_NAME = ln.GetValue("PREQ_NAME").ToString();
                    p.PREQ_DATE = ln.GetValue("PREQ_DATE").ToString();
                    p.DES_VENDOR = ln.GetValue("DES_VENDOR").ToString();
                    p.LIFNR = ln.GetValue("LIFNR").ToString();
                    p.TABIX = ln.GetValue("TABIX").ToString();
                    p.EPSTP = ln.GetValue("EPSTP").ToString();
                    p.INFNR = ln.GetValue("INFNR").ToString();
                    p.EKORG = ln.GetValue("EKORG").ToString();
                    p.G_L_ACCT = ln.GetValue("G_L_ACCT").ToString();
                    p.COST_CTR = ln.GetValue("COST_CTR").ToString();
                    p.KONNR = ln.GetValue("KONNR").ToString();
                    p.KTPNR = ln.GetValue("KTPNR").ToString();
                    p.VRTKZ = ln.GetValue("VRTKZ").ToString();
                    p.SAKTO = ln.GetValue("SAKTO").ToString();
                    p.AUFNR = ln.GetValue("AUFNR").ToString();
                    p.KOKRS = ln.GetValue("KOKRS").ToString();
                    p.KOSTL = ln.GetValue("KOSTL").ToString();
                    p.TEXTO_CAB = ln.GetValue("TEXTO_CAB").ToString();
                    p.TEXTO_POS = ln.GetValue("TEXTO_POS").ToString();
                    p.RECIBIDO = ln.GetValue("RECIBIDO").ToString();
                    p.PROCESADO = ln.GetValue("PROCESADO").ToString();
                    p.ERROR = ln.GetValue("ERROR").ToString();
                    p.PREIS = ln.GetValue("PREIS").ToString();
                    p.WAERS = ln.GetValue("WAERS").ToString();
                    p.FRGGR = ln.GetValue("FRGGR").ToString();
                    p.FRGRL = ln.GetValue("FRGRL").ToString();
                    p.FRGKZ = ln.GetValue("FRGKZ").ToString();
                    p.FRGZU = ln.GetValue("FRGZU").ToString();
                    p.FRGST = ln.GetValue("FRGST").ToString();
                    p.FRGC1 = ln.GetValue("FRGC1").ToString();
                    p.FRGCT = ln.GetValue("FRGCT").ToString();
                    p.LOEKZ = ln.GetValue("LOEKZ").ToString();
                    p.PEDIDO = ln.GetValue("PEDIDO").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaPosicionesSolPed(connection, p);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure ln in serSol)
            {
                SolPed_Ser_Vis s = new SolPed_Ser_Vis();
                try
                {
                    s.FOLIO_SAM = ln.GetValue("FOLIO_SAM").ToString();
                    s.BANFN = ln.GetValue("BANFN").ToString();
                    s.BNFPO = ln.GetValue("BNFPO").ToString();
                    s.EXTROW = ln.GetValue("EXTROW").ToString();
                    s.SRVPOS = ln.GetValue("SRVPOS").ToString();
                    s.MENGE = ln.GetValue("MENGE").ToString();
                    s.MEINS = ln.GetValue("MEINS").ToString();
                    s.KTEXT1 = ln.GetValue("KTEXT1").ToString();
                    s.TBTWR = ln.GetValue("TBTWR").ToString();
                    s.WAERS = ln.GetValue("WAERS").ToString();
                    s.KOSTL = ln.GetValue("KOSTL").ToString();
                    s.NETWR = ln.GetValue("NETWR").ToString();

                    DALC_Sol_Ped.ObtenerInstancia().IngresaServiciosSolPed(connection, s);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in txtCab)
            {
                TextoCabeceraSolped tc = new TextoCabeceraSolped();
                try
                {
                    tc.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    tc.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    tc.INDICE = linea.GetValue("INDICE").ToString();
                    tc.TDFORMAT = linea.GetValue("TDFORMAT").ToString();
                    tc.TDLINE = linea.GetValue("TDLINE").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().IngresarTextoCSV(connection, tc);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in txtPos)
            {
                TextoPosicionesSolped tp = new TextoPosicionesSolped();
                try
                {
                    tp.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    tp.FOLIO_SAP = linea.GetValue("FOLIO_SAP").ToString();
                    tp.PREQ_ITEM = linea.GetValue("PREQ_ITEM").ToString();
                    tp.INDICE = linea.GetValue("INDICE").ToString();
                    tp.TDFORMAT = linea.GetValue("TDFORMAT").ToString();
                    tp.TDLINE = linea.GetValue("TDLINE").ToString();
                    DALC_Sol_Ped.ObtenerInstancia().IngresarTextoPSV(connection, tp);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZDAT_SOLPED_SAM", solped);
            return true;
        }
        public bool ZAVISO_SAM(string aviso)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZAVISO_SAM");
            FUNCTION.SetValue("QMNUM", aviso);
            IRfcTable ta1 = FUNCTION.GetTable("NOTIFICA");
            IRfcTable ta2 = FUNCTION.GetTable("ACTIVIDADES");
            IRfcTable ta3 = FUNCTION.GetTable("TEXTOS");
            IRfcTable ta4 = FUNCTION.GetTable("DMS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                Avisos av = new Avisos();
                try
                {
                    av.QMNUM = linea.GetValue("QMNUM").ToString();
                    av.QMART = linea.GetValue("QMART").ToString();
                    av.QMTXT = linea.GetValue("QMTXT").ToString();
                    av.TXT04_ES = linea.GetValue("TXT04_ES").ToString();
                    av.TXT04_EN = linea.GetValue("TXT04_EN").ToString();
                    av.TXT30_ES = linea.GetValue("TXT30_ES").ToString();
                    av.TXT30_EN = linea.GetValue("TXT30_EN").ToString();
                    av.TPLNR = linea.GetValue("TPLNR").ToString();
                    av.PLTXT = linea.GetValue("PLTXT").ToString();
                    av.EQUNR = linea.GetValue("EQUNR").ToString();
                    av.EQKTX = linea.GetValue("EQKTX").ToString();
                    av.BAUTL = linea.GetValue("BAUTL").ToString();
                    av.INGRP = linea.GetValue("INGRP").ToString();
                    av.IWERK = linea.GetValue("IWERK").ToString();
                    av.INNAM = linea.GetValue("INNAM").ToString();
                    av.ARBPL = linea.GetValue("ARBPL").ToString();
                    av.SWERK = linea.GetValue("SWERK").ToString();
                    av.KTEXT = linea.GetValue("KTEXT").ToString();
                    av.I_PARNR = linea.GetValue("I_PARNR").ToString();
                    av.NAME_LIST = linea.GetValue("NAME_LIST").ToString();
                    av.PARNR_VERA = linea.GetValue("PARNR_VERA").ToString();
                    av.NAME_VERA = linea.GetValue("NAME_VERA").ToString();
                    av.QMNAM = linea.GetValue("QMNAM").ToString();
                    av.QMDAT = linea.GetValue("QMDAT").ToString();
                    av.MZEIT = linea.GetValue("MZEIT").ToString();
                    av.QMGRP = linea.GetValue("QMGRP").ToString();
                    av.HEADKTXT = linea.GetValue("HEADKTXT").ToString();
                    av.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    av.LVORM = linea.GetValue("LVORM").ToString();
                    av.AUFNR = linea.GetValue("ORDEN").ToString();
                    DALC_Avisos.obtenerInstancia().IngresarAvisos(connection, av);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Actividad ac = new Actividad();
                try
                {
                    ac.QMNUM = linea.GetValue("QMNUM").ToString();
                    ac.FENUM = linea.GetValue("FENUM").ToString();
                    ac.MNGRP = linea.GetValue("MNGRP").ToString();
                    ac.MNCOD = linea.GetValue("MNCOD").ToString();
                    ac.KURZTEXT_ES = linea.GetValue("KURZTEXT_ES").ToString();
                    ac.KURZTEXT_EN = linea.GetValue("KURZTEXT_EN").ToString();
                    ac.MATXT = linea.GetValue("MATXT").ToString();
                    ac.MNGFA = linea.GetValue("MNGFA").ToString();
                    ac.PSTER = linea.GetValue("PSTER").ToString();
                    ac.PSTUR = linea.GetValue("PSTUR").ToString();
                    ac.PETER = linea.GetValue("PETER").ToString();
                    ac.PETUR = linea.GetValue("PETUR").ToString();
                    ac.MANUM = linea.GetValue("MANUM").ToString();
                    ac.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    DALC_Avisos.obtenerInstancia().IngresarActividad(connection, ac);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta3)
            {
                Textos tx = new Textos();
                try
                {
                    tx.QMNUM = linea.GetValue("QMNUM").ToString();
                    tx.TEXTO = linea.GetValue("TEXTO").ToString();
                    tx.FOIO_SAM = linea.GetValue("FOIO_SAM").ToString();
                    DALC_Avisos.obtenerInstancia().IngresarTextos(connection, tx);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta4)
            {
                DMS dm = new DMS();
                try
                {
                    dm.QMNUM = linea.GetValue("QMNUM").ToString();
                    dm.DOKAR = linea.GetValue("DOKAR").ToString();
                    dm.DOKNR = linea.GetValue("DOKNR").ToString();
                    dm.LATEST_REL = linea.GetValue("LATEST_REL").ToString();
                    dm.LATEST_VERSION = linea.GetValue("LATEST_VERSION").ToString();
                    dm.ICON_STATUS = linea.GetValue("ICON_STATUS").ToString();
                    dm.DOKTL = linea.GetValue("DOKTL").ToString();
                    dm.DOKVR = linea.GetValue("DOKVR").ToString();
                    dm.DKTXT = linea.GetValue("DKTXT").ToString();
                    DALC_Avisos.obtenerInstancia().IngresarDMS(connection, dm);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZPM_NOTIFICA", aviso);
            return true;
        }
        public bool ZRESERVAS_SAM(string reserva)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZRESERVAS_SAM");
            FUNCTION.SetValue("RSNUM", reserva);
            IRfcTable tabResb = FUNCTION.GetTable("IT_RESB");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in tabResb)
            {
                ReservasMateriales rem = new ReservasMateriales();
                try
                {
                    rem.RSNUM = linea.GetValue("RSNUM").ToString();
                    rem.RSPOS = linea.GetValue("RSPOS").ToString();
                    rem.XLOEK = linea.GetValue("XLOEK").ToString();
                    rem.XWAOK = linea.GetValue("XWAOK").ToString();
                    rem.KZEAR = linea.GetValue("KZEAR").ToString();
                    rem.MATNR = linea.GetValue("MATNR").ToString();
                    rem.WERKS = linea.GetValue("WERKS").ToString();
                    rem.LGORT = linea.GetValue("LGORT").ToString();
                    rem.CHARG = linea.GetValue("CHARG").ToString();
                    rem.SOBKZ = linea.GetValue("SOBKZ").ToString();
                    rem.BDTER = linea.GetValue("BDTER").ToString();
                    rem.BDMNG = linea.GetValue("BDMNG").ToString();
                    rem.MEINS = linea.GetValue("MEINS").ToString();
                    rem.SHKZG = linea.GetValue("SHKZG").ToString();
                    rem.FMENG = linea.GetValue("FMENG").ToString();
                    rem.ENMNG = linea.GetValue("ENMNG").ToString();
                    rem.ENWRT = linea.GetValue("ENWRT").ToString();
                    rem.WAERS = linea.GetValue("WAERS").ToString();
                    rem.ERFMG = linea.GetValue("ERFMG").ToString();
                    rem.ERFME = linea.GetValue("ERFME").ToString();
                    rem.KOSTL = linea.GetValue("KOSTL").ToString();
                    rem.AUFNR = linea.GetValue("AUFNR").ToString();
                    rem.BWART = linea.GetValue("BWART").ToString();
                    rem.SAKNR = linea.GetValue("SAKNR").ToString();
                    rem.UMWRK = linea.GetValue("UMWRK").ToString();
                    rem.UMLGO = linea.GetValue("UMLGO").ToString();
                    rem.SGTXT = linea.GetValue("SGTXT").ToString();
                    rem.LMENG = linea.GetValue("LMENG").ToString();
                    rem.STLTY = linea.GetValue("STLTY").ToString();
                    rem.STLNR = linea.GetValue("STLNR").ToString();
                    rem.POTX1 = linea.GetValue("POTX1").ToString();
                    rem.POTX2 = linea.GetValue("POTX2").ToString();
                    rem.UMREZ = linea.GetValue("UMREZ").ToString();
                    rem.UMREN = linea.GetValue("UMREN").ToString();
                    rem.AUFPL = linea.GetValue("AUFPL").ToString();
                    rem.PLNFL = linea.GetValue("PLNFL").ToString();
                    rem.VORNR = linea.GetValue("VORNR").ToString();
                    rem.PEINH = linea.GetValue("PEINH").ToString();
                    rem.AFPOS = linea.GetValue("AFPOS").ToString();
                    rem.MATKL = linea.GetValue("MATKL").ToString();
                    rem.LIFNR = linea.GetValue("LIFNR").ToString();
                    rem.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    rem.LVORM = linea.GetValue("LVORM").ToString();
                    rem.UNAME = linea.GetValue("UNAME").ToString();
                    DALC_ReservasMateriales.ObtenerInstancia().IngresaReservasMateriales(connection, rem);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZMM_RESERVAS_MATERIALES", reserva);
            return true;
        }
        public bool ZHIS_PEDIDOS_MAT(string documento)
        {
            RfcRepository repo = rfcDesti.Repository;
            IRfcFunction FUNCTION = repo.CreateFunction("ZHIS_PEDIDOS_MAT");
            FUNCTION.SetValue("DOCUMENTO", documento);
            IRfcTable ta1 = FUNCTION.GetTable("PROV_PEDIDOS");
            IRfcTable ta2 = FUNCTION.GetTable("PROV_PEDIDOS_II");
            IRfcTable ta3 = FUNCTION.GetTable("SERVICIOS");
            try
            {
                FUNCTION.Invoke(rfcDesti);
            }
            catch (Exception) { return false; }
            foreach (IRfcStructure linea in ta1)
            {
                Pedidos_I pedd = new Pedidos_I();
                try
                {
                    pedd.EBELN = linea.GetValue("EBELN").ToString();
                    DALC_Pedidos.ObtenerInstancia().BorrarPedido_I(connection, pedd);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta1)
            {
                Pedidos_I pd = new Pedidos_I();
                try
                {
                    pd.EBELN = linea.GetValue("EBELN").ToString();
                    pd.EBELP = linea.GetValue("EBELP").ToString();
                    pd.MATNR = linea.GetValue("MATNR").ToString();
                    pd.TXZ01 = linea.GetValue("TXZ01").ToString();
                    pd.MENGE = linea.GetValue("MENGE").ToString();
                    pd.MEINS = linea.GetValue("MEINS").ToString();
                    pd.BEWTP = linea.GetValue("BEWTP").ToString();
                    pd.BWART = linea.GetValue("BWART").ToString();
                    pd.MBLNR = linea.GetValue("MBLNR").ToString();
                    pd.FOLIO_SAM = linea.GetValue("FOLIO_SAM").ToString();
                    pd.BUZEI = linea.GetValue("BUZEI").ToString();
                    pd.CANTIADE = linea.GetValue("CANTIADE").ToString();
                    pd.UM_PED = linea.GetValue("UM_PED").ToString();
                    pd.EINDT = linea.GetValue("EINDT").ToString();
                    pd.BUDAT = linea.GetValue("BUDAT").ToString();

                    DALC_Pedidos.ObtenerInstancia().IngresaPedido_I(connection, pd);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Pedidos_II ped2 = new Pedidos_II();
                try
                {
                    ped2.EBELN = linea.GetValue("EBELN").ToString();
                    ped2.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_Pedidos.ObtenerInstancia().BorrarPedido_II(connection, ped2);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta2)
            {
                Pedidos_II pd = new Pedidos_II();
                try
                {
                    pd.BSTYP = linea.GetValue("BSTYP").ToString();
                    pd.EBELN = linea.GetValue("EBELN").ToString();
                    pd.LIFNR = linea.GetValue("LIFNR").ToString();
                    pd.NAME1 = linea.GetValue("NAME1").ToString();
                    pd.BEDAT = linea.GetValue("BEDAT").ToString();
                    pd.G_NAME = linea.GetValue("G_NAME").ToString();
                    pd.EKORG = linea.GetValue("EKORG").ToString();
                    pd.EKOTX = linea.GetValue("EKOTX").ToString();
                    pd.EKGRP = linea.GetValue("EKGRP").ToString();
                    pd.EKNAM = linea.GetValue("EKNAM").ToString();
                    pd.BUKRS = linea.GetValue("BUKRS").ToString();
                    pd.BUTXT = linea.GetValue("BUTXT").ToString();
                    pd.EBELP = linea.GetValue("EBELP").ToString();
                    pd.KNTTP = linea.GetValue("KNTTP").ToString();
                    pd.PSTYP = linea.GetValue("PSTYP").ToString();
                    pd.MATNR = linea.GetValue("MATNR").ToString();
                    pd.TXZ01 = linea.GetValue("TXZ01").ToString();
                    pd.MENGE = linea.GetValue("MENGE").ToString();
                    pd.MEINS = linea.GetValue("MEINS").ToString();
                    pd.EINDT = linea.GetValue("EINDT").ToString();
                    pd.WERKS = linea.GetValue("WERKS").ToString();
                    pd.LGORT = linea.GetValue("LGORT").ToString();
                    pd.CHARG = linea.GetValue("CHARG").ToString();
                    pd.AFNAM = linea.GetValue("AFNAM").ToString();
                    pd.INFNR = linea.GetValue("INFNR").ToString();
                    pd.BANFN = linea.GetValue("BANFN").ToString();
                    pd.BNFPO = linea.GetValue("BNFPO").ToString();
                    pd.KONNR = linea.GetValue("KONNR").ToString();
                    pd.MATKL = linea.GetValue("MATKL").ToString();
                    pd.IDNLF = linea.GetValue("IDNLF").ToString();
                    pd.EAN11 = linea.GetValue("EAN11").ToString();
                    pd.LICHA = linea.GetValue("LICHA").ToString();
                    pd.BSART = linea.GetValue("BSART").ToString();
                    pd.BATXT = linea.GetValue("BATXT").ToString();
                    pd.G_POS = linea.GetValue("G_POS").ToString();
                    pd.PACKNO = linea.GetValue("PACKNO").ToString();
                    pd.SUB_PACKNO = linea.GetValue("SUB_PACKNO").ToString();
                    pd.BWART = linea.GetValue("BWART").ToString();
                    pd.CANTIDADE = linea.GetValue("CANTIDADE").ToString();
                    pd.KOSTL = linea.GetValue("KOSTL").ToString();
                    pd.AUFNR = linea.GetValue("AUFNR").ToString();
                    pd.SAKTO = linea.GetValue("SAKTO").ToString();
                    pd.FRGGR = linea.GetValue("FRGGR").ToString();
                    pd.FRGSX = linea.GetValue("FRGSX").ToString();
                    pd.FRGKE = linea.GetValue("FRGKE").ToString();
                    pd.FRGZU = linea.GetValue("FRGZU").ToString();
                    pd.BEDNR = linea.GetValue("BEDNR").ToString();
                    pd.FRGC1 = linea.GetValue("FRGC1").ToString();
                    pd.FRGCT = linea.GetValue("FRGCT").ToString();
                    pd.LVORM = linea.GetValue("LOEKZ").ToString();

                    DALC_Pedidos.ObtenerInstancia().IngresaPedido_II(connection, pd);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta3)
            {
                Servicio ped3 = new Servicio();
                try
                {
                    ped3.EBELN = linea.GetValue("EBELN").ToString();
                    ped3.WERKS = linea.GetValue("WERKS").ToString();
                    DALC_Pedidos.ObtenerInstancia().BorrarServicio(connection, ped3);
                }
                catch (Exception) { return false; }
            }
            foreach (IRfcStructure linea in ta3)
            {
                Servicio pd = new Servicio();
                try
                {
                    pd.EBELN = linea.GetValue("EBELN").ToString();
                    pd.EBELP = linea.GetValue("EBELP").ToString();
                    pd.EXTROW = linea.GetValue("EXTROW").ToString();
                    pd.SRVPOS = linea.GetValue("SRVPOS").ToString();
                    pd.MENGE = linea.GetValue("MENGE").ToString();
                    pd.MEINS = linea.GetValue("MEINS").ToString();
                    pd.KTEXT1 = linea.GetValue("KTEXT1").ToString();
                    pd.TBTWR = linea.GetValue("TBTWR").ToString();
                    pd.WAERS = linea.GetValue("WAERS").ToString();
                    pd.NETWR = linea.GetValue("NETWR").ToString();
                    pd.ACT_MENGE = linea.GetValue("ACT_MENGE").ToString();
                    pd.AUFNR = linea.GetValue("AUFNR").ToString();
                    pd.WERKS = linea.GetValue("WERKS").ToString();
                    pd.DATUM = linea.GetValue("DATUM").ToString();

                    DALC_Pedidos.ObtenerInstancia().IngresaServicio(connection, pd);
                }
                catch (Exception) { return false; }
            }
            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZHIS_PEDIDOS_MAT", documento);
            return true;
        }
        public bool ZINV_SAM_RESERVAS()
        {
            string mensaje = "Correcto";
            List<VALIDA_MOVI_ERROR_MDL_Result> v = ACC_Inventarios.ObtenerInstancia().ValidarErrores(connection).ToList();
            if (v.Count() <= 0)
            {
                RfcRepository sin = rfcDesti.Repository;
                IRfcFunction FUNCTION = sin.CreateFunction("ZINV_SAM_RESERVAS");
                IRfcTable INVENTARIOS = FUNCTION.GetTable("INVENTARIO_SAM");
                IRfcTable INVENTARIOE = FUNCTION.GetTable("INVENTARIO_EE");
                try
                {
                    FUNCTION.Invoke(rfcDesti);
                }
                catch (Exception)
                {
                    mensaje = "Error";
                    return false;
                }
                ACC_Inventarios.ObtenerInstancia().TRuncateIventarioReservas(connection);
                foreach (IRfcStructure linea in INVENTARIOS)
                {
                    Inventarios inv = new Inventarios();
                    try
                    {
                        inv.MATNR = linea.GetValue("MATNR").ToString();
                        inv.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                        inv.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                        inv.WERKS = linea.GetValue("WERKS").ToString();
                        inv.MEINS = linea.GetValue("MEINS").ToString();
                        inv.LGORT = linea.GetValue("LGORT").ToString();
                        inv.LGOBE = linea.GetValue("LGOBE").ToString();
                        inv.CLABS = linea.GetValue("CLABS").ToString();
                        inv.CINSM = linea.GetValue("CINSM").ToString();
                        inv.CSPEM = linea.GetValue("CSPEM").ToString();
                        inv.CUMLM = linea.GetValue("CUMLM").ToString();
                        inv.CHARG = linea.GetValue("CHARG").ToString();
                        inv.MTART = linea.GetValue("MTART").ToString();
                        inv.MATKL = linea.GetValue("MATKL").ToString();
                        inv.SERNR = linea.GetValue("SERNR").ToString();
                        inv.XCHPF = linea.GetValue("XCHPF").ToString();
                        inv.ult_stock_libre = linea.GetValue("CLABS").ToString();
                        inv.cnt_cent_dest = "0";
                        inv.ult_cnt_dest = "0";
                        inv.ult_trans = linea.GetValue("CUMLM").ToString();
                        ACC_Inventarios.ObtenerInstancia().InsertarInvRuta_Reservas(connection, inv);
                    }
                    catch (Exception)
                    {
                        mensaje = "Error";
                        return false;
                    }
                }
                foreach (IRfcStructure linea in INVENTARIOE)
                {
                    Inventarios inv = new Inventarios();
                    try
                    {
                        inv.MATNR = linea.GetValue("MATNR").ToString();
                        inv.MAKTX_ES = linea.GetValue("MAKTX_ES").ToString();
                        inv.MAKTX_EN = linea.GetValue("MAKTX_EN").ToString();
                        inv.WERKS = linea.GetValue("WERKS").ToString();
                        inv.MEINS = linea.GetValue("MEINS").ToString();
                        inv.LGORT = linea.GetValue("LGORT").ToString();
                        inv.LGOBE = linea.GetValue("LGOBE").ToString();
                        inv.CLABS = linea.GetValue("CLABS").ToString();
                        inv.CINSM = linea.GetValue("CINSM").ToString();
                        inv.CSPEM = linea.GetValue("CSPEM").ToString();
                        inv.CUMLM = linea.GetValue("CUMLM").ToString();
                        inv.CHARG = linea.GetValue("CHARG").ToString();
                        inv.MTART = linea.GetValue("MTART").ToString();
                        inv.MATKL = linea.GetValue("MATKL").ToString();
                        inv.SERNR = linea.GetValue("SERNR").ToString();
                        inv.XCHPF = linea.GetValue("XCHPF").ToString();
                        inv.SOBKZ = linea.GetValue("SOBKZ").ToString();
                        inv.VBELN = linea.GetValue("VBELN").ToString();
                        inv.POSNR = linea.GetValue("POSNR").ToString();
                        ACC_Inventarios.ObtenerInstancia().InsertarInvEE_Rservas(connection, inv);
                    }
                    catch (Exception)
                    {
                        mensaje = "Error";
                        return false;
                    }
                }
                DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZINV_SAM_RESERVAS", mensaje);
                DALC_sincronizacion.ObtenerInstancia().IndicadorConsumoRFC(connection, "ZINV_SAM_RESERVAS", "X", "1000");
            }
            return true;
        }
    }
}
