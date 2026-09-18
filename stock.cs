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
    public partial class stock : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        public stock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id_cmb.Text = "";
            nm_cmb.Text = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = true;
                nm_cmb.Visible = false;
                con.Open();
                string s1 = "select pid from stock";
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    id_cmb.Items.Add(d[0].ToString());
                }
                con.Close();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = false;
                nm_cmb.Visible = true;
                con.Open();
                string s1 = "select pnm from stock";
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    nm_cmb.Items.Add(d[0].ToString());
                }
                con.Close();
            }

        }
        public void display()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from stock";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void id_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string dis = "select * from stock where pid=" + id_cmb.Text + "";
                OleDbCommand cmd = new OleDbCommand(dis, con);
                var reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                con.Close();
            }

        }

        private void nm_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string s1 = "select * from stock where pnm='" + nm_cmb.Text + "'";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            var d = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }
    }
}
