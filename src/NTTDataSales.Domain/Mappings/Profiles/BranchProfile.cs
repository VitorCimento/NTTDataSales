using AutoMapper;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Branch;

namespace NTTDataSales.Domain.Mappings.Profiles;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<CreateBranchDTO, Branch>();
        CreateMap<UpdateBranchDTO, Branch>();
        CreateMap<Branch, ReadBranchDTO>();
    }
}
