using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("medikament", Schema = "project")]
[Index("Ime", Name = "UNQ_medikament_ime", IsUnique = true)]
public partial class Medikament
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(256)]
    public string Ime { get; set; } = null!;

    [Column("proizvoditel")]
    [StringLength(256)]
    public string Proizvoditel { get; set; } = null!;

    [Column("tip_medikament")]
    [MaxLength(1)]
    public char TipMedikament { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Insulin? Insulin { get; set; }

    [InverseProperty("Medikament")]
    public virtual ICollection<Inventar> Inventar { get; set; } = new List<Inventar>();

    [InverseProperty("IdNavigation")]
    public virtual LentiMerenjeShekjer? LentiMerenjeShekjer { get; set; }
}
