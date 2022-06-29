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
        

        int Tipo { get; set; }
        Point Inicio { get; set; }
        Point Fin { get; set; }

        Rectangle Rectangle { get; set; }


        void Draw(ref Graphics g, Pen p);
    }
}
