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
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Invoice Type", Description = "Invoice Type")]
        [ForeignKey("LookUpMasterInvoiceType")]
        public int FKTypeOfInvoice { get; set; }
        public virtual LookUpMaster LookUpMasterInvoiceType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? TypeofInvoice { get; set; }

        [Display(Name = "Invoice Category", Description = "Invoice Category")]
        [ForeignKey("LookUpMasterInvoiceCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterInvoiceCategory { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Category { get; set; }

        [Display(Name = "Season", Description = "Season")]
        [ForeignKey("FKSeasonCode")]
        public int FKSeason { get; set; }
        public virtual Season FKSeasonCode { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Season { get; set; }

        [Display(Name = "Unit", Description = "Unit")]
        [ForeignKey("Unit")]
        public int FKUnit { get; set; }
        public virtual UnitMaster Unit { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? UnitName { get; set; }

        [Display(Name = "Party", Description = "Party")]
        //[ForeignKey("Party")]
        public int FKParty { get; set; }
        //public virtual PartyInfo Party { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CustomerName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDt { get; set; }

        public bool IncludeBillTo { get; set; }
        [Display(Name = "BillTo", Description = "Bill To")]
        //[ForeignKey("BillTo")]
        public int FKBillTo { get; set; }
        //public virtual PartyInfo BillTo { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? BillToCustomerName { get; set; }

        public bool IncludeNotifyTo { get; set; }
        [Display(Name = "NotifyTo", Description = "Notify To")]
        //[ForeignKey("NotifyTo")]
        public int FKNotifyTo { get; set; }
        //public virtual PartyInfo NotifyTo { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? NotifyToCustomerName { get; set; }

        public bool IncludeDeliveryTo { get; set; }
        [Display(Name = "DeliveryTo", Description = "Delivery To")]
        //[ForeignKey("DeliveryTo")]
        public int FKDeliveryTo { get; set; }
        //public virtual PartyInfo DeliveryTo { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? DeliveryToCustomerName { get; set; }

        [Display(Name = "PaymentTerms", Description = "PaymentTerms")]
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

        [Display(Name = "ModeofTransport", Description = "ModeofTransport")]
        [ForeignKey("LookUpMasterModeofTransport")]
        public int FKModeofTransport { get; set; }
        public virtual LookUpMaster LookUpMasterModeofTransport { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ModeofTransport { get; set; }

        [Column(TypeName = "Varchar(250)")]
        public string Remarks { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ExchangeRate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ValueinINR { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsDiscountValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsGrossValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GSTValues { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemsNettValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesPlus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesMinus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountValue { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal RoundoffPlus { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal RoundoffMinus { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal NettValue { get; set; }

        public Boolean IsEntryCompleted { get; set; }

        public Boolean IsDispatched { get; set; }
        public DateTime? DispatchedOn { get; set; }

        [Display(Name = "Location", Description = "Location")]
        [ForeignKey("LocationID")]
        public int FKLocation { get; set; }
        public virtual Locations LocationID { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Location { get; set; }

        [Display(Name = "Destination", Description = "Destination")]
        [ForeignKey("LookUpMasterDestination")]
        public int FKDestination { get; set; }
        public virtual LookUpMaster LookUpMasterDestination { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Destination { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string InvoiceTo { get; set; }

        public int FKFromState { get; set; }
        public int FKToState { get; set; }
        public int FKDeliveryChallan { get; set; }
        
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? DCNo { get; set; }
        public int FKToLocation { get; set; }
        
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ToLocation { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderRefNo { get; set; }

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
    }
}
