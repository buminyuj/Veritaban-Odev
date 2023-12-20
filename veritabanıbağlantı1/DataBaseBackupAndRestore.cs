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

                // Veritabanını kapatabilmeniz için kullanıcıyı SINGLE_USER moduna getirme
                string str1 = string.Format("ALTER DATABASE [ " + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ");
                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                // Restore işlemi
                string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + textBox2.Text + "' WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd2.ExecuteNonQuery();

                // Kullanıcıyı tekrar MULTI_USER moduna getirme
                string str3 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Veritabanının yedekten geri getirilmesi işlemi başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
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
