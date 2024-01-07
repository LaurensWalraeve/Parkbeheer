using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.Model
{
    public class HuurcontractEntity
    {
        [Key]
        public string Id { get; set; }


        [ForeignKey("HuisId")]
        public int HuisId { get; set; } // Foreign key

        public HuisEntity Huis { get; set; } // Navigation property


        [ForeignKey("HuurderId")]
        public int HuurderId { get; set; } // Foreign key

        public HuurderEntity Huurder { get; set; } // Navigation property

        [Required]
        public int Huurperiode_Aantaldagen { get; set; }

        [Required]
        public DateTime Huurperiode_StartDatum { get; set; }

        [Required]
        public DateTime Huurperiode_EindDatum { get; set; }
    }
}
