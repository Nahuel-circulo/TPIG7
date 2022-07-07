﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace WinFormsApp1
{
    internal class Flecha : Figuras
    {
        int tipo;
        Point inicio;
        Point fin;
        Rectangle flechaRect;

        public Flecha(int tipo, Point inicio, Point fin)
        {
            this.tipo = tipo;
            this.inicio = inicio;
            this.fin = fin;
        }
        public Flecha(Poco p)
        {
            this.tipo = p.Tipo;
            this.inicio = p.Punto;
            this.fin = new Point(p.Punto.X + p.Ancho, p.Punto.Y + p.Alto);
        }

        public int Tipo { get => tipo; set => tipo = value; }
        public Point Inicio { get => inicio; set => inicio = value; }
        public Point Fin { get => fin; set => fin = value; }
     

        public void Draw(ref Graphics g, Pen p)
        {
            
            if (Tipo == 2)
            {
                p.StartCap = LineCap.Flat;
                p.EndCap = LineCap.ArrowAnchor;
            }
            if (Tipo == 3)
            {
                p.StartCap = LineCap.ArrowAnchor;
                p.EndCap = LineCap.ArrowAnchor;
            }
            if (Tipo == 4)
            {
                p.StartCap = LineCap.Flat;
                p.EndCap = LineCap.Flat;
            }

            g.DrawLine(p,Inicio,Fin);
        }

        public Poco GetPOCO()
        {
            Poco p = new Poco();
            p.Tipo = tipo;
            p.Texto = "";
            p.Punto = inicio;
            p.Ancho = fin.X ;
            p.Alto = fin.Y ;
            p.StringFormat = new StringFormat();
            flechaRect = new Rectangle(p.Punto,new Size(p.Ancho,p.Alto));
            return p;
        }

        public bool Contiene(Point p)
        {
            //No Funciona
            Debug.WriteLine(flechaRect);
            return flechaRect.Contains(p);
        }

        public Point Location()
        {
            return inicio;
        }
    }
}
