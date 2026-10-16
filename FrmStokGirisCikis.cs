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
    public partial class FrmStokGirisCikis : Form
    {
        public FrmStokGirisCikis()
        {
            InitializeComponent();
        }

        
        private void HareketleriGetir()
        {
            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_StokHareketleri", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        
        private void FrmStokGirisCikis_Load(object sender, EventArgs e)
        {
            HareketleriGetir();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                {
                    baglanti.Open();

                    
                    SqlCommand komut = new SqlCommand("Sp_StokHareketiEkle", baglanti);
                    komut.CommandType = CommandType.StoredProcedure; 

                    komut.Parameters.AddWithValue("@p1", textBox1.Text);
                    komut.Parameters.AddWithValue("@p2", comboBox1.Text);
                    komut.Parameters.AddWithValue("@p3", textBox2.Text);
                    komut.Parameters.AddWithValue("@p4", textBox3.Text);

                    komut.ExecuteNonQuery();
                }

                MessageBox.Show("Stok hareketi başarıyla işlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                HareketleriGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            FrmAnaMenu anaMenu = new FrmAnaMenu();
            anaMenu.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.CurrentRow != null)
                {
                    
                    DialogResult cevap = MessageBox.Show("Seçili kaydı silmek istediğinize emin misiniz?", "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (cevap == DialogResult.Yes)
                    {
                        
                        int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["HareketId"].Value);

                        using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                        {
                            baglanti.Open();

                            SqlCommand komut = new SqlCommand("Sp_StokHareketiSil", baglanti);
                            komut.CommandType = CommandType.StoredProcedure;

                           
                            komut.Parameters.AddWithValue("@HareketId", seciliId);

                            komut.ExecuteNonQuery();
                        }

                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        HareketleriGetir();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (dataGridView1.CurrentRow != null)
                {
                   
                    int seciliId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["HareketId"].Value);

                    using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;"))
                    {
                        baglanti.Open();

                        SqlCommand komut = new SqlCommand("Sp_StokHareketiGuncelle", baglanti);
                        komut.CommandType = CommandType.StoredProcedure;

                        
                        komut.Parameters.AddWithValue("@HareketId", seciliId);
                        komut.Parameters.AddWithValue("@p1", textBox1.Text);
                        komut.Parameters.AddWithValue("@p2", comboBox1.Text);
                        komut.Parameters.AddWithValue("@p3", textBox2.Text);
                        komut.Parameters.AddWithValue("@p4", textBox3.Text);

                        komut.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HareketleriGetir(); 
                }
                else
                {
                    MessageBox.Show("Lütfen güncellemek için tablodan bir kayıt seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Detayı: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (dataGridView1.CurrentRow != null)
                {
                    textBox1.Text = dataGridView1.CurrentRow.Cells["StokKodu"].Value.ToString();
                    comboBox1.Text = dataGridView1.CurrentRow.Cells["IslemTipi"].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells["DepoKodu"].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells["Miktar"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler aktarılırken hata oluştu: " + ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}