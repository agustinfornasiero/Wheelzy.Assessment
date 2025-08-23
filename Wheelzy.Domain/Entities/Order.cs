
namespace Wheelzy.Domain.Entities
{
    public record Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
    }
}
