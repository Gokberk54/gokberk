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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        Sqlbaglatisi bgl = new Sqlbaglatisi();

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select*From Tbl_sekreter where sekreterTc=@p1 and sekretersifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",mskTC.Text);
            komut.Parameters.AddWithValue("@p2",txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmSekreterDetay frs = new FrmSekreterDetay();
                frs.Tcnumara = mskTC.Text;
                frs.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre");
            }
            bgl.baglanti().Close();

        }
    }
}
