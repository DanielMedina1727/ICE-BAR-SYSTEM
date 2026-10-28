using System;
using System.Drawing;
using System.Windows.Forms;

namespace IceBarSystem.UI
{
    /// <summary>
    /// Tema visual y formato estandarizado estilo xdnt123 para ICE BAR SYSTEM
    /// </summary>
    public static class Theme
    {
        // ========================================================
        // COLORES FORMATO XDNT123
        // ========================================================
        public static readonly Color Navy             = Color.Navy;
        public static readonly Color SidebarNavy       = Color.FromArgb(4, 41, 68);
        public static readonly Color SidebarHover      = Color.FromArgb(45, 45, 48);
        public static readonly Color SidebarSubmenu    = Color.FromArgb(8, 30, 52);
        public static readonly Color BarraTituloBg     = Color.FromArgb(232, 236, 241);

        public static readonly Color MainBg            = Color.FromArgb(240, 244, 247);
        public static readonly Color CardBg            = Color.White;
        public static readonly Color PanelBorder       = Color.FromArgb(222, 226, 230);

        // Botones de acción estándar xdnt123
        public static readonly Color BtnBlue           = Color.FromArgb(52, 152, 219);  // Nuevo / Buscar / Info
        public static readonly Color BtnGreen          = Color.FromArgb(46, 204, 113);  // Guardar / Confirmar / Venta
        public static readonly Color BtnYellow         = Color.FromArgb(241, 196, 15);  // Limpiar / Modificar
        public static readonly Color BtnRed            = Color.FromArgb(231, 76, 60);   // Eliminar / Quitar / Cancelar
        public static readonly Color BtnLogin          = Color.FromArgb(0, 102, 204);   // Botón principal Login
        public static readonly Color BtnPurple         = Color.FromArgb(156, 39, 176);  // Auditoría / Incidencias

        // Textos
        public static readonly Color TextDark          = Color.FromArgb(33, 37, 41);
        public static readonly Color TextMuted         = Color.FromArgb(108, 117, 125);
        public static readonly Color TextWhite         = Color.White;

        // ========================================================
        // FUENTES ESTÁNDAR
        // ========================================================
        public static readonly Font TitleFont          = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
        public static readonly Font SubtitleFont       = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font ButtonFont         = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        public static readonly Font BodyBoldFont       = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        public static readonly Font BodyFont           = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        public static readonly Font SmallFont          = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        public static readonly Font TotalFont          = new Font("Segoe UI", 20F, FontStyle.Bold);

        // ========================================================
        // HELPERS DE ESTILO
        // ========================================================

        public static void StyleButton(Button btn, Color bgColor, Color textColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bgColor;
            btn.ForeColor = textColor;
            btn.Font = ButtonFont;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = ControlPaint.Dark(bgColor, 0.08f);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = bgColor;
            };
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 235, 240);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Encabezado
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Filas
            dgv.RowTemplate.Height = 32;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = TextDark;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = BtnBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Filas alternadas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = BtnBlue;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }

        public static void SetPlaceholder(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || textBox.Text == placeholder)
            {
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray;
            }

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = TextDark;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }
    }
}
