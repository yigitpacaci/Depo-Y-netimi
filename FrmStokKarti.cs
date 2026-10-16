using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace depo_yönetimi
{
    public partial class FrmStokKarti : Form
    {
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn StokKodu;
        private DataGridViewTextBoxColumn StokAdı;
        private DataGridViewTextBoxColumn Birim;
        private DataGridViewTextBoxColumn Açıklama;
        private Button button4;
        private Button button5;
        private Label label1;

        public FrmStokKarti()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.StokKodu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StokAdı = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Birim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Açıklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(64, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stok Kodu";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stok Adı";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Birim";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Açıklama";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(158, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(158, 107);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(158, 147);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(158, 189);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 7;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 8;
            this.button1.Text = "Kaydet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(158, 342);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "Güncelle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(158, 384);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 32);
            this.button3.TabIndex = 10;
            this.button3.Text = "Sil";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StokKodu,
            this.StokAdı,
            this.Birim,
            this.Açıklama});
            this.dataGridView1.Location = new System.Drawing.Point(550, 147);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(620, 194);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // StokKodu
            // 
            this.StokKodu.HeaderText = "Stok Kodu";
            this.StokKodu.MinimumWidth = 6;
            this.StokKodu.Name = "StokKodu";
            this.StokKodu.Width = 125;
            // 
            // StokAdı
            // 
            this.StokAdı.HeaderText = "Stok Adı";
            this.StokAdı.MinimumWidth = 6;
            this.StokAdı.Name = "StokAdı";
            this.StokAdı.Width = 125;
            // 
            // Birim
            // 
            this.Birim.HeaderText = "Birim";
            this.Birim.MinimumWidth = 6;
            this.Birim.Name = "Birim";
            this.Birim.Width = 125;
            // 
            // Açıklama
            // 
            this.Açıklama.HeaderText = "Açıklama";
            this.Açıklama.MinimumWidth = 6;
            this.Açıklama.Name = "Açıklama";
            this.Açıklama.Width = 125;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "Geri Dön";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(991, 99);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "Listeleme";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FrmStokKarti
            // 
            this.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.ClientSize = new System.Drawing.Size(1386, 507);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmStokKarti";
            this.Load += new System.EventHandler(this.FrmStokKarti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
                {
                    int secilen = e.RowIndex;
                    
                    
                    textBox1.Text = dataGridView1.Rows[secilen].Cells[0].Value?.ToString();
                    textBox2.Text = dataGridView1.Rows[secilen].Cells[1].Value?.ToString();
                    textBox3.Text = dataGridView1.Rows[secilen].Cells[2].Value?.ToString();
                    textBox4.Text = dataGridView1.Rows[secilen].Cells[3].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler aktarılırken hata oluştu: " + ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);

            
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        private void StokKartlariniGetir()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                {
                    baglanti.Open();

                    SqlCommand cmd0 = new SqlCommand();
                    cmd0.Connection = baglanti;
                    cmd0.CommandText = "Sp_StokKartiListele";
                    cmd0.CommandType = CommandType.StoredProcedure;
                    cmd0.CommandTimeout = baglanti.ConnectionTimeout;
                    cmd0.Parameters.Clear();

                    SqlDataAdapter da = new SqlDataAdapter(cmd0);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmStokKarti_Load(object sender, EventArgs e)
        {
            StokKartlariniGetir();
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    SqlCommand komut = new SqlCommand("Sp_StokEkle", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    komut.Parameters.AddWithValue("@StokKodu", textBox1.Text); 
                    komut.Parameters.AddWithValue("@StokAdi", textBox2.Text);   
                    komut.Parameters.AddWithValue("@BirimId", Convert.ToInt32(textBox3.Text));
                    komut.Parameters.AddWithValue("@Aciklama", textBox4.Text); 

                    komut.ExecuteNonQuery();
                }

                
                LogYoneticisi.Kaydet(textBox1.Text + " kodlu yeni stok karta eklendi.", "FrmStokKarti");

                MessageBox.Show("Yeni stok kartı başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Sp_StokKartiGuncelle", baglanti);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                komut.Parameters.AddWithValue("@p3", textBox3.Text);
                komut.Parameters.AddWithValue("@p4", textBox4.Text);
                komut.ExecuteNonQuery();
            }
            MessageBox.Show("Stok kartı başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StokKartlariniGetir();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            string silinecekStokKodu = textBox1.Text;
            string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";

            using (System.Data.SqlClient.SqlConnection baglanti = new System.Data.SqlClient.SqlConnection(baglantiCumlesi))
            {
                baglanti.Open();

              
                string kontrolSorgusu = "SELECT COUNT(*) FROM Tbl_StokHareketleri WHERE StokKodu = @Stok";
                System.Data.SqlClient.SqlCommand kontrolKomutu = new System.Data.SqlClient.SqlCommand(kontrolSorgusu, baglanti);
                kontrolKomutu.Parameters.AddWithValue("@Stok", silinecekStokKodu);

                
                int islemSayisi = (int)kontrolKomutu.ExecuteScalar();

              
                if (islemSayisi > 0)
                {
                    LogYoneticisi.YoneticiyeMailAt("Kritik Güvenlik İhlali", "Sistem Uyarısı: İşlem görmüş bir stok kartı silinmeye çalışıldı. Sistem işlemi otomatik olarak engelledi.");
                    MessageBox.Show("DİKKAT: Bu stok kartı daha önce işlem görmüş (Giriş/Çıkış veya Transfer yapılmış)!\n\nVeri bütünlüğünün bozulmaması için işlem görmüş bir stok kartı silinemez.", "Silme Reddedildi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
            }

            
           
        

            try
            {
                
                if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
                {
                   
                    
                    string seciliKodu = dataGridView1.CurrentRow.Cells[0].Value?.ToString();

                    
                    if (!string.IsNullOrWhiteSpace(seciliKodu))
                    {
                        DialogResult cevap = MessageBox.Show(seciliKodu + " kodlu stok kartını silmek istediğinize emin misiniz?", "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (cevap == DialogResult.Yes)
                        {
                            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                            {
                                baglanti.Open();
                                SqlCommand komut = new SqlCommand("Sp_StokKartiSil", baglanti);
                                komut.CommandType = CommandType.StoredProcedure;

                                
                                komut.Parameters.AddWithValue("@p1", seciliKodu);
                                komut.ExecuteNonQuery();
                            }

                            MessageBox.Show("Stok kartı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StokKartlariniGetir(); 

                            
                            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seçtiğiniz satırda geçerli bir Stok Kodu bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için tablodan bir ürün seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmAnaMenu anaMenu = new FrmAnaMenu();
            anaMenu.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            StokKartlariniGetir();
        }
    }
    }
