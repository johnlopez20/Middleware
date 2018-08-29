namespace MiddlewareSincronizacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnParar = new System.Windows.Forms.Button();
            this.mynotifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónServidorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracíonRutaDeFacturaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionRequestorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónServidorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarRutaFacturaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confiurarLocalHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tooltipDatos = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tmrSincroniza = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Enabled = false;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnIniciar.Image = global::MiddlewareSincronizacion.Properties.Resources.btnIniciar_Image;
            this.btnIniciar.Location = new System.Drawing.Point(7, 26);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(146, 137);
            this.btnIniciar.TabIndex = 1;
            this.btnIniciar.UseCompatibleTextRendering = true;
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnParar
            // 
            this.btnParar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnParar.Enabled = false;
            this.btnParar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnParar.Image = global::MiddlewareSincronizacion.Properties.Resources.btnParar_Image;
            this.btnParar.Location = new System.Drawing.Point(159, 27);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(131, 137);
            this.btnParar.TabIndex = 2;
            this.btnParar.UseVisualStyleBackColor = false;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // mynotifyicon
            // 
            this.mynotifyicon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mynotifyicon.BalloonTipText = "Meddleware BF";
            this.mynotifyicon.BalloonTipTitle = "Ejecutando Meddleware";
            this.mynotifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("mynotifyicon.Icon")));
            this.mynotifyicon.Text = "Meddleware";
            this.mynotifyicon.Visible = true;
            this.mynotifyicon.BalloonTipClicked += new System.EventHandler(this.mynotifyicon_BalloonTipClicked);
            this.mynotifyicon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mynotifyicon_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.herramientasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(300, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem,
            this.configuraciónServidorToolStripMenuItem1,
            this.configurarRutaFacturaciónToolStripMenuItem,
            this.confiurarLocalHostToolStripMenuItem});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónServidorToolStripMenuItem,
            this.configuracíonRutaDeFacturaciónToolStripMenuItem,
            this.configuracionRequestorToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            this.opcionesToolStripMenuItem.Visible = false;
            // 
            // configuraciónServidorToolStripMenuItem
            // 
            this.configuraciónServidorToolStripMenuItem.Name = "configuraciónServidorToolStripMenuItem";
            this.configuraciónServidorToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.configuraciónServidorToolStripMenuItem.Text = "Configuración Servidor";
            this.configuraciónServidorToolStripMenuItem.Click += new System.EventHandler(this.configuraciónServidorToolStripMenuItem_Click);
            // 
            // configuracíonRutaDeFacturaciónToolStripMenuItem
            // 
            this.configuracíonRutaDeFacturaciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.configuracíonRutaDeFacturaciónToolStripMenuItem.Name = "configuracíonRutaDeFacturaciónToolStripMenuItem";
            this.configuracíonRutaDeFacturaciónToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.configuracíonRutaDeFacturaciónToolStripMenuItem.Text = "Configuracíon ruta de facturación";
            this.configuracíonRutaDeFacturaciónToolStripMenuItem.MouseEnter += new System.EventHandler(this.configuracíonRutaDeFacturaciónToolStripMenuItem_MouseEnter);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(77, 22);
            this.toolStripMenuItem2.Text = " ";
            // 
            // configuracionRequestorToolStripMenuItem
            // 
            this.configuracionRequestorToolStripMenuItem.Name = "configuracionRequestorToolStripMenuItem";
            this.configuracionRequestorToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.configuracionRequestorToolStripMenuItem.Text = "Configuracion Requestor";
            this.configuracionRequestorToolStripMenuItem.Visible = false;
            // 
            // configuraciónServidorToolStripMenuItem1
            // 
            this.configuraciónServidorToolStripMenuItem1.Name = "configuraciónServidorToolStripMenuItem1";
            this.configuraciónServidorToolStripMenuItem1.Size = new System.Drawing.Size(223, 22);
            this.configuraciónServidorToolStripMenuItem1.Text = "Configuración Servidor";
            this.configuraciónServidorToolStripMenuItem1.Click += new System.EventHandler(this.configuraciónServidorToolStripMenuItem_Click);
            // 
            // configurarRutaFacturaciónToolStripMenuItem
            // 
            this.configurarRutaFacturaciónToolStripMenuItem.Name = "configurarRutaFacturaciónToolStripMenuItem";
            this.configurarRutaFacturaciónToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.configurarRutaFacturaciónToolStripMenuItem.Text = "Configurar Ruta Facturación";
            this.configurarRutaFacturaciónToolStripMenuItem.Visible = false;
            // 
            // confiurarLocalHostToolStripMenuItem
            // 
            this.confiurarLocalHostToolStripMenuItem.Name = "confiurarLocalHostToolStripMenuItem";
            this.confiurarLocalHostToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.confiurarLocalHostToolStripMenuItem.Text = "Confiurar LocalHost";
            this.confiurarLocalHostToolStripMenuItem.Click += new System.EventHandler(this.confiurarLocalHostToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Perpetua Titling MT", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // tmrSincroniza
            // 
            this.tmrSincroniza.Interval = 1000;
            this.tmrSincroniza.Tick += new System.EventHandler(this.tmrSincroniza_Tick);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(300, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(316, 307);
            this.MinimumSize = new System.Drawing.Size(316, 207);
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Middleware";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Inicio_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Inicio_FormClosed);
            this.Resize += new System.EventHandler(this.Inicio_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.NotifyIcon mynotifyicon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónServidorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracíonRutaDeFacturaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionRequestorToolStripMenuItem;
        private System.Windows.Forms.ToolTip tooltipDatos;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem configuraciónServidorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configurarRutaFacturaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confiurarLocalHostToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrSincroniza;
    }
}

