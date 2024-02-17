using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Models;

[Table("lenti_merenje_shekjer", Schema = "project")]
public partial class LentiMerenjeShekjer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("LentiMerenjeShekjer")]
    public virtual Medikament Medikament { get; set; } = null!;
}
