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
using System.Text.Json;
using System.IO;

namespace WinFormsApp1
{
    public partial class VentanaPrinsipal : Form
    {
        Bitmap Bitmap;
        Graphics g;
        Lienso Lienso;
        bool pintar;
        int TipoForma;







        public VentanaPrinsipal()
        {
            InitializeComponent();
            Bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g= Graphics.FromImage(Bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Lienso = new Lienso(ref g, ref pictureBox1, ref Bitmap);
            TipoForma = 0;





        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lienso.SetTipoDeFlecha(0);
            TipoForma = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lienso.SetTipoDeFlecha(0);
            TipoForma = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lienso.SetTipoDeFlecha(2);
            TipoForma = 2;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Lienso.SetTipoDeFlecha(3);
            TipoForma = 3;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Lienso.SetTipoDeFlecha(4);
            TipoForma = 4;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pintar = true;
            Lienso.PuntoDeEsquina = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {
                switch (TipoForma)
                {
                    case 0:
                        Lienso.PreViewRectangulo(e.Location);
                        break;
                    case 1:
                        Lienso.PreViewCirculo(e.Location);
                        break ;
                    case 2:
                        Lienso.PreViewFlecha(e.Location);
                        break;

                    case 3:
                        Lienso.PreViewFlecha(e.Location);
                        break;
                    case 4:
                        Lienso.PreViewFlecha(e.Location);
                        break;
                    default:
                        break;
                }
                
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pintar=false;
            Lienso.GuardarFigura(e.Location, TipoForma);

         
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void GueardarJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JSON |*.json";
            saveFileDialog1.Title = "Guardar como";
            saveFileDialog1.ShowDialog();

            List<Poco> pocos = new List<Poco>();
            foreach(var item in Lienso.Figuras)
            {
                pocos.Add(item.GetPOCO());
            }

            if (saveFileDialog1.FileName != "")
            {
                var serializer = JsonSerializer.Serialize(pocos);
                System.IO.File.WriteAllText(saveFileDialog1.FileName, serializer);
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
            openFileDialog1.Filter = "JSON |*.json";
            openFileDialog1.Title = "Abrir JSON";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                Lienso.SetPoco( JsonSerializer.Deserialize<List<Poco>>(File.ReadAllText(openFileDialog1.FileName)));

                Lienso.ReDraing();
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea descartar los cambios?", "Borrar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Lienso.Clear();
                Lienso.ReDraing(); 
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lienso.cambios())
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea descartar los cambios?", "Borrar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }

            }else this.Close();

        }
    }
}
