using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class MaterialsViewModel
    {
        public Materials materials { get; set; }
        public IEnumerable<LookUpMaster> FKCategory { get; set; }
        public IEnumerable<LookUpMaster> FKType { get; set; }
        public IEnumerable<LookUpMaster> FKSubType { get; set; }
        public IEnumerable<LookUpMaster> FKBrand { get; set; }
        public IEnumerable<LookUpMaster> FKSource { get; set; }
        public IEnumerable<LookUpMaster> FKUom { get; set; }
        public IEnumerable<LookUpMaster> FKColour { get; set; }
        public IEnumerable<HSNCodeMaster> FKHSNCode { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
