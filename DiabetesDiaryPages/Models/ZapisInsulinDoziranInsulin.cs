using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[PrimaryKey("DijabeticarId", "Data", "InsulinId")]
[Table("zapis_insulin_doziran_insulin", Schema = "project")]
public partial class ZapisInsulinDoziranInsulin
{
    [Key]
    [Column("dijabeticar_id")]
    public int DijabeticarId { get; set; }

    [Key]
    [Column("data")]
    public DateOnly Data { get; set; }

    [Key]
    [Column("insulin_id")]
    public int InsulinId { get; set; }

    [Column("kolicina")]
    public int Kolicina { get; set; }

    [ForeignKey("InsulinId")]
    [InverseProperty("ZapisInsulinDoziranInsulin")]
    public virtual Insulin Insulin { get; set; } = null!;

    [ForeignKey("DijabeticarId, Data")]
    [InverseProperty("ZapisInsulinDoziranInsulin")]
    public virtual ZapisInsulin ZapisInsulin { get; set; } = null!;
}
