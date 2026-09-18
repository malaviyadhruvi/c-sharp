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
    public partial class sales_return : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        int r = 0;
        DataTable dt = new DataTable();
        int q1 = 0, q2;

        public sales_return()
        {
            InitializeComponent();
        }

        public void display()
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();
            string di = "select * from sales_return";
            OleDbCommand cmd = new OleDbCommand(di, con);
            var reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void enable()
        {
            srid.Enabled = true;
            billno.Enabled = true;
            sid.Enabled = true;
            pid.Enabled = true;
            pnm.Enabled = true;
            id.Enabled = true;
            nm.Enabled = true;
            pprice.Enabled = true;
            sqty.Enabled = true;
            amt.Enabled = true;
            srqty.Enabled = true;
            sramt.Enabled = true;
            sdate.Enabled = true;
            srdate.Enabled = true;
        }
        public void enbfla()
        {
            srid.Enabled = false;
            billno.Enabled = false;
            sid.Enabled = false;
            pid.Enabled = false;
            pnm.Enabled = false;
            id.Enabled = false;
            nm.Enabled = false;
            pprice.Enabled = false;
            sqty.Enabled = false;
            amt.Enabled = false;
            srqty.Enabled = false;
            sramt.Enabled = false;
            sdate.Enabled = false;
            srdate.Enabled = false;
        }

        private void insert_Click(object sender, EventArgs e)
        {
            enable();
            auto_inc();
        }

        private void sales_return_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            con.Open();

            string s3 = "select pid from stock";
            OleDbCommand cmd5 = new OleDbCommand(s3, con);
            OleDbDataReader d1 = cmd5.ExecuteReader();
            while (d1.Read())
            {
                pid.Items.Add(d1[0].ToString());
            }

            string s2 = "select id from customer";
            OleDbCommand cmd2 = new OleDbCommand(s2, con);
            OleDbDataReader dd = cmd2.ExecuteReader();
            while (dd.Read())
            {
                id.Items.Add(dd[0].ToString());
            }

            string s1 = "select sid from sales";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            OleDbDataReader d = cmd.ExecuteReader();
            while (d.Read())
            {
                sid.Items.Add(d[0].ToString());
            }
            con.Close();
            display();
        }
        public void auto_inc()
        {
            OleDbConnection con = new OleDbConnection(s);
            adp = new OleDbDataAdapter("select max(srid) from sales_return", con);
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
            srid.Text = cno.ToString();
        }

        private void sid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string s1 = "select sdate from sales where sid=" + sid.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);
            sdate.Text = dss.Tables[0].Rows[0][0].ToString();
        }

        private void pid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string s1 = "select pnm, pprice,pqty from product where pid=" + sid.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);
            pnm.Text = dss.Tables[0].Rows[0][0].ToString();
            pprice.Text = dss.Tables[0].Rows[0][1].ToString();
            sqty.Text = dss.Tables[0].Rows[0][2].ToString();
           // pqty.Text = dss.Tables[0].Rows[0][3].ToString();
     
        }

        private void id_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string s1 = "select nm from customer where id=" + id.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);
            nm.Text = dss.Tables[0].Rows[0][0].ToString();
        }
       
        public void ammm()
        {
            int a = Convert.ToInt32(pprice.Text);
            int b = Convert.ToInt32(sqty.Text);
            int amo = a * b;
            amt.Text = Convert.ToString(amo);
        }
        public void total()
        {
            int a = Convert.ToInt32(sqty.Text);
            int b = Convert.ToInt32(srqty.Text);
            int c = Convert.ToInt32(pprice.Text);
            int amo = a - b;
            double amt = amo * c;
            sramt.Text = Convert.ToString(amt);
        }

        private void show_Click(object sender, EventArgs e)
        {
             if (srid.Text == "")
            {
                MessageBox.Show("please Enter add button for id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                if (string.IsNullOrEmpty(srid.Text.Trim()))
                {
                    errorProvider1.SetError(srid, "sales return id is required.");
                    return;
                }
                else
                {
                    errorProvider1.SetError(srid, string.Empty);
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
                if (string.IsNullOrEmpty(sid.Text.Trim()))
                {
                    errorProvider3.SetError(sid, "sales id is required.");
                    return;
                }
                else
                {
                    errorProvider3.SetError(sid, string.Empty);
                }
                if (string.IsNullOrEmpty(pid.Text.Trim()))
                {
                    errorProvider4.SetError(pid, "product id is required.");
                    return;
                }
                else
                {
                    errorProvider4.SetError(pid, string.Empty);
                }
                if (string.IsNullOrEmpty(pnm.Text.Trim()))
                {
                    errorProvider5.SetError(pnm, "product name is required.");
                    return;
                }
                else
                {
                    errorProvider5.SetError(pnm, string.Empty);
                }
                if (string.IsNullOrEmpty(id.Text.Trim()))
                {
                    errorProvider6.SetError(id, "customer id is required.");
                    return;
                }
                else
                {
                    errorProvider6.SetError(id, string.Empty);
                }
                if (string.IsNullOrEmpty(nm.Text.Trim()))
                {
                    errorProvider7.SetError(nm, "customer name is required.");
                    return;
                }
                else
                {
                    errorProvider7.SetError(nm, string.Empty);
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
                if (string.IsNullOrEmpty(sqty.Text.Trim()))
                {
                    errorProvider9.SetError(sqty, "sales qty  is required.");
                    return;
                }
                else
                {
                    errorProvider9.SetError(sqty, string.Empty);
                }
                if (string.IsNullOrEmpty(amt.Text.Trim()))
                {
                    errorProvider10.SetError(amt, " amount is required.");
                    return;
                }
                else
                {
                    errorProvider10.SetError(amt, string.Empty);
                }
                if (string.IsNullOrEmpty(srqty.Text.Trim()))
                {
                    errorProvider11.SetError(srqty, " sales return quantity is required.");
                    return;
                }
                else
                {
                    errorProvider11.SetError(srqty, string.Empty);
                }
                if (string.IsNullOrEmpty(sramt.Text.Trim()))
                {
                    errorProvider12.SetError(sramt, " sales return amount is required.");
                    return;
                }
                else
                {
                    errorProvider12.SetError(sramt, string.Empty);
                }
                if (string.IsNullOrEmpty(sramt.Text.Trim()))
                {
                    errorProvider13.SetError(sdate, "sales date is required.");
                    return;
                }
                else
                {
                    errorProvider13.SetError(sdate, string.Empty);
                }
                if (string.IsNullOrEmpty(srdate.Text.Trim()))
                {
                    errorProvider14.SetError(srdate, " sales return date is required.");
                    return;
                }
                else
                {
                    errorProvider14.SetError(srdate, string.Empty);
                }
                
                OleDbCommand cmd6 = new OleDbCommand();

                if (MessageBox.Show("Do You Really Want To Add This Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {

                    OleDbConnection con = new OleDbConnection(s);
                    con.Open();
                    OleDbCommand cmd;
                    string q = "insert into sales_return values('" + srid.Text + "','" + billno.Text + "','" + sid.Text + "','" + pid.Text + "','" + pnm.Text + "','" + id.Text + "','" + nm.Text + "','" + pprice.Text + "','" + sqty.Text + "','" + amt.Text + "','" + srqty.Text + "','" + sramt.Text + "','" + sdate.Value.Date.ToShortDateString() + "','" + srdate.Value.Date.ToShortDateString() + "')";

                    OleDbCommand cmd1 = new OleDbCommand(q, con);
                    cmd1.ExecuteNonQuery();

                    string s1 = "select pid from stock";
                    int temp = 0;
                    OleDbCommand cmdd = new OleDbCommand(s1, con);
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
                        //  con.Close();
                        MessageBox.Show("insert successfully");

                    }
                    /* else
                     {
                       
                         OleDbCommand cmd3 = new OleDbCommand("insert into stock values('" + rowid.Text + "','" + pinm.Text + "','" + qty.Text + "')", con);
                         cmd3.Connection = con;
                         cmd3.ExecuteNonQuery();
                        
                         MessageBox.Show("insert");
                     }*/
                    con.Close();
                }
                display();
                enbfla();
                //t_clear();

          }
        }
        public void t_clear()
        {
            srid.Text = "";
            billno.Text = "";
            billno.Text = "";
            sid.Text = "";
            pid.Text = "";
            pnm.Text = "";
            id.Text = "";
            nm.Text = "";
            pprice.Text = "";
            sqty.Text = "";
            amt.Text = "";
            srqty.Text = "";
            sramt.Text = "";
            sdate.Text = "";
            srdate.Text = "";
        }
        private void edit_Click(object sender, EventArgs e)
        {
            if (srid.Text == "")
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
                string s1 = "select srid from sales_return";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    if (srid.Text == d.GetInt32(0).ToString())
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
                    String update = "update sales_return set  billno='" + billno.Text + "',sid='" + sid.Text + "',pid='" + pid.Text + "',pnm='" + pnm.Text + "',id='" + id.Text + "',nm='" + nm.Text + "' ,pprice='" + pprice.Text + "',sqty='" + sqty.Text + "',amt='" + amt.Text + "',srqty='" + srqty.Text + "',sramt='" + sramt.Text + "',sdate='" + sdate.Value.Date.ToShortDateString() + "',srdate='" + srdate.Value.Date.ToShortDateString() + "' where srid=" + srid.Text + "";
                    cmdd = new OleDbCommand(update, con);
                    cmdd.Connection = con;
                    cmdd.ExecuteNonQuery();
                    /*    String up = "update stock set product_name='" + pinm.Text + "',quantity='" + prqty.Text + "' where rid=" + comboBox1.Text + "";
                        cmdd = new OleDbCommand(up, con);
                        cmdd.Connection = con;
                        cmdd.ExecuteNonQuery();*/
                    con.Close();
                    MessageBox.Show("update successfully");
                    display();
                    t_clear();
                }
                else
                {
                    MessageBox.Show("this is new id so can't updated.....plese save record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                enbfla();
                t_clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                srid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                billno.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                sid.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                pid.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                pnm.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                id.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                nm.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                pprice.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                sqty.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                amt.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                srqty.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                sramt.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                sdate.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                srdate.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                // tamt.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (srid.Text == "")
            {

                if (MessageBox.Show("Please enter id for delete record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    enbfla();
                    srid.Enabled = true;
                }
            }

            else
            {
                OleDbConnection con = new OleDbConnection(s);
                con.Open();
                string s1 = "select srid from sales_return";
                int temp = 0;
                OleDbCommand cmd = new OleDbCommand(s1, con);
                OleDbDataReader d = cmd.ExecuteReader();
                while (d.Read())
                {
                    if (srid.Text == d.GetInt32(0).ToString())
                    {
                        temp = 1;
                        break;
                    }
                }
                if (temp == 1)
                {
                    string q = "delete from sales_return where srid=" + srid.Text.ToString() + "";
                    OleDbCommand cmdd = new OleDbCommand(q, con);
                    cmdd.ExecuteNonQuery();
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

        private void clear_Click(object sender, EventArgs e)
        {
            t_clear();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                OleDbConnection con = new OleDbConnection(s);
                id_cmb.Visible = true;
                nm_cmb.Visible = false;
                con.Open();
                string s1 = "select srid from sales_return";
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
                string s1 = "select pnm from sales_return";
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
            string dis = "select * from sales_return where srid=" + id_cmb.Text + "";
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
            string s1 = "select * from sales_return where pnm='" + nm_cmb.Text + "'";
            OleDbCommand cmd = new OleDbCommand(s1, con);
            var d = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(d);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void display(int c)
        {
            DataGridViewRow row = this.dataGridView1.Rows[c];

            srid.Text = row.Cells["srid"].Value.ToString();
            billno.Text = row.Cells["billno"].Value.ToString();
            sid.Text = row.Cells["sid"].Value.ToString();
            pid.Text = row.Cells["pid"].Value.ToString();
            pnm.Text = row.Cells["pnm"].Value.ToString();
            id.Text = row.Cells["id"].Value.ToString();
            nm.Text = row.Cells["nm"].Value.ToString();
            pprice.Text = row.Cells["pprice"].Value.ToString();
            sqty.Text = row.Cells["sqty"].Value.ToString();
            amt.Text = row.Cells["amt"].Value.ToString();
            srqty.Text = row.Cells["srqty"].Value.ToString();
            sramt.Text = row.Cells["sramt"].Value.ToString();
            sdate.Text = row.Cells["sdate"].Value.ToString();
            srdate.Text = row.Cells["srdate"].Value.ToString();

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

       

        private void pqty_TextChanged(object sender, EventArgs e)
        {
           /* DataSet dss = new DataSet();
            string s1 = "select pqty from sales where sid=" + sid.SelectedItem.ToString();
            adp = new OleDbDataAdapter(s1, s);
            adp.Fill(dss);

            pqty.Text = dss.Tables[0].Rows[0][0].ToString();*/
        }

        private void amt_Click(object sender, EventArgs e)
        {
            ammm();
        }

        private void sramt_Click(object sender, EventArgs e)
        {
            total();
        }
       
    }
}
