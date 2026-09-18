using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace project1
{
    public partial class company : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        int r = 0;
        public void display()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from company";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void t_clear()
        {
            cid.Text = "";
            cnm.Text = "";
            address.Text = "";
            ct.Text = "";
            cno.Text = "";
            eid.Text = "";
        }
        public void auto_inc()
        {
            OleDbConnection con = new OleDbConnection(s);
            adp = new OleDbDataAdapter("select max(cid) from company", con);
            ds = new DataSet();
            adp.Fill(ds);

            int cno;

            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                cno = 101;
            }
            else
            {
                cno = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                cno = cno + 1;
            }
            cid.Text = cno.ToString();
        }
        public company()
        {
            InitializeComponent();
        }

        private void show_Click(object sender, EventArgs e)
        {
            if (cid.Text == "")
            {
                MessageBox.Show("please Enter add button for id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                if (string.IsNullOrEmpty(cid.Text.Trim()))
                {
                    errorProvider1.SetError(cid, "id is required.");
                    return;
                }
                else
                {
                    errorProvider1.SetError(cid, string.Empty);
                }
                if (string.IsNullOrEmpty(cnm.Text.Trim()))
                {
                    errorProvider2.SetError(cnm, "name is required.");
                    return;
                }
                else
                {
                    errorProvider2.SetError(cnm, string.Empty);
                }
                if (string.IsNullOrEmpty(address.Text.Trim()))
                {
                    errorProvider3.SetError(address, "address is required.");
                    return;
                }
                else
                {
                    errorProvider3.SetError(address, string.Empty);
                }
                if (string.IsNullOrEmpty(ct.Text.Trim()))
                {
                    errorProvider4.SetError(ct, "city is required.");
                    return;
                }
                else
                {
                    errorProvider4.SetError(ct, string.Empty);
                }               
                if (string.IsNullOrEmpty(cno.Text.Trim()))
                {
                    errorProvider5.SetError(cno, "contact number is required.");
                    return;
                }
                else
                {
                    errorProvider6.SetError(cno, string.Empty);
                }
                if (string.IsNullOrEmpty(eid.Text.Trim()))
                {
                    errorProvider6.SetError(eid, "email is required.");
                    return;
                }
                else
                {
                    errorProvider6.SetError(eid, string.Empty);
                }
                if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    cid.Enabled = true;
                    OleDbConnection con = new OleDbConnection(s);
                    OleDbCommand cmd;
                    con.Open();
                    cmd = new OleDbCommand("insert into company values('" + cid.Text + "','" + cnm.Text + "','" + address.Text + "','" + ct.Text + "','" + cno.Text + "','" + eid.Text + "')", con);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    t_clear();
                    display();

                }
            }
            cid.Enabled = false;
            cnm.Enabled = false;
            address.Enabled = false;
            ct.Enabled = false;
            cno.Enabled = false;
            eid.Enabled = false;       
        }

        private void insert_Click(object sender, EventArgs e)
        {
            auto_inc();
            cid.Enabled = true;
            cnm.Enabled = true;
            address.Enabled = true;
            ct.Enabled = true;
            cno.Enabled = true;
            eid.Enabled = true;
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (cid.Text == "")
            {
                if (MessageBox.Show("Please enter id for update record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    cid.Enabled = true;
                    cnm.Enabled = true;
                    address.Enabled = true;
                    ct.Enabled = true;
                    cno.Enabled = true;
                    eid.Enabled = true;
                }
            }
            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select cid from company";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (cid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                con.Close();
                if (temp == 1)
                {
                    con.Open();
                    OleDbCommand cmdd;
                    String update = "update company set cnm='" + cnm.Text + "',address='" + address.Text + "',ct='" + ct.Text + "',cno='" + cno.Text.ToString() + "',eid='" + eid.Text + "' where cid=" + cid.Text + "";
                    cmdd = new OleDbCommand(update, con);
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("update successfully");
                    display();
                }
                else
                {
                    MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (cid.Text == "")
            {

                if (MessageBox.Show("Please enter id for delete record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    cid.Enabled = true;
                    cnm.Enabled = false;
                    address.Enabled = false;
                    ct.Enabled = false;
                    cno.Enabled = false;
                    eid.Enabled = false;
                }
            }

            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select cid from company";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    if (cid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                if (temp == 1)
                {
                    string q = "delete from company where cid=" + cid.Text.ToString() + "";
                    OleDbCommand cmdd = new OleDbCommand(q, con);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cid.Enabled = false;
                    cnm.Enabled = false;
                    address.Enabled = false;
                    ct.Enabled = false;
                    cno.Enabled = false;
                    eid.Enabled = false;
                    t_clear();
                    display();
                }
                else
                {
                    MessageBox.Show("this is new id so can't delete....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            t_clear();
            if (radioButton1.Checked == true)
            {
                id_cmb.Text = "";
            }
            if (radioButton2.Checked == true)
            {
                nm_cmb.Text = "";
                id_cmb.Text = "";
            }
            cid.Enabled = false;
            cnm.Enabled = false;
            address.Enabled = false;
            ct.Enabled = false;
            cno.Enabled = false;
            eid.Enabled = false;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                cnm.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                address.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                ct.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cno.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                eid.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
            }
        }
        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            cid.Text = row.Cells["cid"].Value.ToString();
            cnm.Text = row.Cells["cnm"].Value.ToString();
            address.Text = row.Cells["address"].Value.ToString();
            ct.Text = row.Cells["ct"].Value.ToString();
            cno.Text = row.Cells["cno"].Value.ToString();
            eid.Text = row.Cells["eid"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (r != 0)
            {
                r--;
                display(r);
            }
            else
            {
                MessageBox.Show("You are on First record");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (r != dataGridView1.RowCount - 2)
            {
                r++;
                display(r);
            }
            else
            {
                MessageBox.Show("You are on Last record");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            display(dataGridView1.RowCount - 2);
        }

        private void id_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string dis = "select * from company where cid=" + id_cmb.Text + "";
            OleDbCommand cmd = new OleDbCommand(dis, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void nm_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string s1 = "select * from company where cnm='" + nm_cmb.Text + "'";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            var d = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = true;
                nm_cmb.Visible = false;
                con.Open();
                string s1 = "select cid from company";
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
                string s1 = "select cnm from company";
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    nm_cmb.Items.Add(d[0].ToString());
                }
                con.Close();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void company_Load(object sender, EventArgs e)
        {
            id_cmb.Visible = false;
            nm_cmb.Visible = false;
            display();
        }

        private void cid_Leave(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string ss = "select cid from company";
            int temp = 0;
            OleDbCommand cmd1 = new OleDbCommand(ss, con);
            OleDbDataReader d1 = cmd1.ExecuteReader();

            while (d1.Read())
            {
                if (cid.Text == d1.GetInt32(0).ToString())
                {
                    temp = 1;
                    break;
                }
            }
            if (temp == 1)
            {
                string s1 = "select * from company where cid=" + cid.Text;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    cnm.Text = d[1].ToString();
                    address.Text = d[2].ToString();
                    ct.Text = d[3].ToString();
                    cno.Text = d[4].ToString();
                    eid.Text = d[5].ToString();

                }
                con.Close();
            }
            else
            {
                MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cnm_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(cnm.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid company Name ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cnm.Focus();

            }
        }

        private void ct_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.Match(ct.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid city ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ct.Focus();
            }
        }

        private void cno_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(cno.Text, @"^\d{10}$").Success)
            {
                MessageBox.Show("Enter Only 10 Digit Or Phone Number", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cno.Focus();

            }
        }

        private void eid_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(eid.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                MessageBox.Show("Invalid Email ID", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                eid.Focus();
            }
        }  
    }
}
