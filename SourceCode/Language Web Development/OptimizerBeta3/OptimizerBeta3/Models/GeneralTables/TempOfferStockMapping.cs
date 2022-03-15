using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempOfferStockMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ArtId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string ArticleGroup { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ArticleName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ArticleColor { get; set; }
        public int Stock { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string OfferAddMode { get; set; }
        public int OfferId { get; set; }
        
    }
}
