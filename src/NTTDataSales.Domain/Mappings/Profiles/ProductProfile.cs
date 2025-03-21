using AutoMapper;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Product;

namespace NTTDataSales.Domain.Mappings.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDTO, Product>();
        CreateMap<UpdateProductDTO, Product>();
        CreateMap<Product, ReadProductDTO>();
    }
}
