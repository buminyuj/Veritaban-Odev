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

        public void DeleteCard(int KartId)//Urun silme fonksiyonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryDellCard = "DELETE FROM KrediKart WHERE KartId=@Id";

                using (SqlCommand command = new SqlCommand(queryDellCard, connection))
                {
                    command.Parameters.AddWithValue("@Id", KartId);
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
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

            if (selectedRowIndex >= 0)
            {
                int kartId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["KartId"].Value);

                DeleteCard(kartId);

                // Show a message box indicating that the card has been successfully deleted
                MessageBox.Show("Kart başarıyla silindi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the displayed cards after deletion
                ShowMyCards();
            }
            else
            {
                MessageBox.Show("Lütfen bir kart seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        int i= 0;   
        private void button4_Click(object sender, EventArgs e)//Kart Düzenle
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryUpdate = "UPDATE KrediKart SET KartNo=@KartNo, Ad=@Ad, Soyad=@Soyad, CVV=@CVV, Tarih=@Tarih WHERE KartId=@Id";

                    using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@KartNo", textBox1.Text);
                        command.Parameters.AddWithValue("@Ad", textBox2.Text.ToLower());
                        command.Parameters.AddWithValue("@Soyad", textBox3.Text.ToLower());
                        command.Parameters.AddWithValue("@CVV", textBox4.Text);
                       

                        // Tarih değerini DateTime türüne dönüştürme
                        DateTime Tarih;

                        // Eğer tarih bilgisi girilmişse, dönüşüm yap
                        if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text) && DateTime.TryParse(maskedTextBox1.Text, out Tarih))
                        {
                            command.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = Tarih;
                        }
                        else
                        {
                            // Eğer tarih bilgisi girilmemişse, mevcut tarih bilgisini kullan
                            command.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = dataGridView1.Rows[i].Cells[5].Value;
                        }

                        command.Parameters.AddWithValue("@Id", dataGridView1.Rows[i].Cells[0].Value);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Kayıtlar Başarıyla Güncellendi!");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[i].Cells[5].Value as string;
        }
    }
    }


