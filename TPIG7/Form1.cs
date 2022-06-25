using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        private Pen pen = new Pen(Color.Black, 5);

        // rectangulo auxiliar usado para el dibujo
        private Rectangle rect;

        // el grafico :v
        private Graphics g;

        // variable que se encarga de manejar en que momento se dibuja algo o no 
        private bool pintar = false;


        string form = "rectangle";

        //variable que define el brush( no tengo idea de que pingo es un brush pero lo defino aca )
        private Brush brush;

        //todavia no uso esto pero es una fuente 
        private Font font;

        //variable que define el bitmap( no tengo idea de que pingo es un bitmap pero lo defino aca )
        private Bitmap bitmap;

        

        bool ecribiendo = false;


        //constructor
        public Form1()
        

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

        //exportar como imagen
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        }

        private void button5_Click(object sender, EventArgs e)
        {

        private void button3_Click(object sender, EventArgs e)
        {
            dibujo= 2;
            pen.EndCap = LineCap.Flat;
            pen.StartCap = LineCap.Flat;
            saveFileDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dibujo = 3;
            
            pen.StartCap = LineCap.Flat;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dibujo = 4; 
            pen.EndCap = LineCap.ArrowAnchor;
        }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
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
        {
        }
        }
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
        //        Pen pen = new Pen(Color.Black, 5);
            dibujar();
        //        g.DrawRectangle(pen, rect);
        //}
        private void previewLinea(MouseEventArgs e)
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) { 

            g.Clear(Color.White);
            pintar = true;
        }
            dibujar();
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Está seguro que desea descartar los cambios?", "Borrar", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bitmap);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pictureBox1.Image = bitmap;
                g.Clear(Color.White);
                rectangulos.Clear();

            }
        }

        private void imagenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg |*.jpg|BMP |*.bmp";
            saveFileDialog1.Title = "Guardar como";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {

                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
            }
                    case 1:
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
                        this.pictureBox1.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }
            pictureBox1.Refresh();
                fs.Close();
            }
        }

        private void jSONToolStripMenuItem_Click(object sender, EventArgs e)
            y = 0;
        }
            saveFileDialog1.Filter = "JSON |*.json";
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
            saveFileDialog1.ShowDialog();

        {
            {
                string json = JsonConvert.SerializeObject(rectangulos);
                System.IO.File.WriteAllText(saveFileDialog1.FileName, json);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Forma item in rectangulos)
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
               // dibujar(item);
            }
        }
        private string botonArrastrando = "";
            openFileDialog1.Title = "Abrir imagen";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                bitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        //}



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

      





















        private void Form1_Load(object sender, EventArgs e)
        {
            //Graphics golfgti6;
            //golfgti6 = pictureBox2.CreateGraphics();
            //Pen lapiz = new Pen(Color.Black, 5);
            //golfgti6.DrawEllipse(lapiz, 20, 20, 250, 200);
            

            rc = new ResizableControl(pictureBox2);
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
        //    arrowStartY = e.Y;
        //}
        //{
        //    label2.Text = "Arrow End: " + e.X + ", " + e.Y;

        }
        //    {
        //        {


            if (form == "rectangle")
            {
                //g.DrawRectangle(pen, rect);
                rectangulos.Add(new Forma("rectangle", rect));
            }
        //        panel1.Invalidate();
        //    }
        //}


        //        pen.EndCap = LineCap.ArrowAnchor;
        //        arrowEndY);
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
