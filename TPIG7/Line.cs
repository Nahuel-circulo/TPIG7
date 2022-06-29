using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIG7
{
    internal class Line
    {

        private int arrowPositionXStart;
        private int arrowPositionXEnd;
        private int arrowPositionYStart;
        private int arrowPositionYEnd;
        public Point startLinePoint;
        public Point endLinePoint;
        private System.Drawing.Pen lapiz;



        public Line(int xStart, int xEnd, int yStart, int yEnd, Pen pen)
        {

            this.ArrowPositionXStart = xStart;
            this.ArrowPositionXEnd = xEnd;
            this.ArrowPositionYStart = yStart;
            this.ArrowPositionYEnd = yEnd;

            startLinePoint = new Point(ArrowPositionXStart, ArrowPositionYStart);
            endLinePoint = new Point(ArrowPositionXEnd, ArrowPositionYEnd);
            this.Lapiz = (Pen)pen.Clone();
        }


        public Pen Lapiz { get => lapiz; set => lapiz = value; }
        public int ArrowPositionXStart { get => arrowPositionXStart; set => arrowPositionXStart = value; }
        public int ArrowPositionXEnd { get => arrowPositionXEnd; set => arrowPositionXEnd = value; }
        public int ArrowPositionYStart { get => arrowPositionYStart; set => arrowPositionYStart = value; }
        public int ArrowPositionYEnd { get => arrowPositionYEnd; set => arrowPositionYEnd = value; }

        public void dibujar(Graphics g)
        {
            g.DrawLine(lapiz,
               ArrowPositionXStart,
               ArrowPositionYStart,
               ArrowPositionXEnd,
               ArrowPositionYEnd);
        }
    }
}
