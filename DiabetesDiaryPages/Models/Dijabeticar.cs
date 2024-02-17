using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("dijabeticar", Schema = "project")]
public partial class Dijabeticar
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("data_na_otkrivanje")]
    public DateOnly DataNaOtkrivanje { get; set; }

    [Column("tip_dijabetes_id")]
    public int TipDijabetesId { get; set; }

    [InverseProperty("Dijabeticar")]
    public virtual ICollection<Faksimil> Faksimil { get; set; } = new List<Faksimil>();

    [ForeignKey("Id")]
    [InverseProperty("Dijabeticar")]
    public virtual Covek Covek { get; set; } = null!;

    [InverseProperty("Dijabeticar")]
    public virtual ICollection<Inventar> Inventar { get; set; } = new List<Inventar>();

    [ForeignKey("TipDijabetesId")]
    [InverseProperty("Dijabeticar")]
    public virtual TipDijabetes TipDijabetes { get; set; } = null!;

    [InverseProperty("Dijabeticar")]
    public virtual ICollection<Zapis> Zapis { get; set; } = new List<Zapis>();

    [InverseProperty("Dijabeticar")]
    public virtual ICollection<ZdrastvenRezultat> ZdrastvenRezultat { get; set; } = new List<ZdrastvenRezultat>();
}
