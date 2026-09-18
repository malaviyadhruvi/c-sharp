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
    public partial class product : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter da;
        
        DataTable dtt = new DataTable();
        int r = 0;
        public void display()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from product";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void t_clear()
        {
            pid.Text = "";
            pnm.Text = "";
            pprice.Text = "";
            pqty.Text = "";
            cid.Text = "";
            cnm.Text = "";
        }
       
        public product()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
       // int index;
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        public void auto_inc()
        {
            OleDbConnection con = new OleDbConnection(s);
            adp = new OleDbDataAdapter("select max(pid) from product", con);
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
            pid.Text = cno.ToString();
        }
        protected void load_data(int pos)
        {

            OleDbConnection conn = new OleDbConnection(s);
            OleDbCommand cmdd = new OleDbCommand("select * from product", conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdd);
            da.Fill(dt);
            pid.Text = dt.Rows[pos][0].ToString();
            pnm.Text = dt.Rows[pos][1].ToString();
            pprice.Text = dt.Rows[pos][2].ToString();
            pqty.Text = dt.Rows[pos][3].ToString();  
 
        }
        
        private void insert_Click(object sender, EventArgs e)
        {
            auto_inc();
            pid.Enabled = true;
            pnm.Enabled = true;
            pprice.Enabled = true;
            pqty.Enabled = true;
            cid.Enabled = true;
            cnm.Enabled = true;
        }

        private void cid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string s1 = "select cnm from company where cid=" + cid.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);
            cnm.Text = dss.Tables[0].Rows[0][0].ToString();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (pid.Text == "")
            {
                MessageBox.Show("please Enter add button for id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else
            {
                if (string.IsNullOrEmpty(pid.Text.Trim()))
                {
                    errorProvider1.SetError(pid, "id is required.");
                    return;
                }
                else
                {
                    errorProvider1.SetError(pid, string.Empty);
                }
                if (string.IsNullOrEmpty(pnm.Text.Trim()))
                {
                    errorProvider2.SetError(pnm, "name is required.");
                    return;
                }
                else
                {
                    errorProvider2.SetError(pnm, string.Empty);
                }
                if (string.IsNullOrEmpty(pprice.Text.Trim()))
                {
                    errorProvider3.SetError(pprice, "price is required.");
                    return;
                }
                else
                {
                    errorProvider3.SetError(pprice, string.Empty);
                }

                if (string.IsNullOrEmpty(pqty.Text.Trim()))
                {
                    errorProvider4.SetError(pqty, "product qty is required.");
                    return;
                }
                else
                {
                    errorProvider4.SetError(pqty, string.Empty);
                }
                if (string.IsNullOrEmpty(cid.Text.Trim()))
                {
                    errorProvider5.SetError(cid, "company id is required.");
                    return;
                }
                else
                {
                    errorProvider5.SetError(cid, string.Empty);
                }

                if (string.IsNullOrEmpty(cnm.Text.Trim()))
                {
                    errorProvider6.SetError(cnm, "company name is required.");
                    return;
                }
                else
                {
                    errorProvider6.SetError(cnm, string.Empty);
                }

                if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    pid.Enabled = true;
                    OleDbConnection con = new OleDbConnection(s);
                    OleDbCommand cmd;
                    con.Open();
                    cmd = new OleDbCommand("insert into product values('" + pid.Text.ToString() + "','" + pnm.Text + "','" + pprice.Text + "','" + pqty.Text.ToString() + "','" + cid.Text.ToString() + "','" + cnm.Text + "')", con);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    t_clear();
                    display();

                }
            }
            pid.Enabled = false;
            pnm.Enabled = false;
            pprice.Enabled = false;
            pqty.Enabled = false;
            cid.Enabled = true;
            cnm.Enabled = false;
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (pid.Text == "")
            {
                if (MessageBox.Show("Please enter id for update record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    pid.Enabled = true;
                    pnm.Enabled = true;
                    pprice.Enabled = true;
                    pqty.Enabled = true;
                    cid.Enabled = true;
                    cnm.Enabled = true;
                }
            }
            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select pid from product";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    if (pid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                con.Close();
                if (temp == 1)
                {
                    String update = "update product set pnm='" + pnm.Text + "',pprice='" + pprice.Text + "',pqty='" + pqty.Text + "',cid='" + cid.Text + "',cnm='" + cnm.Text + "' where pid=" + pid.Text + "";
                    con.Open();
                    OleDbCommand cmdd = new OleDbCommand(update, con);
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully update", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    t_clear();
                    display();
                    pid.Enabled = false;
                    pnm.Enabled = false;
                    pprice.Enabled = false;
                    pqty.Enabled = false;
                    cid.Enabled = false;
                    cnm.Enabled = false;
                }
                else
                {
                    MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (pid.Text == "")
            {

                if (MessageBox.Show("Please enter id for delete record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    pid.Enabled = true;
                    pnm.Enabled = false;
                    pprice.Enabled = false;
                    pqty.Enabled = false;
                    cid.Enabled = false;
                    cnm.Enabled = false;
                }
            }

            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select pid from product";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    if (pid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                if (temp == 1)
                {
                    string q = "delete from product where pid=" + pid.Text.ToString() + "";
                    OleDbCommand cmdd = new OleDbCommand(q, con);
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    pid.Enabled = false;
                    pnm.Enabled = false;
                    pprice.Enabled = false;
                    pqty.Enabled = false;
                    cid.Enabled = false;
                    cnm.Enabled = false;
                    t_clear();
                    display();
                }
                else
                {
                    MessageBox.Show("this is new id so can't delete.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            pid.Text = "";
            pnm.Text = "";
            pprice.Text = "";
            pqty.Text = "";
            cid.Text = "";
            cnm.Text = "";
            if (radioButton1.Checked == true)
            {
                id_cmb.Text = "";
            }
            if (radioButton2.Checked == true)
            {
                nm_cmb.Text = "";
                id_cmb.Text = "";
            }
            pid.Enabled = false;
            pnm.Enabled = false;
            pprice.Enabled = false;
            pqty.Enabled = false;
            cid.Enabled = false;
            cnm.Enabled = false;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            id_cmb.Visible = true;
            if (radioButton1.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = true;
                nm_cmb.Visible = false;
                con.Open();
                string s1 = "select pid from product";
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
            nm_cmb.Visible = true;
            if (radioButton2.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = false;
                nm_cmb.Visible = true;
                con.Open();
                string s1 = "select pnm from product";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                pnm.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                pprice.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                pqty.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cid.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                cnm.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
              
              
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
            }
        }
        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            pid.Text = row.Cells["pid"].Value.ToString();
            pnm.Text = row.Cells["pnm"].Value.ToString();
            pprice.Text = row.Cells["pprice"].Value.ToString();
            pqty.Text = row.Cells["pqty"].Value.ToString();
            cid.Text = row.Cells["cid"].Value.ToString();
            cnm.Text = row.Cells["cnm"].Value.ToString();
        }

        private void first_Click(object sender, EventArgs e)
        {
             display(0);
        }

        private void previous_Click(object sender, EventArgs e)
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

        private void next_Click(object sender, EventArgs e)
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

        private void last_Click(object sender, EventArgs e)
        {
            display(dataGridView1.RowCount - 2);
        }

        private void product_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string s1 = "select cid from company";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            OleDbDataReader d = cmd.ExecuteReader();
            while (d.Read())
            {
                cid.Items.Add(d[0].ToString());
            }           
            id_cmb.Visible = false;
            nm_cmb.Visible = false;
            string di = "select * from product";
            OleDbCommand cmd6 = new OleDbCommand(di, con);
            var reader = cmd6.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void pid_Leave(object sender, EventArgs e)
        {
            /*OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string ss = "select pid from product";
            int temp = 0;
            OleDbCommand cmd1 = new OleDbCommand(ss, con);
            OleDbDataReader d1 = cmd1.ExecuteReader();

            while (d1.Read())
            {
                if (pid.Text == d1.GetInt32(0).ToString())
                {
                    temp = 1;
                    break;
                }
            }
            if (temp == 1)
            {
                string s1 = "select * from product where pid=" + pid.Text;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    pnm.Text = d[1].ToString();
                    pprice.Text = d[2].ToString();
                    pqty.Text = d[3].ToString();
                    cid.Text = d[4].ToString();
                    cnm.Text = d[5].ToString();

                }
                con.Close();
            }
            else
            {
                MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
        }

        private void pnm_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(pnm.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid product Name ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pnm.Focus();

            }
        }

        private void pprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cnm_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(cnm.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid item Name ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cnm.Focus();

            }
        }

        private void cid_Leave(object sender, EventArgs e)
        {
          /*  if (!Regex.Match(cid.Text, @"^\d{3}$").Success)
            {

                MessageBox.Show("Enter Only 3 Digit Or valid id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cid.Focus();
            }*/
        }

        private void id_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string dis = "select * from product where pid=" + id_cmb.Text + "";
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
            string s1 = "select * from product where pnm='" + nm_cmb.Text + "'";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            var d = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
