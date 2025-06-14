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
        Random rand = new Random();
        int delta; // delta tempo
        int initBraccioMainY; // posizione verticale braccio main
        int initBraccioMainX; // posizione orizzontale braccio main
        int initBraccio1Y; // posizione verticale braccio 1
        int initBraccio1X; // posizione orizzontale braccio 1
        int initBraccio2Y; // posizione verticale braccio 2
        int initBraccio2X; // posizione orizzontale braccio 2
        int initGancioX; // posizione orizzontale gancio
        int metaGancio;
        int initPianta1Y; //posizione verticale pianta
        int initPianta1X; //posizione orizzontale pianta
        int initPianta2X; //posizione orizzontale pianta 2
        int initPianta2Y; //posiziono orizzontale pianta 2
        int spostamentoBraccioY = 80; // spostamento in pixel verticale
        int spostamentoBraccioX = 240; // spostamento in pixel verticale
        bool prelievo1 = false; // prelievo pianta 1
        bool prelievo2 = false; // prelievo pianta 2;
        int duratay = 2000; // durata spostamento verticale
        int duratax = 2000;
        int ypos = 0; // delta spostamento verticale
        int xpos = 0; // delta spostamento orizzontale
        bool presa = false;
        bool sulRullo = false;
        bool consentiMovimento = false;
        //Variabili di controllo
        int crescita1; //indice di crescita pianta 1
        int crescita2; //indice di crescita pianta 2
        bool grasped1;
        bool grasped2;

        double temperatura;
        bool finestraAperta;
        bool condizionatoreOn;
        bool luceOn;
        int deltaTemp = 5;
        double tempOttimale = 36;
        int tickPerGiorno;
        int cambioMese;
        bool giornataCorta;
        int tickLuce;

        int altezzaPianta;
        int maxHeightBraccio;
        int FCS;
        int FCP;
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

            metaGancio = (int) Gancio.Width / 2;

            FCS = (int) ImmaginePianta1.Left + (ImmaginePianta1.Width / 2);
            FCP = (int) ImmaginePianta2.Left + (ImmaginePianta2.Width / 2);
            FCD = (int) Gancio.Left + metaGancio;
            altezzaPianta = ImmaginePianta1.Top;

            // Raccolta posizione iniziale piante
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

            Reset_Sensori();

            // Raccolta lista attuatori
            attuatori = this.Controls
            .OfType<TextBox>()
            .Where(tb => tb.Name.StartsWith("Attuat"))
            .ToList();

            Reset_Attuatori();

            coloreTubi = TuboVasca.BackColor;
            Flusso.Visible = false;
            Flusso2.Visible = false;
            tubi = this.Controls
            .OfType<PictureBox>()
            .Where(tb => tb.Name.StartsWith("Tubo"))
            .ToList();

            crescita1 = 1;
            crescita2 = 1;
            grasped1 = false;
            grasped2 = false;
            maxHeightBraccio = BraccioMain.Top;
            maxAperturaBraccio = Braccio2.Left - Braccio1.Left;
            minAperturaBraccio = ImmaginePianta1.Width + Braccio1.Width;

            altezzaVasca = Vasca.Height;
            altezzaSerbatoio = Acqua.Height;

            finestraAperta = false;
            condizionatoreOn = false;
            Condizionatore_On_Off(false);
            Finestra_Open_Close(false);
            luceOn = true;
            Luce_Check();
            luceOn = false;

            int temp = 5;
            int delta = Temperatura.Height - temp;
            Temperatura.Height = (int) temp;
            Temperatura.Top += (int) delta;
            temperatura = temp;

            tickPerGiorno = 0;
            cambioMese = 0;
            Calendario.Value = new DateTime(2025, 1, 1);
            giornataCorta = true;
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
            Reset_Attuatori();
            Reset_Tubi();

            Flusso.Visible = false;
            Flusso2.Visible = false;
            svuotamento = false;
            grasped1 = false;
            grasped2 = false;
            prelievo1 = false;
            prelievo2 = false;
            presa = false;
            sulRullo = false;
            consentiMovimento = false;

            Laser1.BringToFront();
            Laser2.BringToFront();

            finestraAperta = false;
            condizionatoreOn = false;
            Condizionatore_On_Off(false);
            Finestra_Open_Close(false);
            luceOn = true;
            Lampada_Check();
            luceOn = false;

            int temp = 5;
            int delta = Temperatura.Height - temp;
            Temperatura.Height = (int) temp;
            Temperatura.Top += (int) delta;
            temperatura = temp;

            tickPerGiorno = 0;
            cambioMese = 0;
            Calendario.Value = new DateTime(2025, 1, 1);
            giornataCorta = true;
        }

        // **** MASTER TIMER ****
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            Svuotamento_Vasca();
            Serbatoio_Svuota();
            Serbatoio_Riempi();

            Update_Data();
            Lampada_Check();

            Temperatura_Check();
            Finestra_Check();
            Condizionatore_Check();

            // Crescita pianta
            double probabilita = Probabilita_Crescita();
            if (Prob_Evento(probabilita) && crescita1 < 3)
            {
                crescita1++;
                Crescita_Pianta(ImmaginePianta1, 1);
            }
            if(Prob_Evento(probabilita) && crescita2 < 3)
            {
                crescita2++;
                Crescita_Pianta(ImmaginePianta2, 2);
            }

            Laser_Check();

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
                int dist;
                if (initBraccioMainY < altezzaPianta - BraccioMain.Height)
                {
                    dist = (altezzaPianta - BraccioMain.Height) - initBraccioMainY;
                    if (dist >= 0 && dist <= xpos)
                    {
                        xpos = dist;
                    }
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
                int dist;
                if (initBraccioMainY > maxHeightBraccio)
                {
                    dist = initBraccioMainY - maxHeightBraccio;
                    if (dist >= 0 && dist <= xpos)
                    {
                        xpos = dist;
                    }
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
                int dist;
                if ( (prelievo1 && (initGancioX + metaGancio) > FCS) || (prelievo2 && (initGancioX + metaGancio) > FCP) )
                {
                    if (prelievo1)
                    {
                        dist = (initGancioX + metaGancio) - FCS;
                        if (dist >= 0 && dist <= xpos)
                        {
                            xpos = dist;
                        }       
                    }
                    else if (prelievo2)
                    {
                        dist = (initGancioX + metaGancio) - FCP;
                        if (dist >= 0 && dist <= xpos)
                        {
                            xpos = dist;
                        }
                    }
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
                // OPZIONALE : SE AUTOMATIZZIAMO IL BRACCIO PER MUOVERLO SOLO IN CASO DI CRESCITA PIANTE NON SERVE
                else if (!prelievo1 && !prelievo2 && (initGancioX + metaGancio) > FCS)
                {
                    dist = (initGancioX + metaGancio) - FCS;
                    if (dist >= 0 && dist <= xpos)
                    {
                        xpos = dist;
                    }
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
                int dist;
                if ((initGancioX + metaGancio) < FCD)
                {
                    dist = FCD - (initGancioX + metaGancio);
                    if (dist >= 0 && dist <= xpos)
                    {
                        xpos = dist;
                    }
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
                int dist;
                if ((initBraccio2X - initBraccio1X) > minAperturaBraccio)
                {
                    dist = (initBraccio2X - initBraccio1X) - minAperturaBraccio;
                    if (dist >= 0 && dist <= xpos)
                    {
                        xpos = dist;
                    }
                    Braccio1.Left = (int) initBraccio1X + xpos / 2;
                    Braccio2.Left = (int) initBraccio2X - xpos / 2;
                    BraccioMain.Width -= xpos;
                    BraccioMain.Left += xpos / 2;
                }
            }
            else if (AttuatBraccioRilascio.Text == "True")
            {
                int dist;
                if ((initBraccio2X - initBraccio1X) < maxAperturaBraccio)
                {
                    dist = maxAperturaBraccio - (initBraccio2X - initBraccio1X);
                    if (dist >= 0 && dist <= xpos)
                    {
                        xpos = dist;
                    }
                    Braccio1.Left = (int) initBraccio1X - xpos / 2;
                    Braccio2.Left = (int) initBraccio2X + xpos / 2;
                    BraccioMain.Width += xpos;
                    BraccioMain.Left -= xpos / 2;
                }
            }
            
            if (prelievo1)
            {
                Movimento_Rullo(initPianta1X, ImmaginePianta1);
            }
            else if (prelievo2)
            {
                Movimento_Rullo(initPianta2X, ImmaginePianta2);
            }

            // Gestione sensori
            Check_Sensori();

            if (initBraccio1X <= (initGancioX + metaGancio) - (statoInizialeBraccio[2] / 2))
            {
                grasped1 = false;
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

            SensoreFCS.Text = (prelievo1 && (initGancioX + metaGancio) <= FCS) ? "True" : "False";
            if (SensoreFCS.Text == "True")
            {
                //Console.WriteLine("Pos Gancio = " + (Gancio.Left + metaGancio));
                //Console.WriteLine("FCS = " + FCS);
            }
            SensoreFCS.ForeColor = (prelievo1 && (initGancioX + metaGancio) <= FCS) ? Color.Red : Color.Black;

            SensoreFineCorsaPianta2.Text = (prelievo2 && (initGancioX + metaGancio) <= FCP) ? "True" : "False";
            if (SensoreFineCorsaPianta2.Text == "True")
            {
                //Console.WriteLine("Pos Gancio = " + (Gancio.Left + metaGancio));
                //Console.WriteLine("FCP = " + FCP);
            }
            SensoreFineCorsaPianta2.ForeColor = (prelievo2 && (initGancioX + metaGancio) <= FCP) ? Color.Red : Color.Black;

            SensoreFCD.Text = (initGancioX + (Gancio.Width / 2)) < FCD ? "False" : "True";
            if (SensoreFCD.Text == "True")
            {
                //Console.WriteLine("Pos Gancio = " + (Gancio.Left + metaGancio));
                //Console.WriteLine("FCD = " + FCD);
            }
            SensoreFCD.ForeColor = (initGancioX + metaGancio) < FCD ? Color.Black : Color.Red;

            if ((initBraccio2X - initBraccio1X) <= minAperturaBraccio && SensoreFCBottom.Text == "True")
            {
                if(SensoreFCS.Text == "True")
                {
                    SensoreGrasp.Text = "True";
                    SensoreGrasp.ForeColor = Color.Red;
                    grasped1 = true;
                }
                else if(SensoreFineCorsaPianta2.Text == "True")
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

            if (grasped1 && AttuatBraccioRilascio.Text == "True"  && (initBraccio2X - initBraccio1X) > minAperturaBraccio)
            {
                SensoreRelease.Text = "True";
                SensoreRelease.ForeColor = Color.Red;
            }
            else
            {
                SensoreRelease.Text = "False";
                SensoreRelease.ForeColor = Color.Black;
            }

            SensoreRulloOccupato.Text = sulRullo ? "True" : "False";
            SensoreRulloOccupato.ForeColor = sulRullo ? Color.Red : Color.Black;
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
                attuatore.Text = "False";
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
        private void Movimento_Rullo(int posPianta, PictureBox pianta)
        {
            if ((pianta.Top + pianta.Height >= Rullo.Top) && (pianta.Left >= Rullo.Left))
            {
                sulRullo = true;
            }
            if (sulRullo && SensoreFCTop.Text == "True")
            {
                consentiMovimento = true;
            }
            if (sulRullo && consentiMovimento)
            {
                AttuatRullo.Text = "True";
                AttuatRullo.ForeColor = Color.Red;
                pianta.Left = posPianta + xpos;

                if (pianta.Right >= this.ClientSize.Width)
                {
                    AttuatRullo.Text = "False";
                    AttuatRullo.ForeColor = Color.Black;
                    Rigenera_Pianta(pianta);
                    sulRullo = false;
                    consentiMovimento = false;
                }
            }
        }

        // **** GESTIONE SMALTIMENTO E RIPOSIZIONAMENTO PIANTA ****
        private void Rigenera_Pianta(PictureBox pianta)
        {
            // Rimuovila "visivamente"
            pianta.Visible = false;

            // ... Poi "ricreala" nel punto iniziale
            int numPianta = prelievo1 ? 1 : 2;
            pianta.Left = statoInizialePianta[numPianta*2 - 2];
            pianta.Top = statoInizialePianta[numPianta*2 - 1];

            crescita1 = prelievo1 ? 1 : crescita1;
            crescita2 = prelievo2 ? 1 : crescita2;

            Crescita_Pianta(pianta, numPianta);

            if (prelievo1)
            {
                prelievo1 = false;
                SensorePianta1Pronta.Text = "False";
                SensorePianta1Pronta.ForeColor = Color.Black;
                Laser1.BringToFront();
            }
            if (prelievo2)
            {
                prelievo2 = false;
                SensorePianta2Pronta.Text = "False";
                SensorePianta2Pronta.ForeColor = Color.Black;
                Laser2.BringToFront();
            }

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
            else if (numPianta == 2)
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
            AttuatBraccioSu.ForeColor = Color.Red;
            AttuatBraccioSx.Text = "False";
            AttuatBraccioSx.ForeColor = Color.Black;
            AttuatBraccioDx.Text = "False";
            AttuatBraccioDx.ForeColor = Color.Black;
            AttuatBraccioGiu.Text = "False";
            AttuatBraccioGiu.ForeColor = Color.Black;
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "False";
            AttuatBraccioSu.ForeColor = Color.Black;
            AttuatBraccioSx.Text = "True";
            AttuatBraccioSx.ForeColor = Color.Red;
            AttuatBraccioDx.Text = "False";
            AttuatBraccioDx.ForeColor = Color.Black;
            AttuatBraccioGiu.Text = "False";
            AttuatBraccioGiu.ForeColor = Color.Black;
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "False";
            AttuatBraccioSu.ForeColor = Color.Black;
            AttuatBraccioSx.Text = "False";
            AttuatBraccioSx.ForeColor = Color.Black;
            AttuatBraccioDx.Text = "True";
            AttuatBraccioDx.ForeColor = Color.Red;
            AttuatBraccioGiu.Text = "False";
            AttuatBraccioGiu.ForeColor = Color.Black;
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            Reset_PresaRilascio_Braccio();

            AttuatBraccioSu.Text = "False";
            AttuatBraccioSu.ForeColor = Color.Black;
            AttuatBraccioSx.Text = "False";
            AttuatBraccioSx.ForeColor = Color.Black;
            AttuatBraccioDx.Text = "False";
            AttuatBraccioDx.ForeColor = Color.Black;
            AttuatBraccioGiu.Text = "True";
            AttuatBraccioGiu.ForeColor = Color.Red;
        }

        private void Presa_Rilascio_Click(object sender, EventArgs e)
        {
            Reset_Comandi_Braccio();

            presa = !presa;
            AttuatBraccioPresa.Text = presa ? "True" : "False";
            AttuatBraccioPresa.ForeColor = presa ? Color.Red : Color.Black;
            AttuatBraccioRilascio.Text = presa ? "False" : "True";
            AttuatBraccioRilascio.ForeColor = presa ? Color.Black : Color.Red;
        }

        private void Reset_Comandi_Braccio()
        {
            AttuatBraccioSu.Text = "False";
            AttuatBraccioSu.ForeColor = Color.Black;
            AttuatBraccioSx.Text = "False";
            AttuatBraccioSx.ForeColor = Color.Black;
            AttuatBraccioDx.Text = "False";
            AttuatBraccioDx.ForeColor = Color.Black;
            AttuatBraccioGiu.Text = "False";
            AttuatBraccioGiu.ForeColor = Color.Black;
        }

        private void Reset_PresaRilascio_Braccio()
        {
            AttuatBraccioPresa.Text = "False";
            AttuatBraccioPresa.ForeColor = Color.Black;
            AttuatBraccioRilascio.Text = "False";
            AttuatBraccioRilascio.ForeColor = Color.Black;
        }

        private void Laser_Check()
        {
            bool prel1 = false;
            bool prel2 = false;
            if(crescita1 == 3)
            {
                Laser1.SendToBack();
                SensorePianta1Pronta.Text = "True";
                SensorePianta1Pronta.ForeColor = Color.Red;
                if (!prelievo2)
                {
                    prel1 = true;
                }
            }
            if(crescita2 == 3)
            {
                Laser2.SendToBack();
                SensorePianta1Pronta.Text = "True";
                SensorePianta2Pronta.ForeColor = Color.Red;
                if (!prelievo1)
                {
                    prel2 = true;
                }
            }

            // priorità alla pianta più vicina al rullo
            if (prel1 && prel2)
            {
                prelievo2 = true;
                prelievo1 = false;
            }
            else
            {
                prelievo1 = prel1;
                prelievo2 = prel2;
            }
        }

        private void Svuotamento_Vasca()
        {
            if (Vasca.Height > 0)
            {
                if (Prob_Evento(30))
                {
                    Vasca.Height -= 1;
                    Vasca.Top += 1;
                }
            }
            else
            {
                SensoreVascaPienaVuota.Text = "True";
                SensoreVascaPienaVuota.ForeColor = Color.Red;
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
                if (SensoreVascaPienaVuota.Text == "True")
                {
                    if (!svuotamento)
                    {
                        quantitaSerbatoio = Acqua.Height;
                        quantitaVasca = 0;
                        vasca_top = Vasca.Top;
                    }
                    svuotamento = true;
                    AttuatSvuotaSerbatoio.Text = "True";
                    AttuatSvuotaSerbatoio.ForeColor = Color.Red;

                    quantitaSerbatoio -= 2;
                    quantitaVasca += 2;
                    vasca_top -= 2;

                    Acqua.Height = quantitaSerbatoio;
                    Acqua.Top += 2;
                    Vasca.Height = quantitaVasca;
                    Vasca.Top = vasca_top;

                    TuboVasca.BackColor = Color.SkyBlue;
                    SensoreSerbatoioFull.Text = "False";
                    SensoreSerbatoioFull.ForeColor = Color.Black;
                }
                if (Vasca.Height >= altezzaVasca)
                {
                    svuotamento = false;
                    AttuatSvuotaSerbatoio.Text = "False";
                    AttuatSvuotaSerbatoio.ForeColor = Color.Black;

                    SensoreVascaPienaVuota.Text = "False";
                    SensoreVascaPienaVuota.ForeColor = Color.Black;
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
                Flusso2.Visible = true;
                AttuatRiempiSerbatoio.Text = "True";
                AttuatRiempiSerbatoio.ForeColor = Color.Red;

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
                Flusso2.Visible = false;
                serbatoioNonVuoto = true;
                AttuatRiempiSerbatoio.Text = "False";
                AttuatRiempiSerbatoio.ForeColor = Color.Black;

                SensoreSerbatoioFull.Text = "True";
                SensoreSerbatoioFull.ForeColor = Color.Red;
                Reset_Tubi();
            }
        }

        private bool Prob_Evento(double prob)
        {
            int probabilita = rand.Next(1, 101);
            return probabilita <= prob;
        }

        private void Finestra_Check()
        {
            double delta = 0.1;
            if (finestraAperta)
            {
                temperatura = temperatura - delta;
                Update_Temp();
            }
        }

        private void Condizionatore_Check()
        {
            double delta = 0.1;
            if (condizionatoreOn)
            {
                temperatura = temperatura + delta;
                Update_Temp();
            }
        }

        private void Update_Temp()
        {
            int temp = (int)temperatura;
            int altezza = Temperatura.Height - temp;
            Temperatura.Height = temp;
            Temperatura.Top += altezza;
        }

        private void Temperatura_Check()
        {
            if (temperatura < (tempOttimale - deltaTemp))
            {
                SensoreTempFredda.Text = "True";
                SensoreTempFredda.ForeColor = Color.Red;

                Condizionatore_On_Off(true);
                Finestra_Open_Close(false);
            }
            if (temperatura > (tempOttimale + deltaTemp))
            {
                SensoreTempCalda.Text = "True";
                SensoreTempCalda.ForeColor = Color.Red;

                Condizionatore_On_Off(false);
                Finestra_Open_Close(true);
            }
            if (temperatura >= (tempOttimale - deltaTemp) && temperatura <= (tempOttimale + deltaTemp))
            {
                SensoreTempFredda.Text = "False";
                SensoreTempFredda.ForeColor = Color.Black;
                SensoreTempCalda.Text = "False";
                SensoreTempCalda.ForeColor = Color.Black;

                Condizionatore_On_Off(false);
                Finestra_Open_Close(false);
            }
        }

        private void Condizionatore_On_Off(bool accendi)
        {
            if (accendi)
            {
                AttuatCondizionatore.Text = "True";
                AttuatCondizionatore.ForeColor = Color.Red;

                Conditioner.Image = Properties.Resources.Condizionatore_on;
                condizionatoreOn = true;
            }
            else
            {
                AttuatCondizionatore.Text = "False";
                AttuatCondizionatore.ForeColor = Color.Black;

                Conditioner.Image = Properties.Resources.Condizionatore_off;
                condizionatoreOn = false;
            }

            SensoreCondizionatore.Text = condizionatoreOn ? "True" : "False";
            SensoreCondizionatore.ForeColor = condizionatoreOn ? Color.Red : Color.Black;
        }

        private void Finestra_Open_Close(bool apri)
        {
            if (apri)
            {
                AttuatFinestra.Text = "True";
                AttuatFinestra.ForeColor = Color.Red;

                Finestra.Image = Properties.Resources.Finestra_open;
                finestraAperta = true;
            }
            else
            {
                AttuatFinestra.Text = "False";
                AttuatFinestra.ForeColor = Color.Black;

                Finestra.Image = Properties.Resources.Finestra_closed;
                finestraAperta = false;
            }

            SensoreFinestra.Text = finestraAperta ? "True" : "False";
            SensoreFinestra.ForeColor = finestraAperta ? Color.Red : Color.Black;
        }

        private void Update_Data()
        {
            int mese = Calendario.Value.Month;
            bool meseCambiato = false;
            if (tickPerGiorno >= 9)
            {
                Calendario.Value = Calendario.Value.AddDays(1);
                tickPerGiorno = 0;
            }
            else
            {
                tickPerGiorno++;
            }

            if (mese != Calendario.Value.Month)
            {
                cambioMese++;
                meseCambiato = true;
            }

            if (cambioMese >= 12)
            {
                cambioMese = 0;
            }
            if (cambioMese % 3 == 0 && meseCambiato)
            {
                Console.WriteLine("Mese_Chech()");
                temperatura = Mese_Check();
            }
            if (cambioMese % 6 == 0 && meseCambiato)
            {
                giornataCorta = Luce_Check();
            }
        }

        private int Mese_Check()
        {
            int mese = Calendario.Value.Month;

            switch (mese)
            {
                case 1: case 2: case 3:
                    return 5;
                case 4: case 5: case 6:
                    return 25;
                case 7: case 8: case 9:
                    return 67;
                case 10: case 11: case 12:
                    return 47;
                default: return 36;
            }
        }

        private bool Luce_Check()
        {
            int mese = Calendario.Value.Month;

            switch (mese)
            {
                case 1: case 2: case 3:
                case 4: case 5: case 6:
                    return true;
                case 7: case 8: case 9:
                case 10: case 11: case 12:
                    return false;
                default: return true;
            }
        }
        private void Lampada_Check()
        {
            if (giornataCorta && tickLuce <= 6 || !giornataCorta && tickLuce <= 9)
            {
                tickLuce++;
            }
            else
            {
                if (luceOn)
                {
                    AttuatLampada.Text = "False";
                    AttuatLampada.ForeColor = Color.Black;
                    Lampada.Image = Properties.Resources.Lampada_off;
                    luceOn = false;
                }
                else
                {
                    AttuatLampada.Text = "True";
                    AttuatLampada.ForeColor = Color.Red;
                    Lampada.Image = Properties.Resources.Lampada_on;
                    luceOn = true;
                }
                tickLuce = 0;
            }

            SensoreLuci.Text = luceOn ? "True" : "False";
            SensoreLuci.ForeColor = luceOn ? Color.Red : Color.Black;
        }

        private double Probabilita_Crescita()
        {
            double probVasca = Vasca.Height / altezzaVasca;
            double deltaT = Math.Abs(temperatura - tempOttimale);
            double probTemp = deltaT / tempOttimale;
            double probabilita = probVasca * probTemp;
            if (probabilita <= 0.33)
            {
                probabilita = 0.33;
            }
            return probabilita * 30;
        }
    }
}