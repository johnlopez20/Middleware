namespace MiddlewareSincronizacion
{
    partial class frmConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguracion));
            this.btnGuardarConfig = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSystemSAP = new System.Windows.Forms.TextBox();
            this.txtSystemNuberSAP = new System.Windows.Forms.TextBox();
            this.txtAplicationServer = new System.Windows.Forms.TextBox();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.txtSAProuter = new System.Windows.Forms.TextBox();
            this.pnlFondo = new System.Windows.Forms.Panel();
            this.lklClose = new System.Windows.Forms.LinkLabel();
            this.pnlFondo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuardarConfig
            // 
            this.btnGuardarConfig.Enabled = false;
            this.btnGuardarConfig.Location = new System.Drawing.Point(309, 283);
            this.btnGuardarConfig.Name = "btnGuardarConfig";
            this.btnGuardarConfig.Size = new System.Drawing.Size(93, 55);
            this.btnGuardarConfig.TabIndex = 0;
            this.btnGuardarConfig.Text = "Guardar";
            this.btnGuardarConfig.UseVisualStyleBackColor = true;
            this.btnGuardarConfig.Click += new System.EventHandler(this.btnGuardarConfig_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(184, 283);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(119, 55);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Probar \r\nconexión";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "System";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "System Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Application Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Client";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "User";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(96, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Language";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(96, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "SAProuter";
            // 
            // txtSystemSAP
            // 
            this.txtSystemSAP.Location = new System.Drawing.Point(199, 23);
            this.txtSystemSAP.Name = "txtSystemSAP";
            this.txtSystemSAP.Size = new System.Drawing.Size(158, 26);
            this.txtSystemSAP.TabIndex = 10;
            this.txtSystemSAP.Text = "PRO";
            this.txtSystemSAP.TextChanged += new System.EventHandler(this.txtSystemSAP_TextChanged);
            // 
            // txtSystemNuberSAP
            // 
            this.txtSystemNuberSAP.Location = new System.Drawing.Point(199, 55);
            this.txtSystemNuberSAP.Name = "txtSystemNuberSAP";
            this.txtSystemNuberSAP.Size = new System.Drawing.Size(158, 26);
            this.txtSystemNuberSAP.TabIndex = 11;
            this.txtSystemNuberSAP.Text = "00";
            this.txtSystemNuberSAP.TextChanged += new System.EventHandler(this.txtSystemNuberSAP_TextChanged);
            // 
            // txtAplicationServer
            // 
            this.txtAplicationServer.Location = new System.Drawing.Point(199, 87);
            this.txtAplicationServer.Name = "txtAplicationServer";
            this.txtAplicationServer.Size = new System.Drawing.Size(158, 26);
            this.txtAplicationServer.TabIndex = 12;
            this.txtAplicationServer.Text = "192.168.254.11";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(199, 119);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(158, 26);
            this.txtClient.TabIndex = 13;
            this.txtClient.Text = "400";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(199, 149);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(158, 26);
            this.txtUser.TabIndex = 14;
            this.txtUser.Text = "desarrollo";
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(199, 181);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(158, 26);
            this.txtPassword.TabIndex = 15;
            this.txtPassword.Text = "prisma.1";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtLanguage
            // 
            this.txtLanguage.Location = new System.Drawing.Point(199, 210);
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.Size = new System.Drawing.Size(48, 26);
            this.txtLanguage.TabIndex = 16;
            this.txtLanguage.Text = "es";
            // 
            // txtSAProuter
            // 
            this.txtSAProuter.Location = new System.Drawing.Point(199, 242);
            this.txtSAProuter.Name = "txtSAProuter";
            this.txtSAProuter.Size = new System.Drawing.Size(158, 26);
            this.txtSAProuter.TabIndex = 17;
            this.txtSAProuter.Text = "/H/148.244.119.194/H/";
            // 
            // pnlFondo
            // 
            this.pnlFondo.Controls.Add(this.lklClose);
            this.pnlFondo.Controls.Add(this.txtSAProuter);
            this.pnlFondo.Controls.Add(this.txtLanguage);
            this.pnlFondo.Controls.Add(this.txtPassword);
            this.pnlFondo.Controls.Add(this.txtUser);
            this.pnlFondo.Controls.Add(this.txtClient);
            this.pnlFondo.Controls.Add(this.txtAplicationServer);
            this.pnlFondo.Controls.Add(this.txtSystemNuberSAP);
            this.pnlFondo.Controls.Add(this.txtSystemSAP);
            this.pnlFondo.Controls.Add(this.label8);
            this.pnlFondo.Controls.Add(this.label7);
            this.pnlFondo.Controls.Add(this.label6);
            this.pnlFondo.Controls.Add(this.label5);
            this.pnlFondo.Controls.Add(this.label4);
            this.pnlFondo.Controls.Add(this.label3);
            this.pnlFondo.Controls.Add(this.label2);
            this.pnlFondo.Controls.Add(this.label1);
            this.pnlFondo.Controls.Add(this.btnTest);
            this.pnlFondo.Controls.Add(this.btnGuardarConfig);
            this.pnlFondo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.pnlFondo.Location = new System.Drawing.Point(0, 0);
            this.pnlFondo.Name = "pnlFondo";
            this.pnlFondo.Size = new System.Drawing.Size(449, 352);
            this.pnlFondo.TabIndex = 0;
            // 
            // lklClose
            // 
            this.lklClose.AutoSize = true;
            this.lklClose.Location = new System.Drawing.Point(415, 9);
            this.lklClose.Name = "lklClose";
            this.lklClose.Size = new System.Drawing.Size(20, 20);
            this.lklClose.TabIndex = 18;
            this.lklClose.TabStop = true;
            this.lklClose.Text = "X";
            this.lklClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklClose_LinkClicked);
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 352);
            this.ControlBox = false;
            this.Controls.Add(this.pnlFondo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfiguracion";
            this.Shown += new System.EventHandler(this.frmConfiguracion_Shown);
            this.pnlFondo.ResumeLayout(false);
            this.pnlFondo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGuardarConfig;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSystemSAP;
        private System.Windows.Forms.TextBox txtSystemNuberSAP;
        private System.Windows.Forms.TextBox txtAplicationServer;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.TextBox txtSAProuter;
        private System.Windows.Forms.Panel pnlFondo;
        private System.Windows.Forms.LinkLabel lklClose;

    }
}