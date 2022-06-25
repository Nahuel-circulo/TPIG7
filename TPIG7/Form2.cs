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
        private string texto;

        public string Texto { get => texto; set => texto = value; }

        public Form2()
        {
            InitializeComponent();
            textBox1.Text = texto;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            texto = textBox1.Text;
        }
    }
}
