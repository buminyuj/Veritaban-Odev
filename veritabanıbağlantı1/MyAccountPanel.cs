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
    public partial class MyAccountPanel : Form
    {
        public MyAccountPanel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)//Kartlarım
        {
            Form1 loginForm = new Form1();
            loginForm.ShowDialog();
            int loggedInCustomerID = loginForm.GetLoggedInCustomerID();
            MyCards mycards = new MyCards(loggedInCustomerID);
            mycards.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)//adreslerim
        {
            Form1 loginForm = new Form1();
            loginForm.ShowDialog();
            int loggedInCustomerID = loginForm.GetLoggedInCustomerID();
            MyAdresses myadres = new MyAdresses(loggedInCustomerID);
            myadres.Show();
            this.Hide();
        }
    }
    }

