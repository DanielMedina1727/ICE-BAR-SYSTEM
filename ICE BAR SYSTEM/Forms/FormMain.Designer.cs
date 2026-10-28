namespace IceBarSystem.Forms
{
    partial class FormMain
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
            this.MenuVertical = new System.Windows.Forms.Panel();
            this.btnCerrSes = new System.Windows.Forms.Button();
            this.btnConfigConexion = new System.Windows.Forms.Button();
            this.btnIncidencias = new System.Windows.Forms.Button();
            this.SubmenuVentas = new System.Windows.Forms.Panel();
            this.btnHistorialVentas = new System.Windows.Forms.Button();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            this.btnVentas = new System.Windows.Forms.Button();
            this.SubmenuMantenedor = new System.Windows.Forms.Panel();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnConsultaStock = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnMantenedor = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblSubtituloLogo = new System.Windows.Forms.Label();
            this.lblTituloLogo = new System.Windows.Forms.Label();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.lblModuloActual = new System.Windows.Forms.Label();
            this.lblReloj = new System.Windows.Forms.Label();
            this.lblUsuarioInfo = new System.Windows.Forms.Label();
            this.iconminimizar = new System.Windows.Forms.Button();
            this.iconrestaurar = new System.Windows.Forms.Button();
            this.iconmaximizar = new System.Windows.Forms.Button();
            this.iconcerrar = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.MenuVertical.SuspendLayout();
            this.SubmenuVentas.SuspendLayout();
            this.SubmenuMantenedor.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.BarraTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuVertical
            // 
            this.MenuVertical.AutoScroll = true;
            this.MenuVertical.BackColor = System.Drawing.Color.Navy;
            this.MenuVertical.Controls.Add(this.btnCerrSes);
            this.MenuVertical.Controls.Add(this.btnConfigConexion);
            this.MenuVertical.Controls.Add(this.btnIncidencias);
            this.MenuVertical.Controls.Add(this.SubmenuVentas);
            this.MenuVertical.Controls.Add(this.btnVentas);
            this.MenuVertical.Controls.Add(this.SubmenuMantenedor);
            this.MenuVertical.Controls.Add(this.btnMantenedor);
            this.MenuVertical.Controls.Add(this.btnDashboard);
            this.MenuVertical.Controls.Add(this.panelLogo);
            this.MenuVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuVertical.Location = new System.Drawing.Point(0, 0);
            this.MenuVertical.Name = "MenuVertical";
            this.MenuVertical.Size = new System.Drawing.Size(250, 720);
            this.MenuVertical.TabIndex = 0;
            // 
            // btnCerrSes
            // 
            this.btnCerrSes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrSes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrSes.FlatAppearance.BorderSize = 0;
            this.btnCerrSes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCerrSes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrSes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCerrSes.ForeColor = System.Drawing.Color.White;
            this.btnCerrSes.Location = new System.Drawing.Point(0, 675);
            this.btnCerrSes.Name = "btnCerrSes";
            this.btnCerrSes.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCerrSes.Size = new System.Drawing.Size(250, 45);
            this.btnCerrSes.TabIndex = 8;
            this.btnCerrSes.Text = "🚪   Cerrar Sesión";
            this.btnCerrSes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrSes.UseVisualStyleBackColor = true;
            this.btnCerrSes.Click += new System.EventHandler(this.btnCerrSes_Click);
            // 
            // btnConfigConexion
            // 
            this.btnConfigConexion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigConexion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConfigConexion.FlatAppearance.BorderSize = 0;
            this.btnConfigConexion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnConfigConexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigConexion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConfigConexion.ForeColor = System.Drawing.Color.White;
            this.btnConfigConexion.Location = new System.Drawing.Point(0, 490);
            this.btnConfigConexion.Name = "btnConfigConexion";
            this.btnConfigConexion.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnConfigConexion.Size = new System.Drawing.Size(250, 45);
            this.btnConfigConexion.TabIndex = 7;
            this.btnConfigConexion.Text = "⚙️   Configuración BD";
            this.btnConfigConexion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigConexion.UseVisualStyleBackColor = true;
            this.btnConfigConexion.Click += new System.EventHandler(this.btnConfigConexion_Click);
            // 
            // btnIncidencias
            // 
            this.btnIncidencias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIncidencias.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnIncidencias.FlatAppearance.BorderSize = 0;
            this.btnIncidencias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnIncidencias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIncidencias.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnIncidencias.ForeColor = System.Drawing.Color.White;
            this.btnIncidencias.Location = new System.Drawing.Point(0, 445);
            this.btnIncidencias.Name = "btnIncidencias";
            this.btnIncidencias.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnIncidencias.Size = new System.Drawing.Size(250, 45);
            this.btnIncidencias.TabIndex = 6;
            this.btnIncidencias.Text = "⚠️   Incidencias";
            this.btnIncidencias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncidencias.UseVisualStyleBackColor = true;
            this.btnIncidencias.Click += new System.EventHandler(this.btnIncidencias_Click);
            // 
            // SubmenuVentas
            // 
            this.SubmenuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(30)))), ((int)(((byte)(52)))));
            this.SubmenuVentas.Controls.Add(this.btnHistorialVentas);
            this.SubmenuVentas.Controls.Add(this.btnRegistrarVenta);
            this.SubmenuVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.SubmenuVentas.Location = new System.Drawing.Point(0, 365);
            this.SubmenuVentas.Name = "SubmenuVentas";
            this.SubmenuVentas.Size = new System.Drawing.Size(250, 80);
            this.SubmenuVentas.TabIndex = 5;
            // 
            // btnHistorialVentas
            // 
            this.btnHistorialVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistorialVentas.FlatAppearance.BorderSize = 0;
            this.btnHistorialVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnHistorialVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialVentas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHistorialVentas.ForeColor = System.Drawing.Color.White;
            this.btnHistorialVentas.Location = new System.Drawing.Point(0, 40);
            this.btnHistorialVentas.Name = "btnHistorialVentas";
            this.btnHistorialVentas.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnHistorialVentas.Size = new System.Drawing.Size(250, 40);
            this.btnHistorialVentas.TabIndex = 1;
            this.btnHistorialVentas.Text = "• Historial de Ventas";
            this.btnHistorialVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorialVentas.UseVisualStyleBackColor = true;
            this.btnHistorialVentas.Click += new System.EventHandler(this.btnHistorialVentas_Click);
            // 
            // btnRegistrarVenta
            // 
            this.btnRegistrarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarVenta.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegistrarVenta.FlatAppearance.BorderSize = 0;
            this.btnRegistrarVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnRegistrarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarVenta.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRegistrarVenta.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarVenta.Location = new System.Drawing.Point(0, 0);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnRegistrarVenta.Size = new System.Drawing.Size(250, 40);
            this.btnRegistrarVenta.TabIndex = 0;
            this.btnRegistrarVenta.Text = "• Registrar Venta (POS)";
            this.btnRegistrarVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrarVenta.UseVisualStyleBackColor = true;
            this.btnRegistrarVenta.Click += new System.EventHandler(this.btnRegistrarVenta_Click);
            // 
            // btnVentas
            // 
            this.btnVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVentas.FlatAppearance.BorderSize = 0;
            this.btnVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentas.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnVentas.ForeColor = System.Drawing.Color.White;
            this.btnVentas.Location = new System.Drawing.Point(0, 320);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnVentas.Size = new System.Drawing.Size(250, 45);
            this.btnVentas.TabIndex = 4;
            this.btnVentas.Text = "🛒   Ventas ▼";
            this.btnVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentas.UseVisualStyleBackColor = true;
            this.btnVentas.Click += new System.EventHandler(this.btnVentas_Click);
            // 
            // SubmenuMantenedor
            // 
            this.SubmenuMantenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(30)))), ((int)(((byte)(52)))));
            this.SubmenuMantenedor.Controls.Add(this.btnUsuarios);
            this.SubmenuMantenedor.Controls.Add(this.btnConsultaStock);
            this.SubmenuMantenedor.Controls.Add(this.btnProductos);
            this.SubmenuMantenedor.Dock = System.Windows.Forms.DockStyle.Top;
            this.SubmenuMantenedor.Location = new System.Drawing.Point(0, 200);
            this.SubmenuMantenedor.Name = "SubmenuMantenedor";
            this.SubmenuMantenedor.Size = new System.Drawing.Size(250, 120);
            this.SubmenuMantenedor.TabIndex = 3;
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 80);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(250, 40);
            this.btnUsuarios.TabIndex = 2;
            this.btnUsuarios.Text = "• Gestión de Usuarios";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnConsultaStock
            // 
            this.btnConsultaStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultaStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConsultaStock.FlatAppearance.BorderSize = 0;
            this.btnConsultaStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnConsultaStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultaStock.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConsultaStock.ForeColor = System.Drawing.Color.White;
            this.btnConsultaStock.Location = new System.Drawing.Point(0, 40);
            this.btnConsultaStock.Name = "btnConsultaStock";
            this.btnConsultaStock.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnConsultaStock.Size = new System.Drawing.Size(250, 40);
            this.btnConsultaStock.TabIndex = 1;
            this.btnConsultaStock.Text = "• Consulta de Stock";
            this.btnConsultaStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultaStock.UseVisualStyleBackColor = true;
            this.btnConsultaStock.Click += new System.EventHandler(this.btnConsultaStock_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProductos.FlatAppearance.BorderSize = 0;
            this.btnProductos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnProductos.ForeColor = System.Drawing.Color.White;
            this.btnProductos.Location = new System.Drawing.Point(0, 0);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnProductos.Size = new System.Drawing.Size(250, 40);
            this.btnProductos.TabIndex = 0;
            this.btnProductos.Text = "• Productos e Inventario";
            this.btnProductos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnMantenedor
            // 
            this.btnMantenedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMantenedor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMantenedor.FlatAppearance.BorderSize = 0;
            this.btnMantenedor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnMantenedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMantenedor.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMantenedor.ForeColor = System.Drawing.Color.White;
            this.btnMantenedor.Location = new System.Drawing.Point(0, 155);
            this.btnMantenedor.Name = "btnMantenedor";
            this.btnMantenedor.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnMantenedor.Size = new System.Drawing.Size(250, 45);
            this.btnMantenedor.TabIndex = 2;
            this.btnMantenedor.Text = "📦   Mantenedor ▼";
            this.btnMantenedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMantenedor.UseVisualStyleBackColor = true;
            this.btnMantenedor.Click += new System.EventHandler(this.btnMantenedor_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 110);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(250, 45);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "📊   Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(30)))), ((int)(((byte)(55)))));
            this.panelLogo.Controls.Add(this.lblSubtituloLogo);
            this.panelLogo.Controls.Add(this.lblTituloLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 110);
            this.panelLogo.TabIndex = 0;
            // 
            // lblSubtituloLogo
            // 
            this.lblSubtituloLogo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtituloLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(190)))), ((int)(((byte)(230)))));
            this.lblSubtituloLogo.Location = new System.Drawing.Point(10, 65);
            this.lblSubtituloLogo.Name = "lblSubtituloLogo";
            this.lblSubtituloLogo.Size = new System.Drawing.Size(230, 25);
            this.lblSubtituloLogo.TabIndex = 1;
            this.lblSubtituloLogo.Text = "Sistema de Bar && Ventas";
            this.lblSubtituloLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTituloLogo
            // 
            this.lblTituloLogo.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTituloLogo.ForeColor = System.Drawing.Color.White;
            this.lblTituloLogo.Location = new System.Drawing.Point(10, 20);
            this.lblTituloLogo.Name = "lblTituloLogo";
            this.lblTituloLogo.Size = new System.Drawing.Size(230, 40);
            this.lblTituloLogo.TabIndex = 0;
            this.lblTituloLogo.Text = "🍸 ICE BAR SYSTEM";
            this.lblTituloLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(241)))));
            this.BarraTitulo.Controls.Add(this.lblModuloActual);
            this.BarraTitulo.Controls.Add(this.lblReloj);
            this.BarraTitulo.Controls.Add(this.lblUsuarioInfo);
            this.BarraTitulo.Controls.Add(this.iconminimizar);
            this.BarraTitulo.Controls.Add(this.iconrestaurar);
            this.BarraTitulo.Controls.Add(this.iconmaximizar);
            this.BarraTitulo.Controls.Add(this.iconcerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(250, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(1030, 45);
            this.BarraTitulo.TabIndex = 1;
            this.BarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraTitulo_MouseDown);
            // 
            // lblModuloActual
            // 
            this.lblModuloActual.AutoSize = true;
            this.lblModuloActual.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblModuloActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(41)))), ((int)(((byte)(68)))));
            this.lblModuloActual.Location = new System.Drawing.Point(15, 12);
            this.lblModuloActual.Name = "lblModuloActual";
            this.lblModuloActual.Size = new System.Drawing.Size(84, 20);
            this.lblModuloActual.TabIndex = 0;
            this.lblModuloActual.Text = "Dashboard";
            // 
            // lblReloj
            // 
            this.lblReloj.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblReloj.AutoSize = true;
            this.lblReloj.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblReloj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(100)))));
            this.lblReloj.Location = new System.Drawing.Point(540, 14);
            this.lblReloj.Name = "lblReloj";
            this.lblReloj.Size = new System.Drawing.Size(130, 17);
            this.lblReloj.TabIndex = 1;
            this.lblReloj.Text = "28/10/2026 00:00:00";
            // 
            // lblUsuarioInfo
            // 
            this.lblUsuarioInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUsuarioInfo.AutoSize = true;
            this.lblUsuarioInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsuarioInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(41)))), ((int)(((byte)(68)))));
            this.lblUsuarioInfo.Location = new System.Drawing.Point(710, 14);
            this.lblUsuarioInfo.Name = "lblUsuarioInfo";
            this.lblUsuarioInfo.Size = new System.Drawing.Size(170, 17);
            this.lblUsuarioInfo.TabIndex = 2;
            this.lblUsuarioInfo.Text = "👤 Usuario | Administrador";
            // 
            // iconminimizar
            // 
            this.iconminimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconminimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconminimizar.FlatAppearance.BorderSize = 0;
            this.iconminimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconminimizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconminimizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.iconminimizar.Location = new System.Drawing.Point(895, 6);
            this.iconminimizar.Name = "iconminimizar";
            this.iconminimizar.Size = new System.Drawing.Size(32, 32);
            this.iconminimizar.TabIndex = 3;
            this.iconminimizar.Text = "—";
            this.iconminimizar.UseVisualStyleBackColor = true;
            this.iconminimizar.Click += new System.EventHandler(this.iconminimizar_Click);
            // 
            // iconrestaurar
            // 
            this.iconrestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconrestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconrestaurar.FlatAppearance.BorderSize = 0;
            this.iconrestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconrestaurar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconrestaurar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.iconrestaurar.Location = new System.Drawing.Point(938, 6);
            this.iconrestaurar.Name = "iconrestaurar";
            this.iconrestaurar.Size = new System.Drawing.Size(32, 32);
            this.iconrestaurar.TabIndex = 4;
            this.iconrestaurar.Text = "❐";
            this.iconrestaurar.UseVisualStyleBackColor = true;
            this.iconrestaurar.Visible = false;
            this.iconrestaurar.Click += new System.EventHandler(this.iconrestaurar_Click);
            // 
            // iconmaximizar
            // 
            this.iconmaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconmaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconmaximizar.FlatAppearance.BorderSize = 0;
            this.iconmaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconmaximizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconmaximizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.iconmaximizar.Location = new System.Drawing.Point(938, 6);
            this.iconmaximizar.Name = "iconmaximizar";
            this.iconmaximizar.Size = new System.Drawing.Size(32, 32);
            this.iconmaximizar.TabIndex = 5;
            this.iconmaximizar.Text = "⬜";
            this.iconmaximizar.UseVisualStyleBackColor = true;
            this.iconmaximizar.Click += new System.EventHandler(this.iconmaximizar_Click);
            // 
            // iconcerrar
            // 
            this.iconcerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconcerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconcerrar.FlatAppearance.BorderSize = 0;
            this.iconcerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconcerrar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.iconcerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.iconcerrar.Location = new System.Drawing.Point(980, 6);
            this.iconcerrar.Name = "iconcerrar";
            this.iconcerrar.Size = new System.Drawing.Size(35, 32);
            this.iconcerrar.TabIndex = 6;
            this.iconcerrar.Text = "✕";
            this.iconcerrar.UseVisualStyleBackColor = true;
            this.iconcerrar.Click += new System.EventHandler(this.iconcerrar_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(250, 45);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1030, 675);
            this.panelContenedor.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.MenuVertical);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICE BAR SYSTEM";
            this.MenuVertical.ResumeLayout(false);
            this.SubmenuVentas.ResumeLayout(false);
            this.SubmenuMantenedor.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuVertical;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblTituloLogo;
        private System.Windows.Forms.Label lblSubtituloLogo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnMantenedor;
        private System.Windows.Forms.Panel SubmenuMantenedor;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnConsultaStock;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Panel SubmenuVentas;
        private System.Windows.Forms.Button btnRegistrarVenta;
        private System.Windows.Forms.Button btnHistorialVentas;
        private System.Windows.Forms.Button btnIncidencias;
        private System.Windows.Forms.Button btnConfigConexion;
        private System.Windows.Forms.Button btnCerrSes;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.Label lblModuloActual;
        private System.Windows.Forms.Label lblReloj;
        private System.Windows.Forms.Label lblUsuarioInfo;
        private System.Windows.Forms.Button iconminimizar;
        private System.Windows.Forms.Button iconrestaurar;
        private System.Windows.Forms.Button iconmaximizar;
        private System.Windows.Forms.Button iconcerrar;
        private System.Windows.Forms.Panel panelContenedor;
    }
}

