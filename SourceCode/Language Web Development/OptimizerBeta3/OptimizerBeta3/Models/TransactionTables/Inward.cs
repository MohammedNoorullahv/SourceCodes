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
    public class Inward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        [ForeignKey("Unit")]
        public int FKUnit { get; set; }
        public virtual UnitMaster Unit { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? UnitName { get; set; }

        [Display(Name = "Party", Description = "Party")]
        [ForeignKey("Party")]
        public int FKParty { get; set; }
        public virtual PartyInfo Party { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? SupplierName { get; set; }

        [Display(Name = "Department", Description = "Department")]
        [ForeignKey("LookUpMasterDepartment")]
        public int FKDepartment { get; set; }
        public virtual LookUpMaster LookUpMasterDepartment { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Department { get; set; }

        [Display(Name = "POType", Description = "POType")]
        [ForeignKey("LookUpMasterPOType")]
        public int FKPOType { get; set; }
        public virtual LookUpMaster LookUpMasterPOType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? POType { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string InwardNo { get; set; }
        public DateTime InwardDt { get; set; }

        [Display(Name = "DocumentType", Description = "DocumentType")]
        [ForeignKey("LookUpMasterDocumentType")]
        public int FKDocumentType { get; set; }
        public virtual LookUpMaster LookUpMasterDocumentType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? DocumentType { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string SupplierDocumentNo { get; set; }

        public DateTime SupplierDocumentDt { get; set; }

        [Display(Name = "Currency", Description = "Currency")]
        [ForeignKey("LookUpMasterCurrency")]
        public int FKCurrency { get; set; }
        public virtual LookUpMaster LookUpMasterCurrency { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Currency { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ExchangeRate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal InvoiceGrossValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GSTValues { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesPlus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesMinus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal InvoiceNettValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal InvoiceDtlValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DifferenceValue { get; set; }
        
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Remarks { get; set; }

        [Display(Name = "Location", Description = "Location")]
        //[ForeignKey("LocationID")]
        public int FKLocation { get; set; }
        //public virtual Locations LocationID { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Location { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Quantity must be a natural number only")]

        //[RegularExpression(@"\d + (\.\d{1, 2})?", ErrorMessage = "Quantity must be a natural number only")]
        public int DocumentQuantity { get; set; }
        public int ArrivedQuantity { get; set; }


        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Category { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? FLAM { get; set; }

        [Display(Name = "Quality", Description = "Quality")]
        [ForeignKey("LookUpMasterQuality")]
        public int FKQuality { get; set; }
        public virtual LookUpMaster LookUpMasterQuality { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Quality { get; set; }

        public Boolean IsEntryCompleted { get; set; }

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
