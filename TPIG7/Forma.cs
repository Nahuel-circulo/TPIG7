using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
