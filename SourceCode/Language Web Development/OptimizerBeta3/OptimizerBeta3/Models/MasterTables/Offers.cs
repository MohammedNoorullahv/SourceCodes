using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class Offers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? OfferId { get; set; }

        [Display(Name = "Offer Name", Description = "Offer Name")]
        [ForeignKey("LookUpMasterOffer")]
        public int FKOffer { get; set; }
        public virtual LookUpMaster LookUpMasterOffer { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OfferName { get; set; }

        [Display(Name = "Offer Category", Description = "Offer Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKOfferCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OfferCategory { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool IsExtendable { get; set; }

        public bool IsAnniverseryBased { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountValue { get; set; }
        public int BuyPair { get; set; }
        public int OfferPair { get; set; }
        public bool IsProductCompliment { get; set; }
        public bool IsRangeOffer { get; set; }
        public int? RangeSlNo { get; set; }
        public bool ApplicableForItem { get; set; }
        public bool ApplicableForInvoice { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal MinimumBillValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal MaximumDiscountValue { get; set; }

        [Display(Name = "Offer Type", Description = "Offer Type")]
        [ForeignKey("LookUpMasterType")]
        public int FKOfferType { get; set; }
        public virtual LookUpMaster LookUpMasterType { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OfferType { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string CouponCode { get; set; }

        public Boolean IsVenueBased { get; set; }

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
