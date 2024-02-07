using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "Data", "HranaId")]
[Table("obrok", Schema = "project")]
public partial class Obrok
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("data")]
    public DateOnly Data { get; set; }

    [Key]
    [Column("hrana_id")]
    public int HranaId { get; set; }

    [Column("kolicina")]
    public int Kolicina { get; set; }

    [ForeignKey("HranaId")]
    [InverseProperty("Obrok")]
    public virtual Hrana Hrana { get; set; } = null!;

    [ForeignKey("DijabeticarId, Data")]
    [InverseProperty("Obrok")]
    public virtual ZapisHrana ZapisHrana { get; set; } = null!;
}
