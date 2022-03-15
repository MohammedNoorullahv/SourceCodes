using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class AreaMasterViewModel
    {
        public AreaMaster AreaMaster { get; set; }
        public IEnumerable<AreaLookUpMaster> FKCountry { get; set; }
        public IEnumerable<AreaLookUpMaster> FKState { get; set; }
        public IEnumerable<AreaLookUpMaster> FKCity { get; set; }
        public IEnumerable<AreaLookUpMaster> FKArea { get; set; }
        public IEnumerable<AreaLookUpMaster> FKPincode { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
