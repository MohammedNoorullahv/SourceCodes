using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempStockForOfferMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int FKCategory { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Category { get; set; }
        public int FKUnit { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string UnitName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string ArticleGroupName { get; set; }
        public int FKLocation { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string LocationName { get; set; }
        public int FKArticleDetail { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string ArticleDescription { get; set; }
        
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Colour { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string OrderReferenceNo { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Size { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public Decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public Decimal MRP { get; set; }
        public int FKStage { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Status { get; set; }

    }
}
