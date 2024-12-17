using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AracSatımVeTeknikServis
{
    public partial class FrmBackUp_Restore : Form
    {
        public FrmBackUp_Restore(int a, Form b)
        {
            InitializeComponent();
            PerId = a;
            _frmEski = b;
        }
        int PerId;
        Form _frmEski;
        sqlBaglanti bgl = new sqlBaglanti();
        private void FrmBackUp_Restore_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * From Personel where PersonelID=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", PerId);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAd.Text = dr[1].ToString();
                lblSoyad.Text = dr[2].ToString();
                lblRol.Text = dr[3].ToString();
            }
            dr.Close();
            bgl.baglanti().Close();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            _frmEski.Show();
            this.Close();
        }

        private void btnYedekle_Click(object sender, EventArgs e)
        {
            // SQL komutu: Stored Procedure'yi çağırma
            string sqlCommand = "EXEC dbo.yedekle";

            try
            {
                SqlCommand command = new SqlCommand(sqlCommand, bgl.baglanti());
                // Yedekleme işlemi başarılıysa kullanıcıya bilgi ver
                MessageBox.Show("Veritabanı yedeği başarıyla alındı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı göster
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnYedektendn_Click(object sender, EventArgs e)
        {
            string sqlCommand = "EXEC dbo.yedektendön";

            try
            {
                SqlCommand command = new SqlCommand(sqlCommand, bgl.baglanti());
                        // Stored Procedure'yi çalıştır
                        command.ExecuteNonQuery();

                // Geri yükleme işlemi başarılıysa kullanıcıya bilgi ver
                MessageBox.Show("Veritabanı yedeği başarıyla geri yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı göster
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
