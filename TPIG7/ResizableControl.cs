using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPIG7
{
    internal class ResizableControl
    {
        public Control withEventsField_mControl;
        public Control MControl
        {
            
            get { return withEventsField_mControl; }
            set
            {
                if (withEventsField_mControl != null)
                {
                    withEventsField_mControl.MouseDown -= mControl_MouseDown;
                    withEventsField_mControl.MouseUp -= mControl_MouseUp;
                    withEventsField_mControl.MouseMove -= mControl_MouseMove;
                    withEventsField_mControl.MouseLeave -= mControl_MouseLeave;
                }

                withEventsField_mControl = value;

                if (withEventsField_mControl != null)
                {
                    withEventsField_mControl.MouseDown += mControl_MouseDown;
                    withEventsField_mControl.MouseUp += mControl_MouseUp;
                    withEventsField_mControl.MouseMove += mControl_MouseMove;
                    withEventsField_mControl.MouseLeave += mControl_MouseLeave;
                }
            }
        }
        private bool mMouseDown = false;
        private EdgeEnum mEdge = EdgeEnum.None;
        private int mWidth = 4;

        private bool mOutlineDrawn = false;
        private enum EdgeEnum
        {
            None,
            Right,
            Left,
            Top,
            Bottom,
            TopLeft
        }

        public ResizableControl(Control Control)
        {
            MControl = Control;
        }


        private void mControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mMouseDown = true;
            }
        }


        private void mControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mMouseDown = false;
        }


        private void mControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Control c = (Control)sender;
            Graphics g = c.CreateGraphics();
            switch (mEdge)
            {
                case EdgeEnum.TopLeft:
                    g.FillRectangle(Brushes.Blue, 0, 0, mWidth * 4, mWidth * 4);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Left:
                    g.FillRectangle(Brushes.Blue, 0, 0, mWidth, c.Height);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Right:
                    g.FillRectangle(Brushes.Blue, c.Width - mWidth, 0, c.Width, c.Height);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Top:
                    g.FillRectangle(Brushes.Blue, 0, 0, c.Width, mWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Bottom:
                    g.FillRectangle(Brushes.Blue, 0, c.Height - mWidth, c.Width, mWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.None:
                    if (mOutlineDrawn)
                    {
                        c.Refresh();
                        mOutlineDrawn = false;
                    }
                    break;
            }

            if (mMouseDown & mEdge != EdgeEnum.None)
            {
                c.SuspendLayout();
                switch (mEdge)
                {
                    case EdgeEnum.TopLeft:
                        c.SetBounds(c.Left + e.X, c.Top + e.Y, c.Width, c.Height);
                        break;
                    case EdgeEnum.Left:
                        c.SetBounds(c.Left + e.X, c.Top, c.Width - e.X, c.Height);
                        break;
                    case EdgeEnum.Right:
                        c.SetBounds(c.Left, c.Top, c.Width - (c.Width - e.X), c.Height);
                        break;
                    case EdgeEnum.Top:
                        c.SetBounds(c.Left, c.Top + e.Y, c.Width, c.Height - e.Y);
                        break;
                    case EdgeEnum.Bottom:
                        c.SetBounds(c.Left, c.Top, c.Width, c.Height - (c.Height - e.Y));
                        break;
                }
                c.ResumeLayout();
            }
            else
            {
                while (true)
                {
                    if (e.X <= (mWidth * 4) && e.Y <= (mWidth * 4))
                    {

                        //top left corner
                        c.Cursor = Cursors.SizeAll;
                        mEdge = EdgeEnum.TopLeft;
                        break;
                    }else if (e.X <= mWidth)
                    {
                        //left edge
                        c.Cursor = Cursors.VSplit;
                        mEdge = EdgeEnum.Left;
                        break;

                    }else if(e.X > c.Width - (mWidth + 1))
                    {
                        //right edge
                        c.Cursor = Cursors.VSplit;
                        mEdge = EdgeEnum.Right;
                        break;
                    }else if(e.Y <= mWidth)
                    {
                        //top edge
                        c.Cursor = Cursors.HSplit;
                        mEdge = EdgeEnum.Top;
                        break;
                    }else if (e.Y > c.Height - (mWidth + 1))
                    {
                        //bottom edge
                        c.Cursor = Cursors.HSplit;
                        mEdge = EdgeEnum.Bottom;
                        break;
                    }else 
                    {
                        //no edge
                        c.Cursor = Cursors.Default;
                        mEdge = EdgeEnum.None;
                        break;
                    }
                    
 
                        
                }
            }
        }


        private void mControl_MouseLeave(object sender, System.EventArgs e)
        {
            Control c = (Control)sender;
            mEdge = EdgeEnum.None;
            c.Refresh();
        }
    }
}
