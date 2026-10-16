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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace depo_yönetimi
{
    public partial class FrmBolumTransfer : Form
    {
        string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";

        public FrmBolumTransfer()
        {
            InitializeComponent();
        }

        private void FrmBolumTransfer_Load(object sender, EventArgs e)
        {

            TransferleriListele();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
            LogYonetimi.SistemeLogYaz("Admin", "Bölüm Transfer Ekranından Çıktı");
        }

        private void TransferleriListele()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    
                    SqlCommand komut = new SqlCommand("Sp_TransferListele", baglanti);
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
                MessageBox.Show("Transfer listesi yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                
                var hucreDegeri = dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                
                if (hucreDegeri != null && hucreDegeri != DBNull.Value)
                {
                    int transferId;
                    
                    if (int.TryParse(hucreDegeri.ToString(), out transferId))
                    {
                        DetaylariListele(transferId);
                    }
                    else
                    {
                        
                        MessageBox.Show("HATA: Çekilmeye çalışılan ID sayı formatında değil. Gelen değer: " + hucreDegeri.ToString());
                    }
                }
            }
        }

        private void DetaylariListele(int transferId)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Sp_TransferDetayListele", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.Parameters.AddWithValue("@TransferId", transferId);

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    MessageBox.Show("SQL'den Gelen Detay Sayısı: " + dt.Rows.Count);
                    dataGridView2.DataSource = dt;

                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Detaylar yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void TabloyuFiltrele()
        {
            
            DataTable dt = dataGridView1.DataSource as DataTable;

            if (dt == null) return; 

            
            string arama = textBox5.Text.Trim();
            string kaynak = textBox2.Text.Trim(); 
            string hedef = textBox3.Text.Trim();

            List<string> kosullar = new List<string>();

         
            if (!string.IsNullOrEmpty(arama))
            {
                kosullar.Add($"(Convert([Stok Kodu], 'System.String') LIKE '%{arama}%' OR [Kaynak Depo] LIKE '%{arama}%' OR [Hedef Depo] LIKE '%{arama}%')");
            }

           
            if (!string.IsNullOrEmpty(kaynak))
            {
                kosullar.Add($"[Kaynak Depo] LIKE '%{kaynak}%'");
            }

           
            if (!string.IsNullOrEmpty(hedef))
            {
                kosullar.Add($"[Hedef Depo] LIKE '%{hedef}%'");
            }

            
            if (kosullar.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Join(" AND ", kosullar);
            }
            else
            {
                dt.DefaultView.RowFilter = string.Empty;
            }
        }






































        private void buttonTransferYap_Click(object sender, EventArgs e)
        {
            try
            {

                string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";

                using (System.Data.SqlClient.SqlConnection baglanti = new System.Data.SqlClient.SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    System.Data.SqlClient.SqlCommand komut = new System.Data.SqlClient.SqlCommand("Sp_DepoTransferYap", baglanti);
                    komut.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    komut.Parameters.AddWithValue("@StokKodu", textBox1.Text.Trim());
                    komut.Parameters.AddWithValue("@CikisDepoKodu", textBox2.Text.Trim());
                    komut.Parameters.AddWithValue("@GirisDepoKodu", textBox3.Text.Trim());
                    komut.Parameters.AddWithValue("@Miktar", Convert.ToInt32(textBox4.Text.Trim()));

                    komut.ExecuteNonQuery();

                    System.Windows.Forms.MessageBox.Show("Transfer işlemi başarıyla tamamlandı.", "Sistem Bilgisi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Transfer Sırasında Hata: " + ex.Message, "Sistem Uyarısı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    
                    SqlCommand komut = new SqlCommand("Sp_StokTransfer", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    komut.Parameters.AddWithValue("@StokKodu", textBox1.Text);
                    komut.Parameters.AddWithValue("@KaynakDepo", textBox2.Text);
                    komut.Parameters.AddWithValue("@HedefDepo", textBox3.Text);
                    komut.Parameters.AddWithValue("@Miktar", Convert.ToDecimal(textBox4.Text));

                    komut.ExecuteNonQuery();
                }

                
                LogYoneticisi.Kaydet(textBox1.Text + " kodlu ürün, " + textBox2.Text + " deposundan " + textBox3.Text + " deposuna transfer edildi.", "FrmBolumTransfer");

                MessageBox.Show("Stok transferi başarıyla gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TransferleriListele();

                
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transfer sırasında hata oluştu: " + ex.Message, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGeri_Click(object sender, EventArgs e)
        {
            FrmAnaMenu anaMenu = new FrmAnaMenu();
            anaMenu.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Sp_StokOzetRaporu", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor yüklenirken hata: " + ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TabloyuFiltrele();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        {
            
            if (e.RowIndex >= 0)
            {
                
                var miktarSutunu = dataGridView2.Columns["Depodaki Kalan Toplam Miktar"];

                if (miktarSutunu != null)
                {
                    
                    var hucreDegeri = dataGridView2.Rows[e.RowIndex].Cells[miktarSutunu.Index].Value;

                    if (hucreDegeri != null)
                    {
                        decimal miktar = 0;
                        if (decimal.TryParse(hucreDegeri.ToString(), out miktar))
                        {
                            
                            if (miktar < 10) 
                            {
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            }
                            else if (miktar >= 100) 
                            {
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            }
                            else 
                            {
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }
        }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV Dosyası (*.csv)|*.csv";
                sfd.FileName = "StokRaporu_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                sfd.Title = "Raporu Kaydet";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, new System.Text.UTF8Encoding(true)))
                        {

                            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                            {
                                sw.Write(dataGridView2.Columns[i].HeaderText);
                                
                                if (i < dataGridView2.Columns.Count - 1) sw.Write(";");
                            }
                            sw.Write(sw.NewLine);

                           
                            foreach (DataGridViewRow row in dataGridView2.Rows)
                            {
                                if (!row.IsNewRow) 
                                {
                                    for (int i = 0; i < dataGridView2.Columns.Count; i++)
                                    {
                                        if (row.Cells[i].Value != null)
                                        {
                                           
                                            sw.Write(row.Cells[i].Value.ToString().Replace(";", ","));
                                        }
                                        if (i < dataGridView2.Columns.Count - 1) sw.Write(";");
                                    }
                                    sw.Write(sw.NewLine);
                                }
                            }
                        }
                        MessageBox.Show("Rapor başarıyla Excel formatında kaydedildi!", "İşlem Tamam", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya kaydedilirken hata oluştu. Dosya başka bir programda açık olabilir.\nHata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ekranda dışa aktarılacak veri yok. Lütfen önce tabloyu doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Sp_TransferListele", baglanti);
                    komut.CommandType = CommandType.StoredProcedure;

                    
                    komut.Parameters.AddWithValue("@BaslangicTarihi", dateTimePicker1.Value.Date);
                    komut.Parameters.AddWithValue("@BitisTarihi", dateTimePicker2.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                   
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtreleme sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       


































        private void button6_Click(object sender, EventArgs e)
        {
            
            if (dataGridView2.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Dosyası (*.pdf)|*.pdf";
                sfd.FileName = "StokRaporu_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                sfd.Title = "Raporu PDF Olarak Kaydet";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        
                        string fontYolu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                        BaseFont arial = BaseFont.CreateFont(fontYolu, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        iTextSharp.text.Font turkceFont = new iTextSharp.text.Font(arial, 10, iTextSharp.text.Font.NORMAL);
                        iTextSharp.text.Font baslikFont = new iTextSharp.text.Font(arial, 11, iTextSharp.text.Font.BOLD);

                        
                        PdfPTable pdfTablo = new PdfPTable(dataGridView2.Columns.Count);
                        pdfTablo.DefaultCell.Padding = 5;
                        pdfTablo.WidthPercentage = 100;
                        pdfTablo.HorizontalAlignment = Element.ALIGN_LEFT;

                       
                        foreach (DataGridViewColumn column in dataGridView2.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, baslikFont));
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(230, 230, 230); 
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfTablo.AddCell(cell);
                        }

                       
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    string hucreDegeri = cell.Value == null ? "" : cell.Value.ToString();
                                    PdfPCell pdfCell = new PdfPCell(new Phrase(hucreDegeri, turkceFont));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    pdfTablo.AddCell(pdfCell);
                                }
                            }
                        }

                       
                        using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            Document pdfDoc = new Document(PageSize.A4, 15f, 15f, 30f, 30f);
                            PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();

                          
                            Paragraph baslik = new Paragraph("Stok Özet Raporu\n\n", new iTextSharp.text.Font(arial, 16, iTextSharp.text.Font.BOLD));
                            baslik.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(baslik);

                           
                            pdfDoc.Add(pdfTablo);
                            pdfDoc.Close();
                            stream.Close();
                        }

                        MessageBox.Show("Rapor başarıyla PDF formatında kaydedildi!", "İşlem Tamam", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("PDF oluşturulurken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ekranda dışa aktarılacak veri yok. Önce Stok Raporu butonuna basınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        






































        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel (CSV) Dosyası (*.csv)|*.csv";
            ofd.Title = "Görüntülenecek Transfer Excel Dosyasını Seçin";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable excelTablosu = new DataTable();
                    string[] satirlar = System.IO.File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);

                    if (satirlar.Length > 0)
                    {
                        string[] basliklar = satirlar[0].Split(';');
                        foreach (string baslik in basliklar)
                        {
                            if (!string.IsNullOrWhiteSpace(baslik))
                            {
                                excelTablosu.Columns.Add(baslik.Trim());
                            }
                        }

                        for (int i = 1; i < satirlar.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(satirlar[i]))
                            {
                                string[] hucreler = satirlar[i].Split(';');
                                DataRow yeniSatir = excelTablosu.NewRow();
                                for (int j = 0; j < Math.Min(hucreler.Length, excelTablosu.Columns.Count); j++)
                                {
                                    yeniSatir[j] = hucreler[j];
                                }
                                excelTablosu.Rows.Add(yeniSatir);
                            }
                        }
                    }

                    dataGridView3.DataSource = excelTablosu;
                    MessageBox.Show("Transfer listesi kontrol için başarıyla yüklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya okunurken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    
                    foreach (DataGridViewRow satir in dataGridView1.Rows)
                    {
                       
                        if (satir.IsNewRow || satir.Cells[0].Value == null || string.IsNullOrWhiteSpace(satir.Cells[0].Value.ToString()))
                            continue;

                        int transferId = Convert.ToInt32(satir.Cells[0].Value);
                        string kaynakDepo = satir.Cells[3].Value.ToString();
                        string hedefDepo = satir.Cells[4].Value.ToString();
                        decimal yeniMiktar = Convert.ToDecimal(satir.Cells[5].Value);

                        string sorgu = "UPDATE Tbl_StokTransferleri SET KaynakDepo = @kaynak, HedefDepo = @hedef, Miktar = @miktar WHERE TransferId = @id";

                        using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                        {
                            komut.Parameters.AddWithValue("@kaynak", kaynakDepo);
                            komut.Parameters.AddWithValue("@hedef", hedefDepo);
                            komut.Parameters.AddWithValue("@miktar", yeniMiktar);
                            komut.Parameters.AddWithValue("@id", transferId);

                            komut.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Tablodaki güncel veriler SQL veritabanına başarıyla işlendi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogYonetimi.SistemeLogYaz("Admin", "Bölüm Transfer İşlemi Gerçekleştirdi");
                TabloyuFiltrele(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmBolumTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogYonetimi.SistemeLogYaz("Admin", "Sistemden Çıkış Yaptı");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                   
                    string sorgu = "SELECT LogId AS [Kayıt No], KullaniciAdi AS [Kullanıcı], IslemTipi AS [Yapılan İşlem], Tarih AS [İşlem Saati] FROM Tbl_KullaniciLoglari ORDER BY Tarih DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    
                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loglar okunurken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dt = (System.Data.DataTable)dataGridView1.DataSource;

                if (dt != null)
                {
                    
                    string aranan = textBox2.Text.Trim();

                    if (string.IsNullOrEmpty(aranan))
                    {
                      
                        dt.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        
                        dt.DefaultView.RowFilter = string.Format("Convert([Kaynak Depo], 'System.String') LIKE '%{0}%' OR Convert([Hedef Depo], 'System.String') LIKE '%{0}%' OR Convert([Stok Kodu], 'System.String') LIKE '%{0}%'", aranan);
                    }
                }
            }
            catch (Exception ex)
            {
               
                System.Windows.Forms.MessageBox.Show("Filtreleme yapılamadı. Sütun isimlerini kontrol edin.\nHata Detayı: " + ex.Message, "Sistem Uyarısı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
    }
}















