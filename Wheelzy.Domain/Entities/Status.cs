
namespace Wheelzy.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string  Name { get; set; } = string.Empty;
        public bool RequiresStatusDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
