using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("hrana", Schema = "project")]
public partial class Hrana
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(500)]
    public string Ime { get; set; } = null!;

    [Column("proizvoditel")]
    [StringLength(1000)]
    public string Proizvoditel { get; set; } = null!;

    [Column("glikemiski_indeks")]
    public int GlikemiskiIndeks { get; set; }

    [Column("proteini")]
    [Precision(3, 1)]
    public decimal Proteini { get; set; }

    [Column("shekjeri")]
    [Precision(3, 1)]
    public decimal Shekjeri { get; set; }

    [Column("masti")]
    [Precision(3, 1)]
    public decimal Masti { get; set; }

    [Column("jaglehidrati")]
    [Precision(3, 1)]
    public decimal Jaglehidrati { get; set; }

    [Column("kalorii")]
    public int Kalorii { get; set; }

    [Column("nacin_prigotvuvanje_hrana_id")]
    public int NacinPrigotvuvanjeHranaId { get; set; }

    [ForeignKey("NacinPrigotvuvanjeHranaId")]
    [InverseProperty("Hrana")]
    public virtual NacinPrigotvuvanjeHrana NacinPrigotvuvanjeHrana { get; set; } = null!;

    [InverseProperty("Hrana")]
    public virtual ICollection<Obrok> Obrok { get; set; } = new List<Obrok>();
}
