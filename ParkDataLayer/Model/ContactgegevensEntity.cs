using System.ComponentModel.DataAnnotations;

namespace ParkDataLayer.Model
{
    public class ContactgegevensEntity
    {
        [MaxLength(100)]
        public string Adres { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Tel { get; set; }
    }
}
