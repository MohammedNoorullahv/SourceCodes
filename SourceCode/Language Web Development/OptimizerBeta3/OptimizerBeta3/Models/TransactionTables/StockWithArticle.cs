using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.TransactionTables
{
    public class StockWithArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string StockNo { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Brand { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Product { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string ArticleNo { get; set; }

        [Column(TypeName = "varchar(60)")]
        public string ArticleDescription { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string Variant { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string ColorDescription { get; set; }
        
        [Column(TypeName = "Decimal(18,2)")]
        public decimal Size { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ItemDescription { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string ArticleName { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string LeatherType { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Group { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Dept { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string EANCode { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Type { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Category { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Vendor { get; set; }
        public DateTime DOM { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string SICM { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal MRP { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DealerPrice { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ProductTax { get; set; }
        public int Quantity { get; set; }
        public int ArrivedQty { get; set; }
        public int SoldQty { get; set; }
        public int BalQty { get; set; }
        public int FKArticleDetailId { get; set; }
        public int FKOrderDetailId { get; set; }
        public DateTime StockInitiatedDate { get; set; }
        public DateTime LastTranDate { get; set; }
        public int FKOffer { get; set; }
        public int OfferCount { get; set; }

        [Column(TypeName = "varchar(1)")]
        public string? OfferType { get; set; }

        [Display(Name = "Category", Description = "Category")]
        [ForeignKey("LookUpMasterCategory")]
        public int FKCategory { get; set; }
        public virtual LookUpMaster LookUpMasterCategory { get; set; }
        public int FKOrderMainId { get; set; }
        public int FKOrderId { get; set; }
        
        [Column(TypeName = "varchar(1)")]
        public string FLAM { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string SizeinString { get; set; }
        public int FKSupplier { get; set; }

    }
}
