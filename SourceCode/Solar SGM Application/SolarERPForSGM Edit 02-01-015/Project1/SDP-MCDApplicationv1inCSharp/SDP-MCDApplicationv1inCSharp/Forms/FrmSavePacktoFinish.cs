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
    public partial class FrmSavePacktoFinish : Form
    {

        MCD myccMCD;
        DBConnect dbConnection;
        StrUserAuthentication myOptimizerStrUserAuthentication;
        FrmScanning frmscanning;
        StrReadytoDispatch mystrReadytoDispatch;
        StrProductStockBoxDtls strProductStockBoxDtls;

        SGM sgm;

        string sEntryUpdates = string.Empty;
        int nJobcardQuantity, nCorrectBox, nWrongBox, nTotalBox, nSavingQuantity, nSizeCount, nIncludeCount, nIsManualSave, nYesNo, nRowCount, keyascii, nStringLength, nWithSize, nBoxNoLength, nColonLocation, nBoxNo, nSize, nBoxQuantity;
        string sSDID, stbBarcodeText, sSpoolId, sSpoolCode, sBarcode, sJobcardNo, sSalesOrderNo;
        string sEVAProcess, sBeginsWith, sDescription, sStatus, sArticle, sBuyerCode, sBuyer, sJobcardDetailID;
        string sFrom, sShipper, sPackNo, sSalesOrderDetailId;
        string sSize1, sSize2, sSize3, sSize4, sSize5, sSize6, sSize7, sSize8, sSize9, sSize10;
        string sSize01, sSize02, sSize03, sSize04, sSize05, sSize06, sSize07, sSize08, sSize09;
        string sSize11, sSize12, sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize, sPartQtySize;
        int nQuantity1, nQuantity2, nQuantity3, nQuantity4, nQuantity5, nQuantity6, nQuantity7, nQuantity8, nQuantity9, nQuantity10;
        int nQuantity01, nQuantity02, nQuantity03, nQuantity04, nQuantity05, nQuantity06, nQuantity07, nQuantity08, nQuantity09;
        int nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18, nQuantity, nPackQty, nTotalQuantity;
        int nMouldCompletedQty, nFinishPendingQty, nFinishCompletedQty, nMouldPendingQty, nLossQuantity;
        int nFKStatus, nLen, nProducedQuantity;
        string sPartQuantity, sWIPLocation, sFromLocation, sFromStage, sID;
        string SPBPID;
        int NPBPQuantity, NPBPCurrentQuantity, nOriginalBoxQuantity, nQuantityCount;
        int nPBPSavingQuantity;
        int NRJCQty, NRRQty;
        string SRStatus;

        public FrmSavePacktoFinish(SGM _sgm)
        {
            InitializeComponent();
            sgm = _sgm;
            tbSpoolNo.Text = sgm.sSpoolId;
            myccMCD = new MCD(sgm.connectionString);
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new StrUserAuthentication();
            frmscanning = new FrmScanning(sgm);
            strProductStockBoxDtls = new StrProductStockBoxDtls();
        }

        private void JobcardVerification()
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from JobcardDetail where JobcardNo=@JobcardNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid Jobcard No.");
                    sStatus = "Invalid Jobcard No.";
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    MessageBox.Show("Invalid Jobcard No. Multiple Jobcards Existing");
                    sStatus = "Invalid Jobcard No. Multiple Jobcards Existing";
                }
                else
                {
                    nJobcardQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                    sArticle = ds.Tables[0].Rows[0]["Article"].ToString();
                    sBuyerCode = ds.Tables[0].Rows[0]["BuyerCode"].ToString();
                    sBuyer = ds.Tables[0].Rows[0]["BuyerGroupCode"].ToString();

                    sJobcardDetailID = ds.Tables[0].Rows[0]["ID"].ToString();

                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Select * from Buyer where BuyerCode=@BuyerCode";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add(new SqlParameter("@BuyerCode", SqlDbType.VarChar)).Value = sBuyerCode;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);

                        sBuyer = ds1.Tables[0].Rows[0]["BuyerName"].ToString();
                    }

                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Select * from jobcardwip Where JobcardNo=@JobcardNo";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            sStatus = "WIP NOT PLANNED";
                        }
                        else
                        {
                            sEVAProcess = ds1.Tables[0].Rows[0]["Process"].ToString();
                        }
                    }

                    if (sgm.sSection == "PACK")
                    {
                        sShipper = ds.Tables[0].Rows[0]["Shipper"].ToString();
                        sSize01 = ds.Tables[0].Rows[0]["Size01"].ToString();
                        sSize02 = ds.Tables[0].Rows[0]["Size02"].ToString();
                        sSize03 = ds.Tables[0].Rows[0]["Size03"].ToString();
                        sSize04 = ds.Tables[0].Rows[0]["Size04"].ToString();
                        sSize05 = ds.Tables[0].Rows[0]["Size05"].ToString();
                        sSize06 = ds.Tables[0].Rows[0]["Size06"].ToString();
                        sSize07 = ds.Tables[0].Rows[0]["Size07"].ToString();
                        sSize08 = ds.Tables[0].Rows[0]["Size08"].ToString();
                        sSize09 = ds.Tables[0].Rows[0]["Size09"].ToString();
                        sSize10 = ds.Tables[0].Rows[0]["Size10"].ToString();
                        sSize11 = ds.Tables[0].Rows[0]["Size11"].ToString();
                        sSize12 = ds.Tables[0].Rows[0]["Size12"].ToString();
                        sSize13 = ds.Tables[0].Rows[0]["Size13"].ToString();
                        sSize14 = ds.Tables[0].Rows[0]["Size14"].ToString();
                        sSize15 = ds.Tables[0].Rows[0]["Size15"].ToString();
                        sSize16 = ds.Tables[0].Rows[0]["Size16"].ToString();
                        sSize17 = ds.Tables[0].Rows[0]["Size17"].ToString();
                        sSize18 = ds.Tables[0].Rows[0]["Size18"].ToString();

                        sJobcardDetailID = ds.Tables[0].Rows[0]["ID"].ToString();
                        sSalesOrderDetailId = ds.Tables[0].Rows[0]["SalesOrderDetailID"].ToString();
                    }
                }
            }
        }

        private void BarcodeSettings()
        {
            nStringLength = sBarcode.Length;
            sBeginsWith = sBarcode.Substring(0, 1);

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from BarcodeSettings Where StringLength=@StringLength AND BeginsWith LIKE @BeginsWith + '%'";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@StringLength", SqlDbType.Int)).Value = nStringLength;
                cmd.Parameters.Add(new SqlParameter("@BeginsWith", SqlDbType.VarChar)).Value = sBeginsWith;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count != 1)
                {
                    MessageBox.Show("Invalid Jobcard");
                }
                else
                    sDescription = ds.Tables[0].Rows[0]["Description"].ToString();
            }
        }

        private void UpdatePackingDetailInRework()
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from ProductStockRework Where FromJobcardNo = @mJobcardNo";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    strProductStockBoxDtls.FromJobCardNo = ds.Tables[0].Rows[0]["FromJobcardNo"].ToString();
                    strProductStockBoxDtls.ToJobCardNo = ds.Tables[0].Rows[0]["ToJobcardNo"].ToString();
                    strProductStockBoxDtls.FromStage = ds.Tables[0].Rows[0]["FromStage"].ToString();
                    strProductStockBoxDtls.ToStage = ds.Tables[0].Rows[0]["ToStage"].ToString();
                    strProductStockBoxDtls.RCode = ds.Tables[0].Rows[0]["RCode"].ToString();
                    strProductStockBoxDtls.WIPLocation = "FINISH";

                    NRJCQty = Convert.ToInt32(ds.Tables[0].Rows[0]["JCQuantity"].ToString());

                    if (ds.Tables[0].Rows[0]["RQty"] != DBNull.Value)
                    {
                        NRRQty = Convert.ToInt32(ds.Tables[0].Rows[0]["RQty"].ToString());
                    }
                    else
                    {
                        NRRQty = 0;
                    }
                    
                    
                    if (NRRQty >= NRJCQty)
                    {
                        SRStatus = "COMPLETED";
                        sStatus = "Not Completed";
                        return;
                    }
                    else if (SRStatus == "COMPLETED")
                    {
                        SRStatus = "COMPLETED";
                        sStatus = "Not Completed";
                        return;
                    }
                    if (NRRQty + Convert.ToInt32(strProductStockBoxDtls.Quantity) > NRJCQty)
                    {
                        SRStatus = "COMPLETED";
                        sStatus = "Not Completed";
                        return;
                    }
                    else
                    {
                        SRStatus = "PENDING";
                        NRRQty = NRRQty + Convert.ToInt32(strProductStockBoxDtls.Quantity);
                    }
                }
                else
                {
                    sStatus = "Not Completed";
                    return;
                }

            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "sgm_ReworkReplace";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "CHKRRPKGBDTL";
                cmd.Parameters.Add(new SqlParameter("@mFromJobCardNo", SqlDbType.VarChar)).Value = strProductStockBoxDtls.FromJobCardNo;
                cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = nBoxNo;
                cmd.Parameters.Add(new SqlParameter("@mRcode", SqlDbType.VarChar)).Value = strProductStockBoxDtls.RCode;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    SRStatus = "ALREADY SCANNED";
                    sStatus = "Not Completed";
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from PackingDetail Where JobcardNo = @mJobcardNo And CartonNo = @mCartonNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = nBoxNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                strProductStockBoxDtls.CartonNo = Convert.ToInt32(ds.Tables[0].Rows[0]["CartonNo"].ToString());
                strProductStockBoxDtls.Quantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["Quantity"].ToString());
                strProductStockBoxDtls.Size01 = ds.Tables[0].Rows[0]["Size01"].ToString();
                strProductStockBoxDtls.Quantity01 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"].ToString());
                strProductStockBoxDtls.Size02 = ds.Tables[0].Rows[0]["Size02"].ToString();
                strProductStockBoxDtls.Quantity02 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"].ToString());
                strProductStockBoxDtls.Size03 = ds.Tables[0].Rows[0]["Size03"].ToString();
                strProductStockBoxDtls.Quantity03 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"].ToString());
                strProductStockBoxDtls.Size04 = ds.Tables[0].Rows[0]["Size04"].ToString();
                strProductStockBoxDtls.Quantity04 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"].ToString());
                strProductStockBoxDtls.Size05 = ds.Tables[0].Rows[0]["Size05"].ToString();
                strProductStockBoxDtls.Quantity05 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"].ToString());
                strProductStockBoxDtls.Size06 = ds.Tables[0].Rows[0]["Size06"].ToString();
                strProductStockBoxDtls.Quantity06 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"].ToString());
                strProductStockBoxDtls.Size07 = ds.Tables[0].Rows[0]["Size07"].ToString();
                strProductStockBoxDtls.Quantity07 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"].ToString());
                strProductStockBoxDtls.Size08 = ds.Tables[0].Rows[0]["Size08"].ToString();
                strProductStockBoxDtls.Quantity08 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"].ToString());
                strProductStockBoxDtls.Size09 = ds.Tables[0].Rows[0]["Size09"].ToString();
                strProductStockBoxDtls.Quantity09 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"].ToString());
                strProductStockBoxDtls.Size10 = ds.Tables[0].Rows[0]["Size10"].ToString();
                strProductStockBoxDtls.Quantity10 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"].ToString());
                strProductStockBoxDtls.Size11 = ds.Tables[0].Rows[0]["Size11"].ToString();
                strProductStockBoxDtls.Quantity11 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"].ToString());
                strProductStockBoxDtls.Size12 = ds.Tables[0].Rows[0]["Size12"].ToString();
                strProductStockBoxDtls.Quantity12 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"].ToString());
                strProductStockBoxDtls.Size13 = ds.Tables[0].Rows[0]["Size13"].ToString();
                strProductStockBoxDtls.Quantity13 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"].ToString());
                strProductStockBoxDtls.Size14 = ds.Tables[0].Rows[0]["Size14"].ToString();
                strProductStockBoxDtls.Quantity14 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"].ToString());
                strProductStockBoxDtls.Size15 = ds.Tables[0].Rows[0]["Size15"].ToString();
                strProductStockBoxDtls.Quantity15 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"].ToString());
                strProductStockBoxDtls.Size16 = ds.Tables[0].Rows[0]["Size16"].ToString();
                strProductStockBoxDtls.Quantity16 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"].ToString());
                strProductStockBoxDtls.Size17 = ds.Tables[0].Rows[0]["Size17"].ToString();
                strProductStockBoxDtls.Quantity17 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"].ToString());
                strProductStockBoxDtls.Size18 = ds.Tables[0].Rows[0]["Size18"].ToString();
                strProductStockBoxDtls.Quantity18 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"].ToString());
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "sgm_ReworkReplace";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSPSBDTL";

                cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();
                cmd.Parameters.Add(new SqlParameter("@mFromJobCardNo", SqlDbType.VarChar)).Value = strProductStockBoxDtls.FromJobCardNo;
                cmd.Parameters.Add(new SqlParameter("@mToJobCardNo", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ToJobCardNo;
                cmd.Parameters.Add(new SqlParameter("@mFromStage", SqlDbType.VarChar)).Value = strProductStockBoxDtls.FromStage;
                cmd.Parameters.Add(new SqlParameter("@mToStage", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ToStage;
                cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.BigInt)).Value = strProductStockBoxDtls.CartonNo;
                cmd.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Decimal)).Value = strProductStockBoxDtls.Quantity;
                cmd.Parameters.Add(new SqlParameter("@mSize01", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size01;
                cmd.Parameters.Add(new SqlParameter("@mQuantity01", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity01;
                cmd.Parameters.Add(new SqlParameter("@mSize02", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size02;
                cmd.Parameters.Add(new SqlParameter("@mQuantity02", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity02;
                cmd.Parameters.Add(new SqlParameter("@mSize03", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size03;
                cmd.Parameters.Add(new SqlParameter("@mQuantity03", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity03;
                cmd.Parameters.Add(new SqlParameter("@mSize04", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size04;
                cmd.Parameters.Add(new SqlParameter("@mQuantity04", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity04;
                cmd.Parameters.Add(new SqlParameter("@mSize05", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size05;
                cmd.Parameters.Add(new SqlParameter("@mQuantity05", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity05;
                cmd.Parameters.Add(new SqlParameter("@mSize06", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size06;
                cmd.Parameters.Add(new SqlParameter("@mQuantity06", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity06;
                cmd.Parameters.Add(new SqlParameter("@mSize07", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size07;
                cmd.Parameters.Add(new SqlParameter("@mQuantity07", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity07;
                cmd.Parameters.Add(new SqlParameter("@mSize08", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size08;
                cmd.Parameters.Add(new SqlParameter("@mQuantity08", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity08;
                cmd.Parameters.Add(new SqlParameter("@mSize09", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size09;
                cmd.Parameters.Add(new SqlParameter("@mQuantity09", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity09;
                cmd.Parameters.Add(new SqlParameter("@mSize10", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size10;
                cmd.Parameters.Add(new SqlParameter("@mQuantity10", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity10;
                cmd.Parameters.Add(new SqlParameter("@mSize11", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size11;
                cmd.Parameters.Add(new SqlParameter("@mQuantity11", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity11;
                cmd.Parameters.Add(new SqlParameter("@mSize12", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size12;
                cmd.Parameters.Add(new SqlParameter("@mQuantity12", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity12;
                cmd.Parameters.Add(new SqlParameter("@mSize13", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size13;
                cmd.Parameters.Add(new SqlParameter("@mQuantity13", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity13;
                cmd.Parameters.Add(new SqlParameter("@mSize14", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size14;
                cmd.Parameters.Add(new SqlParameter("@mQuantity14", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity14;
                cmd.Parameters.Add(new SqlParameter("@mSize15", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size15;
                cmd.Parameters.Add(new SqlParameter("@mQuantity15", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity15;
                cmd.Parameters.Add(new SqlParameter("@mSize16", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size16;
                cmd.Parameters.Add(new SqlParameter("@mQuantity16", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity16;
                cmd.Parameters.Add(new SqlParameter("@mSize17", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size17;
                cmd.Parameters.Add(new SqlParameter("@mQuantity17", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity17;
                cmd.Parameters.Add(new SqlParameter("@mSize18", SqlDbType.VarChar)).Value = strProductStockBoxDtls.Size18;
                cmd.Parameters.Add(new SqlParameter("@mQuantity18", SqlDbType.Int)).Value = strProductStockBoxDtls.Quantity18;
                //cmd.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = strProductStockBoxDtls.EnteredOnMachineID;
                //cmd.Parameters.Add(new SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = strProductStockBoxDtls.CreatedBy;
                cmd.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                //cmd.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ModifiedBy;
                //cmd.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = strProductStockBoxDtls.ModifiedDate;
                //cmd.Parameters.Add(new SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ExeVersionNo;
                //cmd.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = strProductStockBoxDtls.IsApproved;
                //cmd.Parameters.Add(new SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ApprovedBy;
                //cmd.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value = strProductStockBoxDtls.ApprovedOn;
                //cmd.Parameters.Add(new SqlParameter("@mModuleName", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ModuleName;
                cmd.Parameters.Add(new SqlParameter("@mRCode", SqlDbType.VarChar)).Value = strProductStockBoxDtls.RCode;
                cmd.Parameters.Add(new SqlParameter("@mWIPLocation", SqlDbType.VarChar)).Value = strProductStockBoxDtls.WIPLocation;


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                sStatus = "Completed";
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "sgm_ReworkReplace";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDPKGDTL";

                cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = strProductStockBoxDtls.ToJobCardNo;
                cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = nBoxNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "sgm_ReworkReplace";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDPSRW";

                cmd.Parameters.Add(new SqlParameter("@mRQty", SqlDbType.Int)).Value = NRRQty;
                cmd.Parameters.Add(new SqlParameter("@mStatus", SqlDbType.VarChar)).Value = SRStatus;
                cmd.Parameters.Add(new SqlParameter("@mRCode", SqlDbType.VarChar)).Value = strProductStockBoxDtls.RCode;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
            }
        }




















































































































































































































































































































































































































































































































































































































































































































































































































        private void cbSaveAll_Click(object sender, EventArgs e)
        {
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

                nTotalBox = 0;
                nCorrectBox = 0;
                nWrongBox = 0;
                sEntryUpdates = "Without Sizes";

                for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                {
                    tbSpoolNo.Text = sgm.sSpoolId;

                    switch (ds.Tables[0].Rows[j]["Department"].ToString())
                    {
                      
                        case "PACKING2FINISH":
                            sgm.sSection = "PACK2FINISH";
                            break;

                    }

                    using (SqlConnection conn2 = new SqlConnection(sgm.connectionString))
                    {
                        conn2.Open();
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = conn2;
                        if (sEntryUpdates == "Without Sizes")
                            cmd2.CommandText = "Select * from SpoolDetails Where SpoolHID=@SpoolHID AND WithSize = '0' Order by Barcode,Size";
                        else
                            cmd2.CommandText = "Select '' As ID, Left(Barcode,17) AS Barcode, '0' As IsManualSave,Sum(SavingQuantity) As SavingQuantity  from SpoolDetails where SpoolHID=@SpoolHID AND WithSize = '1' Group By Left(Barcode,17) Order by Barcode";
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.Add(new SqlParameter("@SpoolHID", SqlDbType.VarChar)).Value = sgm.sSpoolId;

                        SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        adapter2.Fill(ds2);

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i <= ds2.Tables[0].Rows.Count - 1; i++)
                            {
                                sSDID = ds2.Tables[0].Rows[i]["ID"].ToString();
                                sBarcode = ds2.Tables[0].Rows[i]["Barcode"].ToString();
                                nStringLength = sBarcode.Length;
                                sJobcardNo = sBarcode.Trim().Substring(0, 13);
                                sSalesOrderNo = sBarcode.Trim().Substring(0, 9);
                                nBoxNoLength = nStringLength - sJobcardNo.Length;
                                nBoxNo = Convert.ToInt32(sBarcode.Trim().Substring(sJobcardNo.Length + 1));
                                nIsManualSave = (Convert.ToInt32(ds2.Tables[0].Rows[i]["IsManualSave"]) * -1);
                                nSavingQuantity = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                                nPBPSavingQuantity = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                                sSize = ds2.Tables[0].Rows[i]["Size"].ToString();
                                BarcodeSettings();

                                JobcardVerification();
                                
                                using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                {
                                    conn3.Open();
                                    SqlCommand cmd3 = new SqlCommand();
                                    cmd3.Connection = conn3;
                                    cmd3.CommandText = "Select * from PackingDetail Where JobcardNo = @mJobcardNo And CartonNo = @mCartonNo";
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                    cmd3.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = nBoxNo;

                                    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                    DataSet ds3 = new DataSet();
                                    adapter3.Fill(ds3);

                                    DataTable dt3 = new DataTable();
                                    adapter3.Fill(dt3);

                                    if (dt3.Rows.Count == 0)
                                    {
                                        sStatus = "INVALID JOBCARD NO.";
                                        nWrongBox = nWrongBox + 1;
                                    }
                                    else if (dt3.Rows.Count == 1)
                                    {
                                        if (ds3.Tables[0].Rows[0]["WIPLocation"].ToString() == "PACKING")
                                        {
                                            UpdatePackingDetailInRework();
                                            if (sStatus == "Completed")
                                            {
                                                sStatus = "SUCCESSFULLY UPDATED";
                                                nCorrectBox = nCorrectBox + 1;
                                            }
                                            else
                                            {
                                                sStatus = "REWORK / REPLACE NOT PLANNED";
                                                nWrongBox = nWrongBox + 1;
                                            }
                                            

                                        }
                                        else
                                        {
                                            sStatus = "BOX NOT IN PACKING SECTION";
                                            nWrongBox = nWrongBox + 1;
                                        }
                                        
                                    }
                                    else
                                    {
                                        sStatus = "INVALID JOBCARD NO. MULTIPLE JOBCARDS EXISTING";
                                        nWrongBox = nWrongBox + 1;
                                    }


                                }

                                using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                {
                                    conn3.Open();
                                    SqlCommand cmd3 = new SqlCommand();
                                    cmd3.Connection = conn3;
                                    cmd3.CommandText = "Select * from AbbrevTable Where Group_ = 'scanstatus' And FullName_ = @Fullname";
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.Parameters.Add(new SqlParameter("@Fullname", SqlDbType.VarChar)).Value = sStatus;

                                    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                    DataSet ds3 = new DataSet();
                                    adapter3.Fill(ds3);

                                    if (ds3.Tables[0].Rows.Count == 1)
                                    {
                                        nFKStatus = Convert.ToInt32(ds3.Tables[0].Rows[0]["Abbrev_"]);
                                    }
                                    else
                                    {
                                        nFKStatus = 99;
                                    }

                                    using (SqlConnection conn4 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn4.Open();
                                        SqlCommand cmd4 = new SqlCommand();
                                        cmd4.Connection = conn4;
                                        if (sEntryUpdates == "Without Sizes")
                                        {
                                            cmd4.CommandText = "Update SpoolDetails Set IsUpdated = '1', UpdatedOn = @UpdatedOn, FKStatus = @FKStatus Where ID =  @ID";
                                        }
                                        else
                                        {
                                            cmd4.CommandText = "Update SpoolDetails Set IsUpdated = '1', UpdatedOn = @UpdatedOn, FKStatus = @FKStatus Where SpoolHID = @SpoolHID And Barcode Like @Barcode + '%'";
                                        }
                                        cmd4.CommandType = CommandType.Text;
                                        cmd4.Parameters.Add(new SqlParameter("@UpdatedOn", SqlDbType.DateTime)).Value = DateTime.Now;
                                        cmd4.Parameters.Add(new SqlParameter("@FKStatus", SqlDbType.Int)).Value = nFKStatus;
                                        cmd4.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = sSDID;

                                        cmd4.Parameters.Add(new SqlParameter("@SpoolHID", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                                        cmd4.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar)).Value = sBarcode;

                                        SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);
                                        DataSet ds4 = new DataSet();
                                        adapter4.Fill(ds4);
                                    }

                                }
                            }
                        }
                        sSpoolId = ds.Tables[0].Rows[j]["SpoolId"].ToString();

                        using (SqlConnection conn5 = new SqlConnection(sgm.connectionString))
                        {
                            conn5.Open();
                            SqlCommand cmd5 = new SqlCommand();
                            cmd5.Connection = conn5;
                            cmd5.CommandText = "Update Spool Set BoxCount = @BoxCount , Quantity = @Quantity, IsUPdated = '1' Where SpoolId = @SpoolID";
                            cmd5.CommandType = CommandType.Text;

                            cmd5.Parameters.Add(new SqlParameter("@BoxCount", SqlDbType.Int)).Value = Convert.ToInt32(tbTotalCartons.Text.Trim());
                            cmd5.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int)).Value = Convert.ToInt32(tbTotalQty.Text.Trim());
                            cmd5.Parameters.Add(new SqlParameter("@SpoolID", SqlDbType.VarChar)).Value = sSpoolId;

                            SqlDataAdapter adapter5 = new SqlDataAdapter(cmd5);
                            DataSet ds5 = new DataSet();
                            adapter5.Fill(ds5);
                        }
                    }
                }
            }

            //if (nWrongBox > 0)
            //{
            //    //plSave.Visible = false;
            //    //plwrongBoxStatus.Visible = true;
            //    //plwrongBoxStatus.BringToFront();

            //    //tbTotalCartonScanned.Text = nTotalBox.ToString();
            //    //tbCorrectBox.Text = nCorrectBox.ToString();
            //    //tbWrongBox.Text = nWrongBox.ToString();

            //    //lbWrongBox.DataSource = myccMCD.LoadWrongScannedBoxes(sSpoolId);
            //    //lbWrongBox.DisplayMember = "ScannedBoxes";
            //    //lbWrongBox.ValueMember = "ScannedBoxes";
            //}
            //else
            //{
            //    FrmSelection frmSelection = new FrmSelection(sgm);
            //    this.Hide();
            //    frmSelection.ShowDialog();
            //    this.Close();
            //    this.Dispose();
            //}

            FrmSelection frmSelection = new FrmSelection(sgm);
            this.Hide();
            frmSelection.ShowDialog();
            this.Close();
            this.Dispose();

        }




    }
}