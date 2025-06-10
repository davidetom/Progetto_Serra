
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Braccio = new System.Windows.Forms.PictureBox();
            this.StartVerticalMov = new System.Windows.Forms.Button();
            this.SensBraccioGiu = new System.Windows.Forms.TextBox();
            this.SensBraccioSu = new System.Windows.Forms.TextBox();
            this.SensBraccioSx = new System.Windows.Forms.TextBox();
            this.SensBraccioDx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MasterTimer = new System.Windows.Forms.Timer(this.components);
            this.UpButton = new System.Windows.Forms.Button();
            this.RightButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Braccio)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(272, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(257, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Serra Idroponica";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
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
            // Braccio
            // 
            this.Braccio.BackColor = System.Drawing.Color.Transparent;
            this.Braccio.Image = ((System.Drawing.Image)(resources.GetObject("Braccio.Image")));
            this.Braccio.Location = new System.Drawing.Point(243, 277);
            this.Braccio.Name = "Braccio";
            this.Braccio.Size = new System.Drawing.Size(293, 58);
            this.Braccio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Braccio.TabIndex = 4;
            this.Braccio.TabStop = false;
            // 
            // StartVerticalMov
            // 
            this.StartVerticalMov.Location = new System.Drawing.Point(35, 51);
            this.StartVerticalMov.Name = "StartVerticalMov";
            this.StartVerticalMov.Size = new System.Drawing.Size(111, 32);
            this.StartVerticalMov.TabIndex = 5;
            this.StartVerticalMov.Text = "StartVerticalMov";
            this.StartVerticalMov.UseVisualStyleBackColor = true;
            this.StartVerticalMov.Click += new System.EventHandler(this.button1_Click);
            // 
            // SensBraccioGiu
            // 
            this.SensBraccioGiu.Location = new System.Drawing.Point(98, 103);
            this.SensBraccioGiu.Name = "SensBraccioGiu";
            this.SensBraccioGiu.Size = new System.Drawing.Size(134, 20);
            this.SensBraccioGiu.TabIndex = 6;
            this.SensBraccioGiu.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // SensBraccioSu
            // 
            this.SensBraccioSu.Location = new System.Drawing.Point(98, 138);
            this.SensBraccioSu.Name = "SensBraccioSu";
            this.SensBraccioSu.Size = new System.Drawing.Size(134, 20);
            this.SensBraccioSu.TabIndex = 7;
            this.SensBraccioSu.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // SensBraccioSx
            // 
            this.SensBraccioSx.Location = new System.Drawing.Point(98, 175);
            this.SensBraccioSx.Name = "SensBraccioSx";
            this.SensBraccioSx.Size = new System.Drawing.Size(134, 20);
            this.SensBraccioSx.TabIndex = 8;
            // 
            // SensBraccioDx
            // 
            this.SensBraccioDx.Location = new System.Drawing.Point(98, 214);
            this.SensBraccioDx.Name = "SensBraccioDx";
            this.SensBraccioDx.Size = new System.Drawing.Size(134, 20);
            this.SensBraccioDx.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Braccio giù";
            this.label1.Click += new System.EventHandler(this.label1_Click);
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
            this.UpButton.Location = new System.Drawing.Point(362, 66);
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
            this.RightButton.Location = new System.Drawing.Point(422, 103);
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
            this.DownButton.Location = new System.Drawing.Point(362, 138);
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
            this.LeftButton.Location = new System.Drawing.Point(302, 103);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(54, 37);
            this.LeftButton.TabIndex = 17;
            this.LeftButton.Text = "Left";
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 642);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SensBraccioDx);
            this.Controls.Add(this.SensBraccioSx);
            this.Controls.Add(this.SensBraccioSu);
            this.Controls.Add(this.SensBraccioGiu);
            this.Controls.Add(this.StartVerticalMov);
            this.Controls.Add(this.Braccio);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Braccio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox Braccio;
        private System.Windows.Forms.Button StartVerticalMov;
        private System.Windows.Forms.TextBox SensBraccioGiu;
        private System.Windows.Forms.TextBox SensBraccioSu;
        private System.Windows.Forms.TextBox SensBraccioSx;
        private System.Windows.Forms.TextBox SensBraccioDx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer MasterTimer;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button LeftButton;
    }
}

