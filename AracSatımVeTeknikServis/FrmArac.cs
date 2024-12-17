using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Excel = Microsoft.Office.Interop.Excel;

namespace AracSatımVeTeknikServis
{
    public partial class FrmArac : Form
    {
        public FrmArac(int b, Form frmEski)
        {
            InitializeComponent();
            PerId = b;
            _frmEski = frmEski;
        }
        Form _frmEski;
        sqlBaglanti bgl = new sqlBaglanti();
        int PerId;
        private void FrmArac_Load(object sender, EventArgs e)
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM Araclar", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat
        }
        public void DataGridArama()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Araclar where AracMarka=@p1 or AracPlaka=@p2 or AracModel=@p3", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtMarka.Text);
            cmd.Parameters.AddWithValue("@p2", txtAracPlaka.Text);
            cmd.Parameters.AddWithValue("@p3", txtModel.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("INSERT INTO Araclar VALUES (@p1, @p2, @p3, @p4, @p5)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtAracPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", txtMarka.Text);
                cmd.Parameters.AddWithValue("@p3", txtModel.Text);
                cmd.Parameters.AddWithValue("@p4", rtbOzellikler.Text);
                cmd.Parameters.AddWithValue("@p5", txtFiyat.Text);

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
                string selectedPlaka = selectedRow.Cells["AracPlaka"].Value.ToString();

                SqlCommand cmd = new SqlCommand("Select * From Araclar where AracPlaka=@p1", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", selectedPlaka);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtAracPlaka.Text = dr[0].ToString();
                    txtMarka.Text = dr[1].ToString();
                    txtModel.Text = dr[2].ToString();
                    rtbOzellikler.Text = dr[3].ToString();
                    txtFiyat.Text = dr[4].ToString();
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
                SqlCommand cmd = new SqlCommand("Update Araclar set AracMarka=@p2,AracModel=@p3,AracOzellikler=@p4,AracFiyat=@p5 where AracPlaka=@p1 ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtAracPlaka.Text);
                cmd.Parameters.AddWithValue("@p2", txtMarka.Text);
                cmd.Parameters.AddWithValue("@p3", txtModel.Text);
                cmd.Parameters.AddWithValue("@p4", rtbOzellikler.Text);
                cmd.Parameters.AddWithValue("@p5", txtFiyat.Text);

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
                SqlCommand cmd = new SqlCommand("Delete Araclar where AracPlaka=@p1 ", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtAracPlaka.Text);

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

        private void btnGeri_Click(object sender, EventArgs e)
        {
            _frmEski.Show();
            this.Close();
        }

        private void btnPdfAktar_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Araclar.pdf");
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
                    FileName = "Araclar.xlsx" // Varsayılan dosya adı
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt"; // Yalnızca .txt dosyalarını seç
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportTxt(filePath);
                }
            }
        }

        private void ImportTxt(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim(); // Satırdaki fazla boşlukları temizle
                        string[] data = line.Split('\t'); // Tab ile ayır

                        // Veri uzunluğu kontrolü yapılabilir
                        if (data.Length >= 5)
                        {
                            SaveToDatabase(data);
                        }
                        else
                        {
                            //MessageBox.Show("Geçersiz veri formatı: " + line);
                        }
                    }
                }
                DataGridGuncelle();
                MessageBox.Show("TXT dosyasından veri başarıyla import edildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void SaveToDatabase(string[] data)
        {
            try
            {
                using (SqlCommand komut = new SqlCommand(
                    "INSERT INTO Araclar (AracPlaka, AracMarka, AracModel, AracOzellikler, AracFiyat) VALUES (@AracPlaka, @AracMarka, @AracModel, @AracOzellikler, @AracFiyat)",
                    bgl.baglanti()))
                {
                    // Veritabanına parametreli ekleme işlemi
                    komut.Parameters.AddWithValue("@AracPlaka", data[0]);
                    komut.Parameters.AddWithValue("@AracMarka", data[1]);
                    komut.Parameters.AddWithValue("@AracModel", data[2]);
                    komut.Parameters.AddWithValue("@AracOzellikler", data[3]);
                    komut.Parameters.AddWithValue("@AracFiyat", data[4]);

                    // Sorguyu çalıştır
                    komut.ExecuteNonQuery();
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (.txt)|.txt"; // Yalnızca .txt dosyalarını seç
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    ExportText(filePath);
                }
            }
        }

        private void ExportText(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Her hücreyi Tab (\t) ile ayırarak yaz
                        string line = string.Join("\t", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString() ?? ""));
                        sw.WriteLine(line);
                    }
                }
                MessageBox.Show("Veri başarıyla dışa aktarıldı: " + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
