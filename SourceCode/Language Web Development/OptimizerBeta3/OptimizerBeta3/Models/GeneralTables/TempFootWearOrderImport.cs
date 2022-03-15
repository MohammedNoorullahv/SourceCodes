using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempFootWearOrderImport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SlNo { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string OrderNo { get; set; }
        public DateTime OrderDt { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string SizeCode { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string ArticleCode { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string SKU { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Article { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Color { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Group { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Size01 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size02 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size03 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size04 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size05 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size06 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size07 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size08 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size09 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size10 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size11 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size12 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size13 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size14 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size15 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size16 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size17 { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Size18 { get; set; }

        public int Quantity01 { get; set; }
        public int Quantity02 { get; set; }
        public int Quantity03 { get; set; }
        public int Quantity04 { get; set; }
        public int Quantity05 { get; set; }
        public int Quantity06 { get; set; }
        public int Quantity07 { get; set; }
        public int Quantity08 { get; set; }
        public int Quantity09 { get; set; }
        public int Quantity10 { get; set; }
        public int Quantity11 { get; set; }
        public int Quantity12 { get; set; }
        public int Quantity13 { get; set; }
        public int Quantity14 { get; set; }
        public int Quantity15 { get; set; }
        public int Quantity16 { get; set; }
        public int Quantity17 { get; set; }
        public int Quantity18 { get; set; }
        public int Total { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Remarks1 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Remarks2 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Remarks3 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Remarks4 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Remarks5 { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string status { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string IPAddress { get; set; }
        public int POId { get; set; }


    }
}
