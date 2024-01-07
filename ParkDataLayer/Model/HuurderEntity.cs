using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model
{
    public class HuurderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Naam { get; set; }

        public ContactgegevensEntity Contactgegevens { get; set; } // Complex type

        public List<HuurcontractEntity> Huurcontracten { get; set; } = new List<HuurcontractEntity>();
    }
}
