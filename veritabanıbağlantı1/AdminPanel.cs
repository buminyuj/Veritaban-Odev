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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }


        public void ShowPersonelRecords()
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryList = "SELECT * FROM Personel";

                using (SqlCommand command = new SqlCommand(queryList, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        public void DeletePersonal(int id)
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryDell = "DELETE FROM Personel WHERE PersonelId=@Id";

                using (SqlCommand command = new SqlCommand(queryDell, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



        private void button1_Click(object sender, EventArgs e) //EKLE
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
            string Ad = textBox1.Text;
            string Soyad = textBox2.Text;
            string Rol = textBox3.Text;
            string Maaş = textBox4.Text;
            var IseAlimTarihi = maskedTextBox1.Text;


            try
            {
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {
                    connnection.Open();
                    //string queryAdd = "SELECT PersonelAd,PersonelSoyad,PersonelRol,PersonelMaaş,IseAlimTarihi WHERE PersonelAd=@Ad AND PersonelSoyad=@Soyad AND PersonelRol=@Rol AND PersonelMaaş=@Maaş AND IseAlimTarihi=@IseAlimTarihi";
                    string queryAdd = "INSERT INTO Personel (PersonelAd,PersonelSoyad,PersonelRol,PersonalMaas,IseAlimTarihi) VALUES (@Ad,@Soyad,@Rol,@Maaş,@IseAlimTarihi)";
                    using (SqlCommand command = new SqlCommand(queryAdd, connnection))
                    {
                        command.Parameters.AddWithValue("@Ad", Ad);
                        command.Parameters.AddWithValue("@Soyad", Soyad);
                        command.Parameters.AddWithValue("@Rol", Rol);
                        command.Parameters.AddWithValue("@Maaş", Maaş);
                        command.Parameters.AddWithValue("@IseAlimTarihi", IseAlimTarihi);
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.");
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

        private void button3_Click(object sender, EventArgs e) //LİSTELE
        {
            ShowPersonelRecords();
            AdminPanelineDön();

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        public void AdminPanelineDön()
        {
            DialogResult result = MessageBox.Show("Admin paneline dönmek istiyor musunuz?", "Dön", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.Show();
                this.Hide();
            }


        }


        private void button4_Click(object sender, EventArgs e) //SİL
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                DeletePersonal(id);
                ShowPersonelRecords();
                //buranın aşağısındaki kodu silersek silinmiş halini otomatik oluşturur fakat personel ekleme paneline geçmez
                //AdminPanel adminPanel = new AdminPanel();
                //adminPanel.Show();
                //this.Hide();
                AdminPanelineDön();
            }
        }
        int i = 0;
        private void button2_Click(object sender, EventArgs e) //DÜZENLE
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryUpdate = "UPDATE Personel SET PersonelAd=@Ad, PersonelSoyad=@Soyad, PersonelRol=@Rol, PersonalMaas=@Maaş, IseAlimTarihi=@IseAlimTarihi WHERE PersonelId=@Id";

                    using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Ad", textBox1.Text);
                        command.Parameters.AddWithValue("@Soyad", textBox2.Text);
                        command.Parameters.AddWithValue("@Rol", textBox3.Text);
                        command.Parameters.AddWithValue("@Maaş", Convert.ToDecimal(textBox4.Text));

                        // Tarih değerini DateTime türüne dönüştürme
                        DateTime iseAlimTarihi;

                        // Eğer tarih bilgisi girilmişse, dönüşüm yap
                        if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text) && DateTime.TryParse(maskedTextBox1.Text, out iseAlimTarihi))
                        {
                            command.Parameters.Add("@IseAlimTarihi", SqlDbType.DateTime).Value = iseAlimTarihi;
                        }
                        else
                        {
                            // Eğer tarih bilgisi girilmemişse, mevcut tarih bilgisini kullan
                            command.Parameters.Add("@IseAlimTarihi", SqlDbType.DateTime).Value = dataGridView1.Rows[i].Cells[3].Value;
                        }

                        command.Parameters.AddWithValue("@Id", dataGridView1.Rows[i].Cells[0].Value);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Kayıtlar Başarıyla Güncellendi!");
                        ShowPersonelRecords();
                        AdminPanelineDön();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            //finally { this.Close(); } u kapatmak için sorup çalışacak şekilde ekleyebiliriz
        }



        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[i].Cells[3].Value as string;


        }
    }
}
