using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "Data")]
[Table("zapis_so_izmeren_sekjer", Schema = "project")]
public partial class ZapisSoIzmerenSekjer
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("data")]
    public DateOnly Data { get; set; }

    [Column("vrednost")]
    [Precision(4, 1)]
    public decimal Vrednost { get; set; }

    [ForeignKey("DijabeticarId, Data")]
    [InverseProperty("ZapisSoIzmerenSekjer")]
    public virtual Zapis Zapis { get; set; } = null!;
}
