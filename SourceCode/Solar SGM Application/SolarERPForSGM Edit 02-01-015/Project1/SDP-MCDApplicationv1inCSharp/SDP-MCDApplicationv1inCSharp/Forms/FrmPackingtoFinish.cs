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
    public partial class FrmPackingtoFinish : Form
    {
        MCD myccMCD;
        DBConnect dbConnection;
        StrUserAuthentication myOptimizerStrUserAuthentication;
        SGM sgm;
         
        string FinYear = string.Empty;
        string sSpoolCode = string.Empty;
        string sBarcode, sJobcardNo, sSalesOrderNo;
        int nSavingQuantity, nSizeCount, nIncludeCount;
        int nYesNo, nRowCount, keyascii, nStringLength, nWithSize, nBoxNoLength, nColonLocation, nBoxNo, nBoxQuantity;
        decimal nSize;
        int nJCQty;

        public FrmPackingtoFinish(SGM _sgm)
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
                            nBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                            nSavingQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                        }
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
                        FrmSavePacktoFinish frmSavePacktoFinish = new FrmSavePacktoFinish(sgm);

                        frmSavePacktoFinish.tbTotalCartons.Text = ds1.Tables[0].Rows[0]["BoxCount"].ToString();
                        frmSavePacktoFinish.tbTotalQty.Text = ds1.Tables[0].Rows[0]["Quantity"].ToString();

                        frmSavePacktoFinish.lbScannedBoxes.DataSource = myccMCD.LoadScannedBoxes(sgm.sSpoolId);
                        frmSavePacktoFinish.lbScannedBoxes.DisplayMember = "ScannedBoxes";
                        frmSavePacktoFinish.lbScannedBoxes.ValueMember = "ScannedBoxes";
                        frmSavePacktoFinish.plSave.Visible = true;
                        frmSavePacktoFinish.plSave.BringToFront();
                        this.Hide();
                        frmSavePacktoFinish.ShowDialog();
                        this.Close();
                        this.Dispose();
                    }
                }
            }
        }

        

        
    }
}