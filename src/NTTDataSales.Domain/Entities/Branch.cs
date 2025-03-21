using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Entities;

public class Branch
{
    [Key, Required]
    public int ID { get; set; }

    [Required, MaxLength(30)]
    public string NAME { get; set; }
}
