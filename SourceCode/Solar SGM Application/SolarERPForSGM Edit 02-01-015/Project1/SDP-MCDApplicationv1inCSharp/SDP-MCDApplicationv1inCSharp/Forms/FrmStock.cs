using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SDP_MCDApplicationv1inCSharp.ComponentClasses;
using SDP_MCDApplicationv1inCSharp.Forms;
using System.Data.SqlClient;

namespace SDP_MCDApplicationv1inCSharp.Forms
{
    public partial class FrmStock : Form
    {
        MCD myccMCD;
        DBConnect dbConnection;
        StrUserAuthentication myOptimizerStrUserAuthentication;
        SGM sgm;

        string FinYear = string.Empty;
        string sSpoolCode = string.Empty;
        string sBarcode, sJobcardNo, sSalesOrderNo;
        int nSavingQuantity, nSizeCount, nIncludeCount;
        int nYesNo, nRowCount, keyascii, nStringLength, nWithSize, nBoxNoLength, nColonLocation, nBoxNo,  nBoxQuantity;
        decimal nSize;
        int nJCQty, NScanCount;

        string SSection;
        int NCartonNo, NStockMonth;
        DateTime DStockDate;

        public FrmStock(SGM _sgm)
        {
            InitializeComponent();
            sgm = _sgm;
            //lblSection.Text = sgm.sSellectionMessage;
            myccMCD = new MCD(sgm.connectionString);
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new StrUserAuthentication();
        }

        
        private void tbBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyascii = (int)e.KeyChar;
            if (keyascii == 13)
            {
                nStringLength = tbBarcode.Text.Trim().Length;
                if (nStringLength < 5)
                {
                    MessageBox.Show("Invalid Barcode");
                    tbLastBarcode.Text = tbBarcode.Text.Trim();
                    tbBarcode.Text = "";
                    return;
                }

                sBarcode = tbBarcode.Text.Trim();
                DataSet dsSelBoxInfo = new DataSet();

                sJobcardNo = sBarcode.Substring(0, 13);
                sSalesOrderNo = sBarcode.Substring(0, 9);
                nBoxNoLength = nStringLength - sJobcardNo.Length;
                nBoxNo = Convert.ToInt32(tbBarcode.Text.Trim().Substring(sJobcardNo.Length + 1));

                using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from PackingDetail Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                    cmd.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Barcode");
                        tbLastBarcode.Text = tbBarcode.Text.Trim();
                        tbBarcode.Text = "";
                        return;
                    }

                    

                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                        sJobcardNo = ds.Tables[0].Rows[0]["JobcardNo"].ToString();
                        NCartonNo = Convert.ToInt32(ds.Tables[0].Rows[0]["CartonNo"].ToString());
                        DStockDate = DateTime.Now;
                        NStockMonth = Convert.ToInt32(DateTime.Now.ToString("yy") + DateTime.Now.Day.ToString());
                        if (rbPackingSection.Checked == true)
                        { 
                            SSection = "PACKING";
                        }
                        else
                        {
                            SSection = "FINISH";
                        }

                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn;
                        cmd1.CommandText = "Select * from PhysicalProductStock Where JobcardNo=@mJobcardNo AND CartonNo=@mCartonNo AND Section = @mSection And StockMonth = @mStockMonth";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd1.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = NCartonNo;
                        cmd1.Parameters.Add(new SqlParameter("@mSection", SqlDbType.VarChar)).Value = SSection;
                        cmd1.Parameters.Add(new SqlParameter("@mStockMonth", SqlDbType.Int)).Value = NStockMonth;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            NScanCount = 1;
                        }
                        else
                        {
                            NScanCount = ds1.Tables[0].Rows.Count + 1;
                        }

                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = conn;
                        cmd2.CommandText = "Update PackingDetail Set Status = 'IN STOCK' Where JobcardNo=@mJobcardNo AND CartonNo=@mCartonNo";
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd2.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = NCartonNo;
                        
                        SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        adapter2.Fill(ds2);

                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = conn;
                        cmd3.CommandText = "Insert Into PhysicalProductStock(Id,Section,JobcardNo,CartonNo,StockDate,StockMonth,Quantity,ScanCount)  Values (@mId,@mSection,@mJobcardNo,@mCartonNo,@mStockDate,@mStockMonth,@mQuantity,@mScanCount)";
                        cmd3.CommandType = CommandType.Text;

                        cmd3.Parameters.Add(new SqlParameter("@mId", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();
                        cmd3.Parameters.Add(new SqlParameter("@mSection", SqlDbType.VarChar)).Value = SSection;
                        cmd3.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd3.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = NCartonNo;
                        cmd3.Parameters.Add(new SqlParameter("@mStockDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd3.Parameters.Add(new SqlParameter("@mStockMonth", SqlDbType.Int)).Value = NStockMonth;
                        cmd3.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Int)).Value = nBoxQuantity;
                        cmd3.Parameters.Add(new SqlParameter("@mScanCount", SqlDbType.Int)).Value = NScanCount;

                        SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                        DataSet ds3 = new DataSet();
                        adapter3.Fill(ds3);

                        tbLastBarcode.Text = sJobcardNo;
                        tbQuantity.Text = nBoxQuantity.ToString();

                        if (tbBoxCount.Text.Trim() == "")
                        {
                            tbBoxCount.Text = "1";
                        }
                        else
                        {
                            tbBoxCount.Text = (Convert.ToInt32(tbBoxCount.Text) + 1).ToString();
                        }
                        
                        tbBarcode.Text = "";
                        tbBarcode.Focus();


                    }
                    else
                        return;
                }

            }          
        }

        private void cbExitBarcodeScanning_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Spool where SpoolCode=@SpoolCode";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@SpoolCode", SqlDbType.VarChar)).Value = sSpoolCode;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

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
                                cmd3.ExecuteNonQuery();
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
                                cmd4.ExecuteNonQuery();
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

        private void cbBack_Click_1(object sender, EventArgs e)
        {
            FrmSelection frmselection = new FrmSelection(sgm);
            frmselection.ShowDialog();
            this.Hide();
        }

    
   
     
        private void tbBarcode_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}

