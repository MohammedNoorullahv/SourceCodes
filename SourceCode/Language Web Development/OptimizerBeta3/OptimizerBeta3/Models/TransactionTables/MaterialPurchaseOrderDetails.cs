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
    public class MaterialPurchaseOrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Purchase Order", Description = "Purchase Order")]
        [ForeignKey("MaterialPurchaseOrder")]
        public int FKPurchaseOrder { get; set; }
        public virtual MaterialPurchaseOrder MaterialPurchaseOrder { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? PurchaseOrderNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderReferenceNo { get; set; }

        [Display(Name = "Material", Description = "Material")]
        [ForeignKey("MaterialId")]
        public int FKMaterial { get; set; }
        public virtual Materials MaterialId { get; set; }
        
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Material { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string UOM { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public DateTime DeliveryDate { get; set; }
        public Boolean IsPartDeliveryAllowed { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ReceivedQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CancelledQuantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceQuantity { get; set; }

        [Display(Name = "PO Status", Description = "PO Status")]
        [ForeignKey("LookUpMasterPOStatus")]
        public int FKOrderStatus { get; set; }
        public virtual LookUpMaster LookUpMasterPOStatus { get; set; }
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
