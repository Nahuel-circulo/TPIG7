using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPIG7
{
    internal class Forma
    {

        private string type;

        private System.Drawing.Rectangle form;

        public Forma(string type, Rectangle form)
        {
            this.Type = type;
            this.Form = form;
        }

        public Rectangle Form { get => form; set => form = value; }
        public string Type { get => type; set => type = value; }

        public MyForma CombertirAContol(int Width, int Height, ContextMenuStrip c)
        {
            MyForma a = new MyForma(Width,Height,type,c);
            a.Location = new Point(form.X, form.Y);

            a.Name = "myRectangle";
            a.SizeMode = PictureBoxSizeMode.StretchImage;
            a.ContextMenuStrip = c;
            return a;
        }
    }
}
