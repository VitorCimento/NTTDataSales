using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NTTDataSales.Domain.Context;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Sale;
using NTTDataSales.Domain.Mappings.DTO;

namespace NTTDataSales.ORM.Repositories;

public class SaleRepository
{
    private readonly NttContext _context;
    private IMapper _mapper;

    public SaleRepository(NttContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;        
    }

    public PagedDTO<ReadSaleDTO> GetPaged(int page, int size, string order, string direction)
    {
        var totalItems = _context.Products.Count();
        var skip = page - 1;

        var dbList = GetDbList(page, size, order, direction);
        var returnDTO = GetPagedData(page, size, totalItems, dbList);

        return returnDTO;
    }

    public async Task<Sale?> GetByIdAsync(int id) => await _context.Sales
        .Include(x => x.CUSTOMER)
        .Include(x => x.BRANCH)
        .FirstOrDefaultAsync(x => x.ID == id);

    public async Task<Sale> CreateAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale?> PutAsync(int id, UpdateSaleDTO saleDTO)
    {
        var sale = await GetByIdAsync(id);

        if (sale == null) return null;

        _mapper.Map(saleDTO, sale);
        await _context.SaveChangesAsync();

        return sale;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sale = await GetByIdAsync(id);

        if (sale == null) return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();

        return true;
    }

    private IQueryable<Sale> GetDbList(int page, int size, string order, string direction)
    {
        var skip = page - 1;

        Expression<Func<Sale, dynamic>> sortBy = order switch
        {
            nameof(Sale.ID) => x => x.ID,
            nameof(Sale.BRANCHID) => x => x.BRANCHID,
            nameof(Sale.CUSTOMERID) => x => x.CUSTOMERID,
            nameof(Sale.DATE) => x => x.DATE,
            nameof(Sale.TOTALVALUE) => x => x.TOTALVALUE,
            _ => x => x.ID
        };

        return direction == "asc"
            ? _context.Sales
                .OrderBy(sortBy)
                .Include(x => x.CUSTOMER)
                .Include(x => x.BRANCH)
                .Skip(skip * size)
                .Take(size)
            : _context.Sales
                .OrderByDescending(sortBy)
                .Include(x => x.CUSTOMER)
                .Include(x => x.BRANCH)
                .Skip(skip * size)
                .Take(size);
    }

    private PagedDTO<ReadSaleDTO> GetPagedData(int page, int size, int total, IQueryable dbList)
    {
        var totalPages = Convert.ToInt32(Math.Ceiling((decimal)total / (decimal)size));
        var dto = _mapper.Map<List<ReadSaleDTO>>(dbList);

        return new PagedDTO<ReadSaleDTO>
        {
            data = dto,
            currentPage = page,
            totalPages = totalPages,
            totalItems = total
        };
    }}
