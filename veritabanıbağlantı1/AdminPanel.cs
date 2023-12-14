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

        public void SearchPersonelAd()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string querySearch = "SELECT * FROM Personel WHERE PersonelAd=@Ad";


                    using (SqlCommand command = new SqlCommand(querySearch, connection))
                    {

                        command.Parameters.AddWithValue("@Ad", textBox5.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox5.Clear();
        } //hepsi aynı kod sadece farklı textboxlardan değer almak için aradıkları column ve bilgi aldıkları textbox değişiyor
        public void SearchPersonelSoyad()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string querySearch = "SELECT * FROM Personel WHERE PersonelSoyad=@Soyad";


                    using (SqlCommand command = new SqlCommand(querySearch, connection))
                    {

                        command.Parameters.AddWithValue("@Soyad", textBox6.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox6.Clear();
        }
        public void SearchPersonelRol()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string querySearch = "SELECT * FROM Personel WHERE PersonelRol=@Rol";


                    using (SqlCommand command = new SqlCommand(querySearch, connection))
                    {

                        command.Parameters.AddWithValue("@Rol", textBox7.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox7.Clear();
        }
        public void SearchPersonelId()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string querySearch = "SELECT * FROM Personel WHERE PersonelId=@Id";


                    using (SqlCommand command = new SqlCommand(querySearch, connection))
                    {

                        command.Parameters.AddWithValue("@Id", textBox8.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox8.Clear();
        }
        public void SearchPersonelMaas()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True"; // Bağlantı dizesini kendi veritabanı bağlantınıza uygun şekilde değiştirin.

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string querySearch = "SELECT * FROM Personel WHERE PersonalMaas=@Maas";


                    using (SqlCommand command = new SqlCommand(querySearch, connection))
                    {

                        command.Parameters.AddWithValue("@Maas", textBox9.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox9.Clear();
        }
        public void SearchPersonelByDate()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // MaskedTextBox'ın içeriğini DateTime'a çevirme
                    if (DateTime.TryParse(maskedTextBox2.Text, out DateTime selectedDate))
                    {
                        // Eğer başarıyla çevrilebilirse, sorguyu çalıştır
                        string querySearch = "SELECT * FROM Personel WHERE IseAlimTarihi = @IseAlimTarihi";

                        using (SqlCommand command = new SqlCommand(querySearch, connection))
                        {
                            command.Parameters.AddWithValue("@IseAlimTarihi", selectedDate);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir tarih giriniz.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }

            maskedTextBox2.Clear(); // MaskedTextBox'ı temizle
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
           // dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

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

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim() != "")
            {
                // TextBox5 doluysa SearchPersonelAd fonksiyonunu çağır
                SearchPersonelAd();
            }
            else if (textBox6.Text.Trim() != "")
            {
                // TextBox6 doluysa SearchPersonelSoyad fonksiyonunu çağır
                SearchPersonelSoyad();
            }
            else if (textBox7.Text.Trim() != "")
            // TextBox7 doluysa SearchPersonelSoyad fonksiyonunu çağır
            {
                SearchPersonelRol();
            }
            else if (textBox8.Text.Trim() != "")
            {
                // TextBox8 doluysa SearchPersonelSoyad fonksiyonunu çağır
                SearchPersonelId();
            }
            else if (textBox9.Text.Trim() != "")//pushdenemelikyorumsatırı
            {
                SearchPersonelMaas();
            }
            else if (maskedTextBox2.Text.Trim() != "")
            {
               SearchPersonelByDate();
            }

            AdminPanelineDön();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
            DataObject copydata=dataGridView1.GetClipboardContent();
            if (copydata != null) Clipboard.SetDataObject(copydata);
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlwbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object miseddata = System.Reflection.Missing.Value;
            xlwbook=xlapp.Workbooks.Add(miseddata);

            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlwbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1,1];
            xlr.Select();
            xlsheet.Columns.ColumnWidth = 15;
            xlsheet.Rows.RowHeight = 20;



            xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
           
        }
    }
}
