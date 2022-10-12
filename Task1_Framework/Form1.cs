using System;
using System.Windows.Forms;
using Task1_Lib;

namespace Task1_Framework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Lib.OutPutLogic(textBox1.Text));
        }
    }
}
