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
    public partial class AdminOrderPanel : Form
    {
        public AdminOrderPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//admin seçim paneline geri döner
        {
            AdminSelectionPanel adminSelectionPanel = new AdminSelectionPanel();
            adminSelectionPanel.Show();
            this.Close();
        }

       
    }
}
