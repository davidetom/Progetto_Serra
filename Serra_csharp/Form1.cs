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
        bool presa = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // **** START, STOP, RESET ****
        private void StartButton_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = false;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = false;
            AttuatBraccioSu.Text = "";
            AttuatBraccioSx.Text = "";
            AttuatBraccioDx.Text = "";
            AttuatBraccioGiu.Text = "";
            AttuatBraccioPresa.Text = "";
            AttuatBraccioRilascio.Text = "";
        }

        // **** MASTER TIMER ****
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            delta = MasterTimer.Interval;
            initBraccioY = Braccio.Top;
            initBraccioX = Braccio.Left;
            ypos = (int) spostamentoBraccioY * delta / duratay;
            xpos = (int)spostamentoBraccioX * delta / duratax;

            if(AttuatBraccioGiu.Text == "True")
            {
                Braccio.Top = initBraccioY + ypos;
            }
            else if (AttuatBraccioSu.Text == "True")
            {
                Braccio.Top = initBraccioY - ypos;
            }
            else if (AttuatBraccioSx.Text == "True")
            {
                Braccio.Left = initBraccioX - xpos;
            }
            else if (AttuatBraccioDx.Text == "True")
            {
                Braccio.Left = initBraccioX + xpos;
            }

            if (AttuatBraccioPresa.Text == "True")
            {
                Braccio.Width -= xpos;
                Braccio.Left = (int) initBraccioX + (xpos/2);
            }
            else if (AttuatBraccioRilascio.Text == "True")
            {
                Braccio.Width += xpos;
                Braccio.Left = (int) initBraccioX - (xpos/2);
            }
        }

        // **** COMANDI BRACCIO ****

        private void UpButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "True";
            AttuatBraccioSx.Text = "False";
            AttuatBraccioDx.Text = "False";
            AttuatBraccioGiu.Text = "False";
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "False";
            AttuatBraccioSx.Text = "True";
            AttuatBraccioDx.Text = "False";
            AttuatBraccioGiu.Text = "False";
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "False";
            AttuatBraccioSx.Text = "False";
            AttuatBraccioDx.Text = "True";
            AttuatBraccioGiu.Text = "False";
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "False";
            AttuatBraccioSx.Text = "False";
            AttuatBraccioDx.Text = "False";
            AttuatBraccioGiu.Text = "True";
        }

        private void Presa_Rilascio_Click(object sender, EventArgs e)
        {
            Reset_Comandi_Braccio();

            if (!presa)
            {
                AttuatBraccioPresa.Text = "True";
                AttuatBraccioRilascio.Text = "False";
            }
            else
            {
                AttuatBraccioRilascio.Text = "True";
                AttuatBraccioPresa.Text = "False";
            }

            presa = !presa;
        }

        private void Reset_Comandi_Braccio()
        {
            AttuatBraccioSu.Text = "False";
            AttuatBraccioSx.Text = "False";
            AttuatBraccioDx.Text = "False";
            AttuatBraccioGiu.Text = "False";
        }

        private void Reset_PresaRilascio_Braccio()
        {
            AttuatBraccioPresa.Text = "";
            AttuatBraccioRilascio.Text = "";
        }
    }
}
