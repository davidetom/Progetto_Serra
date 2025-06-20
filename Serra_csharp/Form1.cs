using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace Serra_csharp
{
    public partial class Form1 : Form
    {
        Random rand = new Random(); // generatore random
        int delta; // delta tempo
        int spostamentoX = 240; // spostamento in pixel orizzontale
        int spostamentoY = 80; // spostamento in pixel verticale
        int durataX = 2000; // durata spostamento orizzontale
        int durataY = 2000; // durata spostamento verticale
        int ypos = 0; // delta spostamento verticale
        int xpos = 0; // delta spostamento orizzontale

        // Variabili di salvataggio
        int[] statoInizialeBraccio = new int[12];
        int[] statoInizialePianta = new int[4];
        int[] statoInizialeRecipienti = new int[4];

        // Variabili braccio
        int initBraccioMainX; // posizione verticale braccio main
        int initBraccioMainY; // posizione orizzontale braccio main
        int initBraccio1X; // posizione verticale braccio 1
        int initBraccio1Y; // posizione orizzontale braccio 1
        int initBraccio2X; // posizione verticale braccio 2
        int initBraccio2Y; // posizione orizzontale braccio 2
        int initCarrelloX; // posizione orizzontale carrello
        int initGancioX; // posizione orizzontale gancio
        int metaGancio;
        int centroBraccio; // posizione centrale del braccio
        int distanzaBracci; // larghezza apertura del braccio
        int maxAperturaBraccio; // apertura massima braccio
        int minAperturaBraccio; // apertura minima braccio
        bool prelievo1 = false; // per processo di prelievo pianta 1
        bool prelievo2 = false; // per processo di prelievo pianta 2;
        bool grasped1; // pianta 1 presa
        bool grasped2; // pianta 2 presa

        //Variabili fine corsa
        int FCS; // fine corsa sinistro (pianta 1)
        int FCP; // fine corsa pianta 2
        int FCD; // fine corsa destro
        int FCB; // fine corsa basso
        int FCT; // fine corsa alto

        // Variabili piante
        int initPianta1X; // posizione verticale pianta
        int initPianta1Y; // posizione orizzontale pianta
        int initPianta2X; // posizione orizzontale pianta 2
        int initPianta2Y; // posiziono orizzontale pianta 2
        int crescita1; // indice di crescita pianta 1
        int crescita2; // indice di crescita pianta 2

        // Variabili rullo
        bool sulRullo = false; // pianta sul rullo
        bool consentiMovimento = false; // pianta si può muovere

        // Variabili per gestione temperatura
        double temperatura;
        readonly double tempOttimale = 36; // temperatura ottimale
        bool condizionatoreOn; // condizionatore on/off
        bool ariaCalda; // aria calda o fredda

        // Variaili per gestione ossigeno
        double ossigeno;
        bool finestraAperta; // finestra aperta/chiusa

        // Variabili per gestione luce
        int durataGiorno; // tick per ore di luce

        // Variabili per gestione data
        int tickPerGiorno; // tick per giorno
        int cambioMese; // è cambiato il mese

        // Variabili serbatoio
        int altezzaSerbatoio; // altezza massima acqua
        bool svuotamento; // svuotamento attivo o no
        int quantitaSerbatoio; // altezza acqua
        bool riempimentoInCorso = false; // riempimento serbatoio
        int quanteVolteSvuota;
        int tempoSvuotamento;
        Color coloreTubi;

        // Variabili vasca
        int vasca_top; // posizione dell'acqua della vasca rispetto il bordo superiore
        int altezzaVasca; // altezza massima acqua
        int quantitaVasca; // altezza acqua

        // Liste per sensori, attuatori e tubi
        List<TextBox> sensori; // lista dei sensori
        List<TextBox> attuatori; // lista degli attuatori
        List<PictureBox> tubi; // lista dei tubi


        // INIZIO VARIABILI X TWINCAT
        private TcAdsClient tcClient;
        private int[] hConnect;
        private AdsStream dataStream;
        private AdsBinaryReader binRead;

        private int NUM_ELEMENTS_BOOL = 34;
        private int NUM_ELEMENTS_DOUBLE = 2;
        private int[] hvar_name;

        private string[] dataPLC = {"MAIN.SensoreStart", "MAIN.SensoreReset", "MAIN.SensoreFCBottom", "MAIN.SensoreFCTop", "MAIN.SensoreFCS",
                                    "MAIN.SensoreFCD", "MAIN.SensoreFCP", "MAIN.SensoreGrasp", "MAIN.SensoreRelease", "MAIN.SensoreRulloOccupato",
                                    "MAIN.SensoreVascaVuota", "MAIN.SensoreIsGiorno", "MAIN.SensoreSerbatoioOn", "MAIN.SensoreSerbatoioFull", "MAIN.SensoreSerbatoioEmpty",
                                    "MAIN.SensoreCondizionatore", "MAIN.SensoreApriFinestra", "MAIN.SensoreTempCalda", "MAIN.SensoreTempFredda", "MAIN.SensoreO2Low",
                                    "MAIN.SensorePianta1Pronta", "MAIN.SensorePianta2Pronta",
                                    "MAIN.AttuatBraccioGiu", "MAIN.AttuatBraccioSu", "MAIN.AttuatBraccioSx", "MAIN.AttuatBraccioDx", "MAIN.AttuatBraccioPresa",
                                    "MAIN.AttuatBraccioRilascio", "MAIN.AttuatRiempiSerbatoio", "MAIN.AttuatSvuotaSerbatoio", "MAIN.AttuatFinestra", "MAIN.AttuatCondizionatore",
                                    "MAIN.AttuatRullo", "MAIN.AttuatLampada",
                                    "MAIN.Temperatura", "MAIN.Ossigeno"};

        int delay_invio_temp_o2 = 0;
        // FINE VARIABILI X TWINCAT



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
            statoInizialeBraccio[10] = Carrello.Left;
            statoInizialeBraccio[11] = Carrello.Top;

            metaGancio = (int)Gancio.Width / 2;
            maxAperturaBraccio = Braccio2.Left - Braccio1.Left;
            minAperturaBraccio = ImmaginePianta1.Width + Braccio1.Width;
            grasped1 = false;
            grasped2 = false;

            // Calibrazione dei fine corsa
            FCS = (int)ImmaginePianta1.Left + (ImmaginePianta1.Width / 2);
            FCP = (int)ImmaginePianta2.Left + (ImmaginePianta2.Width / 2);
            FCD = (int)Gancio.Left + metaGancio;
            FCB = (int)ImmaginePianta1.Top - BraccioMain.Height;
            FCT = (int)BraccioMain.Top;

            // Raccolta posizione iniziale piante
            statoInizialePianta[0] = ImmaginePianta1.Left;
            statoInizialePianta[1] = ImmaginePianta1.Top;
            statoInizialePianta[2] = ImmaginePianta2.Left;
            statoInizialePianta[3] = ImmaginePianta2.Top;
            
            crescita1 = 1;
            crescita2 = 1;
            Crescita_Pianta(ImmaginePianta1, 1);
            Crescita_Pianta(ImmaginePianta2, 2);

            // Raccolta posizione iniziale vasca
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

            SensTemperatura.Text = "0°";
            SensOssigeno.Text = "0%";

            // Raccolta lista attuatori
            attuatori = this.Controls
            .OfType<TextBox>()
            .Where(tb => tb.Name.StartsWith("Attuat"))
            .ToList();

            Reset_Attuatori();

            // Raccolta lista tubi
            tubi = this.Controls
            .OfType<PictureBox>()
            .Where(tb => tb.Name.StartsWith("Tubo"))
            .ToList();

            Reset_Tubi();

            // Calibrazione serbatoio
            coloreTubi = TuboVasca.BackColor;
            Flusso.Visible = false;
            Flusso2.Visible = false;
            riempimentoInCorso = false;
            svuotamento = false;
            quanteVolteSvuota = 0;
            tempoSvuotamento = 0;

            // Calibrazione vasca
            altezzaVasca = Vasca.Height;
            altezzaSerbatoio = Acqua.Height;

            // Calibrazione temperatura
            temperatura = 36;
            Update_Temp();

            // Calibrazione condizionatore
            condizionatoreOn = false;
            Conditioner.Image = Properties.Resources.Condizionatore_off;
            AriaCaldaFredda.BackColor = Color.White;

            // Calibrazione ossigeno
            ossigeno = 100;
            Update_O2();

            // Calibrazione finestra
            finestraAperta = false;
            Finestra.Image = Properties.Resources.Finestra_closed;

            TempOx_Display();

            // Calibrazione luce
            Lampada.Image = Properties.Resources.Lampada_off;

            // Calibrazione calendario
            tickPerGiorno = 0;
            cambioMese = 0;
            Calendario.Value = new DateTime(2025, 1, 1);
        }

        // **** START, STOP, RESET ****
        private void StartButton_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = true;
            Attiva_Sensore("SensoreStart", true);
            Attiva_Sensore("SensoreReset", false);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = false;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            MasterTimer.Enabled = false;
            Attiva_Sensore("SensoreStart", true);
            Attiva_Sensore("SensoreReset", false);

            // Reset posizione braccio robotico
            BraccioMain.Left = statoInizialeBraccio[0];
            BraccioMain.Top = statoInizialeBraccio[1];
            BraccioMain.Width = statoInizialeBraccio[2];
            Braccio1.Left = statoInizialeBraccio[3];
            Braccio1.Top = statoInizialeBraccio[4];
            Braccio2.Left = statoInizialeBraccio[5];
            Braccio2.Top = statoInizialeBraccio[6];
            Gancio.Left = statoInizialeBraccio[7];
            Gancio.Top = statoInizialeBraccio[8];
            Gancio.Height = statoInizialeBraccio[9];
            Carrello.Left = statoInizialeBraccio[10];
            Carrello.Top = statoInizialeBraccio[11];

            grasped1 = false;
            grasped2 = false;
            prelievo1 = false;
            prelievo2 = false;

            // Reset rullo
            sulRullo = false;
            consentiMovimento = false;

            // Reset posizione piante
            ImmaginePianta1.Left = statoInizialePianta[0];
            ImmaginePianta1.Top = statoInizialePianta[1];
            ImmaginePianta2.Left = statoInizialePianta[2];
            ImmaginePianta2.Top = statoInizialePianta[3];
            crescita1 = 1;
            crescita2 = 1;
            Crescita_Pianta(ImmaginePianta1, 1);
            Crescita_Pianta(ImmaginePianta2, 2);

            //Reset laser
            Laser1.BringToFront();
            Laser2.BringToFront();

            // Reset vasca
            Acqua.Top = statoInizialeRecipienti[0];
            Acqua.Height = statoInizialeRecipienti[1];
            Vasca.Top = statoInizialeRecipienti[2];
            Vasca.Height = statoInizialeRecipienti[3];
            
            // Reset sensori, attuatori, tubi
            Reset_Sensori();
            Reset_Attuatori();
            Reset_Tubi();

            SensTemperatura.Text = "0°";
            SensOssigeno.Text = "0%";

            // Reset serbatoio
            Flusso.Visible = false;
            Flusso2.Visible = false;
            svuotamento = false;
            riempimentoInCorso = false;
            quanteVolteSvuota = 0;
            tempoSvuotamento = 0;

            // Reset temperatura
            temperatura = 36;
            Update_Temp();

            // Reset condizionatore
            condizionatoreOn = false;
            Conditioner.Image = Properties.Resources.Condizionatore_off;
            AriaCaldaFredda.BackColor = Color.White;

            // Reset ossigeno
            ossigeno = 100;
            Update_O2();

            // Reset finestra
            finestraAperta = false;
            Finestra.Image = Properties.Resources.Finestra_closed;

            TempOx_Display();

            // Reset luce
            Lampada.Image = Properties.Resources.Lampada_off;

            // Reset calendario
            tickPerGiorno = 0;
            cambioMese = 0;
            Calendario.Value = new DateTime(2025, 1, 1);
        }

        // **** MASTER TIMER ****
        private void MasterTimer_Tick(object sender, EventArgs e)
        {
            //Invio di temperatura e ossigeno
            Aggiorna_Temp_O2();

            // AGGIORNAMENTI DI STATO
            Svuotamento_Vasca();
            Serbatoio_Check();

            Update_Data();
            Ora_Check();
            Lampada_Check();

            Temperatura_Check();
            Condizionatore_Check();
            Update_Temp();

            Diminuzione_ossigenazione();
            Finestra_Check();
            Update_O2();

            TempOx_Display();

            // Crescita pianta
            double probabilita = Probabilita_Crescita();
            if (Prob_Evento(probabilita) && crescita1 < 3)
            {
                crescita1++;
                Crescita_Pianta(ImmaginePianta1, 1);
            }
            if (Prob_Evento(probabilita) && crescita2 < 3)
            {
                crescita2++;
                Crescita_Pianta(ImmaginePianta2, 2);
            }

            Laser_Check();

            // Posizioni braccio e pianta x movimento
            delta = MasterTimer.Interval;
            Posizioni_Velocita_Braccio_Piante();

            // Comandi braccio
            Movimento_Braccio();
            Check_FineCorsa_Braccio();
            Presa_Rilascio_Braccio();
            Check_Presa_Rilascio_Braccio();

            // Per movimento rullo
            if (prelievo1) Rullo_Check(initPianta1X, ImmaginePianta1);
            else if (prelievo2) Rullo_Check(initPianta2X, ImmaginePianta2);
        }

        // **** CALCOLO POSIZIONI E VELOCITA X BRACCIO E PIANTE ****
        private void Posizioni_Velocita_Braccio_Piante()
        {
            initBraccioMainX = BraccioMain.Left;
            initBraccioMainY = BraccioMain.Top;
            initBraccio1X = Braccio1.Left;
            initBraccio1Y = Braccio1.Top;
            initBraccio2X = Braccio2.Left;
            initBraccio2Y = Braccio2.Top;
            initGancioX = Gancio.Left;
            initCarrelloX = Carrello.Left;
            initPianta1X = ImmaginePianta1.Left;
            initPianta1Y = ImmaginePianta1.Top;
            initPianta2X = ImmaginePianta2.Left;
            initPianta2Y = ImmaginePianta2.Top;
            centroBraccio = initGancioX + metaGancio;
            distanzaBracci = initBraccio2X - initBraccio1X;

            // Calcolo velocità di spostamento
            xpos = (int)spostamentoX * delta / durataX;
            ypos = (int)spostamentoY * delta / durataY;
        }

        // **** GESTIONE MOVIMENTO BRACCIO ****
        private void Movimento_Braccio()
        {
            if (AttuatBraccioGiu.Text == "True")
            {
                int dist = FCB - initBraccioMainY;
                if (dist >= 0 && dist <= ypos) ypos = dist;
                Console.WriteLine("Dist Giù: " + dist);

                BraccioMain.Top = initBraccioMainY + ypos;
                Braccio1.Top = initBraccio1Y + ypos;
                Braccio2.Top = initBraccio2Y + ypos;
                Gancio.Height += ypos;

                ImmaginePianta1.Top = grasped1 ? initPianta1Y + ypos : initPianta1Y;
                ImmaginePianta2.Top = grasped2 ? initPianta2Y + ypos : initPianta2Y;
            }
            else if (AttuatBraccioSu.Text == "True")
            {
                int dist = initBraccioMainY - FCT;
                if (dist >= 0 && dist <= ypos) ypos = dist;

                BraccioMain.Top = initBraccioMainY - ypos;
                Braccio1.Top = initBraccio1Y - ypos;
                Braccio2.Top = initBraccio2Y - ypos;
                Gancio.Height -= ypos;

                ImmaginePianta1.Top = grasped1 ? initPianta1Y - ypos : initPianta1Y;
                ImmaginePianta2.Top = grasped2 ? initPianta2Y - ypos : initPianta2Y;
            }
            else if (AttuatBraccioSx.Text == "True")
            {
                int dist = 0;
                if (prelievo1)
                {
                    dist = centroBraccio - FCS;
                    if (dist >= 0 && dist <= xpos) xpos = dist; 
                }
                else if (prelievo2)
                {
                    dist = centroBraccio - FCP;
                    if (dist >= 0 && dist <= xpos) xpos = dist;
                }
                Console.WriteLine("Dist Sx: " + dist);

                BraccioMain.Left = initBraccioMainX - xpos;
                Braccio1.Left = initBraccio1X - xpos;
                Braccio2.Left = initBraccio2X - xpos;
                Gancio.Left = initGancioX - xpos;
                Carrello.Left = initCarrelloX - xpos;

                ImmaginePianta1.Left = grasped1 ? initPianta1X - xpos : initPianta1X;
                ImmaginePianta2.Left = grasped2 ? initPianta2X - xpos : initPianta2X;
            }
            else if (AttuatBraccioDx.Text == "True")
            {
                int dist;
                dist = FCD - centroBraccio;
                if (dist >= 0 && dist <= xpos) xpos = dist;

                BraccioMain.Left = initBraccioMainX + xpos;
                Braccio1.Left = initBraccio1X + xpos;
                Braccio2.Left = initBraccio2X + xpos;
                Gancio.Left = initGancioX + xpos;
                Carrello.Left = initCarrelloX + xpos;

                ImmaginePianta1.Left = grasped1 ? initPianta1X + xpos : initPianta1X;
                ImmaginePianta2.Left = grasped2 ? initPianta2X + xpos : initPianta2X;
            }
        }

        private void Check_FineCorsa_Braccio()
        {
            SensoreFCBottom.Text = initBraccioMainY < FCB ? "False" : "True";
            SensoreFCBottom.ForeColor = initBraccioMainY < FCB ? Color.Red : Color.Green;

            SensoreFCTop.Text = initBraccioMainY > FCT ? "False" : "True";
            SensoreFCTop.ForeColor = initBraccioMainY > FCT ? Color.Red : Color.Green;

            SensoreFCS.Text = (prelievo1 && centroBraccio <= FCS) ? "True" : "False";
            SensoreFCS.ForeColor = (prelievo1 && centroBraccio <= FCS) ? Color.Green : Color.Red;

            SensoreFineCorsaPianta2.Text = (prelievo2 && centroBraccio <= FCP) ? "True" : "False";
            SensoreFineCorsaPianta2.ForeColor = (prelievo2 && centroBraccio <= FCP) ? Color.Green : Color.Red;

            SensoreFCD.Text = centroBraccio < FCD ? "False" : "True";
            SensoreFCD.ForeColor = centroBraccio < FCD ? Color.Red : Color.Green;
        }

        // **** GESTIONE PRESA BRACCIO
        private void Presa_Rilascio_Braccio()
        {
            if (AttuatBraccioPresa.Text == "True")
            {
                int dist = distanzaBracci - minAperturaBraccio;
                if (dist >= 0 && dist <= xpos) xpos = dist;

                Braccio1.Left = (int)initBraccio1X + xpos / 2;
                Braccio2.Left = (int)initBraccio2X - xpos / 2;
                BraccioMain.Width -= xpos;
                BraccioMain.Left += xpos / 2;
            }
            else if (AttuatBraccioRilascio.Text == "True")
            {
                int dist = maxAperturaBraccio - distanzaBracci;
                if (dist >= 0 && dist <= xpos) xpos = dist;

                Braccio1.Left = (int)initBraccio1X - xpos / 2;
                Braccio2.Left = (int)initBraccio2X + xpos / 2;
                BraccioMain.Width += xpos;
                BraccioMain.Left -= xpos / 2;
            }
        }

        private void Check_Presa_Rilascio_Braccio()
        {
            // Presa effettuata su pianta 1 o 2
            if (distanzaBracci <= minAperturaBraccio)
            {
                if (prelievo1 || prelievo2)
                {
                    Attiva_Sensore("SensoreGrasp", true);
                    grasped1 = prelievo1;
                    grasped2 = prelievo2;
                }
            }

            // Rilascio pianta 1 o 2
            if ((grasped1 || grasped2) && AttuatBraccioRilascio.Text == "True" && distanzaBracci > minAperturaBraccio)
            {
                Attiva_Sensore("SensoreRelease", true);
                Attiva_Sensore("SensoreGrasp", false);

                if (distanzaBracci >= maxAperturaBraccio)
                {
                    grasped1 = false;
                    grasped2 = false;
                }
            }
            else
            {
                Attiva_Sensore("SensoreRelease", false);
            }
        }

        // **** GESTIONE RULLO ****
        private void Rullo_Check(int posPianta, PictureBox pianta)
        {
            if ((pianta.Top + pianta.Height >= Rullo.Top) && (pianta.Left >= Rullo.Left))
            {
                sulRullo = true;
                Attiva_Sensore("SensoreRulloOccupato", true);
            }
            if (sulRullo && AttuatRullo.Text == "True")
            {
                consentiMovimento = true;
            }
            if (AttuatRullo.Text == "False")
            {
                consentiMovimento = false;
            }

            Movimento_Rullo(posPianta, pianta);
        }

        private void Movimento_Rullo(int posPianta, PictureBox pianta)
        {
            if (sulRullo && consentiMovimento)
            {
                pianta.Left = posPianta + xpos;
            }

            if (pianta.Right >= this.ClientSize.Width)
            {
                Attiva_Sensore("SensoreRulloOccupato", false);
                Rigenera_Pianta(pianta);
                sulRullo = false;
            }
        }

        // **** SMALTIMENTO E RIPOSIZIONAMENTO PIANTA ****
        private void Rigenera_Pianta(PictureBox pianta)
        {
            // Rimuovila "visivamente"
            pianta.Visible = false;

            // ... Poi "ricreala" nel punto iniziale
            int numPianta = prelievo1 ? 1 : 2;
            pianta.Left = statoInizialePianta[numPianta * 2 - 2];
            pianta.Top = statoInizialePianta[numPianta * 2 - 1];

            crescita1 = prelievo1 ? 1 : crescita1;
            crescita2 = prelievo2 ? 1 : crescita2;

            Crescita_Pianta(pianta, numPianta);

            if (prelievo1)
            {
                Attiva_Sensore("SensorePianta1Pronta", false);
                prelievo1 = false;
                Laser1.BringToFront();
            }
            if (prelievo2)
            {
                Attiva_Sensore("SensorePianta2Pronta", false);
                prelievo2 = false;
                Laser2.BringToFront();
            }

            pianta.Visible = true;
        }

        // **** GESTIONE CRESCITA PIANTA ****
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

        // **** PROBABILITA' DI CRESCITA PIANTA
        private double Probabilita_Crescita()
        {
            double probVasca = Vasca.Height / altezzaVasca;
            double deltaT = Math.Abs(temperatura - tempOttimale);
            double probTemp = deltaT / tempOttimale;
            double probLuce = durataGiorno > 6 ? 1.0 : 0.5;
            double probO2;
            if (ossigeno >= 70)
            {
                probO2 = 1;
            }
            else if (ossigeno <= 30)
            {
                probO2 = 0.2;
            }
            else
            {
                probO2 = 0.5;
            }

            double probabilita = probVasca * probTemp * probLuce * probO2;
            if (probabilita <= 0.33)
            {
                probabilita = 0.33;
            }
            return probabilita * 30;
        }

        // **** GESTIONE LASER ****
        private void Laser_Check()
        {
            bool prel1 = false;
            bool prel2 = false;
            if (crescita1 == 3)
            {
                Laser1.SendToBack();
                if (!prelievo2)
                {
                    prel1 = true;
                }
            }
            if (crescita2 == 3)
            {
                Laser2.SendToBack();
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

            if (prelievo1)
            {
                Attiva_Sensore("SensorePianta1Pronta", true);
            }
            else if (prelievo2)
            {
                Attiva_Sensore("SensorePianta2Pronta", true);
            }
        }

        // **** GESTIONE ACQUA VASCA ****
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
                Attiva_Sensore("SensoreVascaVuota", true);
            }
        }

        // **** GESTIONE SERBATOIO ****
        private void Serbatoio_Check()
        {
            // Controllo serbatoio vuoto
            if (Acqua.Height <= 0)
            {
                Attiva_Sensore("SensoreSerbatoioEmpty", true);
            }
            if (Acqua.Height >= altezzaSerbatoio)
            {
                Attiva_Sensore("SensoreSerbatoioFull", true);
            }

            Serbatoio_Riempi();
            Serbatoio_Svuota();
        }

        private void Serbatoio_Svuota()
        {
            if (riempimentoInCorso) return;
            if (AttuatSvuotaSerbatoio.Text == "True")
            {
                if (!svuotamento)
                {
                    quantitaSerbatoio = Acqua.Height;
                    quantitaVasca = 0;
                    vasca_top = Vasca.Top;
                    quanteVolteSvuota++;
                    tempoSvuotamento = 0;
                }
                Attiva_Sensore("SensoreSerbatoioFull", false);
                Attiva_Sensore("SensoreVascaVuota", false);
                Attiva_Sensore("SensoreSerbatoioOn", true);
                svuotamento = true;
                tempoSvuotamento++;

                quantitaSerbatoio -= 2;
                quantitaVasca += 2;
                vasca_top -= 2;

                Acqua.Height = quantitaSerbatoio;
                Acqua.Top += 2;
                Vasca.Height = quantitaVasca;
                Vasca.Top = vasca_top;

                TuboVasca.BackColor = Color.SkyBlue;
            }
            if (Vasca.Height >= altezzaVasca)
            {
                Attiva_Sensore("SensoreSerbatoioOn", false);
                TuboVasca.BackColor = coloreTubi;
                if (quanteVolteSvuota >= 3)
                {
                    Acqua.Height = 0;
                    Acqua.Top = statoInizialeRecipienti[0] + altezzaSerbatoio;
                    quanteVolteSvuota = 0;
                }
            }
            if (AttuatSvuotaSerbatoio.Text == "False")
            {
                if (svuotamento && tempoSvuotamento >= 10)
                {
                    Vasca.Height = altezzaVasca;
                    Vasca.Top = statoInizialeRecipienti[2];
                }

                svuotamento = false;
                tempoSvuotamento = 0;
            }
        }

        private void Serbatoio_Riempi()
        {
            if (AttuatRiempiSerbatoio.Text == "True")
            {
                Attiva_Sensore("SensoreSerbatoioEmpty", false);
                riempimentoInCorso = true;
                Flusso.Visible = true;
                Flusso2.Visible = true;

                Acqua.Height += 2;
                Acqua.Top -= 2;
                foreach (PictureBox tubo in tubi)
                {
                    tubo.BackColor = Color.SkyBlue;
                }
                TuboVasca.BackColor = coloreTubi;
            }
            else if (AttuatRiempiSerbatoio.Text == "False")
            {
                riempimentoInCorso = false;
                Flusso.Visible = false;
                Flusso2.Visible = false;
                Reset_Tubi();
            }
        }

        // **** GESTIONE TEMPERATURA ****
        private void Update_Temperatura()
        {
            double temp = temperatura;
            double tempAmbiente = Mese_Check();

            if (finestraAperta)
            {
                temperatura = (temp + tempAmbiente) / 2;
            }
            else
            {
                temperatura = ((temp * 4) + (tempAmbiente * 1)) / 5;
            }
        }

        private void Temperatura_Check()
        {
            if (temperatura< (tempOttimale - 5))
            {
                Attiva_Sensore("SensoreTempFredda", true);
                ariaCalda = true;
            }
            if (temperatura > (tempOttimale + 5))
            {
                Attiva_Sensore("SensoreTempCalda", true);
                ariaCalda = false;
            }
            if (temperatura >= (tempOttimale - 5) && temperatura <= (tempOttimale + 5))
            {
                Attiva_Sensore("SensoreTempFredda", false);
                Attiva_Sensore("SensoreTempCalda", false);
            }

            Condizionatore_On_Off();
        }

        private void Condizionatore_On_Off()
        {
            if (AttuatCondizionatore.Text == "True")
            {
                Conditioner.Image = Properties.Resources.Condizionatore_on;
                Attiva_Sensore("SensoreCondizionatore", true);
                condizionatoreOn = true;

                AriaCaldaFredda.BackColor = ariaCalda ? Color.Red : Color.Blue;
            }
            if (AttuatCondizionatore.Text == "False")
            {
                Conditioner.Image = Properties.Resources.Condizionatore_off;
                Attiva_Sensore("SensoreCondizionatore", false);
                condizionatoreOn = false;

                AriaCaldaFredda.BackColor = Color.White;
            }
        }

        private void Condizionatore_Check()
        {
            double delta = ariaCalda ? 0.7 : -0.7;
            if (condizionatoreOn)
            {
                temperatura = temperatura + delta;
            }
        }

        // **** GESTIONE GRAFICA TERMOMETRO ****
        private void Update_Temp()
        {
            int temp = (int)temperatura;
            int altezza = Temperatura.Height - temp;
            Temperatura.Height = temp;
            Temperatura.Top += altezza;
        }

        // **** GESTIONE OSSIGENAZIONE ****
        private void Diminuzione_ossigenazione()
        {
            if (ossigeno > 0)
            {
                if (Prob_Evento(50))
                {
                    ossigeno -= 1;
                }
            }

            if (ossigeno <= 20)
            {
                Attiva_Sensore("SensoreO2Low", true);
                Attiva_Sensore("SensoreApriFinestra", true);
            }

            Finestra_Open_Close();
        }

        private void Finestra_Open_Close()
        {
            if (AttuatFinestra.Text == "True")
            {
                Finestra.Image = Properties.Resources.Finestra_open;
                finestraAperta = true;
                Attiva_Sensore("SensoreO2Low", false);
            }
            if (AttuatFinestra.Text == "False")
            {
                Finestra.Image = Properties.Resources.Finestra_closed;
                finestraAperta = false;
            }
        }

        private void Finestra_Check()
        {
            double delta = 5;

            if (finestraAperta)
            {
                ossigeno += delta;
            }

            if (ossigeno >= 100)
            {
                ossigeno = 100;
                Attiva_Sensore("SensoreApriFinestra", false);
            }
        }

        // **** GESTIONE GRAFICA MISURATORE O2 ****
        private void Update_O2()
        {
            int o2 = (int) (ossigeno * 0.72);
            int altezza = O2.Height - o2;
            O2.Height = (int)o2;
            O2.Top += (int)altezza;
        }

        // **** DISPLAY TEMPERATURA E OSSIGENO
        private void TempOx_Display()
        {
            double temp = temperatura * 0.695;
            Console.WriteLine("Temperatura: " + temp);
            SensTemperatura.Text = temp.ToString("F2") + "°";
            SensOssigeno.Text = ossigeno.ToString() + "%";
        }

        // **** GESTIONE DATA X TEMPERATURA E LUCE ****
        private void Update_Data()
        {
            int mese = Calendario.Value.Month;
            bool meseCambiato = false;
            if (tickPerGiorno >= 9)
            {
                Calendario.Value = Calendario.Value.AddDays(1);
                Update_Temperatura();
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

            if (meseCambiato)
            {
                Durata_Giornata_Check();
            }
        }

        private int Mese_Check()
        {
            int mese = Calendario.Value.Month;

            switch (mese)
            {
                case 1:
                case 2:
                case 3:
                    return 5;
                case 4:
                case 5:
                case 6:
                    return 25;
                case 7:
                case 8:
                case 9:
                    return 67;
                case 10:
                case 11:
                case 12:
                    return 47;
                default: return 36;
            }
        }

        // **** GESTIONE LAMPADA ****
        private void Durata_Giornata_Check()
        {
            int mese = Calendario.Value.Month;
            bool giornataCorta;

            switch (mese)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    giornataCorta = true;
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    giornataCorta = false;
                    break;
                default:
                    giornataCorta = false;
                    break;
            }

            durataGiorno = giornataCorta ? 3: 6;
        }

        private void Ora_Check()
        {
            if (tickPerGiorno <= durataGiorno)
            {
                Attiva_Sensore("SensoreIsGiorno", true);
            }
            else
            {
                Attiva_Sensore("SensoreIsGiorno", false);
            }
        }

        private void Lampada_Check()
        {
            if (AttuatLampada.Text == "True")
            {
                Lampada.Image = Properties.Resources.Lampada_on;
            }
            if (AttuatLampada.Text == "False")
            {
                Lampada.Image = Properties.Resources.Lampada_off;
            }
        }

        // **** GESTIONE PROBABILITA' DEGLI EVENTI ****
        private bool Prob_Evento(double prob)
        {
            int probabilita = rand.Next(1, 101);
            return probabilita <= prob;
        }

        // **** RESET SENSORI, ATTUATORI E TUBI ****
        private void Reset_Sensori()
        {
            foreach (TextBox sensore in sensori)
            {
                sensore.Text = "False";
                sensore.ForeColor = Color.Red;
            }
        }

        private void Reset_Attuatori()
        {
            foreach (TextBox attuatore in attuatori)
            {
                attuatore.Text = "False";
            }
        }

        private void Reset_Tubi()
        {
            foreach (PictureBox tubo in tubi)
            {
                tubo.BackColor = coloreTubi;
            }
        }

        // **** ATTIVA/DISATTIVA SENSORI ****
        private void Attiva_Sensore(string nomeSensore, bool attiva)
        {
            var sensore = sensori.FirstOrDefault(tb => tb.Name == nomeSensore);

            if (attiva)
            {
                sensore.Text = "True";
                sensore.ForeColor = Color.Green;
            }
            else
            {
                sensore.Text = "False";
                sensore.ForeColor = Color.Red;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////

        // **** CONNECTION ****
        private void ConnButton_Click(object sender, EventArgs e)
        {
            tcClient = new TcAdsClient();
            tcClient.Connect("127.0.0.1.1.1", 851);

            dataStream = new AdsStream(NUM_ELEMENTS_BOOL + NUM_ELEMENTS_DOUBLE*8);
            binRead = new AdsBinaryReader(dataStream);

            hConnect = new int[NUM_ELEMENTS_BOOL + NUM_ELEMENTS_DOUBLE];
            hConnect[0] = tcClient.AddDeviceNotification(dataPLC[0], dataStream, 0, 1, AdsTransMode.OnChange, 100, 0, SensoreStart);
            hConnect[1] = tcClient.AddDeviceNotification(dataPLC[1], dataStream, 1, 1, AdsTransMode.OnChange, 100, 0, SensoreReset);
            hConnect[2] = tcClient.AddDeviceNotification(dataPLC[2], dataStream, 2, 1, AdsTransMode.OnChange, 100, 0, SensoreFCBottom);
            hConnect[3] = tcClient.AddDeviceNotification(dataPLC[3], dataStream, 3, 1, AdsTransMode.OnChange, 100, 0, SensoreFCTop);
            hConnect[4] = tcClient.AddDeviceNotification(dataPLC[4], dataStream, 4, 1, AdsTransMode.OnChange, 100, 0, SensoreFCS);
            hConnect[5] = tcClient.AddDeviceNotification(dataPLC[5], dataStream, 5, 1, AdsTransMode.OnChange, 100, 0, SensoreFCD);
            hConnect[6] = tcClient.AddDeviceNotification(dataPLC[6], dataStream, 6, 1, AdsTransMode.OnChange, 100, 0, SensoreFineCorsaPianta2);
            hConnect[7] = tcClient.AddDeviceNotification(dataPLC[7], dataStream, 7, 1, AdsTransMode.OnChange, 100, 0, SensoreGrasp);
            hConnect[8] = tcClient.AddDeviceNotification(dataPLC[8], dataStream, 8, 1, AdsTransMode.OnChange, 100, 0, SensoreRelease);
            hConnect[9] = tcClient.AddDeviceNotification(dataPLC[9], dataStream, 9, 1, AdsTransMode.OnChange, 100, 0, SensoreRulloOccupato);
            hConnect[10] = tcClient.AddDeviceNotification(dataPLC[10], dataStream, 10, 1, AdsTransMode.OnChange, 100, 0, SensoreVascaVuota);
            hConnect[11] = tcClient.AddDeviceNotification(dataPLC[11], dataStream, 11, 1, AdsTransMode.OnChange, 100, 0, SensoreIsGiorno);
            hConnect[12] = tcClient.AddDeviceNotification(dataPLC[12], dataStream, 12, 1, AdsTransMode.OnChange, 100, 0, SensoreSerbatoioOn);
            hConnect[13] = tcClient.AddDeviceNotification(dataPLC[13], dataStream, 13, 1, AdsTransMode.OnChange, 100, 0, SensoreSerbatoioFull);
            hConnect[14] = tcClient.AddDeviceNotification(dataPLC[14], dataStream, 14, 1, AdsTransMode.OnChange, 100, 0, SensoreSerbatoioEmpty);
            hConnect[15] = tcClient.AddDeviceNotification(dataPLC[15], dataStream, 15, 1, AdsTransMode.OnChange, 100, 0, SensoreCondizionatore);
            hConnect[16] = tcClient.AddDeviceNotification(dataPLC[16], dataStream, 16, 1, AdsTransMode.OnChange, 100, 0, SensoreApriFinestra);
            hConnect[17] = tcClient.AddDeviceNotification(dataPLC[17], dataStream, 17, 1, AdsTransMode.OnChange, 100, 0, SensoreTempCalda);
            hConnect[18] = tcClient.AddDeviceNotification(dataPLC[18], dataStream, 18, 1, AdsTransMode.OnChange, 100, 0, SensoreTempFredda);
            hConnect[19] = tcClient.AddDeviceNotification(dataPLC[19], dataStream, 19, 1, AdsTransMode.OnChange, 100, 0, SensoreO2Low);
            hConnect[20] = tcClient.AddDeviceNotification(dataPLC[20], dataStream, 20, 1, AdsTransMode.OnChange, 100, 0, SensorePianta1Pronta);
            hConnect[21] = tcClient.AddDeviceNotification(dataPLC[21], dataStream, 21, 1, AdsTransMode.OnChange, 100, 0, SensorePianta2Pronta);
            hConnect[22] = tcClient.AddDeviceNotification(dataPLC[22], dataStream, 22, 1, AdsTransMode.OnChange, 100, 0, AttuatBraccioGiu);
            hConnect[23] = tcClient.AddDeviceNotification(dataPLC[23], dataStream, 23, 1, AdsTransMode.OnChange, 100, 0, AttuatBraccioSu);
            hConnect[24] = tcClient.AddDeviceNotification(dataPLC[24], dataStream, 24, 1, AdsTransMode.OnChange, 100, 0, AttuatBraccioSx);
            hConnect[25] = tcClient.AddDeviceNotification(dataPLC[25], dataStream, 25, 1, AdsTransMode.OnChange, 100, 0, AttuatBraccioDx);
            hConnect[26] = tcClient.AddDeviceNotification(dataPLC[26], dataStream, 26, 1, AdsTransMode.OnChange, 100, 0, AttuatBraccioPresa);
            hConnect[27] = tcClient.AddDeviceNotification(dataPLC[27], dataStream, 27, 1, AdsTransMode.OnChange, 100, 0, AttuatBraccioRilascio);
            hConnect[28] = tcClient.AddDeviceNotification(dataPLC[28], dataStream, 28, 1, AdsTransMode.OnChange, 100, 0, AttuatRiempiSerbatoio);
            hConnect[29] = tcClient.AddDeviceNotification(dataPLC[29], dataStream, 29, 1, AdsTransMode.OnChange, 100, 0, AttuatSvuotaSerbatoio);
            hConnect[30] = tcClient.AddDeviceNotification(dataPLC[30], dataStream, 30, 1, AdsTransMode.OnChange, 100, 0, AttuatFinestra);
            hConnect[31] = tcClient.AddDeviceNotification(dataPLC[31], dataStream, 31, 1, AdsTransMode.OnChange, 100, 0, AttuatCondizionatore);
            hConnect[32] = tcClient.AddDeviceNotification(dataPLC[32], dataStream, 32, 1, AdsTransMode.OnChange, 100, 0, AttuatRullo);
            hConnect[33] = tcClient.AddDeviceNotification(dataPLC[33], dataStream, 33, 1, AdsTransMode.OnChange, 100, 0, AttuatLampada);
            hConnect[34] = tcClient.AddDeviceNotification(dataPLC[34], dataStream, 34, 8, AdsTransMode.OnChange, 100, 0, SensTemperatura);
            hConnect[35] = tcClient.AddDeviceNotification(dataPLC[35], dataStream, 42, 8, AdsTransMode.OnChange, 100, 0, SensOssigeno);

            tcClient.AdsNotification += new AdsNotificationEventHandler(OnNotification);
            ConnText.Text = "Connnected!";

            hvar_name = new int[NUM_ELEMENTS_BOOL + NUM_ELEMENTS_DOUBLE];
            for (int i = 0; i < NUM_ELEMENTS_BOOL + NUM_ELEMENTS_DOUBLE; i++)
            {
                hvar_name[i] = tcClient.CreateVariableHandle(dataPLC[i]);
            }
        }

        // *** LETTURA DA TWINCAT ****
        private void OnNotification(object sender, AdsNotificationEventArgs e)
        {
            string strValue = "";
            if (e.NotificationHandle == hConnect[0]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[1]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[2]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[3]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[4]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[5]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[6]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[7]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[8]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[9]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[10]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[11]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[12]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[13]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[14]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[15]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[16]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[17]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[18]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[19]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[20]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[21]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[22]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[23]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[24]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[25]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[26]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[27]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[28]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[29]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[30]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[31]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[32]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[33]) strValue = binRead.ReadBoolean().ToString();
            else if (e.NotificationHandle == hConnect[34]) strValue = binRead.ReadDouble().ToString();
            else if (e.NotificationHandle == hConnect[35]) strValue = binRead.ReadDouble().ToString();

            ((TextBox)e.UserData).Invoke(new Action(() => ((TextBox)e.UserData).Text = String.Format(strValue)));
        }

        // **** SCRITTURA SU TWINCAT ****

        private void Aggiorna_Temp_O2()
        {
            delay_invio_temp_o2++;
            if (delay_invio_temp_o2 >= 5 && hvar_name != null)
            {
                try
                {
                    tcClient.WriteAny(hvar_name[34], (temperatura * 0.695));
                    tcClient.WriteAny(hvar_name[35], ossigeno);

                    delay_invio_temp_o2 = 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore invio ADS: " + ex.Message);
                }
            }
        }
        private void SensoreStart_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreStart.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[0], true);
                }
                else if (SensoreStart.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[0], false);
                }
            }
        }
        private void SensoreReset_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreReset.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[1], true);
                }
                else if (SensoreReset.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[1], false);
                }
            }
        }
        private void SensoreFCBottom_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreFCBottom.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[2], true);
                }
                else if (SensoreFCBottom.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[2], false);
                }
            }
        }
        private void SensoreFCTop_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreFCTop.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[3], true);
                }
                else if (SensoreFCTop.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[3], false);
                }
            }
        }
        private void SensoreFCS_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreFCS.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[4], true);
                }
                else if (SensoreFCS.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[4], false);
                }
            }
        }
        private void SensoreFCD_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreFCD.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[5], true);
                }
                else if (SensoreFCD.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[5], false);
                }
            }
        }
        private void SensoreFineCorsaPianta2_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreFineCorsaPianta2.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[6], true);
                }
                else if (SensoreFineCorsaPianta2.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[6], false);
                }
            }
        }
        private void SensoreGrasp_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreGrasp.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[7], true);
                }
                else if (SensoreGrasp.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[7], false);
                }
            }
        }
        private void SensoreRelease_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreRelease.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[8], true);
                }
                else if (SensoreRelease.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[8], false);
                }
            }
        }
        private void SensoreRulloOccupato_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreRulloOccupato.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[9], true);
                }
                else if (SensoreRulloOccupato.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[9], false);
                }
            }
        }
        private void SensoreVascaVuota_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreVascaVuota.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[10], true);
                }
                else if (SensoreVascaVuota.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[10], false);
                }
            }
        }
        private void SensoreIsGiorno_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreIsGiorno.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[11], true);
                }
                else if (SensoreIsGiorno.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[11], false);
                }
            }
        }
        private void SensoreSerbatoioOn_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreSerbatoioOn.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[12], true);
                }
                else if (SensoreSerbatoioOn.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[12], false);
                }
            }
        }
        private void SensoreSerbatoioFull_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreSerbatoioFull.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[13], true);
                }
                else if (SensoreSerbatoioFull.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[13], false);
                }
            }
        }
        private void SensoreSerbatoioEmpty_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreSerbatoioEmpty.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[14], true);
                }
                else if (SensoreSerbatoioEmpty.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[14], false);
                }
            }
        }
        private void SensoreCondizionatore_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreCondizionatore.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[15], true);
                }
                else if (SensoreCondizionatore.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[15], false);
                }
            }
        }
        private void SensoreApriFinestra_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreApriFinestra.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[16], true);
                }
                else if (SensoreApriFinestra.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[16], false);
                }
            }
        }
        private void SensoreTempCalda_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreTempCalda.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[17], true);
                }
                else if (SensoreTempCalda.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[17], false);
                }
            }
        }
        private void SensoreTempFredda_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreTempFredda.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[18], true);
                }
                else if (SensoreTempFredda.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[18], false);
                }
            }
        }
        private void SensoreO2Low_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensoreO2Low.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[19], true);
                }
                else if (SensoreO2Low.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[19], false);
                }
            }
        }
        private void SensorePianta1Pronta_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensorePianta1Pronta.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[20], true);
                }
                else if (SensorePianta1Pronta.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[20], false);
                }
            }
        }
        private void SensorePianta2Pronta_TextChanged(object sender, EventArgs e)
        {
            if (hvar_name != null)
            {
                if (SensorePianta2Pronta.Text.Equals("True"))
                {
                    tcClient.WriteAny(hvar_name[21], true);
                }
                else if (SensorePianta2Pronta.Text.Equals("False"))
                {
                    tcClient.WriteAny(hvar_name[21], false);
                }
            }
        }
    }
}