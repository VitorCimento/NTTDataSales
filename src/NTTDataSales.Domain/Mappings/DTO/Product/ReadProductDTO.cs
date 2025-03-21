using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.Product;

public class ReadProductDTO
{
    public int ID { get; set; }

    public string NAME { get; set; }

    public string DESCRIPTION { get; set; }

    public double PRICE { get; set; }
}
