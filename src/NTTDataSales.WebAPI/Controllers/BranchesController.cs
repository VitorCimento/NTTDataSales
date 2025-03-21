using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Branch;
using NTTDataSales.Domain.Mappings.DTO.Product;
using NTTDataSales.ORM.Repositories;

namespace NTTDataSales.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchesController : ControllerBase
{
    private BranchRepository _repository;
    private IMapper _mapper;

    public BranchesController(BranchRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string order = "id")
    {
        var split = order.Split(" ");
        var newOrder = split[0];
        var direction = split.Length > 1 ? split[1] : "asc";

        var paged = _repository.GetPaged(page, size, newOrder, direction);

        return Ok(paged);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var branch = await _repository.GetByIdAsync(id);

        if (branch == null) return NotFound();

        return Ok(branch);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBranchDTO branchDTO)
    {
        var branch = _mapper.Map<Branch>(branchDTO);
        var createdBranch = await _repository.CreateAsync(branch);

        return CreatedAtAction(nameof(Get), new { createdBranch.ID }, createdBranch);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateBranchDTO branchDTO)
    {
        var branch = await _repository.PutAsync(id, branchDTO);

        if (branch == null) return NotFound();

        return Ok(branch);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _repository.DeleteAsync(id);

        if (!response) return NotFound();

        return Ok(new { message = "Branch removed successfully!" });
    }
}
