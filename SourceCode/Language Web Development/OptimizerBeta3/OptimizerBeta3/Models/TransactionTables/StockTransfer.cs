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
    public class StockTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(1)")]
        public string MaterialorFinishedProduct { get; set; }

        [Display(Name = "Season", Description = "Season")]
        [ForeignKey("SeasonCode")]
        public int FKSeason { get; set; }
        public virtual Season SeasonCode { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Season { get; set; }

        [Display(Name = "FKOutwardType", Description = "FKOutwardType")]
        [ForeignKey("LookUpMasterFKOutwardType")]
        public int FKOutwardType { get; set; }
        public virtual LookUpMaster LookUpMasterFKOutwardType { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OutwardType { get; set; }

        [Display(Name = "FromUnit", Description = "FromUnit")]
        [ForeignKey("FromUnit")]
        public int FKFromUnit { get; set; }
        public virtual UnitMaster FromUnit { get; set; }

        [Display(Name = "From Unit", Description = "From Unit")]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? FromUnitName { get; set; }
        public int FKFromState { get; set; }

        [Display(Name = "From Department", Description = "From Department")]
        //[ForeignKey("LookUpMasterFromDepartment")]
        public int FKFromDepartment { get; set; }
        //public virtual LookUpMaster LookUpMasterFromDepartment { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? FromDepartment { get; set; }

        [Display(Name = "FromLocation", Description = "FromLocation")]
        [ForeignKey("LookUpMasterFromLocation")]
        public int FKFromLocation { get; set; }
        public virtual LookUpMaster LookUpMasterFromLocation { get; set; }

        [Display(Name = "From Location", Description = "From Location")]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? FromLocation{ get; set; }

        [Display(Name = "ToUnit", Description = "ToUnit")]
        [ForeignKey("ToUnit")]
        public int FKToUnit { get; set; }
        public virtual UnitMaster ToUnit { get; set; }

        [Display(Name = "To Unit", Description = "To Unit")]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ToUnitName { get; set; }
        public int FKToState { get; set; }

        [Display(Name = "To Department", Description = "To Department")]
        //[ForeignKey("LookUpMasterToDepartment")]
        public int FKToDepartment { get; set; }
        //public virtual LookUpMaster LookUpMasterToDepartment { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ToDepartment { get; set; }

        [Display(Name = "ToLocation", Description = "ToLocation")]
        [ForeignKey("LookUpMasterToLocation")]
        public int FKToLocation { get; set; }
        public virtual LookUpMaster LookUpMasterToLocation { get; set; }

        [Display(Name = "To Location", Description = "To Location")]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ToLocation { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string STNo { get; set; }
        public DateTime STDt { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal TransferredQty { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ReceivedQty { get; set; }

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

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DtlValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DifferenceValue { get; set; }

        [Column(TypeName = "Varchar(250)")]
        public string Remarks { get; set; }
        public Boolean IsEntryCompleted { get; set; }

        [Display(Name = "Quality", Description = "Quality")]
        [ForeignKey("LookUpMasterQuality")]
        public int FKQuality { get; set; }
        public virtual LookUpMaster LookUpMasterQuality { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Quality { get; set; }

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
