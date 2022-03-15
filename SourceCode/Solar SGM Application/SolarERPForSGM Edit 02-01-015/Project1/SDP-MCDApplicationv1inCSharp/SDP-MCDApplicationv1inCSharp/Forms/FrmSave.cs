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
    public partial class FrmSave : Form
    {
        MCD myccMCD;
        DBConnect dbConnection;
        StrUserAuthentication myOptimizerStrUserAuthentication;
        FrmScanning frmscanning;
        StrReadytoDispatch mystrReadytoDispatch;

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

        public FrmSave(SGM _sgm)
        {
            InitializeComponent();
            sgm = _sgm;
            tbSpoolNo.Text = sgm.sSpoolId;
            myccMCD = new MCD(sgm.connectionString);
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new StrUserAuthentication();
            frmscanning = new FrmScanning(sgm);
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

        private void ProductionQuantityUpdates()
        {
            nBoxNoLength = nStringLength - sJobcardNo.Length;
            nBoxNo = Convert.ToInt32(sBarcode.Trim().Substring(sJobcardNo.Length + 1));

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
                    stbBarcodeText = "";
                    return;
                }

                nPackQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                nOriginalBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);

                sSize1 = ds.Tables[0].Rows[0]["Size01"].ToString();
                sSize2 = ds.Tables[0].Rows[0]["Size02"].ToString();
                sSize3 = ds.Tables[0].Rows[0]["Size03"].ToString();
                sSize4 = ds.Tables[0].Rows[0]["Size04"].ToString();
                sSize5 = ds.Tables[0].Rows[0]["Size05"].ToString();
                sSize6 = ds.Tables[0].Rows[0]["Size06"].ToString();
                sSize7 = ds.Tables[0].Rows[0]["Size07"].ToString();
                sSize8 = ds.Tables[0].Rows[0]["Size08"].ToString();
                sSize9 = ds.Tables[0].Rows[0]["Size09"].ToString();
                sSize10 = ds.Tables[0].Rows[0]["Size10"].ToString();
                sSize11 = ds.Tables[0].Rows[0]["Size11"].ToString();
                sSize12 = ds.Tables[0].Rows[0]["Size12"].ToString();
                sSize13 = ds.Tables[0].Rows[0]["Size13"].ToString();
                sSize14 = ds.Tables[0].Rows[0]["Size14"].ToString();
                sSize15 = ds.Tables[0].Rows[0]["Size15"].ToString();
                sSize16 = ds.Tables[0].Rows[0]["Size16"].ToString();
                sSize17 = ds.Tables[0].Rows[0]["Size17"].ToString();
                sSize18 = ds.Tables[0].Rows[0]["Size18"].ToString();

                if (sEntryUpdates == "Without Sizes")
                {
                    nQuantity1 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"]);
                    nQuantity2 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"]);
                    nQuantity3 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"]);
                    nQuantity4 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"]);
                    nQuantity5 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"]);
                    nQuantity6 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"]);
                    nQuantity7 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"]);
                    nQuantity8 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"]);
                    nQuantity9 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"]);
                    nQuantity10 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"]);
                    nQuantity11 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"]);
                    nQuantity12 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"]);
                    nQuantity13 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"]);
                    nQuantity14 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"]);
                    nQuantity15 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"]);
                    nQuantity16 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"]);
                    nQuantity17 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"]);
                    nQuantity18 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"]);
                    nQuantityCount = 0;
                    if (nQuantity1 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity2 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity3 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity4 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity5 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity6 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity7 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity8 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity9 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity10 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity11 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity12 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity13 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity14 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity15 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity16 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity17 > 0) nQuantityCount = nQuantityCount + 1;
                    if (nQuantity18 > 0) nQuantityCount = nQuantityCount + 1;

                    if (nQuantityCount == 1)
                    {
                        if (nQuantity1 > 0) nQuantity1 = nSavingQuantity;
                        else if (nQuantity2 > 0) nQuantity2 = nSavingQuantity;
                        else if (nQuantity3 > 0) nQuantity3 = nSavingQuantity;
                        else if (nQuantity4 > 0) nQuantity4 = nSavingQuantity;
                        else if (nQuantity5 > 0) nQuantity5 = nSavingQuantity;
                        else if (nQuantity6 > 0) nQuantity6 = nSavingQuantity;
                        else if (nQuantity7 > 0) nQuantity7 = nSavingQuantity;
                        else if (nQuantity8 > 0) nQuantity8 = nSavingQuantity;
                        else if (nQuantity9 > 0) nQuantity9 = nSavingQuantity;
                        else if (nQuantity10 > 0) nQuantity10 = nSavingQuantity;
                        else if (nQuantity11 > 0) nQuantity11 = nSavingQuantity;
                        else if (nQuantity12 > 0) nQuantity12 = nSavingQuantity;
                        else if (nQuantity13 > 0) nQuantity13 = nSavingQuantity;
                        else if (nQuantity14 > 0) nQuantity14 = nSavingQuantity;
                        else if (nQuantity15 > 0) nQuantity15 = nSavingQuantity;
                        else if (nQuantity16 > 0) nQuantity16 = nSavingQuantity;
                        else if (nQuantity17 > 0) nQuantity17 = nSavingQuantity;
                        else if (nQuantity18 > 0) nQuantity18 = nSavingQuantity;
                    }
                    else
                    {
                    }
                }
                else
                {
                    using (SqlConnection conn2 = new SqlConnection(sgm.connectionString))
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = conn2;
                        cmd2.CommandText = "Select * from  SpoolDetails Where  SpoolHID =@SpoolHID AND Barcode Like '%' + @Barcode% + '%' Order by Size";
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Parameters.Add(new SqlParameter("@SpoolHID", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                        cmd2.Parameters.Add(new SqlParameter("@Barcode", SqlDbType.VarChar)).Value = sBarcode;

                        SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        adapter2.Fill(ds2);

                        for (int i = 0; i <= ds2.Tables[0].Rows.Count - 1; i++)
                        {
                            nSize = Convert.ToInt32(ds2.Tables[0].Rows[0]["Size"].ToString());

                            if (sSize1 == nSize.ToString()) nQuantity1 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize2 == nSize.ToString()) nQuantity2 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize3 == nSize.ToString()) nQuantity3 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize4 == nSize.ToString()) nQuantity4 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize5 == nSize.ToString()) nQuantity5 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize6 == nSize.ToString()) nQuantity6 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize7 == nSize.ToString()) nQuantity7 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize8 == nSize.ToString()) nQuantity8 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize9 == nSize.ToString()) nQuantity9 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize10 == nSize.ToString()) nQuantity10 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize11 == nSize.ToString()) nQuantity11 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize12 == nSize.ToString()) nQuantity12 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize13 == nSize.ToString()) nQuantity13 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize14 == nSize.ToString()) nQuantity14 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize15 == nSize.ToString()) nQuantity15 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize16 == nSize.ToString()) nQuantity16 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize17 == nSize.ToString()) nQuantity17 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if (sSize18 == nSize.ToString()) nQuantity18 = Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                        }
                    }
                }

                nTotalQuantity = nQuantity1 + nQuantity2 + nQuantity3 + nQuantity4 + nQuantity5 + nQuantity6 + nQuantity7 + nQuantity8 + nQuantity9 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18;

                if (nTotalQuantity != nOriginalBoxQuantity)
                {
                    //MessageBox.Show("Difference Quantity");
                }

                if (sgm.sProcess.ToUpper() == "PRODUCTION")
                {
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
                            stbBarcodeText = "";
                        }

                        else
                        {


                            if (ds1.Tables[0].Rows[0]["MouldCompletedQty"] == DBNull.Value)
                                nMouldCompletedQty = 0;
                            else
                                nMouldCompletedQty = Convert.ToInt32(ds1.Tables[0].Rows[0]["MouldCompletedQty"]);

                            if (ds1.Tables[0].Rows[0]["FINCompletedQty"] != DBNull.Value)
                                nFinishCompletedQty = Convert.ToInt32(ds1.Tables[0].Rows[0]["FINCompletedQty"]);
                            else
                                nFinishCompletedQty = 0;

                            if (sgm.sSection == "MOULD")
                            {
                                using (SqlConnection conn22 = new SqlConnection(sgm.connectionString))
                                {
                                    conn22.Open();
                                    SqlCommand cmd22 = new SqlCommand();
                                    cmd22.Connection = conn22;
                                    cmd22.CommandText = "Select * From JobcardDetail Where JobcardNo = @mJobcardNo";
                                    cmd22.CommandType = CommandType.Text;
                                    cmd22.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                    SqlDataAdapter adapter22 = new SqlDataAdapter(cmd22);
                                    DataSet ds22 = new DataSet();
                                    adapter22.Fill(ds22);
                                    cmd22.ExecuteNonQuery();

                                    if (ds22.Tables[0].Rows[0]["JobCardStatus"].ToString().ToUpper() == "PENDING")
                                    {
                                        SqlCommand cmd23 = new SqlCommand();
                                        cmd23.Connection = conn22;
                                        cmd23.CommandText = "Update JobcardDetail Set JobCardStatus = 'WIP' Where JobcardNo = @mJobcardNo";
                                        cmd23.CommandType = CommandType.Text;
                                        cmd23.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                        SqlDataAdapter adapter23 = new SqlDataAdapter(cmd23);
                                        DataSet ds23 = new DataSet();
                                        adapter23.Fill(ds23);
                                        cmd23.ExecuteNonQuery();
                                    }
                                }

                                if (ds.Tables[0].Rows[0]["MouldScanDate"] == DBNull.Value)
                                {
                                    using (SqlConnection conn2 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn2.Open();
                                        SqlCommand cmd2 = new SqlCommand();
                                        cmd2.Connection = conn2;
                                        cmd2.CommandText = "Update PackingDetail Set MouldScanDate=@MouldScanDate, WIPLocation = 'FINISH', MouldQuantity=@MouldQuantity, Status = 'IN STOCK', CartonCBM = @Version  Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                                        cmd2.CommandType = CommandType.Text;
                                        cmd2.Parameters.Add(new SqlParameter("@MouldScanDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                        cmd2.Parameters.Add(new SqlParameter("@MouldQuantity", SqlDbType.Int)).Value = nTotalQuantity;
                                        cmd2.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                        cmd2.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;
                                        cmd2.Parameters.Add(new SqlParameter("@Version", SqlDbType.Decimal)).Value = sgm.nVersion;

                                        SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                                        DataSet ds2 = new DataSet();
                                        adapter2.Fill(ds2);
                                        cmd2.ExecuteNonQuery();

                                        sStatus = "Successfully Updated";
                                        stbBarcodeText = "";


                                        InsertIntoAuditDatabase();
                                    }
                                }
                                else
                                {
                                    if (ds.Tables[0].Rows[0]["MouldQuantity"] == DBNull.Value)
                                    {
                                        using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                        {

                                            conn3.Open();
                                            SqlCommand cmd3 = new SqlCommand();
                                            cmd3.Connection = conn3;
                                            cmd3.CommandText = "Update PackingDetail Set MouldQuantity=@MouldQuantity, Status = 'IN STOCK', CartonCBM = @Version Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                                            cmd3.CommandType = CommandType.Text;
                                            cmd3.Parameters.Add(new SqlParameter("@MouldQuantity", SqlDbType.Int)).Value = nTotalQuantity;
                                            cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                            cmd3.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;
                                            cmd3.Parameters.Add(new SqlParameter("@Version", SqlDbType.Decimal)).Value = sgm.nVersion;

                                            SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                            DataSet ds3 = new DataSet();
                                            adapter3.Fill(ds3);
                                            cmd3.ExecuteNonQuery();

                                            sStatus = "Successfully Updated";
                                            stbBarcodeText = "";
                                            InsertIntoAuditDatabase();
                                        }
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]) < nPackQty)
                                    {
                                        using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                        {

                                            conn3.Open();
                                            SqlCommand cmd3 = new SqlCommand();
                                            cmd3.Connection = conn3;
                                            cmd3.CommandText = "Update PackingDetail Set MouldQuantity=@MouldQuantity, Status = 'IN STOCK', CartonCBM = @Version Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                                            cmd3.CommandType = CommandType.Text;
                                            cmd3.Parameters.Add(new SqlParameter("@MouldQuantity", SqlDbType.Int)).Value = Convert.ToInt32(ds.Tables[0].Rows[0]["MouldQuantity"]) + nTotalQuantity;
                                            cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                            cmd3.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;
                                            cmd3.Parameters.Add(new SqlParameter("@Version", SqlDbType.Decimal)).Value = sgm.nVersion;

                                            SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                            DataSet ds3 = new DataSet();
                                            adapter3.Fill(ds3);
                                            cmd3.ExecuteNonQuery();

                                            sStatus = "Successfully Updated";
                                            stbBarcodeText = "";
                                            InsertIntoAuditDatabase();
                                        }
                                    }
                                    else
                                    {
                                        sStatus = "Scanned Alerady";
                                        stbBarcodeText = "";
                                        return;
                                    }
                                }

                                nMouldCompletedQty = nMouldCompletedQty + nPackQty;
                                nMouldPendingQty = nJobcardQuantity - nMouldCompletedQty;

                                if (ds1.Tables[0].Rows[0]["MouldStartDate"] == DBNull.Value)
                                {
                                    using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn3.Open();
                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.Connection = conn3;
                                        cmd3.CommandText = "Update JobcardWIP Set MouldStartDate=@MouldStartDate Where JobcardNo=@JobcardNo";
                                        cmd3.CommandType = CommandType.Text;
                                        cmd3.Parameters.Add(new SqlParameter("@MouldStartDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                        cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                        SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                        DataSet ds3 = new DataSet();
                                        adapter3.Fill(ds3);
                                        cmd3.ExecuteNonQuery();
                                    }

                                    using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn3.Open();
                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.Connection = conn3;
                                        cmd3.CommandText = "Update PackingDetail Set ReadyToDispatch = '0', Status = 'IN STOCK', CartonCBM = @Version Where JobcardNo=@JobcardNo AND ReadyToDispatch Is Null";
                                        cmd3.CommandType = CommandType.Text;
                                        cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                        cmd3.Parameters.Add(new SqlParameter("@Version", SqlDbType.Decimal)).Value = sgm.nVersion;

                                        SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                        DataSet ds3 = new DataSet();
                                        adapter3.Fill(ds3);
                                        cmd3.ExecuteNonQuery();

                                    }
                                }

                                using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                {
                                    conn3.Open();
                                    SqlCommand cmd3 = new SqlCommand();
                                    cmd3.Connection = conn3;
                                    if (nMouldPendingQty == 0)
                                        cmd3.CommandText = "Update JobcardWIP Set MouldCompletedQty=@MouldCompletedQty, MouldPendingQty=@MouldPendingQty, MouldEndDate=@MouldEndDate  Where JobcardNo=@JobcardNo";
                                    else
                                        cmd3.CommandText = "Update JobcardWIP Set MouldCompletedQty=@MouldCompletedQty, MouldPendingQty=@MouldPendingQty Where JobcardNo=@JobcardNo";
                                    cmd3.CommandType = CommandType.Text;
                                    if (nMouldPendingQty == 0)
                                    {
                                        cmd3.Parameters.Add(new SqlParameter("@MouldEndDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                    }
                                    cmd3.Parameters.Add(new SqlParameter("@MouldCompletedQty", SqlDbType.Int)).Value = nMouldCompletedQty;
                                    cmd3.Parameters.Add(new SqlParameter("@MouldPendingQty", SqlDbType.Int)).Value = nMouldPendingQty;
                                    cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                    DataSet ds3 = new DataSet();
                                    adapter3.Fill(ds3);
                                    cmd3.ExecuteNonQuery();
                                }


                            }
                            else if (sgm.sSection == "FINISH")
                            {
                                if (ds.Tables[0].Rows[0]["MouldScanDate"] == DBNull.Value)
                                {
                                    sStatus = "MOULD PRODUCTION NOT DONE";
                                    stbBarcodeText = "";
                                }
                                else
                                {

                                    nFinishCompletedQty = nFinishCompletedQty + nPackQty;
                                    nFinishPendingQty = nJobcardQuantity - nFinishCompletedQty;

                                    if (ds1.Tables[0].Rows[0]["FINStartDate"] == DBNull.Value)
                                    {
                                        using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                        {
                                            conn3.Open();
                                            SqlCommand cmd3 = new SqlCommand();
                                            cmd3.Connection = conn3;
                                            cmd3.CommandText = "Update JobcardWIP Set FINStartDate=@FINStartDate Where JobcardNo=@JobcardNo";
                                            cmd3.CommandType = CommandType.Text;
                                            cmd3.Parameters.Add(new SqlParameter("@FINStartDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                            cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                            SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                            DataSet ds3 = new DataSet();
                                            adapter3.Fill(ds3);
                                            cmd3.ExecuteNonQuery();
                                        }
                                    }

                                    using (SqlConnection conn31 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn31.Open();
                                        SqlCommand cmd31 = new SqlCommand();
                                        cmd31.Connection = conn31;
                                        cmd31.CommandText = "Update PackingDetail Set FinishScanDate=@FinishScanDate, WIPLocation = 'PACKING', FinishedQuantity=@FinishedQuantity, Status = 'IN STOCK', CartonCBM = @Version Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                                        cmd31.CommandType = CommandType.Text;
                                        cmd31.Parameters.Add(new SqlParameter("@FinishScanDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                        cmd31.Parameters.Add(new SqlParameter("@FinishedQuantity", SqlDbType.Int)).Value = nTotalQuantity;
                                        cmd31.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                        cmd31.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;
                                        cmd31.Parameters.Add(new SqlParameter("@Version", SqlDbType.Decimal)).Value = sgm.nVersion;

                                        SqlDataAdapter adapter31 = new SqlDataAdapter(cmd31);
                                        DataSet ds31 = new DataSet();
                                        adapter31.Fill(ds31);
                                        cmd31.ExecuteNonQuery();

                                        sStatus = "Successfully Updated";
                                        stbBarcodeText = "";
                                        InsertIntoAuditDatabase();
                                    }

                                    using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn3.Open();
                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.Connection = conn3;
                                        if (nFinishPendingQty == 0)
                                            cmd3.CommandText = "Update JobcardWIP Set FinCompletedQty=@FinCompletedQty, FinPendingQty=@FinPendingQty, FINEndDate=@FINEndDate  Where JobcardNo=@JobcardNo";
                                        else
                                            cmd3.CommandText = "Update JobcardWIP Set FinCompletedQty=@FinCompletedQty, FinPendingQty=@FinPendingQty Where JobcardNo=@JobcardNo";
                                        cmd3.CommandType = CommandType.Text;
                                        if (nFinishPendingQty == 0)
                                        {
                                            cmd3.Parameters.Add(new SqlParameter("@FINEndDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                        }
                                        cmd3.Parameters.Add(new SqlParameter("@FinCompletedQty", SqlDbType.Int)).Value = nFinishCompletedQty;
                                        cmd3.Parameters.Add(new SqlParameter("@FinPendingQty", SqlDbType.Int)).Value = nFinishPendingQty;
                                        cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                        SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                        DataSet ds3 = new DataSet();
                                        adapter3.Fill(ds3);
                                        cmd3.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
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

        private void UpdateReadyToDispatch()
        {
            nLen = sBarcode.Trim().Length;

            sStatus = "";
            nStringLength = sBarcode.Trim().Length;

            nBoxNoLength = nStringLength - sJobcardNo.Length;
            nBoxNo = Convert.ToInt32(sBarcode.Trim().Substring(sJobcardNo.Length + 1));

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from PackingDetail Where ReadytoDispatch Not In ('0','1') And JobcardNo = @JobcardNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@JobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Update PackingDetail Set ReadytoDispatch = '0', Status = 'IN STOCK', CartonCBM = @Version Where JobcardNo = @JobcardNo";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add(new SqlParameter("@Version", SqlDbType.Decimal)).Value = sgm.nVersion;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);

                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from PackingDetail where JobcardNo = @JobCardNo And CartonNo = @CartonNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@JobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    sStatus = "This Carton Does not belong to this Packing Detail";
                }
                else if (ds.Tables[0].Rows[0]["WIPLocation"].ToString() != "PACKING")
                {
                    sStatus = "This Carton is Not Received at Packing Section";
                }
                else if (Convert.ToInt32(ds.Tables[0].Rows[0]["ReadytoDispatch"]) * -1 == 1)
                {
                    sStatus = "This Carton Already Included for Invoice Generation";
                }
                else
                {
                    UpdatePackingFromMD();

                }
            }
        }

        private void UpdatePackingFromMD()
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from PackingDetail where JobcardNo = @JobcardNo And CartonNo = @CartonNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@JobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    sStatus = "This Carton Does not belong to packing section";
                }
                else if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]) != nSavingQuantity)
                {
                    sStatus = "Others";
                }
                else
                {
                    if (sEntryUpdates == "Without Sizes")
                    {
                        nQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                        nQuantity01 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"]);
                        nQuantity02 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"]);
                        nQuantity03 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"]);
                        nQuantity04 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"]);
                        nQuantity05 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"]);
                        nQuantity06 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"]);
                        nQuantity07 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"]);
                        nQuantity08 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"]);
                        nQuantity09 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"]);
                        nQuantity10 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"]);
                        nQuantity11 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"]);
                        nQuantity12 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"]);
                        nQuantity13 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"]);
                        nQuantity14 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"]);
                        nQuantity15 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"]);
                        nQuantity16 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"]);
                        nQuantity17 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"]);
                        nQuantity18 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"]);


                        if (nQuantity01 > 0)
                            nQuantity01 = nSavingQuantity;
                        else if (nQuantity02 > 0)
                            nQuantity02 = nSavingQuantity;
                        else if (nQuantity03 > 0)
                            nQuantity03 = nSavingQuantity;
                        else if (nQuantity04 > 0)
                            nQuantity04 = nSavingQuantity;
                        else if (nQuantity05 > 0)
                            nQuantity05 = nSavingQuantity;
                        else if (nQuantity06 > 0)
                            nQuantity06 = nSavingQuantity;
                        else if (nQuantity07 > 0)
                            nQuantity07 = nSavingQuantity;
                        else if (nQuantity08 > 0)
                            nQuantity08 = nSavingQuantity;
                        else if (nQuantity09 > 0)
                            nQuantity09 = nSavingQuantity;
                        else if (nQuantity10 > 0)
                            nQuantity10 = nSavingQuantity;
                        else if (nQuantity11 > 0)
                            nQuantity11 = nSavingQuantity;
                        else if (nQuantity12 > 0)
                            nQuantity12 = nSavingQuantity;
                        else if (nQuantity13 > 0)
                            nQuantity13 = nSavingQuantity;
                        else if (nQuantity14 > 0)
                            nQuantity14 = nSavingQuantity;
                        else if (nQuantity15 > 0)
                            nQuantity15 = nSavingQuantity;
                        else if (nQuantity16 > 0)
                            nQuantity16 = nSavingQuantity;
                        else if (nQuantity17 > 0)
                            nQuantity17 = nSavingQuantity;
                        else if (nQuantity18 > 0)
                            nQuantity18 = nSavingQuantity;


                        nQuantity = nQuantity01 + nQuantity02 + nQuantity03 + nQuantity04 + nQuantity05 + nQuantity06 + nQuantity07 + nQuantity08 + nQuantity09 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18;

                    }
                    else
                    {
                    }

                    nTotalQuantity = nQuantity01 + nQuantity02 + nQuantity03 + nQuantity04 + nQuantity05 + nQuantity06 + nQuantity07 + nQuantity08 + nQuantity09 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18;

                    sFrom = "Mobile Computer";
                    JobcardVerification();



                    //InsertReadytoDispatch();
                    using (SqlConnection connSelR2D = new SqlConnection(sgm.connectionString))
                    {
                        object sRes = null;
                        connSelR2D.Open();
                        SqlCommand cmdSelR2D = new SqlCommand();
                        cmdSelR2D.Connection = connSelR2D;
                        cmdSelR2D.CommandText = "proc_PackingEntries";
                        cmdSelR2D.CommandType = CommandType.StoredProcedure;

                        cmdSelR2D.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "SELRDYTODIS";
                        cmdSelR2D.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                        SqlDataAdapter adapterSelR2D = new SqlDataAdapter(cmdSelR2D);
                        DataSet dsSelR2D = new DataSet();
                        adapterSelR2D.Fill(dsSelR2D);

                        if (dsSelR2D.Tables[0].Rows.Count == 0)
                        {
                            using (SqlConnection connInsR2D = new SqlConnection(sgm.connectionString))
                            {
                                connInsR2D.Open();
                                SqlCommand cmdInsR2D = new SqlCommand();
                                cmdInsR2D.Connection = connInsR2D;
                                cmdInsR2D.CommandText = "proc_PackingEntries";
                                cmdInsR2D.CommandType = CommandType.StoredProcedure;

                                cmdInsR2D.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSRDYTODIS";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();
                                cmdInsR2D.Parameters.Add(new SqlParameter("@minvoiceno", SqlDbType.VarChar)).Value = "";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mInvoiceSerialNo", SqlDbType.VarChar)).Value = "";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mbuyer", SqlDbType.VarChar)).Value = sBuyer;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mshipper", SqlDbType.VarChar)).Value = sShipper;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mArticleNo", SqlDbType.VarChar)).Value = sArticle;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mrate", SqlDbType.Decimal)).Value = 0;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mquantity", SqlDbType.Decimal)).Value = nQuantity;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value = sBuyerCode;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = "";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = "";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = sgm.strSystemName;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mPackNo", SqlDbType.VarChar)).Value = sPackNo;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = 0;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value = "";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value = DateTime.Now;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mModuleName", SqlDbType.VarChar)).Value = "Barcode Scanning";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value = sJobcardDetailID;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value = "";
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value = sSalesOrderDetailId;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize01", SqlDbType.VarChar)).Value = sSize01;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value = nQuantity01;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize02", SqlDbType.VarChar)).Value = sSize02;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value = nQuantity02;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize03", SqlDbType.VarChar)).Value = sSize03;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value = nQuantity03;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize04", SqlDbType.VarChar)).Value = sSize04;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value = nQuantity04;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize05", SqlDbType.VarChar)).Value = sSize05;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value = nQuantity05;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize06", SqlDbType.VarChar)).Value = sSize06;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value = nQuantity06;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize07", SqlDbType.VarChar)).Value = sSize07;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value = nQuantity07;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize08", SqlDbType.VarChar)).Value = sSize08;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value = nQuantity08;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize09", SqlDbType.VarChar)).Value = sSize09;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value = nQuantity09;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize10", SqlDbType.VarChar)).Value = sSize10;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value = nQuantity10;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize11", SqlDbType.VarChar)).Value = sSize11;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value = nQuantity11;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize12", SqlDbType.VarChar)).Value = sSize12;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value = nQuantity12;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize13", SqlDbType.VarChar)).Value = sSize13;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value = nQuantity13;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize14", SqlDbType.VarChar)).Value = sSize14;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value = nQuantity14;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize15", SqlDbType.VarChar)).Value = sSize15;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value = nQuantity15;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize16", SqlDbType.VarChar)).Value = sSize16;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value = nQuantity16;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize17", SqlDbType.VarChar)).Value = sSize17;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value = nQuantity17;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSize18", SqlDbType.VarChar)).Value = sSize18;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value = nQuantity18;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;
                                cmdInsR2D.Parameters.Add(new SqlParameter("@mSpoolDt", SqlDbType.DateTime)).Value = sgm.dSpoolDt;


                                sRes = cmdInsR2D.ExecuteNonQuery();
                                sStatus = "Successfully Updated";
                                //if ((int)sRes == 1)
                                //    return true;
                                //else
                                //    return false;
                            }
                        }
                        else
                        {
                            sID = ds.Tables[0].Rows[0]["ID"].ToString();
                            nProducedQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]) + Convert.ToInt32(nQuantity);

                            using (SqlConnection connUpdR2D = new SqlConnection(sgm.connectionString))
                            {
                                connUpdR2D.Open();
                                SqlCommand cmdUpdR2D = new SqlCommand();
                                cmdUpdR2D.Connection = connUpdR2D;
                                cmdUpdR2D.CommandText = "proc_PackingEntries";
                                cmdUpdR2D.CommandType = CommandType.StoredProcedure;

                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDRDYTODIS";
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sID;
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value = nQuantity01 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value = nQuantity02 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value = nQuantity03 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value = nQuantity04 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value = nQuantity05 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value = nQuantity06 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value = nQuantity07 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value = nQuantity08 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value = nQuantity09 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value = nQuantity10 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value = nQuantity11 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value = nQuantity12 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value = nQuantity13 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value = nQuantity14 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value = nQuantity15 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value = nQuantity16 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value = nQuantity17 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"]);
                                cmdUpdR2D.Parameters.Add(new SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value = nQuantity18 + Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"]);

                                sRes = cmdUpdR2D.ExecuteNonQuery();
                                sStatus = "Successfully Updated";
                                //if ((int)sRes == 1)
                                //    return true;
                                //else
                                //    return false;
                            }
                        }
                    }

                    using (SqlConnection connPKGDtl = new SqlConnection(sgm.connectionString))
                    {
                        connPKGDtl.Open();
                        SqlCommand cmdPKGDtl = new SqlCommand();
                        cmdPKGDtl.Connection = connPKGDtl;
                        cmdPKGDtl.CommandText = "proc_PackingEntries";
                        cmdPKGDtl.CommandType = CommandType.StoredProcedure;

                        cmdPKGDtl.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDPKGDTL";
                        cmdPKGDtl.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmdPKGDtl.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = nBoxNo;
                        cmdPKGDtl.Parameters.Add(new SqlParameter("@mReadyToDispatchDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmdPKGDtl.Parameters.Add(new SqlParameter("@mPackedQuantity", SqlDbType.Int)).Value = nTotalQuantity;

                        SqlDataAdapter adapterPKGDtl = new SqlDataAdapter(cmdPKGDtl);
                        DataSet dsPKGDtl = new DataSet();
                        adapterPKGDtl.Fill(dsPKGDtl);
                    }


                    using (SqlConnection connUpdR2d = new SqlConnection(sgm.connectionString))
                    {
                        connUpdR2d.Open();
                        SqlCommand cmdUpdR2d = new SqlCommand();
                        cmdUpdR2d.Connection = connUpdR2d;
                        cmdUpdR2d.CommandText = "proc_PackingEntries";
                        cmdUpdR2d.CommandType = CommandType.StoredProcedure;

                        if (sgm.sSelectedArticle == "SOL-LEA")
                            cmdUpdR2d.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPKGSUML";
                        else
                            cmdUpdR2d.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPKGSUM";

                        cmdUpdR2d.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                        SqlDataAdapter adapterUpdR2d = new SqlDataAdapter(cmdUpdR2d);
                        DataSet dsUpdR2d = new DataSet();
                        adapterUpdR2d.Fill(dsUpdR2d);

                        using (SqlConnection connUpdR2D1 = new SqlConnection(sgm.connectionString))
                        {
                            connUpdR2D1.Open();
                            SqlCommand cmdUpdR2d1 = new SqlCommand();
                            cmdUpdR2d1.Connection = connUpdR2D1;
                            cmdUpdR2d1.CommandText = "proc_PackingEntries";
                            cmdUpdR2d1.CommandType = CommandType.StoredProcedure;

                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDRDYTODISBYJC";
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mquantity", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["PkgStock"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mPackNo", SqlDbType.VarChar)).Value = dsUpdR2d.Tables[0].Rows[0]["NoofCarton"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity01"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity02"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity03"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity04"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity05"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity06"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity07"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity08"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity09"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity10"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity11"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity12"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity13"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity14"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity15"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity16"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity17"];
                            cmdUpdR2d1.Parameters.Add(new SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value = dsUpdR2d.Tables[0].Rows[0]["Quantity18"];

                            SqlDataAdapter adapterUpdR2d1 = new SqlDataAdapter(cmdUpdR2d1);
                            DataSet dsUpdR2d1 = new DataSet();
                            adapterUpdR2d1.Fill(dsUpdR2d1);
                            sStatus = "Successfully Updated";
                        }
                    }
                }
            }
        }

        private void UpdateProductionByProcess()
        {
            string sProductionId, sReworkType;

            if (sgm.sSection == "Mould")
            {
                sProductionId = "Production";
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(sgm.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from ProductStockRework where ToJobcardNo = @WorkOrderNo And ToStage = 'FINISHING'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar)).Value = sJobcardNo;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        sProductionId = "Production";
                    }
                    else
                    {
                        sReworkType = ds.Tables[0].Rows[0]["ReworkType"].ToString();

                        using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                        {
                            conn1.Open();
                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.Connection = conn1;
                            cmd1.CommandText = "Select * from ProductStockBoxDtls where ToJobcardNo = @WorkOrderNo And ToStage = 'FINISHING' And CartonNo = @CartonNo And WIPLocation = 'FINISH'";
                            cmd1.CommandType = CommandType.Text;
                            cmd1.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar)).Value = sJobcardNo;
                            cmd1.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            adapter1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count == 1)
                            {
                                sProductionId = sReworkType;
                            }
                            else
                            {
                                sProductionId = "Production";
                            }
                        }
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from ProductionByProcess where WorkOrderNo = @WorkOrderNo And ProductionId = @ProductionId And ProcessName = @ProcessName And ProcessDate = @ProcessDate And Size = @Size";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar)).Value = sJobcardNo;
                cmd.Parameters.Add(new SqlParameter("@ProcessName", SqlDbType.VarChar)).Value = sgm.sSection;
                cmd.Parameters.Add(new SqlParameter("@ProductionId", SqlDbType.VarChar)).Value = sProductionId;
                cmd.Parameters.Add(new SqlParameter("@ProcessDate", SqlDbType.DateTime)).Value = DateTime.Now;
                cmd.Parameters.Add(new SqlParameter("@Size", SqlDbType.VarChar)).Value = sgm.sSection;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Insert Into ProductionByProcess(ID,ProcessName,ProcessDate,ShiftNo,	MachineNo,	SalesOrderNo,	Article,	Size,	Pcs,	Quantity,	Unit,	Price,	Value,	CompanyCode,	WorkOrderNo,	LocationName,	BuyerCode,	Buyer,	CreatedDate,	ModifiedDate,	EnteredOnMachineID,	ModuleName,	JobCardDetailID,ProductionId,	Location,	FromLocation,	CurrentQuantity,	FromStage) values (@ID,	@ProcessName,	@ProcessDate,	@ShiftNo,	@MachineNo,	@SalesOrderNo,	@Article,	@Size,	@Pcs,	@Quantity,	@Unit,	@Price,	@Value,	@CompanyCode,	@WorkOrderNo,	@LocationName,	@BuyerCode,	@Buyer,	@CreatedDate,	@ModifiedDate,	@EnteredOnMachineID,	@ModuleName,	@JobCardDetailID,@ProductionId,	@Location,	@FromLocation,	@CurrentQuantity,	@FromStage)";
                        cmd1.CommandType = CommandType.Text;

                        cmd1.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = Guid.NewGuid().ToString();
                        cmd1.Parameters.Add(new SqlParameter("@ProcessName", SqlDbType.VarChar)).Value = sgm.sSection;
                        cmd1.Parameters.Add(new SqlParameter("@ProcessDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd1.Parameters.Add(new SqlParameter("@ShiftNo", SqlDbType.VarChar)).Value = "";
                        cmd1.Parameters.Add(new SqlParameter("@MachineNo", SqlDbType.VarChar)).Value = "";
                        cmd1.Parameters.Add(new SqlParameter("@SalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                        cmd1.Parameters.Add(new SqlParameter("@Article", SqlDbType.VarChar)).Value = sArticle;
                        cmd1.Parameters.Add(new SqlParameter("@Size", SqlDbType.VarChar)).Value = sSize;
                        cmd1.Parameters.Add(new SqlParameter("@Pcs", SqlDbType.Int)).Value = 0;
                        cmd1.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int)).Value = nPBPSavingQuantity;
                        cmd1.Parameters.Add(new SqlParameter("@Unit", SqlDbType.VarChar)).Value = "PRS";
                        cmd1.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal)).Value = 0;
                        cmd1.Parameters.Add(new SqlParameter("@Value", SqlDbType.Decimal)).Value = 0;
                        cmd1.Parameters.Add(new SqlParameter("@CompanyCode", SqlDbType.VarChar)).Value = "SSPL";
                        cmd1.Parameters.Add(new SqlParameter("@WorkOrderNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd1.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar)).Value = "";
                        cmd1.Parameters.Add(new SqlParameter("@BuyerCode", SqlDbType.VarChar)).Value = sBuyerCode;
                        cmd1.Parameters.Add(new SqlParameter("@Buyer", SqlDbType.VarChar)).Value = sBuyer;
                        cmd1.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd1.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd1.Parameters.Add(new SqlParameter("@EnteredOnMachineID", SqlDbType.VarChar)).Value = sgm.strIPAddress;
                        cmd1.Parameters.Add(new SqlParameter("@ModuleName", SqlDbType.VarChar)).Value = "Barcode Scanning";
                        cmd1.Parameters.Add(new SqlParameter("@JobCardDetailID", SqlDbType.VarChar)).Value = sJobcardDetailID;
                        cmd1.Parameters.Add(new SqlParameter("@ProductionId", SqlDbType.VarChar)).Value = sProductionId;
                        cmd1.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar)).Value = "";
                        cmd1.Parameters.Add(new SqlParameter("@FromLocation", SqlDbType.VarChar)).Value = "";
                        cmd1.Parameters.Add(new SqlParameter("@CurrentQuantity", SqlDbType.Int)).Value = nPBPSavingQuantity;
                        cmd1.Parameters.Add(new SqlParameter("@FromStage", SqlDbType.VarChar)).Value = "";


                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                    }
                }
                else
                {
                    SPBPID = ds.Tables[0].Rows[0]["ID"].ToString();
                    NPBPQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]) + nPBPSavingQuantity;
                    NPBPCurrentQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentQuantity"]) + +nPBPSavingQuantity;

                    using (SqlConnection conn1 = new SqlConnection(sgm.connectionString))
                    {
                        conn1.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn1;
                        cmd1.CommandText = "Update ProductionByProcess Set Quantity =  @Quantity, CurrentQuantity = @CurrentQuantity Where ID = @ID";
                        cmd1.CommandType = CommandType.Text;

                        cmd1.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = SPBPID;
                        cmd1.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int)).Value = NPBPQuantity;
                        cmd1.Parameters.Add(new SqlParameter("@CurrentQuantity", SqlDbType.Int)).Value = NPBPCurrentQuantity;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                    }
                }
            }
        }

        private void EVAProductionQuantityUpdates()
        {
            nBoxNoLength = nStringLength - sJobcardNo.Length;
            nBoxNo = Convert.ToInt32(sBarcode.Trim().Substring(sJobcardNo.Length + 1));

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
                    stbBarcodeText = "";
                    return;
                }

                nPackQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                nOriginalBoxQuantity = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);

                sSize1 = ds.Tables[0].Rows[0]["Size01"].ToString();
                sSize2 = ds.Tables[0].Rows[0]["Size02"].ToString();
                sSize3 = ds.Tables[0].Rows[0]["Size03"].ToString();
                sSize4 = ds.Tables[0].Rows[0]["Size04"].ToString();
                sSize5 = ds.Tables[0].Rows[0]["Size05"].ToString();
                sSize6 = ds.Tables[0].Rows[0]["Size06"].ToString();
                sSize7 = ds.Tables[0].Rows[0]["Size07"].ToString();
                sSize8 = ds.Tables[0].Rows[0]["Size08"].ToString();
                sSize9 = ds.Tables[0].Rows[0]["Size09"].ToString();
                sSize10 = ds.Tables[0].Rows[0]["Size10"].ToString();
                sSize11 = ds.Tables[0].Rows[0]["Size11"].ToString();
                sSize12 = ds.Tables[0].Rows[0]["Size12"].ToString();
                sSize13 = ds.Tables[0].Rows[0]["Size13"].ToString();
                sSize14 = ds.Tables[0].Rows[0]["Size14"].ToString();
                sSize15 = ds.Tables[0].Rows[0]["Size15"].ToString();
                sSize16 = ds.Tables[0].Rows[0]["Size16"].ToString();
                sSize17 = ds.Tables[0].Rows[0]["Size17"].ToString();
                sSize18 = ds.Tables[0].Rows[0]["Size18"].ToString();

                nQuantity1 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"].ToString());
                nQuantity2 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"].ToString());
                nQuantity3 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"].ToString());
                nQuantity4 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"].ToString());
                nQuantity5 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"].ToString());
                nQuantity6 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"].ToString());
                nQuantity7 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"].ToString());
                nQuantity8 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"].ToString());
                nQuantity9 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"].ToString());
                nQuantity10 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"].ToString());
                nQuantity11 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"].ToString());
                nQuantity12 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"].ToString());
                nQuantity13 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"].ToString());
                nQuantity14 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"].ToString());
                nQuantity15 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"].ToString());
                nQuantity16 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"].ToString());
                nQuantity17 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"].ToString());
                nQuantity18 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"].ToString());

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
                        case "MOULDING":
                            sgm.sSection = "MOULD";
                            break;

                        case "FINISHING":
                            sgm.sSection = "FINISH";
                            break;

                        case "PACKING":
                            sgm.sSection = "PACK";
                            break;

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
                                sStatus = "OTHERS";
                                if (sgm.sSection != "PACK")
                                {
                                    if (sArticle.Substring(0, 10) == "SOL-SYN-EV" || sArticle.Substring(0, 10) == "SOL-LEA-EV")
                                    {
                                        if (sEVAProcess == "")
                                        {
                                            sStatus = "Process Not Defined";
                                            return;
                                        }
                                        //EVAProductionQuantityUpdates();
                                    }
                                    else if (sArticle.Substring(9, 2) == "RU")
                                    {
                                        if (sEVAProcess == "")
                                        {
                                            sStatus = "Process Not Defined";
                                            return;
                                        }
                                        if (sEVAProcess == "M2P")
                                        {
                                            sStatus = "Process Not Defined";
                                            return;
                                        }
                                        //EVAProductionQuantityUpdates();
                                    }
                                    else
                                    {
                                        sStatus = "OTHERS";
                                        ProductionQuantityUpdates();
                                    }

                                    //New C# Coding Starts Here
                                    if (sStatus == "Successfully Updated")
                                    {
                                        nCorrectBox = nCorrectBox + 1;
                                        UpdateProductionByProcess();
                                    }
                                    else
                                        nWrongBox = nWrongBox + 1;

                                    nTotalBox = nTotalBox + 1;
                                }
                                else
                                {
                                    sStatus = "OTHERS";
                                    UpdateReadyToDispatch();

                                    if (sStatus == "Successfully Updated")
                                    {
                                        nCorrectBox = nCorrectBox + 1;
                                        UpdateProductionByProcess();
                                        InsertIntoAuditDatabase();
                                    }
                                    else
                                        nWrongBox = nWrongBox + 1;

                                    nTotalBox = nTotalBox + 1;
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
                                        //MessageBox.Show(sStatus);
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

            if (nWrongBox > 0)
            {
                plSave.Visible = false;
                plwrongBoxStatus.Visible = true;
                plwrongBoxStatus.BringToFront();

                tbTotalCartonScanned.Text = nTotalBox.ToString();
                tbCorrectBox.Text = nCorrectBox.ToString();
                tbWrongBox.Text = nWrongBox.ToString();

                lbWrongBox.DataSource = myccMCD.LoadWrongScannedBoxes(sSpoolId);
                lbWrongBox.DisplayMember = "ScannedBoxes";
                lbWrongBox.ValueMember = "ScannedBoxes";
            }
            else
            {
                FrmSelection frmSelection = new FrmSelection(sgm);
                this.Hide();
                frmSelection.ShowDialog();
                this.Close();
                this.Dispose();
            }


        }

        private void cbExitSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Spool where SpoolId=@sSpoolId";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@sSpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;

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

        private void cbSaveBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmscanning.ShowDialog();
            this.Close();
            this.Dispose();
        }

        private void cbExitSplDtls_Click(object sender, EventArgs e)
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

        private void cbBackSplDtls_Click(object sender, EventArgs e)
        {
            FrmSelection frmSelection = new FrmSelection(sgm);
            this.Hide();
            frmSelection.ShowDialog();
            this.Close();
            this.Dispose();
        }

        private void cbDeleteSpool_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from Spool where SpoolID=@SpoolId";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@SpoolId", SqlDbType.VarChar)).Value = sgm.sSpoolId;

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
                            }

                            FrmSelection frmSelection = new FrmSelection(sgm);
                            this.Hide();
                            frmSelection.ShowDialog();
                            this.Close();
                            this.Dispose();
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    FrmSelection frmSelection = new FrmSelection(sgm);
                    this.Hide();
                    frmSelection.ShowDialog();
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void InsertIntoAuditDatabase()
        {
            //    Dim daInsPkgDtlinAudit As New SqlDataAdapter("Insert Into PackingDetail SELECT ID, JobCardNo, PackingDate, BuyerGroupCode, BuyerCode, Shipper, InvoiceNo, ArticleGroup, Article, ColorCode, LeatherCode, CartonNo, Quantity, Unit, Weight, Size01, Quantity01, Size02, Quantity02, Size03, Quantity03, Size04, Quantity04, Size05, Quantity05, Size06, Quantity06, Size07, Quantity07, Size08, Quantity08, Size09, Quantity09, Size10, Quantity10, Size11, Quantity11, Size12, Quantity12, Size13, Quantity13, Size14, Quantity14, Size15, Quantity15, Size16, Quantity16, Size17, Quantity17, Size18, Quantity18, EnteredOnMachineID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Variant, CustomerStyleNo, ExeVersionNo, IsApproved, ApprovedBy, ApprovedOn, ModuleName, IsPacked, DCCartonNo, 'Modified', PackingNo, Location, PackingListNo, JobCardDetailsID, SalesOrderDetailID, AssortmentID, OrderNo, SalesOrderNo, InvoiceID, IsAssorted, MaterialCode, CartonCBM, BarCode, MouldScanDate, FinishScanDate, IsMouldUpdate, IsFinishUpdate, MtoFScanDate, FtoPScanDate, WIPLocation, ReadyToDispatch, ReadyToDispatchDate, InvYear, MouldQuantity, FinishedQuantity, PackedQuantity, InvoicedQuantity, PackingCartonNo,Remarks,Status FROM  AHGroup_SSPL.dbo.PackingDetail WHERE        (JobCardNo = '" & sJobcardNo & "') AND (CartonNo = '" & nBoxNo & "')", sConstrAudit)
            //Dim dsInsPkgDtlinAudit As New DataSet
            //daInsPkgDtlinAudit.Fill(dsInsPkgDtlinAudit)
            //dsInsPkgDtlinAudit.AcceptChanges()
            //using (SqlConnection conn = new SqlConnection("Data Source=SSPLSERVER;Initial Catalog=AHGroup_SSPL_Audit;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"))
            //{
            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conn;
            //    cmd.CommandText = "Insert Into PackingDetail SELECT ID, JobCardNo, PackingDate, BuyerGroupCode, BuyerCode, Shipper, InvoiceNo, ArticleGroup, Article, ColorCode, LeatherCode, CartonNo, Quantity, Unit, Weight, Size01, Quantity01, Size02, Quantity02, Size03, Quantity03, Size04, Quantity04, Size05, Quantity05, Size06, Quantity06, Size07, Quantity07, Size08, Quantity08, Size09, Quantity09, Size10, Quantity10, Size11, Quantity11, Size12, Quantity12, Size13, Quantity13, Size14, Quantity14, Size15, Quantity15, Size16, Quantity16, Size17, Quantity17, Size18, Quantity18, EnteredOnMachineID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Variant, CustomerStyleNo, ExeVersionNo, IsApproved, ApprovedBy, ApprovedOn, ModuleName, IsPacked, DCCartonNo, 'Modified', PackingNo, Location, PackingListNo, JobCardDetailsID, SalesOrderDetailID, AssortmentID, OrderNo, SalesOrderNo, InvoiceID, IsAssorted, MaterialCode, CartonCBM, BarCode, MouldScanDate, FinishScanDate, IsMouldUpdate, IsFinishUpdate, MtoFScanDate, FtoPScanDate, WIPLocation, ReadyToDispatch, ReadyToDispatchDate, InvYear, MouldQuantity, FinishedQuantity, PackedQuantity, InvoicedQuantity, PackingCartonNo,Remarks,Status FROM  AHGroup_SSPL.dbo.PackingDetail WHERE (JobCardNo = @mJobcardNo) AND (CartonNo = @mCartonNo)";
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
            //    cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = nBoxNo;

            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adapter.Fill(ds);
            //}
        }
        // Print Option //

        // Print Option //


    }


}