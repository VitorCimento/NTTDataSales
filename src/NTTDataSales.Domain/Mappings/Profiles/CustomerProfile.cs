using AutoMapper;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Customer;

namespace NTTDataSales.Domain.Mappings.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerDTO, Customer>();
        CreateMap<UpdateCustomerDTO, Customer>();
        CreateMap<Customer, ReadCustomerDTO>();
    }
}
