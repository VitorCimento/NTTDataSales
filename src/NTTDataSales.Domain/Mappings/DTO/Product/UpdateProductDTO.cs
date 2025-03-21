using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.Product;

public class UpdateProductDTO
{
    [Required, MaxLength(60)]
    public string NAME { get; set; }

    [Required, MaxLength(80)]
    public string DESCRIPTION { get; set; }

    [Required, Precision(15, 2)]
    public double PRICE { get; set; }
}
