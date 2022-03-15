using OptimizerBeta3.Models.HRTables;
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
    public class InvoiceToPerson
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

        [Display(Name = "PersonName", Description = "PersonName")]
        [ForeignKey("FKPersonName")]
        public int FKPerson { get; set; }
        public virtual CustomerPerson FKPersonName { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? PersonName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDt { get; set; }

        public bool IncludeDeliveryTo { get; set; }
        [Display(Name = "DeliveryTo", Description = "Delivery To")]
        //[ForeignKey("DeliveryTo")]
        public int FKDeliveryTo { get; set; }
        //public virtual PartyInfo DeliveryTo { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? DeliveryToCustomerName { get; set; }


        [Column(TypeName = "Varchar(250)")]
        public string Remarks { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemDtlGrossAmount { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemDtlDiscountValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")] 
        public decimal ItemDtlTaxableValue { get; set; }
        [Column(TypeName = "Decimal(18,2)")] 
        public decimal ItemDtlGSTValue { get; set; }
        [Column(TypeName = "Decimal(18,2)")] 
        public decimal ItemDtlNettValue { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")] 
        public decimal InvDiscountPercentagePWise { get; set; }
        [Column(TypeName = "Decimal(18,2)")] 
        public decimal InvDiscountValuePWise { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal InvDiscountPercentageBWise { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal InvDiscountValueBWise { get; set; }

        [Column(TypeName = "Decimal(18,2)")] 
        public decimal InvOtherCharges { get; set; }
        [Column(TypeName = "Decimal(18,2)")] 
        public decimal InvRoundOff { get; set; }
        [Column(TypeName = "Decimal(18,2)")] 
        public decimal InvNettValue { get; set; }

        public Boolean IsEntryCompleted { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string CardInfo { get; set; }
        public int CardNo { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CardValue { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string UPIInfo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string UPITranNo { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal UPIValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CashReceived { get; set; }


        [Column(TypeName = "Decimal(18,2)")]
        public decimal BalCashToPay { get; set; }

        public int FKEstimate { get; set; }

        [Display(Name = "SalesPerson", Description = "SalesPerson")]
        [ForeignKey("FKSalesPersonId")]
        public int FKSalesPerson { get; set; }
        public virtual Employee FKSalesPersonId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? SalesPerson { get; set; }


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
