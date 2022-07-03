using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinFormsApp1
{
    internal class Forma: Figuras
    {
        int tipo;
        Rectangle rectangle;
        string texto;
      
        public Forma(int tipo, Rectangle rectangle)
        {
            this.tipo = tipo;
            this.rectangle = rectangle;
        }

        public Forma(Poco p)
        {
            tipo = p.Tipo;
            rectangle = new Rectangle(p.Punto.X,p.Punto.Y, p.Ancho, p.Alto);
        }

        public int Tipo { get => tipo; set => tipo = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public string Texto { get => texto; set => texto = value; }
  

        public void Draw(ref Graphics g, Pen p)
        {
            if(tipo == 0)
            {
                g.DrawRectangle(p, rectangle);

            }
            else
            {
                g.DrawEllipse(p, rectangle);
            }
            
        }
        public Poco GetPOCO()
        {
            Poco p = new Poco();
            p.Tipo = tipo;
            p.Punto = rectangle.Location;
            p.Ancho = rectangle.Width;
            p.Alto = rectangle.Height;
            return p;
        }
    }
}
