using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TPIG7
{
    internal class Forma2D : Forma
    {
        private Rectangle rectangle;
        private int tipo;
        private string contenido;

        public Forma2D(Rectangle rectangle, int tipo, string contenido)
        {
            this.rectangle = rectangle;
            this.tipo = tipo;
            this.contenido = contenido;
        }

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public string Contenido { get => contenido; set => contenido = value; }

      
    }
}
