using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "Data")]
[Table("zapis_insulin", Schema = "project")]
public partial class ZapisInsulin
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("data")]
    public DateOnly Data { get; set; }

    [ForeignKey("DijabeticarId, Data")]
    [InverseProperty("ZapisInsulin")]
    public virtual Zapis Zapis { get; set; } = null!;

    [InverseProperty("ZapisInsulin")]
    public virtual ICollection<ZapisInsulinDoziranInsulin> ZapisInsulinDoziranInsulin { get; set; } = new List<ZapisInsulinDoziranInsulin>();
}
