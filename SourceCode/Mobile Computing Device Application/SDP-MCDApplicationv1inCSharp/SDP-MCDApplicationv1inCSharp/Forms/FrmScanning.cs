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
    public partial class FrmScanning : Form
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
        int nJCQty;
        

        public FrmScanning(SGM _sgm)
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

                if (sBarcode.Contains(":") == true)
                {
                    nWithSize = 1;

                    sJobcardNo = sBarcode.Substring(0, 13);
                    sSalesOrderNo = sBarcode.Substring(0, 9);
                    nBoxNoLength = nStringLength - sJobcardNo.Length;
                    nColonLocation = sBarcode.IndexOf(":");

                    if (nColonLocation == 15)
                        nBoxNo = Convert.ToInt32(sBarcode.Substring(15, 1));
                    else if (nColonLocation == 16)
                        nBoxNo = Convert.ToInt32(sBarcode.Substring(15, 2));
                    else if (nColonLocation == 17)
                        nBoxNo = Convert.ToInt32(sBarcode.Substring(15, 3));

                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from  SpoolDetails Where  SpoolHID=@SpoolId AND Barcode=@Barcode";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                        cmd.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar)).Value = sBarcode.Trim().Substring(0, nColonLocation);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show("This Box Already Scanned With Size Info", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        nSize = Convert.ToDecimal(sBarcode.Substring(nColonLocation + 1));
                        tbSize.Text = nSize.ToString();
                    }

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
                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"].ToString());
                            nSavingQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"].ToString());
                        }
                        else
                            return;
                    }

                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from vw_SolarPackingSingleSizeLabel Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        nSizeCount = ds.Tables[0].Rows.Count;

                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["Size"].ToString()) == nSize)
                            {
                                nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"].ToString());
                                nSavingQuantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"].ToString());
                                break;
                            }
                        }
                    }
                    tbQuantity.Focus();
                }
                else
                {
                    nWithSize = 0;
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
                            if (!chkbxManualSave.Checked)
                            {
                                switch (sgm.sSelectedSection)
                                {
                                    case "MOULDING":
                                        if (ds.Tables[0].Rows[0]["MouldQuantity"] != DBNull.Value)
                                        {
                                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]) > 0)
                                            {
                                                MessageBox.Show("This Box is Already Scanned");
                                                return;
                                            }
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]);
                                        }
                                        else
                                        {
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                        }
                                        nSavingQuantity = nBoxQuantity;
                                        break;
                                    case "FINISHING":
                                        if (ds.Tables[0].Rows[0]["FinishedQuantity"] != DBNull.Value)
                                        {
                                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["FinishedQuantity"]) > 0)
                                            {
                                                MessageBox.Show("This Box is Already Scanned");
                                                return;
                                            }
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["FinishedQuantity"]);
                                        }
                                        else
                                        {
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                        }
                                        nSavingQuantity = nBoxQuantity;
                                        break;
                                    case "PACKING":
                                        if (ds.Tables[0].Rows[0]["PackedQuantity"] != DBNull.Value)
                                        {
                                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["PackedQuantity"]) > 0)
                                            {
                                                MessageBox.Show("This Box is Already Scanned");
                                                return;
                                            }
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["FinishedQuantity"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["PackedQuantity"]);

                                        }
                                        else
                                        {
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                        }
                                        nSavingQuantity = nBoxQuantity;
                                        break;
                                    case "DISPATCH":
                                        break;
                                }

                                
                            }
                            else
                            {
                                switch (sgm.sSelectedSection)
                                {
                                    case "MOULDING":
                                        if (ds.Tables[0].Rows[0]["MouldQuantity"] != DBNull.Value)
                                        {
                                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]) > Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]))
                                            {
                                                MessageBox.Show("This Box is Already Scanned");
                                                return;
                                            }
                                            
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]);
                                        }
                                        else
                                        {
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                        }
                                        
                                        nSavingQuantity = nBoxQuantity;
                                        break;
                                    case "FINISHING":
                                        if (ds.Tables[0].Rows[0]["FinishedQuantity"] != DBNull.Value)
                                        {
                                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["FinishedQuantity"]) > Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]))
                                            {
                                                MessageBox.Show("This Box is Already Scanned");
                                                return;
                                            }
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["FinishedQuantity"]);
                                        }
                                        else
                                        {
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                        }
                                        nSavingQuantity = nBoxQuantity;
                                        break;
                                    case "PACKING":
                                        if (ds.Tables[0].Rows[0]["PackedQuantity"] != DBNull.Value)
                                        {
                                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["PackedQuantity"]) > Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]))
                                            {
                                                MessageBox.Show("This Box is Already Scanned");
                                                return;
                                            }
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["FinishedQuantity"]) - Convert.ToInt32(ds.Tables[0].Rows[0]["PackedQuantity"]);
                                        }
                                        else
                                        {
                                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                        }
                                        
                                        nSavingQuantity = nBoxQuantity;
                                        break;
                                    case "DISPATCH":
                                        break;
                                }
                            }
                        }
                        else
                            return;
                    }

                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from vw_SolarPackingSingleSizeLabel Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        nSizeCount = ds.Tables[0].Rows.Count;

                        if (nSizeCount == 1)
                        {
                            tbSize.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                            nSize = Convert.ToDecimal(ds.Tables[0].Rows[0]["Size"]);
                        }
                        else
                        {
                            tbSize.Text = "MULTI SIZE";
                            nSize = 0;

                            if (chkbxManualSave.Checked)
                            { 
                                MessageBox.Show("Manual Change Quantity for Multiple Size Boxes without Size is not allowed.");
                                chkbxManualSave.Enabled = false;
                                return;
                            }
                        }
                    }
                }

                if (chkbxNoBoxAdd.Checked)
                    nIncludeCount = 0;
                else
                    nIncludeCount = 1;

                if (nWithSize != 0)
                {
                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from  SpoolDetails Where  SpoolHID=@SpoolId AND Barcode LIKE '%' + @Barcode + '%'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                        cmd.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar)).Value = tbBarcode.Text.Trim();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show("This Box Already Scanned", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        //nSize = Convert.ToInt32(sBarcode.Substring(nColonLocation + 1));
                        //tbSize.Text = nSize.ToString();
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from  SpoolDetails Where  SpoolHID=@SpoolId AND Barcode=@Barcode";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                        cmd.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar)).Value = tbBarcode.Text.Trim();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show("This Box Already Scanned", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        //nSize = Convert.ToInt32(sBarcode.Substring(nColonLocation + 1));
                        //tbSize.Text = nSize.ToString();
                    }
                }

                if (!chkbxManualSave.Checked)
                {
                    lblBalQty.Visible = false;
                    tbBalQty.Visible = false;
                    tbBalQty.Text = "";


                    using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Insert into SpoolDetails(ID,CreatedBy,CreatedDate,ModuleName,SpoolHID,BarCode,WithSize,Size,BoxQuantity,SavingQuantity,IsUpdated,IncludeCount,IsManualSave,SizeCount) Values (@ID,@CreatedBy,@CreatedDate,@ModuleName,@SpoolHID,@BarCode,@WithSize,@Size,@BoxQuantity,@SavingQuantity,@IsUpdated,@IncludeCount,@IsManualSave,@SizeCount)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar)).Value = sgm.sUserName;
                        cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar)).Value = "MCD";
                        cmd.Parameters.Add(new SqlParameter("@SpoolHID", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                        cmd.Parameters.Add(new SqlParameter("@BarCode", SqlDbType.VarChar)).Value = sBarcode;
                        cmd.Parameters.Add(new SqlParameter("@WithSize", SqlDbType.Int)).Value = nWithSize;
                        cmd.Parameters.Add(new SqlParameter("@Size", SqlDbType.Int)).Value = nSize;
                        cmd.Parameters.Add(new SqlParameter("@BoxQuantity", SqlDbType.Int)).Value = nBoxQuantity;
                        cmd.Parameters.Add(new SqlParameter("@SavingQuantity", SqlDbType.Int)).Value = nSavingQuantity;
                        cmd.Parameters.Add(new SqlParameter("@IsUpdated", SqlDbType.Bit)).Value = 0;
                        cmd.Parameters.Add(new SqlParameter("@IncludeCount", SqlDbType.Int)).Value = nIncludeCount;
                        cmd.Parameters.Add(new SqlParameter("@IsManualSave", SqlDbType.Bit)).Value = chkbxManualSave.CheckState;
                        cmd.Parameters.Add(new SqlParameter("@SizeCount", SqlDbType.Int)).Value = nSizeCount;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        //cmd.ExecuteNonQuery();

                        sgm.nScannedBoxes++;
                        lbScannedBoxes.Text = "Sanned Boxes :- " + sgm.nScannedBoxes.ToString();
                        tbLastBarcode.Text = tbBarcode.Text;
                        tbQuantity.Text = nBoxQuantity.ToString();
                        tbBarcode.Text = "";
                        tbBarcode.Focus();
                    }
                }
                else
                {
                    lblBalQty.Visible = true;
                    tbBalQty.Visible = true;
                    tbBalQty.Text = nBoxQuantity.ToString();

                    tbQuantity.Text = "";
                    tbQuantity.Focus();
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

        private void cbSaveBarcode_Click(object sender, EventArgs e)
        {
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

                if (ds.Tables[0].Rows.Count > 1)
                {
                    MessageBox.Show("Multiple Spool Open Simultaneously");
                    return;
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No Spool Available for further process");
                    return;
                }
                else if (ds.Tables[0].Rows.Count == 1)
                {
                    sgm.sSpoolId = ds.Tables[0].Rows[0]["SpoolID"].ToString();
                    sgm.dSpoolDt = Convert.ToDateTime(ds.Tables[0].Rows[0]["SpoolDate"]);

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
                            MessageBox.Show("Boxes Not Scanned");
                            return;
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

        private void chkbxManualSave_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkbxManualSave.Checked == true)
            {
                cbManualSave.Enabled = true;
                chkbxNoBoxAdd.Enabled = true;
            }
            else
            {
                cbManualSave.Enabled = false;
                chkbxNoBoxAdd.Enabled = false;
                chkbxNoBoxAdd.Checked = false;
            }
            tbBarcode.Focus();
        }

        private void cbManualSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from vw_JobcardDtlQtyinCrossApply Where JobcardNo = @Jobcard And Size = @Size";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Jobcard", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd.Parameters.Add(new SqlParameter("@Size", SqlDbType.VarChar)).Value = tbSize.Text.Trim();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                nJCQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"].ToString());
                conn.Close();
            }

            int nProducedQty, nSpoolQty;
            
            using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
            {
                conn1.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn1;
                cmd1.CommandText = "Select Cast ((IsNull(Sum(Quantity),0)) As Int) AS Quantity From ProductionByProcess Where WorkOrderNo = @Jobcard And Size = @Size And ProcessName = @ProcessName";
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.Add(new SqlParameter("@Jobcard", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd1.Parameters.Add(new SqlParameter("@Size", SqlDbType.VarChar)).Value = tbSize.Text.Trim();
                cmd1.Parameters.Add(new SqlParameter("@ProcessName", SqlDbType.VarChar)).Value = sgm.sSection;

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adapter1.Fill(ds1);
                
                nProducedQty = Convert.ToInt32(ds1.Tables[0].Rows[0]["Quantity"].ToString());
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select SP.Department,SPD.Size,SUBSTRING(SPD.BarCode,1,13) As JobcardNo,Cast ((IsNull(Sum(SPD.SavingQuantity),0)) As Int) AS Quantity From SpoolDetails As SPD, Spool As SP Where SP.SpoolID = spd.SpoolHID And sp.IsUpdated = '0' And SP.Department = @Department And SUBSTRING(SPD.BarCode,1,13) = @Jobcard And SPD.Size = @Size Group By SP.Department,SPD.Size,SUBSTRING(SPD.BarCode,1,13)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Jobcard", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd.Parameters.Add(new SqlParameter("@Size", SqlDbType.VarChar)).Value = tbSize.Text.Trim();
                cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.VarChar)).Value = sgm.sSelectedSection;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                
                nSpoolQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"].ToString());
            }

            if ((nProducedQty + nSpoolQty + Convert.ToInt32(tbQuantity.Text.Trim())) > nJCQty)
            { 
                MessageBox.Show("Quantity Exceeds the Jobcard Quantity");
            }
            else if (Convert.ToInt32(tbQuantity.Text.Trim()) + 3 > nBoxQuantity)
            {
                MessageBox.Show("Quantity Exceeds the Box Max Quantity");
            }
            else
            { 
                using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from  SpoolDetails Where  SpoolHID=@SpoolId AND Barcode LIKE '%' + @Barcode + '%'";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                cmd.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar)).Value = tbBarcode.Text.Trim();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("This Box Already Scanned", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {
                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Select * from vw_SolarPackingSingleSizeLabel Where JobcardNo = @JobCardNo And CartonNo = @CartonNo";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add(new SqlParameter("@JobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd1.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);

                        nSizeCount = ds1.Tables[0].Rows.Count;


                        using (SqlConnection conn2 = new SqlConnection(sgm.connectionString))
                        {
                            conn2.Open();
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = conn2;
                            cmd2.CommandText = "Insert Into SpoolDetails(ID,CreatedBy,CreatedDate,ModuleName,SpoolHID,BarCode,WithSize,Size,BoxQuantity,SavingQuantity,IsUpdated,IncludeCount,IsManualSave,Sizecount) Values (@ID,@CreatedBy,@CreatedDate,@ModuleName,@SpoolHID,@BarCode,@WithSize,@Size,@BoxQuantity,@SavingQuantity,@IsUpdated,@IncludeCount,@IsManualSave,@Sizecount)";
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Parameters.Add(new SqlParameter("@JobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                            cmd2.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                            cmd2.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();;
                            cmd2.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar)).Value = sgm.sUserName;
                            cmd2.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                            cmd2.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar)).Value = "MCD";
                            cmd2.Parameters.Add(new SqlParameter("@SpoolHID", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                            cmd2.Parameters.Add(new SqlParameter("@BarCode", SqlDbType.VarChar)).Value = tbBarcode.Text.Trim();
                            cmd2.Parameters.Add(new SqlParameter("@WithSize", SqlDbType.Int)).Value = nWithSize;
                            cmd2.Parameters.Add(new SqlParameter("@Size", SqlDbType.Int)).Value = nSize;
                            cmd2.Parameters.Add(new SqlParameter("@BoxQuantity", SqlDbType.Int)).Value = nBoxQuantity;
                            cmd2.Parameters.Add(new SqlParameter("@SavingQuantity", SqlDbType.Int)).Value = Convert.ToInt32(tbQuantity.Text.Trim());
                            cmd2.Parameters.Add(new SqlParameter("@IsUpdated", SqlDbType.Bit)).Value = 0;
                            cmd2.Parameters.Add(new SqlParameter("@IncludeCount", SqlDbType.Int)).Value = nIncludeCount;
                            cmd2.Parameters.Add(new SqlParameter("@IsManualSave", SqlDbType.Bit)).Value = 1;
                            cmd2.Parameters.Add(new SqlParameter("@SizeCount", SqlDbType.Int)).Value = nSizeCount;


                            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                            DataSet ds2 = new DataSet();
                            adapter2.Fill(ds2);
                            //cmd2.ExecuteNonQuery();

                            tbLastBarcode.Text = tbBarcode.Text;
                            tbQuantity.Text = "";
                            tbBarcode.Text = "";
                            tbBarcode.Focus();
                        }
                    }
                }
            }
        }
        }

        private void tbBarcode_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}

