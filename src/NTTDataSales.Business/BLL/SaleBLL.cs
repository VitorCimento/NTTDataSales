using AutoMapper;
using NTTDataSales.Domain.Entities;
using NTTDataSales.Domain.Mappings.DTO.Sale;
using NTTDataSales.ORM.Repositories;

namespace NTTDataSales.Business.BLL;

public class SaleBLL
{
    private SaleRepository _repository;
    private IMapper _mapper;

    public SaleBLL(SaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Sale> CreateSaleAsync(Sale sale)
    {
        ApllyDiscount(ref sale);

        sale.TOTALVALUE = sale.SALEITEMS.Sum(x => x.TOTALPRICE);
        sale.TOTALDISCOUNT = sale.SALEITEMS.Sum(x => x.DISCOUNT);

        await _repository.CreateAsync(sale);

        return sale;
    }

    public async Task<bool> CancelSale(int id)
    {
        var sale = await _repository.GetByIdAsync(id);
        
        if (sale == null) return false;
        if (sale.SALECANCELLED) return true;

        var dto = _mapper.Map<UpdateSaleDTO>(sale);
        dto.SALECANCELLED = true;

        await _repository.PutAsync(id, dto);

        Console.WriteLine($"[{DateTime.Now}] Sale {id} cancelled!");

        return true;
    }

    public void ApllyDiscount(ref Sale sale)
    {
        sale.SALEITEMS
            .Where(x => x.QUATITY < 4)
            .ToList()
            .ForEach(item =>
            {
                item.TOTALPRICE = item.UNITARYPRICE * item.QUATITY;
                item.DISCOUNT = 0;
            });

        sale.SALEITEMS
            .Where(x => x.QUATITY is >= 4 and < 10)
            .ToList()
            .ForEach(item =>
            {
                item.TOTALPRICE = item.UNITARYPRICE * item.QUATITY;
                item.DISCOUNT = Math.Round(item.TOTALPRICE * 0.1M, 2, MidpointRounding.AwayFromZero);
                item.TOTALPRICE -= item.DISCOUNT;
            });

        sale.SALEITEMS
            .Where(x => x.QUATITY is >= 10 and <= 20)
            .ToList()
            .ForEach(item =>
            {
                item.TOTALPRICE = item.UNITARYPRICE * item.QUATITY;
                item.DISCOUNT = Math.Round(item.TOTALPRICE * 0.2M, 2, MidpointRounding.AwayFromZero);
                item.TOTALPRICE -= item.DISCOUNT;
            });
    }
}
