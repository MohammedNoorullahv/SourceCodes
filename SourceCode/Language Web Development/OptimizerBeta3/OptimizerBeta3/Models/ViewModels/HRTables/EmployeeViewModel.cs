using OptimizerBeta3.Models.HRTables;
using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.HRTables
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<UnitMaster> FKUnit { get; set; }
        public IEnumerable<LookUpMaster> FKSalutation { get; set; }
        public IEnumerable<LookUpMaster> FKMaritalStatus { get; set; }
        public IEnumerable<LookUpMaster> FKArea { get; set; }
        public IEnumerable<LookUpMaster> FKCity { get; set; }
        public IEnumerable<LookUpMaster> FKPincode { get; set; }
        public IEnumerable<MdlStateMaster> FKState { get; set; }
        public IEnumerable<LookUpMaster> FKDOBProofType { get; set; }
        public IEnumerable<LookUpMaster> FKDepartment { get; set; }
        public IEnumerable<LookUpMaster> FKDesignation { get; set; }
        public IEnumerable<LookUpMaster> FKEmpCategory { get; set; }
        public IEnumerable<LookUpMaster> FKReligion { get; set; }
        public IEnumerable<LookUpMaster> FKQualification { get; set; }
        public IEnumerable<LookUpMaster> FKGender { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
        










    }
}
