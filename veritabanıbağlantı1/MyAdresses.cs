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

namespace veritabanıbağlantı1
{
    public partial class MyAdresses : Form
    {
        private int MusteriID;
        private readonly int loggedInCustomerID;
        public MyAdresses(int musteriID)//içine bu gelebilir duruma göre bozulursa denenir-->int loggedInCustomerID, int musteriID
        {
            InitializeComponent();
            MusteriID = musteriID;
        }

        private void button2_Click(object sender, EventArgs e)//adres ekle
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
            string Sehir = textBox1.Text.ToLower();
            string Ilce = textBox2.Text.ToLower();
            string Mahalle = textBox3.Text.ToLower();
            string Sokak = textBox4.Text.ToLower();
            string AdresNo = textBox5.Text.ToLower();
            string Blok = textBox6.Text.ToLower();
            string DaireNo = textBox7.Text.ToLower();

            try
            {
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {
                    connnection.Open();
                    string queryAdd = "INSERT INTO Adresler (MusteriID,Sehir,Ilce,Mahalle,Sokak,AdresNo,Blok,DaireNo) VALUES (@MusteriID,@Sehir,@Ilce,@Mahalle,@Sokak,@AdresNo,@Blok,@DaireNo)";
                    using (SqlCommand command = new SqlCommand(queryAdd, connnection))
                    {
                        command.Parameters.AddWithValue("@MusteriID", MusteriID);
                        command.Parameters.AddWithValue("@Sehir", Sehir);
                        command.Parameters.AddWithValue("@Ilce", Ilce);
                        command.Parameters.AddWithValue("@Mahalle", Mahalle);
                        command.Parameters.AddWithValue("@Sokak", Sokak);
                        command.Parameters.AddWithValue("@AdresNo", AdresNo);
                        command.Parameters.AddWithValue("@Blok", Blok);
                        command.Parameters.AddWithValue("@DaireNo", DaireNo);
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

        public void ShowMyAdresses()//adres listeleme fonksiyonu
        {

            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //MyCards myCardsForm = new MyCards(loggedInCustomerID);
                //myCardsForm.Show();
                string queryList = "SELECT * FROM Adresler WHERE MusteriID = @MusteriID";

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
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//adres görüntüle
        {
            ShowMyAdresses();
        }

        public void DeleteAdress(int AdresId)//Adres silme fonksiyonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryDellAdress = "DELETE FROM Adresler WHERE AdresId=@Id";

                using (SqlCommand command = new SqlCommand(queryDellAdress, connection))
                {
                    command.Parameters.AddWithValue("@Id", AdresId);
                    command.ExecuteNonQuery();
                }
            }
        }

       
        private void button4_Click(object sender, EventArgs e)//Adres Sil
        {
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

            if (selectedRowIndex >= 0)
            {
                int AdresId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["AdresId"].Value);

                DeleteAdress(AdresId);

                // Show a message box indicating that the card has been successfully deleted
                MessageBox.Show("Adres başarıyla silindi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the displayed cards after deletion
                ShowMyAdresses();
            }
            else
            {
                MessageBox.Show("Lütfen bir kart seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        int i = 0;
        private void button3_Click(object sender, EventArgs e)//adres düzenle
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryUpdate = "UPDATE Adresler SET Sehir=@Sehir, Ilce=@Ilce, Mahalle=@Mahalle, Sokak=@Sokak, AdresNo=@AdresNo,Blok=@Blok,DaireNo=@DaireNo WHERE AdresId=@Id";

                    using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Sehir", textBox1.Text.ToLower());
                        command.Parameters.AddWithValue("@Ilce", textBox2.Text.ToLower());
                        command.Parameters.AddWithValue("@Mahalle", textBox3.Text.ToLower());
                        command.Parameters.AddWithValue("@Sokak", textBox4.Text.ToLower());
                        command.Parameters.AddWithValue("@AdresNo", textBox5.Text.ToLower());
                        command.Parameters.AddWithValue("@Blok", textBox6.Text.ToLower());
                        command.Parameters.AddWithValue("@DaireNo", textBox7.Text.ToLower());
                        command.Parameters.AddWithValue("@Id", dataGridView1.Rows[i].Cells[8].Value);

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
            textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();

        }
    }
}
