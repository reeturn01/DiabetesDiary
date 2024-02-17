using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("datoteka_metapodatoci", Schema = "project")]
[Index("Hash", Name = "UNQ_datoteka_metapodatoci_hash", IsUnique = true)]
public partial class DatotekaMetapodatoci
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(400)]
    public string Ime { get; set; } = null!;

    [Column("hash")]
    [StringLength(400)]
    public string Hash { get; set; } = null!;

    [Column("opis")]
    [StringLength(1000)]
    public string Opis { get; set; } = null!;

    [Column("mime")]
    [StringLength(300)]
    public string Mime { get; set; } = null!;

    [Column("tip_datoteka")]
    [MaxLength(1)]
    public char TipDatoteka { get; set; }

    [InverseProperty("DatotekaMetapodatoci")]
    public virtual Faksimil? Faksimil { get; set; }

    [InverseProperty("DatotekaMetapodatoci")]
    public virtual ZdrastvenRezultat? ZdrastvenRezultat { get; set; }
}
