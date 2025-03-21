using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.Customer;

public class ReadCustomerDTO
{
    public int ID { get; set; }

    public string NAME { get; set; }

    public string DOCUMENT { get; set; }

    public string PHONE { get; set; }

    public string EMAIL { get; set; }
}
