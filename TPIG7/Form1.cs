using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPIG7
{
    public partial class Form1 : Form
    {
        // lista que se encarga de manejar todo lo que se dibuja en el grafico (graphics)
        private List<Forma> rectangulos= new List<Forma>();
        
        //posisiones respectivas del maus en el grafico
        private int x,h, arrowStartX, arrowEndX = 0;
        private int y,w, arrowStartY, arrowEndY = 0;

        //lapis para dibujar cosas en el grafico , contienen toda la informacion respectiva de un lapis XD
        private Pen pen = new Pen(Color.Black, 2);

        // rectangulo auviliar usado para el dibujo
        private Rectangle rect;

        // el grafico :v
        private Graphics g;

        // variable que se encarga de manejar en que momento se dibuja algo o no 
        private bool pintar = false;

        // enumeracion que define los tipos de dibujos que se pueden hacer 
        private enum dibujos { cuadrado, circulo , linea, flechaDoble,flecha }

        //variable que define que dibujo se esta por hacer en el momento
        private int dibujo;

        //variable que define el brush( no tengo idea de que pingo es un brush ero lo defino aca )
        private Brush brush;

        private Font font;

        private Bitmap bitmap;

        private char caracter;

        
     

        //constructor
        public Form1()
        {
            InitializeComponent();
            
            brush = new SolidBrush(Color.Black);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dibujo = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dibujo = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dibujo= 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dibujo = 3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dibujo = 4;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(e.KeyChar);
            label1.Text = stringBuilder.ToString();
            
            
        }





        //private void panel3_Paint(object sender, PaintEventArgs e)
        //{
        //    using (Graphics g = this.panel2.CreateGraphics())
        //    {
        //        Pen pen = new Pen(Color.Black, 5);

        //        g.DrawRectangle(pen, rect);

        //        pen.Dispose();
        //    }
        //}


        //funcion que registra la pocicon de el maus al momento de precionar el boton del maus, valga la redundancia
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            pintar = true;
        }

        //funcion que se encarga de pintar mientras se esta precionanddo un boton 
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {
                g.Clear(Color.White);
                h = e.X - x;
                w = e.Y - y;
                rect = new Rectangle(x, y, h, w);
                g.DrawRectangle(pen, rect);
                pictureBox1.Refresh();
                
               
               


            }

        }
        //funcion qe se encargar de setear cosas cuando se suelta el boton del maus 
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pintar = false;
            bool escrito = false;
            g.DrawRectangle(pen, rect);
            StringBuilder sb = new StringBuilder();
            //do
            //{
            //    sb.Append(caracter);

            //    g.DrawString(sb.ToString(), font, brush, rect.X, rect.Y);
                
            //} while (escrito);
            pictureBox1.Refresh();

            rectangulos.Add(new Forma2D());
            x = 0;
            y = 0;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


            foreach (Forma item in rectangulos)
            {
               // dibujar(item);
            }

        }

        //private void dibujar(Forma forma)
        //{
        //    switch (forma.Tipo)
        //    {
        //        case 0: dibujarRectangulo(forma);
        //            break;
        //        default: break;
                    

                    
        //    }
            

        //}

        private void dibujarRectangulo(Forma2D forma)
        {
            g.DrawRectangle(pen,forma.Rectangle);
            //g.DrawString(forma.Contenido,forma.Font,brush,forma.PuntoDeEscritura);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;
        }




























































        //private void panel2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    arrowStartX = e.X;
        //    arrowStartY = e.Y;

        //    label1.Text = "Arrow Start: " + arrowStartX + ", " + arrowStartY;
        //    pintar = true;
        //}

        //private void panel2_MouseMove(object sender, MouseEventArgs e)
        //{

        //    label2.Text = "Arrow End: " + e.X + ", " + e.Y;


        //    if (pintar)
        //    {

        //        panel2.Refresh();
        //        pen.StartCap = LineCap.RoundAnchor;
        //        pen.EndCap = LineCap.ArrowAnchor;
        //        using (Graphics g = this.panel2.CreateGraphics())
        //        {

        //            g.DrawLine(pen, arrowStartX,
        //            arrowStartY,
        //            e.X,
        //            e.Y);


        //        }
        //        panel1.Invalidate();

        //    }

        //}

        //private void panel2_MouseUp(object sender, MouseEventArgs e)
        //{
        //    arrowEndX = e.X;
        //    arrowEndY = e.Y;

        //    pintar = false;
        //    panel2.Refresh();
        //}

        //private void panel2_Paint(object sender, PaintEventArgs e)
        //{
        //    using (Graphics g = this.panel2.CreateGraphics())
        //    {
        //        Pen pen = new Pen(Color.Black, 5);
        //        pen.StartCap = LineCap.RoundAnchor;
        //        pen.EndCap = LineCap.ArrowAnchor;

        //        g.DrawLine(pen, arrowStartX,
        //        arrowStartY,
        //        arrowEndX,
        //        arrowEndY);

        //        pen.Dispose();

        //    }

        



    }
}
