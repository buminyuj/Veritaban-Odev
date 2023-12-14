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
            string Ad = textBox2.Text.ToLower();
            string Soyad = textBox3.Text.ToLower();
            string Rol = textBox1.Text.ToLower();
            string Email = textBox4.Text;
            string Sifre = textBox5.Text;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string queryLogin = "SELECT * FROM Personel WHERE PersonelAd=@Ad AND PersonelSoyad=@Soyad AND Email=@Email AND Sifre=@Sifre";

                    using (SqlCommand command = new SqlCommand(queryLogin, con))
                    {
                        command.Parameters.AddWithValue("@Ad", Ad);
                        command.Parameters.AddWithValue("@Soyad", Soyad);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Sifre", Sifre);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string personelRol = reader["PersonelRol"].ToString();

                            switch (personelRol)
                            {
                                case "admin":
                                    MessageBox.Show("Tebrikler Giriş Başarılı! Admin Seçim Paneline Yönlendiriliyorsunuz.");
                                    AdminSelectionPanel adminSelectionPanel = new AdminSelectionPanel();
                                    adminSelectionPanel.Show();
                                    break;

                                case "teknik":
                                    MessageBox.Show("Tebrikler Giriş Başarılı! Teknik Paneline Yönlendiriliyorsunuz.");
                                    TeknikPanel teknikPanel = new TeknikPanel();
                                    teknikPanel.Show();
                                    break;

                                case "personel":
                                    MessageBox.Show("Tebrikler Giriş Başarılı! Personel Paneline Yönlendiriliyorsunuz.");
                                    PersonelPanel personelPanel = new PersonelPanel();
                                    personelPanel.Show();
                                    break;

                                default:
                                    MessageBox.Show("Tanımsız Rol: " + personelRol);
                                    break;
                            }

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


        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

