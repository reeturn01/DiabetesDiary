using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("tip_dijabetes", Schema = "project")]
[Index("Ime", Name = "UNQ_Tip_dijabetes_ime", IsUnique = true)]
public partial class TipDijabetes
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(256)]
    public string Ime { get; set; } = null!;

    [InverseProperty("TipDijabetes")]
    public virtual ICollection<Dijabeticar> Dijabeticar { get; set; } = new List<Dijabeticar>();
}
