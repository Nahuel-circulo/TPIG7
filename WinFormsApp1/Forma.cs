using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace WinFormsApp1
{
    internal class Forma : Figuras 
    {
        int tipo;
        Rectangle rectangle;
        string texto;
        StringFormat stringFormat;
        Font font;
        Brush brush;


        public Forma(int tipo, Rectangle rectangle, StringFormat s,Font f , Brush b)
        {
            
            this.texto = "";
            this.tipo = tipo;
            this.rectangle = rectangle;
            this.stringFormat = s;
            this.font = f;
            this.brush = b;
        }

        public Forma(int tipo, Rectangle rectangle, StringFormat s, Font f, Brush b, string t)
        {

            this.texto = t;
            this.tipo = tipo;
            this.rectangle = rectangle;
            this.stringFormat = s;
            this.font = f;
            this.brush = b;
        }
        public Forma(Poco p, StringFormat s, Font f, Brush b)
        {
            tipo = p.Tipo;
            rectangle = new Rectangle(p.Punto.X, p.Punto.Y, p.Ancho, p.Alto);
            this.texto= p.Texto;
            this.stringFormat = s;
            this.font = f;
            this.brush = b;
        }

        public int Tipo { get => tipo; set => tipo = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public string Texto { get => texto; set => texto = value; }


     

        public void Draw(ref Graphics g, Pen p)
        {
            if (tipo == 0)
            {
                g.DrawRectangle(p, rectangle);

            }
            else
            {
                g.DrawEllipse(p, rectangle);
            }
            if (texto != "" || texto !=null)
            {
                g.DrawString(texto, font, brush, rectangle, stringFormat);
            }

        }
        public Poco GetPOCO()
        {
            Poco p = new Poco();
            p.Tipo = tipo;
            p.Texto = texto;
            p.Punto = rectangle.Location;
            p.Ancho = rectangle.Width;
            p.Alto = rectangle.Height;
            p.StringFormat = stringFormat;
            return p;
        }

        public bool Contiene(Point p)
        {
            return rectangle.Contains(p);
        }

        public Point Location()
        {
            return rectangle.Location;
        }

    }
}
