

namespace Wheelzy.Domain.Entities
{
    public class CaseQuote
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int BuyerZipQuoteId { get; set; }
        public decimal Amount { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Case Case { get; set; } = null!;
        public BuyerZipQuote BuyerZipQuote { get; set; } = null!;
    }
}
