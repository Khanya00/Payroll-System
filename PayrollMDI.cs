using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApplication
{
    public partial class PayrollMDI : Form
    {
        public PayrollMDI()
        {
            InitializeComponent();
        }

        private void manageEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeForm objEmployeeForm = new EmployeeForm();
            objEmployeeForm.MdiParent = this;
            objEmployeeForm.Show();
        }

        private void payrollCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PayrollCalculator objPayrollCalculator = new PayrollCalculator();
            objPayrollCalculator.MdiParent = this;
            objPayrollCalculator.Show();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren) //Each form is called the childForm, the collection of these forms is this.MdiChildren
            {
                childForm.Close(); //closes each child form
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        EmployeeForm objEmployeeForm = null;
        private void manageEmployeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ( objEmployeeForm == null)
            {
                objEmployeeForm = new EmployeeForm();
                objEmployeeForm.MdiParent = this;
                objEmployeeForm.FormClosed += ObjEmployeeForm_FormClosed;
                objEmployeeForm.Show();
            }
            else
            {
                objEmployeeForm.Activate();
            }
        }

        private void ObjEmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            objEmployeeForm = null;
        }

        PayrollCalculator objPayrollCalculator = null;
        private void payrollCalculatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (objPayrollCalculator == null) //if an instance of this form is not running...
            {
                objPayrollCalculator = new PayrollCalculator();
                objPayrollCalculator.MdiParent = this;
                objPayrollCalculator.FormClosed += ObjPayrollCalculator_FormClosed;
                objPayrollCalculator.Show();
            }
            else //if there is an instance of this form running go ahead and activate that particular form
            {
                objPayrollCalculator.Activate();
            }
            
        }

        private void ObjPayrollCalculator_FormClosed(object sender, FormClosedEventArgs e)
        {
            objPayrollCalculator = null;
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void arrangeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void aboutPayrollApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPayrollSystem objAbout = new AboutPayrollSystem();
            objAbout.MdiParent = this;
            objAbout.Show();
        }

        private void registerUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterUser objRegisterUser = new RegisterUser();
            objRegisterUser.MdiParent = this;
            objRegisterUser.Show();
        }

        private void PayrollMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult objDialog = MessageBox.Show("Are you sure you want to exit this application", "Form Closing....", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (objDialog == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                Login objLogIn = new Login();
                objLogIn.Visible = true;
            }
        }
    }
}
