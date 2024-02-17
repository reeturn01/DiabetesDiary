using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("doktor", Schema = "project")]
public partial class Doktor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [InverseProperty("Doktor")]
    public virtual ICollection<Faksimil> Faksimil { get; set; } = new List<Faksimil>();

    [ForeignKey("Id")]
    [InverseProperty("Doktor")]
    public virtual Covek Covek { get; set; } = null!;
}
