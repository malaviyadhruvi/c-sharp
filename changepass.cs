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
    public partial class changepass : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\@dg\df\Database1.accdb");
        OleDbDataAdapter da;
        DataTable dt;
        public changepass()
        {
            InitializeComponent();
        }

        private void change_Click(object sender, EventArgs e)
        {
            da = new OleDbDataAdapter("select * from Login where password='" + op.Text + "'", con);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                panel8.Visible = true;
                panel10.Visible = true;


            }
            else
            {
                MessageBox.Show("Please! Enter Correct Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                op.Text = "";
                op.Focus();
            }
            if (np.Text == "")
            {
                MessageBox.Show("Please! Enter New Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                np.Focus();
            }
            else if (cp.Text == "")
            {
                MessageBox.Show("Please! Retype Your Password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cp.Focus();
            }
            else
            {
                if (np.Text.Equals(cp.Text))
                {
                    con.Open();
                    da.UpdateCommand = new OleDbCommand("update login set [password] ='" + cp.Text + "' where [password]='" + op.Text + "'", con);
                    da.UpdateCommand.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Congratulation..! Your Password Has Been Successfully Changed...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
                    panel8.Visible = false;
                    panel10.Visible = false;
                    op.Focus();                   
                    login l = new login();
                    l.Show();
                    //this.Close();
                    

                }
                else
                {
                    MessageBox.Show("Please! Type Same Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cp.Text = "";
                    cp.Focus();
                }
            }
        }
        private void reset_Click(object sender, EventArgs e)
        {
            op.Text = "";
            np.Text = "";
            cp.Text = "";
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want To Exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {                
                mdi m = new mdi();
                m.Show();                
            }           
        }
    }
}
