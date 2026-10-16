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
using depo_yönetimi;
using System.Data.SqlClient;

namespace depo_yönetimi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";

            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    
                    string sorgu = "SELECT * FROM Tbl_Kullanicilar WHERE KullaniciAdi=@adi AND Sifre=@sifre";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@adi", textBox1.Text);
                        komut.Parameters.AddWithValue("@sifre", textBox2.Text);

                        SqlDataReader oku = komut.ExecuteReader();

                        if (oku.Read())
                        {
                            
                            LogYonetimi.SistemeLogYaz(textBox1.Text, "Sisteme Başarıyla Giriş Yaptı");

                            LogYonetimi.AktifKullanici = textBox1.Text;
                            MessageBox.Show("Giriş Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FrmAnaMenu fr = new FrmAnaMenu();
                            fr.Show();

                            
                            this.Hide();
                        }
                        else 
                        {
                            MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                           
                            LogYonetimi.SistemeLogYaz(textBox1.Text, "Hatalı Şifre Denemesi Yaptı");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
