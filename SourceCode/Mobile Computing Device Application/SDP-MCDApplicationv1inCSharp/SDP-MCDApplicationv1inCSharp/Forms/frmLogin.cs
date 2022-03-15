using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using SDP_MCDApplicationv1inCSharp.ComponentClasses;
using SDP_MCDApplicationv1inCSharp.Forms;

namespace SDP_MCDApplicationv1inCSharp
{
    public partial class frmLogin : Form
    {

        MCD myccMCD;
        StrEmployee employee;
        DBConnect dbConnection;
        StrUserAuthentication myOptimizerStrUserAuthentication;
        string sSystemName = string.Empty;
        SGM sgm;

        

        public frmLogin()
        {
            InitializeComponent();
            sgm = new SGM();
            sgm.connectionString = DBConnect.GetConnectionString();
            employee = new StrEmployee();
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new StrUserAuthentication();
            myccMCD = new MCD(sgm.connectionString);
        }

        private void cbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbLogin_Click(object sender, EventArgs e)
        {
            
            if (tbUserName.Text.Trim() == "" || tbPassword.Text.Trim() == "")
            {
                MessageBox.Show("Cannot Login Without User Name and Password.");
            }
            else
            {
                employee.userName = tbUserName.Text.Trim();
                dbConnection.SelectEmployee(employee.userName);

                if (sgm.sUserPassword == tbPassword.Text.Trim())
                {
                    LoginCheck();
                }
                else
                {
                    MessageBox.Show("Invalid Password");
                }

                if (sgm.sPermissionGranted == "Y")
                {
                    sgm.nVersion = Convert.ToDecimal(lblVersion.Text.Trim());
                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from Spool Where UserName=@UserName AND IsUpdated='0'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = sgm.sUserName;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            sgm.sSpoolId = "";
                            FrmSelection frmSelection = new FrmSelection(sgm);
                            this.Hide();
                            frmSelection.ShowDialog();
                            
                            this.Close();
                            this.Dispose();
                        }
                        else
                        {
                            sgm.sSpoolId = ds.Tables[0].Rows[0]["SpoolID"].ToString();
                            sgm.dSpoolDt = Convert.ToDateTime(ds.Tables[0].Rows[0]["SpoolDate"]);

                            sgm.sProcess = "PRODUCTION";

                            sgm.sSelectedSection = ds.Tables[0].Rows[0]["Department"].ToString();

                            if (sgm.sSelectedSection == "MOULDING")
                            {
                                sgm.sSection = "MOULD";
                            }
                            else if (sgm.sSelectedSection == "FINISHING")
                            {
                                sgm.sSection = "FINISH";
                            }
                            else
                            {
                                sgm.sSection = "PACK";
                            }
                            
                            

                            using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                            {
                                conn1.Open();
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = conn1;
                                cmd1.CommandText = "Select IsNull(Sum(IncludeCount),0) As BoxCount,IsNull(Sum(SavingQuantity),0) As Quantity From SpoolDetails Where SpoolHID=@SpoolHID";
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.Add(new SqlParameter("@SpoolHID", SqlDbType.VarChar)).Value = sgm.sSpoolId;

                                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                DataSet ds1 = new DataSet();
                                adapter1.Fill(ds1);

                                if (Convert.ToInt32(ds1.Tables[0].Rows[0]["BoxCount"]) == 0)
                                {
                                    sgm.sSpoolId = "";
                                    FrmSelection frmSelection = new FrmSelection(sgm);
                                    this.Hide();
                                    frmSelection.ShowDialog();
                                    this.Close();
                                    this.Dispose();
                                }
                                FrmSave frmSave = new FrmSave(sgm);

                                frmSave.tbTotalCartons.Text = ds1.Tables[0].Rows[0]["BoxCount"].ToString();
                                frmSave.tbTotalQty.Text = ds1.Tables[0].Rows[0]["Quantity"].ToString();

                                frmSave.lbScannedBoxes.DataSource = myccMCD.LoadScannedBoxes(sgm.sSpoolId);
                                frmSave.lbScannedBoxes.DisplayMember = "ScannedBoxes";
                                frmSave.lbScannedBoxes.ValueMember = "ScannedBoxes";
                                frmSave.plSave.Visible = true;
                                frmSave.plSave.BringToFront();
                                this.Hide();
                                frmSave.ShowDialog();
                                this.Close();
                                this.Dispose();
                            }
                        }
                    }



                    
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

            dbConnection.CheckUserAlreadyLogin(myOptimizerStrUserAuthentication.sUserName);

            if (sgm.sLoggedin == "Y")
            {
                sgm.sPermissionGranted = "N";
                if (sgm.sIPAddress == h.AddressList.GetValue(0).ToString())
                {
                    MessageBox.Show("Another / Same User Already Logged In this System! \nUser Name : [ " + sgm.sLoggedUser + " ] at System : [ " + sSystemName + " ]" + "  You Can't login now! ");
                    Application.Exit();
                }
                else if (sgm.sIPAddress == h.AddressList.GetValue(0).ToString())
                {
                    MessageBox.Show("User Already logged In as  " +
                       "user : [ " + tbUserName.Text.Trim() + " ] at System : [ " + sSystemName + " ]" +
                       "Do you want to close the previous login ?");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("User Already Logged In As " +
                       "user : [ " + tbUserName.Text.Trim() + " ] at System : [ " + sSystemName + " ]" +
                       "  You Can't login now! ");
                    Application.Exit();
                }
            }
            else
            {
                sgm.sPermissionGranted = "N";
                dbConnection.InsertUserAuthentication(myOptimizerStrUserAuthentication);
                sgm.sPermissionGranted = "Y";
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                tbServerName.Text = conn.DataSource;
                tbDatabaseName.Text = conn.Database;
            }
            lblDate.Text = DateTime.Now.ToString();
        }

        private void Panel1_GotFocus(object sender, EventArgs e)
        {

        }
        
    }
}