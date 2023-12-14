using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace veritabanıbağlantı1
{
    public partial class AdminLoginPage : Form
    {
        public AdminLoginPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
            string Ad = textBox2.Text;
            string Soyad = textBox3.Text;
            string Rol = textBox1.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string queryLogin = "SELECT * FROM Personel WHERE PersonelRol='Admin' AND PersonelAd=@Ad AND PersonelSoyad=@Soyad";

                    using (SqlCommand command = new SqlCommand(queryLogin, con))
                    {
                        command.Parameters.AddWithValue("@Ad", Ad);
                        command.Parameters.AddWithValue("@Soyad", Soyad);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            MessageBox.Show("Tebrikler Giriş Başarılı!");
                            AdminPanel geçiş = new AdminPanel();
                            geçiş.Show();
                            this.Hide();
                            con.Close();
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Giriş Başarısız. Lütfen bilgilerinizi kontrol edin.");
                        }
                        con.Close();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

    }
}

