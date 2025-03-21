using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NTTDataSales.Domain.Context;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO;
using NTTDataSales.Domain.Mappings.DTO.Customer;

namespace NTTDataSales.ORM.Repositories;

public class CustomerRepository
{
    private readonly NttContext _context;
    private IMapper _mapper;

    public CustomerRepository(NttContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PagedDTO<ReadCustomerDTO> GetPaged(int page, int size, string order, string direction)
    {
        var totalItems = _context.Customers.Count();
        var skip = page - 1;

        var dbList = GetDbList(page, size, order, direction);
        var returnDTO = GetPagedData(page, size, totalItems, dbList);

        return returnDTO;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> PutAsync(int id, UpdateCustomerDTO customerDTO)
    {
        var customer = await GetByIdAsync(id);

        if (customer == null) return null;

        _mapper.Map(customerDTO, customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await GetByIdAsync(id);

        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return true;
    }

    private IQueryable<Customer> GetDbList(int page, int size, string order, string direction)
    {
        var skip = page - 1;

        Expression<Func<Customer, dynamic>> sortBy = order switch
        {
            nameof(Customer.ID) => x => x.ID,
            nameof(Customer.DOCUMENT) => x => x.DOCUMENT,
            nameof(Customer.NAME) => x => x.NAME,
            nameof(Customer.PHONE) => x => x.PHONE,
            nameof(Customer.EMAIL) => x => x.EMAIL,
            _ => x => x.ID
        };

        return direction == "asc"
            ? _context.Customers
                .OrderBy(sortBy)
                .Skip(skip * size)
                .Take(size)
            : _context.Customers
                .OrderByDescending(sortBy)
                .Skip(skip * size)
                .Take(size);
    }

    private PagedDTO<ReadCustomerDTO> GetPagedData(int page, int size, int total, IQueryable dbList)
    {
        var totalPages = Convert.ToInt32(Math.Ceiling((decimal)total / (decimal)size));
        var dto = _mapper.Map<List<ReadCustomerDTO>>(dbList);

        return new PagedDTO<ReadCustomerDTO>
        {
            data = dto,
            currentPage = page,
            totalPages = totalPages,
            totalItems = total
        };
    }
}
