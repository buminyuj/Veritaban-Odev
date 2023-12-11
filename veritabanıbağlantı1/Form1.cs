using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace veritabanıbağlantı1
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string ad = textBox2.Text;
            string soyad = textBox3.Text;
            string şifre = textBox4.Text;

            con = new SqlConnection("Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True");
            com = new SqlCommand();

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Musteri WHERE Email='" + email + "' AND MusteriAd='" + ad + "' AND MusteriSoyad='" + soyad + "' AND Sifre='" + şifre + "'";
                dr = com.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("Tebrikler Giriş Başarılı!");

                    // Başarılı giriş sonrasında kullanıcıya soru sor
                    DialogResult result = MessageBox.Show("Çıkış yapmak istiyor musunuz?", "Çıkış", MessageBoxButtons.YesNo);

                    // Kullanıcının seçimine göre işlem yap
                    if (result == DialogResult.Yes)
                    {
                        // Çıkış yap
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show("Hatalı bir giriş yapıldı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}
