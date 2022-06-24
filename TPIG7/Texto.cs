using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TPIG7
{
    internal class Texto
    {
        private string palabras;
        private Font font;
        private Brush brush;
        StringFormat s;

        public Texto(string palabras, Font font, Brush brush)
        {
            this.palabras = palabras;
            this.font = font;
            this.brush = brush;
            s = new StringFormat();
            s.Alignment = StringAlignment.Center;
            s.LineAlignment = StringAlignment.Center;
        }

        public Font Font { get => font; set => font = value; }
        public string Palabras { get => palabras; set => palabras = value; }
        public Brush Brush { get => brush; set => brush = value; }

        public void DibujarTexto(Graphics g, Pen p, Rectangle rectangle)
        {
            g.DrawString(palabras, font, brush, rectangle,s);
        }



    }

}
