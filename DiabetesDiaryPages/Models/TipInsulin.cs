using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("tip_insulin", Schema = "project")]
[Index("Ime", Name = "UNQ_Tip_insulin_ime", IsUnique = true)]
public partial class TipInsulin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(256)]
    public string Ime { get; set; } = null!;

    [InverseProperty("TipInsulin")]
    public virtual ICollection<Insulin> Insulin { get; set; } = new List<Insulin>();
}
