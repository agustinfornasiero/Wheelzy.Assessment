

namespace Wheelzy.Domain.Entities
{
    public class Case
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string Zip { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Customer Customer { get; set; } = null!;
        public Car Car { get; set; } = null!;
        public List<CaseQuote> CaseQuotes { get; set; } = new();
        public List<CaseStatus> CaseStatuses { get; set; } = new();
    }
}
