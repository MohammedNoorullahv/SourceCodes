using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class UnitMasterViewModel
    {
        public UnitMaster UnitMaster { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
        public IEnumerable<LookUpMaster> FKUnitType { get; set; }
        public IEnumerable<LookUpMaster> FKArea { get; set; }
        public IEnumerable<LookUpMaster> FKCity { get; set; }
        public IEnumerable<LookUpMaster> FKPincode { get; set; }
        public IEnumerable<MdlStateMaster> FKState { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
