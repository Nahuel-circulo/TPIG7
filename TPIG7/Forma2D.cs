using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TPIG7
{
    //clase que se encarga de registrar todos los datos de una figura en 2D sea un rectangulo o un circulo 
    internal class Forma2D : Forma
    {
        private int tipo;
        private Rectangle rectangle;
        private Texto texto;


        public Forma2D(Rectangle rectangle, int tipo)
        {
            
            this.Rectangle = rectangle;
            this.Tipo = tipo;
           
        }

        public Forma2D(Rectangle rectangle, int tipo,Texto texto)
        {
            this.texto = texto;
            this.Rectangle = rectangle;
            this.Tipo = tipo;

        }


        //public Forma2D(Rectangle rectangle, int tipo, string contenido, Font font)
        //{
        //    this.Font = font;
        //    this.Rectangle = rectangle;
        //    this.Tipo = tipo;
        //    this.Contenido = contenido;
        //    this.puntoDeEscritura = new Point(rectangle.Size);
        //}
        public Forma2D()
        { 
        }

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        internal Texto Texto { get => texto; set => texto = value; }

        public void Dibujar(Graphics g, Pen p)
        {
            switch (Tipo)
            {
                case 0:
                    g.DrawRectangle(p, rectangle);
                    break;
                case 1:
                    g.DrawEllipse(p, rectangle);
                    break;




            }
            if(texto != null)
            {
                texto.DibujarTexto(g, p, rectangle);
            }
        }
        public void DibujarTexto(Graphics g, Pen p, string texto)
        {
             
        }





    }
}
 
