using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIG7
{
    internal interface Forma
    {
        //private Font font;
        //private string contenido;
        // private int tipo;
        //protected Point puntoDeEscritura;




        //  public Font Font { get => font; set => font = value; }
        // public string Contenido { get => contenido; set => contenido = value; }
        //  public int Tipo { get => tipo; set => tipo = value; }
        //public Point PuntoDeEscritura { get => puntoDeEscritura; }

        void Dibujar(Graphics g, Pen p);
       // void DibujarTexto(Graphics g, Pen p, string texto);



        private string type;

        private System.Drawing.Rectangle form;

        public Forma(string type, Rectangle form)
        {
            this.Type = type;
            this.Form = form;
        }

    }
}
