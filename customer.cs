
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
using System.Text.RegularExpressions;

namespace project1
{
    public partial class customer : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        int r=0;
        
        public void auto_inc()
        {
            OleDbConnection con = new OleDbConnection(s);
            adp = new OleDbDataAdapter("select max(id) from customer", con);
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
            id.Text = cno.ToString();
        }
        public customer()
        {
            InitializeComponent();
        }
        public void t_clear()
        {
            id.Text = "";
            nm.Text = "";
            ad.Text = "";
            cty.Text = "";
            cn.Text = "";
            ed.Text = "";
        }
        public void display()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from customer";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        DataTable dt = new DataTable();

        protected void load_data(int pos)
        {
            OleDbConnection conn = new OleDbConnection(s);
            OleDbCommand cmdd = new OleDbCommand("select * from customer", conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            da.Fill(dt);
            id.Text = dt.Rows[pos][0].ToString();
            nm.Text = dt.Rows[pos][1].ToString();
            ad.Text = dt.Rows[pos][2].ToString();
            cty.Text = dt.Rows[pos][3].ToString();
            cn.Text = dt.Rows[pos][4].ToString();
            ed.Text = dt.Rows[pos][5].ToString();
        }
        
        private void insert_Click(object sender, EventArgs e)
        {
            auto_inc();
            id.Enabled = true;
            nm.Enabled = true;
            ad.Enabled = true;
            cty.Enabled = true;
            cn.Enabled = true;
            ed.Enabled = true;
        }

        private void show_Click(object sender, System.EventArgs e)
        {
            if (id.Text == "")
            {
                MessageBox.Show("please Enter add button for id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                if (string.IsNullOrEmpty(id.Text.Trim()))
                {
                    errorProvider1.SetError(id, "id is required.");
                    return;
                }
                else
                {
                    errorProvider1.SetError(id, string.Empty);
                }
                if (string.IsNullOrEmpty(nm.Text.Trim()))
                {
                    errorProvider2.SetError(nm, "name is required.");
                    return;
                }
                else
                {
                    errorProvider2.SetError(nm, string.Empty);
                }
                if (string.IsNullOrEmpty(ad.Text.Trim()))
                {
                    errorProvider3.SetError(ad, "address is required.");
                    return;
                }
                else
                {
                    errorProvider3.SetError(ad, string.Empty);
                }
                if (string.IsNullOrEmpty(cty.Text.Trim()))
                {
                    errorProvider4.SetError(cty, "city is required.");
                    return;
                }
                else
                {
                    errorProvider4.SetError(cty, string.Empty);
                }
                
                if (string.IsNullOrEmpty(cn.Text.Trim()))
                {
                    errorProvider5.SetError(cn, "mobile number is required.");
                    return;
                }
                else
                {
                    errorProvider5.SetError(cn, string.Empty);
                }
                if (string.IsNullOrEmpty(ed.Text.Trim()))
                {
                    errorProvider6.SetError(ed, "email is required.");
                    return;
                }
                else
                {
                    errorProvider6.SetError(ed, string.Empty);
                }
                if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    id.Enabled = true;
                    OleDbConnection con = new OleDbConnection(s);
                    OleDbCommand cmd;
                    con.Open();
                    cmd = new OleDbCommand("insert into customer values('" + id.Text + "','" + nm.Text + "','" + ad.Text + "','" + cty.Text + "','" + cn.Text + "','" + ed.Text + "')", con);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    t_clear();
                    display();

                }
            }
            id.Enabled = false;
            nm.Enabled = false;
            ad.Enabled = false;
            cty.Enabled = false;
            cn.Enabled = false;
            ed.Enabled = false;
        }

        private void edit_Click(object sender, System.EventArgs e)
        {
            if (id.Text == "")
            {
                if (MessageBox.Show("Please enter id for update record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    id.Enabled = true;
                    nm.Enabled = true;
                    ad.Enabled = true;
                    cty.Enabled = true;
                    cn.Enabled = true;
                    ed.Enabled = true;
                }
            }
            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select id from customer";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (id.Text == d.GetInt32(0).ToString())
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
                    String update = "update customer set nm='" + nm.Text + "',ad='" + ad.Text + "',cty='" + cty.Text + "',cn='" + cn.Text.ToString() + "',ed='" + ed.Text + "' where id=" + id.Text + "";
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

        private void delete_Click(object sender, System.EventArgs e)
        {
            if (id.Text == "")
            {

                if (MessageBox.Show("Please enter id for delete record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    id.Enabled = true;
                    nm.Enabled = false;
                    ad.Enabled = false;
                    cty.Enabled = false;
                    cn.Enabled = false;
                    ed.Enabled = false;
                }
            }

            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select id from customer";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    if (id.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                if (temp == 1)
                {
                    string q = "delete from customer where id=" + id.Text.ToString() + "";
                    OleDbCommand cmdd = new OleDbCommand(q, con);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    id.Enabled = false;
                    nm.Enabled = false;
                    ad.Enabled = false;
                    cty.Enabled = false;
                    cn.Enabled = false;
                    ed.Enabled = false;
                    t_clear();
                    display();
                }
                else
                {
                    MessageBox.Show("this is new id so can't delete....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void clear_Click(object sender, System.EventArgs e)
        {
            id.Text = "";
            nm.Text = "";
            ad.Text = "";
            cty.Text = "";          
            cn.Text = "";
            ed.Text = "";
            if (radioButton1.Checked == true)
            {
                id_cmb.Text = "";
            }
            if (radioButton2.Checked == true)
            {
                nm_cmb.Text = "";
                id_cmb.Text = "";
            }
        }

        private void exit_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radioButton1.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = true;
                nm_cmb.Visible = false;
                con.Open();
                string s1 = "select id from customer";
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    id_cmb.Items.Add(d[0].ToString());
                }
                con.Close();
            }
        }

        private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
        {

            if (radioButton2.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = false;
                nm_cmb.Visible = true;
                con.Open();
                string s1 = "select nm from customer";
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    nm_cmb.Items.Add(d[0].ToString());
                }
                con.Close();
            }
        }

        private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
        {
            display();
        }

        private void cid_Leave(object sender, System.EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string ss = "select id from customer";
            int temp = 0;
            OleDbCommand cmd1 = new OleDbCommand(ss, con);
            OleDbDataReader d1 = cmd1.ExecuteReader();

            while (d1.Read())
            {
                if (id.Text == d1.GetInt32(0).ToString())
                {
                    temp = 1;
                    break;
                }
            }
            if (temp == 1)
            {
                string s1 = "select * from customer where id=" + id.Text;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    nm.Text = d[1].ToString();
                    ad.Text = d[2].ToString();
                    cty.Text = d[3].ToString();
                    cn.Text = d[4].ToString();
                    ed.Text = d[5].ToString();

                }
                con.Close();
            }
            else
            {
                MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cnm_Leave(object sender, System.EventArgs e)
        {
            if (!Regex.Match(nm.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid customer Name ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nm.Focus();

            }
        }

        private void add_Leave(object sender, System.EventArgs e)
        {
            if (!Regex.Match(ad.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid customer address ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ad.Focus();

            }
        }

        private void ct_Leave(object sender, System.EventArgs e)
        {
            if (!Regex.Match(cty.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid city ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cty.Focus();

            }
        }

        private void cno_Leave(object sender, System.EventArgs e)
        {
            if (!Regex.Match(cn.Text, @"^\d{10}$").Success)
            {
                MessageBox.Show("Enter Only 10 Digit Or Phone Number", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.Focus();

            }
        }

        private void eid_Leave(object sender, System.EventArgs e)
        {
            if (!Regex.Match(ed.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                MessageBox.Show("Invalid Email ID", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ed.Focus();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                nm.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                ad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cty.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cn.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                ed.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
            }
        }
        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            id.Text = row.Cells["id"].Value.ToString();
            nm.Text = row.Cells["nm"].Value.ToString();
            ad.Text = row.Cells["ad"].Value.ToString();
            cty.Text = row.Cells["cty"].Value.ToString();
            cn.Text = row.Cells["cn"].Value.ToString();
            ed.Text = row.Cells["ed"].Value.ToString();
        }
      private void first_Click(object sender, System.EventArgs e)
        {
            display(0);
        }

        private void previous_Click(object sender, System.EventArgs e)
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

        private void next_Click(object sender, System.EventArgs e)
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

        private void last_Click(object sender, System.EventArgs e)
        {
            display(dataGridView1.RowCount - 2);
        }

        private void id_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {

            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string dis = "select * from customer where id=" + id_cmb.Text + "";
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
            string s1 = "select * from customer where nm='" + nm_cmb.Text + "'";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            var d = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void customer_Load(object sender, EventArgs e)
        {
            id_cmb.Visible = false;
            nm_cmb.Visible = false;
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from customer";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }

    }
}
