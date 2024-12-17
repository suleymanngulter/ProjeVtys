using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Excel = Microsoft.Office.Interop.Excel;

namespace AracSatımVeTeknikServis
{
    public partial class FrmTeknikServis : Form
    {
        public FrmTeknikServis(int a, Form b)
        {
            InitializeComponent();
            PerId = a;
            _frmEski = b;
        }
        int PerId;
        Form _frmEski;
        sqlBaglanti bgl = new sqlBaglanti();
        private void FrmTeknikServis_Load(object sender, EventArgs e)
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
        public void DataGridGuncelle()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("SELECT TeknikServis.ServisNo,TeknikServis.MusteriId, Musteri.MusteriAd + ' ' + Musteri.MusteriSoyad AS MusteriAdiSoyadi,TeknikServis.AracPlaka,TeknikServis.TarihGiris,TeknikServis.TarihCikis,TeknikServis.SorunTanimi,TeknikServis.IslemDetay FROM TeknikServis INNER JOIN Musteri ON TeknikServis.MusteriID = Musteri.MusteriID;\r\n", bgl.baglanti());
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
            SqlCommand cmd = new SqlCommand("SELECT ts.ServisNo, ts.AracPlaka,ts.TarihGiris,ts.TarihCikis,ts.SorunTanimi,ts.IslemDetay FROM TeknikServis ts WHERE ts.MusteriID = @p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtMusId.Text);
            cmd.Parameters.AddWithValue("@p2", txtPlaka.Text);
            //cmd.Parameters.AddWithValue("@p3", cmbRol.Text);
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
                SqlCommand cmd = new SqlCommand("INSERT INTO TeknikServis VALUES (@p2, @p3, @p4, @p5, @p6, @p7)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p2", txtMusId.Text);
                cmd.Parameters.AddWithValue("@p3", txtPlaka.Text);

                // DateTimePicker'dan sadece tarihi almak için Value kullanıyoruz.
                cmd.Parameters.AddWithValue("@p4", dtpTGiriş.Value.Date); // Tarihi sadece alın
                cmd.Parameters.AddWithValue("@p5", dtpTÇıkış.Value.Date); // Tarihi sadece alın

                cmd.Parameters.AddWithValue("@p6", rctSorun.Text);
                cmd.Parameters.AddWithValue("@p7", rctİslem.Text);

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

        // DataGridView üzerindeki satıra çift tıklama olayını işleme
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli satırın kontrolü
            if (e.RowIndex >= 0)
            {
                // Seçilen satır
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Seçilen ServisNo ve diğer verileri almak
                string servisNo = selectedRow.Cells["ServisNo"].Value.ToString();
                string musteriAdiSoyadi = selectedRow.Cells["MusteriAdiSoyadi"].Value.ToString();
                string musID = selectedRow.Cells["MusteriID"].Value.ToString();
                string aracPlaka = selectedRow.Cells["AracPlaka"].Value.ToString();
                string tarihGiris = selectedRow.Cells["TarihGiris"].Value.ToString();
                string tarihCikis = selectedRow.Cells["TarihCikis"].Value.ToString();
                string sorunTanimi = selectedRow.Cells["SorunTanimi"].Value.ToString();
                string islemDetay = selectedRow.Cells["IslemDetay"].Value.ToString();

                // İlgili kontrol elemanlarına verileri atama
                txtId.Text = servisNo;
                txtAd.Text = musteriAdiSoyadi;
                txtMusId.Text = musID;
                dtpTGiriş.Text = tarihGiris;
                dtpTÇıkış.Text = tarihCikis;
                txtPlaka.Text = aracPlaka;
                rctSorun.Text = sorunTanimi;
                rctSorun.Text = islemDetay;

                // Giriş Tarihi (DateTimePicker)
                if (DateTime.TryParse(tarihGiris, out DateTime tarihGirisDate))
                {
                    dtpTGiriş.Value = tarihGirisDate;
                }
                else
                {
                    MessageBox.Show("Geçersiz giriş tarihi formatı: " + tarihGiris);
                }

                // Çıkış Tarihi (DateTimePicker)
                if (DateTime.TryParse(tarihCikis, out DateTime tarihCikisDate))
                {
                    dtpTÇıkış.Value = tarihCikisDate;
                }
                else
                {
                    MessageBox.Show("Geçersiz çıkış tarihi formatı: " + tarihCikis);
                }
            }
        }

        private void txtMusId_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select MusteriAd,MusteriSoyad From Musteri where MusteriID=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtMusId.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[0] + " " + dr[1];
            }
            dr.Close();
            bgl.baglanti().Close();
        }

        private void btnGeri_Click_1(object sender, EventArgs e)
        {
            _frmEski.Show();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

                SqlCommand cmd = new SqlCommand("Select * From Musteri where MusteriID=@p1", bgl.baglanti());
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

                SqlCommand cmd = new SqlCommand("Select * From Araclar where AracPlaka=@p1", bgl.baglanti());
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

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("Update TeknikServis set MusteriId=@p2,AracPlaka=@p3,TarihGiris=@p4,TarihCikis=@p5,SorunTanimi=@p6,IslemDetay=@p7 where ServisNo=@p1 ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtId.Text);
                cmd.Parameters.AddWithValue("@p2", txtMusId.Text);
                cmd.Parameters.AddWithValue("@p3", txtPlaka.Text);
                cmd.Parameters.AddWithValue("@p4", dtpTGiriş.Value.Date);
                cmd.Parameters.AddWithValue("@p5", dtpTÇıkış.Value.Date);
                cmd.Parameters.AddWithValue("@p6", rctSorun.Text);
                cmd.Parameters.AddWithValue("@p7", rctİslem.Text);
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
                SqlCommand cmd = new SqlCommand("Delete TeknikServis where ServisNo=@p1 ", bgl.baglanti());
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
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TeknikServis.pdf");
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
                    FileName = "TeknikServis.xlsx" // Varsayılan dosya adı
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

        private void btnAra_Click(object sender, EventArgs e)
        {
            DataGridArama();
        }

        private void btnTblGuncelle_Click(object sender, EventArgs e)
        {
            DataGridGuncelle();
        }
    }
}
