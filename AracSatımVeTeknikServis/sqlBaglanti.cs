using System;
using System.Data.SqlClient;

namespace AracSatımVeTeknikServis
{
    internal class sqlBaglanti
    {
        // SQL bağlantısını döndüren bir metot
        public SqlConnection baglanti()
        {
            // Bağlantı dizesini tanımlayın
            SqlConnection conn = new SqlConnection("Data Source=ENTER DATA SOURCE;Initial Catalog=AracSatisVeTeknikServisTakipSistemi;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            conn.Open();
            return conn;
        }
    }
}

