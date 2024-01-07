using ParkDataLayer.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HuisEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Straat { get; set; }

    [Required]
    public int Nr { get; set; }

    [Required]
    public bool Actief { get; set; }

    [ForeignKey("ParkId")]
    public string ParkId { get; set; }
    public ParkEntity Park { get; set; } 

    public List<HuurcontractEntity> Huurcontracten { get; set; } = new List<HuurcontractEntity>();
}
