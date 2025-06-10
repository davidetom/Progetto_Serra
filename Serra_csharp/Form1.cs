using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serra_csharp
{
    public partial class Form1 : Form
    {
        int delta; // delta tempo
        int initBraccioY; // posizione verticale braccio
        int initBraccioX; // posizione orizzontale braccio
        int spostamentoBraccioY = 80; // spostamento in pixel verticale
        int spostamentoBraccioX = 80; // spostamento in pixel verticale
        int duratay = 2000; // durata spostamento verticale
        int duratax = 2000;
        int ypos = 0; // delta spostamento verticale
        int xpos = 0; // delta spostamento orizzontale
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            delta = MasterTimer.Interval;
            initBraccioY = Braccio.Top;
            initBraccioX = Braccio.Left;
            ypos = (int) spostamentoBraccioY * delta / duratay;
            xpos = (int)spostamentoBraccioX * delta / duratax;

            if(SensBraccioGiu.Text == "True")
            {
                Braccio.Top = initBraccioY + ypos;
            }
            else if (SensBraccioSu.Text == "True")
            {
                Braccio.Top = initBraccioY - ypos;
            }
            else if (SensBraccioSx.Text == "True")
            {
                Braccio.Left = initBraccioX - xpos;
            }
            else if (SensBraccioDx.Text == "True")
            {
                Braccio.Left = initBraccioX + xpos;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // **** COMANDI BRACCIO ****

        private void UpButton_Click(object sender, EventArgs e)
        {
            SensBraccioSu.Text = "True";
            SensBraccioSx.Text = "False";
            SensBraccioDx.Text = "False";
            SensBraccioGiu.Text = "False";
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            SensBraccioSu.Text = "False";
            SensBraccioSx.Text = "True";
            SensBraccioDx.Text = "False";
            SensBraccioGiu.Text = "False";
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            SensBraccioSu.Text = "False";
            SensBraccioSx.Text = "False";
            SensBraccioDx.Text = "True";
            SensBraccioGiu.Text = "False";
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            SensBraccioSu.Text = "False";
            SensBraccioSx.Text = "False";
            SensBraccioDx.Text = "False";
            SensBraccioGiu.Text = "True";
        }
    }
}
