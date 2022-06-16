using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        
        
       
    }
}
