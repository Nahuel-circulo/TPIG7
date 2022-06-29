using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPIG7
{
    public partial class Form1 : Form
    {
        // lista que se encarga de manejar todo lo que se dibuja en el grafico (graphics)

        private List<Line> lineas = new List<Line>();

        //posisiones respectivas del mouse en el grafico
        private int positionX, arrowStartX, arrowEndX = 0;
        private int positionY, arrowStartY, arrowEndY = 0;

        //lapis para dibujar cosas en el grafico , contienen toda la informacion respectiva de un lapis XD
        private Pen pen = new Pen(Color.Black, 3);

        // rectangulo auxiliar usado para el dibujo
        private Rectangle rect;

        // el grafico :v
        private Graphics g;

        // variable que se encarga de manejar en que momento se dibuja algo o no 
        private bool pintar = false;


        string form = "rectangle";

        //variable que define el brush
        private Brush brush;

        //private Font font;

        private Bitmap bitmap;


        private void button3_Click(object sender, EventArgs e)
        {
            form = "line";
            pen.EndCap = LineCap.Flat;
            pen.StartCap = LineCap.Flat;
            pictureBox1.Cursor = Cursors.Cross;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form = "line";
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.ArrowAnchor;
            pictureBox1.Cursor = Cursors.Cross;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            form = "line";
            pen.EndCap = LineCap.ArrowAnchor;
            pen.StartCap = LineCap.RoundAnchor;
            pictureBox1.Cursor = Cursors.Cross;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form = "rectangle";
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form = "circle";
            pictureBox1.Cursor = Cursors.Hand;
        }


        //exportar como imagen
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg |*.jpeg|BMP |*.bmp";
            saveFileDialog1.Title = "Guardar como";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {

                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();

                DrawToBitmap(bitmap, pictureBox1.ClientRectangle);
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:

                        //this.pictureBox1.Image.Save(fs,ImageFormat.Jpeg);
                        bitmap.Save(fs, ImageFormat.Jpeg);
                        break;

                    case 2:
                        //capture(pictureBox1, saveFileDialog1.FileName);
                        bitmap.Save(fs, ImageFormat.Bmp);
                        break;
                }

                fs.Close();
            }
        }





        // abrir imagen
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



        //constructor
        public Form1()
        {
            InitializeComponent();

            brush = new SolidBrush(Color.Black);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            pictureBox1.Image = bitmap;


        }




        // redimensiona el bitmap
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Width > 0 || pictureBox1.Height > 0)
            {

                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pictureBox1.Image = bitmap;
                drawing();
            }

        }

        // borrar todo y comenzar un diagrama nuevo
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Está seguro que desea descartar los cambios?", "Borrar", MessageBoxButtons.YesNo);
            // arreglar
            if (result == DialogResult.Yes)
            {
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pictureBox1.Image = bitmap;
                g.Clear(Color.White);
                lineas.Clear();
            }


        }

        // exporta los rectangulos a JSON falta hacer las lineas
        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JSON |*.json";
            saveFileDialog1.Title = "Guardar como";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // No Funciona

                //string json = JsonConvert.SerializeObject(formasASerializar);
                //System.IO.File.WriteAllText(saveFileDialog1.FileName, json);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {


        }

        public bool lineIsOver = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;
            positionX = e.X;
            positionY = e.Y;

            pintar = true;


        }

        public bool moving = false;
        private void checkArrowPoint(Line linea, MouseEventArgs e)
        {
            if (((linea.ArrowPositionXStart + 3) >= e.X && e.X >= (linea.ArrowPositionXStart - 3))
                        &&
                        ((linea.ArrowPositionYStart + 3) >= e.Y && e.Y >= (linea.ArrowPositionYStart - 3))
                        ||
                        ((linea.ArrowPositionXEnd + 3) >= e.X && e.X >= (linea.ArrowPositionXEnd - 3))
                        &&
                        ((linea.ArrowPositionYEnd + 3) >= e.Y && e.Y >= (linea.ArrowPositionYEnd - 3)))
            {
                pictureBox1.Cursor = Cursors.Hand;
                lineIsOver = true;
            }
            else
            {
                pictureBox1.Cursor = Cursors.Cross;
                lineIsOver = false;
            }

        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {


                g.Clear(Color.White);

                if (form == "rectangle" || form == "circle")
                {

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

                drawing();

            }
            else
            {
                foreach (var linea in lineas)
                {
                    checkArrowPoint(linea, e);
                }
            }

        }

        private List<MyForma> formasASerializar = new List<MyForma>();
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            pintar = false;
            arrowEndX = e.X;
            arrowEndY = e.Y;


            if (form == "rectangle")
            {


                MyForma otro = new MyForma(rect.Width, rect.Height, form);
                otro.Location = new Point(rect.X, rect.Y);

                otro.Name = "myRectangle";
                otro.SizeMode = PictureBoxSizeMode.StretchImage;

                ResizableControl anotherControl = new ResizableControl(otro);

                formasASerializar.Add(otro);

                pictureBox1.Controls.Add(otro);

                g.Clear(Color.White);
                pictureBox1.Refresh();

            }


            if (form == "circle")
            {

                MyForma otro = new MyForma(rect.Width, rect.Height, form);
                otro.Location = new Point(rect.X, rect.Y);

                otro.Name = "myRectangle";
                otro.SizeMode = PictureBoxSizeMode.StretchImage;
                ResizableControl resizableControl = new ResizableControl(otro);
                pictureBox1.Controls.Add(otro);

                g.Clear(Color.White);
                pictureBox1.Refresh();
            }
            drawing();

            if (form == "line")
            {
                //g.DrawLine(pen,
                //arrowStartX,
                //arrowStartY,
                //arrowEndX,
                //arrowEndY);

                lineas.Add(new Line(arrowStartX, arrowEndX, arrowStartY, arrowEndY, pen));
            }


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            drawing();
        }



        private void drawing()
        {


            foreach (Line item in lineas)
            {
                item.dibujar(g);
            }
            pictureBox1.Refresh();

        }

    }
}
