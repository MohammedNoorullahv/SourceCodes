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
    public partial class Save : Form
    {
        MCD myccMCD;
        DBConnect dbConnection;
        UserAuthentication myOptimizerStrUserAuthentication;
        Scanning scanning;

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
        int nFKStatus, nLen;
        string sPartQuantity, sWIPLocation, sFromLocation, sFromStage;

        public Save(SGM _sgm)
        {
            InitializeComponent();
            sgm = _sgm;
            lblSpoolId.Text = sgm.sSpoolId;
            myccMCD = new MCD(sgm.connectionString);
            dbConnection = new DBConnect(sgm);
            myOptimizerStrUserAuthentication = new UserAuthentication();
            scanning = new Scanning(sgm);
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
                    nJobcardQuantity = Convert.ToInt32( ds.Tables[0].Rows[0]["Quantity"]);
                    sArticle = ds.Tables[0].Rows[0]["Article"].ToString();
                    sBuyerCode = ds.Tables[0].Rows[0]["BuyerCode"].ToString();
                    sBuyer = ds.Tables[0].Rows[0]["BuyerGroupCode"].ToString();

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

                        sJobcardDetailID = ds1.Tables[0].Rows[0]["ID"].ToString();
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
                            nSize = Convert.ToInt32(ds2.Tables[0].Rows[i]["Size"].ToString());

                            if(sSize1 == nSize.ToString()) nQuantity1 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize2 == nSize.ToString()) nQuantity2 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize3 == nSize.ToString()) nQuantity3 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize4 == nSize.ToString()) nQuantity4 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize5 == nSize.ToString()) nQuantity5 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize6 == nSize.ToString()) nQuantity6 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize7 == nSize.ToString()) nQuantity7 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize8 == nSize.ToString()) nQuantity8 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize9 == nSize.ToString()) nQuantity9 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize10 == nSize.ToString()) nQuantity10 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize11 == nSize.ToString()) nQuantity11 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize12 == nSize.ToString()) nQuantity12 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize13 == nSize.ToString()) nQuantity13 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize14 == nSize.ToString()) nQuantity14 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize15 == nSize.ToString()) nQuantity15 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize16 == nSize.ToString()) nQuantity16 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize17 == nSize.ToString()) nQuantity17 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                            else if(sSize18 == nSize.ToString()) nQuantity18 =Convert.ToInt32(ds2.Tables[0].Rows[i]["SavingQuantity"]);
                        }
                    }
                }

                nTotalQuantity = nQuantity1 + nQuantity2 + nQuantity3 + nQuantity4 + nQuantity5 + nQuantity6 + nQuantity7 + nQuantity8 + nQuantity9 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18;

                if (sgm.sProcess == "Production")
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

                        nMouldCompletedQty = Convert.ToInt32(ds1.Tables[0].Rows[0]["MouldCompletedQty"]);
                        if (ds1.Tables[0].Rows[0]["FINCompletedQty"] != DBNull.Value)
                            nFinishCompletedQty = Convert.ToInt32(ds1.Tables[0].Rows[0]["FINCompletedQty"]);
                        else
                            nFinishCompletedQty = 0;

                        if (sgm.sSection == "MOULD")
                        {
                            if(ds.Tables[0].Rows[0]["MouldScanDate"] == DBNull.Value)
                            {
                                using (SqlConnection conn2 = new SqlConnection(sgm.connectionString))
                                {
                                    conn2.Open();
                                    SqlCommand cmd2 = new SqlCommand();
                                    cmd2.Connection = conn2;
                                    cmd2.CommandText = "Update PackingDetail Set MouldScanDate=@MouldScanDate, WIPLocation = 'MOULD', MouldQuantity=@MouldQuantity Where JobcardNo=@JobcardNo AND CartonNo=@CartonNo";
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.Parameters.Add(new SqlParameter("@MouldScanDate", SqlDbType.DateTime)).Value = DateTime.Now;
                                    cmd2.Parameters.Add(new SqlParameter("@MouldQuantity", SqlDbType.Int)).Value = nTotalQuantity;
                                    cmd2.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                                    cmd2.Parameters.Add(new SqlParameter("@CartonNo", SqlDbType.Int)).Value = nBoxNo;

                                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                                    DataSet ds2 = new DataSet();
                                    adapter2.Fill(ds2);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                sStatus = "Scanned Already";
                                stbBarcodeText = "";
                                return;
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
                                    cmd3.CommandText = "Update PackingDetail Set ReadyToDispatch = '0' Where JobcardNo=@JobcardNo AND ReadyToDispatch Is Null";
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

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
                                return;
                            }

                            //========================================
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

                                //using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                //{
                                //    conn.Open();
                                //    SqlCommand cmd3 = new SqlCommand();
                                //    cmd3.Connection = conn3;
                                //    cmd3.CommandText = "Update PackingDetail Set ReadyToDispatch = '0' Where JobcardNo=@JobcardNo AND ReadyToDispatch Is Null";
                                //    cmd3.CommandType = CommandType.Text;
                                //    cmd3.Parameters.Add(new SqlParameter("@JobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                                //    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                                //    DataSet ds3 = new DataSet();
                                //    adapter3.Fill(ds3);
                                //    cmd3.ExecuteNonQuery();
                                //}
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

        

        private void cbSaveAll_Click_1(object sender, EventArgs e)
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

                                BarcodeSettings();

                                JobcardVerification();

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
                                        //EVAProductionQuantityUpdates();
                                    }
                                    else
                                    {
                                        ProductionQuantityUpdates();
                                    }

                                    //New C# Coding Starts Here
                                    if (sStatus == "Successfully Updated")
                                        nCorrectBox = nCorrectBox + 1;
                                    else
                                        nWrongBox = nWrongBox + 1;

                                    nTotalBox = nTotalBox + 1;
                                    
                                }
                                else
                                {
                                    //UpdateReadyToDispatch();
                                    
                                    if (sStatus == "Successfully Updated")
                                        nCorrectBox = nCorrectBox + 1;
                                    else
                                        nWrongBox = nWrongBox + 1;

                                    nTotalBox = nTotalBox + 1;

                                }

                                using (SqlConnection conn3 = new SqlConnection(sgm.connectionString))
                                { 
                                    conn3.Open();
                                    SqlCommand cmd3 = new SqlCommand();
                                    cmd3.Connection = conn3;
                                    cmd3.CommandText = "Select * from AbbrevTable Where Group = 'scanstatus' And FullName_ = @Fullname";
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
                                        MessageBox.Show(sStatus);
                                        nFKStatus = 99;
                                    }

                                    using (SqlConnection conn4 = new SqlConnection(sgm.connectionString))
                                    {
                                        conn4.Open();
                                        SqlCommand cmd4 = new SqlCommand();
                                        cmd4.Connection = conn4;
                                        if (sEntryUpdates == "Without Sizes")
                                        {
                                            cmd.CommandText = "Update SpoolDetails Set IsUpdated = '1', UpdatedOn = @UpdatedOn, FKStatus = @FKStatus Where ID =  @ID";
                                        }
                                        else
                                        {
                                            cmd.CommandText = "Update SpoolDetails Set IsUpdated = '1', UpdatedOn = @UpdatedOn, FKStatus = @FKStatus Where SpoolHID = @SpoolHID And Barcode Like @Barcode + '%'";
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
                                    //New C# Coding Ends Here
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

        private void cbExitSave_Click_1(object sender, EventArgs e)
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

        private void cbSaveBack_Click_1(object sender, EventArgs e)
        {
            scanning.ShowDialog();
        }
    }
}