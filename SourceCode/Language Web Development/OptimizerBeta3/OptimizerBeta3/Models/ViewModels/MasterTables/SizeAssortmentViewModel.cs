using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class SizeAssortmentViewModel
    {
        public SizeMaster FKSizeMaster { get; set; }
        public SizeAssortment SizeAssortment{ get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
