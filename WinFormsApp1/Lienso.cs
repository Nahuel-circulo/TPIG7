using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.Text.Json;
using System.Diagnostics;

namespace WinFormsApp1
{
    internal class Lienso
    {
        List<Figuras> figuras;

        Graphics g;
        Bitmap bitmap;
        Pen pen;
        Brush brush;
        Font font;
        Point puntoDeEsquina;
        PictureBox pictureBox;
        StringFormat stringFormat;


        public Lienso(ref Graphics graphics, ref PictureBox p, ref Bitmap b)
        {
            g = graphics;
            pictureBox = p;
            bitmap = b;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            pictureBox.Image = bitmap;
            pen = new Pen(Color.Black, 3);
            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Figuras = new List<Figuras>();
            brush = new SolidBrush(Color.Black);
            font = new Font(new FontFamily("arial"), 16);
        }


        public void ResizeLienzo(int width, int height)
        {
            if (width != 0 && height != 0)
            {

                this.pictureBox.Width = width;
                this.pictureBox.Height = height;
                this.bitmap = new Bitmap(width, height);
                pictureBox.Image = bitmap;
                g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                Drawing();
            }
        }
        public Point PuntoDeEsquina { get => puntoDeEsquina; set => puntoDeEsquina = value; }
        public Pen Pen { get => pen; set => pen = value; }


        internal List<Figuras> Figuras { get => figuras; set => figuras = value; }

        public void SetTipoDeFlecha(int i)
        {

            if (i == 2)
            {
                pen.StartCap = LineCap.Flat;
                pen.EndCap = LineCap.ArrowAnchor;
            }
            if (i == 3)
            {
                pen.StartCap = LineCap.ArrowAnchor;
                pen.EndCap = LineCap.ArrowAnchor;
            }
            if (i == 4)
            {
                pen.StartCap = LineCap.Flat;
                pen.EndCap = LineCap.Flat;
            }

        }

        public void PreViewRectangulo(Point p)
        {
            g.Clear(Color.White);
            g.DrawRectangle(pen, new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y));
            Drawing();
            pictureBox.Refresh();

        }
        public void PreViewCirculo(Point p)
        {
            g.Clear(Color.White);
            g.DrawEllipse(pen, new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y));
            Drawing();
            pictureBox.Refresh();
        }

        public void PreViewFlecha(Point p)
        {
            g.Clear(Color.White);
            g.DrawLine(Pen, PuntoDeEsquina, p);
            Drawing();
            pictureBox.Refresh();

        }
        public void GuardarFigura(Point p, int tipo)
        {
            g.Clear(Color.White);
            Rectangle r;
            Forma forma;
            Flecha flecha;
            switch (tipo)
            {
                case 0:
                    r = new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y);
                    forma = new Forma(tipo, r, stringFormat, font, brush);
                    figuras.Add(forma);

                    break;
                case 1:
                    r = new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y);
                    forma = new Forma(tipo, r, stringFormat, font, brush);
                    figuras.Add(forma);

                    break;
                case 2:
                    flecha = new Flecha(tipo, puntoDeEsquina, p);
                    figuras.Add(flecha);

                    break;
                case 3:
                    flecha = new Flecha(tipo, puntoDeEsquina, p);
                    figuras.Add(flecha);

                    break;
                case 4:
                    flecha = new Flecha(tipo, puntoDeEsquina, p);
                    figuras.Add(flecha);

                    break;
            }
            Drawing();
        }

        private void Drawing()
        {
            foreach (var intem in figuras)
            {
                intem.Draw(ref g, pen);
            }

            pictureBox.Refresh();
        }

        public void ReDraing()
        {
            g.Clear(Color.White);
            foreach (var intem in figuras)
            {
                intem.Draw(ref g, pen);
            }

            pictureBox.Refresh();
        }

        public void SetPoco(List<Poco> p)
        {
            figuras.Clear();
            foreach (var item in p)
            {
                if (item.Tipo == 0 || item.Tipo == 1)
                {
                    figuras.Add(new Forma(item, stringFormat, font, brush));
                }
                else
                {
                    figuras.Add(new Flecha(item));
                }
            }
        }

        public void Clear()
        {
            g.Clear(Color.White);
            figuras.Clear();
        }

        public bool cambios()
        {
            if (figuras.Count == 0)
            {
                return false;
            }
            return true;
        }
        public void remover(Figuras f)
        {
            figuras.Remove(f);
        }

        public void Deshacer()
        {
            figuras.Remove(figuras.Last());

        }


        public void getFigura(MyForma f)
        {
            figuras.Add(f.GetForma(stringFormat, brush));
            ReDraing();
        }
    }


}

