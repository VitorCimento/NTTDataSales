using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NTTDataSales.Business.BLL;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Product;
using NTTDataSales.Domain.Mappings.DTO.Sale;
using NTTDataSales.ORM.Repositories;

namespace NTTDataSales.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private SaleRepository _repository;
    private IMapper _mapper;
    private SaleBLL _bll;

    public SalesController(SaleRepository repository, IMapper mapper, SaleBLL bll)
    {
        _repository = repository;
        _mapper = mapper;
        _bll = bll;
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
        var sale = await _repository.GetByIdAsync(id);

        if (sale == null) return NotFound();

        return Ok(sale);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSaleDTO saleDTO)
    {
        var sale = _mapper.Map<Sale>(saleDTO);
        var createdSale = await _bll.CreateSaleAsync(sale);

        return CreatedAtAction(nameof(Get), new { createdSale.ID }, createdSale);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateSaleDTO saleDTO)
    {
        var sale = await _repository.PutAsync(id, saleDTO);

        if (sale == null) return NotFound();

        return Ok(sale);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _repository.DeleteAsync(id);

        if (!response) return NotFound();

        return Ok(new { message = "Sale removed successfully!" });
    }

    [HttpPost("cancelSale/{id}")]
    public async Task<IActionResult> CancelSale([FromRoute] int id)
    {
        var response = await _bll.CancelSale(id);

        if (!response) return NotFound();

        return Ok(new { message = "Sale cancelled!" });
    }
}
