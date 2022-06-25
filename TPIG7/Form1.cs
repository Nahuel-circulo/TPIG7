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
        private List<Forma> rectangulos;
        
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

        //variable que define el brush( no tengo idea de que pingo es un brush pero lo defino aca )
        private Brush brush;

        //todavia no uso esto pero es una fuente 
        private Font font;

        //variable que define el bitmap( no tengo idea de que pingo es un bitmap pero lo defino aca )
        private Bitmap bitmap;

        private int hola;

        bool ecribiendo = false;


        //constructor
        public Form1()
        {
            InitializeComponent();
            
            brush = new SolidBrush(Color.Black);
            font = new Font("Arial", 12);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            pictureBox1.Image = bitmap;
            rectangulos = new List<Forma>();
        }

        // funciones que setean cosas dependiendo de la erramienta que se use 
        private void button1_Click(object sender, EventArgs e)
        {
            dibujo = 0;
            pen.EndCap = LineCap.Flat;
            pen.StartCap = LineCap.Flat;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dibujo = 1;
            pen.EndCap = LineCap.Flat;
            pen.StartCap = LineCap.Flat;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dibujo= 2;
            pen.EndCap = LineCap.Flat;
            pen.StartCap = LineCap.Flat;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dibujo = 3;
            
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.Flat;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dibujo = 4; 
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.ArrowAnchor;
        }

        //funcion que registra la pocicon de el maus al momento de precionar el boton del maus, valga la redundancia
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            pintar = true;
        }

        //funcion que se encarga de pintar previews de las formas mientras se esta precionanddo un boton del maus
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {
                switch (dibujo)
                {
                    case 0:
                        previewRectangulo(e);
                        break;
                    case 1:
                        previewCirculos(e);
                        break;
                    case 2:
                        previewLinea(e);
                        break;
                    case 3:
                        previewLinea(e);
                        break;
                    case 4:
                        previewLinea(e);
                        break;
                    default: break;
                }
            }

        }

        //creo que ya te das cuenta de que hace cada funcin de avajo no ??
        private void previewRectangulo(MouseEventArgs e)
        {
            g.Clear(Color.White);
            h = e.X - x;
            w = e.Y - y;
            rect = new Rectangle(x, y, h, w);
            g.DrawRectangle(pen, rect);
            dibujar();
            
        }
        private void previewCirculos(MouseEventArgs e)
        {
            g.Clear(Color.White);
            h = e.X - x;
            w = e.Y - y;
            rect = new Rectangle(x, y, h, w);
            g.DrawEllipse(pen, rect);
            dibujar();
            
        }
        private void previewLinea(MouseEventArgs e)
        {
            g.Clear(Color.White);
            
            g.DrawLine(pen, x, y, e.X, e.Y);
            dibujar();
            

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }


        //funcion qe se encargar de setear cosas cuando se suelta el boton del maus 
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pintar = false;
           
            Form2 form2 = new Form2();
            form2.ShowDialog(); 

            Texto t = new Texto(form2.Texto,font,brush);
            if(dibujo == 0 || dibujo == 1) 
            { 
                rectangulos.Add(new Forma2D(rect, dibujo,t));
            }
            else
            {
                rectangulos.Add(new Forma1D(pen, x, y, e.X, e.Y));
            }
           
            
           
            
            
            dibujar();
            x = 0;
            y = 0;
          
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            dibujar();
        }

        private void dibujar()
        {
            foreach (Forma item in rectangulos)
            {
                item.Dibujar(g, pen);
            }
            pictureBox1.Refresh();
        }
      
        private StringBuilder Escrivir(Keys keys)
        {
            StringBuilder stringBuilder = new StringBuilder();
            KeysConverter converter = new KeysConverter();
            
            if (keys == Keys.Return)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            else 
            {
                stringBuilder.Append(converter.ConvertToString(keys));
            }
            return stringBuilder;
        }




      

      

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg |*.jpg|BMP |*.bmp";
            saveFileDialog1.Title = "Guardar como";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Save the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }

                fs.Close();
            }
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
