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
    public class InvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Invoice", Description = "Invoice")]
        [ForeignKey("Invoice")]
        public int FKInvoiceNo { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDt { get; set; }


        [Display(Name = "Material", Description = "Material")]
        //[ForeignKey("Material")]
        public int FKMaterial { get; set; }
        //public virtual Materials Material { get; set; }

        [Display(Name = "Article", Description = "Article")]
        //[ForeignKey("Article")]
        public int FKArticle { get; set; }
        //public virtual ArticleDetail Article { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Description { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Colour { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string Size { get; set; }
        public int HSNCode { get; set; }
        public int FKHSNCode { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Display(Name = "UOM", Description = "UOM")]
        [ForeignKey("LookUpMasterUOM")]
        public int FKUOM { get; set; }
        public virtual LookUpMaster LookUpMasterUOM { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IIQuantity { get; set; }

        [Display(Name = "IIUOM", Description = "IIUOM")]
        //[ForeignKey("LookUpMasterIIUOM")]
        public int FKIIUom { get; set; }
        //public virtual LookUpMaster LookUpMasterIIUOM { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ValueinINR { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal DiscountValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal GrossValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal SGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal CGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IGSTPercentage { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal IGSTValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]

        public decimal GSTTotalValue { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OthersValuePlus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal OthersValueMinus { get; set; }

        [Column(TypeName = "Decimal(18,2)")]
        public decimal ItemNettValue { get; set; }

        public Boolean IsEntryCompleted { get; set; }

        [DefaultValue(true)]
        public Boolean IsActive { get; set; } = true;

        [Column(TypeName = "varchar(30)")]
        public string? EnteredSystemId { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        [StringLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string EANCode { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StockNo { get; set; }
        public int FKCustomer { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string MaterialorFinishedProduct { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string UOM { get; set; }
    }
}
