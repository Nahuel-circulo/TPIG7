﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace TPIG7
{
    internal class MyForma : PictureBox
    {


        private int borderSize = 3;
        private string tipo = "circle";

        public MyForma(int width, int height, string tipe)
        {
            tipo = tipe;
            //if (tipe == "circle")
            //{
            //    this.Size = new Size(80, 80);
            //}
            //else
            //{
            //    this.Size = new Size(100, 60);
            //}
            this.BackColor = Color.Transparent;   
            this.Size = new Size(width, height);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            
            TextBox text = new TextBox();
            text.Text = "";
            text.Multiline = true;

            text.Multiline = true;
            if (tipe == "circle")
            {
                text.Width = (this.Size.Width - 20) - ((int)(this.Size.Width * 0.25));
                text.Height = (this.Size.Height - 20) - ((int)(this.Size.Height * 0.25));
                text.Location = new Point((int)(this.Size.Width * 0.2), (int)(this.Size.Height * 0.2));
                text.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            }
            else
            {
                text.Location = new Point(10, 10);
                text.Width = this.Size.Width - 20;
                text.Height = this.Size.Height - 20;
                text.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left;
            }
            //text.BorderStyle = BorderStyle.None;
            text.TextAlign = HorizontalAlignment.Center;
            this.Controls.Add(text);
        }

        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "circle")
                {

                    tipo = value;
                }
                else
                {
                    tipo = "rectangle";
                }
                this.Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Height);
        }



        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var graphics = pe.Graphics;
            var contornoRectanfulo = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var bordeRectangulo = Rectangle.Inflate(contornoRectanfulo, -borderSize, -borderSize);
            var smoothSize = borderSize > 0 ? borderSize * 3 : 1;
            using (var pathRegion = new GraphicsPath())
            using (var penSmooth = new Pen(this.Parent.BackColor, smoothSize))
            using (var penBorder = new Pen(Color.Black, BorderSize))
            {
                penBorder.DashStyle = DashStyle.Solid;
                penBorder.DashCap = DashCap.Flat;
                pathRegion.AddRectangle(contornoRectanfulo);
                this.Region = new Region(pathRegion);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;


                // dbujado
                graphics.DrawRectangle(penSmooth, contornoRectanfulo);

                if (borderSize > 0)
                {
                    if (tipo == "circle")
                    {
                        graphics.DrawEllipse(penBorder, bordeRectangulo);
                    }
                    else
                    {
                        graphics.DrawRectangle(penBorder, bordeRectangulo);
                    }
                }
            }

        }
    }
}
