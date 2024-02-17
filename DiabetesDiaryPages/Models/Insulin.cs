using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("insulin", Schema = "project")]
public partial class Insulin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tip_insulin_id")]
    public int? TipInsulinId { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Insulin")]
    public virtual Medikament Medikament { get; set; } = null!;

    [ForeignKey("TipInsulinId")]
    [InverseProperty("Insulin")]
    public virtual TipInsulin? TipInsulin { get; set; }

    [InverseProperty("Insulin")]
    public virtual ICollection<ZapisInsulinDoziranInsulin> ZapisInsulinDoziranInsulin { get; set; } = new List<ZapisInsulinDoziranInsulin>();
}
