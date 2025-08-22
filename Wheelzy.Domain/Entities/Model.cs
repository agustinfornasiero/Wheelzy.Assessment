
namespace Wheelzy.Domain.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Make Make { get; set; } = null!;
    }
}
