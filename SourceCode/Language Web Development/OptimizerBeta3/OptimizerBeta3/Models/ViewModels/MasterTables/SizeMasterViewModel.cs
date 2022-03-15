using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class SizeMasterViewModel
    {
        public SizeMaster SizeMaster { get; set; }
        public IEnumerable<LookUpMaster> FKSizeCategory { get; set; }
        public IEnumerable<LookUpMaster> FKSizeFor { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
