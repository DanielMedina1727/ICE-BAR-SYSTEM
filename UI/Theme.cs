using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IceBarSystem.UI
{
    public static class Theme
    {
        // =========================================
        // PALETA DE COLORES PREMIUM "ICE BAR"
        // =========================================
        public static readonly Color BgDark      = Color.FromArgb(8,  15, 35);        // Fondo principal ultra oscuro
        public static readonly Color BgMid        = Color.FromArgb(12, 20, 45);        // Fondo secundario
        public static readonly Color CardBg        = Color.FromArgb(20, 30, 58);        // Paneles y tarjetas
        public static readonly Color CardDarker    = Color.FromArgb(14, 22, 44);        // Paneles oscuros
        public static readonly Color CardHover     = Color.FromArgb(28, 42, 72);        // Hover en tarjeta
        public static readonly Color SidebarBg     = Color.FromArgb(10, 18, 38);        // Barra lateral
        public static readonly Color SidebarActive = Color.FromArgb(0,  90, 110);       // Botón activo sidebar

        // Cian neón ICE
        public static readonly Color PrimaryCyan  = Color.FromArgb(0,   194, 203);
        public static readonly Color PrimaryHover  = Color.FromArgb(30,  220, 232);
        public static readonly Color BrightCyan    = Color.FromArgb(100, 255, 240);
        public static readonly Color DeepCyan      = Color.FromArgb(0,   140, 160);
        public static readonly Color GlowCyan      = Color.FromArgb(0,   220, 230);

        // Colores de texto
        public static readonly Color TextWhite    = Color.FromArgb(230, 242, 255);
        public static readonly Color TextMuted    = Color.FromArgb(100, 130, 170);
        public static readonly Color TextSoft     = Color.FromArgb(150, 180, 210);

        // Acentos
        public static readonly Color AccentSuccess = Color.FromArgb(16, 185, 129);
        public static readonly Color AccentWarning = Color.FromArgb(245, 158, 11);
        public static readonly Color AccentDanger  = Color.FromArgb(239, 68,  68);
        public static readonly Color AccentPurple  = Color.FromArgb(139, 92, 246);
        public static readonly Color AccentBlue    = Color.FromArgb(59, 130, 246);

        // Bordes
        public static readonly Color BorderColor   = Color.FromArgb(35, 55, 90);
        public static readonly Color BorderGlow    = Color.FromArgb(0, 194, 203, 60);

        // =========================================
        // FUENTES
        // =========================================
        public static readonly Font TitleFont      = new Font("Segoe UI", 15F, FontStyle.Bold);
        public static readonly Font SubtitleFont   = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font BodyBoldFont   = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font BodyFont       = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font SmallFont      = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        public static readonly Font SmallBoldFont  = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        public static readonly Font PriceFont      = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font MegaFont       = new Font("Segoe UI", 28F, FontStyle.Bold);

        // =========================================
        // HELPERS DE ESTILO DE CONTROLES
        // =========================================

        public static void StyleButton(Button btn, Color bgColor, Color textColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bgColor;
            btn.ForeColor = textColor;
            btn.Font = BodyBoldFont;
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(6, 4, 6, 4);

            btn.MouseEnter += (s, e) =>
            {
                if      (bgColor == PrimaryCyan)    btn.BackColor = PrimaryHover;
                else if (bgColor == CardBg)         btn.BackColor = CardHover;
                else if (bgColor == CardDarker)     btn.BackColor = Color.FromArgb(25, 38, 68);
                else if (bgColor == AccentDanger)   btn.BackColor = Color.FromArgb(220, 50, 50);
                else if (bgColor == AccentSuccess)  btn.BackColor = Color.FromArgb(14, 160, 110);
                else if (bgColor == AccentWarning)  btn.BackColor = Color.FromArgb(220, 135, 8);
                else if (bgColor == AccentPurple)   btn.BackColor = Color.FromArgb(120, 72, 220);
                else if (bgColor == AccentBlue)     btn.BackColor = Color.FromArgb(40, 110, 225);
                else                                btn.BackColor = ControlPaint.Light(bgColor, 0.1f);
            };

            btn.MouseLeave += (s, e) => btn.BackColor = bgColor;
        }

        public static void StyleButtonGradient(Button btn, Color from, Color to, Color textColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = textColor;
            btn.Font = BodyBoldFont;
            btn.Cursor = Cursors.Hand;

            btn.Paint += (s, pe) =>
            {
                using var br = new LinearGradientBrush(btn.ClientRectangle, from, to, LinearGradientMode.Horizontal);
                pe.Graphics.FillRectangle(br, btn.ClientRectangle);
                var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(pe.Graphics, btn.Text, btn.Font, btn.ClientRectangle, textColor, flags);
            };
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = CardDarker;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(30, 48, 75);

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 18, 40);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = BrightCyan;
            grid.ColumnHeadersDefaultCellStyle.Font = BodyBoldFont;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            grid.ColumnHeadersHeight = 40;

            grid.RowTemplate.Height = 38;
            grid.DefaultCellStyle.BackColor = CardBg;
            grid.DefaultCellStyle.ForeColor = TextWhite;
            grid.DefaultCellStyle.Font = BodyFont;
            grid.DefaultCellStyle.SelectionBackColor = SidebarActive;
            grid.DefaultCellStyle.SelectionForeColor = BrightCyan;
            grid.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(16, 26, 50);
            grid.AlternatingRowsDefaultCellStyle.ForeColor = TextWhite;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = SidebarActive;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = BrightCyan;

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public static void StyleTextBox(TextBox txt)
        {
            txt.BackColor = CardDarker;
            txt.ForeColor = TextWhite;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = BodyFont;
        }

        public static void StyleComboBox(ComboBox cmb)
        {
            cmb.BackColor = CardDarker;
            cmb.ForeColor = TextWhite;
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Font = BodyFont;
        }

        public static void StyleNumericUpDown(NumericUpDown num)
        {
            num.BackColor = CardDarker;
            num.ForeColor = BrightCyan;
            num.BorderStyle = BorderStyle.FixedSingle;
            num.Font = BodyBoldFont;
        }

        public static void StyleDatePicker(DateTimePicker dtp)
        {
            dtp.CalendarMonthBackground = CardBg;
            dtp.CalendarForeColor = TextWhite;
            dtp.CalendarTitleBackColor = DeepCyan;
            dtp.CalendarTitleForeColor = TextWhite;
            dtp.Font = BodyFont;
        }

        // =========================================
        // BOTÓN DE CONTROL DE VENTANA (xdnt123 style)
        // =========================================
        public static Button CreateWindowControlButton(string text, Color hoverBg, Color hoverFg, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = TextMuted,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(40, 32),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hoverBg;
            btn.MouseEnter += (s, e) => btn.ForeColor = hoverFg;
            btn.MouseLeave += (s, e) => btn.ForeColor = TextMuted;
            btn.Click += onClick;
            return btn;
        }

        // =========================================
        // TARJETA DE ESTADÍSTICA (KPI Card)
        // =========================================
        public static Panel CreateStatCard(string titulo, string valor, string emoji, Color accentColor)
        {
            var card = new Panel
            {
                Size = new Size(180, 90),
                BackColor = CardBg,
                Padding = new Padding(12)
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // Borde izquierdo de acento
                using var brush = new SolidBrush(accentColor);
                e.Graphics.FillRectangle(brush, 0, 0, 4, card.Height);
                // Borde perimetral sutil
                using var pen = new Pen(Color.FromArgb(40, 60, 95), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            var lblEmoji = new Label
            {
                Text = emoji,
                Font = new Font("Segoe UI Emoji", 20F),
                Location = new Point(135, 10),
                Size = new Size(42, 42),
                ForeColor = accentColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblVal = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(12, 12),
                AutoSize = true
            };

            var lblTit = new Label
            {
                Text = titulo,
                Font = SmallFont,
                ForeColor = TextMuted,
                Location = new Point(12, 60),
                AutoSize = true
            };

            card.Controls.Add(lblEmoji);
            card.Controls.Add(lblVal);
            card.Controls.Add(lblTit);
            return card;
        }

        // =========================================
        // SEPARADOR DECORATIVO
        // =========================================
        public static Panel CreateSeparator(bool horizontal = true)
        {
            var sep = new Panel
            {
                BackColor = BorderColor
            };
            if (horizontal)
            {
                sep.Dock = DockStyle.Top;
                sep.Height = 1;
            }
            else
            {
                sep.Dock = DockStyle.Left;
                sep.Width = 1;
            }
            return sep;
        }

        // =========================================
        // BADGE DE ESTADO (pill label)
        // =========================================
        public static Label CreateBadge(string text, Color bgColor)
        {
            var lbl = new Label
            {
                Text = $"  {text}  ",
                Font = SmallBoldFont,
                ForeColor = Color.White,
                BackColor = bgColor,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(4, 2, 4, 2)
            };
            return lbl;
        }

        // =========================================
        // PANEL CON BORDE GLOW CIAN
        // =========================================
        public static void AddGlowBorder(Panel pnl, Color glowColor)
        {
            pnl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var pen = new Pen(glowColor, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };
        }
    }
}
