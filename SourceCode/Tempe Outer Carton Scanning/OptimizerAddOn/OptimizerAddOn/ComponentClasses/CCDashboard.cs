using OptimizerAddOn.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OptimizerAddOn.ComponentClasses
{
    public partial class CCDashboard : Component
    {

        #region DECLARATION
        string sSeason, sBuyerGroupCode, sBuyerBuy, sArticleGroup, sArticle, sBuyerArticle, sSalesOrderID;
        string sColor, sCustomerOrderNo, sWorkOrderNo, sSalesOrderDetailsID, sSalesOrderNo, sId;
        int nOrderQuantity;

        int nTotalCutting, nCuttingBal, nUpperOutput, nUpperOutputBal, nUpperDispatch, nUpperDispatchBal, nFSCharging;
        int nFSChargingBal, nFSOutput, nFSOutputBal, nShippedQuantity, nShippedQuantityBal;

        #endregion
        public CCDashboard()
        {
            InitializeComponent();
        }

        public CCDashboard(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public Boolean UpdateProduction()
        {
            try
            {
                FrmOrderOutstanding myForm = new FrmOrderOutstanding();
                ClearAll();
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmdProd = new SqlCommand();
                    cmdProd.Connection = conn;
                    cmdProd.CommandText = "SLI_Dashboard";
                    cmdProd.CommandType = CommandType.StoredProcedure;

                    cmdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPROD";

                    SqlDataAdapter adapterProd = new SqlDataAdapter(cmdProd);
                    DataTable dtProd = new DataTable();
                    adapterProd.Fill(dtProd);

                    DataSet dsProd = new DataSet();
                    adapterProd.Fill(dsProd);

                    int i = 0;
                    for (i = 0; i < dtProd.Rows.Count; i++)
                    {

                        //myForm.SetProgress(i, dtProd.Rows.Count);

                        sWorkOrderNo = dsProd.Tables[0].Rows[i]["WorkOrderNo"].ToString();
                        sSalesOrderNo = sWorkOrderNo.ToString().Substring(0, 19);
                        sId = dsProd.Tables[0].Rows[i]["Id"].ToString();

                        SqlCommand cmdChkOS = new SqlCommand();
                        cmdChkOS.Connection = conn;
                        cmdChkOS.CommandText = "SLI_Dashboard";
                        cmdChkOS.CommandType = CommandType.StoredProcedure;

                        cmdChkOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "CHECKOS";
                        cmdChkOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                        SqlDataAdapter adapterChkOS = new SqlDataAdapter(cmdChkOS);
                        DataTable dtChkOS = new DataTable();
                        adapterChkOS.Fill(dtChkOS);

                        DataSet dsChkOS = new DataSet();
                        adapterChkOS.Fill(dsChkOS);
                        if (dtChkOS.Rows.Count == 0)
                        {
                            SqlCommand cmdSelJCD = new SqlCommand();
                            cmdSelJCD.Connection = conn;
                            cmdSelJCD.CommandText = "SLI_Dashboard";
                            cmdSelJCD.CommandType = CommandType.StoredProcedure;

                            cmdSelJCD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCD";
                            cmdSelJCD.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sWorkOrderNo;

                            SqlDataAdapter adapterSelJCD = new SqlDataAdapter(cmdSelJCD);
                            DataTable dtSelJCD = new DataTable();
                            adapterSelJCD.Fill(dtSelJCD);

                            DataSet dsSelJCD = new DataSet();
                            adapterSelJCD.Fill(dsSelJCD);
                            if (dtSelJCD.Rows.Count > 0)
                                sSalesOrderDetailsID = dsSelJCD.Tables[0].Rows[0]["SalesOrderDetailID"].ToString();
                            else
                            {
                                sSalesOrderDetailsID = "";
                                MessageBox.Show("Assortment Job Card");
                            }

                            SqlCommand cmdSelSOD = new SqlCommand();
                            cmdSelSOD.Connection = conn;
                            cmdSelSOD.CommandText = "SLI_Dashboard";
                            cmdSelSOD.CommandType = CommandType.StoredProcedure;

                            cmdSelSOD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOD";
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                            SqlDataAdapter adapterSelSOD = new SqlDataAdapter(cmdSelSOD);
                            DataTable dtSelSOD = new DataTable();
                            adapterSelSOD.Fill(dtSelSOD);

                            DataSet dsSelSOD = new DataSet();
                            adapterSelSOD.Fill(dsSelSOD);
                            if (dtSelSOD.Rows.Count > 0)
                            {
                                sSeason = dsSelSOD.Tables[0].Rows[0]["season"].ToString();
                                sBuyerGroupCode = dsSelSOD.Tables[0].Rows[0]["BuyerGroupCode"].ToString();
                                sBuyerBuy = dsSelSOD.Tables[0].Rows[0]["BuyerBuy"].ToString();
                                sArticleGroup = dsSelSOD.Tables[0].Rows[0]["ArticleGroup"].ToString();
                                sArticle = dsSelSOD.Tables[0].Rows[0]["Article"].ToString();
                                sBuyerArticle = dsSelSOD.Tables[0].Rows[0]["BuyerArticle"].ToString();
                                sColor = dsSelSOD.Tables[0].Rows[0]["ColorName"].ToString();
                                sCustomerOrderNo = dsSelSOD.Tables[0].Rows[0]["CustomerOrderNo"].ToString();
                                nOrderQuantity = Convert.ToInt32(dsSelSOD.Tables[0].Rows[0]["OrderQuantity"]);
                                //sSalesOrderID = dsSelSOD.Tables[0].Rows[0]["ID"].ToString();

                                ClearQuantity();
                                string sProcessName = dsProd.Tables[0].Rows[i]["ProcessName"].ToString();
                                int nProdQuantity = Convert.ToInt32(dsProd.Tables[0].Rows[i]["Quantity"]);

                                switch (sProcessName)
                                {
                                    case "ULC":
                                        nTotalCutting = nProdQuantity;
                                        nCuttingBal = nOrderQuantity - nTotalCutting; break;
                                    case "COU":
                                        nUpperOutput = nProdQuantity;
                                        nUpperOutputBal = nOrderQuantity - nUpperOutput; break;
                                    case "KITTING":
                                        nFSCharging = nProdQuantity;
                                        nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                    case "KITTINGC":
                                        nFSCharging = nProdQuantity;
                                        nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                    case "CONVOUT":
                                        nFSOutput = nProdQuantity;
                                        nFSOutputBal = nOrderQuantity - nFSOutput; break;
                                    case "CONVOUTC":
                                        nFSOutput = nProdQuantity;
                                        nFSOutputBal = nOrderQuantity - nFSOutput; break;
                                }

                                SqlCommand cmdInsOS = new SqlCommand();
                                cmdInsOS.Connection = conn;
                                cmdInsOS.CommandText = "SLI_Dashboard";
                                cmdInsOS.CommandType = CommandType.StoredProcedure;

                                cmdInsOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "InsOS";
                                cmdInsOS.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSeason", SqlDbType.VarChar)).Value = sSeason;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = sBuyerGroupCode;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerBuy", SqlDbType.VarChar)).Value = sBuyerBuy;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value = sArticleGroup;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticle", SqlDbType.VarChar)).Value = sArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerArticle", SqlDbType.VarChar)).Value = sBuyerArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mColor", SqlDbType.VarChar)).Value = sColor;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = sWorkOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderDetailsID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mOrderQuantity", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mTotalCutting", SqlDbType.Int)).Value = nTotalCutting;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCuttingBal", SqlDbType.Int)).Value = nCuttingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutput", SqlDbType.Int)).Value = nUpperOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutputBal", SqlDbType.Int)).Value = nUpperOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = 0;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSCharging", SqlDbType.Int)).Value = nFSCharging;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSChargingBal", SqlDbType.Int)).Value = nFSChargingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutput", SqlDbType.Int)).Value = nFSOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutputBal", SqlDbType.Int)).Value = nFSOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = 0;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                                SqlDataAdapter adapterInsOS = new SqlDataAdapter(cmdInsOS);
                                DataTable dtInsOS = new DataTable();
                                adapterInsOS.Fill(dtInsOS);
                            }
                        }
                        else
                        {
                            ClearQuantity();
                            string sOSId = dsChkOS.Tables[0].Rows[0]["Id"].ToString();
                            nTotalCutting = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["TotalCutting"]);
                            nCuttingBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["CuttingBal"]);
                            nUpperOutput = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperOutput"]);
                            nUpperOutputBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperOutputBal"]);
                            nUpperDispatch = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatch"]);
                            nUpperDispatchBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatchBal"]);
                            nFSCharging = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSCharging"]);
                            nFSChargingBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSChargingBal"]);
                            nFSOutput = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSOutput"]);
                            nFSOutputBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSOutputBal"]);
                            nShippedQuantity = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["ShippedQuantity"]);
                            nShippedQuantityBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["ShippedQuantityBal"]);
                            nOrderQuantity = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["OrderQuantity"]);

                            string sPBPId = dsProd.Tables[0].Rows[i]["ID"].ToString();
                            string sProcessName = dsProd.Tables[0].Rows[i]["ProcessName"].ToString();
                            int nProdQuantity = Convert.ToInt32(dsProd.Tables[0].Rows[i]["Quantity"]);

                            switch (sProcessName)
                            {
                                case "ULC":
                                    nTotalCutting = nTotalCutting + nProdQuantity;
                                    nCuttingBal = nOrderQuantity - nTotalCutting; break;
                                case "COU":
                                    nUpperOutput = nUpperOutput + nProdQuantity;
                                    nUpperOutputBal = nOrderQuantity - nUpperOutput; break;
                                case "KITTING":
                                    nFSCharging = nFSCharging + nProdQuantity;
                                    nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                case "KITTINGC":
                                    nFSCharging = nFSCharging + nProdQuantity;
                                    nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                case "CONVOUT":
                                    nFSOutput = nFSOutput + nProdQuantity;
                                    nFSOutputBal = nOrderQuantity - nFSOutput; break;
                                case "CONVOUTC":
                                    nFSOutput = nFSOutput + nProdQuantity;
                                    nFSOutputBal = nOrderQuantity - nFSOutput; break;
                            }

                            SqlCommand cmdUpdOs = new SqlCommand();
                            cmdUpdOs.Connection = conn;
                            cmdUpdOs.CommandText = "SLI_Dashboard";
                            cmdUpdOs.CommandType = CommandType.StoredProcedure;

                            cmdUpdOs.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UpdOs";
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sOSId;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mTotalCutting", SqlDbType.Int)).Value = nTotalCutting;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mCuttingBal", SqlDbType.Int)).Value = nCuttingBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperOutput", SqlDbType.Int)).Value = nUpperOutput;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperOutputBal", SqlDbType.Int)).Value = nUpperOutputBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = nUpperDispatch;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nUpperDispatchBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSCharging", SqlDbType.Int)).Value = nFSCharging;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSChargingBal", SqlDbType.Int)).Value = nFSChargingBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSOutput", SqlDbType.Int)).Value = nFSOutput;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSOutputBal", SqlDbType.Int)).Value = nFSOutputBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = nShippedQuantity;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nShippedQuantityBal;


                            SqlDataAdapter adapterUpdOs = new SqlDataAdapter(cmdUpdOs);
                            DataTable dtUpdOs = new DataTable();
                            adapterUpdOs.Fill(dtUpdOs);
                        }

                        SqlCommand UpdProd = new SqlCommand();
                        UpdProd.Connection = conn;
                        UpdProd.CommandText = "SLI_Dashboard";
                        UpdProd.CommandType = CommandType.StoredProcedure;

                        UpdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDPROD";
                        UpdProd.Parameters.Add(new SqlParameter("@mId", SqlDbType.VarChar)).Value = sId;
                        UpdProd.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        SqlDataAdapter adapterUpdProd = new SqlDataAdapter(UpdProd);
                        DataTable dtUpdProd = new DataTable();
                        adapterUpdProd.Fill(dtUpdProd);
                    }

                    #region "UPPER DISPATCH QUANTITY UPDATE"
                    SqlCommand cmdUpDesp = new SqlCommand();
                    cmdUpDesp.Connection = conn;
                    cmdUpDesp.CommandText = "SLI_Dashboard";
                    cmdUpDesp.CommandType = CommandType.StoredProcedure;

                    cmdUpDesp.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELUPDISP";

                    SqlDataAdapter adapterUpDesp = new SqlDataAdapter(cmdUpDesp);
                    DataTable dtUpDesp = new DataTable();
                    adapterUpDesp.Fill(dtUpDesp);

                    DataSet dsUpDesp = new DataSet();
                    adapterUpDesp.Fill(dsUpDesp);

                    i = 0;
                    for (i = 0; i < dtUpDesp.Rows.Count; i++)
                    {

                        sWorkOrderNo = dsUpDesp.Tables[0].Rows[i]["WorkOrderNo"].ToString();
                        sSalesOrderNo = sWorkOrderNo.ToString().Substring(0, 19);
                        sId = dsUpDesp.Tables[0].Rows[i]["Id"].ToString();

                        //nUpperDispatch = Convert.ToInt32(dsUpDesp.Tables[0].Rows[i]["IssueQuantity"]);

                        SqlCommand cmdChkOS = new SqlCommand();
                        cmdChkOS.Connection = conn;
                        cmdChkOS.CommandText = "SLI_Dashboard";
                        cmdChkOS.CommandType = CommandType.StoredProcedure;

                        cmdChkOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "CHECKOS";
                        cmdChkOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                        SqlDataAdapter adapterChkOS = new SqlDataAdapter(cmdChkOS);
                        DataTable dtChkOS = new DataTable();
                        adapterChkOS.Fill(dtChkOS);

                        DataSet dsChkOS = new DataSet();
                        adapterChkOS.Fill(dsChkOS);
                        if (dtChkOS.Rows.Count == 0)
                        {
                            SqlCommand cmdSelJCD = new SqlCommand();
                            cmdSelJCD.Connection = conn;
                            cmdSelJCD.CommandText = "SLI_Dashboard";
                            cmdSelJCD.CommandType = CommandType.StoredProcedure;

                            cmdSelJCD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCD";
                            cmdSelJCD.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sWorkOrderNo;

                            SqlDataAdapter adapterSelJCD = new SqlDataAdapter(cmdSelJCD);
                            DataTable dtSelJCD = new DataTable();
                            adapterSelJCD.Fill(dtSelJCD);

                            DataSet dsSelJCD = new DataSet();
                            adapterSelJCD.Fill(dsSelJCD);
                            if (dtSelJCD.Rows.Count > 0)
                                sSalesOrderDetailsID = dsSelJCD.Tables[0].Rows[0]["SalesOrderDetailID"].ToString();
                            else
                            {
                                sSalesOrderDetailsID = "";
                                MessageBox.Show("Assortment Job Card");
                            }

                            SqlCommand cmdSelSOD = new SqlCommand();
                            cmdSelSOD.Connection = conn;
                            cmdSelSOD.CommandText = "SLI_Dashboard";
                            cmdSelSOD.CommandType = CommandType.StoredProcedure;

                            cmdSelSOD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOD";
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                            SqlDataAdapter adapterSelSOD = new SqlDataAdapter(cmdSelSOD);
                            DataTable dtSelSOD = new DataTable();
                            adapterSelSOD.Fill(dtSelSOD);

                            DataSet dsSelSOD = new DataSet();
                            adapterSelSOD.Fill(dsSelSOD);
                            if (dtSelSOD.Rows.Count > 0)
                            {
                                sSeason = dsSelSOD.Tables[0].Rows[0]["season"].ToString();
                                sBuyerGroupCode = dsSelSOD.Tables[0].Rows[0]["BuyerGroupCode"].ToString();
                                sBuyerBuy = dsSelSOD.Tables[0].Rows[0]["BuyerBuy"].ToString();
                                sArticleGroup = dsSelSOD.Tables[0].Rows[0]["ArticleGroup"].ToString();
                                sArticle = dsSelSOD.Tables[0].Rows[0]["Article"].ToString();
                                sBuyerArticle = dsSelSOD.Tables[0].Rows[0]["BuyerArticle"].ToString();
                                sColor = dsSelSOD.Tables[0].Rows[0]["ColorName"].ToString();
                                sCustomerOrderNo = dsSelSOD.Tables[0].Rows[0]["CustomerOrderNo"].ToString();
                                nOrderQuantity = Convert.ToInt32(dsSelSOD.Tables[0].Rows[0]["OrderQuantity"]);
                                //sSalesOrderID = dsSelSOD.Tables[0].Rows[0]["ID"].ToString();

                                ClearQuantity();
                                nUpperDispatchBal = nOrderQuantity - nUpperDispatch;


                                SqlCommand cmdInsOS = new SqlCommand();
                                cmdInsOS.Connection = conn;
                                cmdInsOS.CommandText = "SLI_Dashboard";
                                cmdInsOS.CommandType = CommandType.StoredProcedure;

                                cmdInsOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "InsOS";
                                cmdInsOS.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSeason", SqlDbType.VarChar)).Value = sSeason;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = sBuyerGroupCode;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerBuy", SqlDbType.VarChar)).Value = sBuyerBuy;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value = sArticleGroup;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticle", SqlDbType.VarChar)).Value = sArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerArticle", SqlDbType.VarChar)).Value = sBuyerArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mColor", SqlDbType.VarChar)).Value = sColor;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = sWorkOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderDetailsID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mOrderQuantity", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mTotalCutting", SqlDbType.Int)).Value = nTotalCutting;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCuttingBal", SqlDbType.Int)).Value = nCuttingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutput", SqlDbType.Int)).Value = nUpperOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutputBal", SqlDbType.Int)).Value = nUpperOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = nUpperDispatch;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nUpperDispatchBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSCharging", SqlDbType.Int)).Value = nFSCharging;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSChargingBal", SqlDbType.Int)).Value = nFSChargingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutput", SqlDbType.Int)).Value = nFSOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutputBal", SqlDbType.Int)).Value = nFSOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = nShippedQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nShippedQuantityBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                                SqlDataAdapter adapterInsOS = new SqlDataAdapter(cmdInsOS);
                                DataTable dtInsOS = new DataTable();
                                adapterInsOS.Fill(dtInsOS);
                            }
                        }
                        else
                        {
                            ClearQuantity();
                            nUpperDispatch = Convert.ToInt32(dsUpDesp.Tables[0].Rows[i]["IssueQuantity"]);
                            string sOSId = dsChkOS.Tables[0].Rows[0]["Id"].ToString();
                            if (sOSId == "1c519974-34c9-45c0-8540-811d6d5ba14b")
                            {
                                MessageBox.Show(sId);
                            }
                            //nUpperDispatchBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatchBal"]);
                            nOrderQuantity = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["OrderQuantity"]);

                            nUpperDispatch = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatch"]) + nUpperDispatch;
                            nUpperDispatchBal = nOrderQuantity - nUpperDispatch;

                            SqlCommand cmdUpdOs = new SqlCommand();
                            cmdUpdOs.Connection = conn;
                            cmdUpdOs.CommandText = "SLI_Dashboard";
                            cmdUpdOs.CommandType = CommandType.StoredProcedure;

                            cmdUpdOs.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UpdUppDisp";
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sOSId;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = nUpperDispatch;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nUpperDispatchBal;

                            SqlDataAdapter adapterUpdOs = new SqlDataAdapter(cmdUpdOs);
                            DataTable dtUpdOs = new DataTable();
                            adapterUpdOs.Fill(dtUpdOs);
                        }

                        SqlCommand UpdProd = new SqlCommand();
                        UpdProd.Connection = conn;
                        UpdProd.CommandText = "SLI_Dashboard";
                        UpdProd.CommandType = CommandType.StoredProcedure;

                        UpdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDMATISS";
                        UpdProd.Parameters.Add(new SqlParameter("@mId", SqlDbType.VarChar)).Value = sId;
                        UpdProd.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        SqlDataAdapter adapterUpdProd = new SqlDataAdapter(UpdProd);
                        DataTable dtUpdProd = new DataTable();
                        adapterUpdProd.Fill(dtUpdProd);
                    }
                    #endregion

                    #region "SHIPMENT DISPATCH QUANTITY UPDATE"
                    SqlCommand cmdShipd = new SqlCommand();
                    cmdShipd.Connection = conn;
                    cmdShipd.CommandText = "SLI_Dashboard";
                    cmdShipd.CommandType = CommandType.StoredProcedure;

                    cmdShipd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSHIPD";

                    SqlDataAdapter adapterShipd = new SqlDataAdapter(cmdShipd);
                    DataTable dtShipd = new DataTable();
                    adapterShipd.Fill(dtShipd);

                    DataSet dsShipd = new DataSet();
                    adapterShipd.Fill(dsShipd);

                    i = 0;
                    for (i = 0; i < dtShipd.Rows.Count; i++)
                    {
                        sId = dsShipd.Tables[0].Rows[i]["Id"].ToString();

                        ClearQuantity();
                        nShippedQuantity = Convert.ToInt32(dsShipd.Tables[0].Rows[i]["SOShippedQuantity"]);
                        nOrderQuantity = Convert.ToInt32(dsShipd.Tables[0].Rows[0]["OrderQuantity"]);
                        nShippedQuantityBal = nOrderQuantity - nShippedQuantity;

                        SqlCommand cmdUpdOs = new SqlCommand();
                        cmdUpdOs.Connection = conn;
                        cmdUpdOs.CommandText = "SLI_Dashboard";
                        cmdUpdOs.CommandType = CommandType.StoredProcedure;

                        cmdUpdOs.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDSHIPD";
                        cmdUpdOs.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sId;
                        cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = nShippedQuantity;
                        cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nShippedQuantityBal;

                        SqlDataAdapter adapterUpdOs = new SqlDataAdapter(cmdUpdOs);
                        DataTable dtUpdOs = new DataTable();
                        adapterUpdOs.Fill(dtUpdOs);
                    }
                    #endregion
                }
                return true;
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

        public DataTable LoadOrderOutstanding()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELBWOS";

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

        public DataTable LoadOrderOutstandingArticleGroupWise(string sBuyerCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELAGOS";
                    cmd.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = sBuyerCode;

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

        public DataTable LoadOrderOutstandingSalesOrderWise(string sBuyerCode, string sArticleGroup)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSALESORDEROS";
                    cmd.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = sBuyerCode;
                    cmd.Parameters.Add(new SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value = sArticleGroup;

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

        public DataTable LoadPurchaseOrderOS(string sSalesOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPOOS";
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

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

        public DataTable LoadLeatherPurchaseOrderOS(string sSalesOrderNo, string sAgainst, string sCustomerOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sAgainst == "PO")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELLEAPOOS";
                    else
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELLEAPOOSASBOM";

                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;

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

        public DataTable LoadUpperMaterials(string sSalesOrderNo, string sAgainst, string sCustomerOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sAgainst == "PO")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELUPMATASPO";
                    else
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELUPMATASBOM";

                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;

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

        public DataTable LoadSoles(string sSalesOrderNo, string sAgainst, string sCustomerOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sAgainst == "PO")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOLEASPO";
                    else
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOLEASBOM";

                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;

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

        public DataTable LoadFullShoeMaterials(string sSalesOrderNo, string sAgainst, string sCustomerOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sAgainst == "PO")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELFSMASPO";
                    else
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELFSMASBOM";

                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;

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
        public DataTable LoadPackingMaterials(string sSalesOrderNo, string sAgainst, string sCustomerOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sAgainst == "PO")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPACKASPO";
                    else
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPACKASBOM";

                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;

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

        public DataTable LoadProductionStatus(string sSalesOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_Dashboard";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSTAGEPROD";
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

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
        public void ClearAll()
        {
            sSeason = ""; sBuyerGroupCode = ""; sBuyerBuy = ""; sArticleGroup = ""; sArticle = ""; sBuyerArticle = ""; sSalesOrderID = "";
            sColor = ""; sCustomerOrderNo = ""; sWorkOrderNo = ""; sSalesOrderDetailsID = ""; sSalesOrderNo = "";
            nOrderQuantity = 0;
        }

        public void ClearQuantity()
        {
            nTotalCutting = 0; nCuttingBal = 0; nUpperOutput = 0; nUpperOutputBal = 0; nUpperDispatch = 0; nUpperDispatchBal = 0; nFSCharging = 0;
            nFSChargingBal = 0; nFSOutput = 0; nFSOutputBal = 0; nShippedQuantity = 0; nShippedQuantityBal = 0;
        }
    }
}
