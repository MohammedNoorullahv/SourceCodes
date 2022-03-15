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
using System.Windows.Forms;

namespace OptimizerAddOn.ComponentClasses
{
    public partial class CCHRSalaryMaster : Component
    {
        public CCHRSalaryMaster()
        {
            InitializeComponent();
        }

        public CCHRSalaryMaster(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public bool InsertSalaryMaster(StrHRSalaryMaster strHRSalaryMaster)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionStringHR))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "proc_sliHR";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "INSERT";
                    cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = strHRSalaryMaster.ID;
                    cmd.Parameters.Add(new SqlParameter("@mslno", SqlDbType.VarChar)).Value = strHRSalaryMaster.slno;
                    cmd.Parameters.Add(new SqlParameter("@mEmpcode", SqlDbType.VarChar)).Value = strHRSalaryMaster.Empcode;
                    cmd.Parameters.Add(new SqlParameter("@mEmpname", SqlDbType.VarChar)).Value = strHRSalaryMaster.Empname;
                    cmd.Parameters.Add(new SqlParameter("@mDepartment", SqlDbType.VarChar)).Value = strHRSalaryMaster.Department;
                    cmd.Parameters.Add(new SqlParameter("@mDesignation", SqlDbType.VarChar)).Value = strHRSalaryMaster.Designation;
                    cmd.Parameters.Add(new SqlParameter("@mBasic", SqlDbType.Decimal)).Value = strHRSalaryMaster.Basic;
                    cmd.Parameters.Add(new SqlParameter("@mDA", SqlDbType.Decimal)).Value = strHRSalaryMaster.DA;
                    cmd.Parameters.Add(new SqlParameter("@mHRA", SqlDbType.Decimal)).Value = strHRSalaryMaster.HRA;
                    cmd.Parameters.Add(new SqlParameter("@mConveyance", SqlDbType.Decimal)).Value = strHRSalaryMaster.Conveyance;
                    cmd.Parameters.Add(new SqlParameter("@mLTA", SqlDbType.Decimal)).Value = strHRSalaryMaster.LTA;
                    cmd.Parameters.Add(new SqlParameter("@mMA", SqlDbType.Decimal)).Value = strHRSalaryMaster.MA;
                    cmd.Parameters.Add(new SqlParameter("@mWA", SqlDbType.Decimal)).Value = strHRSalaryMaster.WA;
                    cmd.Parameters.Add(new SqlParameter("@mBPA", SqlDbType.Decimal)).Value = strHRSalaryMaster.BPA;
                    cmd.Parameters.Add(new SqlParameter("@mEA", SqlDbType.Decimal)).Value = strHRSalaryMaster.EA;
                    cmd.Parameters.Add(new SqlParameter("@mOA", SqlDbType.Decimal)).Value = strHRSalaryMaster.OA;
                    cmd.Parameters.Add(new SqlParameter("@mGross", SqlDbType.Decimal)).Value = strHRSalaryMaster.Gross;
                    cmd.Parameters.Add(new SqlParameter("@mNetAmount", SqlDbType.Decimal)).Value = strHRSalaryMaster.NetAmount;
                    cmd.Parameters.Add(new SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                    cmd.Parameters.Add(new SqlParameter("@mGroupCode", SqlDbType.VarChar)).Value = strHRSalaryMaster.GroupCode;
                    cmd.Parameters.Add(new SqlParameter("@mGroupName", SqlDbType.VarChar)).Value = strHRSalaryMaster.GroupName;
                    cmd.Parameters.Add(new SqlParameter("@mUnit", SqlDbType.VarChar)).Value = strHRSalaryMaster.Unit;
                    cmd.Parameters.Add(new SqlParameter("@mCompanyCode", SqlDbType.VarChar)).Value = strHRSalaryMaster.CompanyCode;
                    cmd.Parameters.Add(new SqlParameter("@mCompanyName", SqlDbType.VarChar)).Value = strHRSalaryMaster.CompanyName;
                    cmd.Parameters.Add(new SqlParameter("@mUnitName", SqlDbType.VarChar)).Value = strHRSalaryMaster.UnitName;
                    cmd.Parameters.Add(new SqlParameter("@mExcessPF", SqlDbType.Decimal)).Value = strHRSalaryMaster.ExcessPF;
                    cmd.Parameters.Add(new SqlParameter("@mEMPCategory", SqlDbType.VarChar)).Value = strHRSalaryMaster.EMPCategory;
                    cmd.Parameters.Add(new SqlParameter("@mEncashment", SqlDbType.Decimal)).Value = strHRSalaryMaster.Encashment;
                    cmd.Parameters.Add(new SqlParameter("@mSpecialAllowance", SqlDbType.Decimal)).Value = strHRSalaryMaster.SpecialAllowance;
                    cmd.Parameters.Add(new SqlParameter("@mOldEmpCode", SqlDbType.VarChar)).Value = strHRSalaryMaster.OldEmpCode;
                    cmd.Parameters.Add(new SqlParameter("@mPFPercentage", SqlDbType.Decimal)).Value = strHRSalaryMaster.PFPercentage;
                    cmd.Parameters.Add(new SqlParameter("@mESIPercentage", SqlDbType.Decimal)).Value = strHRSalaryMaster.ESIPercentage;
                    cmd.Parameters.Add(new SqlParameter("@mMonthlyBonus", SqlDbType.Decimal)).Value = strHRSalaryMaster.MonthlyBonus;
                    cmd.Parameters.Add(new SqlParameter("@mFoodAllowance", SqlDbType.Decimal)).Value = strHRSalaryMaster.FoodAllowance;
                    cmd.Parameters.Add(new SqlParameter("@mIsActive", SqlDbType.Bit)).Value = strHRSalaryMaster.IsActive;
                    cmd.Parameters.Add(new SqlParameter("@mValidFrom", SqlDbType.DateTime)).Value = strHRSalaryMaster.ValidFrom;
                    cmd.Parameters.Add(new SqlParameter("@mSalaryMasterID", SqlDbType.VarChar)).Value = strHRSalaryMaster.SalaryMasterID;
                    cmd.Parameters.Add(new SqlParameter("@mReason", SqlDbType.VarChar)).Value = strHRSalaryMaster.Reason;
                    cmd.Parameters.Add(new SqlParameter("@mCanteenApplicable", SqlDbType.Bit)).Value = strHRSalaryMaster.CanteenApplicable;
                    cmd.Parameters.Add(new SqlParameter("@mInsuredAmount", SqlDbType.Int)).Value = strHRSalaryMaster.InsuredAmount;
                    cmd.Parameters.Add(new SqlParameter("@mMonthlyInsurance", SqlDbType.Int)).Value = strHRSalaryMaster.MonthlyInsurance;
                    cmd.Parameters.Add(new SqlParameter("@mPFEligible", SqlDbType.VarChar)).Value = strHRSalaryMaster.PFEligible;
                    cmd.Parameters.Add(new SqlParameter("@mESIEligible", SqlDbType.VarChar)).Value = strHRSalaryMaster.ESIEligible;
                    cmd.Parameters.Add(new SqlParameter("@mSalaryType", SqlDbType.VarChar)).Value = strHRSalaryMaster.SalaryType;
                    cmd.Parameters.Add(new SqlParameter("@mRetrenchmentPercentage", SqlDbType.Decimal)).Value = strHRSalaryMaster.RetrenchmentPercentage;
                    cmd.Parameters.Add(new SqlParameter("@mRevisedGross", SqlDbType.Decimal)).Value = strHRSalaryMaster.RevisedGross;
                    cmd.Parameters.Add(new SqlParameter("@mLessValue", SqlDbType.Decimal)).Value = strHRSalaryMaster.LessValue;
                    cmd.Parameters.Add(new SqlParameter("@mHRARM", SqlDbType.Decimal)).Value = strHRSalaryMaster.HRARM;
                    cmd.Parameters.Add(new SqlParameter("@mOARM", SqlDbType.Decimal)).Value = strHRSalaryMaster.OARM;



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
    }
}
