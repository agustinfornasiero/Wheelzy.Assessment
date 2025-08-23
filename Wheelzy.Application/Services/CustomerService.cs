using Microsoft.EntityFrameworkCore;
using Wheelzy.Infrastructure;

namespace Wheelzy.Application.Services
{
    public class CustomerService
    {
        private readonly WheelzyDbContext _db;
        public CustomerService(WheelzyDbContext db) => _db = db;

        public async Task UpdateCustomerBalanceByInvoicesAsync(IEnumerable<InvoiceDto> invoices, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(invoices);

            var aggregates = invoices.Where(i => i.CustomerId.HasValue).GroupBy(i => i.CustomerId!.Value).Select(g =>
            new { CustomerId = g.Key, Total = g.Sum(i => i.Total) }).ToList();

            if (aggregates.Count == 0) return;

            var ids = aggregates.Select(a => a.CustomerId).ToList();

            var totals = aggregates.ToDictionary(a => a.CustomerId, a => a.Total);

            await using var tx = await _db.Database.BeginTransactionAsync(cancellationToken);
            var customers = await _db.Customers.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);

            foreach( var c in customers)
                c.Balance -= totals[c.Id];

            await _db.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);
        }
    }
}

public record InvoiceDto(int? CustomerId, decimal Total);
