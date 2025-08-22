

namespace Wheelzy.Domain.Entities
{
    public class SubModel
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Model Model { get; set; } = null!;
    }
}
