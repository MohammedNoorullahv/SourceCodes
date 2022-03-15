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
    public class PurchaseOrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Purchase Order", Description = "Purchase Order")]
        [ForeignKey("PurchaseOrderMain")]
        public int FKPurchaseOrderMain { get; set; }
        public virtual PurchaseOrderMain PurchaseOrderMain { get; set; }

        [StringLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string? PurchaseOrderNo { get; set; }

        [StringLength(19)]
        [Column(TypeName = "varchar(19)")]
        public string? PurchaseOrderMainNo { get; set; }

        [StringLength(23)]
        [Column(TypeName = "varchar(23)")]
        public string? PurchaseOrderDtlNo { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ArticleGroup { get; set; }

        [Display(Name = "Article", Description = "Article")]
        [ForeignKey("GetArticle")]
        public int FKArticle{ get; set; }
        public virtual ArticleDetail GetArticle { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string ArticleDescription { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string ArticleColor { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderReferenceNo { get; set; }

        
        public int TotalOrderQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public int FKAssortmentId { get; set; }
        public int NoofCartons { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size01 { get; set; }

        public int? Quantity01 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size02 { get; set; }
        public int? Quantity02 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size03 { get; set; }
        public int? Quantity03 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size04 { get; set; }
        public int? Quantity04 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size05 { get; set; }
        public int? Quantity05 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size06 { get; set; }
        public int? Quantity06 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size07 { get; set; }
        public int? Quantity07 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size08 { get; set; }
        public int? Quantity08 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size09 { get; set; }
        public int? Quantity09 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size10 { get; set; }
        public int? Quantity10 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size11 { get; set; }
        public int? Quantity11 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size12 { get; set; }
        public int? Quantity12 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size13 { get; set; }
        public int? Quantity13 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size14 { get; set; }
        public int? Quantity14 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size15 { get; set; }
        public int? Quantity15 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size16 { get; set; }
        public int? Quantity16 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size17 { get; set; }
        public int? Quantity17 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Size18 { get; set; }
        public int? Quantity18 { get; set; }

        public DateTime DeliveryDate { get; set; }
        public Boolean IsPartDeliveryAllowed { get; set; }

        public int ReceivedQuantity { get; set; }

        public int CancelledQuantity { get; set; }

        public int BalanceQuantity { get; set; }

        [Display(Name = "PO Status", Description = "PO Status")]
        [ForeignKey("LookUpMasterPOStatus")]
        public int FKOrderStatus { get; set; }
        public virtual LookUpMaster LookUpMasterPOStatus { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderStatus { get; set; }

        [Display(Name = "U.O.M", Description = "U.O.M")]
        [ForeignKey("LookUpMasterUOM")]
        public int FKUOM { get; set; }
         public virtual LookUpMaster LookUpMasterUOM { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

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
