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
    public class LGPurchaseOrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Purchase Order", Description = "Purchase Order")]
        [ForeignKey("PurchaseOrderMain")]
        public int FKPurchaseOrderMain { get; set; }
        public virtual LGPurchaseOrderMain PurchaseOrderMain { get; set; }

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
        public string LeatherGoodsGroup { get; set; }

        [Display(Name = "LeatherGoods", Description = "LeatherGoods")]
        [ForeignKey("GetLeatherGoods")]
        public int FKLeatherGoods { get; set; }
        public virtual LeatherGoodsDetail GetLeatherGoods { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string LeatherGoodsDescription { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string LeatherGoodsColor { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderReferenceNo { get; set; }

        [Display(Name = "LeatherGoodsSize", Description = "LeatherGoodsSize")]
        [ForeignKey("GetLeatherGoodsSize")]
        public int FKLGSize { get; set; }
        public virtual SizeMasterforLeatherGoods GetLeatherGoodsSize { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string LeatherGoodsSize { get; set; }
        
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Quantity should be Integer only")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

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
