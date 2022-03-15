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
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Type of Order", Description = "Type of Order")]
        [ForeignKey("LookUpMasterTypeOfOrder")]
        public int FKTypeOfOrder { get; set; }
        public virtual LookUpMaster LookUpMasterTypeOfOrder { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? TypeofOrder { get; set; }

        [Display(Name = "Season", Description = "Season")]
        [ForeignKey("FKSeasonCode")]
        public int FKSeason { get; set; }
        public virtual Season FKSeasonCode { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Season { get; set; }

        [Display(Name = "Source", Description = "Source")]
        [ForeignKey("LookUpMasterSource")]
        public int FKSource { get; set; }
        public virtual LookUpMaster LookUpMasterSource { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Source { get; set; }

        [Display(Name = "Unit", Description = "Unit")]
        [ForeignKey("UnitMaster")]
        public int FKUnit { get; set; }
        public virtual UnitMaster UnitMaster { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? UnitName { get; set; }

        [Display(Name = "Supplier", Description = "Supplier")]
        [ForeignKey("PartyInfo")]
        public int FKParty { get; set; }
        public virtual PartyInfo PartyInfo { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? SupplierName { get; set; }

        [Required]
        [StringLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string PurchaseOrderNo { get; set; }

        public DateTime PurchaseOrderDt { get; set; }
        
        [Display(Name = "PO Status", Description = "PO Status")]
        [ForeignKey("LookUpMasterPOStatus")]
        public int FKOrderStatus { get; set; }
        public virtual LookUpMaster LookUpMasterPOStatus { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderStatus { get; set; }

        [Display(Name = "Payment Terms", Description = "Payment Terms")]
        [ForeignKey("LookUpMasterPaymentTerms")]
        public int FKPaymentTerms { get; set; }
        public virtual LookUpMaster LookUpMasterPaymentTerms { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? PaymentTerms { get; set; }

        [Display(Name = "Currency", Description = "Currency")]
        [ForeignKey("LookUpMasterCurrency")]
        public int FKCurrency { get; set; }
        public virtual LookUpMaster LookUpMasterCurrency { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Currency { get; set; }

        [StringLength(250)]
        [Column(TypeName = "varchar(250)")]
        public string? Remarks { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Quantity must be a natural number only")]
        [Column(TypeName = "decimal(18,2)")]
        public int TotalOrderQuantity { get; set; }
        
        public int? EnteredQuantity { get; set; }

        public int? ReceivedQuantity { get; set; }

        public int? CancelledQuantity { get; set; }

        public int? BalanceQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? POValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExchangeRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? POValueinINR { get; set; }

        public Boolean IsPOManualClosed { get; set; }

        public Boolean IsAssortmentOrder { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? FLAM { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        public Boolean IsArticleStockGenerated { get; set; } = false;

        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Category { get; set; }

        [Display(Name = "Order Method", Description = "Order Method")]
        [ForeignKey("LookUpMasterOrderMethod")]
        public int FKOrderMethod { get; set; }
        public virtual LookUpMaster LookUpMasterOrderMethod { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderMethod { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? EnteredSystemId { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
