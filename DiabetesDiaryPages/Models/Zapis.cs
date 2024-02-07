using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "Data")]
[Table("zapis", Schema = "project")]
public partial class Zapis
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("data")]
    public DateOnly Data { get; set; }

    [ForeignKey("DijabeticarId")]
    [InverseProperty("Zapis")]
    public virtual Dijabeticar Dijabeticar { get; set; } = null!;

    [InverseProperty("Zapis")]
    public virtual ZapisHrana? ZapisHrana { get; set; }

    [InverseProperty("Zapis")]
    public virtual ZapisInsulin? ZapisInsulin { get; set; }

    [InverseProperty("Zapis")]
    public virtual ZapisSoIzmerenSekjer? ZapisSoIzmerenSekjer { get; set; }
}
