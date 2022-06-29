﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.Text.Json;


namespace WinFormsApp1
{
    internal class Lienso
    {
        TachoBasura tachoBasura;
        Graphics g;
        Bitmap bitmap;
        Pen pen;
        Brush brush;
        Font font;
        Point puntoDeEsquina;
        PictureBox pictureBox;
        

        public Lienso(ref Graphics graphics, ref PictureBox p, ref Bitmap b)
        {
            g = graphics;
            pictureBox = p;
            bitmap = b;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            pictureBox.Image = bitmap;
            pen = new Pen(Color.Black, 3);
            TachoBasura = new TachoBasura();

        }

        public Point PuntoDeEsquina { get => puntoDeEsquina; set => puntoDeEsquina = value; }
        public Pen Pen { get => pen; set => pen = value; }
        
        internal TachoBasura TachoBasura { get => tachoBasura; set => tachoBasura = value; }
        

        public void SetTipoDeFlecha(int i)
        { 
            
            if(i== 2)
            {
                pen.StartCap = LineCap.Flat;
                pen.EndCap = LineCap.ArrowAnchor;
            }
            if (i== 3)
            {
                pen.StartCap = LineCap.ArrowAnchor;
                pen.EndCap = LineCap.ArrowAnchor;
            }
            if (i == 4)
            {
                pen.StartCap = LineCap.Flat;
                pen.EndCap = LineCap.Flat;
            }

        }

        public void PreViewRectangulo(Point p)
        {
            g.Clear(Color.White);
            g.DrawRectangle(pen, new Rectangle(PuntoDeEsquina.X,puntoDeEsquina.Y,p.X - PuntoDeEsquina.X, p.Y- puntoDeEsquina.Y));
            Draing();
            pictureBox.Refresh();

        }
        public void PreViewCirculo(Point p)
        {
            g.Clear(Color.White);
            g.DrawEllipse(pen, new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y));
            Draing();
            pictureBox.Refresh();
        }

        public void PreViewFlecha(Point p)
        {
            g.Clear(Color.White);
            g.DrawLine(Pen, PuntoDeEsquina, p);
            Draing();
            pictureBox.Refresh();

        }
        public void GuardarFigura(Point p,int tipo)
        {
            g.Clear(Color.White);
            Rectangle r;
            Forma forma;
            Flecha flecha;
            switch (tipo)
            {
                case 0:
                    r = new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y);
                    forma = new Forma(tipo, r);
                    tachoBasura.AddForma(forma);
                    break;
                case 1:
                    r = new Rectangle(PuntoDeEsquina.X, puntoDeEsquina.Y, p.X - PuntoDeEsquina.X, p.Y - puntoDeEsquina.Y);
                    forma = new Forma(tipo, r);
                    tachoBasura.AddForma(forma);
                    break;
                case 2:
                    flecha = new Flecha(tipo, puntoDeEsquina, p);
                    tachoBasura.AddFlecha(flecha);
                    break;
                case 3:
                    flecha = new Flecha(tipo, puntoDeEsquina, p);
                    tachoBasura.AddFlecha(flecha);
                    break;
                case 4:
                    flecha = new Flecha(tipo, puntoDeEsquina, p);
                    tachoBasura.AddFlecha(flecha);
                    break;
            }
            Draing();
        }

        private void Draing()
        {
            foreach(var intem in tachoBasura.FlechaList)
            {
                intem.Draw(ref g, pen);
            }
            foreach (var intem in tachoBasura.FormaList)
            {
                intem.Draw(ref g, pen);
            }
                pictureBox.Refresh();
        }

        public void ReDraing()
        {
            g.Clear(Color.White);
            foreach (var intem in tachoBasura.FlechaList)
            {
                intem.Draw(ref g, pen);
            }
            foreach (var intem in tachoBasura.FormaList)
            {
                intem.Draw(ref g, pen);
            }
            pictureBox.Refresh();
        }



    }

   
}

