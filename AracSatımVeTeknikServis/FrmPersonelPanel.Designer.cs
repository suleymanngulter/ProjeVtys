namespace AracSatımVeTeknikServis
{
    partial class FrmPersonelPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPersonelPanel));
            this.btnAracs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPer = new System.Windows.Forms.Button();
            this.btnGeri = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMus = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTeknik = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSatis = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnLoglama = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSilinen = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAracs
            // 
            this.btnAracs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAracs.BackgroundImage")));
            this.btnAracs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAracs.Location = new System.Drawing.Point(19, 34);
            this.btnAracs.Name = "btnAracs";
            this.btnAracs.Size = new System.Drawing.Size(235, 222);
            this.btnAracs.TabIndex = 0;
            this.btnAracs.UseVisualStyleBackColor = true;
            this.btnAracs.Click += new System.EventHandler(this.Arac_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(529, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 41);
            this.label1.TabIndex = 2;
            this.label1.Text = "Personel Paneli";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAracs);
            this.groupBox1.Location = new System.Drawing.Point(12, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 273);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Araçlar";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPer);
            this.groupBox2.Location = new System.Drawing.Point(338, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 273);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Personel";
            // 
            // btnPer
            // 
            this.btnPer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPer.BackgroundImage")));
            this.btnPer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPer.Location = new System.Drawing.Point(19, 34);
            this.btnPer.Name = "btnPer";
            this.btnPer.Size = new System.Drawing.Size(235, 222);
            this.btnPer.TabIndex = 0;
            this.btnPer.UseVisualStyleBackColor = true;
            this.btnPer.Click += new System.EventHandler(this.btnPer_Click);
            // 
            // btnGeri
            // 
            this.btnGeri.Location = new System.Drawing.Point(12, 12);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(90, 33);
            this.btnGeri.TabIndex = 5;
            this.btnGeri.Text = "Geri";
            this.btnGeri.UseVisualStyleBackColor = true;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMus);
            this.groupBox3.Location = new System.Drawing.Point(665, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 273);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Müşteriler";
            // 
            // btnMus
            // 
            this.btnMus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMus.BackgroundImage")));
            this.btnMus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMus.Location = new System.Drawing.Point(19, 34);
            this.btnMus.Name = "btnMus";
            this.btnMus.Size = new System.Drawing.Size(235, 222);
            this.btnMus.TabIndex = 0;
            this.btnMus.UseVisualStyleBackColor = true;
            this.btnMus.Click += new System.EventHandler(this.btnMus_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTeknik);
            this.groupBox4.Location = new System.Drawing.Point(997, 84);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(277, 273);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Teknik Servis";
            // 
            // btnTeknik
            // 
            this.btnTeknik.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTeknik.BackgroundImage")));
            this.btnTeknik.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTeknik.Location = new System.Drawing.Point(19, 34);
            this.btnTeknik.Name = "btnTeknik";
            this.btnTeknik.Size = new System.Drawing.Size(235, 222);
            this.btnTeknik.TabIndex = 0;
            this.btnTeknik.UseVisualStyleBackColor = true;
            this.btnTeknik.Click += new System.EventHandler(this.btnTeknik_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSatis);
            this.groupBox5.Location = new System.Drawing.Point(12, 394);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(277, 273);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Satışlar";
            // 
            // btnSatis
            // 
            this.btnSatis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSatis.BackgroundImage")));
            this.btnSatis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSatis.Location = new System.Drawing.Point(19, 34);
            this.btnSatis.Name = "btnSatis";
            this.btnSatis.Size = new System.Drawing.Size(235, 222);
            this.btnSatis.TabIndex = 0;
            this.btnSatis.UseVisualStyleBackColor = true;
            this.btnSatis.Click += new System.EventHandler(this.btnSatis_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnLoglama);
            this.groupBox6.Location = new System.Drawing.Point(338, 394);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(277, 273);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Loglama";
            // 
            // btnLoglama
            // 
            this.btnLoglama.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoglama.BackgroundImage")));
            this.btnLoglama.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoglama.Location = new System.Drawing.Point(19, 34);
            this.btnLoglama.Name = "btnLoglama";
            this.btnLoglama.Size = new System.Drawing.Size(235, 222);
            this.btnLoglama.TabIndex = 0;
            this.btnLoglama.UseVisualStyleBackColor = true;
            this.btnLoglama.Click += new System.EventHandler(this.btnLoglama_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnSilinen);
            this.groupBox7.Location = new System.Drawing.Point(665, 394);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(277, 273);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Silinenler";
            // 
            // btnSilinen
            // 
            this.btnSilinen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSilinen.BackgroundImage")));
            this.btnSilinen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSilinen.Location = new System.Drawing.Point(19, 34);
            this.btnSilinen.Name = "btnSilinen";
            this.btnSilinen.Size = new System.Drawing.Size(235, 222);
            this.btnSilinen.TabIndex = 0;
            this.btnSilinen.UseVisualStyleBackColor = true;
            this.btnSilinen.Click += new System.EventHandler(this.btnSilinen_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button1);
            this.groupBox8.Location = new System.Drawing.Point(997, 394);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(277, 273);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Back Up / Restore";
            this.groupBox8.Enter += new System.EventHandler(this.groupBox8_Enter);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(19, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 222);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmPersonelPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(1301, 707);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnGeri);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPersonelPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPersonelPanel";
            this.Load += new System.EventHandler(this.FrmPersonelPanel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAracs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPer;
        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnMus;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnTeknik;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSatis;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnLoglama;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnSilinen;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button button1;
    }
}