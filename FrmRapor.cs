using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace depo_yönetimi
{
    public partial class FrmRapor : Form
    {
        public FrmRapor()
        {
            InitializeComponent();
        }

        private void FrmRapor_Load(object sender, EventArgs e)
        {
            LoglariListele();
        }

        private void LoglariListele()
        {
            try
            {
                string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Sp_LogListele", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}