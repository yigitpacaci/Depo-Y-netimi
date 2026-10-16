using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace depo_yönetimi
{
    public partial class FrmStokSec : Form
    {
        public string IlkAramaMetni = "";

        public FrmStokSec()
        {
            InitializeComponent();
        }

        public string SecilenStokKodu { get; set; }

        SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;");

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = IlkAramaMetni;

            StoklariGetir(textBox1.Text);
        }

        void StoklariGetir(string arananMetin)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Sp_StokSecimiArama", baglanti);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("@Filtre", arananMetin);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection("Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1"))
            {
                baglanti.Open();

                
                SqlCommand cmd = new SqlCommand("sp_DepoStokFiltrele", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@StokKodu", textBox1.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                dataGridView1.DataSource = dt;

                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                SecilenStokKodu = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Lütfen bir stok seçin!");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                SecilenStokKodu = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Lütfen bir stok seçin!");
            }
        }
       

        void SecimiOnayla()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                SecilenStokKodu = dataGridView1.SelectedRows[0].Cells["StokKodu"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir stok seçin!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmStokSec secimEkrani = new FrmStokSec();
            if (secimEkrani.ShowDialog() == DialogResult.OK)
            {
                
                textBox1.Text = secimEkrani.SecilenStokKodu;
            }



            SecimiOnayla();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            StoklariGetir(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                string secilenKod = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

               
                textBox1.Text = secilenKod;

                
                SecilenStokKodu = secilenKod;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}