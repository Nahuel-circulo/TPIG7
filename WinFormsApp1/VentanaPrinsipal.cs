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
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class VentanaPrincipal : Form
    {
        Bitmap Bitmap;
        Graphics g;
        Lienso Lienso;
        bool pintar;
        int TipoForma;
      
        Point p;

        public VentanaPrincipal()
        {
            InitializeComponent();
            Bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(Bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Lienso = new Lienso(ref g, ref pictureBox1, ref Bitmap);
            TipoForma = 0;
         
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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pintar = true;
            Lienso.PuntoDeEsquina = e.Location;
            if (e.Button == MouseButtons.Right) p = e.Location;
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
                File.WriteAllText(saveFileDialog1.FileName, serializer);
            }

            
        }

        private void imagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg |*.jpeg|BMP |*.bmp";
            saveFileDialog1.Title = "Guardar como";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();

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
            this.Close();

        }

        private void VentanaPrinsipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Lienso.cambios())
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea descartar los cambios?", "Borrar", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                
            }
            
        }

        private void editarToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            List<Figuras> lista = new List<Figuras>();
            
            foreach (Figuras forma in Lienso.Figuras)
            {
                
                if (forma.Contiene(p))
                {
                    
                    MyForma otro = new MyForma((Forma)forma);
                    otro.Location = forma.Location();
                    lista.Add(forma);
                    otro.Name = "myRectangle";
                    otro.SizeMode = PictureBoxSizeMode.StretchImage;
                    ResizableControl resizableControl = new ResizableControl(otro);
                    otro.ContextMenuStrip = MenuEditarControles;
                    pictureBox1.Controls.Add(otro);
                    
                   
                }
            }
            foreach(Figuras figuras in lista)
            {
                Lienso.remover(figuras);
            }
            
            Lienso.ReDraing();
        }

        private void terminarEdicionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MenuEditarControles.SourceControl != null)
            {
            Lienso.getFigura(MenuEditarControles.SourceControl as MyForma);
            pictureBox1.Controls.Remove(MenuEditarControles.SourceControl);
            }
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Figuras> lista = new List<Figuras>();

            foreach (Figuras forma in Lienso.Figuras)
            { 
                if (forma.Contiene(p))
                {
                    lista.Add(forma);
                }
            }
            foreach (Figuras figuras in lista)
            {
                Lienso.remover(figuras);
            }

            Lienso.ReDraing();
        }

       
        private void borrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            pictureBox1.Controls.Remove(MenuEditarControles.SourceControl);
        }

        private void VentanaPrincipal_Resize(object sender, EventArgs e)
        {
            Lienso.ResizeLienzo(pictureBox1.Width, pictureBox1.Height);
            Lienso.ReDraing();
        }

        private void eliminarUltimaFiguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lienso.Deshacer();
            Lienso.ReDraing();
        }
    }
}
