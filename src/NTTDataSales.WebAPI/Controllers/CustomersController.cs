using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Customer;
using NTTDataSales.ORM.Repositories;

namespace NTTDataSales.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private CustomerRepository _repository;
    private IMapper _mapper;

    public CustomersController(CustomerRepository repository, IMapper mapper)
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
        var customer = await _repository.GetByIdAsync(id);

        if (customer == null) return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerDTO customerDTO)
    {
        var customer = _mapper.Map<Customer>(customerDTO);
        var createdCustomer = await _repository.CreateAsync(customer);

        return CreatedAtAction(nameof(Get), new { createdCustomer.ID }, createdCustomer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateCustomerDTO customerDTO)
    {
        var costumer = await _repository.PutAsync(id, customerDTO);

        if (costumer == null) return NotFound();

        return Ok(costumer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _repository.DeleteAsync(id);

        if (!response) return NotFound();

        return Ok(new { message = "Product removed successfully!" });
    }
}
