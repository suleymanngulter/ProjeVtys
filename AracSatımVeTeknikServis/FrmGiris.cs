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

namespace AracSatımVeTeknikServis
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("Select * From Personel where KullaniciAdi=@p1 and Sifre=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtUsername.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmPersonelPanel frm = new FrmPersonelPanel((int)dr[0], dr[3].ToString(),this);
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Şifresi");
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: "+ex.Message);
            }
            bgl.baglanti().Close();
        }
    }
}
