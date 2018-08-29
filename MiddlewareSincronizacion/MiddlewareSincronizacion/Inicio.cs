using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MiddlewareSincronizacion.Clases;
using MiddlewareSincronizacion.AccesoDatos;
using MiddlewareSincronizacion.Entidades;
using System.Net.NetworkInformation;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion
{
    public partial class Inicio : Form
    {
        Thread Hilo1, Hilo2, Hilo3, Hilo4, Hilo5, Hilo6, Hilo7, Hilo8, Hilo9, Hilo10, Hilo11;
        private bool conexionValida = false;
        private ConfigSAP configSAP;
        private frmConfiguracion frmConfig;
        private String rutaFacturacion;
        private bool cierreForzado = false;
        private frmLocalHost frmLocal;
        private Principal ppal;
        private int timer = 0, limiteTimer = 5;
        private bool NetworkAccess = false;
        bool accion = false;
        bool inicia = false;
        private EntityConnectionStringBuilder connection;
        public Inicio()
        {
            rutaFacturacion = String.Empty;
            frmConfig = new frmConfiguracion();
            frmLocal = new frmLocalHost();
            Hilo4 = new Thread(new ThreadStart(ping_SAP));
            InitializeComponent();
            btnIniciar.Enabled = true;
            btnParar.Enabled = false;
            Thread.Sleep(2000);
            iniciarPrograma();
        }
        private void ping_SAP()
        {
            while (true)
            {
                pingNetwork();
                Thread.Sleep(10000);
            }
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            iniciarPrograma();
        }
        private void iniciarPrograma()
        {
            bool ck = true;
            while (ck)
            {
                if (frmConfig.probarCon2())
                {
                    if (frmLocal.TestCon())
                    {
                        connection = frmLocal.Connection();
                        //tmrSincroniza.Enabled = true;
                        //tmrSincroniza.Start();
                        btnIniciar.Enabled = false;
                        btnParar.Enabled = true;
                        configSAP = frmConfig.ObtenerDatosConfig();
                        ppal = new Principal(configSAP, connection);
                        DALC_sincronizacion.ObtenerInstancia().EventoMeddleware(connection, "ENCENDIDO");
                        try
                        {
                            NetworkAccess = true;
                            iniciarConsulta();
                            Hilo4.Start();
                            ck = false;
                        }
                        catch (Exception) { ck = true; }
                    }
                    else
                    {
                        ck = true;
                    }
                }
                else
                {
                    ck = true;
                }
            }
        }
        private void iniciarConsulta()
        {
            CheckForIllegalCrossThreadCalls = false;
            Hilo2 = new Thread(new ThreadStart(IniciarProcesosOuput));
            Hilo2.Start();
            //Hilo3 = new Thread(new ThreadStart(IniciarEnvios));
            //Hilo3.Start();
            //Hilo9 = new Thread(new ThreadStart(IniciarProcesoOperacion));
            //Hilo9.Start();
            //Hilo11 = new Thread(new ThreadStart(IniciarProcesoTransferencia));
            //Hilo11.Start();
            Hilo5 = new Thread(new ThreadStart(VerificarRed));
            Hilo5.Start();
            //Hilo7 = new Thread(new ThreadStart(IniciarConsultaTiempo));
            //Hilo7.Start();
        }
        private void btnParar_Click(object sender, EventArgs e)
        {
            //btnIniciar.Enabled = true;
            //btnParar.Enabled = false;
            //timer = 0;
            //Hilo1.Abort();
            //Hilo1.Join();
            //Hilo2.Abort();
            //Hilo2.Join();
            //Hilo3.Abort();
            //Hilo3.Join();
            //Hilo9.Abort();
            //Hilo9.Join();
            //Hilo4.Abort();
            //Hilo4.Join();
            //Hilo7.Abort();
            //Hilo7.Join();
            //Hilo11.Abort();
            //Hilo11.Join();
            //tmrSincroniza.Stop();
            //DALC_sincronizacion.ObtenerInstancia().EventoMeddleware(connection, "APAGADO");
        }
        private void VerificarRed()
        {
            while (true)
            {
                if (!NetworkAccess)
                {
                    try
                    {
                        btnIniciar.Enabled = true;
                        btnParar.Enabled = false;
                        Hilo1.Abort();
                        Hilo1.Join();
                        Hilo2.Abort();
                        Hilo2.Join();
                        Hilo3.Abort();
                        Hilo3.Join();
                        //Hilo9.Abort();
                        //Hilo9.Join();
                        Hilo7.Abort();
                        Hilo7.Join();
                        Hilo11.Abort();
                        Hilo11.Join();
                        DALC_sincronizacion.ObtenerInstancia().EventoMeddleware(connection, "APAGADO");
                        Hilo6 = new Thread(new ThreadStart(VerificarRedHilo6));
                        Hilo6.Start();
                        //Hilo8 = new Thread(new ThreadStart(Reiniciar));
                        //Hilo8.Start();
                        //Hilo8.Abort();
                        //Hilo8.Join();
                        Hilo5.Abort();
                        Hilo5.Join();
                        
                    }
                    catch (Exception) { }
                }
                Thread.Sleep(2000);
            }
        }
        private void VerificarRedHilo6()
        {
            while (true)
            {
                if (NetworkAccess)
                {
                    btnIniciar.Enabled = false;
                    Hilo4.Abort();
                    Hilo4.Join();
                    Hilo4 = new Thread(new ThreadStart(ping_SAP));
                    iniciarPrograma();
                    return;
                }
            }
        }
        private void configuraciónServidorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirConfigServidorSAP();
        }
        private void abrirConfigServidorSAP()
        {
            frmConfig.ShowDialog();
            conexionValida = frmConfig.conexionValida;
            if (conexionValida)
            {
                configSAP = frmConfig.configSAP;
            }
        }
        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cierreForzado)
            {
                e.Cancel = true;
                mynotifyicon.Visible = true;
                mynotifyicon.ShowBalloonTip(500);
                this.Hide();
            }
        }
        private void configuracíonRutaDeFacturaciónToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            configuracíonRutaDeFacturaciónToolStripMenuItem.ToolTipText = rutaFacturacion;
        }
        private void Inicio_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                mynotifyicon.Visible = true;
                mynotifyicon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                mynotifyicon.Visible = false;

            }
        }
        private void mynotifyicon_MouseClick(object sender, MouseEventArgs e)
        {
            MostrarAplicacion();
        }
        private void MostrarAplicacion()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void mynotifyicon_BalloonTipClicked(object sender, EventArgs e)
        {
            MostrarAplicacion();
        }
        private void Inicio_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Hilo1 != null && Hilo1.IsAlive)
            {
                Hilo1.Abort();
                Hilo1.Join();
            }
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Esta cerrando el sistema. Esto causara se detenga" +
                            "el servicio.\n¿Desea continuar?", "Precaución",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                cierreForzado = true;
                Application.Exit();
                Close();
            }
        }
        private void confiurarLocalHostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocal.ShowDialog();
        }
        private void IniciarConsultaTiempo()
        {
            while (true)
            {
                List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
                try
                {
                    DALC_sincronizacion.ObtenerInstancia().VaciarTablaTiempos(connection);
                    foreach (centro_MDL_Result c in cc)
                    {
                        if (ppal.P_ZTIEMPO_RFC(c.centro))
                        {
                            inicia = true;
                            List<SELECT_tiempos_O_MDL_Result> tt = DALC_StoredProcedures.ObtenerInstancia().ObtenerTiempoO(connection).ToList();
                            foreach (SELECT_tiempos_O_MDL_Result t in tt)
                            {
                                int tims = 0;
                                int horas = 0;
                                string per = t.periodo;
                                if (per == "24:00:00")
                                {
                                    tims = 86400000;
                                }
                                else
                                {
                                    DateTime ts = Convert.ToDateTime(per);
                                    horas = Convert.ToInt32(ts.Hour);
                                    tims = horas * 3600000;
                                }
                                Hilo1 = new Thread(new ThreadStart(IniciarProcesoContinuo));
                                Hilo1.Start();
                                Hilo2 = new Thread(new ThreadStart(IniciarProcesosOuput));
                                Hilo2.Start();
                                Hilo3 = new Thread(new ThreadStart(IniciarEnvios));
                                Hilo3.Start();
                                //Hilo9 = new Thread(new ThreadStart(IniciarProcesoOperacion));
                                //Hilo9.Start();
                                Hilo11 = new Thread(new ThreadStart(IniciarProcesoTransferencia));
                                Hilo11.Start();
                                Thread.Sleep(tims);
                                Hilo1.Abort();
                                Hilo1.Join();
                                Hilo2.Abort();
                                Hilo2.Join();
                                Hilo3.Abort();
                                Hilo3.Join();
                                //Hilo9.Abort();
                                //Hilo9.Join();
                                Hilo11.Abort();
                                Hilo11.Join();
                                inicia = false;
                                accion = false;
                            }
                        }
                        else
                        {
                            inicia = false;
                            DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZTIEMPO_RFC", "Error");
                        }
                    }
                }
                catch (Exception) { }
            }
        }
        private void IniciarProcesoContinuo()
        {
            List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
            while (true)
            {
                try
                {
                    if (inicia)
                    {
                        DALC_sincronizacion.ObtenerInstancia().vaciaSicronizacion(connection);
                        ppal.P_ZSAM_RFC_SINCRONIZAR();
                        //foreach (centro_MDL_Result c in cc)
                        //{
                        //    if (ppal.P_ZSAM_RFC_SINCRONIZAR(c.centro))
                        //    {
                        //        //obtener rfc de ultimo registro
                        //        //actualizar estatus de rfc anteriores a la ultima encontrada
                        //    }
                        //    else
                        //    {
                        //        accion = false;
                        //        DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, "ZSAM_RFC_SINCRONIZAR", "Error");
                        //    }
                        //}
                        accion = true;
                        Thread.Sleep(360000000);
                    }
                }
                catch (Exception) { }
            }
        }
        private void IniciarEnvios()
        {
            while (true)
            {
                try
                {
                    if (accion)
                    {
                        //Thread.Sleep(20000);
                        List<sincronizacion_input_MDL_Result> sincro = DALC_Rfcs.obtenerInstancia().ObtenerSincronizacionInput(connection).ToList();
                        foreach (sincronizacion_input_MDL_Result s in sincro)
                        {
                            switch (s.rfc)
                            {
                                case "ZGUARDACONT_DOC_INV_CREA":
                                    ppal.ZGUARDACONT_DOC_INV_CREA();
                                    break;
                                case "ZSD_CREA_PEDIDO":
                                    ppal.ZCREA_PEDIDO_SD();
                                    break;
                                case "ZPM_CONTADOR_SAM_VS_SAP_BD":
                                    ppal.P_ZPM_CONTADOR_SAM_VS_SAP_BD();
                                    break;
                                case "ZPM_NOTIFICACION_SAM_VS_SAP_BD":
                                    ppal.P_ZPM_NOTIFICACION_SAM_VS_SAP_BD();
                                    break;
                                case "ZGUARDA_AVISOS":
                                    ppal.P_ZGUARDA_AVISOS();
                                    break;
                                case "ZGUARDA_SOLPED_SAM":
                                    ppal.P_ZGUARDA_SOLPED_SAM();
                                    break;
                                case "ZPM_CONSUMOS_SAM_VS_SAP_BD":
                                    ppal.P_ZPM_CONSUMOS_SAM_VS_SAP_BD();
                                    break;
                                case "ZMM_ENTRADAS_SAM_VS_SAP_BD":
                                    ppal.P_ZMM_ENTRADAS_SAM_VS_SAP_BD();
                                    break;
                                case "ZMM_RESERVAS_SAM_VS_SAP_BD":
                                    ppal.P_ZMM_RESERVAS_SAM_VS_SAP_BD();
                                    break;
                                case "ZPM_ORDENESPM_SAM_VS_SAP_BD":
                                    ppal.P_ZPM_ORDENESPM_SAM_VS_SAP_BD();
                                    break;
                                case "ZPM_STATUSORDEN_SAM_VS_SAP_BD":
                                    ppal.P_ZPM_STATUSORDEN_SAM_VS_SAP_BD();
                                    break;
                                case "ZPM_CIERRA_AVISOS_SAP_VS_SAM":
                                    ppal.P_ZPM_CIERRA_AVISOS_SAP_VS_SAM();
                                    break;
                                case "ZMM_LOTE_SAM_VS_SAP_BD":
                                    ppal.P_ZMM_LOTE_SAM_VS_SAP_BD();
                                    break;
                                case "ZQM_LOTEZIW41_SAM_VS_SAP_BD":
                                    ppal.P_ZQM_LOTEZIW41_SAM_VS_SAP_BD();
                                    break;
                                case "ZMM_MASIVO_DEFECTOS101":
                                    ppal.P_ZMM_MASIVO_DEFECTOS101();
                                    break;
                                case "ZSAM_GUARDA_SOLPED_CANCEL":
                                    ppal.P_ZSAM_GUARDA_SOLPED_CANCEL();
                                    break;
                                case "ZQM_DECISION_MASIVO":
                                    ppal.P_ZQM_DECISION_MASIVO();
                                    break;
                                case "ZGUARDA_ACTUALIZA":
                                    ppal.P_ZGUARDA_ACTUALIZA();
                                    break;
                                case "ZGUARDA_ACT_AVISOS":
                                    ppal.P_ZGUARDA_ACT_AVISOS();
                                    break;
                                case "ZMOV_SAM":
                                    ppal.ZMOV_SAM();
                                    break;
                                case "ZNOTIFICACIONES_ORDEN":
                                    ppal.ZNOTIFICACIONES_ORDEN();
                                    break;
                                case "ZPROCESA_ESTATUS":
                                    ppal.ZPROCESA_ESTATUS();
                                    break;
                                case "ZMM_MOVMAT_SAM_VS_SAP_BD":
                                    if (ppal.ValidacionMov())
                                    {
                                        if (ppal.P_ZMM_MOVMAT_SAM_VS_SAP_BD())
                                        {
                                            bool val = true;
                                            Reiniciar(val);
                                            //Hilo8 = new Thread(new ThreadStart(Reiniciar));
                                            //Hilo8.Start();
                                            //Hilo3.Abort();
                                            //Hilo3.Join();
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
        }
        private void Reiniciar(bool ac)
        {
            while (ac)
            {
                if (ppal.P_ZEXTRAE_ESTATUS())
                {
                    ppal.P_ZBORRA_ESTATUS();
                    if (ppal.VerificarErrores())
                    {
                        InicarProcesoInventarios();
                    }
                    //Hilo3 = new Thread(new ThreadStart(IniciarEnvios));
                    //Hilo3.Start();
                    ac = false;
                    //Hilo8.Abort();
                    //Hilo8.Join();
                }
                else { }
            }
        }
        private void InicarProcesoInventarios()
        {
            try
            {
                List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
                foreach (centro_MDL_Result ce in cc)
                {
                    List<SELECT_sincronizacion_inventarios_MDL_Result> im = DALC_sincronizacion.ObtenerInstancia().ObtenerRfc(connection).ToList();
                    foreach (SELECT_sincronizacion_inventarios_MDL_Result iv in im)
                    {
                        switch (iv.rfc)
                        {
                            case "ZINV_SAM":
                                ppal.P_ZINV_SAM(ce.centro);
                                break;
                            //case "ZMM_STOCK_TRANSFERENCIA":
                            //    ppal.P_ZMM_STOCK_TRANSFERENCIA(ce.centro);
                            //    break;
                        }
                    }
                }
            }
            catch (Exception exc) { }
        }
        private void IniciarProcesosOuput()
        {
            while (true)
            {
                try
                {
                    if (accion)
                    {
                        //Thread.Sleep(60000);
                        bool bandera = true;
                        string rfc = "";
                        List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
                        List<centros303_MDL_Result> c303 = DALC_StoredProcedures.ObtenerInstancia().obtenercentros303(connection).ToList();
                        //List<sincronizacion_output_MDL_Result> so = DALC_StoredProcedures.ObtenerInstancia().sOutput(connection).ToList();

                        foreach (centros303_MDL_Result c in c303)
                        {
                            //Obtener RFC ZMATERIAL_ALMACEN_303
                            List<sincronizacion_output_MDL_Result> so = DALC_StoredProcedures.ObtenerInstancia().sOutput(connection).ToList();
                            foreach (sincronizacion_output_MDL_Result ss in so)
                            {
                                if (bandera)
                                {
                                    switch (ss.rfc)
                                    {
                                        case "ZMATERIAL_CENTRO_303":
                                            if (!ppal.P_ZMATERIAL_CENTRO_303(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMATERIAL_CENTRO_303";
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, rfc, "Error");
                                    break;
                                }
                            }
                        }
                        foreach (centro_MDL_Result c in cc)
                        {
                            //OBTENER RFCS POR CENTRO
                            List<OBTENER_RFCO_CENTRO_MDL_Result> so = DALC_StoredProcedures.ObtenerInstancia().ObtOut(connection, c.centro).ToList();
                            foreach (OBTENER_RFCO_CENTRO_MDL_Result ss in so)
                            {
                                if (bandera)
                                {
                                   switch (ss.rfc)
                                    {
                                       case "ZINV_SAM_RESERVAS":
                                           if(!ppal.ZINV_SAM_RESERVAS())
                                           {
                                               bandera = false;
                                               rfc = "ZINV_SAM_RESERVAS";
                                           }
                                            break;
                                        case "ZIMPRESORAS":
                                            if (!ppal.ZIMPRESORAS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZIMPRESORAS";
                                            }
                                            break;
                                        case "ZCATALOGOS_PP":
                                            if (!ppal.ZCATALOGOS_PP(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_PP";
                                            }
                                            break;
                                        case "ZMOTIVOS_RECHAZO":
                                            if (!ppal.ZMOTIVOS_RECHAZO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMOTIVOS_RECHAZO";
                                            }
                                            break;
                                        case "ZINV_SAM":
                                            if (!ppal.ZINV_SAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZINV_SAM";
                                            }
                                            break;
                                        case "ZPP_SAM_BOMEQUI":
                                            if (!ppal.ZPP_SAM_BOMEQUI(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPP_SAM_BOMEQUI";
                                            }
                                            break;
                                       case "ZMM_MATERIAL_CENTRO":
                                            if (!ppal.ZMM_MATERIAL_CENTRO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_MATERIAL_CENTRO";
                                            }
                                            break;
                                        case "ZPP_HOJA_RUTA_SAM":
                                            if (!ppal.ZPP_HOJA_RUTA_SAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPP_HOJA_RUTA_SAM";
                                            }
                                            break;
                                       case "ZORDENES_PP":
                                            if (!ppal.ZORDENES_PP(c.centro))
                                           {
                                               bandera = false;
                                               rfc = "ZORDENES_PP";
                                           }
                                           break;
                                       case "ZMATERIALES_PP":
                                           if (!ppal.ZMATERIALES_PP(c.centro))
                                           {
                                               bandera = false;
                                               rfc = "ZMATERIALES_PP"; 
                                           }
                                           break;
                                       case "ZMATERIALES_PRECIOS":
                                           if(!ppal.ZMATERIALES_PRECIOS())
                                           {
                                               bandera = false;
                                               rfc = "ZMATERIALES_PRECIOS";
                                           }
                                           break;
                                       case "ZMM_DOC_INVENTARIOS":
                                           if(!ppal.ZMM_DOC_INVENTARIOS(c.centro))
                                           {
                                               bandera = false;
                                               rfc = "ZMM_DOC_INVENTARIOS";
                                           }
                                           break;
                                        case "ZSAM_TOLERANCIA":
                                            if (!ppal.P_ZSAM_TOLERANCIA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_TOLERANCIA";
                                            }
                                            break;
                                        case "ZMM_CLM":
                                            if (!ppal.P_ZMM_CLM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_CLM";
                                            }
                                            break;
                                        case "ZMM_OBYC":
                                            if (!ppal.P_ZMM_OBYC(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_OBYC";
                                            }
                                            break;
                                        case "ZMATERIAL_CENTRO":
                                            if (!ppal.P_ZMATERIAL_CENTRO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMATERIAL_CENTRO";
                                            }
                                            break;
                                        case "ZSAM_ORDENES_NOPM":
                                            if (!ppal.P_ZSAM_ORDENES_NOPM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "P_ZSAM_ORDENES_NOPM";
                                            }
                                            break;
                                        case "ZPM_VIS_NOTIFICACIONES":
                                            if (!ppal.P_ZPM_VIS_NOTIFICACIONES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPM_VIS_NOTIFICACIONES";
                                            }
                                            break;
                                        case "ZHOJA_RUTA_SAM":
                                            if (!ppal.P_ZHOJA_RUTA_SAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZHOJA_RUTA_SAM";
                                            }
                                            break;
                                        case "ZUBI_EQU":
                                            if (!ppal.P_ZUBI_EQU(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZUBI_EQU";
                                            }
                                            break;
                                        case "ZPM_NOTIFICA":
                                            if (!ppal.P_ZPM_NOTIFICA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPM_NOTIFICA";
                                            }
                                            break;
                                        case "ZUBI_TEC":
                                            if (!ppal.P_ZUBI_TEC(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZUBI_TEC";
                                            }
                                            break;
                                        case "ZORDENES_PM":
                                            if (!ppal.P_ZORDENES_PM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZORDENES_PM";
                                            }
                                            break;
                                        case "ZRELACION_EQUIPOS":
                                            if (!ppal.P_ZRELACION_EQUIPOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZRELACION_EQUIPOS";
                                            }
                                            break;
                                        case "ZMM_RESERVAS_MATERIALES":
                                            if (!ppal.P_ZMM_RESERVAS_MATERIALES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_RESERVAS_MATERIALES";
                                            }
                                            break;
                                        case "ZQM_PLAN_INSP_CENTRO":
                                            if (!ppal.P_ZQM_PLAN_INSP_CENTRO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZQM_PLAN_INSP_CENTRO";
                                            }
                                            break;
                                        case "ZSAM_LIST_MOV_VIS":
                                            if (!ppal.P_ZSAM_LIST_MOV_VIS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_LIST_MOV_VIS";
                                            }
                                            break;
                                        case "ZPROVEEDORES":
                                            if (!ppal.P_ZPROVEEDORES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPROVEEDORES";
                                            }
                                            break;
                                        //case "ZCLIENTES":
                                            //if (!ppal.P_ZCLIENTES(c.centro))
                                            //{
                                            //    bandera = false;
                                            //    rfc = "ZCLIENTES";
                                            //}
                                            //break;
                                        case "ZORDENES_PMSAM":
                                            if (!ppal.P_ZORDENES_PMSAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZORDENES_PMSAM";
                                            }
                                            break;
                                        case "ZPM_MATERIALES_ALMACEN":
                                            if (!ppal.P_ZPM_MATERIALES_ALMACEN(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPM_MATERIALES_ALMACEN";
                                            }
                                            break;
                                        case "ZDAT_SOLPED_SAM":
                                            if (!ppal.P_ZDAT_SOLPED_SAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZDAT_SOLPED_SAM";
                                            }
                                            break;
                                        case "ZHIS_PEDIDOS":
                                            if (!ppal.P_ZHIS_PEDIDOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZHIS_PEDIDOS";
                                            }
                                            break;
                                        case "ZMATERIALES":
                                            if (!ppal.P_ZMATERIALES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMATERIALES";
                                            }
                                            break;
                                        case "ZCATALOGOS_PM":
                                            if (!ppal.P_ZCATALOGOS_PM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_PM";
                                            }
                                            break;
                                        case "ZCATALOGOS_QM":
                                            if (!ppal.P_ZCATALOGOS_QM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_QM";
                                            }
                                            break;
                                        case "ZCATALOGOS_MM":
                                            if (!ppal.P_ZCATALOGOS_MM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_MM";
                                            }
                                            break;
                                        case "ZCATALOGO_IW31":
                                            if (!ppal.P_ZCATALOGO_IW31(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGO_IW31";
                                            }
                                            break;
                                        case "ZCATALOGOS_IW21":
                                            if (!ppal.P_ZCATALOGOS_IW21(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_IW21";
                                            }
                                            break;
                                        case "ZMM_ALMACENES_IVEND":
                                            if (!ppal.P_ZMM_ALMACENES_IVEND(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_ALMACENES_IVEND";
                                            }
                                            break;
                                        case "ZREG_INFO":
                                            if (!ppal.P_ZREG_INFO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREG_INFO";
                                            }
                                            break;
                                        case "ZSAM_BOMEQUI":
                                            if (!ppal.P_ZSAM_BOMEQUI(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_BOMEQUI";
                                            }
                                            break;
                                        case "ZREP_ENTRADAS":
                                            if (!ppal.P_ZREP_ENTRADAS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_ENTRADAS";
                                            }
                                            break;
                                        case "ZREP_RESERVAS":
                                            if (!ppal.P_ZREP_RESERVAS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_RESERVAS";
                                            }
                                            break;
                                        case "ZREP_SOLPED":
                                            if (!ppal.P_ZREP_SOLPED(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_SOLPED";
                                            }
                                            break;
                                        case "ZREP_MOVIMIENTOS":
                                            if (!ppal.P_ZREP_MOVIMIENTOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_MOVIMIENTOS";
                                            }
                                            break;
                                        case "ZREP_CONSUMOS":
                                            if (!ppal.P_ZREP_CONSUMOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_CONSUMOS";
                                            }
                                            break;
                                        case "ZREP_AVISOS":
                                            if (!ppal.P_ZREP_AVISOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_AVISOS";
                                            }
                                            break;
                                        case "ZREP_NOTIFICACIONES":
                                            if (!ppal.P_ZREP_NOTIFICACIONES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_NOTIFICACIONES";
                                            }
                                            break;
                                        case "ZREP_ORDENES":
                                            if (!ppal.P_ZREP_ORDENES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_ORDENES";
                                            }
                                            break;
                                        //case "ZMM_STOCK_TRANSFERENCIA":
                                        //    if (!ppal.P_ZMM_STOCK_TRANSFERENCIA(c.centro))
                                        //    {
                                        //        bandera = false;
                                        //        rfc = "ZMM_STOCK_TRANSFERENCIA";
                                        //    }
                                        //    break;
                                        case "ZREPORTE_SOLPED":
                                            if (!ppal.P_ZREPORTE_SOLPE(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREPORTE_SOLPED";
                                            }
                                            break;
                                        case "ZREP_STATUS_ORD":
                                            if (!ppal.P_ZREP_STATUS_ORD(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_STATUS_ORD";
                                            }
                                            break;
                                        case "ZREP_CONTADOR_EQUIPO":
                                            if (!ppal.P_ZREP_CONTADOR_EQUIPO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_CONTADOR_EQUIPO";
                                            }
                                            break;
                                        case "ZSAM_USER_PERMISOS":
                                            if (!ppal.P_ZSAM_USER_PERMISOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_USER_PERMISOS";
                                            }
                                            break;
                                        case "ZVALIDA_SOLPED":
                                            if (!ppal.P_ZVALIDA_SOLPED(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZVALIDA_SOLPED";
                                            }
                                            break;
                                        case "ZTIPO_SOLPED":
                                            if (!ppal.P_ZTIPO_SOLPED(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZTIPO_SOLPED";
                                            }
                                            break;
                                        case "ZREP_DEFECTOS_MM":
                                            if (!ppal.P_ZREP_DEFECTOS_MM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_DEFECTOS_MM";
                                            }
                                            break;
                                        case "ZREP_LOTESINS_MM":
                                            if (!ppal.P_ZREP_LOTESINS_MM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_LOTESINS_MM";
                                            }
                                            break;
                                        case "ZQM_NOTIFICA":
                                            if(!ppal.P_ZQM_NOTIFICA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZQM_NOTIFICA";
                                            }
                                            break;
                                        case "ZREP_LOTESINS_PM":
                                            if (!ppal.P_ZREP_LOTESINS_PM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_LOTESINS_PM";
                                            }
                                            break;
                                        case "ZREP_ACT_AVISO":
                                            if (!ppal.P_ZREP_ACT_AVISO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_ACT_AVISO";
                                            }
                                            break;
                                        case "ZREP_DMSMASIVO":
                                            if (!ppal.P_ZREP_DMSMASIVO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_DMSMASIVO";
                                            }
                                            break;
                                        case "ZREP_ACTUALIZA":
                                            if (!ppal.P_ZREP_ACTUALIZA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_ACTUALIZA";
                                            }
                                            break;
                                        case "ZSD_HIS_PEDIDOS":
                                            if (!ppal.ZSD_HIS_PEDIDOS())
                                            {
                                                bandera = false;
                                                rfc = "ZSD_HIS_PEDIDOS";
                                            }
                                            break;
                                        case "ZSD_MATERIALES":
                                            if(!ppal.ZSD_MATERIALES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSD_MATERIALES";
                                            }
                                            break;
                                        case "ZCLIENTES":
                                            if(!ppal.ZSD_CLIENTES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCLIENTES";
                                            }
                                            break;
                                        case "ZSD_INTERLOCUTORES":
                                            if(!ppal.ZSD_INTERLOCUTORES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSD_INTERLOCUTORES";
                                            }
                                            break;
                                        case "ZSD_FLUJO_DOC":
                                            if(!ppal.ZSD_FLUJO_DOC(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSD_FLUJO_DOC";
                                            }
                                            break;
                                        case "ZSD_AVISOS_VENTA":
                                            if(!ppal.ZSD_AVISOS_VENTA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSD_AVISOS_VENTA";
                                            }
                                            break;
                                        case "ZCATALOGOS_SD":
                                            if (!ppal.ZCATALOGOS_SD(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_SD";
                                            }
                                            break;
                                       case "ZSD_FLUJO_VENCIMIENTO_DEUDOR":
                                           if(!ppal.ZSD_FLUJO_VENCIMIENTO_DEUDOR(c.centro))
                                           {
                                               bandera = false;
                                               rfc = "ZSD_FLUJO_VENCIMIENTO_DEUDOR";
                                           }
                                           break;
                                    }
                                }
                                else
                                {
                                    DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, rfc, "Error");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
            }

        }
        private void IniciarProcesoTransferencia()
        {
            int tim = 0;
            while (true)
            {
                try
                {
                    if (accion)
                    {
                        bool bandera = true;
                        string rfc = "";
                        List<SELECT_tiempos_Transferencia_MDL_Result> tt = DALC_StoredProcedures.ObtenerInstancia().ObtenerTiemposT(connection).ToList();
                        foreach (SELECT_tiempos_Transferencia_MDL_Result timet in tt)
                        {
                            string per = timet.periodo;
                            DateTime ts = Convert.ToDateTime(per);
                            Thread.Sleep(ts.Minute);
                            int minutos = Convert.ToInt32(ts.Minute);
                            tim = minutos * 60000;
                        }
                        List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
                        foreach (centro_MDL_Result c in cc)
                        {
                            List<OBTENER_RFCT_CENTRO_MDL_Result> ot = DALC_StoredProcedures.ObtenerInstancia().ObtenerRfcT(connection, c.centro).ToList();
                            foreach(OBTENER_RFCT_CENTRO_MDL_Result rfct in ot)
                            {
                                if (bandera)
                                {
                                    switch (rfct.rfc)
                                    {
                                        case "ZMM_STOCK_TRANSFERENCIA":
                                            if (!ppal.ZMM_STOCK_TRANSFERENCIA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_STOCK_TRANSFERENCIA";
                                            }
                                            break;
                                        case "ZMATERIALES_PP":
                                            if(!ppal.ZMATERIALES_PP(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMATERIALES_PP";
                                            }
                                            break;
                                        case "ZORDENES_DELTA":
                                            if(!ppal.ZORDENES_DELTA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZORDENES_DELTA";
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, rfc, "Error");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
                Thread.Sleep(tim);
            }
        }
        private void IniciarProcesoOperacion()
        {
            int tims = 0;
            while (true)
            {
                try
                {
                    if (accion)
                    {
                        bool bandera = true;
                        string rfc = "";
                        List<SELECT_tiempos_P_MDL_Result> tm = DALC_StoredProcedures.ObtenerInstancia().ObtenerTiempoP(connection).ToList();
                        foreach (SELECT_tiempos_P_MDL_Result t in tm)
                        {
                            string per = t.periodo;
                            DateTime ts = Convert.ToDateTime(per);
                            Thread.Sleep(ts.Minute);
                            int minutos = Convert.ToInt32(ts.Minute);
                            tims = minutos * 60000;
                        }
                        List<centro_MDL_Result> cc = DALC_StoredProcedures.ObtenerInstancia().ObtenerCentros(connection).ToList();
                        foreach (centro_MDL_Result c in cc)
                        {
                            List<OBTENER_RFCP_CENTRO_MDL_Result> po = DALC_StoredProcedures.ObtenerInstancia().ObtPro(connection, c.centro).ToList();
                            foreach (OBTENER_RFCP_CENTRO_MDL_Result ss in po)
                            {
                                if (bandera)
                                {
                                    switch (ss.rfc)
                                    {
                                        case "ZSAM_TOLERANCIA":
                                            if (!ppal.P_ZSAM_TOLERANCIA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_TOLERANCIA";
                                            }
                                            break;
                                        case "ZMM_OBYC":
                                            if (!ppal.P_ZMM_OBYC(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_OBYC";
                                            }
                                            break;
                                        case "ZMATERIAL_CENTRO":
                                            if (!ppal.P_ZMATERIAL_CENTRO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMATERIAL_CENTRO";
                                            }
                                            break;
                                        case "ZSAM_ORDENES_NOPM":
                                            if (!ppal.P_ZSAM_ORDENES_NOPM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "P_ZSAM_ORDENES_NOPM";
                                            }
                                            break;
                                        case "ZPM_VIS_NOTIFICACIONES":
                                            if (!ppal.P_ZPM_VIS_NOTIFICACIONES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPM_VIS_NOTIFICACIONES";
                                            }
                                            break;
                                        case "ZHOJA_RUTA_SAM":
                                            if (!ppal.P_ZHOJA_RUTA_SAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZHOJA_RUTA_SAM";
                                            }
                                            break;
                                        case "ZUBI_EQU":
                                            if (!ppal.P_ZUBI_EQU(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZUBI_EQU";
                                            }
                                            break;
                                        case "ZPM_NOTIFICA":
                                            if (!ppal.P_ZPM_NOTIFICA(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPM_NOTIFICA";
                                            }
                                            break;
                                        case "ZUBI_TEC":
                                            if (!ppal.P_ZUBI_TEC(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZUBI_TEC";
                                            }
                                            break;
                                        case "ZORDENES_PM":
                                            if (!ppal.P_ZORDENES_PM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZORDENES_PM";
                                            }
                                            break;
                                        case "ZRELACION_EQUIPOS":
                                            if (!ppal.P_ZRELACION_EQUIPOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZRELACION_EQUIPOS";
                                            }
                                            break;
                                        case "ZMM_RESERVAS_MATERIALES":
                                            if (!ppal.P_ZMM_RESERVAS_MATERIALES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_RESERVAS_MATERIALES";
                                            }
                                            break;
                                        case "ZQM_PLAN_INSP_CENTRO":
                                            if (!ppal.P_ZQM_PLAN_INSP_CENTRO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZQM_PLAN_INSP_CENTRO";
                                            }
                                            break;
                                        case "ZSAM_LIST_MOV_VIS":
                                            if (!ppal.P_ZSAM_LIST_MOV_VIS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_LIST_MOV_VIS";
                                            }
                                            break;
                                        case "ZPROVEEDORES":
                                            if (!ppal.P_ZPROVEEDORES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPROVEEDORES";
                                            }
                                            break;
                                        //case "ZCLIENTES":
                                        //    if (!ppal.P_ZCLIENTES(c.centro))
                                        //    {
                                        //        bandera = false;
                                        //        rfc = "ZCLIENTES";
                                        //    }
                                        //    break;
                                        case "ZORDENES_PMSAM":
                                            if (!ppal.P_ZORDENES_PMSAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZORDENES_PMSAM";
                                            }
                                            break;
                                        case "ZPM_MATERIALES_ALMACEN":
                                            if (!ppal.P_ZPM_MATERIALES_ALMACEN(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZPM_MATERIALES_ALMACEN";
                                            }
                                            break;
                                        case "ZDAT_SOLPED_SAM":
                                            if (!ppal.P_ZDAT_SOLPED_SAM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZDAT_SOLPED_SAM";
                                            }
                                            break;
                                        case "ZHIS_PEDIDOS":
                                            if (!ppal.P_ZHIS_PEDIDOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZHIS_PEDIDOS";
                                            }
                                            break;
                                        case "ZMATERIALES":
                                            if (!ppal.P_ZMATERIALES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMATERIALES";
                                            }
                                            break;
                                        case "ZCATALOGOS_PM":
                                            if (!ppal.P_ZCATALOGOS_PM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_PM";
                                            }
                                            break;
                                        case "ZCATALOGOS_QM":
                                            if (!ppal.P_ZCATALOGOS_QM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_QM";
                                            }
                                            break;
                                        case "ZCATALOGOS_MM":
                                            if (!ppal.P_ZCATALOGOS_MM(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_MM";
                                            }
                                            break;
                                        case "ZCATALOGO_IW31":
                                            if (!ppal.P_ZCATALOGO_IW31(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGO_IW31";
                                            }
                                            break;
                                        case "ZCATALOGOS_IW21":
                                            if (!ppal.P_ZCATALOGOS_IW21(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_IW21";
                                            }
                                            break;
                                        case "ZMM_ALMACENES_IVEND":
                                            if (!ppal.P_ZMM_ALMACENES_IVEND(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZMM_ALMACENES_IVEND";
                                            }
                                            break;
                                        case "ZREG_INFO":
                                            if (!ppal.P_ZREG_INFO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREG_INFO";
                                            }
                                            break;
                                        case "ZSAM_BOMEQUI":
                                            if (!ppal.P_ZSAM_BOMEQUI(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_BOMEQUI";
                                            }
                                            break;
                                        case "ZREP_ENTRADAS":
                                            if (!ppal.P_ZREP_ENTRADAS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_ENTRADAS";
                                            }
                                            break;
                                        case "ZREP_RESERVAS":
                                            if (!ppal.P_ZREP_RESERVAS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_RESERVAS";
                                            }
                                            break;
                                        case "ZREP_SOLPED":
                                            if (!ppal.P_ZREP_SOLPED(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_SOLPED";
                                            }
                                            break;
                                        case "ZREP_MOVIMIENTOS":
                                            if (!ppal.P_ZREP_MOVIMIENTOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_MOVIMIENTOS";
                                            }
                                            break;
                                        case "ZREP_CONSUMOS":
                                            if (!ppal.P_ZREP_CONSUMOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_CONSUMOS";
                                            }
                                            break;
                                        case "ZREP_AVISOS":
                                            if (!ppal.P_ZREP_AVISOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_AVISOS";
                                            }
                                            break;
                                        case "ZREP_NOTIFICACIONES":
                                            if (!ppal.P_ZREP_NOTIFICACIONES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_NOTIFICACIONES";
                                            }
                                            break;
                                        case "ZREP_ORDENES":
                                            if (!ppal.P_ZREP_ORDENES(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_ORDENES";
                                            }
                                            break;
                                        case "ZREPORTE_SOLPED":
                                            if (!ppal.P_ZREPORTE_SOLPE(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREPORTE_SOLPED";
                                            }
                                            break;
                                        case "ZREP_STATUS_ORD":
                                            if (!ppal.P_ZREP_STATUS_ORD(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_STATUS_ORD";
                                            }
                                            break;
                                        case "ZREP_CONTADOR_EQUIPO":
                                            if (!ppal.P_ZREP_CONTADOR_EQUIPO(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZREP_CONTADOR_EQUIPO";
                                            }
                                            break;
                                        case "ZSAM_USER_PERMISOS":
                                            if (!ppal.P_ZSAM_USER_PERMISOS(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZSAM_USER_PERMISOS";
                                            }
                                            break;
                                        case "ZVALIDA_SOLPED":
                                            if (!ppal.P_ZVALIDA_SOLPED(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZVALIDA_SOLPED";
                                            }
                                            break;
                                        case "ZCATALOGOS_SD":
                                            if (!ppal.ZCATALOGOS_SD(c.centro))
                                            {
                                                bandera = false;
                                                rfc = "ZCATALOGOS_SD";
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    DALC_sincronizacion.ObtenerInstancia().InsertarErrorMDL(connection, rfc, "Error");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
                Thread.Sleep(tims);
            }
        }
        private void pingNetwork()
        {
            //Ping Pings = new Ping();
            //int timeout = 10;

            //if (Pings.Send("192.168.100.44").Status == IPStatus.Success)
            //{
            //    NetworkAccess = true;
            //}
            //else
            //{
            //    NetworkAccess = false;
            //}
            //try
            //{
            //    System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
            //    NetworkAccess = true;
            //}
            //catch (Exception) { NetworkAccess = false; }
            if (ppal.Ping())
            {
                NetworkAccess = true;
            }
            else
            {
                NetworkAccess = false;
            }
        }
        private void tmrSincroniza_Tick(object sender, EventArgs e)
        {
            timer++;
            label1.Text = timer.ToString();
            if (!NetworkAccess)
            {
                timer = 0;
            }
            if (timer == limiteTimer)
            {
                iniciarConsulta();
            }
        }
    }
}
