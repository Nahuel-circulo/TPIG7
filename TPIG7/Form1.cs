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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPIG7
{
    public partial class Form1 : Form
    {
        // lista que se encarga de manejar todo lo que se dibuja en el grafico (graphics)
        private List<Forma> rectangulos = new List<Forma>();
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

                FileStream fs = (FileStream)saveFileDialog1.OpenFile();

                Rectangle bounds = pictureBox1.Bounds;
                Point pt = pictureBox1.PointToScreen(bounds.Location);
                Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(pt.X - pictureBox1.Location.X, pt.Y - pictureBox1.Location.Y), Point.Empty, bounds.Size);
                }
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        //this.pictureBox1.Image.Save(fs,
                        //   System.Drawing.Imaging.ImageFormat.Jpeg);
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




        private void capture(Control ctrl, string fileName)
        {
            Rectangle bounds = ctrl.Bounds;
            Point pt = ctrl.PointToScreen(bounds.Location);
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(pt.X - ctrl.Location.X, pt.Y - ctrl.Location.Y), Point.Empty, bounds.Size);
            }
            bitmap.Save(fileName, ImageFormat.Png);
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



        ResizableControl rc;

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
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            pictureBox1.Image = bitmap;
            drawing();

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
                rectangulos.Clear();
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
                string json = JsonConvert.SerializeObject(rectangulos);
                System.IO.File.WriteAllText(saveFileDialog1.FileName, json);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {


        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;
            positionX = e.X;
            positionY = e.Y;

            //if (form == "rectangle" || form == "circle")
            //{

            //MyForma otro = new MyForma(100,60,form);
            //otro.Location = new Point(arrowStartX, arrowStartY);

            //otro.Name = "myForma2";
            //otro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            //ResizableControl resizableControl = new ResizableControl(otro);

            //pictureBox1.Controls.Add(otro);
            //}
            pintar = true;
        }

        private void editarToolStripMenuItem1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach(Forma forma in rectangulos)
            {
                int YS = forma.Form.Y;
                int XS = forma.Form.X;
                int YI = forma.Form.Y- forma.Form.Height;
                int XI = forma.Form.X- forma.Form.Width;
                if ((e.Y<YS && e.Y >YI)&& (e.X < XS && e.X > XI))
                {
                    
                    MyForma otro = forma.CombertirAContol(forma.Form.Width, forma.Form.Height,contextMenuStrip1);
                    ResizableControl resizableControl = new ResizableControl(otro);
                    pictureBox1.Controls.Add(otro);
                    pictureBox1.Refresh();
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

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

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            
            arrowEndX = e.X;
            arrowEndY = e.Y;

            if (pintar)
            {
                if (form == "rectangle")
                {

                    rectangulos.Add(new Forma("rectangle", rect));


                    //MyForma otro = new MyForma(rect.Width, rect.Height, form);
                    //otro.Location = new Point(rect.X, rect.Y);

                    //otro.Name = "myRectangle";
                    //otro.SizeMode = PictureBoxSizeMode.StretchImage;
                    //ResizableControl resizableControl = new ResizableControl(otro);
                    //pictureBox1.Controls.Add(otro);
                    //g.Clear(Color.White);
                    pictureBox1.Refresh();
                    g.DrawRectangle(pen, rect);

                }

                if (form == "circle")
                {

                    rectangulos.Add(new Forma("circle", rect));
                    //MyForma otro = new MyForma(rect.Width, rect.Height, form);
                    //otro.Location = new Point(rect.X, rect.Y);

                    //otro.Name = "myRectangle";
                    //otro.SizeMode = PictureBoxSizeMode.StretchImage;
                    //ResizableControl resizableControl = new ResizableControl(otro);
                    //pictureBox1.Controls.Add(otro);

                    //g.Clear(Color.White);
                    pictureBox1.Refresh();
                    g.DrawEllipse(pen, rect);
                }

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
            pintar = false;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            drawing();
        }



        private void drawing()
        {

            foreach (Forma formas in rectangulos)
            {
                if (formas.Type == "rectangle")
                {
                    g.DrawRectangle(pen, formas.Form);
                }
                else
                {
                    g.DrawEllipse(pen, formas.Form);
                }
            }

            foreach (Line item in lineas)
            {
                item.dibujar(g);
            }
            pictureBox1.Refresh();

        }

    }
}
