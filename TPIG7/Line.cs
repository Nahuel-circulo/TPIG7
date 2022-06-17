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

        private int arrowPositionXStart, arrowPositionXEnd, arrowPostionYStart, arrowPostionYEnd;
        
        private System.Drawing.Pen lapiz;
        
        public Line(int arrowPositionXStart, int arrowPositionXEnd, int arrowPostionYStart, int arrowPostionYEnd, Pen lapiz)
        {
            this.ArrowPositionXStart = arrowPositionXStart;
            this.ArrowPositionXEnd = arrowPositionXEnd;
            this.ArrowPostionYStart = arrowPostionYStart;
            this.ArrowPostionYEnd = arrowPostionYEnd;
            this.Lapiz = lapiz;
        }

        public int ArrowPositionXStart { get => arrowPositionXStart; set => arrowPositionXStart = value; }
        public int ArrowPositionXEnd { get => arrowPositionXEnd; set => arrowPositionXEnd = value; }
        public int ArrowPostionYStart { get => arrowPostionYStart; set => arrowPostionYStart = value; }
        public int ArrowPostionYEnd { get => arrowPostionYEnd; set => arrowPostionYEnd = value; }
        public Pen Lapiz { get => lapiz; set => lapiz = value; }
    }
}
