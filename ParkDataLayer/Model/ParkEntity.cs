using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model
{
    public class ParkEntity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Naam { get; set; }

        [MaxLength(500)]
        public string Locatie { get; set; }

        public List<HuisEntity> Huizen { get; set; } = new List<HuisEntity>(); 
    }
}
