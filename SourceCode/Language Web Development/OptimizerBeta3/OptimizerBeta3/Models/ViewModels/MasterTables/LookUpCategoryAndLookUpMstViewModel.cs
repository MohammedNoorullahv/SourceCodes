using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class LookUpCategoryAndLookUpMstViewModel
    {
        public IEnumerable<LookUpCategory> lookUpCategorieslist { get; set; }
        public LookUpMaster LookUpMasters { get; set; }
        public List<String> LookUpMstList { get; set; }
        public string StatusMessage { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDt { get; set; }
        public int Id { get; set; }
    }
}
