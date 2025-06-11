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
        int[] statoInizialeBraccio = new int[6];
        int delta; // delta tempo
        int initBraccioMainY; // posizione verticale braccio main
        int initBraccioMainX; // posizione orizzontale braccio main
        int initBraccio1Y; // posizione verticale braccio 1
        int initBraccio1X; // posizione orizzontale braccio 1
        int initBraccio2Y; // posizione verticale braccio 2
        int initBraccio2X; // posizione orizzontale braccio 2
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
            statoInizialeBraccio[0] = BraccioMain.Left;
            statoInizialeBraccio[1] = BraccioMain.Top;
            statoInizialeBraccio[2] = Braccio1.Left;
            statoInizialeBraccio[3] = Braccio1.Top;
            statoInizialeBraccio[4] = Braccio2.Left;
            statoInizialeBraccio[5] = Braccio2.Top;
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

            BraccioMain.Left = statoInizialeBraccio[0];
            BraccioMain.Top =  statoInizialeBraccio[1];
            Braccio1.Left = statoInizialeBraccio[2];
            Braccio1.Top = statoInizialeBraccio[3];
            Braccio2.Left = statoInizialeBraccio[4];
            Braccio2.Top = statoInizialeBraccio[5];

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
            initBraccioMainX = BraccioMain.Left;
            initBraccioMainY = BraccioMain.Top;
            initBraccio1X = Braccio1.Left;
            initBraccio1Y = Braccio1.Top;
            initBraccio2X = Braccio2.Left;
            initBraccio2Y = Braccio2.Top;
            ypos = (int) spostamentoBraccioY * delta / duratay;
            xpos = (int)spostamentoBraccioX * delta / duratax;

            if(AttuatBraccioGiu.Text == "True")
            {
                BraccioMain.Top = initBraccioMainY + ypos;
                Braccio1.Top = initBraccio1Y + ypos;
                Braccio2.Top = initBraccio2Y + ypos;
            }
            else if (AttuatBraccioSu.Text == "True")
            {
                BraccioMain.Top = initBraccioMainY - ypos;
                Braccio1.Top = initBraccio1Y - ypos;
                Braccio2.Top = initBraccio2Y - ypos;
            }
            else if (AttuatBraccioSx.Text == "True")
            {
                BraccioMain.Left = initBraccioMainX - xpos;
                Braccio1.Left = initBraccio1X - xpos;
                Braccio2.Left = initBraccio2X - xpos;
            }
            else if (AttuatBraccioDx.Text == "True")
            {
                BraccioMain.Left = initBraccioMainX + xpos;
                Braccio1.Left = initBraccio1X + xpos;
                Braccio2.Left = initBraccio2X + xpos;
            }

            if (AttuatBraccioPresa.Text == "True")
            {
                Braccio1.Left = initBraccio1X + xpos;
                Braccio2.Left = initBraccio2X - xpos;
            }
            else if (AttuatBraccioRilascio.Text == "True")
            {
                Braccio1.Left = initBraccio1X - xpos;
                Braccio2.Left = initBraccio2X + xpos;
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
