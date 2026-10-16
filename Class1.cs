using System;
using System.Data; 
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;




namespace depo_yönetimi
{
    public static class LogYoneticisi
    {
        public static void Kaydet(string yapilanIslem, string formIsmi)
        {
            try
            {
                string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();

                    
                    SqlCommand komut = new SqlCommand("Sp_LogEkle", baglanti);
                    komut.CommandType = CommandType.StoredProcedure; 

                    komut.Parameters.AddWithValue("@Islem", yapilanIslem);
                    komut.Parameters.AddWithValue("@FormAdi", formIsmi);

                    komut.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
                System.Windows.Forms.MessageBox.Show("Log Hatası: " + ex.Message);
            }
        }
    



public static void YoneticiyeMailAt(string konu, string mesaj)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;

              
                smtp.Credentials = new NetworkCredential("sistemin.maili@gmail.com", "uygulama_sifren_buraya");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sistemin.maili@gmail.com", "ERP Güvenlik Sistemi");
                mail.To.Add("patron.maili@gmail.com");

                mail.Subject = konu;
                mail.Body = mesaj;

                smtp.Send(mail);
            }
            catch (Exception)
            {
                // İnternet yoksa veya şifre yanlışsa program çökmesin
            }
        }
    }
}