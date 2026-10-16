using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace depo_yönetimi
{
    public partial class FrmAnaMenu : Form
    {
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label1;
        private Button button1;

        public FrmAnaMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(671, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Stok Kartı İşlemleri";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(304, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(671, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "Depo Tanımlama";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(304, 244);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(671, 47);
            this.button3.TabIndex = 2;
            this.button3.Text = "Stok Giriş/Çıkış";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(304, 334);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(671, 44);
            this.button4.TabIndex = 3;
            this.button4.Text = "Depoları Listele/Rapor";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(304, 418);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(671, 46);
            this.button5.TabIndex = 4;
            this.button5.Text = "Transfer";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label 1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FrmAnaMenu
            // 
            this.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.ClientSize = new System.Drawing.Size(1343, 525);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FrmAnaMenu";
            this.Load += new System.EventHandler(this.FrmAnaMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmStokKarti formStok = new FrmStokKarti();
            formStok.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDepoTanima fr = new FrmDepoTanima();
            fr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmStokGirisCikis fr = new FrmStokGirisCikis();
            fr.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            FrmRapor raporFormu = new FrmRapor();
            raporFormu.Show();
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            label1.Text = "Hoşgeldiniz, " + LogYonetimi.AktifKullanici;
            if (LogYonetimi.AktifKullanici != "admin")
            {
                
                button4.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            FrmBolumTransfer transferEkrani = new FrmBolumTransfer();
            transferEkrani.Show();
            this.Hide();


        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
    }
           
}