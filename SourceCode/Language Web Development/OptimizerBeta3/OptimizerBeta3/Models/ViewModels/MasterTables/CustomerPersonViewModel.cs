using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class CustomerPersonViewModel
    {
        public CustomerPerson CustomerPerson { get; set; }
        public IEnumerable<LookUpMaster> FKArea { get; set; }
        public IEnumerable<LookUpMaster> FKCity { get; set; }
        public IEnumerable<LookUpMaster> FKPincode { get; set; }
        public IEnumerable<MdlStateMaster> FKState { get; set; }
        public IEnumerable<LookUpMaster> FKCountry { get; set; }
        public IEnumerable<LookUpMaster> FKCustomerOf { get; set; }
        public IEnumerable<LookUpMaster> FKGender { get; set; }
        public IEnumerable<LookUpMaster> FKMaritalStatus { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
