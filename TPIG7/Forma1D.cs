using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace TPIG7
{
    internal class Forma1D :Forma
    {
        private Pen pen;
        private int arrowStartX,
                arrowStartY,
                arrowEndX,
                arrowEndY;

        public int ArrowStartX { get => arrowStartX; set => arrowStartX = value; }
        public int ArrowStartY { get => arrowStartY; set => arrowStartY = value; }
        public int ArrowEndX { get => arrowEndX; set => arrowEndX = value; }
        public int ArrowEndY { get => arrowEndY; set => arrowEndY = value; }
        public Pen Pen { get => pen; set => pen = value; }

        public Forma1D(Pen pen, int arrowStartX, int arrowStartY, int arrowEndX, int arrowEndY)
        {
            Pen = pen;
            ArrowStartX = arrowStartX;
            ArrowStartY = arrowStartY;
            ArrowEndX = arrowEndX;
            ArrowEndY = arrowEndY;
        }

        public void Dibujar(Graphics g , Pen p)
        {
            g.DrawLine(p, arrowStartX,
                arrowStartY,
                arrowEndX,
                arrowEndY);
        }

        public void DibujarTexto(Graphics g, Pen p,string texto)
        {

        }
    }

}
