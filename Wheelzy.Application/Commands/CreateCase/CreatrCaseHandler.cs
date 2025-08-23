
using Microsoft.EntityFrameworkCore;
using Wheelzy.Domain.Entities;
using Wheelzy.Infrastructure;

namespace Wheelzy.Application.Commands.CreateCase
{
    public class CreatrCaseHandler
    {
        private readonly WheelzyDbContext _db;
        public CreatrCaseHandler(WheelzyDbContext db) => _db = db;

        public async Task<int> Handle(CreateCaseCommand cmd, CancellationToken cancellationToken = default)
        {
            var caze = new Case { CustomerId = cmd.CustomerId, CarId = cmd.CarId, Zip = cmd.Zip };

            var candidates = await _db.BuyerZipQuotes.Where(b => b.Zip == cmd.Zip &&
                                                            b.IsActive &&
                                                            (b.EffectiveFrom == null || b.EffectiveFrom <= DateTime.UtcNow)
                                                            && (b.EffectiveTo == null || b.EffectiveTo >= DateTime.UtcNow))
                                                            .ToListAsync(cancellationToken);

            foreach (var z in candidates)
                 caze.CaseQuotes.Add(new CaseQuote { BuyerZipQuoteId = z.Id, Amount = z.Amount });

            var current = caze.CaseQuotes.OrderByDescending(q => q.Amount).FirstOrDefault();
            if (current is not null)
                current.IsCurrent = true;

            caze.CaseStatuses.Add(new CaseStatus { StatusId = 1, IsCurrent = true, ChangedBy = "system" });

            _db.Cases.Add(caze);

            await _db.SaveChangesAsync(cancellationToken);

            return caze.Id;

            
        }


    }
}
