using OptimizerBeta3.Models.GeneralTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.GeneralTables
{
    public class ComplaintsViewModel
    {
        public Complaint Complaint { get; set; }
        public IEnumerable<ComplaintLookUpMaster> FKMenuCategory { get; set; }
        public IEnumerable<ComplaintLookUpMaster> FKMenuName { get; set; }
        public IEnumerable<ComplaintLookUpMaster> FKLocation { get; set; }
        public IEnumerable<ComplaintLookUpMaster> FKAdminName { get; set; }
        public IEnumerable<ComplaintLookUpMaster> FKUserName { get; set; }
        public IEnumerable<ComplaintLookUpMaster> FKStatus { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
