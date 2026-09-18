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
    public partial class mdi : Form
    {
        public mdi()
        {
            InitializeComponent();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            company cm = new company();
            cm.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            purchase p = new purchase();
            p.Show();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales s = new sales();
            s.Show();
        }

        private void parchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales_return sr = new sales_return();
            sr.Show();
        }

        private void notpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc");
        }

        private void paintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mspaint.exe");
        }

        private void wordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Wordpad.exe");
        }

        private void mojilaFireFoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("firefox");
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stock st = new stock();
            st.Show();
        }

        private void companyMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            company_report cr = new company_report();
            cr.Show();
        }

        private void itemMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            product_report ir = new product_report();
            ir.Show();
        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            purchase_report pr = new purchase_report();
            pr.Show();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales_report sr = new sales_report();
            sr.Show();
        }

        private void salesReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sales_return_report srr = new sales_return_report();
            srr.Show();
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stock_report str = new stock_report();
            str.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Change Your Password?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                changepass cp = new changepass();
                cp.Show();
            }
        }

        private void customerMasterReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer_report cr = new customer_report();
            cr.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want To Exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Close();               
            }
        }

        private void aboutUsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            aboutus a = new aboutus();
            a.Show();
        }

        private void customerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.Show();
        }

        private void ptoductMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            product pro = new product();
            pro.Show();
        }


        private void billMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bill b = new bill();
            b.Show();
        }
    }
}
