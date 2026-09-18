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
    public partial class customer_report : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adp = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        public customer_report()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            OleDbCommand cmd = new OleDbCommand();
            if (radioButton1.Checked == true)
            {

                id_cmb.Items.Clear();
                id_cmb.Visible = true;
                cmd = new OleDbCommand("select id from customer", con);
                adp = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                adp.Fill(ds);
                con.Close();
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    id_cmb.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
                id_cmb.Visible = false;
                id_cmb.Text = "";
            }
        }

        private void show_Click(object sender, EventArgs e)
        {
            string st = Application.StartupPath + "\\report\\customer_report.rpt";
            axCrystalReport1.ReportFileName = st;

            if (radioButton1.Checked == true)
            {
                string str = Application.StartupPath + "\\report\\customer_report.rpt";
                axCrystalReport1.ReportFileName = str;

                axCrystalReport1.SelectionFormula = "{ customer.id}=" + id_cmb.Text + "";
            }
            else if (radioButton2.Checked == true)
            {
                string str = Application.StartupPath + "\\report\\customer_report.rpt";
                axCrystalReport1.ReportFileName = str;
                axCrystalReport1.SelectionFormula = "{ customer.nm}='" + nm_cmb.Text + "' ";
            }
            else
            {
                axCrystalReport1.SelectionFormula = "{ customer.id}>0";
            }

            axCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            axCrystalReport1.WindowShowRefreshBtn = true;
            axCrystalReport1.Action = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(s);
            OleDbCommand cmd = new OleDbCommand();
            if (radioButton2.Checked == true)
            {

                nm_cmb.Items.Clear();
                nm_cmb.Visible = true;
                cmd = new OleDbCommand("select nm from customer", con);
                adp = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                adp.Fill(ds);
                con.Close();
                int a = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
                for (int i = 0; i < a; i++)
                {
                    nm_cmb.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            else
            {
                nm_cmb.Visible = false;
                nm_cmb.Text = "";
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }
    }
}
