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

        private List<Rectangle> rectangulos= new List<Rectangle>();
        private int x,h, arrowStartX, arrowEndX = 0;
        private int y,w, arrowStartY, arrowEndY = 0;
        private Pen pen = new Pen(Color.Black, 5);
        private Rectangle rect;
        private Graphics g;
        private bool pintar = false;

        public Form1()
        {
            InitializeComponent();
            g = this.panel2.CreateGraphics();
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

        private void panel12_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            pintar = true;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintar)
            {
                h = e.X - x;
                w = e.Y - y;
                rect = new Rectangle(x, y, h, w);


                panel2.Refresh();

                g.DrawRectangle(pen, rect);



            }

        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            pintar = false;
            rectangulos.Add(rect);
            x = 0;
            y = 0;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {


            drawing();

        }

        private void drawing()
        {

            foreach (Rectangle item in rectangulos)
            {
                g.DrawRectangle(pen, item);
            }

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;


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
}
