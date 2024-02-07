using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("nacin_prigotvuvanje_hrana", Schema = "project")]
[Index("Ime", Name = "UNQ_Nacin_prigotvuvanje_hrana_ime", IsUnique = true)]
public partial class NacinPrigotvuvanjeHrana
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(256)]
    public string Ime { get; set; } = null!;

    [InverseProperty("NacinPrigotvuvanjeHrana")]
    public virtual ICollection<Hrana> Hrana { get; set; } = new List<Hrana>();
}
