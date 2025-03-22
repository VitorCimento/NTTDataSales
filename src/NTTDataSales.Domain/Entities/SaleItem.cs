using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NTTDataSales.Domain.Entities;

public class SaleItem
{
    public int ID { get; set; }

    [Range(1, 20)]
    public int QUATITY { get; set; }

    [Required, Precision(12,2)]
    public decimal UNITARYPRICE { get; set; }
    
    [Required, Precision(12, 2)]
    public decimal TOTALPRICE { get; set; }
    
    [Required, Precision(12, 2)]
    public decimal DISCOUNT {  get; set; }

    [Required]
    public bool ITEMCANCELLED { get; set; } = false;

    [Required]
    public int PRODUCTID { get; set; }

    public virtual Product PRODUCT { get; set; }
}
