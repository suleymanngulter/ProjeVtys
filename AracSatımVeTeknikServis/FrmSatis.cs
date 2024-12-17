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
using Excel = Microsoft.Office.Interop.Excel;

namespace AracSatımVeTeknikServis
{
    public partial class FrmSatis : Form
    {
        public FrmSatis(int a, Form b)
        {
            InitializeComponent();
            PerId = a;
            _frmEski = b;
        }
        int PerId;
        Form _frmEski;
        sqlBaglanti bgl = new sqlBaglanti();
        private void FrmSatis_Load(object sender, EventArgs e)
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
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            _frmEski.Show();
            this.Close();
        }

        public void DataGridGuncelle()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("Select s.SatisID,s.MusteriID,m.MusteriAd+' '+m.MusteriSoyad as MusteriAdSoyad,s.AracPlaka,s.islemTarihi,s.tutar From Satislar s inner join Musteri m on s.MusteriID=m.MusteriID", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat

            System.Data.DataTable dt2 = new System.Data.DataTable();
            SqlCommand cmd2 = new SqlCommand("SELECT MusteriId,MusteriAd+' '+MusteriSoyad as MusteriAdSoyad FROM Musteri", bgl.baglanti());
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2); // SqlDataAdapter ile verileri al
            da2.Fill(dt2); // DataTable'ı doldur
            dataGridView2.DataSource = dt2; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat

            System.Data.DataTable dt3 = new System.Data.DataTable();
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM Araclar", bgl.baglanti());
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3); // SqlDataAdapter ile verileri al
            da3.Fill(dt3); // DataTable'ı doldur
            dataGridView3.DataSource = dt3; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat
        }

        public void DataGridArama()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("Select s.SatisID,s.MusteriID,s.AracPlaka,s.islemTarihi,s.tutar From Satislar s WHERE s.MusteriID = @p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtMusId.Text);
            cmd.Parameters.AddWithValue("@p2", txtPlaka.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli satırın kontrolü
            if (e.RowIndex >= 0)
            {
                // Seçilen satır
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Seçilen ServisNo ve diğer verileri almak
                string servisNo = selectedRow.Cells["SatisID"].Value.ToString();
                string musteriAdiSoyadi = selectedRow.Cells["MusteriAdSoyad"].Value.ToString();
                string musID = selectedRow.Cells["MusteriID"].Value.ToString();
                string aracPlaka = selectedRow.Cells["AracPlaka"].Value.ToString();
                string tarihGiris = selectedRow.Cells["islemTarihi"].Value.ToString();
                string tutar = selectedRow.Cells["tutar"].Value.ToString();

                // İlgili kontrol elemanlarına verileri atama
                txtId.Text = servisNo;
                txtAd.Text = musteriAdiSoyadi;
                txtMusId.Text = musID;
                dtpTGiriş.Text = tarihGiris;
                txtPlaka.Text = aracPlaka;
                txtTutar.Text = tutar;

                // Giriş Tarihi (DateTimePicker)
                if (DateTime.TryParse(tarihGiris, out DateTime tarihGirisDate))
                {
                    dtpTGiriş.Value = tarihGirisDate;
                }
                else
                {
                    MessageBox.Show("Geçersiz giriş tarihi formatı: " + tarihGiris);
                }
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli satırın kontrolü
            if (e.RowIndex >= 0)
            {
                // Seçilen satır
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                // Plaka sütununun değerini al (Sütunun adı "Plaka" ise)
                string selectedID = selectedRow.Cells["MusteriID"].Value.ToString();

                SqlCommand cmd = new SqlCommand("Select MusteriId,MusteriAd+ ' '+ MusteriSoyad From Musteri where MusteriID=@p1", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", selectedID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtMusId.Text = dr[0].ToString();
                    txtAd.Text = dr[1].ToString();
                }
                dr.Close();
                bgl.baglanti().Close();
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli satırın kontrolü
            if (e.RowIndex >= 0)
            {
                // Seçilen satır
                DataGridViewRow selectedRow = dataGridView3.Rows[e.RowIndex];

                // Plaka sütununun değerini al (Sütunun adı "Plaka" ise)
                string selectedPlaka = selectedRow.Cells["AracPlaka"].Value.ToString();

                SqlCommand cmd = new SqlCommand("Select AracPlaka From Araclar where AracPlaka=@p1", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", selectedPlaka);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtPlaka.Text = dr[0].ToString();
                }
                dr.Close();
                bgl.baglanti().Close();

            }
        }

        private void btnTblGuncelle_Click(object sender, EventArgs e)
        {
            DataGridGuncelle();
        }


        private void btnPdfAktar_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Satis.pdf");
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportDataGridViewToExcel(dataGridView1);
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
                    FileName = "Satis.xlsx" // Varsayılan dosya adı
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

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Satislar VALUES (@p1, @p3, @p4, @p5)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtMusId.Text);
                cmd.Parameters.AddWithValue("@p3", txtPlaka.Text);
                cmd.Parameters.AddWithValue("@p4", dtpTGiriş.Value.Date); // Sadece tarih kısmını al
                cmd.Parameters.AddWithValue("@p5", txtTutar.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
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
                bgl.baglanti().Close();
            }

            DataGridGuncelle();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("Update Satislar set MusteriId=@p2,AracPlaka=@p3,islemTarihi=@p4,tutar=@p7 where SatisID=@p1 ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtId.Text);
                cmd.Parameters.AddWithValue("@p2", txtMusId.Text);
                cmd.Parameters.AddWithValue("@p3", txtPlaka.Text);
                cmd.Parameters.AddWithValue("@p4", dtpTGiriş.Value.Date);
                cmd.Parameters.AddWithValue("@p7", txtTutar.Text);
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
                SqlCommand cmd = new SqlCommand("Delete Satislar where SatisID=@p1 ", bgl.baglanti());
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

        private void btnAra_Click(object sender, EventArgs e)
        {
            DataGridArama();
        }
    }
}
