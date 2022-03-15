using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.MasterTables
{
    public class OfferDtlStockMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Display(Name = "Offer Id", Description = "Offer Id")]
        [ForeignKey("Offer")]
        public int FKOffer { get; set; }
        public virtual Offers Offer { get; set; }

        public int FKArticleGroup { get; set; }
        public int FKArticle { get; set; }
        public int FKArticleDtl { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string? StockNo { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? EANCode { get; set; }
        
        [Column(TypeName = "varchar(5)")]
        public string OfferAddMode { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? ArticleGroup { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string? ArticleName { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string? ArticleColor { get; set; }
        
        [Column(TypeName = "varchar(20)")]
        public string? Stock { get; set; }

        
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
