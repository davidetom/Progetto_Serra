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
        int[] statoInizialeBraccio = new int[10];
        int[] statoInizialePianta = new int[2];
        int delta; // delta tempo
        int initBraccioMainY; // posizione verticale braccio main
        int initBraccioMainX; // posizione orizzontale braccio main
        int initBraccio1Y; // posizione verticale braccio 1
        int initBraccio1X; // posizione orizzontale braccio 1
        int initBraccio2Y; // posizione verticale braccio 2
        int initBraccio2X; // posizione orizzontale braccio 2
        int initGancioX; // posizione orizzontale gancio
        int initPiantaY; //posizione verticale pianta
        int initPiantaX; //posizione orizzontale pianta
        int spostamentoBraccioY = 80; // spostamento in pixel verticale
        int spostamentoBraccioX = 240; // spostamento in pixel verticale
        int duratay = 2000; // durata spostamento verticale
        int duratax = 2000;
        int ypos = 0; // delta spostamento verticale
        int xpos = 0; // delta spostamento orizzontale
        bool presa = false;
        bool sulRullo = false;
        bool consentiMovimento = false;
        //Variabili di controllo
        int crescita;
        bool grasped;

        int altezzaPianta;
        int maxHeightBraccio;
        int FCS;
        int FCD;
        int maxAperturaBraccio;
        int minAperturaBraccio;
        List<TextBox> sensori; // lista dei sensori
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Raccolta posizioni iniziali braccio robotico
            statoInizialeBraccio[0] = BraccioMain.Left;
            statoInizialeBraccio[1] = BraccioMain.Top;
            statoInizialeBraccio[2] = BraccioMain.Width;
            statoInizialeBraccio[3] = Braccio1.Left;
            statoInizialeBraccio[4] = Braccio1.Top;
            statoInizialeBraccio[5] = Braccio2.Left;
            statoInizialeBraccio[6] = Braccio2.Top;
            statoInizialeBraccio[7] = Gancio.Left;
            statoInizialeBraccio[8] = Gancio.Top;
            statoInizialeBraccio[9] = Gancio.Height;

            // Raccolta poszione iniziale pianta
            statoInizialePianta[0] = ImmaginePianta.Left;
            statoInizialePianta[1] = ImmaginePianta.Top;

            // Raccolta lista sensori
            sensori = this.Controls
            .OfType<TextBox>()
            .Where(tb => tb.Name.StartsWith("Sensore"))
            .ToList();

            crescita = 1;
            grasped = false;
            altezzaPianta = ImmaginePianta.Top;
            maxHeightBraccio = BraccioMain.Top;
            FCS = (int) ImmaginePianta.Left + (ImmaginePianta.Width / 2) - (Gancio.Width / 2);
            FCD = (int) Rullo.Left + (BraccioMain.Width / 2);
            maxAperturaBraccio = Braccio1.Left;
            minAperturaBraccio = ImmaginePianta.Left - (BraccioMain.Left + Braccio1.Width);
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
            BraccioMain.Width = statoInizialeBraccio[2];
            Braccio1.Left = statoInizialeBraccio[3];
            Braccio1.Top = statoInizialeBraccio[4];
            Braccio2.Left = statoInizialeBraccio[5];
            Braccio2.Top = statoInizialeBraccio[6];
            Gancio.Left = statoInizialeBraccio[7];
            Gancio.Top = statoInizialeBraccio[8];
            Gancio.Height = statoInizialeBraccio[9];

            ImmaginePianta.Left = statoInizialePianta[0];
            ImmaginePianta.Top = statoInizialePianta[1];
            crescita = 1;
            Crescita_Pianta();

            Reset_Sensori();

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
            // Crescita pianta
            Random rand = new Random();
            int probCrescita = rand.Next(1, 101);
            if (probCrescita < 10 && crescita < 3)
            {
                crescita++;
                Crescita_Pianta();
                Laser_Check();
            }

            // Movimento braccio e pianta
            delta = MasterTimer.Interval;
            initBraccioMainX = BraccioMain.Left;
            initBraccioMainY = BraccioMain.Top;
            initBraccio1X = Braccio1.Left;
            initBraccio1Y = Braccio1.Top;
            initBraccio2X = Braccio2.Left;
            initBraccio2Y = Braccio2.Top;
            initGancioX = Gancio.Left;
            initPiantaX = ImmaginePianta.Left;
            initPiantaY = ImmaginePianta.Top;

            // Calcolo velocità di spostamento
            ypos = (int)spostamentoBraccioY * delta / duratay;
            xpos = (int)spostamentoBraccioX * delta / duratax;

            if (AttuatBraccioGiu.Text == "True")
            {
                if (initBraccioMainY < altezzaPianta - BraccioMain.Height)
                {
                    BraccioMain.Top = initBraccioMainY + ypos;
                    Braccio1.Top = initBraccio1Y + ypos;
                    Braccio2.Top = initBraccio2Y + ypos;
                    Gancio.Height += ypos;
                    if (grasped)
                    {
                        ImmaginePianta.Top = initPiantaY + ypos;
                    }
                }
            }
            else if (AttuatBraccioSu.Text == "True")
            {
                if (initBraccioMainY > maxHeightBraccio)
                {
                    BraccioMain.Top = initBraccioMainY - ypos;
                    Braccio1.Top = initBraccio1Y - ypos;
                    Braccio2.Top = initBraccio2Y - ypos;
                    Gancio.Height -= ypos;
                    if (grasped)
                    {
                        ImmaginePianta.Top = initPiantaY - ypos;
                    }
                }
            }
            else if (AttuatBraccioSx.Text == "True")
            {
                if (initGancioX > FCS)
                {
                    BraccioMain.Left = initBraccioMainX - xpos;
                    Braccio1.Left = initBraccio1X - xpos;
                    Braccio2.Left = initBraccio2X - xpos;
                    Gancio.Left = initGancioX - xpos;
                    if (grasped)
                    {
                        ImmaginePianta.Left = initPiantaX - xpos;
                    }
                }
            }
            else if (AttuatBraccioDx.Text == "True")
            {
                if (initGancioX < FCD)
                {
                    BraccioMain.Left = initBraccioMainX + xpos;
                    Braccio1.Left = initBraccio1X + xpos;
                    Braccio2.Left = initBraccio2X + xpos;
                    Gancio.Left = initGancioX + xpos;
                    if (grasped)
                    {
                        ImmaginePianta.Left = initPiantaX + xpos;
                    }
                }
            }

            // Chiusura/Apertura braccio
            if (AttuatBraccioPresa.Text == "True")
            {
                if (initBraccio1X < ((initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2) + minAperturaBraccio))
                {
                    Braccio1.Left = (int) initBraccio1X + xpos / 2;
                    Braccio2.Left = (int) initBraccio2X - xpos / 2;
                    BraccioMain.Width -= xpos;
                    BraccioMain.Left += xpos / 2;
                }
            }
            else if (AttuatBraccioRilascio.Text == "True")
            {
                if (initBraccio1X > ((initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2)))
                {
                    Braccio1.Left = (int) initBraccio1X - xpos / 2;
                    Braccio2.Left = (int) initBraccio2X + xpos / 2;
                    BraccioMain.Width += xpos;
                    BraccioMain.Left -= xpos / 2;
                }
            }

            Movimento_Rullo(initPiantaX);

            // Gestione sensori
            Check_Sensori();

            if (initBraccio1X <= (initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2))
            {
                grasped = false;
            }
        }

        // **** SEZIONE SENSORI ****
        private void Check_Sensori()
        {
            SensoreFCBottom.Text = initBraccioMainY < altezzaPianta - BraccioMain.Height ? "False" : "True";
            SensoreFCBottom.ForeColor = initBraccioMainY < altezzaPianta - BraccioMain.Height ? Color.Black : Color.Red;

            SensoreFCTop.Text = initBraccioMainY > maxHeightBraccio ? "False" : "True";
            SensoreFCTop.ForeColor = initBraccioMainY > maxHeightBraccio ? Color.Black : Color.Red;

            SensoreFCS.Text = initGancioX > FCS ? "False" : "True";
            SensoreFCS.ForeColor = initGancioX > FCS ? Color.Black : Color.Red;

            SensoreFCD.Text = initGancioX < FCD ? "False" : "True";
            SensoreFCD.ForeColor = initGancioX < FCD ? Color.Black : Color.Red;

            if (initBraccio1X >= ((initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2) + minAperturaBraccio) && SensoreFCBottom.Text == "True")
            {
                SensoreGrasp.Text = "True";
                SensoreGrasp.ForeColor = Color.Red;
                grasped = true;

            }
            else
            {
                SensoreGrasp.Text = "False";
                SensoreGrasp.ForeColor = Color.Black;
            }

            if (grasped && AttuatBraccioRilascio.Text == "True"  && initBraccio1X > (initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2))
            {
                SensoreRelease.Text = "True";
                SensoreRelease.ForeColor = Color.Red;
            }
            else
            {
                SensoreRelease.Text = "False";
                SensoreRelease.ForeColor = Color.Black;
            }

            SensoreNastroOccupato.Text = sulRullo ? "True" : "False";
            SensoreNastroOccupato.ForeColor = sulRullo ? Color.Red : Color.Black;
        }

        private void Reset_Sensori()
        {
            foreach (TextBox sensore in sensori)
            {
                sensore.Text = "False";
                sensore.ForeColor = Color.Black;
            }

        }

        // **** GESTIONE RULLO ****
        private void Movimento_Rullo(int posPianta)
        {
            if ((ImmaginePianta.Top + ImmaginePianta.Height >= Rullo.Top) && (ImmaginePianta.Left >= Rullo.Left))
            {
                sulRullo = true;
            }
            if (sulRullo && SensoreFCTop.Text == "True")
            {
                consentiMovimento = true;
            }
            if (sulRullo && consentiMovimento)
            {
                ImmaginePianta.Left = posPianta + xpos;
                if (ImmaginePianta.Right >= this.ClientSize.Width)
                {
                    Rigenera_Pianta();
                    sulRullo = false;
                    consentiMovimento = false;
                }
            }
        }

        // **** GESTIONE SMALTIMENTO E RIPOSIZIONAMENTO PIANTA ****
        private void Rigenera_Pianta()
        {
            // Rimuovila "visivamente"
            ImmaginePianta.Visible = false;

            // ... Poi "ricreala" nel punto iniziale
            ImmaginePianta.Left = statoInizialePianta[0];
            ImmaginePianta.Top = statoInizialePianta[1];
            crescita = 1;
            Crescita_Pianta();
            ImmaginePianta.Visible = true;
        }

        private void Crescita_Pianta()
        {
            switch (crescita)
            {
                case 1:
                    ImmaginePianta.Image = Properties.Resources.Pianta1;
                    break;
                case 2:
                    ImmaginePianta.Image = Properties.Resources.Pianta2;
                    break;
                case 3:
                    ImmaginePianta.Image = Properties.Resources.Pianta3;
                    break;
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

        private void Laser_Check()
        {
            if(crescita == 3)
            {
                Laser.SendToBack();
                SensorePiantaPronta.Text = "True";
            }
        }

    }
}
