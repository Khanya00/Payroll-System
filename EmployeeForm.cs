using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;

namespace PayrollApplication
{
    public partial class EmployeeForm : Form
    {
        string gender;
        string maritalStatus;
        bool isMember;

        public EmployeeForm()
        {
            InitializeComponent();
        }

        //Checked Items Controls Method
        private void CheckedItems()
        {
            //Gender
            if (rbFemale.Checked)
            {
                gender = "Female";
            }
            else
            {
                gender = "Male";
            }
            //MaritalStatus
            if (rbSingle.Checked)
            {
                maritalStatus = "Single";
            }
            else
            {
                maritalStatus = "Married";
            }
            //UnionMembership
            if (cbUnionMember.Checked)
            {
                isMember = true;
            }
            else
            {
                isMember = false;
            }
        }

        //Clear Controls Method
        private void ClearControls()
        {
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txtNINumber.Text = "";
            dtpDateOfBirth.Value = new DateTime(1990, 12, 30);
            rbSingle.Checked = false;
            rbMarried.Checked = false;
            cbUnionMember.Checked = false;
            txtAddress.Text = string.Empty;
            txtCity.Text = null;
            txtPostalCode.Text = "";
            cmbCountry.SelectedIndex = 0;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtNotes.Text = "";
        }

        #region Buttons

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isControlsDataValid())
            {
                CheckedItems();
                //This is our connection string = connStr
                string connStr = ConfigurationManager.ConnectionStrings["Payroll_System_DBConnectionString"].ConnectionString;

                //Open the connection
                //Instantiate the sqlConnection

                SqlConnection objSqlConnection = new SqlConnection(connStr);
                try
                {
                    //Open the connection
                    objSqlConnection.Open();
                    //Prepare the Insert Command text

                    string InsertCommand = "INSERT INTO tblEmployee VALUES (" + Convert.ToInt32(txtEmployeeID.Text) + ", '" + txtFirstName.Text + "', '" + txtLastName.Text + "', '" + gender + "', " +
                        "'" + txtNINumber.Text + "', '" + dtpDateOfBirth.Value.ToString("yyyy/MM/dd") + "', '" + maritalStatus + "', '" + isMember + "', '" + txtAddress.Text + "', '" + txtCity.Text + "', " +
                        "'"+ txtPostalCode.Text +"', '"+ cmbCountry.SelectedItem.ToString() +"', '"+ txtPhoneNumber.Text +"', '"+ txtEmail.Text +"', '"+ txtNotes.Text +"')";

                    //Instatiate the sql command and pass in CommandText and connection object
                    SqlCommand objSqlCommand = new SqlCommand(InsertCommand, objSqlConnection);

                    //Execute the query identified in our command object
                    objSqlCommand.ExecuteNonQuery();

                    // TODO: This line of code loads data into the 'payroll_System_DBDataSet.tblEmployee' table. You can move, or remove it, as needed.
                    this.tblEmployeeTableAdapter.Fill(this.payroll_System_DBDataSet.tblEmployee);

                    //Display success message
                    MessageBox.Show("Employee with ID: " + (txtEmployeeID.Text) + " " + " has been added successfully.", "Insertion Successful",
                                    MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    ClearControls();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("The following error occurred: " + ex.Message, "Data Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    //Close connection
                    objSqlConnection.Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isControlsDataValid())
            {
                CheckedItems();
                //This is our connection string = connStr
                string connStr = ConfigurationManager.ConnectionStrings["Payroll_System_DBConnectionString"].ConnectionString;

                //Open the connection
                //Instantiate the sqlConnection

                SqlConnection objSqlConnection = new SqlConnection(connStr);
                try
                {
                    //Open the connection
                    objSqlConnection.Open();
                    //Prepare the Update Command text
                    string UpdateCommand = " UPDATE tblEmployee SET firstName = '" + txtFirstName.Text + "', lastName = '" + txtLastName.Text + "', " +
                        "gender = '" + this.gender + "', NINumber = '" + txtNINumber.Text + "', DateOfBirth = '" + dtpDateOfBirth.Value.ToString("yyyy/MM/dd") + "', " +
                        "maritalStatus = '" + this.maritalStatus + "', isMember = '" + this.isMember + "', address = '" + txtAddress.Text + "', city = '" + txtCity.Text + "', " +
                        "postCode = '" + txtPostalCode.Text + "', country = '" + cmbCountry.SelectedIndex.ToString() + "', phoneNumber = '" + txtPhoneNumber.Text + "', " +
                        "email = '" + txtEmail.Text + "', notes = '" + txtNotes.Text + "' WHERE employeeID = " + txtEmployeeID.Text + "";

                    //Instatiate the sql command and pass in CommandText and connection object
                    SqlCommand objSqlCommand = new SqlCommand(UpdateCommand, objSqlConnection);

                    //Execute the query identified in our command object
                    objSqlCommand.ExecuteNonQuery();

                    // TODO: This line of code loads data into the 'payroll_System_DBDataSet.tblEmployee' table. You can move, or remove it, as needed.
                    this.tblEmployeeTableAdapter.Fill(this.payroll_System_DBDataSet.tblEmployee);

                    //Display success message
                    MessageBox.Show("Employee with ID: " + (txtEmployeeID.Text) + " " + " has been updated successfully.", "Update Successful",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ClearControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The following error occurred: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //Close connection
                    objSqlConnection.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult objDialogResult = MessageBox.Show("Are you sure you want to permanently delete this Employee's record?", "Confirm Record Deletion", 
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (objDialogResult == DialogResult.Yes)
            {
                //This is our connection string = connStr
                string connStr = ConfigurationManager.ConnectionStrings["Payroll_System_DBConnectionString"].ConnectionString;

                //Open the connection
                //Instantiate the sqlConnection

                SqlConnection objSqlConnection = new SqlConnection(connStr);
                try
                {
                    //Open the connection
                    objSqlConnection.Open();
                    //Prepare the Update Command text
                    string DeleteCommand = " DELETE FROM tblEmployee WHERE employeeID = " + txtEmployeeID.Text + "";

                    //Instatiate the sql command and pass in CommandText and connection object
                    SqlCommand objSqlCommand = new SqlCommand(DeleteCommand, objSqlConnection);

                    //Execute the query identified in our command object
                    objSqlCommand.ExecuteNonQuery();

                    // TODO: This line of code loads data into the 'payroll_System_DBDataSet.tblEmployee' table. You can move, or remove it, as needed.
                    this.tblEmployeeTableAdapter.Fill(this.payroll_System_DBDataSet.tblEmployee);

                    //Display success message
                    MessageBox.Show("Employee with ID: " + (txtEmployeeID.Text) + " " + " has been deleted successfully.", "Update Successful",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ClearControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The following error occurred: " + ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //Close connection
                    objSqlConnection.Close();
                }
        }

        }
        
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            rbFemale.Checked = false;
            rbMale.Checked = false;
            txtNINumber.Text = "";
            dtpDateOfBirth.Value = new DateTime(1990, 12, 30);
            rbSingle.Checked = false;
            rbMarried.Checked = false;
            cbUnionMember.Checked = false;
            txtAddress.Text = string.Empty;
            txtCity.Text = null;
            txtPostalCode.Text = "";
            cmbCountry.SelectedIndex = 0;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtNotes.Text = "";

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            PreviewForm objPreviewForm = new PreviewForm();

            CheckedItems();

            objPreviewForm.PreviewEmployeeData(Convert.ToInt32(txtEmployeeID.Text), txtFirstName.Text, txtLastName.Text, 
                gender, txtNINumber.Text, dtpDateOfBirth.Text, maritalStatus, isMember, txtAddress.Text, txtCity.Text, 
                txtPostalCode.Text, cmbCountry.SelectedItem.ToString(), txtPhoneNumber.Text, txtEmail.Text, txtNotes.Text);

            objPreviewForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Implicit Validation: Keypress Events
        bool isNumberOrBackspace;
        private void txtEmployeeID_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                isNumberOrBackspace = true;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Digits Only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                isNumberOrBackspace = true;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Digits Only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Explict Validation for all Controls
        private bool isControlsDataValid()
        {
            Regex objEmployeeID = new Regex("^[0-9]{3,4}$");
            Regex objFirstName = new Regex("^[A-Z][a-zA-Z]*$");
            Regex objLastName = new Regex("^[A-Z][a-zA-Z]*$");
            //Must be 9 characters only
            //First two characters must be letters
            //Next 6 characters must be numeric digits
            //Final character can only be A,B,C,D or space (space = " \s")
            //First character must not be D,F,I,Q,U or V
            //Second character must not be D,F,I,O,Q,U or V
            // Example, NiNO Format = SB123456C

            Regex objNINumber = new Regex(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$");

            //Social Security Number Format 000-00-0000
            // /d means any decimal digit

            //Regex objSSN = new Regex(@"^\d{3}-\d{2}-\d{4}$");

            //email 
            //peter20@yahoo.com

            Regex objEmail = new Regex("[A-Za-z0-9]{1,30}@[A-Za-z0-9]{1,30}.[A-Za-z]{2,3}");

            //employee ID validation
            if (Convert.ToInt32(txtEmployeeID.Text.Length) < 1)
            {
                MessageBox.Show("Please enter a Employee ID", "Data Entry Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                txtEmployeeID.Focus();
                txtEmployeeID.BackColor = Color.Silver;
                return false;
            }
            else if (!objEmployeeID.IsMatch(txtEmployeeID.Text))
            {
                MessageBox.Show("Please enter a valid Employee ID", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtEmployeeID.Focus();
                txtEmployeeID.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtEmployeeID.BackColor = Color.White;
            }

            //firstName Validation
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtFirstName.Focus();
                txtFirstName.BackColor = Color.Silver;
                return false;
            }
            else if (!objFirstName.IsMatch(txtFirstName.Text))
            {
                MessageBox.Show("Please enter valid First Name", "Data Entry Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);

                txtFirstName.Focus();
                txtFirstName.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtFirstName.BackColor = Color.White;
            }

            //Last Name Validation
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
                txtLastName.Focus();
                txtLastName.BackColor = Color.Silver;
                return false;
            }
            else if (!objLastName.IsMatch(txtLastName.Text))
            {
                MessageBox.Show("Please enter valid Last Name", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtLastName.Focus();
                txtLastName.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtLastName.BackColor = Color.White;
            }

            //Gender Validation
            if(rbFemale.Checked == false && rbMale.Checked == false)
            {
                MessageBox.Show("Please check either Male or Female", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                grpGender.Focus();
                rbFemale.BackColor = Color.Silver;
                rbMale.BackColor = Color.Silver;
                return false;
            }
            else
            {
                rbFemale.BackColor = Color.CornflowerBlue;
                rbMale.BackColor = Color.CornflowerBlue;
            }

            //National Insurance Number
            if(txtNINumber.Text == "")
            {
                MessageBox.Show("Please enter National Insurance Number", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtNINumber.Focus();
                txtNINumber.BackColor = Color.Silver;
                return false;
            }
            else if (!objNINumber.IsMatch(txtNINumber.Text))
            {
                MessageBox.Show("Please enter a valid National Insurance Number", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtNINumber.Focus();
                txtNINumber.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtNINumber.BackColor = Color.White;
            }

            //Marital Status Validation
            if (rbSingle.Checked == false && rbMarried.Checked == false)
            {
                MessageBox.Show("Please check either Single or Married", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                grpMaritalStatus.Focus();
                rbSingle.BackColor = Color.Silver;
                rbMarried.BackColor = Color.Silver;
                return false;
            }
            else
            {
                rbSingle.BackColor = Color.CornflowerBlue;
                rbMarried.BackColor = Color.CornflowerBlue;
            }

            //Address Validation
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter your address details", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtAddress.Focus();
                txtAddress.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtAddress.BackColor = Color.White;
            }

            //City Validation
            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter City", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtCity.Focus();
                txtCity.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtCity.BackColor = Color.White;
            }

            //postalcode Validation
            if (txtPostalCode.Text == "")
            {
                MessageBox.Show("Please enter your postal code", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPostalCode.Focus();
                txtPostalCode.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtPostalCode.BackColor = Color.White;
            }

            //Country Validation
            if(cmbCountry.SelectedIndex == 0 || cmbCountry.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a country on the list", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                cmbCountry.Focus();
                cmbCountry.BackColor = Color.Silver;
                return false;
            }
            else
            {
                cmbCountry.BackColor = Color.White;
            }

            //Phone Number Validation
            if (txtPhoneNumber.Text.Length == 0)
            {
                MessageBox.Show("Please enter your phone number", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPhoneNumber.Focus();
                txtPhoneNumber.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtPhoneNumber.BackColor = Color.White;
            }

            //Email Validation
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter your email address", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtEmail.Focus();
                txtEmail.BackColor = Color.Silver;
                return false;
            }
            //else if (!objEmail.IsMatch(txtEmail.Text))
            //{
            //    MessageBox.Show("Please enter a valid email address", "Data Entry Error", MessageBoxButtons.OK,
            //        MessageBoxIcon.Error);

            //    txtEmail.Focus();
            //    txtEmail.BackColor = Color.Silver;
            //    return false;
            //}
            else if (txtEmail.Text.Length >= 1)
            {
                try
                {
                    MailAddress objMail = new MailAddress(txtEmail.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                    txtEmail.Focus();
                    txtEmail.BackColor = Color.Silver;
                    return false;
                }
            }
            else
            {
                txtEmail.BackColor = Color.White;
            }

            //Notes Validation
            if (txtNotes.Text.Length > 65)
            {
                MessageBox.Show("Too much text entered, Please enter fewer text!", "Data Entry Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtNotes.Focus();
                txtNotes.BackColor = Color.Silver;
                return false;
            }
            else
            {
                txtNotes.BackColor = Color.White;
            }

            return true;

        }
        #endregion

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'payroll_System_DBDataSet.tblEmployee' table. You can move, or remove it, as needed.
            this.tblEmployeeTableAdapter.Fill(this.payroll_System_DBDataSet.tblEmployee);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEmployeeID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            gender = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            txtNINumber.Text = dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            dtpDateOfBirth.Text = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            maritalStatus = dataGridView1.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            isMember = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[7].FormattedValue.ToString()); //boolean, convert to boolean
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
            txtCity.Text = dataGridView1.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
            txtPostalCode.Text = dataGridView1.Rows[e.RowIndex].Cells[10].FormattedValue.ToString();
            cmbCountry.Text = dataGridView1.Rows[e.RowIndex].Cells[11].FormattedValue.ToString();
            txtPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[12].FormattedValue.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[13].FormattedValue.ToString();
            txtNotes.Text = dataGridView1.Rows[e.RowIndex].Cells[14].FormattedValue.ToString();

            //Gender
            if (gender == "Male")
            {
                rbMale.Checked = true;
                rbFemale.Checked = false;
            }
            else
            {
                rbMale.Checked = false;
                rbFemale.Checked = true;
            }
            //Marital Status
            if (maritalStatus == "Single")
            {
                rbSingle.Checked = true;
                rbMarried.Checked = false;
            }
            else
            {
                rbSingle.Checked = false;
                rbMarried.Checked = true;
            }
            //UnionMembership
            if(isMember == true)
            {
                cbUnionMember.Checked = true;
            }
            else
            {
                cbUnionMember.Checked = false;
            }
        }
    }
}
