using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace depo_yönetimi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmAnaMenu anaMenu = new FrmAnaMenu();
            anaMenu.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LogYoneticisi.Kaydet(textBox1.Text + " kodu ile stok hareketleri aratıldı.", "Form2");
                using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                {
                    baglanti.Open();

                    SqlCommand cmd = new SqlCommand("sp_DepoStokFiltrele", baglanti);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StokKodu", textBox1.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("Sp_DepoListeGetir", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Stok Kodu:";
            label2.Text = "Depo Kodu";
            label3.Text = "İşlem Tipi";
            try
            {
                using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Sp_StokRaporu", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string logBaglanti = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";

                
                using (System.Data.SqlClient.SqlConnection baglanti = new System.Data.SqlClient.SqlConnection(logBaglanti))
                {
                    baglanti.Open();
                    string sorgu = "SELECT LogId AS [Kayıt No], KullaniciAdi AS [İşlemi Yapan], IslemTipi AS [Yapılan İşlem], Tarih AS [Zaman] FROM Tbl_KullaniciLoglari ORDER BY Tarih DESC";

                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sorgu, baglanti);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loglar yüklenemedi: " + ex.Message);
            }



 }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
           
            if (dataGridView2.DataSource != null)
            {
                
                System.Data.DataTable dt = (System.Data.DataTable)dataGridView2.DataSource;

               
                
                dt.DefaultView.RowFilter = string.Format("[İşlemi Yapan] LIKE '%{0}%' OR [Yapılan İşlem] LIKE '%{0}%'", textBox1.Text);
            }
             
              try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                if (dt != null)
                {
                    
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        dt.DefaultView.RowFilter = string.Format("[Stok Kodu] = '{0}'", textBox1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void te(object sender, EventArgs e)
        {

        }
    

























private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (dataGridView1.DataSource != null)
            {
                System.Data.DataTable dt = (System.Data.DataTable)dataGridView1.DataSource;

                
                dt.DefaultView.RowFilter = string.Format("[Stok Kodu] LIKE '%{0}%'", textBox2.Text);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                if (dt != null)
                {
                    
                    dt.DefaultView.RowFilter = string.Format("İşlemTipi LIKE '%{0}%'", textBox3.Text);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmStokSec secimEkrani = new FrmStokSec();



            LogYoneticisi.Kaydet(textBox1.Text + " kodu için seçim ekranına gidildi.", "Form2");



            secimEkrani.IlkAramaMetni = textBox1.Text;

            if (secimEkrani.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = secimEkrani.SecilenStokKodu;
            }
        }
    }
}
