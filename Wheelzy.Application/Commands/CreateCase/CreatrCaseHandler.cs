
using Microsoft.EntityFrameworkCore;
using Wheelzy.Domain.Entities;
using Wheelzy.Infrastructure;

namespace Wheelzy.Application.Commands.CreateCase
{
    public class CreateCaseHandler
    {
        private readonly WheelzyDbContext _db;

        public CreateCaseHandler(WheelzyDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            // 1) Crear Case
            var newCase = new Case
            {
                CustomerId = request.CustomerId,
                CarId = request.CarId,
                Zip = request.Zip,
                CreatedAt = DateTime.UtcNow
            };
            _db.Cases.Add(newCase);
            await _db.SaveChangesAsync(cancellationToken);

            // 2) Obtener status "New"
            var statusNew = await _db.Statuses
                .Where(s => s.Name == "New")
                .FirstOrDefaultAsync(cancellationToken);

            if (statusNew == null)
                throw new InvalidOperationException("No existe el estado inicial 'New' en la tabla Statuses.");

            // 3) Insertar CaseStatus inicial
            var caseStatus = new CaseStatus
            {
                CaseId = newCase.Id,
                StatusId = statusNew.Id,
                StatusDate = null, // New no lo requiere
                ChangedBy = "system",
                IsCurrent = true,
                ChangedAt = DateTime.UtcNow
            };
            _db.CaseStatuses.Add(caseStatus);
            await _db.SaveChangesAsync(cancellationToken);

            return newCase.Id;
        }
    }

}
