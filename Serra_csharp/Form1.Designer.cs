
namespace Serra_csharp
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.AttuatBraccioGiu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MasterTimer = new System.Windows.Forms.Timer(this.components);
            this.UpButton = new System.Windows.Forms.Button();
            this.RightButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.AttuatBraccioSx = new System.Windows.Forms.TextBox();
            this.AttuatBraccioSu = new System.Windows.Forms.TextBox();
            this.AttuatBraccioDx = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.AttuatBraccioRilascio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.AttuatBraccioPresa = new System.Windows.Forms.TextBox();
            this.Presa_Rilascio = new System.Windows.Forms.Button();
            this.BraccioMain = new System.Windows.Forms.PictureBox();
            this.Braccio1 = new System.Windows.Forms.PictureBox();
            this.Braccio2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BraccioMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Braccio1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Braccio2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(295, 383);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(35, 277);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 236);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox3.Location = new System.Drawing.Point(190, 480);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(105, 14);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // AttuatBraccioGiu
            // 
            this.AttuatBraccioGiu.Location = new System.Drawing.Point(98, 103);
            this.AttuatBraccioGiu.Name = "AttuatBraccioGiu";
            this.AttuatBraccioGiu.Size = new System.Drawing.Size(48, 20);
            this.AttuatBraccioGiu.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Braccio giù";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Braccio su";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Braccio dx";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Braccio sx";
            // 
            // MasterTimer
            // 
            this.MasterTimer.Tick += new System.EventHandler(this.MasterTimer_Tick);
            // 
            // UpButton
            // 
            this.UpButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpButton.Location = new System.Drawing.Point(812, 451);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(54, 37);
            this.UpButton.TabIndex = 14;
            this.UpButton.Text = "Up";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // RightButton
            // 
            this.RightButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightButton.Location = new System.Drawing.Point(872, 494);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(54, 37);
            this.RightButton.TabIndex = 15;
            this.RightButton.Text = "Right";
            this.RightButton.UseVisualStyleBackColor = true;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownButton.Location = new System.Drawing.Point(812, 537);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(54, 37);
            this.DownButton.TabIndex = 16;
            this.DownButton.Text = "Down";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // LeftButton
            // 
            this.LeftButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftButton.Location = new System.Drawing.Point(752, 494);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(54, 37);
            this.LeftButton.TabIndex = 17;
            this.LeftButton.Text = "Left";
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(378, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(254, 37);
            this.label6.TabIndex = 19;
            this.label6.Text = "Serra Idroponica";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(27, 51);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(61, 30);
            this.StartButton.TabIndex = 20;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(161, 51);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(61, 30);
            this.ResetButton.TabIndex = 21;
            this.ResetButton.Text = "RESET";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(94, 51);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(61, 30);
            this.StopButton.TabIndex = 22;
            this.StopButton.Text = "STOP";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // AttuatBraccioSx
            // 
            this.AttuatBraccioSx.Location = new System.Drawing.Point(98, 175);
            this.AttuatBraccioSx.Name = "AttuatBraccioSx";
            this.AttuatBraccioSx.Size = new System.Drawing.Size(48, 20);
            this.AttuatBraccioSx.TabIndex = 23;
            // 
            // AttuatBraccioSu
            // 
            this.AttuatBraccioSu.Location = new System.Drawing.Point(98, 138);
            this.AttuatBraccioSu.Name = "AttuatBraccioSu";
            this.AttuatBraccioSu.Size = new System.Drawing.Size(48, 20);
            this.AttuatBraccioSu.TabIndex = 24;
            // 
            // AttuatBraccioDx
            // 
            this.AttuatBraccioDx.Location = new System.Drawing.Point(98, 211);
            this.AttuatBraccioDx.Name = "AttuatBraccioDx";
            this.AttuatBraccioDx.Size = new System.Drawing.Size(48, 20);
            this.AttuatBraccioDx.TabIndex = 25;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(728, 383);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(289, 54);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 26;
            this.pictureBox4.TabStop = false;
            // 
            // AttuatBraccioRilascio
            // 
            this.AttuatBraccioRilascio.Location = new System.Drawing.Point(265, 135);
            this.AttuatBraccioRilascio.Name = "AttuatBraccioRilascio";
            this.AttuatBraccioRilascio.Size = new System.Drawing.Size(48, 20);
            this.AttuatBraccioRilascio.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Braccio rilascio";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Braccio presa";
            // 
            // AttuatBraccioPresa
            // 
            this.AttuatBraccioPresa.Location = new System.Drawing.Point(265, 100);
            this.AttuatBraccioPresa.Name = "AttuatBraccioPresa";
            this.AttuatBraccioPresa.Size = new System.Drawing.Size(48, 20);
            this.AttuatBraccioPresa.TabIndex = 27;
            // 
            // Presa_Rilascio
            // 
            this.Presa_Rilascio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Presa_Rilascio.Location = new System.Drawing.Point(812, 494);
            this.Presa_Rilascio.Name = "Presa_Rilascio";
            this.Presa_Rilascio.Size = new System.Drawing.Size(54, 37);
            this.Presa_Rilascio.TabIndex = 31;
            this.Presa_Rilascio.Text = "P/R";
            this.Presa_Rilascio.UseVisualStyleBackColor = true;
            this.Presa_Rilascio.Click += new System.EventHandler(this.Presa_Rilascio_Click);
            // 
            // BraccioMain
            // 
            this.BraccioMain.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BraccioMain.Location = new System.Drawing.Point(247, 277);
            this.BraccioMain.Name = "BraccioMain";
            this.BraccioMain.Size = new System.Drawing.Size(280, 30);
            this.BraccioMain.TabIndex = 32;
            this.BraccioMain.TabStop = false;
            // 
            // Braccio1
            // 
            this.Braccio1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Braccio1.Location = new System.Drawing.Point(247, 306);
            this.Braccio1.Name = "Braccio1";
            this.Braccio1.Size = new System.Drawing.Size(26, 49);
            this.Braccio1.TabIndex = 34;
            this.Braccio1.TabStop = false;
            // 
            // Braccio2
            // 
            this.Braccio2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Braccio2.Location = new System.Drawing.Point(501, 306);
            this.Braccio2.Name = "Braccio2";
            this.Braccio2.Size = new System.Drawing.Size(26, 49);
            this.Braccio2.TabIndex = 35;
            this.Braccio2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 585);
            this.Controls.Add(this.Braccio2);
            this.Controls.Add(this.Braccio1);
            this.Controls.Add(this.BraccioMain);
            this.Controls.Add(this.Presa_Rilascio);
            this.Controls.Add(this.AttuatBraccioRilascio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AttuatBraccioPresa);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.AttuatBraccioDx);
            this.Controls.Add(this.AttuatBraccioSu);
            this.Controls.Add(this.AttuatBraccioSx);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AttuatBraccioGiu);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BraccioMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Braccio1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Braccio2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox AttuatBraccioGiu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer MasterTimer;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button LeftButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox AttuatBraccioSx;
        private System.Windows.Forms.TextBox AttuatBraccioSu;
        private System.Windows.Forms.TextBox AttuatBraccioDx;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox AttuatBraccioRilascio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AttuatBraccioPresa;
        private System.Windows.Forms.Button Presa_Rilascio;
        private System.Windows.Forms.PictureBox BraccioMain;
        private System.Windows.Forms.PictureBox Braccio1;
        private System.Windows.Forms.PictureBox Braccio2;
    }
}

