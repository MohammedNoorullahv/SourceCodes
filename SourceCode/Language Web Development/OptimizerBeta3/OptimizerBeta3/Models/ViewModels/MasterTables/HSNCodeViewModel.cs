using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class HSNCodeViewModel
    {
        public HSNCodeMaster HSNCodeMaster { get; set; }
        public IEnumerable<LookUpMaster> FKPercentageType { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
