using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class OfferDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Offer", Description = "Offer Name")]
        [ForeignKey("Offers")]
        public int FKOffer { get; set; }
        public virtual Offers Offers { get; set; }

        [Display(Name = "Anniversery Info", Description = "Anniversery Info")]
        [ForeignKey("LookUpMasterAnniverseryInfo")]
        public int FKAnniverseryInfo { get; set; }
        public virtual LookUpMaster LookUpMasterAnniverseryInfo { get; set; }

        [Required]
        [Column(TypeName = "Decimal(18,2)")]
        public decimal RangeSubSlNo { get; set; }

        [Required]
        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ValueFrom { get; set; }


        [Column(TypeName = "Decimal(18,2)")]
        public decimal ValueTo { get; set; }
        public bool? IsLastRange { get; set; }

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
