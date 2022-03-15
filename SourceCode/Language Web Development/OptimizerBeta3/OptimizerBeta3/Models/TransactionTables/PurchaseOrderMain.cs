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
    public class PurchaseOrderMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Purchase Order", Description = "Purchase Order")]
        [ForeignKey("PurchaseOrder")]
        public int FKPurchaseOrder { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [StringLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string? PurchaseOrderNo { get; set; }

        [StringLength(21)]
        [Column(TypeName = "varchar(21)")]
        public string? PurchaseOrderMainNo { get; set; }

        [Display(Name = "Article Group", Description = "Article Group")]
        [ForeignKey("GetArticleGroup")]
        public int FKArticleGroup { get; set; }
        public virtual ArticleGroup GetArticleGroup { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ArticleGroup { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Article { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderReferenceNo { get; set; }
        public int TotalOrderQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public DateTime DeliveryDate { get; set; }
        public Boolean IsPartDeliveryAllowed { get; set; }

        [Display(Name = "Size Master", Description = "Size Master")]
        [ForeignKey("SizeMaster")]
        public int FKSizeMaster { get; set; }
        public virtual SizeMaster SizeMaster { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? SizeCode { get; set; }

        
        public int EnteredQuantity { get; set; }
        public int ReceivedQuantity { get; set; }

        public int CancelledQuantity { get; set; }

        public int BalanceQuantity { get; set; }

        [Display(Name = "Destination", Description = "Destination")]
        [ForeignKey("LookUpMasterDestination")]
        public int FKDestination { get; set; }
        public virtual LookUpMaster LookUpMasterDestination { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Destination { get; set; }

        [Display(Name = "Delivery To", Description = "Delivery To")]
        [ForeignKey("LookUpMasterDeliveryTo")]
        public int FKDeliveryTo { get; set; }
        public virtual LookUpMaster LookUpMasterDeliveryTo { get; set; }
        
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? DeliveryTo { get; set; }

        //public int FKDeliveryLocation { get; set; }

        //[StringLength(50)]
        //[Column(TypeName = "varchar(50)")]
        //public string DeliveryLocation { get; set; }

        [Display(Name = "Mode of Transport", Description = "Mode of Transport")]
        [ForeignKey("LookUpMasterModeofTransport")]
        public int FKModeofTransport { get; set; }
        public virtual LookUpMaster LookUpMasterModeofTransport { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ModeofTransport { get; set; }

        [Display(Name = "Order Status", Description = "Order Status")]
        [ForeignKey("LookUpMasterOrderStatus")]
        public int FKOrderStatus { get; set; }
        public virtual LookUpMaster LookUpMasterOrderStatus { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderStatus { get; set; }

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
