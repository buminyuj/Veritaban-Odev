using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace veritabanıbağlantı1
{
    public partial class MyCards : Form
    {
        private int MusteriID;
        private readonly int loggedInCustomerID;

        public MyCards(int musteriID)
        {
            InitializeComponent();
            MusteriID = musteriID;

        }

        private void button2_Click(object sender, EventArgs e)//Kart Ekle
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
            string KartNo = textBox1.Text.ToLower();
            string Ad = textBox2.Text.ToLower();
            string Soyad = textBox3.Text.ToLower();
            var Tarih = maskedTextBox1.Text;
            string CVV = textBox4.Text;

            try
            {
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {
                    connnection.Open();
                    string queryAdd = "INSERT INTO KrediKart (MusteriID,KartNo,Ad,Soyad,Tarih,CVV) VALUES (@MusteriID,@KartNo,@Ad,@Soyad,@Tarih,@Cvv)";
                    using (SqlCommand command = new SqlCommand(queryAdd, connnection))
                    {
                        command.Parameters.AddWithValue("@MusteriID", MusteriID);
                        command.Parameters.AddWithValue("@KartNo", KartNo);
                        command.Parameters.AddWithValue("@Ad", Ad);
                        command.Parameters.AddWithValue("@Soyad", Soyad);
                        command.Parameters.AddWithValue("@Tarih", Tarih);
                        command.Parameters.AddWithValue("@Cvv", CVV);
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.");
                            connnection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenirken bir hata oluştu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        public void ShowMyCards()
        {

            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //MyCards myCardsForm = new MyCards(loggedInCustomerID);
                //myCardsForm.Show();
                string queryList = "SELECT * FROM KrediKart WHERE MusteriID = @MusteriID";

                using (SqlCommand command = new SqlCommand(queryList, connection))
                {
                    command.Parameters.AddWithValue("@MusteriID", MusteriID);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    maskedTextBox1.Clear();
                }
            }
        }

        public void DeleteCard(int id)//Urun silme fonksiyonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryDellCard = "DELETE FROM KrediKart WHERE KartId=@Id";

                using (SqlCommand command = new SqlCommand(queryDellCard, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//KartlarımıGörüntüle
        {
            ShowMyCards();
        }

        private void button3_Click(object sender, EventArgs e)//Kart sil
        {
            DeleteCard(MusteriID);
        }
    }
}

