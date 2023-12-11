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
      
    public partial class UserRegister : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        public UserRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string email = textBox3.Text;
            string şifre = textBox4.Text;
            string şifreTekrar= textBox5.Text;
            con = new SqlConnection("Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True");
            com = new SqlCommand();
            try
            {
                con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO Musteri(email,MusteriAd,MusteriSoyad,Sifre) VALUES(@Email,@MusteriAd,@MusteriSoyad,@Sifre)";
            com.Parameters.AddWithValue("@Email", email);
            com.Parameters.AddWithValue("@MusteriAd", ad);
            com.Parameters.AddWithValue("@MusteriSoyad", soyad);
            com.Parameters.AddWithValue("@Sifre",şifre);
            int affectedRows = com.ExecuteNonQuery();
           // dr = com.ExecuteReader();
            
            if (affectedRows>0)
            {
                if(şifre.Equals(şifreTekrar)){
                    MessageBox.Show("Tebrikler Kayıt Başarılı!");
                    Form1 geçiş = new Form1();
                    geçiş.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Hatalı bir kayıt yapıldı!");
            }
            
            }
            catch(Exception ex) { 
            MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }



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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
