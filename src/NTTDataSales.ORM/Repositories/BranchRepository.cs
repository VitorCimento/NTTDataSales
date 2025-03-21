using AutoMapper;
using NTTDataSales.Domain.Context;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Customer;
using NTTDataSales.Domain.Mappings.DTO;
using System.Linq.Expressions;
using NTTDataSales.Domain.Mappings.DTO.Branch;
using Microsoft.EntityFrameworkCore;

namespace NTTDataSales.ORM.Repositories;

public class BranchRepository
{
    private readonly NttContext _context;
    private IMapper _mapper;

    public BranchRepository(NttContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PagedDTO<ReadBranchDTO> GetPaged(int page, int size, string order, string direction)
    {
        var totalItems = _context.Branches.Count();
        var skip = page - 1;

        var dbList = GetDbList(page, size, order, direction);
        var returnDTO = GetPagedData(page, size, totalItems, dbList);

        return returnDTO;
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        return await _context.Branches.FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task<Branch> CreateAsync(Branch branch)
    {
        await _context.Branches.AddAsync(branch);
        await _context.SaveChangesAsync();
        return branch;
    }

    public async Task<Branch?> PutAsync(int id, UpdateBranchDTO branchDTO)
    {
        var branch = await GetByIdAsync(id);

        if (branch == null) return null;

        _mapper.Map(branchDTO, branch);
        await _context.SaveChangesAsync();

        return branch;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var branch = await GetByIdAsync(id);

        if (branch == null) return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync();

        return true;
    }

    private IQueryable<Branch> GetDbList(int page, int size, string order, string direction)
    {
        var skip = page - 1;

        Expression<Func<Branch, dynamic>> sortBy = order switch
        {
            nameof(Customer.ID) => x => x.ID,
            nameof(Customer.NAME) => x => x.NAME,
            _ => x => x.ID
        };

        return direction == "asc"
            ? _context.Branches
                .OrderBy(sortBy)
                .Skip(skip * size)
                .Take(size)
            : _context.Branches
                .OrderByDescending(sortBy)
                .Skip(skip * size)
                .Take(size);
    }

    private PagedDTO<ReadBranchDTO> GetPagedData(int page, int size, int total, IQueryable dbList)
    {
        var totalPages = Convert.ToInt32(Math.Ceiling((decimal)total / (decimal)size));
        var dto = _mapper.Map<List<ReadBranchDTO>>(dbList);

        return new PagedDTO<ReadBranchDTO>
        {
            data = dto,
            currentPage = page,
            totalPages = totalPages,
            totalItems = total
        };
    }
}
