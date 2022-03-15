using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class SalesPromotionOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Offer Name", Description = "Offer Name")]
        [ForeignKey("LookUpMasterOffer")]
        public int FKOffer { get; set; }
        public virtual LookUpMaster LookUpMasterOffer { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OfferName { get; set; }

        [Display(Name = "Party", Description = "Party")]
        [ForeignKey("Party")]
        public int FKParty { get; set; }
        public virtual PartyInfo Party { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CustomerName { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool IsExtendable { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal MinimumBillValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal MaximumDiscountValue { get; set; }

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
