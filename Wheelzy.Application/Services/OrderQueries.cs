
using Microsoft.EntityFrameworkCore;
using Wheelzy.Domain.Entities;
using Wheelzy.Infrastructure;

namespace Wheelzy.Application.Services
{
    public class OrderQueries
    {
        private readonly WheelzyDbContext _db;
        public OrderQueries(WheelzyDbContext db) => _db = db;

        public async Task<List<OrderDto>> GetOrders(DateTime? dateFrom, DateTime? dateTo,
            List<int> customerIds, List<int> statusIds, bool? isActive, CancellationToken ct = default)
        {
            IQueryable<Order> q = _db.Orders.AsNoTracking();

            if(dateFrom.HasValue) q = q.Where(o => o.OrderDate >= dateFrom.Value);

            if(dateTo.HasValue) q = q.Where(o => o.OrderDate <= dateTo.Value);

            if (customerIds is { Count: > 0 })
                q = q.Where(o => customerIds.Contains(o.CustomerId));

            if (statusIds is { Count: > 0 })
                q = q.Where(o => statusIds.Contains(o.StatusId));

            if(isActive.HasValue)
                q = q.Where(o => o.IsActive == isActive.Value);

            return await q.OrderByDescending(o => o.OrderDate).Select(o => new OrderDto {Id = o.Id,
            Date = o.OrderDate, CustomerId = o.CustomerId, StatusId = o.StatusId, IsActive = o.IsActive})
                .ToListAsync(ct);

        }
    }
}

public record OrderDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public bool IsActive { get; set; }
}


