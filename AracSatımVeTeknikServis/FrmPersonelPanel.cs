using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracSatımVeTeknikServis
{
    public partial class FrmPersonelPanel : Form
    {
        public FrmPersonelPanel(int a,string b,Form c)
        {
            InitializeComponent();
            PerId = a;
            PerYetki = b;
            _frmEski = c;
        }
        int PerId;
        string PerYetki;
        Form _frmEski;
        private void Arac_Click(object sender, EventArgs e)
        {
            FrmArac frm = new FrmArac(PerId,this);
            frm.Show();
            this.Hide();
        }

        private void FrmPersonelPanel_Load(object sender, EventArgs e)
        {
            if(PerYetki!="Admin")
            {
                btnPer.Enabled = false;
            }
            if (PerYetki == "Satis Elemani") 
            { 
                btnTeknik.Enabled = false;
            }
            if(PerYetki=="Teknik Servis")
            {
                btnAracs.Enabled = false;
                btnMus.Enabled = false;
                btnSatis.Enabled = false;
            }
            else if(PerYetki==" ")
            {
                this.Enabled = false;
            }
        }

        private void btnPer_Click(object sender, EventArgs e)
        {
            FrmPersonel frm = new FrmPersonel(PerId, this);
            frm.Show();
            this.Hide();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            _frmEski.Show();
            this.Close();
        }

        private void btnMus_Click(object sender, EventArgs e)
        {
            FrmMusteris frm = new FrmMusteris(PerId, this);
            frm.Show();
            this.Hide();
        }

        private void btnTeknik_Click(object sender, EventArgs e)
        {
            FrmTeknikServis frm = new FrmTeknikServis(PerId, this);
            frm.Show();
            this.Hide();

        }

        private void btnSatis_Click(object sender, EventArgs e)
        {
            FrmSatis frm= new FrmSatis(PerId,this);
            frm.Show();
            this.Hide();
        }

        private void btnLoglama_Click(object sender, EventArgs e)
        {
            FrmLoglama frm = new FrmLoglama(PerId, this);
            frm.Show();
            this.Hide();
        }

        private void btnSilinen_Click(object sender, EventArgs e)
        {
            FrmSilinenler frm = new FrmSilinenler(PerId, this);
            frm.Show();
            this.Hide();
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBackUp_Restore frm = new FrmBackUp_Restore(PerId, this);
            frm.Show();
            this.Hide();
        }
    }
}
