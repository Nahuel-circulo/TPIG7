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
        private List<Forma> rectangulos = new List<Forma>();

        //posisiones respectivas del maus en el grafico
        private int positionX, width, arrowStartX, arrowEndX = 0;
        private int positionY, height, arrowStartY, arrowEndY = 0;

        //lapis para dibujar cosas en el grafico , contienen toda la informacion respectiva de un lapis XD
        private Pen pen = new Pen(Color.Black, 5);

        // rectangulo auviliar usado para el dibujo
        private Rectangle rect;

        // el grafico :v
        private Graphics g;

        // variable que se encarga de manejar en que momento se dibuja algo o no 
        private bool pintar = false;

        // enumeracion que define los tipos de dibujos que se pueden hacer 
        private enum dibujos { cuadrado, circulo, linea, flechaDoble, flecha }


        string form = "rectangle";

        //variable que define el brush( no tengo idea de que pingo es un brush ero lo defino aca )
        private Brush brush;

        private Font font;

        private Bitmap bitmap;

        
        private void button3_Click(object sender, EventArgs e)
        {
            form = "line";
            pen.EndCap = LineCap.Flat;
            pen.StartCap = LineCap.Flat;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form = "line";
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.ArrowAnchor;
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form = "rectangle";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form = "circle";
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

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "JPeg |*.jpg|BMP |*.bmp";
            openFileDialog1.Title = "Abrir imagen";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                bitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form = "line";
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.Flat;
        }


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


        //funcion que registra la pocicon de el maus al momento de precionar el boton del maus, valga la redundancia
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;
            positionX = e.X;
            positionY = e.Y;

            pintar = true;
        }

        //funcion que se encarga de pintar mientras se esta precionanddo un boton 
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {


                g.Clear(Color.White);

                if (form == "rectangle" || form == "circle")
                {
                    g.Clear(Color.White);
                    rect = new Rectangle(
                    Math.Min(e.X, positionX),
                    Math.Min(e.Y, positionY),
                    (e.X - positionX),
                    (e.Y - positionY));
                }

                if (form == "rectangle")
                {
                    g.DrawRectangle(pen, rect);
                }

                if (form == "circle")
                {
                    g.DrawEllipse(pen, rect);
                }

                if (form == "line")
                {

                    arrowEndX = e.X;
                    arrowEndY = e.Y;

                    g.DrawLine(pen, arrowStartX,
                    arrowStartY,
                    e.X,
                    e.Y);

                }


                pictureBox1.Refresh();


            }

        }

        //funcion qe se encargar de setear cosas cuando se suelta el boton del maus 
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            arrowEndX = e.X;
            arrowEndY = e.Y;
            pintar = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (form == "rectangle")
            {
                g.Clear(Color.White);
                g.DrawRectangle(pen, rect);
            }

            if (form == "circle")
            {
                g.DrawEllipse(pen, rect);
            }

            if (form == "line")
            {
                g.DrawLine(pen,
                arrowStartX,
                arrowStartY,
                arrowEndX,
                arrowEndY);
            }






        }



    }
}
