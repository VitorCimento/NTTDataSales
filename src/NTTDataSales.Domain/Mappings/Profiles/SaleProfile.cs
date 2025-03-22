using AutoMapper;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Sale;

namespace NTTDataSales.Domain.Mappings.Profiles;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<CreateSaleDTO, Sale>();
        CreateMap<UpdateSaleDTO, Sale>();
        CreateMap<Sale, ReadSaleDTO>();
        CreateMap<Sale, UpdateSaleDTO>();
    }
}
