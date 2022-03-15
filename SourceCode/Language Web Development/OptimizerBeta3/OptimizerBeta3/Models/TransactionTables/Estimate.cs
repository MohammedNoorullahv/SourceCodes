using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.TransactionTables
{
    public class Estimate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string EstimateNo { get; set; }
        public DateTime EstimateDt { get; set; }

        [Display(Name = "Store", Description = "Store")]
        [ForeignKey("Store")]
        public int FKStore { get; set; }
        public virtual UnitMaster Store { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? StoreName { get; set; }
        public int FKFromState { get; set; }

        [Display(Name = "Location", Description = "Location")]
        [ForeignKey("Location")]
        public int FKLocation { get; set; }
        public virtual Locations Location { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? LocationName { get; set; }

        public int ItemsCount { get; set; }
        public int Quantity { get; set; }
        public int QuantityForOC { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GrossValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GSTValues { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesPlus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesMinus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal NettValue { get; set; }

        public int FKInvoice { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? InvoiceNo { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        [Column(TypeName = "varchar(30)")]
        public string? EnteredSystemId { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsNettValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsDiscountValuePWise { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsDiscountValueBWise { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsGrossValue { get; set; }
        public int? FKOffer { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal OfferPercentagePWise { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal OfferValuePWise { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OfferPercentageBWise { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal OfferValueBWise { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal RoundOff { get; set; }
        
    }
}
