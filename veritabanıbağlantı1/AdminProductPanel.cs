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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.OleDb;


namespace veritabanıbağlantı1
{
    public partial class AdminProductPanel : Form
    {
       SqlConnection con = new SqlConnection("Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True");
        private UserPanel userPanel;
        public string textforlabel1;
        public string textforlabel2;
        public string textforlabel3;
        public string textforlabel4;
        public string textforlabel5;
        public string textforlabel6;
        public string textforlabel7;
        public AdminProductPanel()
        {
            InitializeComponent();
            userPanel = new UserPanel();
        }

        private void button1_Click(object sender, EventArgs e)//admin seçim paneline geri döner
        {
            AdminSelectionPanel adminSelectionPanel = new AdminSelectionPanel();
            adminSelectionPanel.Show();
            this.Hide();
            
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
             textforlabel1= textBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textforlabel2= textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textforlabel3=textBox3.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textforlabel4 = textBox4.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textforlabel5 = textBox5.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textforlabel6 = textBox6.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textforlabel7 = textBox7.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
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
        public void SearchProductName()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductName = "SELECT * FROM Urun Where UrunAdi=@UrunAdi";
                SqlCommand command = new SqlCommand(querySearchProductName, connection);
                command.Parameters.AddWithValue("@UrunAdi", textBox8.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex) {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox8.Clear();
        }


        public void SearchProductBrand()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductBrand = "SELECT * FROM Urun Where Marka=@Marka";
                SqlCommand command = new SqlCommand(querySearchProductBrand, connection);
                command.Parameters.AddWithValue("@Marka", textBox9.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox9.Clear();
        }

        public void SearchProductModel()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductBrand = "SELECT * FROM Urun Where Model=@Model";
                SqlCommand command = new SqlCommand(querySearchProductBrand, connection);
                command.Parameters.AddWithValue("@Model", textBox10.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox10.Clear();
        }

        public void SearchProductPrice()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductBrand = "SELECT * FROM Urun Where Fiyat=@Fiyat";
                SqlCommand command = new SqlCommand(querySearchProductBrand, connection);
                command.Parameters.AddWithValue("@Fiyat", textBox11.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox11.Clear();
        }

        public void SearchProductStock()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductBrand = "SELECT * FROM Urun Where StokMiktari=@StokMiktari";
                SqlCommand command = new SqlCommand(querySearchProductBrand, connection);
                command.Parameters.AddWithValue("@StokMiktari", textBox12.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox12.Clear();
        }

        public void SearchProductWarrantyPeriod()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductBrand = "SELECT * FROM Urun Where GarantiSuresi=@GarantiSuresi";
                SqlCommand command = new SqlCommand(querySearchProductBrand, connection);
                command.Parameters.AddWithValue("@GarantiSuresi", textBox13.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox13.Clear();
        }

        public void SearchProductFeatures()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string querySearchProductBrand = "SELECT * FROM Urun Where Ozellikler=@Ozellikler";
                SqlCommand command = new SqlCommand(querySearchProductBrand, connection);
                // Veri türünü belirt
                command.Parameters.AddWithValue("@Ozellikler", textBox14.Text);
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adptr = new SqlDataAdapter(command);
                adptr.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.ToString());
            }
            textBox14.Clear();
        }



        private void button6_Click(object sender, EventArgs e)//Urun arama butonu
        {
            if(textBox8.Text.Trim() != "") 
            {
                SearchProductName();
            }
            else if (textBox9.Text.Trim() != "")
            {
                SearchProductBrand();
            }
            else if (textBox10.Text.Trim() != "")
            {
                SearchProductModel();
            }
            else if (textBox11.Text.Trim() != "")
            {
                SearchProductPrice();
            }
            else if (textBox12.Text.Trim() != "")
            {
                SearchProductStock();
            }
            else if (textBox13.Text.Trim() != "")
            {
                SearchProductWarrantyPeriod();
            }
            else if (textBox14.Text.Trim() != "")
            {
                SearchProductFeatures();
            }
            AdminProductPanelineDön();
        }

        private void button7_Click(object sender, EventArgs e)//excele aktar butonu
        {
            dataGridView1.SelectAll();
            DataObject copydata = dataGridView1.GetClipboardContent();
            if (copydata != null) Clipboard.SetDataObject(copydata);
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlwbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object miseddata = System.Reflection.Missing.Value;
            xlwbook = xlapp.Workbooks.Add(miseddata);

            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlwbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            xlr.Select();
            xlsheet.Columns.ColumnWidth = 15;
            xlsheet.Rows.RowHeight = 20;



            xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // TextBox'tan indeksi alın
            if (int.TryParse(textBox15.Text, out int labelIndex))
            {
                // Örneğin, UserPanel'deki yeni eklenen label'i güncellemek için:
                userPanel.UpdateLabelText(labelIndex, $"{textforlabel1}\n{textforlabel2}\n{textforlabel3}\n{textforlabel4}\n{textforlabel5}\n{textforlabel6}\n{textforlabel7}");
                MessageBox.Show("Label Başarıyla Güncellendi!");

                // Formu gizle
                //userPanel.Hide();

                // Ana ekranı göster
                DialogResult result = MessageBox.Show("Ana ekranı görmek istiyor musunuz?", "Gör", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    userPanel.Show();
                    DialogResult result2 = MessageBox.Show("Ana ekranı kapatmak istiyor musunuz?", "Kapat", MessageBoxButtons.YesNo);
                    if ( result2 == DialogResult.Yes)
                    {
                        userPanel.Hide();
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Geçerli bir sayı girin.");
            }
        }

        private void button9_Click(object sender, EventArgs e)//Browse button
        {
            OpenFileDialog fdlg=new OpenFileDialog();
            fdlg.Title = "Select File";
            fdlg.FileName=textBox16.Text;
            fdlg.Filter = "Excel Sheet (*.xls)|*.xls|All Files(*.*)|(*.*)";
            fdlg.FilterIndex = 1;//sonradan 0 yapmak gerekebilir.
            fdlg.RestoreDirectory = true;
            if(fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox16.Text = fdlg.FileName;
            }
        }

        private void button10_Click(object sender, EventArgs e)//import button
        {
             //OleDbConnection theConnection = new OleDbConnection(@"provider=Microsoft.Jet.OLEDB.4.0;data source='" + textBox16.Text + "';Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1\"");
            OleDbConnection theConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + textBox16.Text + "';Extended Properties='Excel 12.0;HDR=NO;IMEX=1'");

            //OleDbConnection theConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + textBox16.Text + "';Extended Properties='Excel 12.0;HDR=NO;IMEX=1'");


            theConnection.Open();
            OleDbDataAdapter theDataAdapter = new OleDbDataAdapter("Select * From[Sayfa1$]", theConnection);
            DataSet theSD=new DataSet();
            System.Data.DataTable dt =new System.Data.DataTable();
            theDataAdapter.Fill(dt);
            this.dataGridView1.DataSource = dt.DefaultView;
        }

        void fillGrid()
        {
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True");
            con.Open();
            SqlDataAdapter da=new SqlDataAdapter("select * from Urun",con);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void AdminProductPanel_Load(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void button11_Click(object sender, EventArgs e)//save button
        {
            SqlConnection con2 = new SqlConnection("Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True");
            con2.Open();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                SqlCommand cmdnd = new SqlCommand("Insert into Urun(UrunAdi,Marka,Model,Fiyat,StokMiktari,GarantiSuresi,Ozellikler) values(@UrunAdi, @Marka, @Model, @Fiyat, @StokMiktari, @GarantiSuresi, @Ozellikler)", con2);
                cmdnd.Parameters.AddWithValue("@UrunAdi", dataGridView1.Rows[i].Cells[1].Value.ToString());
                cmdnd.Parameters.AddWithValue("@Marka", dataGridView1.Rows[i].Cells[2].Value.ToString());
                cmdnd.Parameters.AddWithValue("@Model", dataGridView1.Rows[i].Cells[3].Value.ToString());
                cmdnd.Parameters.AddWithValue("@Fiyat", dataGridView1.Rows[i].Cells[4].Value);
                cmdnd.Parameters.AddWithValue("@StokMiktari", dataGridView1.Rows[i].Cells[5].Value);
                cmdnd.Parameters.AddWithValue("@GarantiSuresi", dataGridView1.Rows[i].Cells[6].Value);
                cmdnd.Parameters.AddWithValue("@Ozellikler", dataGridView1.Rows[i].Cells[7].Value);
                cmdnd.ExecuteNonQuery();

            }
            con2.Close();

            MessageBox.Show("ÜRÜNLER KAYDEDİLDİ...");
            fillGrid();
        }
    }

}
