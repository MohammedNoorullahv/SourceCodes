using OptimizerAddOn.ComponentClasses;
using OptimizerAddOn.MDI;
using OptimizerAddOn.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizerAddOn.HR
{
    public partial class FrmSalaryImport : Form
    {
        CCHRSalaryMaster ccHRSalaryMaster;

        StrHRSalaryMaster strHRSalaryMaster;

        string fileName = "";

        string sID;
        string sslno;
        string sEmpcode;
        string sEmpname;
        string sDepartment;
        string sDesignation;
        decimal dBasic;
        decimal dDA;
        decimal dHRA;
        decimal dConveyance;
        decimal dLTA;
        decimal dMA;
        decimal dWA;
        decimal dBPA;
        decimal dEA;
        decimal dOA;
        decimal dGross;
        decimal dNetAmount;
        string sCreatedBy;
        DateTime dCreatedDate;
        string sModifiedBy;
        DateTime dModifiedDate;
        string sEnteredOnMachineID;
        string sModuleName;
        bool bIsApproved;
        string sApprovedBy;
        DateTime dApprovedOn;
        string sGroupCode;
        string sGroupName;
        string sUnit;
        string sCompanyCode;
        string sCompanyName;
        string sUnitName;
        decimal dExcessPF;
        string sEMPCategory;
        decimal dEncashment;
        decimal dSpecialAllowance;
        string sOldEmpCode;
        decimal dPFPercentage;
        decimal dESIPercentage;
        decimal dMonthlyBonus;
        decimal dFoodAllowance;
        bool bIsActive;
        DateTime dValidFrom;
        DateTime dValidTill;
        string sSalaryMasterID;
        string sReason;
        bool bCanteenApplicable;
        int iInsuredAmount;
        int iMonthlyInsurance;
        string sPFEligible;
        string sESIEligible;
        string sSalaryType;
        decimal dRetrenchmentPercentage;
        decimal dRevisedGross;
        decimal dLessValue;
        decimal dHRARM;
        decimal dOARM;

        private void cbExit_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMDI frmMDI = new FrmMDI();
                frmMDI.ShowDialog();
                frmMDI.ToolsBringToFront();
                this.Hide();
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }


        public FrmSalaryImport()
        {
            InitializeComponent();

            ccHRSalaryMaster = new CCHRSalaryMaster();
            strHRSalaryMaster = new StrHRSalaryMaster();
        }

        private void cbGenerateHR_Click(object sender, EventArgs e)
        {
            try
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                int nSlno = 0;
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionStringHR))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select ISNULL(Max(slno),0)  As Slno from HRSalaryMaster";
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dsEmp1 = new DataSet();
                    adapter.Fill(dsEmp1);

                    string sSlno = dsEmp1.Tables[0].Rows[0]["Slno"].ToString();
                    nSlno = Convert.ToInt32(sSlno.ToString().Substring(3, 5));
                }

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "xlx files (*.xls)|*.xls|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;


                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;

                        //Read the contents of the file into a stream
                        var fileStream = openFileDialog.OpenFile();

                        Microsoft.Office.Interop.Excel.Application xlApp;
                        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                        object misValue = System.Reflection.Missing.Value;

                        var fileName = filePath;
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        int NColumnNo = 1, NRowNo = 2;
                        string sErrorList = "";
                        string sIsDataCorrect = "Y";

                        while ((xlWorkSheet.Cells[NRowNo, 1].Value != null))
                        {
                            if ((xlWorkSheet.Cells[NRowNo, 38].Value != null))
                            {
                                sOldEmpCode = xlWorkSheet.Cells[NRowNo, 38].Value.ToString();

                                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionStringHR))
                                {

                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = "Select * from Employee Where OldEmpCode = @mOldEmpCode";
                                    cmd.CommandType = CommandType.Text;

                                    cmd.Parameters.Add(new SqlParameter("@mOldEmpCode", SqlDbType.VarChar)).Value = sOldEmpCode;

                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    DataTable dtEmp = new DataTable();
                                    adapter.Fill(dtEmp);

                                    DataSet dsEmp = new DataSet();
                                    adapter.Fill(dsEmp);

                                    int nRowCount = dtEmp.Rows.Count;
                                    if (nRowCount == 0)
                                    {
                                        sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- Old Employee Code does Not Exist" + Environment.NewLine;
                                        sIsDataCorrect = "N";
                                    }
                                    else if (nRowCount > 1)
                                    {
                                        sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- Multiple Old Employee Code mapped in Employee Table" + Environment.NewLine;
                                        sIsDataCorrect = "N";
                                    }
                                    else
                                    {
                                        sEmpcode = dsEmp.Tables[0].Rows[0]["EmpCode"].ToString();
                                        sEmpname = dsEmp.Tables[0].Rows[0]["EmpFullName"].ToString();
                                        sDepartment = dsEmp.Tables[0].Rows[0]["EmployeeDepartment"].ToString();
                                        sDesignation = dsEmp.Tables[0].Rows[0]["Designation"].ToString();
                                        sGroupCode = dsEmp.Tables[0].Rows[0]["GroupCode"].ToString();
                                        sGroupName = dsEmp.Tables[0].Rows[0]["GroupName"].ToString();
                                        sUnit = dsEmp.Tables[0].Rows[0]["UnitCode"].ToString();
                                        sCompanyCode = dsEmp.Tables[0].Rows[0]["CompanyCode"].ToString();
                                        sCompanyName = dsEmp.Tables[0].Rows[0]["CompanyName"].ToString();
                                        sUnitName = dsEmp.Tables[0].Rows[0]["UnitName"].ToString();
                                        sEMPCategory = dsEmp.Tables[0].Rows[0]["Category"].ToString();
                                        sOldEmpCode = dsEmp.Tables[0].Rows[0]["OldEmpCode"].ToString();


                                        for (int i = 7; i <= 58; i++)
                                        {
                                            if ((xlWorkSheet.Cells[NRowNo, i].Value != null))
                                            {
                                                switch (i)
                                                {
                                                    case 7: dBasic = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 8: dDA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 9: dHRA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 10: dConveyance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 11: dLTA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 12: dMA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 13: dWA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 14: dBPA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 15: dEA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 16: dOA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 17: dGross = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 18: dNetAmount = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 34: dExcessPF = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 36: dEncashment = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 37: dSpecialAllowance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 39: dPFPercentage = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 40: dESIPercentage = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 41: dMonthlyBonus = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 42: dFoodAllowance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 44: dValidFrom = Convert.ToDateTime(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 48: bCanteenApplicable = Convert.ToBoolean(xlWorkSheet.Cells[NRowNo, i].Value); break;
                                                    case 49: iInsuredAmount = Convert.ToInt32(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 50: iMonthlyInsurance = Convert.ToInt32(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 51: sPFEligible = (xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 52: sESIEligible = (xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                    case 53: sSalaryType = (xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;

                                                }
                                            }
                                            else
                                            {
                                                sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- " + xlWorkSheet.Cells[1, i].Value.ToString() + " Not Available" + Environment.NewLine;
                                                sIsDataCorrect = "N";
                                            }
                                        }
                                        nSlno = nSlno + 1;
                                        strHRSalaryMaster.ID = System.Guid.NewGuid().ToString();
                                        strHRSalaryMaster.slno = "SL-" + nSlno.ToString();
                                        strHRSalaryMaster.Empcode = sEmpcode;
                                        strHRSalaryMaster.Empname = sEmpname;
                                        strHRSalaryMaster.Department = sDepartment;
                                        strHRSalaryMaster.Designation = sDesignation;
                                        strHRSalaryMaster.Basic = dBasic;
                                        strHRSalaryMaster.DA = dDA;
                                        strHRSalaryMaster.HRA = dHRA;
                                        strHRSalaryMaster.Conveyance = dConveyance;
                                        strHRSalaryMaster.LTA = dLTA;
                                        strHRSalaryMaster.MA = dMA;
                                        strHRSalaryMaster.WA = dWA;
                                        strHRSalaryMaster.BPA = dBPA;
                                        strHRSalaryMaster.EA = dEA;
                                        strHRSalaryMaster.OA = dOA;
                                        strHRSalaryMaster.Gross = dGross;
                                        strHRSalaryMaster.NetAmount = dNetAmount;
                                        strHRSalaryMaster.GroupCode = sGroupCode;
                                        strHRSalaryMaster.GroupName = sGroupName;
                                        strHRSalaryMaster.Unit = sUnit;
                                        strHRSalaryMaster.CompanyCode = sCompanyCode;
                                        strHRSalaryMaster.CompanyName = sCompanyName;
                                        strHRSalaryMaster.UnitName = sUnitName;
                                        strHRSalaryMaster.ExcessPF = dExcessPF;
                                        strHRSalaryMaster.EMPCategory = sEMPCategory;
                                        strHRSalaryMaster.Encashment = dEncashment;
                                        strHRSalaryMaster.SpecialAllowance = dSpecialAllowance;
                                        strHRSalaryMaster.OldEmpCode = sOldEmpCode;
                                        strHRSalaryMaster.PFPercentage = dPFPercentage;
                                        strHRSalaryMaster.ESIPercentage = dESIPercentage;
                                        strHRSalaryMaster.MonthlyBonus = dMonthlyBonus;
                                        strHRSalaryMaster.FoodAllowance = dFoodAllowance;
                                        strHRSalaryMaster.IsActive = true;
                                        strHRSalaryMaster.ValidFrom = DateTime.Now.Date; //dValidFrom;
                                        strHRSalaryMaster.SalaryMasterID = sSalaryMasterID;
                                        strHRSalaryMaster.Reason = sReason;
                                        strHRSalaryMaster.CanteenApplicable = bCanteenApplicable;
                                        strHRSalaryMaster.InsuredAmount = iInsuredAmount;
                                        strHRSalaryMaster.MonthlyInsurance = iMonthlyInsurance;
                                        strHRSalaryMaster.PFEligible = sPFEligible;
                                        strHRSalaryMaster.ESIEligible = sESIEligible;
                                        strHRSalaryMaster.SalaryType = sSalaryType;
                                        strHRSalaryMaster.RetrenchmentPercentage = dRetrenchmentPercentage;
                                        strHRSalaryMaster.RevisedGross = dRevisedGross;
                                        strHRSalaryMaster.LessValue = dLessValue;
                                        strHRSalaryMaster.HRARM = dHRARM;
                                        strHRSalaryMaster.OARM = dOARM;
                                    }
                                }
                            }
                            else
                            {
                                sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- Old Employee Code Not Exist in the Attached Excel File" + Environment.NewLine;
                                sIsDataCorrect = "N";
                            }
                            NRowNo += 1;
                        }

                        if (sIsDataCorrect == "Y")
                        {
                            while ((xlWorkSheet.Cells[NRowNo, 1].Value != null))
                            {
                                if ((xlWorkSheet.Cells[NRowNo, 38].Value != null))
                                {
                                    sOldEmpCode = xlWorkSheet.Cells[NRowNo, 38].Value.ToString();

                                    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionStringHR))
                                    {

                                        SqlCommand cmd = new SqlCommand();
                                        cmd.Connection = conn;
                                        cmd.CommandText = "Select * from Employee Where OldEmpCode = @mOldEmpCode";
                                        cmd.CommandType = CommandType.Text;

                                        cmd.Parameters.Add(new SqlParameter("@mOldEmpCode", SqlDbType.VarChar)).Value = sOldEmpCode;

                                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                        DataTable dtEmp = new DataTable();
                                        adapter.Fill(dtEmp);

                                        DataSet dsEmp = new DataSet();
                                        adapter.Fill(dsEmp);

                                        int nRowCount = dtEmp.Rows.Count;
                                        if (nRowCount == 0)
                                        {
                                            sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- Old Employee Code does Not Exist" + Environment.NewLine;
                                            sIsDataCorrect = "N";
                                        }
                                        else if (nRowCount > 1)
                                        {
                                            sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- Multiple Old Employee Code mapped in Employee Table" + Environment.NewLine;
                                            sIsDataCorrect = "N";
                                        }
                                        else
                                        {
                                            sEmpcode = dsEmp.Tables[0].Rows[0]["EmpCode"].ToString();
                                            sEmpname = dsEmp.Tables[0].Rows[0]["EmpFullName"].ToString();
                                            sDepartment = dsEmp.Tables[0].Rows[0]["EmployeeDepartment"].ToString();
                                            sDesignation = dsEmp.Tables[0].Rows[0]["Designation"].ToString();
                                            sGroupCode = dsEmp.Tables[0].Rows[0]["GroupCode"].ToString();
                                            sGroupName = dsEmp.Tables[0].Rows[0]["GroupName"].ToString();
                                            sUnit = dsEmp.Tables[0].Rows[0]["UnitCode"].ToString();
                                            sCompanyCode = dsEmp.Tables[0].Rows[0]["CompanyCode"].ToString();
                                            sCompanyName = dsEmp.Tables[0].Rows[0]["CompanyName"].ToString();
                                            sUnitName = dsEmp.Tables[0].Rows[0]["UnitName"].ToString();
                                            sEMPCategory = dsEmp.Tables[0].Rows[0]["Category"].ToString();
                                            sOldEmpCode = dsEmp.Tables[0].Rows[0]["OldEmpCode"].ToString();


                                            for (int i = 7; i <= 58; i++)
                                            {
                                                if ((xlWorkSheet.Cells[NRowNo, i].Value != null))
                                                {
                                                    switch (i)
                                                    {
                                                        //case 1:
                                                        //    Console.WriteLine("Monday");
                                                        //    break;
                                                        case 7: dBasic = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 8: dDA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 9: dHRA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 10: dConveyance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 11: dLTA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 12: dMA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 13: dWA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 14: dBPA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 15: dEA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 16: dOA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 17: dGross = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 18: dNetAmount = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 34: dExcessPF = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 36: dEncashment = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 37: dSpecialAllowance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 39: dPFPercentage = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 40: dESIPercentage = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 41: dMonthlyBonus = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 42: dFoodAllowance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 44: dValidFrom = Convert.ToDateTime(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 48: bCanteenApplicable = Convert.ToBoolean(xlWorkSheet.Cells[NRowNo, i].Value); break;
                                                        case 49: iInsuredAmount = Convert.ToInt32(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 50: iMonthlyInsurance = Convert.ToInt32(xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 51: sPFEligible = (xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 52: sESIEligible = (xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;
                                                        case 53: sSalaryType = (xlWorkSheet.Cells[NRowNo, i].Value.ToString()); break;

                                                    }
                                                    //if (i == 7) dBasic = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 8) dDA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 9) dHRA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 10) dConveyance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 11) dLTA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 12) dMA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 13) dWA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 14) dBPA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 15) dEA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 16) dOA = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 17) dGross = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 18) dNetAmount = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 34) dExcessPF = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 36) dEncashment = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 37) dSpecialAllowance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 39) dPFPercentage = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 40) dESIPercentage = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 41) dMonthlyBonus = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 42) dFoodAllowance = Convert.ToDecimal(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 44) dValidFrom = Convert.ToDateTime(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 48) bCanteenApplicable = Convert.ToBoolean(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 49) iInsuredAmount = Convert.ToInt32(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 50) iMonthlyInsurance = Convert.ToDateTime(xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 51) sPFEligible = (xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 52) sESIEligible = (xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                    //if (i == 53) sSalaryType = (xlWorkSheet.Cells[NRowNo, i].Value.ToString());
                                                }
                                                else
                                                {
                                                    sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- " + xlWorkSheet.Cells[1, i].Value.ToString() + " Not Available" + Environment.NewLine;
                                                    sIsDataCorrect = "N";
                                                }
                                            }
                                            nSlno = nSlno + 1;
                                            strHRSalaryMaster.ID = System.Guid.NewGuid().ToString();
                                            strHRSalaryMaster.slno = "SL-" + nSlno.ToString();
                                            strHRSalaryMaster.Empcode = sEmpcode;
                                            strHRSalaryMaster.Empname = sEmpname;
                                            strHRSalaryMaster.Department = sDepartment;
                                            strHRSalaryMaster.Designation = sDesignation;
                                            strHRSalaryMaster.Basic = dBasic;
                                            strHRSalaryMaster.DA = dDA;
                                            strHRSalaryMaster.HRA = dHRA;
                                            strHRSalaryMaster.Conveyance = dConveyance;
                                            strHRSalaryMaster.LTA = dLTA;
                                            strHRSalaryMaster.MA = dMA;
                                            strHRSalaryMaster.WA = dWA;
                                            strHRSalaryMaster.BPA = dBPA;
                                            strHRSalaryMaster.EA = dEA;
                                            strHRSalaryMaster.OA = dOA;
                                            strHRSalaryMaster.Gross = dGross;
                                            strHRSalaryMaster.NetAmount = dNetAmount;
                                            strHRSalaryMaster.GroupCode = sGroupCode;
                                            strHRSalaryMaster.GroupName = sGroupName;
                                            strHRSalaryMaster.Unit = sUnit;
                                            strHRSalaryMaster.CompanyCode = sCompanyCode;
                                            strHRSalaryMaster.CompanyName = sCompanyName;
                                            strHRSalaryMaster.UnitName = sUnitName;
                                            strHRSalaryMaster.ExcessPF = dExcessPF;
                                            strHRSalaryMaster.EMPCategory = sEMPCategory;
                                            strHRSalaryMaster.Encashment = dEncashment;
                                            strHRSalaryMaster.SpecialAllowance = dSpecialAllowance;
                                            strHRSalaryMaster.OldEmpCode = sOldEmpCode;
                                            strHRSalaryMaster.PFPercentage = dPFPercentage;
                                            strHRSalaryMaster.ESIPercentage = dESIPercentage;
                                            strHRSalaryMaster.MonthlyBonus = dMonthlyBonus;
                                            strHRSalaryMaster.FoodAllowance = dFoodAllowance;
                                            strHRSalaryMaster.IsActive = true;
                                            strHRSalaryMaster.ValidFrom = DateTime.Now.Date; //dValidFrom;
                                            strHRSalaryMaster.SalaryMasterID = sSalaryMasterID;
                                            strHRSalaryMaster.Reason = sReason;
                                            strHRSalaryMaster.CanteenApplicable = bCanteenApplicable;
                                            strHRSalaryMaster.InsuredAmount = iInsuredAmount;
                                            strHRSalaryMaster.MonthlyInsurance = iMonthlyInsurance;
                                            strHRSalaryMaster.PFEligible = sPFEligible;
                                            strHRSalaryMaster.ESIEligible = sESIEligible;
                                            strHRSalaryMaster.SalaryType = sSalaryType;
                                            strHRSalaryMaster.RetrenchmentPercentage = dRetrenchmentPercentage;
                                            strHRSalaryMaster.RevisedGross = dRevisedGross;
                                            strHRSalaryMaster.LessValue = dLessValue;
                                            strHRSalaryMaster.HRARM = dHRARM;
                                            strHRSalaryMaster.OARM = dOARM;


                                            SqlCommand cmd1 = new SqlCommand();
                                            cmd1.Connection = conn;
                                            cmd1.CommandText = "Update HRSalaryMaster Set IsActive = @mIsActive, ValidTill = @mValidTill Where OldEmpCode = @mOldEmpCode And IsActive = @mIsActiveTrue";
                                            cmd1.CommandType = CommandType.Text;

                                            cmd1.Parameters.Add(new SqlParameter("@mOldEmpCode", SqlDbType.VarChar)).Value = sOldEmpCode;
                                            cmd1.Parameters.Add(new SqlParameter("@mIsActive", SqlDbType.Bit)).Value = false;
                                            cmd1.Parameters.Add(new SqlParameter("@mValidTill", SqlDbType.DateTime)).Value = "2021-11-30";
                                            cmd1.Parameters.Add(new SqlParameter("@mIsActiveTrue", SqlDbType.BigInt)).Value = true;


                                            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                            DataSet dsEmp1 = new DataSet();
                                            adapter1.Fill(dsEmp1);

                                            if (ccHRSalaryMaster.InsertSalaryMaster(strHRSalaryMaster) == true)
                                            {
                                                //LoadJobcardDemand(tbJobcardNo.Text.Trim());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    sErrorList = sErrorList + "Line No. " + NRowNo.ToString() + " Error :- Old Employee Code Not Exist in the Attached Excel File" + Environment.NewLine;
                                    sIsDataCorrect = "N";
                                }
                                NRowNo += 1;
                            }
                        }

                        if (sIsDataCorrect == "Y")
                        {
                            MessageBox.Show("Upload Completed");
                        }
                        else
                        {
                            MessageBox.Show("Upload Not Done");
                            using (StreamWriter stream = new FileInfo("E:\\HRUploadErrors.txt").AppendText())
                            {
                                stream.WriteLine(sErrorList);
                                stream.WriteLine("Date : " + DateTime.Now);
                                stream.WriteLine("");
                                stream.WriteLine("");
                            }
                        }

                    }
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }
    }
}





