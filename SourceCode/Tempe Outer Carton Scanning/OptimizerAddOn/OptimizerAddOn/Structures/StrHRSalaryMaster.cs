using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptimizerAddOn.Structures
{
    public class StrHRSalaryMaster
    {
        public string ID { get; set; }
        public string slno { get; set; }
        public string Empcode { get; set; }
        public string Empname { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public decimal Basic { get; set; }
        public decimal DA { get; set; }
        public decimal HRA { get; set; }
        public decimal Conveyance { get; set; }
        public decimal LTA { get; set; }
        public decimal MA { get; set; }
        public decimal WA { get; set; }
        public decimal BPA { get; set; }
        public decimal EA { get; set; }
        public decimal OA { get; set; }
        public decimal Gross { get; set; }
        public decimal NetAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EnteredOnMachineID { get; set; }
        public string ModuleName { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string Unit { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string UnitName { get; set; }
        public decimal ExcessPF { get; set; }
        public string EMPCategory { get; set; }
        public decimal Encashment { get; set; }
        public decimal SpecialAllowance { get; set; }
        public string OldEmpCode { get; set; }
        public decimal PFPercentage { get; set; }
        public decimal ESIPercentage { get; set; }
        public decimal MonthlyBonus { get; set; }
        public decimal FoodAllowance { get; set; }
        public bool IsActive { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        public string SalaryMasterID { get; set; }
        public string Reason { get; set; }
        public bool CanteenApplicable { get; set; }
        public int InsuredAmount { get; set; }
        public int MonthlyInsurance { get; set; }
        public string PFEligible { get; set; }
        public string ESIEligible { get; set; }
        public string SalaryType { get; set; }
        public decimal RetrenchmentPercentage { get; set; }
        public decimal RevisedGross { get; set; }
        public decimal LessValue { get; set; }
        public decimal HRARM { get; set; }
        public decimal OARM { get; set; }

    }
}
