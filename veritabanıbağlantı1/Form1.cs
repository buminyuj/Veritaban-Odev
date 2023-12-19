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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace veritabanıbağlantı1
{
    public partial class Form1 : Form
    {
        private int loggedInCustomerID;
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
        public int GetCustomerIDByUsernameAndPassword(string musteriAd, string musteriSoyad, string email, string sifre)
        {
            int customerID = -1; // -1, geçersiz bir müşteri ID'si olarak kullanılabilir

            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MusteriID FROM Musteri WHERE MusteriAd = @MusteriAd AND MusteriSoyad = @MusteriSoyad AND Email = @Email AND Sifre = @Sifre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MusteriAd", musteriAd);
                    command.Parameters.AddWithValue("@MusteriSoyad", musteriSoyad);
                    command.Parameters.AddWithValue("@Email", email); // email parametresi eklendi
                    command.Parameters.AddWithValue("@Sifre", sifre);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        // Kullanıcı bulundu, müşteri ID'sini al
                        customerID = Convert.ToInt32(result);
                    }
                }
            }

            return customerID;
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
                    loggedInCustomerID = GetCustomerIDByUsernameAndPassword(ad, soyad,email,şifre);
                    UserPanel geçiş = new UserPanel();
                    geçiş.Show();
                    this.Hide();

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

        public int GetLoggedInCustomerID()
        {
            return loggedInCustomerID;
        }

    }
}
