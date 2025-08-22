

namespace Wheelzy.Domain.Entities
{
    public class CaseStatus
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int StatusId { get; set; }
        public DateTime? StatusDate { get; set; }
        public string ChangedBy { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public bool IsCurrent { get; set; }
        public Case Case { get; set; } = null!;
        public Status Status { get; set; } = null!;
    }
}
