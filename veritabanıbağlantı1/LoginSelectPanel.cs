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
    public partial class LoginSelectPanel : Form
    {
        public LoginSelectPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserRegister geçiş = new UserRegister();
            geçiş.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonLogin geçiş = new CommonLogin();
            geçiş.Show();
            this.Hide();
        }
    }
}
