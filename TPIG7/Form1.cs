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
        private int x, arrowStartX, arrowEndX = -1;
        private int y, arrowStartY, arrowEndY = -1;
        private Pen pen = new Pen(Color.Black, 5);
        private Rectangle rect;
        private bool pintando = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            pintando = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pintando)
            {
                rect = new Rectangle(
                    Math.Min(e.X, x),
                    Math.Min(e.Y, y),
                    (e.X - x),
                    (e.Y - y));

                panel1.Invalidate();
                panel1.Refresh();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            pintando = false;

            x = 0;
            y = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = this.panel1.CreateGraphics())
            {
                Pen pen = new Pen(Color.Black, 5);

                g.DrawRectangle(pen, rect);

                pen.Dispose();
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            arrowStartX = e.X;
            arrowStartY = e.Y;

            label1.Text = "Arrow Start: " + arrowStartX + ", " + arrowStartY;
            pintando = true;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {

            label2.Text = "Arrow End: " + e.X + ", " + e.Y;


            if (pintando)
            {

                panel2.Refresh();
                pen.StartCap = LineCap.RoundAnchor;
                pen.EndCap = LineCap.ArrowAnchor;
                using (Graphics g = this.panel2.CreateGraphics())
                {

                    g.DrawLine(pen, arrowStartX,
                    arrowStartY,
                    e.X,
                    e.Y);


                }
                panel1.Invalidate();

            }

        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            arrowEndX = e.X;
            arrowEndY = e.Y;

            pintando = false;
            panel2.Refresh();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = this.panel2.CreateGraphics())
            {
                Pen pen = new Pen(Color.Black, 5);
                pen.StartCap = LineCap.RoundAnchor;
                pen.EndCap = LineCap.ArrowAnchor;

                g.DrawLine(pen, arrowStartX,
                arrowStartY,
                arrowEndX,
                arrowEndY);

                pen.Dispose();

            }

        }


    }
}
