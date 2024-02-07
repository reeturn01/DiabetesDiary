using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "Data")]
[Table("zapis_hrana", Schema = "project")]
public partial class ZapisHrana
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("data")]
    public DateOnly Data { get; set; }

    [InverseProperty("ZapisHrana")]
    public virtual ICollection<Obrok> Obrok { get; set; } = new List<Obrok>();

    [ForeignKey("DijabeticarId, Data")]
    [InverseProperty("ZapisHrana")]
    public virtual Zapis Zapis { get; set; } = null!;
}
