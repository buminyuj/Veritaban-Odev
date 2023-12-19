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
    public partial class UserPanel : Form
    {
        private List<Label> labelList = new List<Label>();
        public UserPanel()
        {
            InitializeComponent();
            labelList.Add(label1);
            labelList.Add(label2);
            labelList.Add(label3);
            labelList.Add(label4);
            labelList.Add(label5);
        }

        public void UpdateLabelText(int labelIndex, string newText)
        {
            if (labelIndex >= 0 && labelIndex < labelList.Count)
            {
                labelList[labelIndex].Text = newText;
            }
            else
            {
                MessageBox.Show("Geçersiz label indeksi.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserPanel_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//Kayıt Ol
        {
            UserRegister register = new UserRegister();
            register.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)//Hesabım
        {
         
            MyAccountPanel mmyaccount= new MyAccountPanel();
            mmyaccount.Show();
            this.Hide();
        }
    }
    }

