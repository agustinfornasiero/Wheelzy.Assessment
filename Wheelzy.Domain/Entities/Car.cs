

namespace Wheelzy.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int MakeId { get; set; }
        public int SubModelId { get; set; }
        public Make Make { get; set; } = null!;
        public Model Model { get; set; } = null!;
        public SubModel SubModel { get; set; } = null!;
    }
}
