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
    public class StockTransferDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "StockTransfer", Description = "StockTransfer")]
        [ForeignKey("StockTransfer")]
        public int FKSTNo { get; set; }
        public virtual StockTransfer StockTransfer { get; set; }
        
        [Column(TypeName = "varchar(20)")]
        public string STNo { get; set; }
        public DateTime STDt { get; set; }

        [Display(Name = "Material", Description = "Material")]
        public int FKMaterial { get; set; }

        [Display(Name = "Article", Description = "Article")]
        public int FKArticleDetail { get; set; }

        [StringLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string EANCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string MaterialorFinishedProduct { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Colour { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string Size { get; set; }
        public int HSNCode { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DispatchedQuantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ReceivedQuantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DifferenceQuantity { get; set; }

        [Display(Name = "UOM", Description = "UOM")]
        [ForeignKey("LookUpMasterUOM")]
        public int FKUOM { get; set; }
        public virtual LookUpMaster LookUpMasterUOM { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IIQuantity { get; set; }

        [Display(Name = "IIUOM", Description = "IIUOM")]
        public int FKIIUom { get; set; }
        

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]

        public decimal GSTTotalValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OthersValuePlus { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal OthersValueMinus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemNettValue { get; set; }

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
