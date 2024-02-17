using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("covek", Schema = "project")]
[Index("Email", Name = "UNQ_covek_email", IsUnique = true)]
[Index("Embg", Name = "UNQ_covek_embg", IsUnique = true)]
public partial class Covek
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("embg")]
    [StringLength(13)]
    public string Embg { get; set; } = null!;

    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Column("ime")]
    [StringLength(256)]
    public string Ime { get; set; } = null!;

    [Column("prezime")]
    [StringLength(256)]
    public string Prezime { get; set; } = null!;

    [Column("pol")]
    [MaxLength(1)]
    public char Pol { get; set; }

    [Column("datum_na_ragjanje")]
    public DateOnly DatumNaRagjanje { get; set; }

    [InverseProperty("Covek")]
    public virtual Dijabeticar? Dijabeticar { get; set; }

    [InverseProperty("Covek")]
    public virtual Doktor? Doktor { get; set; }
}
