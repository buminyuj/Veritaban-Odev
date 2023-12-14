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
    public partial class AdminSelectionPanel : Form
    {
        public AdminSelectionPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//personel işlemlerine gider
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)//ürün işlemlerine gider
        {
            AdminProductPanel adminProductPanel = new AdminProductPanel();
            adminProductPanel.Show(); 
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)//sipariş işlemlerine gider
        {
            AdminOrderPanel adminOrderPanel=new AdminOrderPanel();
            adminOrderPanel.Show();
            this.Hide();
        }
    }
}
