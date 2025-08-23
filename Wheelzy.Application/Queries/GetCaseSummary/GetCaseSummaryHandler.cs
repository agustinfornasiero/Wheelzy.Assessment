

using Microsoft.EntityFrameworkCore;
using Wheelzy.Domain.Entities;
using Wheelzy.Infrastructure;

namespace Wheelzy.Application.Queries.GetCaseSummary
{
    public class GetCaseSummaryHandler
    {
        private readonly WheelzyDbContext _db;
        public GetCaseSummaryHandler(WheelzyDbContext db) => _db = db;

        public async Task<object?> Handle(GetCaseSummaryQuery q, CancellationToken cancellationToken = default)
        {
            return await _db.Cases.AsNoTracking()
                .Where(c => c.Id == q.CaseId)
                .Select(c => new
                {
                    c.Id,
                    Car = new
                    {
                        c.Car.Year,
                        Make = c.Car.Make.Name,
                        Model = c.Car.Model.Name,
                        SubModel = c.Car.SubModel.Name
                    },
                    CurrentQuote = c.CaseQuotes.Where(x => x.IsCurrent)
                    .Select(x => new { Buyer = x.BuyerZipQuote.Buyer.Name, x.Amount }).FirstOrDefault(),
                    CurrentStatus = c.CaseStatuses.Where(s => s.IsCurrent).Select(s =>
                    new { Status = s.Status.Name, s.StatusDate }).FirstOrDefault()
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
