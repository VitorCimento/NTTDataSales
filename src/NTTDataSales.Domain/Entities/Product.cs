using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NTTDataSales.Domain.Entities;

public class Product
{
    [Key, Required]
    public int ID { get; set; }

    [Required, MaxLength(60)]
    public string NAME { get; set; }

    [Required, MaxLength(80)]
    public string DESCRIPTION { get; set; }

    [Required, Precision(15,2)]
    public double PRICE { get; set; }
}
