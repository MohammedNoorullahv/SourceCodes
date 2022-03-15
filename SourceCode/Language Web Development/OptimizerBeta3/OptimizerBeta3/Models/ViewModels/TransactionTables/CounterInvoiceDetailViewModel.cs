﻿using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.ViewModels.TransactionTables
{
    public class CounterInvoiceDetailViewModel
    {
        public InvoiceToPersonDetail InvoiceDetail { get; set; }
        public InvoiceToPerson Invoice { get; set; }
        public IEnumerable<Materials> FKMaterial { get; set; }
        public IEnumerable<ArticleDetail> FKArticle { get; set; }
        public string StatusMessage { get; set; }
        public int Id { get; set; }
    }
}
