using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace depo_yönetimi
{
    public partial class FrmDepoTanima : Form
    {
        public FrmDepoTanima()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DepolariGetir()
        {
            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Depolar", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kutuların boş olup olmadığını kontrol eden kodun (Adres zorunlu değilse buraya eklemedim)
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Depo Kodu ve Depo Adı alanları boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogYonetimi.SistemeLogYaz("Admin", "Yeni Depo Tanımlama İşlemi Yaptı");
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                {
                    baglanti.Open();

                    SqlCommand komut = new SqlCommand("Sp_DepoEkle", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    komut.Parameters.AddWithValue("@kodu", textBox1.Text);
                    komut.Parameters.AddWithValue("@adi", textBox2.Text);

                    
                    komut.Parameters.AddWithValue("@adres", textBox3.Text);

                    komut.ExecuteNonQuery();
                }

                DepolariGetir();
                MessageBox.Show("Yeni depo başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear(); 
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
                {
                    
                    if (dataGridView1.CurrentRow.Cells[3].Value != null)
                    {
                        DialogResult cevap = MessageBox.Show("Seçili depoyu silmek istediğinize emin misiniz?", "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (cevap == DialogResult.Yes)
                        {
                            string seciliKodu = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                            {
                                baglanti.Open();
                                SqlCommand komut = new SqlCommand("Sp_DepoSil", baglanti);
                                komut.CommandType = CommandType.StoredProcedure;
                                komut.Parameters.AddWithValue("@kodu", seciliKodu);
                                komut.ExecuteNonQuery();
                            }

                            MessageBox.Show("Depo başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DepolariGetir();
                            LogYonetimi.SistemeLogYaz("Admin", "Sistemden Bir Depoyu Sildi");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seçtiğiniz satırda geçerli bir Depo Kodu yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için tablodan içi dolu bir kayıt seçin (Boş satıra tıklamayın)!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
                {
                    if (dataGridView1.CurrentRow.Cells[3].Value != null)
                    {
                        string seciliKodu = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                        using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                        {
                            baglanti.Open();

                            SqlCommand komut = new SqlCommand("Sp_DepoGuncelle", baglanti);
                            komut.CommandType = CommandType.StoredProcedure;

                            komut.Parameters.AddWithValue("@kodu", seciliKodu);
                            komut.Parameters.AddWithValue("@adi", textBox2.Text);    
                            komut.Parameters.AddWithValue("@adres", textBox3.Text);  

                            komut.ExecuteNonQuery();
                        }

                        MessageBox.Show("Depo bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        DepolariGetir();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox1.Focus();
                        LogYonetimi.SistemeLogYaz("Admin", "Mevcut Bir Deponun Bilgilerini Güncelledi");
                    }
                    else
                    {
                        MessageBox.Show("Seçtiğiniz satırda geçerli bir Depo Kodu yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen güncellemek için tablodan bir depo seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmAnaMenu anaMenu = new FrmAnaMenu();
            anaMenu.Show();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
                {
                    
                    textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString(); 
                    textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler aktarılırken hata oluştu: " + ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}