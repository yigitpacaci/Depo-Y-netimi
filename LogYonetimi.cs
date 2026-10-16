using System;
using System.Data.SqlClient;



namespace depo_yönetimi
{
    public static class LogYonetimi
    {
        static string baglantiCumlesi = "Server=89.252.161.43,5331;Database=STAJYER;User Id=Stajyer;Password=Stajyer1234;";
        public static string AktifKullanici = "";
        public static void SistemeLogYaz(string kullanici, string islem)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiCumlesi))
                {
                    baglanti.Open();
                    string sorgu = "INSERT INTO Tbl_KullaniciLoglari (KullaniciAdi, IslemTipi, Tarih) VALUES (@kullanici, @islem, @tarih)";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@kullanici", kullanici);
                        komut.Parameters.AddWithValue("@islem", islem);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                        komut.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
    }
}