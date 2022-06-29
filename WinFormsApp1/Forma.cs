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
        Point inicio;
        Point fin;
        public Forma(int tipo, Rectangle rectangle)
        {
            this.tipo = tipo;
            this.rectangle = rectangle;
        }

        public int Tipo { get => tipo; set => tipo = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public string Texto { get => texto; set => texto = value; }
        public Point Inicio { get => inicio; set => inicio = value; }
        public Point Fin { get => fin; set => fin = value; }

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
    }
}
