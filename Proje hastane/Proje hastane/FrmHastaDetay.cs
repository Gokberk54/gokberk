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

namespace Proje_hastane
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        Sqlbaglatisi bgl = new Sqlbaglatisi();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            //Ad Soyad çekme
            SqlCommand komut = new SqlCommand("Select HastaAD,HastaSoyad From Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1" , lblTC.Text);

            SqlDataReader dr = komut.ExecuteReader();

            while(dr.Read())
            {
                lblADSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where HastaTc=" + tc, bgl.baglanti());

        da.Fill(dt);
            dataGridView1.DataSource = dt;


            // Branşları çekelim

            SqlCommand komut2 = new SqlCommand("select bransad From Tbl_Brans", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {   cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad From Tbl_Doktor where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while(dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0]+ " "+ dr3[1]);
            }

        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where Randevubrans='" + cmbBrans.Text+"'" , bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmbilgiDuzenle fr = new FrmbilgiDuzenle();
            fr.TCno = lblTC.Text;
            fr.Show();

        }

        private void btnRanveuAl_Click(object sender, EventArgs e)
        {
           
        }
    }
}
