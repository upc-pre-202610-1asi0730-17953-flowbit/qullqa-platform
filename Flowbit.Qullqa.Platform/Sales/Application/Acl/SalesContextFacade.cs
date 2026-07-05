using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Repositories;
using Qullqa.Platform.v2.Sales.Interfaces.Acl;

namespace Qullqa.Platform.v2.Sales.Application.Acl;

public class SalesContextFacade(ISaleRepository saleRepository) : ISalesContextFacade
{
    public async Task<decimal> GetTotalRevenue(int businessId, DateOnly? dateFrom, DateOnly? dateTo, CancellationToken cancellationToken)
    {
        return await saleRepository.SumPaidTotalByBusinessIdAsync(businessId, dateFrom, dateTo, cancellationToken);
    }

    public async Task<IReadOnlyCollection<(DateOnly Date, decimal Total)>> GetSalesByDay(int businessId, DateOnly dateFrom,
        DateOnly dateTo, CancellationToken cancellationToken)
    {
        var sales = await saleRepository.FindAllByBusinessIdAsync(businessId, dateFrom, dateTo, cancellationToken);
        return sales.Where(sale => sale.Status == SaleStatus.Paid)
            .GroupBy(sale => DateOnly.FromDateTime(sale.Date.UtcDateTime))
            .Select(group => (Date: group.Key, Total: group.Sum(sale => sale.TotalAmount)))
            .OrderBy(entry => entry.Date)
            .ToList();
    }

    public async Task<IReadOnlyCollection<SaleExportRow>> GetSalesForExport(int businessId, DateOnly? dateFrom, DateOnly? dateTo,
        CancellationToken cancellationToken)
    {
        var sales = await saleRepository.FindAllByBusinessIdAsync(businessId, dateFrom, dateTo, cancellationToken);
        return sales.Where(sale => sale.Status == SaleStatus.Paid)
            .Select(sale => new SaleExportRow(sale.Id, sale.Date, sale.PaymentMethod, sale.TotalAmount, sale.Currency))
            .ToList();
    }
}
