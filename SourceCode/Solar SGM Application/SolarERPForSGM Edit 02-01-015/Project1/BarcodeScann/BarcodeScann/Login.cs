using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BarcodeScann.ComponentClasses;

namespace BarcodeScann
{
    public partial class Login : Form
    {
        Employee employee;
        DBConnect dbConnection;
        UserAuthentication myOptimizerStrUserAuthentication;
        string sSystemName = string.Empty;
        SGM sgm;

        public Login()
        {
            InitializeComponent();
            sgm = new SGM();
            sgm.connectionString = DBConnect.GetConnectionString();
            employee = new Employee();
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new UserAuthentication();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "" || txtPassword.Text == "")
                MessageBox.Show("Cannot Login Without User Name and Password.");
            else
            {
                employee.userName = txtUserName.Text.Trim();
                dbConnection.SelectEmployee(employee.userName);

                if (sgm.sUserPassword == txtPassword.Text)
                {
                    LoginCheck();
                }
                else
                {
                    MessageBox.Show("Invalid Password");
                }

                if (sgm.sPermissionGranted == "Y")
                {
                    Selection selection = new Selection(sgm);
                    selection.ShowDialog();
                    this.Hide();
                }
            }
        }

        private void LoginCheck()
        {
            System.Net.IPHostEntry h = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            myOptimizerStrUserAuthentication.sUserName = sgm.sUserName;
            myOptimizerStrUserAuthentication.sIPAddress = h.AddressList.GetValue(0).ToString();
            sgm.strIPAddress = h.AddressList.GetValue(0).ToString();
        
            SqlConnection conn = new SqlConnection(sgm.connectionString);
            myOptimizerStrUserAuthentication.sSystemName = conn.WorkstationId;
            myOptimizerStrUserAuthentication.sServer = conn.Database;
            myOptimizerStrUserAuthentication.sLoginTime = DateTime.Now.ToString();
            myOptimizerStrUserAuthentication.sLogoutTime = "";
            myOptimizerStrUserAuthentication.sIsActive = 1;
            myOptimizerStrUserAuthentication.sVersion = "";
            sSystemName = conn.WorkstationId;

            dbConnection.CheckUserAlredyLogin(myOptimizerStrUserAuthentication.sUserName);

            if (sgm.sLoggedin == "Y")
            {
                sgm.sPermissionGranted = "N";
                if (sgm.sIPAddress == h.AddressList.GetValue(0).ToString())
                {
                    MessageBox.Show("Another / Same User Already Logged In this System! \nUser Name : [ " + sgm.sLoggedUser + " ] at System : [ " + sSystemName + " ]" + "  You Can't login now! ");
                }
                else if (sgm.sIPAddress == h.AddressList.GetValue(0).ToString())
                {
                    MessageBox.Show("User Already logged In as  " +
                       "user : [ " + txtUserName.Text + " ] at System : [ " + sSystemName + " ]" +
                       "Do you want to close the previous login ?");
                }
                else
                {
                    MessageBox.Show("User Already Logged In As " +
                       "user : [ " + txtUserName.Text + " ] at System : [ " + sSystemName + " ]" +
                       "  You Can't login now! ");
                }
            }
            else
            {
                sgm.sPermissionGranted = "N";
                if (dbConnection.InsertUserAuthentication(myOptimizerStrUserAuthentication))
                {
                    MessageBox.Show("LogIn OK", "Hi", MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                }
                sgm.sPermissionGranted = "Y";
            }
        }
    }
}