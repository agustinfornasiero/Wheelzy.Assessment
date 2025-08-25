namespace Wheelzy.Domain.Entities
{
    public record Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }  
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public decimal Amount { get; set; } 
        public Customer Customer { get; set; } 
        public Status Status { get; set; } 
    }
}
