using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("faksimil", Schema = "project")]
public partial class Faksimil
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("doktor_id")]
    public int DoktorId { get; set; }

    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [ForeignKey("DijabeticarId")]
    [InverseProperty("Faksimil")]
    public virtual Dijabeticar Dijabeticar { get; set; } = null!;

    [ForeignKey("DoktorId")]
    [InverseProperty("Faksimil")]
    public virtual Doktor Doktor { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("Faksimil")]
    public virtual DatotekaMetapodatoci IdNavigation { get; set; } = null!;
}
