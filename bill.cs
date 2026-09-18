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
    public partial class bill : Form
    {
        string s = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb";
        OleDbDataAdapter adt = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public bill()
        {
            InitializeComponent();
        }
               
        private void bill_Load(object sender, EventArgs e)
        {
           OleDbConnection conn = new OleDbConnection(s);
            conn.Open();
            string s1 = "select billno from sales";
            OleDbCommand cmd = new OleDbCommand(s1, conn);
            OleDbDataReader d = cmd.ExecuteReader();
            while (d.Read())
            {
                billno.Items.Add(d[0].ToString());
            }

            conn.Close(); 
        }
        private void bid_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(s);
            conn.Open();
            string s1 = "select * from sales where billno= " + billno.Text + " ";
            OleDbCommand cmd = new OleDbCommand(s1, conn);
            OleDbDataReader dd;
            dd = cmd.ExecuteReader();
            while (dd.Read())
            {
               
                id.Text = dd.GetValue(2).ToString();
                pid.Text =dd.GetValue(4).ToString();
                sdate.Text = dd.GetValue(6).ToString();
                pprice.Text = dd.GetValue(7).ToString();
                pqty.Text = dd.GetValue(8).ToString();
                amt.Text = dd.GetValue(9).ToString();
                gst.Text = dd.GetValue(10).ToString();
                tamt.Text = dd.GetValue(11).ToString();

            }
            conn.Close();
            
        }

        private void Show_Click(object sender, EventArgs e)
        {
            string st = Application.StartupPath + "\\report\\bill_report.rpt";
            axCrystalReport1.ReportFileName = st;



            string str = Application.StartupPath + "\\report\\bill_report.rpt";
            axCrystalReport1.ReportFileName = str;

            axCrystalReport1.SelectionFormula = "{ sales.billno}=" + billno.Text + "";


            axCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            axCrystalReport1.WindowShowRefreshBtn = true;
            axCrystalReport1.Action = 1;
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("do you want to exit this form?");
            this.Hide();
        }
       
    }
}
