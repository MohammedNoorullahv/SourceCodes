using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempOfferforInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OfferId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string OfferCode { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string OfferCategory { get; set; }

        public int OfferPair { get; set; }
        public int BuyPair { get; set; }
        public bool IsProductCompliment { get; set; }
        
        [Column(TypeName = "decimal(18,2)")] 
        public decimal DiscountPercentage { get; set; }
        
        [Column(TypeName = "decimal(18,2)")] 
        public decimal DiscountValue { get; set; }
        public bool ApplicableForItem { get; set; }
        public bool ApplicableForInvoice { get; set; }
        [Column(TypeName = "decimal(18,2)")] 
        public decimal MinimumBillValue { get; set; }
        
        [Column(TypeName = "decimal(18,2)")] 
        public decimal MaximumDiscountValue { get; set; }
        public bool IsVenueBased { get; set; }
        public int EstimateId { get; set; }
        public int FKArticleDtl { get; set; }
        public int FKLocation { get; set; }
        public int InvQuantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal InvValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InvDiscountValue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InvGrossValue { get; set; }
        
        [Column(TypeName = "Varchar(200)")]
        public string IncludedArticles { get; set; }
       
        [Column(TypeName = "Varchar(200)")]
        public string ExcludedArticles { get; set; }

    }
}
