using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project1
{
    public partial class info : Form
    {
        public info()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void info_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label5.Text = DateTime.Now.ToLongDateString();
            label5.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want To Exit ??", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login l1 = new login();
            l1.Show();
            this.Close();
        }
    }
}
