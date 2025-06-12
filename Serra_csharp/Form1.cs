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
        int[] statoInizialePianta = new int[4];
        int[] statoInizialeRecipienti = new int[4];
        int delta; // delta tempo
        int initBraccioMainY; // posizione verticale braccio main
        int initBraccioMainX; // posizione orizzontale braccio main
        int initBraccio1Y; // posizione verticale braccio 1
        int initBraccio1X; // posizione orizzontale braccio 1
        int initBraccio2Y; // posizione verticale braccio 2
        int initBraccio2X; // posizione orizzontale braccio 2
        int initGancioX; // posizione orizzontale gancio
        int initPianta1Y; //posizione verticale pianta
        int initPianta1X; //posizione orizzontale pianta
        int initPianta2X; //posizione orizzontale pianta 2
        int initPianta2Y; //posiziono orizzontale pianta 2
        int spostamentoBraccioY = 80; // spostamento in pixel verticale
        int spostamentoBraccioX = 240; // spostamento in pixel verticale
        int duratay = 2000; // durata spostamento verticale
        int duratax = 2000;
        int ypos = 0; // delta spostamento verticale
        int xpos = 0; // delta spostamento orizzontale
        bool presa = false;
        bool sulRullo1 = false;
        bool sulRullo2 = false;
        bool consentiMovimento = false;
        //Variabili di controllo
        int crescita1; //indice di crescita pianta 1
        int crescita2; //indice di crescita pianta 2
        bool grasped1;
        bool grasped2;

        int altezzaPianta;
        int maxHeightBraccio;
        int FCS;
        int FCD;
        int maxAperturaBraccio;
        int minAperturaBraccio;
        int altezzaVasca;
        int altezzaSerbatoio;
        bool serbatoioNonVuoto = true;
        bool svuotamento = false;
        int quantitaSerbatoio;
        int quantitaVasca;
        int vasca_top;

        Color coloreTubi;
        List<TextBox> sensori; // lista dei sensori
        List<TextBox> attuatori; // lista degli attuatori
        List<PictureBox> tubi; // lista dei tubi
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

            // Raccolta posizione iniziale pianta
            statoInizialePianta[0] = ImmaginePianta1.Left;
            statoInizialePianta[1] = ImmaginePianta1.Top;
            statoInizialePianta[2] = ImmaginePianta2.Left;
            statoInizialePianta[3] = ImmaginePianta2.Top;

            // Raccolta posizione iniziale serbatoio e vasca
            statoInizialeRecipienti[0] = Acqua.Top;
            statoInizialeRecipienti[1] = Acqua.Height;
            statoInizialeRecipienti[2] = Vasca.Top;
            statoInizialeRecipienti[3] = Vasca.Height;

            // Raccolta lista sensori
            sensori = this.Controls
            .OfType<TextBox>()
            .Where(tb => tb.Name.StartsWith("Sensore"))
            .ToList();

            SensoreSerbatoioFull.Text = "True";
            SensoreSerbatoioFull.ForeColor = Color.Red;
            SensoreVascaPienaVuota.Text = "True";
            SensoreVascaPienaVuota.ForeColor = Color.Red;

            attuatori = this.Controls
            .OfType<TextBox>()
            .Where(tb => tb.Name.StartsWith("Attuat"))
            .ToList();


            coloreTubi = TuboVasca.BackColor;
            Flusso.Visible = false;
            tubi = this.Controls
            .OfType<PictureBox>()
            .Where(tb => tb.Name.StartsWith("Tubo"))
            .ToList();

            crescita1 = 1;
            crescita2 = 1;
            grasped1 = false;
            grasped2 = false;
            altezzaPianta = ImmaginePianta1.Top;
            maxHeightBraccio = BraccioMain.Top;
            FCS = (int) ImmaginePianta1.Left + (ImmaginePianta1.Width / 2) - (Gancio.Width / 2);
            FCD = (int) Rullo.Left + (BraccioMain.Width / 2);
            maxAperturaBraccio = Braccio1.Left;
            minAperturaBraccio = ImmaginePianta1.Left - (BraccioMain.Left + Braccio1.Width);

            altezzaVasca = Vasca.Height;
            altezzaSerbatoio = Acqua.Height;
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

            ImmaginePianta1.Left = statoInizialePianta[0];
            ImmaginePianta1.Top = statoInizialePianta[1];
            ImmaginePianta2.Left = statoInizialePianta[2];
            ImmaginePianta2.Top = statoInizialePianta[3];
            crescita1 = 1;
            crescita2 = 1;
            Crescita_Pianta(ImmaginePianta1, 1);
            Crescita_Pianta(ImmaginePianta2, 2);

            Acqua.Top = statoInizialeRecipienti[0];
            Acqua.Height = statoInizialeRecipienti[1];
            Vasca.Top = statoInizialeRecipienti[2];
            Vasca.Height = statoInizialeRecipienti[3];
            serbatoioNonVuoto = true;

            Reset_Sensori();
            SensoreSerbatoioFull.Text = "True";
            SensoreSerbatoioFull.ForeColor = Color.Red;
            SensoreVascaPienaVuota.Text = "True";
            SensoreVascaPienaVuota.ForeColor = Color.Red;

            Reset_Attuatori();
            Reset_Tubi();
            Flusso.Visible = false;

            AttuatBraccioSu.Text = "";
            AttuatBraccioSx.Text = "";
            AttuatBraccioDx.Text = "";
            AttuatBraccioGiu.Text = "";
            AttuatBraccioPresa.Text = "";
            AttuatBraccioRilascio.Text = "";
            grasped1 = false;
            grasped2 = false;
        }

        // **** MASTER TIMER ****
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            Svuotamento_Vasca();
            Serbatoio_Svuota();
            Serbatoio_Riempi();

            // Crescita pianta
            if (Prob_Evento(10) && crescita1 < 3)
            {
                crescita1++;
                Crescita_Pianta(ImmaginePianta1, 1);
                Laser_Check();
            }
            else if(Prob_Evento(10) && crescita2 < 3)
            {
                crescita2++;
                Crescita_Pianta(ImmaginePianta2, 2);
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
            initPianta1X = ImmaginePianta1.Left;
            initPianta1Y = ImmaginePianta1.Top;
            initPianta2X = ImmaginePianta2.Left;
            initPianta2Y = ImmaginePianta2.Top;

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
                    if (grasped1)
                    {
                        ImmaginePianta1.Top = initPianta1Y + ypos;
                    }
                    if (grasped2)
                    {
                        ImmaginePianta2.Top = initPianta2Y + ypos;
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
                    if (grasped1)
                    {
                        ImmaginePianta1.Top = initPianta1Y - ypos;
                    }
                    if (grasped2)
                    {
                        ImmaginePianta2.Top = initPianta2Y - ypos;
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
                    if (grasped1)
                    {
                        ImmaginePianta1.Left = initPianta1X - xpos;
                    }
                    if (grasped2)
                    {
                        ImmaginePianta2.Left = initPianta2X - xpos;
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
                    if (grasped1)
                    {
                        ImmaginePianta1.Left = initPianta1X + xpos;
                    }
                    if (grasped2)
                    {
                        ImmaginePianta2.Left = initPianta2X + xpos;
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

            Movimento_Rullo(initPianta1X, ImmaginePianta1, 1);
            Movimento_Rullo(initPianta2X, ImmaginePianta2, 2);

            // Gestione sensori
            Check_Sensori();

            if (initBraccio1X <= (initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2))
            {
                grasped1 = false;
            }
            if (initBraccio1X == (initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2))
            {
                grasped2 = false;
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
                if((initGancioX + Gancio.Width / 2) == (initPianta1X + ImmaginePianta1.Width / 2))
                {
                    SensoreGrasp.Text = "True";
                    SensoreGrasp.ForeColor = Color.Red;
                    grasped1 = true;
                }
                if((initGancioX + Gancio.Width / 2) == (initPianta2X + ImmaginePianta2.Width / 2))
                {
                    SensoreGrasp.Text = "True";
                    SensoreGrasp.ForeColor = Color.Red;
                    grasped2 = true;
                }


            }
            else
            {
                SensoreGrasp.Text = "False";
                SensoreGrasp.ForeColor = Color.Black;
            }

            if (grasped1 && AttuatBraccioRilascio.Text == "True"  && initBraccio1X > (initGancioX + Gancio.Width / 2) - (statoInizialeBraccio[2] / 2))
            {
                SensoreRelease.Text = "True";
                SensoreRelease.ForeColor = Color.Red;
            }
            else
            {
                SensoreRelease.Text = "False";
                SensoreRelease.ForeColor = Color.Black;
            }

            SensoreNastroOccupato.Text = sulRullo1 ? "True" : "False";
            SensoreNastroOccupato.ForeColor = sulRullo1 ? Color.Red : Color.Black;
        }

        private void Reset_Sensori()
        {
            foreach (TextBox sensore in sensori)
            {
                sensore.Text = "False";
                sensore.ForeColor = Color.Black;
            }
        }

        private void Reset_Attuatori()
        {
            foreach (TextBox attuatore in attuatori)
            {
                attuatore.Text = "";
                attuatore.ForeColor = Color.Black;
            }
        }

        private void Reset_Tubi()
        {
            foreach (PictureBox tubo in tubi)
            {
                tubo.BackColor = coloreTubi;
            }
        }

        // **** GESTIONE RULLO ****
        private void Movimento_Rullo(int posPianta, PictureBox pianta, int numPianta)
        {
            if ((pianta.Top + pianta.Height >= Rullo.Top) && (pianta.Left >= Rullo.Left))
            {
                sulRullo1 = true;
            }
            if (sulRullo1 && SensoreFCTop.Text == "True")
            {
                consentiMovimento = true;
            }
            if (sulRullo1 && consentiMovimento)
            {
                pianta.Left = posPianta + xpos;
                if (pianta.Right >= this.ClientSize.Width)
                {
                    Rigenera_Pianta(pianta, numPianta);
                    sulRullo1 = false;
                    consentiMovimento = false;
                }
            }
        }

        // **** GESTIONE SMALTIMENTO E RIPOSIZIONAMENTO PIANTA ****
        private void Rigenera_Pianta(PictureBox pianta, int numPianta)
        {
            // Rimuovila "visivamente"
            pianta.Visible = false;

            // ... Poi "ricreala" nel punto iniziale
            numPianta = numPianta * 2;
            pianta.Left = statoInizialePianta[numPianta-2];
            pianta.Top = statoInizialePianta[numPianta-1];
            if(numPianta == 1)
            {
                crescita1 = 1;
            }
            else
            {
                crescita2 = 1;
            }
            Crescita_Pianta(pianta, numPianta);
            pianta.Visible = true;
        }

        private void Crescita_Pianta(PictureBox pianta, int numPianta)
        {
            if (numPianta == 1)
            {
                switch (crescita1)
                {
                    case 1:
                        pianta.Image = Properties.Resources.Pianta1;
                        break;
                    case 2:
                        pianta.Image = Properties.Resources.Pianta2;
                        break;
                    case 3:
                        pianta.Image = Properties.Resources.Pianta3;
                        break;
                }
            }
            else
            {
                switch (crescita2)
                {
                    case 1:
                        pianta.Image = Properties.Resources.Pianta1;
                        break;
                    case 2:
                        pianta.Image = Properties.Resources.Pianta2;
                        break;
                    case 3:
                        pianta.Image = Properties.Resources.Pianta3;
                        break;
                }
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
            if(crescita1 == 3)
            {
                Laser1.SendToBack();
                SensorePianta1Pronta.Text = "True";
            }
            else if(crescita2 == 3)
            {
                Laser2.SendToBack();
                SensorePianta1Pronta.Text = "True";
            }
        }

        private void Svuotamento_Vasca()
        {
            if (Vasca.Height > 0)
            {
                if (Prob_Evento(50))
                {
                    Vasca.Height -= 1;
                    Vasca.Top += 1;
                }
            }
            else
            {
                SensoreVascaPienaVuota.Text = "False";
                SensoreVascaPienaVuota.ForeColor = Color.Black;
            }
        }

        private void Serbatoio_Svuota()
        {
            if (Acqua.Height <= 0)
            {
                serbatoioNonVuoto = false;
                return;
            }
            if (serbatoioNonVuoto)
            {
                if (SensoreVascaPienaVuota.Text == "False")
                {
                    if (!svuotamento)
                    {
                        quantitaSerbatoio = Acqua.Height;
                        quantitaVasca = 0;
                        vasca_top = Vasca.Top;
                    }
                    svuotamento = true;

                    quantitaSerbatoio -= 2;
                    quantitaVasca += 2;
                    vasca_top -= 2;

                    Acqua.Height = quantitaSerbatoio;
                    Acqua.Top += 2;
                    Console.WriteLine("Livello serbatoio: " + Acqua.Height);
                    Vasca.Height = quantitaVasca;
                    Vasca.Top = vasca_top;
                    Console.WriteLine("Livello Vasca: " + Vasca.Height);

                    TuboVasca.BackColor = Color.SkyBlue;
                    SensoreSerbatoioFull.Text = "False";
                    SensoreSerbatoioFull.ForeColor = Color.Black;
                }
                if (Vasca.Height >= altezzaVasca)
                {
                    svuotamento = false;
                    SensoreVascaPienaVuota.Text = "True";
                    SensoreVascaPienaVuota.ForeColor = Color.Red;
                    TuboVasca.BackColor = coloreTubi;
                }
            }
            if (Acqua.Height <= 5)
            {
                SensoreSerbatoioEmpty.Text = "True";
                SensoreSerbatoioEmpty.ForeColor = Color.Red;
            }
        }

        private void Serbatoio_Riempi()
        {
            if (!serbatoioNonVuoto)
            {
                Flusso.Visible = true;
                Acqua.Height += 2;
                Acqua.Top -= 2;
                foreach (PictureBox tubo in tubi)
                {
                    tubo.BackColor = Color.SkyBlue;
                }
                TuboVasca.BackColor = coloreTubi;
                SensoreSerbatoioEmpty.Text = "False";
                SensoreSerbatoioEmpty.ForeColor = Color.Black;
            }
            if (Acqua.Height >= altezzaSerbatoio)
            {
                Flusso.Visible = false;
                serbatoioNonVuoto = true;
                SensoreSerbatoioFull.Text = "True";
                SensoreSerbatoioFull.ForeColor = Color.Red;
                Reset_Tubi();
            }
        }

        private bool Prob_Evento(int prob)
        {
            Random rand = new Random();
            int probabilita = rand.Next(1, 101);
            return probabilita <= prob;
        }
    }
}