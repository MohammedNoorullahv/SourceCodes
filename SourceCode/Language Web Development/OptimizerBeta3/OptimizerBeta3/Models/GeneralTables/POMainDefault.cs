using OptimizerBeta3.Models.MasterTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class POMainDefault
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsPartDeliveryAllowed { get; set; }
        [Display(Name = "Destination", Description = "Destination")]
        [ForeignKey("LookUpMasterDestination")]
        public int FKDestination { get; set; }
        public virtual LookUpMaster LookUpMasterDestination { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Destination { get; set; }

        [Display(Name = "Delivery To", Description = "Delivery To")]
        [ForeignKey("LookUpMasterDeliveryTo")]
        public int FKDeliveryTo { get; set; }
        public virtual LookUpMaster LookUpMasterDeliveryTo { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? DeliveryTo { get; set; }
        [Display(Name = "Mode of Transport", Description = "Mode of Transport")]
        [ForeignKey("LookUpMasterModeofTransport")]
        public int FKModeofTransport { get; set; }
        public virtual LookUpMaster LookUpMasterModeofTransport { get; set; }
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? ModeofTransport { get; set; }

        [Display(Name = "Order Status", Description = "Order Status")]
        [ForeignKey("LookUpMasterOrderStatus")]
        public int FKOrderStatus { get; set; }
        public virtual LookUpMaster LookUpMasterOrderStatus { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? OrderStatus { get; set; }

    }
}
