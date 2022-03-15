using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class FilterViewModel
    {
        public Filter Filter { get; set; }
        public IEnumerable<LookUpCategory> FKLookUpCategoryId { get; set; }
        public IEnumerable<LookUpMaster> FKLookUpMasterId { get; set; }
        public IEnumerable<LookUpMaster> TableName { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
