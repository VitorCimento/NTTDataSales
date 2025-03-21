using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Entities;

public class Customer
{
    [Key, Required]
    public int ID { get; set; }

    [Required, MaxLength(70)]
    public string NAME { get; set; }

    [Required, MaxLength(14)]
    public string DOCUMENT { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PHONE { get; set; }

    [Required, DataType(DataType.EmailAddress)]
    public string EMAIL { get; set; }
}
