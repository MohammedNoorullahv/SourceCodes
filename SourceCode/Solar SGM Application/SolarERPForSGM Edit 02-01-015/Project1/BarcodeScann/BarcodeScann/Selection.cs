using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BarcodeScann.ComponentClasses;
using System.Data.SqlClient;

namespace BarcodeScann
{
    public partial class Selection : Form
    {
        MCD myccMCD;
        DBConnect dbConnection;
        UserAuthentication myOptimizerStrUserAuthentication;
        SGM sgm;

        string FinYear = string.Empty;
        string sSpoolCode = string.Empty;
        int nYesNo, nRowCount;

        public Selection(SGM _sgm)
        {
            InitializeComponent();
            sgm = _sgm;
            myccMCD = new MCD(sgm.connectionString);
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new UserAuthentication();
        }

        private void LoadComboItems()
        {
            cmbShift.DataSource = myccMCD.LoadShift();
            cmbShift.DisplayMember = "ShiftInfo";
            cmbShift.ValueMember = "SHIFTCODE";

            cmbMachine.DataSource = myccMCD.LoadMachine();
            cmbMachine.DisplayMember = "LocationName";
            cmbMachine.ValueMember = "LocationCode";
        }

        private void LoadAccessRights()
        {
            btnFinishing.Enabled = false;
            btnMoulding.Enabled = false;
            btnPacking.Enabled = false;
            btnDispatch.Enabled = false;

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from AppModuleAccess Where UserName=@UserName Order by AccessModuleName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = sgm.sUserName.ToUpper();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Section not alotted.", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    for (int i = 0; i <= (ds.Tables[0].Rows.Count - 1); i++)
                    {
                        string str = ds.Tables[0].Rows[i]["AccessModuleName"].ToString();

                        switch (ds.Tables[0].Rows[i]["AccessModuleName"].ToString())
                        {
                            case "ALL":
                                btnFinishing.Enabled = true;
                                btnMoulding.Enabled = true;
                                btnPacking.Enabled = true;
                                btnDispatch.Enabled = true;
                                break;
                            case "MOULDING":
                                btnMoulding.Enabled = true;
                                sgm.sSelectedSection = "MOULDING";
                                break;
                            case "FINISHING":
                                btnFinishing.Enabled = true;
                                sgm.sSelectedSection = "FINISHING";
                                break;
                            case "PACKING":
                                btnPacking.Enabled = true;
                                sgm.sSelectedSection = "PACKING";
                                break;
                        }
                    }
                }
            }
        }

        private void Selection_Load(object sender, EventArgs e)
        {
            LoadComboItems();
            LoadAccessRights();
            sgm.sSection = "";
        }

        private void btnMoulding_Click(object sender, EventArgs e)
        {
            if (sgm.sSection == "" | sgm.sSection == "MOULD")
            {
                sgm.sSelectedSection = "MOULDING";
                sgm.sSection = "MOULD";
                sgm.sProcess = "Production";
            }
            else
            {
                MessageBox.Show("Other than MOULD Section already selectd. Kindly save previous Spool and change the section", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            Next();
        }

        private void btnFinishing_Click(object sender, EventArgs e)
        {
            if (sgm.sSection == "" | sgm.sSection == "FINISH")
            {
                sgm.sSelectedSection = "FINISHING";
                sgm.sSection = "FINISH";
                sgm.sProcess = "Production";
            }
            else
            {
                MessageBox.Show("Other than FINISH Section already selectd. Kindly save previous Spool and change the section", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            Next();
        }

        private void btnPacking_Click(object sender, EventArgs e)
        {
            if (sgm.sSection == "" | sgm.sSection == "PACK")
            {
                sgm.sSelectedSection = "PACKING";
                sgm.sSection = "PACK";
                sgm.sProcess = "Production";
            }
            else
            {
                MessageBox.Show("Other than PACK Section already selectd. Kindly save previous Spool and change the section", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            Next();
        }

        private void Next()
        {
            sgm.sShiftCode = cmbShift.SelectedValue.ToString();
            sgm.sMachine = cmbMachine.Text;
            sgm.sMachinecode = cmbMachine.SelectedValue.ToString();
            sgm.sSellectionMessage = "BARCODE SCANNING : " + sgm.sSelectedSection;

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Spool Where Department=@SelectedSection AND Shift=@ShiftCode AND UserName=@UserName AND IsUpdated='0'";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@SelectedSection", SqlDbType.VarChar)).Value = sgm.sSelectedSection;
                cmd.Parameters.Add(new SqlParameter("@ShiftCode", SqlDbType.VarChar)).Value = sgm.sShiftCode;
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = sgm.sUserName;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    GetFinancialYear(DateTime.Now);
                    sSpoolCode = sgm.sSelectedSection.Substring(0, 1) + "-" + DateTime.Now.Month.ToString("d2").ToString() + "-" + FinYear.ToString();


                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Select * from Spool where SpoolCode=@SpoolCode";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add(new SqlParameter("@SpoolCode", SqlDbType.VarChar)).Value = sSpoolCode;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);

                        nRowCount = ds1.Tables[0].Rows.Count + 1;
                        sgm.sSpoolId = sSpoolCode + "-" + nRowCount.ToString().PadLeft(6, '0');
                        Save save = new Save(sgm);

                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = conn1;
                        cmd2.CommandText = "Insert into Spool(ID,CreatedBy,CreatedDate,ModuleName,SpoolID,SpoolDate,Department,Shift,UserName,BoxCount,Quantity,IsUpdated,SpoolCode,DeviceId) Values (@ID,@CreatedBy,@CreatedDate,@ModuleName,@SpoolID,@SpoolDate,@Department,@Shift,@UserName,@BoxCount,@Quantity,@IsUpdated,@SpoolCode,@DeviceId)";
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();
                        cmd2.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar)).Value = sgm.sUserName;
                        cmd2.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd2.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar)).Value = "MCD";
                        cmd2.Parameters.Add(new SqlParameter("@SpoolID", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                        cmd2.Parameters.Add(new SqlParameter("@SpoolDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd2.Parameters.Add(new SqlParameter("@Department", SqlDbType.VarChar)).Value = sgm.sSelectedSection;
                        cmd2.Parameters.Add(new SqlParameter("@Shift", SqlDbType.VarChar)).Value = sgm.sShiftCode;
                        cmd2.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = sgm.sUserName;
                        cmd2.Parameters.Add(new SqlParameter("@BoxCount", SqlDbType.Int)).Value = 0;
                        cmd2.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int)).Value = 0;
                        cmd2.Parameters.Add(new SqlParameter("@IsUpdated", SqlDbType.Bit)).Value = 0;
                        cmd2.Parameters.Add(new SqlParameter("@SpoolCode", SqlDbType.VarChar)).Value = sSpoolCode;
                        cmd2.Parameters.Add(new SqlParameter("@DeviceId", SqlDbType.VarChar)).Value = sgm.strIPAddress;

                        SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        adapter2.Fill(ds2);
                        conn1.Close();
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        sgm.sSpoolId = ds.Tables[0].Rows[0]["SpoolId"].ToString();
                        //Save save = new Save(sgm);

                    }
                    else
                    {
                        MessageBox.Show("ERROR : Multi Spool Is Active", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            Scanning scanning = new Scanning(sgm);
            scanning.ShowDialog();
            this.Hide();
        }

        public string GetFinancialYear(DateTime curDate)
        {
            int CurrentYear = Convert.ToInt32(curDate.Year.ToString().Substring(2));
            int PreviousYear = CurrentYear - 1;
            int NextYear = CurrentYear + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();

            if (curDate.Month > 3)
            {
                FinYear = CurYear.ToString() + NexYear.ToString();
            }
            else
            {
                FinYear = PreYear.ToString() + CurYear.ToString();
            }
            return FinYear;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Spool where SpoolCode=@SpoolCode";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@SpoolCode", SqlDbType.VarChar)).Value = sgm.sSpoolId;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count != 0)
                {
                    switch (MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2))
                    {
                        case DialogResult.Yes:
                            using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                            {
                                conn3.Open();
                                SqlCommand cmd3 = new SqlCommand();
                                cmd3.Connection = conn3;
                                cmd3.CommandText = "Delete from SpoolDetails Where SpoolHID=@SpoolId";
                                cmd3.CommandType = CommandType.Text;
                                cmd3.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;

                                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                DataSet ds3 = new DataSet();
                                adapter3.Fill(ds3);
                                conn3.Close();
                            }

                            using (SqlConnection conn4 = new SqlConnection(sgm.connectionString))
                            {
                                conn4.Open();
                                SqlCommand cmd4 = new SqlCommand();
                                cmd4.Connection = conn4;
                                cmd4.CommandText = "Delete from Spool Where SpoolID=@SpoolId";
                                cmd4.CommandType = CommandType.Text;
                                cmd4.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;

                                SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);
                                DataSet ds4 = new DataSet();
                                adapter4.Fill(ds4);
                                conn4.Close();
                            }

                            myOptimizerStrUserAuthentication.sUserName = sgm.sUserName;
                            myOptimizerStrUserAuthentication.sLogoutTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff");
                            myOptimizerStrUserAuthentication.sIsActive = 0;

                            if (dbConnection.UpdateUserAuthentication(myOptimizerStrUserAuthentication))
                            {
                                Application.Exit();
                            }
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    myOptimizerStrUserAuthentication.sUserName = sgm.sUserName;
                    myOptimizerStrUserAuthentication.sLogoutTime = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff");
                    myOptimizerStrUserAuthentication.sIsActive = 0;

                    if (dbConnection.UpdateUserAuthentication(myOptimizerStrUserAuthentication))
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {

        }
    }
}