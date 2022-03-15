using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.MasterTables
{
    public class LocationViewModel
    {
        public UnitMaster UnitMaster { get; set; }
        public Locations Locations { get; set; }
        public IEnumerable<LookUpMaster> FKLocationType { get; set; }
        
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
