using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "MedikamentId", "RokNaTraenje")]
[Table("inventar", Schema = "project")]
public partial class Inventar
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("medikament_id")]
    public int MedikamentId { get; set; }

    [Key]
    [Column("rok_na_traenje")]
    public DateOnly RokNaTraenje { get; set; }

    [Column("kolicina")]
    public int Kolicina { get; set; }

    [ForeignKey("DijabeticarId")]
    [InverseProperty("Inventar")]
    public virtual Dijabeticar Dijabeticar { get; set; } = null!;

    [ForeignKey("MedikamentId")]
    [InverseProperty("Inventar")]
    public virtual Medikament Medikament { get; set; } = null!;
}
