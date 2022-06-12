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
        Rectangle rect;
        private int x, y, h, w = -1;
        private bool pintar = false;
        private Pen Pen = new Pen(Color.Black, 3);

       
        public Form2()
        {
            InitializeComponent();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            x = 0;
            y = 0;

            pintar = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
        //    w = e.X - x;
        //    h= e.Y - y;
            if(pintar)
            {
                //rect = new Rectangle(x, y, w, h);
                //rectangles.Add(rect);

                panel1.Invalidate();
                panel1.Refresh();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x= e.X;
            y= e.Y;
            w = e.X - x;
            h = e.Y - y;
           

            Graphics g = this.panel1.CreateGraphics();
            rect = new Rectangle(x, y, w, h);
            rectangles.Add(rect);
            Pen pen = new Pen(Color.Black, 5);


            g.DrawRectangle(pen, rect);

            pen.Dispose();


        }
        private void panel1_Move(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = this.panel1.CreateGraphics();
            
            //Pen pen = new Pen(Color.Black, 5);

               
            //g.DrawRectangle(pen, rect);

            //pen.Dispose();
            
        }
    }
}
