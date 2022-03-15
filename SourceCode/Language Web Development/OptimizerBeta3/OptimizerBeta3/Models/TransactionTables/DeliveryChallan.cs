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
    public class DeliveryChallan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Season", Description = "Season")]
        [ForeignKey("SeasonCode")]
        public int FKSeason { get; set; }
        public virtual Season SeasonCode { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Season { get; set; }

        [Display(Name = "FKDCType", Description = "FKDCType")]
        [ForeignKey("LookUpMasterFKDCType")]
        public int FKDCType { get; set; }
        public virtual LookUpMaster LookUpMasterFKDCType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? DCType { get; set; }

        [Display(Name = "From Unit", Description = "From Unit")]
        [ForeignKey("FromUnit")]
        public int FKFromUnit { get; set; }
        public virtual UnitMaster FromUnit { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? FromUnitName { get; set; }
        public int FKFromState { get; set; }

        [Display(Name = "From Location", Description = "From Location")]
        [ForeignKey("LookUpMasterFromLocation")]
        public int FKFromLocation { get; set; }
        public virtual LookUpMaster LookUpMasterFromLocation { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? FromLocation { get; set; }

        [Display(Name = "To Unit", Description = "To Unit")]
        //[ForeignKey("ToUnit")]
        public int FKToUnit { get; set; }
        //public virtual UnitMaster ToUnit { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ToUnitName { get; set; }
        public int FKToState { get; set; }

        [Display(Name = "To Location", Description = "To Location")]
        public int FKToLocation { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ToLocation { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string DCNo { get; set; }
        public DateTime DCDt { get; set; }

        [Display(Name = "ModeofTransport", Description = "ModeofTransport")]
        [ForeignKey("LookUpMasterModeofTransport")]
        public int FKModeofTransport { get; set; }
        public virtual LookUpMaster LookUpMasterModeofTransport { get; set; }
        
        [Column(TypeName = "varchar(20)")]
        public string ModeofTranspoft { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string VehicleNo { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GSTValues { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal GrossValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesPlus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OtherExpensesMinus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal NettValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DtlValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DifferenceValue { get; set; }

        [Column(TypeName = "Varchar(250)")]
        public string? Remarks { get; set; }
        public Boolean IsEntryCompleted { get; set; }


        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string InvoiceTo { get; set; }
        
        public int FKCategory { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Category { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(30)")]
        public string Address { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")] 
        public string Area { get; set; }
        
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")] 
        public string City { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string ToState { get; set; }
        public int Pincode { get; set; }
        
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")] 
        public string GSTNo { get; set; }


        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        [Column(TypeName = "varchar(30)")]
        public string? EnteredSystemId { get; set; }
        public bool IsAcknowledged { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
