using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.Branch;

public class CreateBranchDTO
{
    [Required, MaxLength(30)]
    public string NAME { get; set; }
}
