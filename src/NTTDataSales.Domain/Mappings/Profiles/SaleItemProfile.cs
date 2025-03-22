using AutoMapper;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.SaleItem;

namespace NTTDataSales.Domain.Mappings.Profiles;

public class SaleItemProfile : Profile
{
    public SaleItemProfile()
    {
        CreateMap<CreateSaleItemDTO, SaleItem>();
        CreateMap<UpdateSaleItemDTO, SaleItem>();
        CreateMap<SaleItem, ReadSaleItemDTO>();
    }
}
