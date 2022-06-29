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
        public Control ControlConEventos;
        public Control MControl
        {
            get { return ControlConEventos; }
            set
            {
                if (ControlConEventos != null)
                {
                    ControlConEventos.MouseDown -= mControl_MouseDown;
                    ControlConEventos.MouseUp -= mControl_MouseUp;
                    ControlConEventos.MouseMove -= mControl_MouseMove;
                    ControlConEventos.MouseLeave -= mControl_MouseLeave;
                }

                ControlConEventos = value;

                if (ControlConEventos != null)
                {
                    ControlConEventos.MouseDown += mControl_MouseDown;
                    ControlConEventos.MouseUp += mControl_MouseUp;
                    ControlConEventos.MouseMove += mControl_MouseMove;
                    ControlConEventos.MouseLeave += mControl_MouseLeave;
                }
            }
        }

        private bool mouseClick = false;

        private EsquinasEnum esquinas = EsquinasEnum.None;
        private int mWidth = 4;

        private bool dibujoBorde = false;
        private enum EsquinasEnum
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


        private void mControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseClick = true;
            }
        }


        private void mControl_MouseUp(object sender, MouseEventArgs e)
        {
            mouseClick = false;
        }


        private void mControl_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Graphics g = control.CreateGraphics();
            switch (esquinas)
            {
                case EsquinasEnum.TopLeft:
                    g.FillRectangle(Brushes.Blue, 0, 0, mWidth * 4, mWidth * 4);
                    dibujoBorde = true;
                    break;
                case EsquinasEnum.Left:
                    g.FillRectangle(Brushes.Blue, 0, 0, mWidth, control.Height);
                    dibujoBorde = true;
                    break;
                case EsquinasEnum.Right:
                    g.FillRectangle(Brushes.Blue, control.Width - mWidth, 0, control.Width, control.Height);
                    dibujoBorde = true;
                    break;
                case EsquinasEnum.Top:
                    g.FillRectangle(Brushes.Blue, 0, 0, control.Width, mWidth);
                    dibujoBorde = true;
                    break;
                case EsquinasEnum.Bottom:
                    g.FillRectangle(Brushes.Blue, 0, control.Height - mWidth, control.Width, mWidth);
                    dibujoBorde = true;
                    break;
                case EsquinasEnum.None:
                    if (dibujoBorde)
                    {
                        control.Refresh();
                        dibujoBorde = false;
                    }
                    break;
            }

            if (mouseClick & esquinas != EsquinasEnum.None)
            {
                control.SuspendLayout();
                switch (esquinas)
                {
                    case EsquinasEnum.TopLeft:
                        control.SetBounds(control.Left + e.X, control.Top + e.Y, control.Width, control.Height);
                        break;
                    case EsquinasEnum.Left:
                        control.SetBounds(control.Left + e.X, control.Top, control.Width - e.X, control.Height);
                        break;
                    case EsquinasEnum.Right:
                        control.SetBounds(control.Left, control.Top, control.Width - (control.Width - e.X), control.Height);
                        break;
                    case EsquinasEnum.Top:
                        control.SetBounds(control.Left, control.Top + e.Y, control.Width, control.Height - e.Y);
                        break;
                    case EsquinasEnum.Bottom:
                        control.SetBounds(control.Left, control.Top, control.Width, control.Height - (control.Height - e.Y));
                        break;
                }
                control.ResumeLayout();
            }
            else
            {
                while (true)
                {
                    if (e.X <= (mWidth * 4) && e.Y <= (mWidth * 4))
                    {
                        //esquina superior izquierda
                        control.Cursor = Cursors.SizeAll;
                        esquinas = EsquinasEnum.TopLeft;
                        break;
                    }
                    else if (e.X <= mWidth)
                    {
                        //esquina izquierda
                        control.Cursor = Cursors.VSplit;
                        esquinas = EsquinasEnum.Left;
                        break;

                    }
                    else if (e.X > control.Width - (mWidth + 1))
                    {
                        //esquina derecha
                        control.Cursor = Cursors.VSplit;
                        esquinas = EsquinasEnum.Right;
                        break;
                    }
                    else if (e.Y <= mWidth)
                    {
                        //esquina superior
                        control.Cursor = Cursors.HSplit;
                        esquinas = EsquinasEnum.Top;
                        break;
                    }
                    else if (e.Y > control.Height - (mWidth + 1))
                    {
                        //esquina inferior
                        control.Cursor = Cursors.HSplit;
                        esquinas = EsquinasEnum.Bottom;
                        break;
                    }
                    else
                    {
                        // sin esquina seleccionada
                        control.Cursor = Cursors.Default;
                        esquinas = EsquinasEnum.None;
                        break;
                    }



                }
            }
        }


        private void mControl_MouseLeave(object sender, System.EventArgs e)
        {
            Control c = (Control)sender;
            esquinas = EsquinasEnum.None;
            c.Refresh();
        }
    }
}
