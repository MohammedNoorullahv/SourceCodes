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
    public class AllTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string TransactionType { get; set; }

        public int TranId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string TranRefNo { get; set; }
        public DateTime TranDate { get; set; }

        public int FKTranUnit { get; set; }

        public int FKTranLocation { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string MaterialorFinishedProduct { get; set; }
        public int FKMaterial { get; set; }
        
        public int FKArticle { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Colour { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string Size { get; set; }

        public int FKFromUnit { get; set; }

        public int FKFromLocation { get; set; }

        public int FKFromStage { get; set; }

        public int FKToUnit { get; set; }

        public int FKToLocation { get; set; }

        public int FKToStage { get; set; }

        [Display(Name = "Quality", Description = "Quality")]
        [ForeignKey("LookUpMasterQuality")]
        public int FKQuality { get; set; }
        public virtual LookUpMaster LookUpMasterQuality { get; set; }

        public int HSNCode { get; set; }

        [Display(Name = "UOM", Description = "UOM")]
        [ForeignKey("LookUpMasterUOM")]
        public int FKUOM { get; set; }
        public virtual LookUpMaster LookUpMasterUOM { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal InwardQuantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OutwardQuantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal BalanceQuantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Display(Name = "Status", Description = "Status")]
        [ForeignKey("LookUpMasterStatus")]
        public int FKStatus { get; set; }
        public virtual LookUpMaster LookUpMasterStatus { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }

        [Required]
        [StringLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string EANCode { get; set; }

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
