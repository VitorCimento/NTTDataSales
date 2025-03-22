using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Entities;

public class Sale
{
    [Key, Required]
    public int ID { get; set; }

    [Required, DataType(DataType.DateTime)]
    public DateTime DATE { get; set; } = DateTime.Now;

    [Required]
    public decimal TOTALVALUE { get; set; } = 0;

    [Required]
    public decimal TOTALDISCOUNT { get; set; } = 0;

    [Required]
    public bool SALECANCELLED { get; set; } = false;

    [Required]
    public int CUSTOMERID { get; set; }

    [Required]
    public int BRANCHID { get; set; }

    public virtual Customer CUSTOMER { get; set; }

    public virtual Branch BRANCH { get; set; }

    public virtual ICollection<SaleItem> SALEITEMS { get; set; }
}
