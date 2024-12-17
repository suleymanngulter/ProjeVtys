using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace AracSatımVeTeknikServis
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel(int a, Form b)
        {
            InitializeComponent();
            PerId = a;
            _frmEski = b;
        }
        int PerId;
        Form _frmEski; 
        sqlBaglanti bgl = new sqlBaglanti();
        private void FrmPersonel_Load(object sender, EventArgs e)
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
            DataGridGuncelle();
            ComboBoxVeriGuncelle();

        }
        public void ComboBoxVeriGuncelle()
        {
            try
            {
                // SQL sorgusu: CHECK CONSTRAINT'i al
                SqlCommand cmd = new SqlCommand("SELECT CHECK_CLAUSE FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS WHERE CONSTRAINT_NAME = 'ck_Rol'", bgl.baglanti());

                // Veri okuyucu
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // Kısıtlama ifadesini al
                    string checkClause = dr["CHECK_CLAUSE"].ToString();

                    // "OR" ayrımına göre değerleri al
                    string[] orValues = checkClause.Split(new string[] { "OR" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string value in orValues)
                    {
                        // "='" ve "'" karakterlerini temizle
                        string trimmedValue = value.Trim().Split('=')[1].Trim().Trim('\'', ')', ' ');

                        // ComboBox'a ekle
                        cmbRol.Items.Add(trimmedValue);
                    }
                    cmbRol.Items.Add(" ");
                }

                // Bağlantıyı kapat
                dr.Close();
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        public void DataGridGuncelle()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Personel", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat
        }

        public void DataGridArama()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Personel where PersonelAd=@p1 or PersonelSoyad=@p2 or PersonelRol=@p3", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", cmbRol.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat

        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            _frmEski.Show();
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("INSERT INTO Personel VALUES (@p1, @p2, @p3, @p4, @p5)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtAd.Text);
                cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", cmbRol.Text);
                cmd.Parameters.AddWithValue("@p4", txtKullanıcıAd.Text);
                cmd.Parameters.AddWithValue("@p5", txtsifre.Text);

                // Sorguyu çalıştır
                int rowsAffected = cmd.ExecuteNonQuery();

                // Kontrol için
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Kayıt başarılı bir şekilde eklendi.");
                }
                else
                {
                    MessageBox.Show("Kayıt eklenemedi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                bgl.baglanti().Close();
            }

            // Verileri güncelle
            DataGridGuncelle();


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli satırın kontrolü
            if (e.RowIndex >= 0)
            {
                // Seçilen satır
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Plaka sütununun değerini al (Sütunun adı "Plaka" ise)
                string selectedID = selectedRow.Cells["PersonelID"].Value.ToString();

                SqlCommand cmd = new SqlCommand("Select * From Personel where PersonelID=@p1", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", selectedID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtId.Text = dr[0].ToString();
                    txtAd.Text = dr[1].ToString();
                    txtSoyad.Text = dr[2].ToString();
                    cmbRol.Text = dr[3].ToString();
                    txtKullanıcıAd.Text = dr[4].ToString();
                    txtsifre.Text = dr[5].ToString();
                }
                dr.Close();
                bgl.baglanti().Close();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("Update Personel set PersonelAd=@p2,PersonelSoyad=@p3,PersonelRol=@p4,KullaniciAdi=@p5,Sifre=@p6 where PersonelID=@p1 ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtId.Text);
                cmd.Parameters.AddWithValue("@p2", txtAd.Text);
                cmd.Parameters.AddWithValue("@p3", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@p4", cmbRol.Text);
                cmd.Parameters.AddWithValue("@p5", txtKullanıcıAd.Text);
                cmd.Parameters.AddWithValue("@p6", txtsifre.Text);

                // Sorguyu çalıştır
                int rowsAffected = cmd.ExecuteNonQuery();

                // Kontrol için
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Güncelleme başarılı bir şekilde yapıldı.");
                }
                else
                {
                    MessageBox.Show("Güncelleme yapılamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                bgl.baglanti().Close();
            }
            // Verileri güncelle
            DataGridGuncelle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("Delete Personel where PersonelID=@p1 ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtId.Text);

                // Sorguyu çalıştır
                int rowsAffected = cmd.ExecuteNonQuery();

                // Kontrol için
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Silme başarılı bir şekilde yapıldı.");
                }
                else
                {
                    MessageBox.Show("Silme yapılamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                bgl.baglanti().Close();
            }
            // Verileri güncelle
            DataGridGuncelle();

        }

        private void btnPdfAktar_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Personels.pdf");
            ExportDataGridViewToPDF(dataGridView1, filePath);
        }

        public void ExportDataGridViewToPDF(DataGridView dgv, string filePath)
        {
            try
            {
                // PDF belgesi oluşturuluyor
                Document doc = new Document(PageSize.A4);

                // PDF yazarını ayarla
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

                // Belgeyi aç
                doc.Open();

                // Tabloyu oluştur
                PdfPTable table = new PdfPTable(dgv.ColumnCount);

                // Kolon başlıklarını ekle
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                // Veri satırlarını ekle
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow) // Yeni satırları atla
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(cell.Value?.ToString() ?? string.Empty);
                        }
                    }
                }

                // Tabloyu PDF'e ekle
                doc.Add(table);

                // PDF belgesini kapat
                doc.Close();

                // Kullanıcıya başarılı mesajı
                MessageBox.Show("PDF başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportDataGridViewToExcel(DataGridView dgv)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;

            try
            {
                // Excel uygulamasını başlat
                excelApp = new Excel.Application();
                excelApp.Visible = true; // Excel uygulamasını göster

                // Yeni bir çalışma kitabı oluştur
                workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                // Kolon başlıklarını Excel'e ekle
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }

                // Veri satırlarını Excel'e ekle
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                    }
                }

                // Dosya kaydetme
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx",
                    InitialDirectory = @"C:\Users\Acer-Pc\Desktop", // Varsayılan klasör
                    FileName = "Personels.xlsx" // Varsayılan dosya adı
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Excel dosyası başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Çalışma kitabını kapat
                workbook.Close(false);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bellek sızıntısını önlemek için COM nesnelerini serbest bırak
                if (workbook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                workbook = null;
                excelApp = null;
                GC.Collect();
            }


        }

        private void btnExcel_Click_1(object sender, EventArgs e)
        {
            ExportDataGridViewToExcel(dataGridView1);
        }

        private void btnTblGuncelle_Click(object sender, EventArgs e)
        {
            DataGridGuncelle();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            DataGridArama();
        }
    }
}
