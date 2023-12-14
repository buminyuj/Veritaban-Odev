using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.Sql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace veritabanıbağlantı1
{
    public partial class AdminProductPanel : Form
    {
        public AdminProductPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//admin seçim paneline geri döner
        {
            AdminSelectionPanel adminSelectionPanel = new AdminSelectionPanel();
            adminSelectionPanel.Show();
            this.Close();
        }
        public void AdminProductPanelineDön()//admin ürün paneline dönme fonksiyonu
        {
            DialogResult result = MessageBox.Show("Admin ürün paneline dönmek istiyor musunuz?", "Dön", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                AdminProductPanel adminProductPanel = new AdminProductPanel();
                adminProductPanel.Show();
                this.Hide();
            }
        }


        private void button2_Click_1(object sender, EventArgs e)//Urun ekleme butonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
            string UrunAdi = textBox1.Text.ToUpper();
            string Marka = textBox2.Text.ToUpper();
            string Model = textBox3.Text.ToUpper();
            string Fiyat = textBox4.Text;
            string StokMiktari = textBox5.Text;
            string GarantiSuresi = textBox6.Text;
            string Ozellikler = textBox7.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryProductAdd = "INSERT INTO Urun (UrunAdi, Marka, Model,Fiyat,StokMiktari,GarantiSuresi,Ozellikler) VALUES(@UrunAdi,@Marka,@Model,@Fiyat,@StokMiktari,@GarantiSuresi,@Ozellikler)";
                    using (SqlCommand command = new SqlCommand(queryProductAdd, connection))
                    {
                        command.Parameters.AddWithValue("@UrunAdi", UrunAdi);
                        command.Parameters.AddWithValue("@Marka", Marka);
                        command.Parameters.AddWithValue("@Model", Model);
                        command.Parameters.AddWithValue("@Fiyat", Fiyat);
                        command.Parameters.AddWithValue("@StokMiktari", StokMiktari);
                        command.Parameters.AddWithValue("@GarantiSuresi", GarantiSuresi);
                        command.Parameters.AddWithValue("@Ozellikler", Ozellikler);
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Ürün Kaydı başarıyla eklendi.");
                            connection.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenirken bir hata oluştu.");
                        }
                    }
                }
            }
            catch
            {

            }
        }//button2click(ekle)bitiş parantezi

        public void ShowProductRecords()//Urun listeleme fonksiyonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryList = "SELECT * FROM Urun";

                using (SqlCommand command = new SqlCommand(queryList, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    connection.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//Urun Listele Butonu
        {
            dataGridView1.AllowUserToAddRows = false;
            ShowProductRecords();
            AdminProductPanelineDön();
        }

        int i=0;
        

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)//seçilen satırın bilgilerini textboxlara getirme
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

        private void button4_Click(object sender, EventArgs e)//Urun Duzenleme butonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryProductUpdate = "UPDATE Urun SET UrunAdi=@UrunAdi, Marka=@Marka, Model=@Model, Fiyat=@Fiyat, StokMiktari=@StokMiktari,GarantiSuresi=@GarantiSuresi,Ozellikler=@Ozellikler WHERE UrunId=@Id";

                    using (SqlCommand command = new SqlCommand(queryProductUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@UrunAdi", textBox1.Text.ToUpper());
                        command.Parameters.AddWithValue("@Marka", textBox2.Text.ToUpper());
                        command.Parameters.AddWithValue("@Model", textBox3.Text.ToUpper());
                        command.Parameters.AddWithValue("@Fiyat", Convert.ToDecimal(textBox4.Text));
                        command.Parameters.AddWithValue("@StokMiktari", textBox5.Text);
                        command.Parameters.AddWithValue("@GarantiSuresi", textBox6.Text);
                        command.Parameters.AddWithValue("@Ozellikler", textBox7.Text);
                        command.Parameters.AddWithValue("@Id", dataGridView1.Rows[i].Cells[0].Value);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kayıtlar Başarıyla Güncellendi!");
                        ShowProductRecords();
                        AdminProductPanelineDön();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
        }
        public void DeleteProduct(int id)//Urun silme fonksiyonu
        {
            string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryDellProduct = "DELETE FROM Urun WHERE UrunId=@Id";

                using (SqlCommand command = new SqlCommand(queryDellProduct, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)//Urun silme butonu
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                DeleteProduct(id);
                ShowProductRecords();
                AdminProductPanelineDön();
            }
        }
    }

    




}
