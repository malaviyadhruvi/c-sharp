using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace project1
{
    public partial class login : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb");
                                                    
        OleDbDataReader dr;
        OleDbCommand cmd;
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from login where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Login Successfully...", "LOGIN", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                mdi m = new mdi();
                m.Show();

            }
            else
            {
                MessageBox.Show("Invalid UserName And Password.....", "INVALID", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
