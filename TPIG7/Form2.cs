using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPIG7
{
    public partial class Form2 : Form
    {
        private List<Rectangle> rectangles = new List<Rectangle>();
        Rectangle rect, tempRect;
        
        private int x, y, h, w = -1;
        private bool pintar = false;
        private Pen Pen = new Pen(Color.Black, 3);
        private Graphics g;


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            pintar = true;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {
                h = e.X - x;
                w = e.Y - y;
                tempRect = new Rectangle(x, y, h, w);
                
                 
                panel1.Refresh();

                g.DrawRectangle(Pen, tempRect);
              
               

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            pintar = false;
            rectangles.Add(tempRect);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
            
            drawing();
        }

        
        private void drawing()
        {
           
            foreach (Rectangle item in rectangles)
            {
                g.DrawRectangle(Pen, item);
            }

        }
      

       
        public Form2()
        {
            InitializeComponent();
            g = this.panel1.CreateGraphics();
        }

       
    }
}
