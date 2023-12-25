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
    public partial class DataBaseBackupAndRestore : Form
    {
        SqlConnection con=new SqlConnection("Data Source=DESKTOP-JUSKBE1\\SQLEXPRESS01;Initial Catalog=ProjeDeneme1;Integrated Security=True");
        public DataBaseBackupAndRestore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//browse1
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                button2.Enabled = true;//backup button
            }
        }

        private void button2_Click(object sender, EventArgs e)//yedekle
        {
            string database=con.Database.ToString();
            if(textBox1.Text==string.Empty)
            {
                MessageBox.Show("Lütfen yedeklemek için bir dosya konumu seçin");
            }
            else
            {
                string cmd = "BACKUP DATABASE [" + database + "] TO DISK= '" + textBox1.Text + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".bak'";

                con.Open();
                SqlCommand command=new SqlCommand(cmd,con);
                command.ExecuteNonQuery();
                MessageBox.Show("Veritabanı Başarıyla Yedeklendi");
                con.Close();
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)//browse2
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if(dlg.ShowDialog()== DialogResult.OK )
            {
                textBox2.Text = dlg.FileName;
                button4.Enabled = true;//yedekten geri yükle butonu
            }
        }

        private void button4_Click(object sender, EventArgs e)//restore
        {
            string database = con.Database.ToString();

            try
            {
                con.Open();

                // Set the database to SINGLE_USER mode
                string str1 = string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", database);
                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                // Restore the database
                string str2 = string.Format("USE MASTER RESTORE DATABASE [{0}] FROM DISK='{1}' WITH REPLACE", database, textBox2.Text);
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd2.ExecuteNonQuery();

                // Set the database back to MULTI_USER mode
                string str3 = string.Format("ALTER DATABASE [{0}] SET MULTI_USER", database);
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Veritabanının yedekten geri getirilmesi işlemi başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                // Add additional logging or debugging information here
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



    }
}
