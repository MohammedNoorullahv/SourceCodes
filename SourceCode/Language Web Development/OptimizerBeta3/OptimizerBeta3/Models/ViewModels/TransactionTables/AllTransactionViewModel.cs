using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class AllTransactionViewModel
    {
        public AllTransaction AllTransaction { get; set; }
        
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
