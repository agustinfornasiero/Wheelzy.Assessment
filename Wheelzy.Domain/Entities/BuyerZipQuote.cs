
namespace Wheelzy.Domain.Entities
{
    public class BuyerZipQuote
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public string Zip { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public Buyer Buyer { get; set; } = null!;
        public ZipCode? ZipCode { get; set; }
    }
}
