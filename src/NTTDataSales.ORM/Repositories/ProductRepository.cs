using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NTTDataSales.Domain.Context;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO;
using NTTDataSales.Domain.Mappings.DTO.Product;

namespace NTTDataSales.ORM.Repositories;

public class ProductRepository
{
    private readonly NttContext _context;
    private IMapper _mapper;

    public ProductRepository(NttContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PagedDTO<ReadProductDTO> GetPaged(int page, int size, string order, string direction)
    {
        var totalItems = _context.Products.Count();
        var skip = page - 1;

        var dbList = GetDbList(page, size, order, direction);
        var returnDTO = GetPagedData(page, size, totalItems, dbList);

        return returnDTO;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> PutAsync(int id, UpdateProductDTO productDTO)
    {
        var product = await GetByIdAsync(id);

        if (product == null) return null;

        _mapper.Map(productDTO, product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);

        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }

    private IQueryable<Product> GetDbList(int page, int size, string order, string direction)
    {
        var skip = page - 1;

        Expression<Func<Product, dynamic>> sortBy = order switch
        {
            nameof(Product.ID) => x => x.ID,
            nameof(Product.NAME) => x => x.NAME,
            nameof(Product.DESCRIPTION) => x => x.DESCRIPTION,
            nameof(Product.PRICE) => x => x.PRICE,
            _ => x => x.ID
        };

        return direction == "asc"
            ? _context.Products
                .OrderBy(sortBy)
                .Skip(skip * size)
                .Take(size)
            : _context.Products
                .OrderByDescending(sortBy)
                .Skip(skip * size)
                .Take(size);
    }

    private PagedDTO<ReadProductDTO> GetPagedData(int page, int size, int total, IQueryable dbList)
    {
        var totalPages = Convert.ToInt32(Math.Ceiling((decimal)total / (decimal)size));
        var dto = _mapper.Map<List<ReadProductDTO>>(dbList);

        return new PagedDTO<ReadProductDTO>
        {
            data = dto,
            currentPage = page,
            totalPages = totalPages,
            totalItems = total
        };
    }
}
