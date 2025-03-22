using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.SaleItem;

public class ReadSaleItemDTO
{
    public int ID { get; set; }

    public int QUATITY { get; set; }

    public decimal UNITARYPRICE { get; set; }

    public decimal TOTALPRICE { get; set; }

    public decimal DISCOUNT { get; set; }

    public bool ITEMCANCELLED { get; set; } = false;

    public int PRODUCTID { get; set; }
}
