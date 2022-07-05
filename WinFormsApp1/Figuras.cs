using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinFormsApp1
{
    internal interface Figuras
    {
        

       


        void Draw(ref Graphics g, Pen p);
        Poco GetPOCO();
        bool Contiene(Point p);
        public int Tipo { get; set; }
        public Point Location();
    }
}
