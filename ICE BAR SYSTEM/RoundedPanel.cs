using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#pragma warning disable WFO1000

namespace IceBarSystem
{
    public class RoundedPanel : Panel
    {
        private int borderRadius = 10;
        private Color borderColor = Color.FromArgb(222, 226, 230);
        private int borderWidth = 1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(10)]
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; this.Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; this.Invalidate(); }
        }

        public RoundedPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Width <= 0 || this.Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
                int radius = Math.Max(1, borderRadius * 2);
                if (radius > bounds.Width) radius = bounds.Width;
                if (radius > bounds.Height) radius = bounds.Height;

                path.AddArc(bounds.Left, bounds.Top, radius, radius, 180, 90);
                path.AddArc(bounds.Right - radius, bounds.Top, radius, radius, 270, 90);
                path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(bounds.Left, bounds.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                this.Region = new Region(path);

                // Dibujar borde
                if (borderWidth > 0)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen pen = new Pen(borderColor, borderWidth))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            }
        }
    }
}
#pragma warning restore WFO1000
