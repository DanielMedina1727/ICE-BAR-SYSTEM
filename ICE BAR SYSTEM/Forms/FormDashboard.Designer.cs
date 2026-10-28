namespace IceBarSystem.Forms
{
    partial class FormDashboard
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelTablas = new System.Windows.Forms.TableLayoutPanel();
            this.panelTablaVentas = new IceBarSystem.RoundedPanel();
            this.dgvUltimasVentas = new System.Windows.Forms.DataGridView();
            this.lblTituloVentas = new System.Windows.Forms.Label();
            this.panelTablaStock = new IceBarSystem.RoundedPanel();
            this.dgvBajoStock = new System.Windows.Forms.DataGridView();
            this.lblTituloStock = new System.Windows.Forms.Label();
            this.panelTarjetas = new System.Windows.Forms.TableLayoutPanel();
            this.cardIncidencias = new IceBarSystem.RoundedPanel();
            this.lblValIncidencias = new System.Windows.Forms.Label();
            this.lblTitIncidencias = new System.Windows.Forms.Label();
            this.cardProductos = new IceBarSystem.RoundedPanel();
            this.lblValProductos = new System.Windows.Forms.Label();
            this.lblTitProductos = new System.Windows.Forms.Label();
            this.cardTickets = new IceBarSystem.RoundedPanel();
            this.lblValTickets = new System.Windows.Forms.Label();
            this.lblTitTickets = new System.Windows.Forms.Label();
            this.cardVentasHoy = new IceBarSystem.RoundedPanel();
            this.lblValVentasHoy = new System.Windows.Forms.Label();
            this.lblTitVentasHoy = new System.Windows.Forms.Label();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTablas.SuspendLayout();
            this.panelTablaVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimasVentas)).BeginInit();
            this.panelTablaStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBajoStock)).BeginInit();
            this.panelTarjetas.SuspendLayout();
            this.cardIncidencias.SuspendLayout();
            this.cardProductos.SuspendLayout();
            this.cardTickets.SuspendLayout();
            this.cardVentasHoy.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Navy;
            this.panelHeader.Controls.Add(this.btnCerrar);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1030, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(990, 8);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(32, 32);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "✕";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(350, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📊 PANEL DE CONTROL - DASHBOARD";
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.panelMain.Controls.Add(this.panelTablas);
            this.panelMain.Controls.Add(this.panelTarjetas);
            this.panelMain.Controls.Add(this.panelAcciones);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 50);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(15);
            this.panelMain.Size = new System.Drawing.Size(1030, 625);
            this.panelMain.TabIndex = 1;
            // 
            // panelTablas
            // 
            this.panelTablas.ColumnCount = 2;
            this.panelTablas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelTablas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelTablas.Controls.Add(this.panelTablaVentas, 1, 0);
            this.panelTablas.Controls.Add(this.panelTablaStock, 0, 0);
            this.panelTablas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTablas.Location = new System.Drawing.Point(15, 175);
            this.panelTablas.Name = "panelTablas";
            this.panelTablas.RowCount = 1;
            this.panelTablas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelTablas.Size = new System.Drawing.Size(1000, 435);
            this.panelTablas.TabIndex = 2;
            // 
            // panelTablaVentas
            // 
            this.panelTablaVentas.BackColor = System.Drawing.Color.White;
            this.panelTablaVentas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.panelTablaVentas.BorderRadius = 8;
            this.panelTablaVentas.BorderWidth = 1;
            this.panelTablaVentas.Controls.Add(this.dgvUltimasVentas);
            this.panelTablaVentas.Controls.Add(this.lblTituloVentas);
            this.panelTablaVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTablaVentas.Location = new System.Drawing.Point(508, 3);
            this.panelTablaVentas.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.panelTablaVentas.Name = "panelTablaVentas";
            this.panelTablaVentas.Padding = new System.Windows.Forms.Padding(12);
            this.panelTablaVentas.Size = new System.Drawing.Size(489, 429);
            this.panelTablaVentas.TabIndex = 1;
            // 
            // dgvUltimasVentas
            // 
            this.dgvUltimasVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUltimasVentas.Location = new System.Drawing.Point(12, 42);
            this.dgvUltimasVentas.Name = "dgvUltimasVentas";
            this.dgvUltimasVentas.Size = new System.Drawing.Size(465, 375);
            this.dgvUltimasVentas.TabIndex = 1;
            // 
            // lblTituloVentas
            // 
            this.lblTituloVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloVentas.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTituloVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(41)))), ((int)(((byte)(68)))));
            this.lblTituloVentas.Location = new System.Drawing.Point(12, 12);
            this.lblTituloVentas.Name = "lblTituloVentas";
            this.lblTituloVentas.Size = new System.Drawing.Size(465, 30);
            this.lblTituloVentas.TabIndex = 0;
            this.lblTituloVentas.Text = "🧾 Últimas Ventas Registradas";
            // 
            // panelTablaStock
            // 
            this.panelTablaStock.BackColor = System.Drawing.Color.White;
            this.panelTablaStock.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.panelTablaStock.BorderRadius = 8;
            this.panelTablaStock.BorderWidth = 1;
            this.panelTablaStock.Controls.Add(this.dgvBajoStock);
            this.panelTablaStock.Controls.Add(this.lblTituloStock);
            this.panelTablaStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTablaStock.Location = new System.Drawing.Point(3, 3);
            this.panelTablaStock.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.panelTablaStock.Name = "panelTablaStock";
            this.panelTablaStock.Padding = new System.Windows.Forms.Padding(12);
            this.panelTablaStock.Size = new System.Drawing.Size(489, 429);
            this.panelTablaStock.TabIndex = 0;
            // 
            // dgvBajoStock
            // 
            this.dgvBajoStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBajoStock.Location = new System.Drawing.Point(12, 42);
            this.dgvBajoStock.Name = "dgvBajoStock";
            this.dgvBajoStock.Size = new System.Drawing.Size(465, 375);
            this.dgvBajoStock.TabIndex = 1;
            // 
            // lblTituloStock
            // 
            this.lblTituloStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloStock.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTituloStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTituloStock.Location = new System.Drawing.Point(12, 12);
            this.lblTituloStock.Name = "lblTituloStock";
            this.lblTituloStock.Size = new System.Drawing.Size(465, 30);
            this.lblTituloStock.TabIndex = 0;
            this.lblTituloStock.Text = "⚠️ Alerta: Productos con Menor Stock";
            // 
            // panelTarjetas
            // 
            this.panelTarjetas.ColumnCount = 4;
            this.panelTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelTarjetas.Controls.Add(this.cardIncidencias, 3, 0);
            this.panelTarjetas.Controls.Add(this.cardProductos, 2, 0);
            this.panelTarjetas.Controls.Add(this.cardTickets, 1, 0);
            this.panelTarjetas.Controls.Add(this.cardVentasHoy, 0, 0);
            this.panelTarjetas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTarjetas.Location = new System.Drawing.Point(15, 60);
            this.panelTarjetas.Name = "panelTarjetas";
            this.panelTarjetas.RowCount = 1;
            this.panelTarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelTarjetas.Size = new System.Drawing.Size(1000, 115);
            this.panelTarjetas.TabIndex = 1;
            // 
            // cardIncidencias
            // 
            this.cardIncidencias.BackColor = System.Drawing.Color.White;
            this.cardIncidencias.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.cardIncidencias.BorderRadius = 8;
            this.cardIncidencias.BorderWidth = 2;
            this.cardIncidencias.Controls.Add(this.lblValIncidencias);
            this.cardIncidencias.Controls.Add(this.lblTitIncidencias);
            this.cardIncidencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardIncidencias.Location = new System.Drawing.Point(756, 3);
            this.cardIncidencias.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.cardIncidencias.Name = "cardIncidencias";
            this.cardIncidencias.Padding = new System.Windows.Forms.Padding(12);
            this.cardIncidencias.Size = new System.Drawing.Size(241, 109);
            this.cardIncidencias.TabIndex = 3;
            // 
            // lblValIncidencias
            // 
            this.lblValIncidencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValIncidencias.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValIncidencias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblValIncidencias.Location = new System.Drawing.Point(12, 37);
            this.lblValIncidencias.Name = "lblValIncidencias";
            this.lblValIncidencias.Size = new System.Drawing.Size(217, 60);
            this.lblValIncidencias.TabIndex = 1;
            this.lblValIncidencias.Text = "0";
            this.lblValIncidencias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitIncidencias
            // 
            this.lblTitIncidencias.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitIncidencias.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitIncidencias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitIncidencias.Location = new System.Drawing.Point(12, 12);
            this.lblTitIncidencias.Name = "lblTitIncidencias";
            this.lblTitIncidencias.Size = new System.Drawing.Size(217, 25);
            this.lblTitIncidencias.TabIndex = 0;
            this.lblTitIncidencias.Text = "⚠️ Incidencias Activas";
            this.lblTitIncidencias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardProductos
            // 
            this.cardProductos.BackColor = System.Drawing.Color.White;
            this.cardProductos.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.cardProductos.BorderRadius = 8;
            this.cardProductos.BorderWidth = 2;
            this.cardProductos.Controls.Add(this.lblValProductos);
            this.cardProductos.Controls.Add(this.lblTitProductos);
            this.cardProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardProductos.Location = new System.Drawing.Point(506, 3);
            this.cardProductos.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cardProductos.Name = "cardProductos";
            this.cardProductos.Padding = new System.Windows.Forms.Padding(12);
            this.cardProductos.Size = new System.Drawing.Size(238, 109);
            this.cardProductos.TabIndex = 2;
            // 
            // lblValProductos
            // 
            this.lblValProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValProductos.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblValProductos.Location = new System.Drawing.Point(12, 37);
            this.lblValProductos.Name = "lblValProductos";
            this.lblValProductos.Size = new System.Drawing.Size(214, 60);
            this.lblValProductos.TabIndex = 1;
            this.lblValProductos.Text = "0";
            this.lblValProductos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitProductos
            // 
            this.lblTitProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitProductos.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitProductos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitProductos.Location = new System.Drawing.Point(12, 12);
            this.lblTitProductos.Name = "lblTitProductos";
            this.lblTitProductos.Size = new System.Drawing.Size(214, 25);
            this.lblTitProductos.TabIndex = 0;
            this.lblTitProductos.Text = "📦 Total Productos";
            this.lblTitProductos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardTickets
            // 
            this.cardTickets.BackColor = System.Drawing.Color.White;
            this.cardTickets.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.cardTickets.BorderRadius = 8;
            this.cardTickets.BorderWidth = 2;
            this.cardTickets.Controls.Add(this.lblValTickets);
            this.cardTickets.Controls.Add(this.lblTitTickets);
            this.cardTickets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTickets.Location = new System.Drawing.Point(256, 3);
            this.cardTickets.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.cardTickets.Name = "cardTickets";
            this.cardTickets.Padding = new System.Windows.Forms.Padding(12);
            this.cardTickets.Size = new System.Drawing.Size(238, 109);
            this.cardTickets.TabIndex = 1;
            // 
            // lblValTickets
            // 
            this.lblValTickets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValTickets.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValTickets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblValTickets.Location = new System.Drawing.Point(12, 37);
            this.lblValTickets.Name = "lblValTickets";
            this.lblValTickets.Size = new System.Drawing.Size(214, 60);
            this.lblValTickets.TabIndex = 1;
            this.lblValTickets.Text = "0";
            this.lblValTickets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitTickets
            // 
            this.lblTitTickets.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitTickets.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitTickets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitTickets.Location = new System.Drawing.Point(12, 12);
            this.lblTitTickets.Name = "lblTitTickets";
            this.lblTitTickets.Size = new System.Drawing.Size(214, 25);
            this.lblTitTickets.TabIndex = 0;
            this.lblTitTickets.Text = "🧾 Tickets Emitidos Hoy";
            this.lblTitTickets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardVentasHoy
            // 
            this.cardVentasHoy.BackColor = System.Drawing.Color.White;
            this.cardVentasHoy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.cardVentasHoy.BorderRadius = 8;
            this.cardVentasHoy.BorderWidth = 2;
            this.cardVentasHoy.Controls.Add(this.lblValVentasHoy);
            this.cardVentasHoy.Controls.Add(this.lblTitVentasHoy);
            this.cardVentasHoy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardVentasHoy.Location = new System.Drawing.Point(3, 3);
            this.cardVentasHoy.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.cardVentasHoy.Name = "cardVentasHoy";
            this.cardVentasHoy.Padding = new System.Windows.Forms.Padding(12);
            this.cardVentasHoy.Size = new System.Drawing.Size(241, 109);
            this.cardVentasHoy.TabIndex = 0;
            // 
            // lblValVentasHoy
            // 
            this.lblValVentasHoy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValVentasHoy.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblValVentasHoy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblValVentasHoy.Location = new System.Drawing.Point(12, 37);
            this.lblValVentasHoy.Name = "lblValVentasHoy";
            this.lblValVentasHoy.Size = new System.Drawing.Size(217, 60);
            this.lblValVentasHoy.TabIndex = 1;
            this.lblValVentasHoy.Text = "$0.00";
            this.lblValVentasHoy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitVentasHoy
            // 
            this.lblTitVentasHoy.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitVentasHoy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitVentasHoy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTitVentasHoy.Location = new System.Drawing.Point(12, 12);
            this.lblTitVentasHoy.Name = "lblTitVentasHoy";
            this.lblTitVentasHoy.Size = new System.Drawing.Size(217, 25);
            this.lblTitVentasHoy.TabIndex = 0;
            this.lblTitVentasHoy.Text = "💵 Ventas de Hoy";
            this.lblTitVentasHoy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.btnActualizar);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAcciones.Location = new System.Drawing.Point(15, 15);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(1000, 45);
            this.panelAcciones.TabIndex = 0;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(840, 5);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(155, 35);
            this.btnActualizar.TabIndex = 0;
            this.btnActualizar.Text = "🔄 Actualizar Datos";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1030, 675);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDashboard";
            this.Text = "Dashboard";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelTablas.ResumeLayout(false);
            this.panelTablaVentas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimasVentas)).EndInit();
            this.panelTablaStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBajoStock)).EndInit();
            this.panelTarjetas.ResumeLayout(false);
            this.cardIncidencias.ResumeLayout(false);
            this.cardProductos.ResumeLayout(false);
            this.cardTickets.ResumeLayout(false);
            this.cardVentasHoy.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.TableLayoutPanel panelTarjetas;
        private IceBarSystem.RoundedPanel cardVentasHoy;
        private System.Windows.Forms.Label lblTitVentasHoy;
        private System.Windows.Forms.Label lblValVentasHoy;
        private IceBarSystem.RoundedPanel cardTickets;
        private System.Windows.Forms.Label lblTitTickets;
        private System.Windows.Forms.Label lblValTickets;
        private IceBarSystem.RoundedPanel cardProductos;
        private System.Windows.Forms.Label lblTitProductos;
        private System.Windows.Forms.Label lblValProductos;
        private IceBarSystem.RoundedPanel cardIncidencias;
        private System.Windows.Forms.Label lblTitIncidencias;
        private System.Windows.Forms.Label lblValIncidencias;
        private System.Windows.Forms.TableLayoutPanel panelTablas;
        private IceBarSystem.RoundedPanel panelTablaStock;
        private System.Windows.Forms.Label lblTituloStock;
        private System.Windows.Forms.DataGridView dgvBajoStock;
        private IceBarSystem.RoundedPanel panelTablaVentas;
        private System.Windows.Forms.Label lblTituloVentas;
        private System.Windows.Forms.DataGridView dgvUltimasVentas;
    }
}

