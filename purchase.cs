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
    public partial class purchase : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        int r = 0;
        DataTable dt = new DataTable();
        int q1 = 0, q2;
        public purchase()
        {
            InitializeComponent();
        }
     
        private void insert_Click(object sender, EventArgs e)
        {
            enable();
            auto_inc();
           
        }

        private void purchase_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string s1 = "select pid from product";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            OleDbDataReader d = cmd.ExecuteReader();
            while (d.Read())
            {
                pid.Items.Add(d[0].ToString());
            }
            id_cmb.Visible = false;
            nm_cmb.Visible = false;
            string s2 = "select cid from company";
            OleDbCommand cmdd = new OleDbCommand(s2, con);
            OleDbDataReader d1 = cmdd.ExecuteReader();
            while (d1.Read())
            {
                cid.Items.Add(d1[0].ToString());
            }
            id_cmb.Visible = false;
            nm_cmb.Visible = false;
            string di = "select * from purchase";
            OleDbCommand cmd6 = new OleDbCommand(di, con);
            var reader = cmd6.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void display()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from purchase";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void t_clear()
        {
            prid.Text = "";
            billno.Text = "";
            pid.Text = "";
            pnm.Text = "";
            cid.Text = "";
            cnm.Text = "";
            prdt.Text = "";
            pprice.Text = "";
            pqty.Text = "";
            amt.Text = "";
            gst.Text = "";   
            tamt.Text = "";
        }
        public void auto_inc()
        {
            OleDbConnection con = new OleDbConnection(s);
            adp = new OleDbDataAdapter("select max(prid) from purchase", con);
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
            prid.Text = cno.ToString();
        }      

        public void enable()
        {
            prid.Enabled = true;
            billno.Enabled = true;
            pid.Enabled = true;
            pnm.Enabled = true;
            cid.Enabled = true;
            cnm.Enabled = true;
            prdt.Enabled = true;
            pprice.Enabled = true;
            pqty.Enabled = true;
            amt.Enabled = true;
            gst.Enabled = true;
            tamt.Enabled = true;
        }
        public void enbfla()
        {
            prid.Enabled = false;
            billno.Enabled = false;
            pid.Enabled = false;
            pnm.Enabled = false;
            cid.Enabled = false;
            cnm.Enabled = false;
            prdt.Enabled = false;
            pprice.Enabled = false;
            pqty.Enabled = false;
            amt.Enabled = false;
            gst.Enabled = false;
            tamt.Enabled = false;
        }

        private void pid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string s1 = "select pnm from product where pid=" + pid.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);
            pnm.Text = dss.Tables[0].Rows[0][0].ToString();
        }

        private void cid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string s1 = "select cnm from company where cid=" + cid.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);
            cnm.Text = dss.Tables[0].Rows[0][0].ToString();
        }

        public void amtt()
        {
            double a = Convert.ToInt32(pprice.Text);
            double b = Convert.ToInt32(pqty.Text);
            double amo = a * b;
            amt.Text = Convert.ToString(amo);
        }
        public void totgst()
        {
            double a = Convert.ToInt32(amt.Text);
            double gstt = (a * 5) / 100;
            gst.Text = Convert.ToString(gstt);
        }
        public void totamt()
        {
            double a = Convert.ToInt32(amt.Text);
            double b = Convert.ToInt32(gst.Text);          
            double tot = a + b;
            tamt.Text = Convert.ToString(tot);
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (prid.Text == "")
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
                if (string.IsNullOrEmpty(billno.Text.Trim()))
                {
                    errorProvider2.SetError(billno, "Bill no is required.");
                    return;
                }
                else
                {
                    errorProvider2.SetError(billno, string.Empty);
                }
                if (string.IsNullOrEmpty(pid.Text.Trim()))
                {
                    errorProvider3.SetError(pid, "product id is required.");
                    return;
                }
                else
                {
                    errorProvider3.SetError(pid, string.Empty);
                }
                if (string.IsNullOrEmpty(pnm.Text.Trim()))
                {
                    errorProvider4.SetError(pnm, "product name is required.");
                    return;
                }
                else
                {
                    errorProvider4.SetError(pnm, string.Empty);
                }
                if (string.IsNullOrEmpty(cid.Text.Trim()))
                {
                    errorProvider5.SetError(cid, " company id is required.");
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
                if (string.IsNullOrEmpty(prdt.Text.Trim()))
                {
                    errorProvider7.SetError(prdt, "purchase date is required.");
                    return;
                }
                else
                {
                    errorProvider7.SetError(prdt, string.Empty);
                }
                if (string.IsNullOrEmpty(pprice.Text.Trim()))
                {
                    errorProvider8.SetError(pprice, "price is required.");
                    return;
                }
                else
                {
                    errorProvider8.SetError(pprice, string.Empty);
                }
                if (string.IsNullOrEmpty(pqty.Text.Trim()))
                {
                    errorProvider9.SetError(pqty, "quantity is required.");
                    return;
                }
                else
                {
                    errorProvider9.SetError(pqty, string.Empty);
                }
                if (string.IsNullOrEmpty(amt.Text.Trim()))
                {
                    errorProvider10.SetError(amt, "amount is required.");
                    return;
                }
                else
                {
                    errorProvider10.SetError(amt, string.Empty);
                }
                if (string.IsNullOrEmpty(gst.Text.Trim()))
                {
                    errorProvider11.SetError(gst, "gst is required.");
                    return;
                }
                else
                {
                    errorProvider11.SetError(gst, string.Empty);
                }
                if (string.IsNullOrEmpty(tamt.Text.Trim()))
                {
                    errorProvider12.SetError(tamt, "total amount is required.");
                    return;
                }
                else
                {
                    errorProvider12.SetError(tamt, string.Empty);
                }
               
                if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {

                    OleDbConnection con = new OleDbConnection(s);
                    con.Open();
                    OleDbCommand cmd;
                    string q = "insert into purchase values('" + prid.Text + "','" + billno.Text + "','" + pid.Text + "','" + pnm.Text + "','" + cid.Text + "','" + cnm.Text + "','" + prdt.Value.Date.ToShortDateString() + "','" + pprice.Text + "','" + pqty.Text + "','" + amt.Text + "','" + gst.Text + "','"+ tamt.Text + "')";          
                    OleDbCommand cmd1 = new OleDbCommand(q, con);
                    cmd1.ExecuteNonQuery();
                    string s3 = "select pid from stock";
                    int temp = 0;
                    OleDbCommand cmdd = new OleDbCommand(s3, con);
                    OleDbDataReader d = cmdd.ExecuteReader();

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
                        if (pid.Text != null)
                        {

                            cmd = new OleDbCommand("select pqty from stock where pid=" + pid.Text + "", con);
                            OleDbDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    q1 = Convert.ToInt32(dr[0]);
                                }
                            }
                        }                      
                        cmd = new OleDbCommand("update stock set pqty=" + q2 + " where pid=" + pid.Text + "", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("stock is updated..");
                       
                    }
                    else
                    {

                        OleDbCommand cmd3 = new OleDbCommand("insert into stock values('" + pid.Text + "','" + pnm.Text + "','" + pqty.Text + "')", con);
                        cmd3.Connection = con;
                        cmd3.ExecuteNonQuery();

                    }
                    con.Close();
                }
                display();
                enbfla();
                t_clear();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (prid.Text == "")
            {
                if (MessageBox.Show("Please enter id for update record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    enable();
                }
            }
            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select prid from purchase";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (prid.Text == d.GetInt32(0).ToString())
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
                    String update = "update purchase set billno='" + billno.Text + "',pid='" + pid.Text + "',pnm='" + pnm.Text.ToString() + "',cid='" + cid.Text + "',cnm='" + cnm.Text.ToString() + "',prdt='" + prdt.Value.Date.ToShortDateString() + "',pprice='" + pprice.Text + "' ,pqty='" + pqty.Text + "',amt='" + amt.Text + "',gst='" + gst.Text + "' ,total='" + tamt.Text + "' where prid=" + prid.Text + "";
                    cmdd = new OleDbCommand(update, con);
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    String up = "update stock set pnm='" + pnm.Text + "',pqty='" + pqty.Text + "' where pid=" + pid.Text + "";
                    cmdd = new OleDbCommand(up, con);
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("update successfully");
                    display();
                    t_clear();
                }
                else
                {
                    MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void pqty_Leave(object sender, EventArgs e)
        {
            amtt();
        }

        private void amt_Leave(object sender, EventArgs e)
        {
            totgst();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (prid.Text == "")
            {

                if (MessageBox.Show("Please enter id for delete record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    enbfla();
                    pid.Enabled = true;
                }
            }
            else
            {

                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select prid from purchase";
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
                    string q = "delete from purchase where prid=" + prid.Text.ToString() + "";
                    OleDbCommand cmdd = new OleDbCommand(q, con);
                    cmdd.ExecuteNonQuery();
                    string q1 = "delete from stock where pid=" + pid.Text.ToString() + "";
                    OleDbCommand cmd2 = new OleDbCommand(q1, con);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Has Been Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    enbfla();
                    t_clear();
                    display();
                }
                else
                {
                    MessageBox.Show("this is new id so can't delete....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                prid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                billno.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                pid.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                pnm.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cid.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                cnm.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                prdt.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                pprice.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                pqty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                amt.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                gst.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                tamt.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            prid.Text = "";
            billno.Text = "";
            pid.Text = "";
            pnm.Text = "";
            cid.Text = "";
            cnm.Text = "";
            prdt.Text = "";
            pprice.Text = "";
            pqty.Text = "";
            amt.Text = "";
            gst.Text = "";
            tamt.Text = "";
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }

        private void tamt_Leave(object sender, EventArgs e)
        {
            //totamt();
        }

        private void tamt_Click(object sender, EventArgs e)
        {
            totamt();
        }
        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            prid.Text = row.Cells["prid"].Value.ToString();
            billno.Text = row.Cells["billno"].Value.ToString();
            pid.Text = row.Cells["pid"].Value.ToString();
            pnm.Text = row.Cells["pnm"].Value.ToString();
            cid.Text = row.Cells["cid"].Value.ToString();
            cnm.Text = row.Cells["cnm"].Value.ToString();
            prdt.Text = row.Cells["prdt"].Value.ToString();         
            pprice.Text = row.Cells["pprice"].Value.ToString();
            pqty.Text = row.Cells["pqty"].Value.ToString();
            amt.Text = row.Cells["amt"].Value.ToString();
            gst.Text = row.Cells["gst"].Value.ToString();
            //tamt.Text = row.Cells["tamt"].Value.ToString();
            
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = true;
                nm_cmb.Visible = false;
                con.Open();
                string s1 = "select prid from purchase";
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
                string s1 = "select pnm from purchase";
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

        private void id_cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string dis = "select * from purchase where prid=" + id_cmb.Text + "";
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
            string s1 = "select * from purchase where pnm='" + nm_cmb.Text + "'";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            var d = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void pid_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(pid.Text, @"^\d{3}$").Success)
            {

                MessageBox.Show("Enter Only 3 Digit Or valid id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pid.Focus();
            }
        }

        private void pnm_Leave(object sender, EventArgs e)
        {
            if (!Regex.Match(pnm.Text, "^[a-zA-Z]*$").Success)
            {

                MessageBox.Show("Invalid product Name ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pnm.Focus();

            }
        }
        public void abc()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string ss = "select prid from purchase";
            int temp = 0;
            OleDbCommand cmd1 = new OleDbCommand(ss, con);
            OleDbDataReader d1 = cmd1.ExecuteReader();

            while (d1.Read())
            {
                if (prid.Text == d1.GetInt32(0).ToString())
                {
                    temp = 1;
                    break;
                }
            }
            if (temp == 1)
            {
                string s1 = "select * from purchase where prid=" + prid.Text;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    //rowid.Text = d[1].ToString();
                    billno.Text = d[1].ToString();
                    pid.Text = d[2].ToString();
                    pnm.Text = d[3].ToString();
                    cid.Text = d[4].ToString();
                    cnm.Text = d[5].ToString();
                    prdt.Text = d[6].ToString();
                    pprice.Text = d[7].ToString();
                    pqty.Text = d[8].ToString();
                    amt.Text = d[9].ToString();
                    gst.Text = d[10].ToString();
                    tamt.Text = d[11].ToString();

                }
                con.Close();
            }
        }
        private void prid_TextChanged(object sender, EventArgs e)
        {
            abc();
        }
    }
}
