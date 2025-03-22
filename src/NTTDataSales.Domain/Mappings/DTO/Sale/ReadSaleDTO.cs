using NTTDataSales.Domain.Mappings.DTO.Branch;
using NTTDataSales.Domain.Mappings.DTO.Customer;
using NTTDataSales.Domain.Mappings.DTO.SaleItem;

namespace NTTDataSales.Domain.Mappings.DTO.Sale;

public class ReadSaleDTO
{
    public int ID { get; set; }

    public DateTime DATE { get; set; } = DateTime.Now;

    public decimal TOTALVALUE { get; set; } = 0;

    public decimal TOTALDISCOUNT { get; set; } = 0;

    public bool SALECANCELLED { get; set; } = false;

    public ReadCustomerDTO CUSTOMER  { get; set; }

    public ReadBranchDTO BRANCH { get; set; }

    public ICollection<ReadSaleItemDTO> SALEITEMS { get; set; }
}
