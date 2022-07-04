using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace WinFormsApp1
{
    internal class MyForma : PictureBox
    {


        private int borderSize = 3;
        private int tipo;
        private TextBox text = new TextBox();
        public MyForma(int width, int height, int tipe)
        {
            tipo = tipe;
            this.BackColor = Color.Transparent;
            this.Size = new Size(width, height);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            text.Text = "";
            text.Multiline = true;

            if (tipe == 1)
            {
                text.Width = (this.Size.Width - 20) - ((int)(this.Size.Width * 0.3));
                text.Height = (this.Size.Height - 20) - ((int)(this.Size.Height * 0.3));
                text.Location = new Point((int)(this.Size.Width * 0.2), (int)(this.Size.Height * 0.2));
            }
            else
            {
                text.Location = new Point(10, 10);
                text.Width = this.Size.Width - 20;
                text.Height = this.Size.Height - 20;
            }

            text.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left;

            text.BorderStyle = BorderStyle.None;

            text.TextAlign = HorizontalAlignment.Center;

            ResizableControl tranformToResizable = new ResizableControl(text);

            this.Controls.Add(text);
        }

        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public int Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;
            
                this.Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Height);
            if (this.tipo == 1)
            {
                this.text.Width = (this.Size.Width - 20) - ((int)(this.Size.Width * 0.25));
                this.text.Height = (this.Size.Height - 20) - ((int)(this.Size.Height * 0.25));
                this.text.Location = new Point((int)(this.Size.Width * 0.2), (int)(this.Size.Height * 0.2));
            }
        }



        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var graphics = pe.Graphics;
            var contornoRectangulo = this.ClientRectangle;

            var bordeRectangulo = Rectangle.Inflate(contornoRectangulo, -borderSize, -borderSize);

            var smoothSize = borderSize > 0 ? borderSize * 3 : 1;

            using (var pathRegion = new GraphicsPath())
            using (var penSmooth = new Pen(Color.Transparent, smoothSize))
            using (var penBorder = new Pen(Color.Black, BorderSize))
            {

                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (tipo == 1)
                {
                    graphics.DrawEllipse(penBorder, bordeRectangulo);
                }
                else
                {
                    graphics.DrawRectangle(penBorder, bordeRectangulo);
                }
            }

        }
    }
}
