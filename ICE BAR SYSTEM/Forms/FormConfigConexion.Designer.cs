namespace IceBarSystem.Forms
{
    partial class FormConfigConexion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnCerrarHeader = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnInicializarBD = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnProbar = new System.Windows.Forms.Button();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.txtPasswordSql = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUsuarioSql = new System.Windows.Forms.TextBox();
            this.lblUserSql = new System.Windows.Forms.Label();
            this.chkWindowsAuth = new System.Windows.Forms.CheckBox();
            this.txtBaseDatos = new System.Windows.Forms.TextBox();
            this.lblBD = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.gbConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Navy;
            this.panelHeader.Controls.Add(this.btnCerrarHeader);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(560, 50);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverVentana_MouseDown);
            // 
            // btnCerrarHeader
            // 
            this.btnCerrarHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarHeader.FlatAppearance.BorderSize = 0;
            this.btnCerrarHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCerrarHeader.ForeColor = System.Drawing.Color.White;
            this.btnCerrarHeader.Location = new System.Drawing.Point(520, 8);
            this.btnCerrarHeader.Name = "btnCerrarHeader";
            this.btnCerrarHeader.Size = new System.Drawing.Size(32, 32);
            this.btnCerrarHeader.TabIndex = 1;
            this.btnCerrarHeader.Text = "✕";
            this.btnCerrarHeader.UseVisualStyleBackColor = true;
            this.btnCerrarHeader.Click += new System.EventHandler(this.btnCerrarHeader_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(395, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "⚙️ CONFIGURACIÓN DE CONEXIÓN A SQL SERVER";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.panelMain.Controls.Add(this.lblEstado);
            this.panelMain.Controls.Add(this.panelBotones);
            this.panelMain.Controls.Add(this.gbConfig);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 50);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(15);
            this.panelMain.Size = new System.Drawing.Size(560, 430);
            this.panelMain.TabIndex = 1;
            // 
            // lblEstado
            // 
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblEstado.Location = new System.Drawing.Point(20, 310);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(520, 45);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Estado: Verifique los datos del servidor SQL y haga clic en 'Probar Conexión'.";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnInicializarBD);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnProbar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(15, 365);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(530, 50);
            this.panelBotones.TabIndex = 1;
            // 
            // btnInicializarBD
            // 
            this.btnInicializarBD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnInicializarBD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInicializarBD.FlatAppearance.BorderSize = 0;
            this.btnInicializarBD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicializarBD.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInicializarBD.ForeColor = System.Drawing.Color.White;
            this.btnInicializarBD.Location = new System.Drawing.Point(340, 5);
            this.btnInicializarBD.Name = "btnInicializarBD";
            this.btnInicializarBD.Size = new System.Drawing.Size(180, 40);
            this.btnInicializarBD.TabIndex = 2;
            this.btnInicializarBD.Text = "⚡ Inicializar Tablas/Seed";
            this.btnInicializarBD.UseVisualStyleBackColor = false;
            this.btnInicializarBD.Click += new System.EventHandler(this.btnInicializarBD_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(175, 5);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(155, 40);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "💾 Guardar Conexión";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnProbar
            // 
            this.btnProbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnProbar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProbar.FlatAppearance.BorderSize = 0;
            this.btnProbar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProbar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnProbar.ForeColor = System.Drawing.Color.White;
            this.btnProbar.Location = new System.Drawing.Point(10, 5);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(155, 40);
            this.btnProbar.TabIndex = 0;
            this.btnProbar.Text = "🔌 Probar Conexión";
            this.btnProbar.UseVisualStyleBackColor = false;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // gbConfig
            // 
            this.gbConfig.BackColor = System.Drawing.Color.White;
            this.gbConfig.Controls.Add(this.txtPasswordSql);
            this.gbConfig.Controls.Add(this.lblPass);
            this.gbConfig.Controls.Add(this.txtUsuarioSql);
            this.gbConfig.Controls.Add(this.lblUserSql);
            this.gbConfig.Controls.Add(this.chkWindowsAuth);
            this.gbConfig.Controls.Add(this.txtBaseDatos);
            this.gbConfig.Controls.Add(this.lblBD);
            this.gbConfig.Controls.Add(this.txtServidor);
            this.gbConfig.Controls.Add(this.lblServidor);
            this.gbConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbConfig.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gbConfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(41)))), ((int)(((byte)(68)))));
            this.gbConfig.Location = new System.Drawing.Point(15, 15);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Padding = new System.Windows.Forms.Padding(12);
            this.gbConfig.Size = new System.Drawing.Size(530, 285);
            this.gbConfig.TabIndex = 0;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Parámetros de Servidor SQL Server";
            // 
            // txtPasswordSql
            // 
            this.txtPasswordSql.Enabled = false;
            this.txtPasswordSql.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPasswordSql.Location = new System.Drawing.Point(160, 235);
            this.txtPasswordSql.Name = "txtPasswordSql";
            this.txtPasswordSql.Size = new System.Drawing.Size(345, 25);
            this.txtPasswordSql.TabIndex = 8;
            this.txtPasswordSql.UseSystemPasswordChar = true;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPass.Location = new System.Drawing.Point(20, 238);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(95, 17);
            this.lblPass.TabIndex = 7;
            this.lblPass.Text = "Contraseña SQL:";
            // 
            // txtUsuarioSql
            // 
            this.txtUsuarioSql.Enabled = false;
            this.txtUsuarioSql.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUsuarioSql.Location = new System.Drawing.Point(160, 195);
            this.txtUsuarioSql.Name = "txtUsuarioSql";
            this.txtUsuarioSql.Size = new System.Drawing.Size(345, 25);
            this.txtUsuarioSql.TabIndex = 6;
            // 
            // lblUserSql
            // 
            this.lblUserSql.AutoSize = true;
            this.lblUserSql.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblUserSql.Location = new System.Drawing.Point(20, 198);
            this.lblUserSql.Name = "lblUserSql";
            this.lblUserSql.Size = new System.Drawing.Size(74, 17);
            this.lblUserSql.TabIndex = 5;
            this.lblUserSql.Text = "Usuario SQL:";
            // 
            // chkWindowsAuth
            // 
            this.chkWindowsAuth.AutoSize = true;
            this.chkWindowsAuth.Checked = true;
            this.chkWindowsAuth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowsAuth.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkWindowsAuth.Location = new System.Drawing.Point(160, 155);
            this.chkWindowsAuth.Name = "chkWindowsAuth";
            this.chkWindowsAuth.Size = new System.Drawing.Size(262, 21);
            this.chkWindowsAuth.TabIndex = 4;
            this.chkWindowsAuth.Text = "Autenticación Integrada de Windows (SSPI)";
            this.chkWindowsAuth.UseVisualStyleBackColor = true;
            this.chkWindowsAuth.CheckedChanged += new System.EventHandler(this.chkWindowsAuth_CheckedChanged);
            // 
            // txtBaseDatos
            // 
            this.txtBaseDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtBaseDatos.Location = new System.Drawing.Point(160, 95);
            this.txtBaseDatos.Name = "txtBaseDatos";
            this.txtBaseDatos.Size = new System.Drawing.Size(345, 25);
            this.txtBaseDatos.TabIndex = 3;
            this.txtBaseDatos.Text = "IceBarDB";
            // 
            // lblBD
            // 
            this.lblBD.AutoSize = true;
            this.lblBD.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBD.Location = new System.Drawing.Point(20, 98);
            this.lblBD.Name = "lblBD";
            this.lblBD.Size = new System.Drawing.Size(94, 17);
            this.lblBD.TabIndex = 2;
            this.lblBD.Text = "Base de Datos:";
            // 
            // txtServidor
            // 
            this.txtServidor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtServidor.Location = new System.Drawing.Point(160, 50);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(345, 25);
            this.txtServidor.TabIndex = 1;
            this.txtServidor.Text = ".";
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblServidor.Location = new System.Drawing.Point(20, 53);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(127, 17);
            this.lblServidor.TabIndex = 0;
            this.lblServidor.Text = "Servidor / Instancia:";
            // 
            // FormConfigConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(560, 480);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormConfigConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurar Conexión";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelBotones.ResumeLayout(false);
            this.gbConfig.ResumeLayout(false);
            this.gbConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrarHeader;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label lblBD;
        private System.Windows.Forms.TextBox txtBaseDatos;
        private System.Windows.Forms.CheckBox chkWindowsAuth;
        private System.Windows.Forms.Label lblUserSql;
        private System.Windows.Forms.TextBox txtUsuarioSql;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPasswordSql;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnProbar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnInicializarBD;
        private System.Windows.Forms.Label lblEstado;
    }
}

