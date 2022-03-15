using OptimizerAddOn.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizerAddOn.ComponentClasses
{
    public partial class CCAssignedStock : Component
    {
        public CCAssignedStock()
        {
            InitializeComponent();
        }

        public CCAssignedStock(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public DataTable LoadAssignedMaterial()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELASSNSTK";
                    cmd.Parameters.Add(new SqlParameter("@mStoreCode", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    DataTable dt = new DataTable();
                    return dt;
                }
            }
        }

        public DataTable LoadJobcardDemand(string sFromJobcardNo, string sJobcardNo, string sMaterialFor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sMaterialFor == "Production")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "JOBCARDDEMAND";
                    else if (sMaterialFor == "Packing")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "JOBCARDDEMANDPKG";

                    cmd.Parameters.Add(new SqlParameter("@mStoreCode", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                    cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                    cmd.Parameters.Add(new SqlParameter("@mFromJobcardNo", SqlDbType.VarChar)).Value = sFromJobcardNo;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    DataTable dt = new DataTable();
                    return dt;
                }
            }
        }

        public DataTable LoadJobcardDemandForIssue(string sJobcardNo, string sMaterialFor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sMaterialFor == "Production")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "JOBCARDDEMAND4ISU";
                    else if (sMaterialFor == "Packing")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "JOBCARDDEMANDPKG4ISU";

                    cmd.Parameters.Add(new SqlParameter("@mStoreCode", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                    cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    DataTable dt = new DataTable();
                    return dt;
                }
            }
        }
        public DataTable LoadSalesOrderDemand(string sFromSalesOrderNo, string sToSalesOrderNo, string sMaterialFor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sMaterialFor == "Production")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SALESORDERDEMAND";
                    else if (sMaterialFor == "Packing")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SALESORDERDEMANDPKG";

                    cmd.Parameters.Add(new SqlParameter("@mStoreCode", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                    cmd.Parameters.Add(new SqlParameter("@mFromSalesOrderNo", SqlDbType.VarChar)).Value = sFromSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sToSalesOrderNo;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    DataTable dt = new DataTable();
                    return dt;
                }
            }
        }

    
        public bool StockTransaction(StrTransferData strTransferData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSTOCK";
                    cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = strTransferData.StockId;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (dt.Rows.Count > 0)
                    {
                        decimal dRate = Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        decimal dValue = strTransferData.RevisedStock * dRate;

                        int nTransferNo, nTransferSubSlNo;
                        if (ds.Tables[0].Rows[0]["TransferNo"].ToString() == "")
                        {
                            nTransferNo = 0;
                        }
                        else
                        {
                            nTransferNo = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferNo"]);
                        }

                        if (nTransferNo <= 0)
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = conn;
                            cmd2.CommandText = "SLI_AssignedStock";
                            cmd2.CommandType = CommandType.StoredProcedure;

                            cmd2.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELTRANSFERNO";

                            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                            DataSet ds2 = new DataSet();
                            adapter2.Fill(ds2);

                            nTransferNo = Convert.ToInt32(ds2.Tables[0].Rows[0]["TransferNo"]);
                        }

                        #region UPDATE EXISTING STOCK
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = conn;
                        cmd3.CommandText = "SLI_AssignedStock";
                        cmd3.CommandType = CommandType.StoredProcedure;

                        cmd3.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDSTOCK";

                        //cmd3.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                        //cmd3.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd3.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Decimal)).Value = strTransferData.RevisedStock;
                        cmd3.Parameters.Add(new SqlParameter("@mValue", SqlDbType.Decimal)).Value = dValue;
                        cmd3.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;
                        cmd3.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = strTransferData.StockId;
                        //cmd3.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Decimal)).Value = strTransferData.RevisedStock;
                        

                        SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                        DataSet ds3 = new DataSet();
                        adapter3.Fill(ds3);
                        #endregion

                        #region INSERT MATERIALISSUES TRANSFER

                        SqlCommand cmd6 = new SqlCommand();
                        cmd6.Connection = conn;
                        cmd6.CommandText = "SLI_AssignedStock";
                        cmd6.CommandType = CommandType.StoredProcedure;

                        cmd6.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELTRANSFERSSNO";
                        cmd6.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;

                        SqlDataAdapter adapter6 = new SqlDataAdapter(cmd6);
                        DataSet ds6 = new DataSet();
                        adapter6.Fill(ds6);

                        nTransferSubSlNo = Convert.ToInt32(ds6.Tables[0].Rows[0]["TransferNo"]);

                        SqlCommand cmd4 = new SqlCommand();
                        cmd4.Connection = conn;
                        cmd4.CommandText = "SLI_AssignedStock";
                        cmd4.CommandType = CommandType.StoredProcedure;

                        cmd4.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSERTTRANSFER";

                        cmd4.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mVoucherNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["GRN"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIssueDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIssueQuantity", SqlDbType.Decimal)).Value = strTransferData.IssueQuantity;
                        cmd4.Parameters.Add(new SqlParameter("@mPurchaseOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mTransactionType", SqlDbType.VarChar)).Value = "STOCK MATERIAL TRANSFER";
                        if (strTransferData.FromJobcardNo == "")
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }
                        else
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0,15);
                        }
                        cmd4.Parameters.Add(new SqlParameter("@mIssueUnits", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Unit"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIssuePcs", SqlDbType.Decimal)).Value = 0; //TODO For Leather
                        cmd4.Parameters.Add(new SqlParameter("@mIssuePrice", SqlDbType.Decimal)).Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        cmd4.Parameters.Add(new SqlParameter("@mIssueValue", SqlDbType.Decimal)).Value = strTransferData.IssueQuantity * Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        cmd4.Parameters.Add(new SqlParameter("@mCompanyCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mFromLocation", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                        cmd4.Parameters.Add(new SqlParameter("@mFromStage", SqlDbType.VarChar)).Value = "INSTK";
                        cmd4.Parameters.Add(new SqlParameter("@mToLocation", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                        cmd4.Parameters.Add(new SqlParameter("@mToStage", SqlDbType.VarChar)).Value = "INSTK";
                        cmd4.Parameters.Add(new SqlParameter("@mSupplierCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["SupplierCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mRemarks", SqlDbType.VarChar)).Value = MdlApp.sRemarks;
                        cmd4.Parameters.Add(new SqlParameter("@mMaterialTypeCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialTypeCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd4.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd4.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = MdlApp.sEnteredOnMachineID;
                        cmd4.Parameters.Add(new SqlParameter("@mSize", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Size"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CustWorkOrderNo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = 0;
                        cmd4.Parameters.Add(new SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value = "";
                        cmd4.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.Date)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mModuleName", SqlDbType.VarChar)).Value = "OptimizerAddOn";
                        cmd4.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;
                        cmd4.Parameters.Add(new SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["UnitCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mSource", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Source"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ComponentGroup"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mRID", SqlDbType.VarChar)).Value = "";
                        cmd4.Parameters.Add(new SqlParameter("@mSeason", SqlDbType.VarChar)).Value = "";
                        cmd4.Parameters.Add(new SqlParameter("@mCurrencyCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mExchangeRate", SqlDbType.Decimal)).Value = 0; //Convert.ToDecimal(ds.Tables[0].Rows[0][""].ToString());
                        cmd4.Parameters.Add(new SqlParameter("@mExcessQuantity", SqlDbType.Decimal)).Value = 0;
                        cmd4.Parameters.Add(new SqlParameter("@mStockID", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ID"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mCutterTicketNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mBENo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["BENo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mBEDate", SqlDbType.DateTime)).Value = ds.Tables[0].Rows[0]["BEDate"];
                        cmd4.Parameters.Add(new SqlParameter("@mBEAssessValue", SqlDbType.Decimal)).Value = 0;
                        cmd4.Parameters.Add(new SqlParameter("@mBEDutyRatePercentage", SqlDbType.Decimal)).Value = 0; //ds.Tables[0].Rows[0][""].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mBEMaterialCost", SqlDbType.Decimal)).Value = 0; //ds.Tables[0].Rows[0][""].ToString();
                        if (strTransferData.FromJobcardNo == "")
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mFromSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.FromSalesOrderNo;
                        }
                        else
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mFromSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo.ToString().Substring(0, 15);
                        }
                        cmd4.Parameters.Add(new SqlParameter("@mFromJobcardNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo;
                        cmd4.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;
                        cmd4.Parameters.Add(new SqlParameter("@mTransferSubSNo", SqlDbType.Int)).Value = nTransferSubSlNo;

                        SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);
                        DataSet ds4 = new DataSet();
                        adapter4.Fill(ds4);
                        #endregion

                        #region INSERT STOCK
                        SqlCommand cmd5 = new SqlCommand();
                        cmd5.Connection = conn;
                        cmd5.CommandText = "SLI_AssignedStock";
                        cmd5.CommandType = CommandType.StoredProcedure;

                        cmd5.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSERTSTOCK";

                        cmd5.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mLocation", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Location"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mStage", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Stage"].ToString();
                        //cmd5.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                        if (strTransferData.JobcardNo != "")
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0, 15);
                        }
                        else
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }

                        cmd5.Parameters.Add(new SqlParameter("@mArticle", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Article"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mVariant", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Variant"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ArticleGroup"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mArticleGroupCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ArticleGroupCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mPcs", SqlDbType.Decimal)).Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["Pcs"]);
                        cmd5.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Decimal)).Value = strTransferData.IssueQuantity;
                        cmd5.Parameters.Add(new SqlParameter("@mUnit", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Unit"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mPrice", SqlDbType.Decimal)).Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        cmd5.Parameters.Add(new SqlParameter("@mValue", SqlDbType.Decimal)).Value = strTransferData.IssueQuantity * dRate;
                        cmd5.Parameters.Add(new SqlParameter("@mCompanyCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mJobberCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["JobberCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mLotNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["LotNo"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mColor", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Color"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mRsNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["RsNo"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mPurchaseOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mMaterialColor", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialColor"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mSize", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Size"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mLocationName", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["LocationName"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mBuyerCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["BuyerCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mBuyer", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Buyer"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mSupplierCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["SupplierCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mBrandCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["BrandCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mSupplierMaterialCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["SupplierMaterialCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd5.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd5.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd5.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd5.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = MdlApp.sEnteredOnMachineID;
                        cmd5.Parameters.Add(new SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ExeVersionNo"].ToString();
                        if (ds.Tables[0].Rows[0]["IsApproved"].ToString() == "")
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = 0;
                        }
                        else
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApproved"]);
                        }

                        cmd5.Parameters.Add(new SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ApprovedBy"].ToString();
                        if (ds.Tables[0].Rows[0]["ApprovedOn"].ToString() == "")
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["ApprovedOn"]);
                        }


                        cmd5.Parameters.Add(new SqlParameter("@mModuleName", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ModuleName"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["UnitCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mMaterialTypeCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialTypeCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mGroupCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["GroupCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;
                        cmd5.Parameters.Add(new SqlParameter("@mOrigin", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Origin"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mSource", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Source"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mHideOrSide", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["HideOrSide"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mOwner", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Owner"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mMaterial", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Material"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mQuality", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Quality"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mBatchNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mOldLocation", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["OldLocation"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialSize"].ToString();

                        //cmd5.Parameters.Add(new SqlParameter("@mOldSalesOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["OldSalesOrderNo"].ToString();
                        //cmd5.Parameters.Add(new SqlParameter("@mOldJobCardNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["OldJobCardNo"].ToString();

                        if (strTransferData.JobcardNo != "")
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mOldSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo.ToString().Substring(0, 15);
                            cmd5.Parameters.Add(new SqlParameter("@mOldJobCardNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo.ToString();
                        }
                        else
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mOldSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.FromSalesOrderNo;
                            cmd5.Parameters.Add(new SqlParameter("@mOldJobCardNo", SqlDbType.VarChar)).Value = "";
                        }
                        cmd5.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ComponentGroup"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mLeatherCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["LeatherCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mArticleDetailID", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ArticleDetailID"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mStatus", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Status"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mCurrencyCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mCustWorkOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CustWorkOrderNo"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mSupplierGrade", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["SupplierGrade"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mGRN", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["GRN"].ToString();
                        cmd5.Parameters.Add(new SqlParameter("@mBENo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["BENo"].ToString();
                        if (ds.Tables[0].Rows[0]["BEDate"].ToString() == "")
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mBEDate", SqlDbType.DateTime)).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd5.Parameters.Add(new SqlParameter("@mBEDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["BEDate"]);
                        }
                        //cmd5.Parameters.Add(new SqlParameter("@mBEDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["BEDate"]);
                        cmd5.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;

                        
                        SqlDataAdapter adapter5 = new SqlDataAdapter(cmd5);
                        DataSet ds5 = new DataSet();
                        adapter5.Fill(ds5);
                        #endregion

                        if (strTransferData.JobcardNo != "")
                        {

                            #region UPDATE DEMAND BY JOBCARD

                            SqlCommand cmd7 = new SqlCommand();
                            cmd7.Connection = conn;
                            cmd7.CommandText = "SLI_AssignedStock";
                            cmd7.CommandType = CommandType.StoredProcedure;

                            cmd7.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCASSIGNEDSTK";
                            cmd7.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                            cmd7.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                            cmd7.Parameters.Add(new SqlParameter("@mPlaceofUse", SqlDbType.VarChar)).Value = strTransferData.PlaceofUse;
                            cmd7.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                            cmd7.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;

                            SqlDataAdapter adapter7 = new SqlDataAdapter(cmd7);
                            DataSet ds7 = new DataSet();
                            adapter7.Fill(ds7);

                            decimal dAssignedStock = Convert.ToDecimal(ds7.Tables[0].Rows[0]["AssignQuantity"]) + Convert.ToDecimal(strTransferData.IssueQuantity);


                            SqlCommand cmd8 = new SqlCommand();
                            cmd8.Connection = conn;
                            cmd8.CommandText = "SLI_AssignedStock";
                            cmd8.CommandType = CommandType.StoredProcedure;

                            cmd8.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDJCASSIGNEDSTK";
                            cmd8.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                            cmd8.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                            cmd8.Parameters.Add(new SqlParameter("@mPlaceofUse", SqlDbType.VarChar)).Value = strTransferData.PlaceofUse;
                            cmd8.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                            cmd8.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;
                            cmd8.Parameters.Add(new SqlParameter("@mAssignQuantity", SqlDbType.Decimal)).Value = dAssignedStock;

                            SqlDataAdapter adapter8 = new SqlDataAdapter(cmd8);
                            DataSet ds8 = new DataSet();
                            adapter8.Fill(ds8);

                            #region "REDUCE ASSIGNED STOCK FOR OLD JOBCARD"
                            SqlCommand cmd11 = new SqlCommand();
                            cmd11.Connection = conn;
                            cmd11.CommandText = "SLI_AssignedStock";
                            cmd11.CommandType = CommandType.StoredProcedure;

                            cmd11.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDJCASSIGNEDSTK";
                            cmd11.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                            cmd11.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                            cmd11.Parameters.Add(new SqlParameter("@mPlaceofUse", SqlDbType.VarChar)).Value = strTransferData.PlaceofUse;
                            cmd11.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                            cmd11.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo;
                            cmd11.Parameters.Add(new SqlParameter("@mAssignQuantity", SqlDbType.Decimal)).Value = strTransferData.RevisedAssignStock;

                            SqlDataAdapter adapter11 = new SqlDataAdapter(cmd11);
                            DataSet ds11 = new DataSet();
                            adapter11.Fill(ds11);
                            #endregion

                            #endregion

                        }

                        #region UPDATE DEMAND BY SALESORDER

                        SqlCommand cmd9 = new SqlCommand();
                        cmd9.Connection = conn;
                        cmd9.CommandText = "SLI_AssignedStock";
                        cmd9.CommandType = CommandType.StoredProcedure;

                        cmd9.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOASSIGNEDSTK";
                        cmd9.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                        cmd9.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                        cmd9.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                        if (strTransferData.JobcardNo != "")
                        {
                            cmd9.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0, 15);
                        }
                        else
                        {
                            cmd9.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }


                        SqlDataAdapter adapter9 = new SqlDataAdapter(cmd9);
                        DataSet ds9 = new DataSet();
                        adapter9.Fill(ds9);

                        decimal dSOAssignedStock = Convert.ToDecimal(ds9.Tables[0].Rows[0]["AssignQuantity"]) + Convert.ToDecimal(strTransferData.IssueQuantity);


                        SqlCommand cmd10 = new SqlCommand();
                        cmd10.Connection = conn;
                        cmd10.CommandText = "SLI_AssignedStock";
                        cmd10.CommandType = CommandType.StoredProcedure;

                        cmd10.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDSOASSIGNEDSTK";
                        cmd10.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                        cmd10.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                        cmd10.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                        cmd10.Parameters.Add(new SqlParameter("@mAssignQuantity", SqlDbType.Decimal)).Value = dSOAssignedStock;
                        //cmd10.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        if (strTransferData.JobcardNo != "")
                        {
                            cmd10.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0, 15);
                        }
                        else
                        {
                            cmd10.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }


                        SqlDataAdapter adapter10 = new SqlDataAdapter(cmd10);
                        DataSet ds10 = new DataSet();
                        adapter10.Fill(ds10);
                        #endregion

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    return false;

                }
            }
        }

        public bool StockIssue(StrTransferData strTransferData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSTOCK";
                    cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = strTransferData.StockId;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (dt.Rows.Count > 0)
                    {
                        decimal dRate = Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        decimal dValue = strTransferData.RevisedStock * dRate;

                        int nTransferNo, nTransferSubSlNo;
                        if (ds.Tables[0].Rows[0]["TransferNo"].ToString() == "")
                        {
                            nTransferNo = 0;
                        }
                        else
                        {
                            nTransferNo = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferNo"]);
                        }

                        if (nTransferNo <= 0)
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = conn;
                            cmd2.CommandText = "SLI_AssignedStock";
                            cmd2.CommandType = CommandType.StoredProcedure;

                            cmd2.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELTRANSFERNO";

                            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                            DataSet ds2 = new DataSet();
                            adapter2.Fill(ds2);

                            nTransferNo = Convert.ToInt32(ds2.Tables[0].Rows[0]["TransferNo"]);
                        }

                        #region UPDATE EXISTING STOCK
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = conn;
                        cmd3.CommandText = "SLI_AssignedStock";
                        cmd3.CommandType = CommandType.StoredProcedure;

                        cmd3.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDSTOCK";

                        //cmd3.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                        //cmd3.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                        cmd3.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Decimal)).Value = strTransferData.RevisedStock;
                        cmd3.Parameters.Add(new SqlParameter("@mValue", SqlDbType.Decimal)).Value = dValue;
                        cmd3.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;
                        cmd3.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = strTransferData.StockId;
                        cmd3.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd3.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd3.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = MdlApp.sEnteredOnMachineID;

                        SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                        DataSet ds3 = new DataSet();
                        adapter3.Fill(ds3);
                        #endregion

                        #region INSERT MATERIALISSUES TRANSFER

                        SqlCommand cmd6 = new SqlCommand();
                        cmd6.Connection = conn;
                        cmd6.CommandText = "SLI_AssignedStock";
                        cmd6.CommandType = CommandType.StoredProcedure;

                        cmd6.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELTRANSFERSSNO";
                        cmd6.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;

                        SqlDataAdapter adapter6 = new SqlDataAdapter(cmd6);
                        DataSet ds6 = new DataSet();
                        adapter6.Fill(ds6);

                        nTransferSubSlNo = Convert.ToInt32(ds6.Tables[0].Rows[0]["TransferNo"]);

                        SqlCommand cmd4 = new SqlCommand();
                        cmd4.Connection = conn;
                        cmd4.CommandText = "SLI_AssignedStock";
                        cmd4.CommandType = CommandType.StoredProcedure;

                        cmd4.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSERTTRANSFER";

                        cmd4.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mVoucherNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["GRN"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIssueDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIssueQuantity", SqlDbType.Decimal)).Value = strTransferData.IssueQuantity;
                        cmd4.Parameters.Add(new SqlParameter("@mPurchaseOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mTransactionType", SqlDbType.VarChar)).Value = "JOBCARD ISSUE";
                        if (strTransferData.FromJobcardNo == "")
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }
                        else
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0, 15);
                        }
                        cmd4.Parameters.Add(new SqlParameter("@mIssueUnits", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Unit"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIssuePcs", SqlDbType.Decimal)).Value = 0; //TODO For Leather
                        cmd4.Parameters.Add(new SqlParameter("@mIssuePrice", SqlDbType.Decimal)).Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        cmd4.Parameters.Add(new SqlParameter("@mIssueValue", SqlDbType.Decimal)).Value = strTransferData.IssueQuantity * Convert.ToDecimal(ds.Tables[0].Rows[0]["Price"]);
                        cmd4.Parameters.Add(new SqlParameter("@mCompanyCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mFromLocation", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                        cmd4.Parameters.Add(new SqlParameter("@mFromStage", SqlDbType.VarChar)).Value = "INSTK";
                        cmd4.Parameters.Add(new SqlParameter("@mToLocation", SqlDbType.VarChar)).Value = strTransferData.ToLocation;
                        cmd4.Parameters.Add(new SqlParameter("@mToStage", SqlDbType.VarChar)).Value = "WIP";
                        cmd4.Parameters.Add(new SqlParameter("@mSupplierCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["SupplierCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mRemarks", SqlDbType.VarChar)).Value = MdlApp.sRemarks;
                        cmd4.Parameters.Add(new SqlParameter("@mMaterialTypeCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["MaterialTypeCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd4.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = MdlApp.sUserName;
                        cmd4.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = MdlApp.sEnteredOnMachineID;
                        cmd4.Parameters.Add(new SqlParameter("@mSize", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Size"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CustWorkOrderNo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = 0;
                        cmd4.Parameters.Add(new SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value = "";
                        cmd4.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.Date)).Value = DateTime.Now;
                        cmd4.Parameters.Add(new SqlParameter("@mModuleName", SqlDbType.VarChar)).Value = "OptimizerAddOn";
                        cmd4.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;
                        cmd4.Parameters.Add(new SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["UnitCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mSource", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Source"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ComponentGroup"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mRID", SqlDbType.VarChar)).Value = "";
                        cmd4.Parameters.Add(new SqlParameter("@mSeason", SqlDbType.VarChar)).Value = "";
                        cmd4.Parameters.Add(new SqlParameter("@mCurrencyCode", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mExchangeRate", SqlDbType.Decimal)).Value = 0; //Convert.ToDecimal(ds.Tables[0].Rows[0][""].ToString());
                        cmd4.Parameters.Add(new SqlParameter("@mExcessQuantity", SqlDbType.Decimal)).Value = 0;
                        cmd4.Parameters.Add(new SqlParameter("@mStockID", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["ID"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mCutterTicketNo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mBENo", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["BENo"].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mBEDate", SqlDbType.DateTime)).Value = ds.Tables[0].Rows[0]["BEDate"];
                        cmd4.Parameters.Add(new SqlParameter("@mBEAssessValue", SqlDbType.Decimal)).Value = 0;
                        cmd4.Parameters.Add(new SqlParameter("@mBEDutyRatePercentage", SqlDbType.Decimal)).Value = 0; //ds.Tables[0].Rows[0][""].ToString();
                        cmd4.Parameters.Add(new SqlParameter("@mBEMaterialCost", SqlDbType.Decimal)).Value = 0; //ds.Tables[0].Rows[0][""].ToString();
                        if (strTransferData.FromJobcardNo == "")
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mFromSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.FromSalesOrderNo;
                        }
                        else
                        {
                            cmd4.Parameters.Add(new SqlParameter("@mFromSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo.ToString().Substring(0, 15);
                        }
                        cmd4.Parameters.Add(new SqlParameter("@mFromJobcardNo", SqlDbType.VarChar)).Value = strTransferData.FromJobcardNo;
                        cmd4.Parameters.Add(new SqlParameter("@mTransferNo", SqlDbType.Int)).Value = nTransferNo;
                        cmd4.Parameters.Add(new SqlParameter("@mTransferSubSNo", SqlDbType.Int)).Value = nTransferSubSlNo;

                        SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);
                        DataSet ds4 = new DataSet();
                        adapter4.Fill(ds4);
                        #endregion

                        if (strTransferData.JobcardNo != "")
                        {

                            #region UPDATE DEMAND BY JOBCARD

                            SqlCommand cmd7 = new SqlCommand();
                            cmd7.Connection = conn;
                            cmd7.CommandText = "SLI_AssignedStock";
                            cmd7.CommandType = CommandType.StoredProcedure;

                            cmd7.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCISSUEDSTK";
                            cmd7.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                            cmd7.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                            cmd7.Parameters.Add(new SqlParameter("@mPlaceofUse", SqlDbType.VarChar)).Value = strTransferData.PlaceofUse;
                            cmd7.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                            cmd7.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;

                            SqlDataAdapter adapter7 = new SqlDataAdapter(cmd7);
                            DataSet ds7 = new DataSet();
                            adapter7.Fill(ds7);

                            decimal dAssignedStock = Convert.ToDecimal(ds7.Tables[0].Rows[0]["IssueQuantity"]) + Convert.ToDecimal(strTransferData.IssueQuantity);


                            SqlCommand cmd8 = new SqlCommand();
                            cmd8.Connection = conn;
                            cmd8.CommandText = "SLI_AssignedStock";
                            cmd8.CommandType = CommandType.StoredProcedure;

                            cmd8.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDJCISSUEDSTK";
                            cmd8.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                            cmd8.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                            cmd8.Parameters.Add(new SqlParameter("@mPlaceofUse", SqlDbType.VarChar)).Value = strTransferData.PlaceofUse;
                            cmd8.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                            cmd8.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo;
                            cmd8.Parameters.Add(new SqlParameter("@mIssueQuantity", SqlDbType.Decimal)).Value = dAssignedStock;

                            SqlDataAdapter adapter8 = new SqlDataAdapter(cmd8);
                            DataSet ds8 = new DataSet();
                            adapter8.Fill(ds8);
                            #endregion

                        }

                        #region UPDATE DEMAND BY SALESORDER

                        SqlCommand cmd9 = new SqlCommand();
                        cmd9.Connection = conn;
                        cmd9.CommandText = "SLI_AssignedStock";
                        cmd9.CommandType = CommandType.StoredProcedure;

                        cmd9.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOISSUEDDSTK";
                        cmd9.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                        cmd9.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                        cmd9.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                        if (strTransferData.JobcardNo != "")
                        {
                            cmd9.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0, 15);
                        }
                        else
                        {
                            cmd9.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }


                        SqlDataAdapter adapter9 = new SqlDataAdapter(cmd9);
                        DataSet ds9 = new DataSet();
                        adapter9.Fill(ds9);

                        decimal dSOAssignedStock = Convert.ToDecimal(ds9.Tables[0].Rows[0]["IssueQuantity"]) + Convert.ToDecimal(strTransferData.IssueQuantity);


                        SqlCommand cmd10 = new SqlCommand();
                        cmd10.Connection = conn;
                        cmd10.CommandText = "SLI_AssignedStock";
                        cmd10.CommandType = CommandType.StoredProcedure;

                        cmd10.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDSOISSUEDSTK";
                        cmd10.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = strTransferData.MaterialCode;
                        cmd10.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = strTransferData.ComponentGroup;
                        cmd10.Parameters.Add(new SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value = strTransferData.MaterialSize;
                        cmd10.Parameters.Add(new SqlParameter("@mIssueQuantity", SqlDbType.Decimal)).Value = dSOAssignedStock;
                        //cmd10.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        if (strTransferData.JobcardNo != "")
                        {
                            cmd10.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.JobcardNo.ToString().Substring(0, 15);
                        }
                        else
                        {
                            cmd10.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strTransferData.SalesOrderNo;
                        }


                        SqlDataAdapter adapter10 = new SqlDataAdapter(cmd10);
                        DataSet ds10 = new DataSet();
                        adapter10.Fill(ds10);
                        #endregion

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    return false;

                }
            }
        }

        internal bool StockTransaction(object strTransferData)
        {
            throw new NotImplementedException();
        }

        public DataTable LoadStoresList(string sIPAddress)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSTORES";
                    cmd.Parameters.Add(new SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value = sIPAddress;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    adapter.Fill(ds);

                    return ds;
                }
            }
            catch (Exception Exp)
            {

                //HandleException(this Name, Exp);
                //throw Exp;
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
                DataTable dt = new DataTable();
                return dt;
            }

        }

        public DataTable LoadLocations(string sUnitCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_AssignedStock";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELLOCATION";
                    cmd.Parameters.Add(new SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value = sUnitCode;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    adapter.Fill(ds);

                    return ds;
                }
            }
            catch (Exception Exp)
            {

                //HandleException(this Name, Exp);
                //throw Exp;
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
                DataTable dt = new DataTable();
                return dt;
            }

        }
    }
}
