using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptimizerBeta3.Models.GeneralTables
{
    public class TempOfferVenueMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int FKUnitId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string StrCode { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CompanyName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string City { get; set; }

        public int FKOffer { get; set; }

    }
}
