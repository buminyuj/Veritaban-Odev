using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace veritabanıbağlantı1
{
    public partial class CommonLogin : Form
    {
        public CommonLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)//user login
        {
            Form1 geçiş = new Form1();
            geçiş.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)//admin(PERSONEL GİRİŞ NORMALDE)login
        {
            AdminLoginPage geçiş = new AdminLoginPage();
            geçiş.Show();
            this.Hide();
            
        }
    }
}
