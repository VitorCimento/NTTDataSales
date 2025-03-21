using System.ComponentModel.DataAnnotations;

namespace NTTDataSales.Domain.Mappings.DTO.Branch;

public class UpdateBranchDTO
{
    [Required, MaxLength(30)]
    public string NAME { get; set; }
}
