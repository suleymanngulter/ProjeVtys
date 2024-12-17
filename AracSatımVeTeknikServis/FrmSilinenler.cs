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
    public partial class FrmSilinenler : Form
    {
        public FrmSilinenler(int a, Form b)
        {
            InitializeComponent();
            PerId = a;
            _frmEski = b;
        }
        int PerId;
        Form _frmEski;
        sqlBaglanti bgl = new sqlBaglanti();
        private void FrmSilinenler_Load(object sender, EventArgs e)
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

        private void btnTblGuncelle_Click(object sender, EventArgs e)
        {
            DataGridGuncelle();
        }
        public void DataGridGuncelle()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Silinenler", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat
        }

        private void btnPdfAktar_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Silinenler.pdf");
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
                    FileName = "Silinenler.xlsx" // Varsayılan dosya adı
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportDataGridViewToExcel(dataGridView1);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Geçerli satırın kontrolü
            if (e.RowIndex >= 0)
            {
                // Seçilen satır
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Seçilen ServisNo ve diğer verileri almak
                string logId = selectedRow.Cells["SilinenID"].Value.ToString();
                string tablename = selectedRow.Cells["TabloAdı"].Value.ToString();
                string LogMessage = selectedRow.Cells["SilinenBilgiler"].Value.ToString();
                string tarihGiris = selectedRow.Cells["SilinmeTarihi"].Value.ToString();

                // İlgili kontrol elemanlarına verileri atama
                txtId.Text = logId;
                txtTabloAd.Text = tablename;
                rtbMessage.Text = LogMessage;
                dtpTGiriş.Text = tarihGiris;

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

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Komutu oluştur
                SqlCommand cmd = new SqlCommand("Delete FROM Silinenler where SilinenID=@p1 ", bgl.baglanti());
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
        public void DataGridArama()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * From Silinenler where SilinenId=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtId.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd); // SqlDataAdapter ile verileri al
            da.Fill(dt); // DataTable'ı doldur
            dataGridView1.DataSource = dt; // DataTable'ı DataGridView'e bağla
            bgl.baglanti().Close(); // Bağlantıyı kapat
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            DataGridArama();
        }
    }
}
