using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("zdrastven_rezultat", Schema = "project")]
public partial class ZdrastvenRezultat
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [ForeignKey("DijabeticarId")]
    [InverseProperty("ZdrastvenRezultat")]
    public virtual Dijabeticar Dijabeticar { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("ZdrastvenRezultat")]
    public virtual DatotekaMetapodatoci DatotekaMetapodatoci { get; set; } = null!;
}
