
using Microsoft.EntityFrameworkCore;
using Wheelzy.Infrastructure;

namespace Wheelzy.Application.Commands.ChangeCaseStatus
{
    public class ChangeCaseStatusHandler
    {
        private readonly WheelzyDbContext _db;
        public ChangeCaseStatusHandler(WheelzyDbContext db) => _db = db;

        public async Task<bool> Handle(ChangeCaseStatusCommand cmd, CancellationToken cancellationToken = default)
        {
            var caze = await _db.Cases.Include(c => c.CaseStatuses).FirstOrDefaultAsync(c =>
            c.Id == cmd.CaseId, cancellationToken);
            if (caze is null) return false;

            var requiresDate = await _db.Statuses.Where(s => s.Id == cmd.StatusId).Select(s => s.RequireStatusDate).FirstOrDefaultAsync(cancellationToken);
            if (requiresDate && cmd.StatusDate is null) throw new InvalidOperationException("StatusDate is required for this state.");

            foreach (var s in caze.CaseStatuses.Where(s => s.IsCurrent))
                s.IsCurrent = false;

            caze.CaseStatuses.Add(new Domain.Entities.CaseStatus
            {
                StatusId = cmd.StatusId,
                StatusDate = cmd.StatusDate,
                ChangedBy = cmd.ChangedBy,
                IsCurrent = true
            });

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
