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
    public partial class CCPacking : Component
    {
        public CCPacking()
        {
            InitializeComponent();
        }

        public CCPacking(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public bool InsertPacking(StrPacking strPacking)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Packing";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSPACKING";
                    cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = strPacking.ID;
                    cmd.Parameters.Add(new SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value = strPacking.JobCardNo;
                    cmd.Parameters.Add(new SqlParameter("@mShipper", SqlDbType.VarChar)).Value = strPacking.Shipper;
                    cmd.Parameters.Add(new SqlParameter("@mPackingDate", SqlDbType.DateTime)).Value = strPacking.PackingDate;
                    cmd.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = strPacking.BuyerGroupCode;
                    cmd.Parameters.Add(new SqlParameter("@mBuyerCode", SqlDbType.VarChar)).Value = strPacking.BuyerCode;
                    cmd.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Decimal)).Value = strPacking.Quantity;
                    cmd.Parameters.Add(new SqlParameter("@mNetWt", SqlDbType.Decimal)).Value = strPacking.NetWt;
                    cmd.Parameters.Add(new SqlParameter("@mGrossWt", SqlDbType.Decimal)).Value = strPacking.GrossWt;
                    cmd.Parameters.Add(new SqlParameter("@mFromCarton", SqlDbType.Decimal)).Value = strPacking.FromCarton;
                    cmd.Parameters.Add(new SqlParameter("@mToCarton", SqlDbType.Decimal)).Value = strPacking.ToCarton;
                    cmd.Parameters.Add(new SqlParameter("@mPerBoxPackingQty", SqlDbType.Decimal)).Value = strPacking.PerBoxPackingQty;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark", SqlDbType.VarChar)).Value = strPacking.CartonMark;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark2", SqlDbType.VarChar)).Value = strPacking.CartonMark2;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark3", SqlDbType.VarChar)).Value = strPacking.CartonMark3;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark4", SqlDbType.VarChar)).Value = strPacking.CartonMark4;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark5", SqlDbType.VarChar)).Value = strPacking.CartonMark5;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark6", SqlDbType.VarChar)).Value = strPacking.CartonMark6;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark7", SqlDbType.VarChar)).Value = strPacking.CartonMark7;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark8", SqlDbType.VarChar)).Value = strPacking.CartonMark8;
                    cmd.Parameters.Add(new SqlParameter("@mCartonMark9", SqlDbType.VarChar)).Value = strPacking.CartonMark9;
                    cmd.Parameters.Add(new SqlParameter("@mTotalCarton", SqlDbType.Decimal)).Value = strPacking.TotalCarton;
                    cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = strPacking.InvoiceNo;
                    cmd.Parameters.Add(new SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value = strPacking.EnteredOnMachineID;
                    cmd.Parameters.Add(new SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = strPacking.CreatedBy;
                    cmd.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = strPacking.CreatedDate;
                    cmd.Parameters.Add(new SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = strPacking.ModifiedBy;
                    cmd.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = strPacking.ModifiedDate;
                    cmd.Parameters.Add(new SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value = strPacking.ExeVersionNo;
                    cmd.Parameters.Add(new SqlParameter("@mIsApproved", SqlDbType.Bit)).Value = strPacking.IsApproved;
                    cmd.Parameters.Add(new SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value = strPacking.ApprovedBy;
                    //cmd.Parameters.Add(new SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value = strPacking.ApprovedOn;
                    cmd.Parameters.Add(new SqlParameter("@mModuleName", SqlDbType.VarChar)).Value = strPacking.ModuleName;
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = strPacking.SalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value = strPacking.JobCardDetailID;
                    cmd.Parameters.Add(new SqlParameter("@mArticleName", SqlDbType.VarChar)).Value = strPacking.ArticleName;
                    cmd.Parameters.Add(new SqlParameter("@mArticleColor", SqlDbType.VarChar)).Value = strPacking.ArticleColor;
                    cmd.Parameters.Add(new SqlParameter("@mStatus", SqlDbType.VarChar)).Value = strPacking.Status;
                    cmd.Parameters.Add(new SqlParameter("@mPackingListNo", SqlDbType.VarChar)).Value = strPacking.PackingListNo;
                    cmd.Parameters.Add(new SqlParameter("@mFinalPerBoxQty", SqlDbType.Int)).Value = strPacking.FinalPerBoxQty;
                    cmd.Parameters.Add(new SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value = strPacking.OrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mTypeOfPacking", SqlDbType.VarChar)).Value = strPacking.TypeOfPacking;
                    cmd.Parameters.Add(new SqlParameter("@mCustWorkorderNo", SqlDbType.VarChar)).Value = strPacking.CustWorkorderNo;
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value = strPacking.SalesOrderDetailID;
                    cmd.Parameters.Add(new SqlParameter("@mModeOfPacking", SqlDbType.VarChar)).Value = strPacking.ModeOfPacking;


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    return true;
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

        public bool InsertOuterCartonPackedInfo(StrOuterCartonPackedInfo strOuterCartonPackedInfo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Packing";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELOCPKDINFO";
                    cmd.Parameters.Add(new SqlParameter("@mJobcardno", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.JobcardNo;
                    cmd.Parameters.Add(new SqlParameter("@mBoxSlNo", SqlDbType.Int)).Value = strOuterCartonPackedInfo.BoxSlNo;
                    cmd.Parameters.Add(new SqlParameter("@mInnerBoxNo", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.InnerBoxNo;
                    cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.CartonNo;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn;
                        cmd1.CommandText = "SLI_Packing";
                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSOCPKDINFO";
                        cmd1.Parameters.Add(new SqlParameter("@mJobcardno", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.JobcardNo;
                        cmd1.Parameters.Add(new SqlParameter("@mBoxSlNo", SqlDbType.Int)).Value = strOuterCartonPackedInfo.BoxSlNo;
                        cmd1.Parameters.Add(new SqlParameter("@mInnerBoxNo", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.InnerBoxNo;
                        cmd1.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.CartonNo;
                        cmd1.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Int)).Value = strOuterCartonPackedInfo.Quantity;
                        cmd1.Parameters.Add(new SqlParameter("@mSize", SqlDbType.Int)).Value = strOuterCartonPackedInfo.Size;
                        cmd1.Parameters.Add(new SqlParameter("@mPackedOn", SqlDbType.DateTime)).Value = strOuterCartonPackedInfo.PackedOn;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = conn;
                        cmd1.CommandText = "SLI_Packing";
                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDOCPKDINFO";
                        cmd1.Parameters.Add(new SqlParameter("@mJobcardno", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.JobcardNo;
                        cmd1.Parameters.Add(new SqlParameter("@mBoxSlNo", SqlDbType.Int)).Value = strOuterCartonPackedInfo.BoxSlNo;
                        cmd1.Parameters.Add(new SqlParameter("@mInnerBoxNo", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.InnerBoxNo;
                        cmd1.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.VarChar)).Value = strOuterCartonPackedInfo.CartonNo;
                        cmd1.Parameters.Add(new SqlParameter("@mQuantity", SqlDbType.Int)).Value = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]) + strOuterCartonPackedInfo.Quantity;
                        cmd1.Parameters.Add(new SqlParameter("@mSize", SqlDbType.Int)).Value = strOuterCartonPackedInfo.Size;
                        cmd1.Parameters.Add(new SqlParameter("@mPackedOn", SqlDbType.DateTime)).Value = strOuterCartonPackedInfo.PackedOn;

                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        adapter1.Fill(ds1);
                    }
                    

                    return true;
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

        public DataTable LoadPackingStatus(string sSalesOrderDetailsId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Packing";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPACKINGSTS";
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value = sSalesOrderDetailsId;

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
    }
}
