using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempStockView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string FLAM { get; set; }

        public int? FKUnit { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string UnitName { get; set; }

        public int? FKLocation { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string LocationName { get; set; }

        public int? FKStage { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Stage { get; set; }

        public int? FKUOM { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string UOM { get; set; }

        public int? FKSource { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Source { get; set; }

        public int? FKQuality { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Quality { get; set; }

        public int? FKStatus { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Status { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string StockNo { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string? EANCode { get; set; }

        public int? FKMaterial { get; set; }

        public int? FKArticleDetail { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Colour { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Size { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string OrderReferenceNo { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IPAddress { get; set; }


    }
}
