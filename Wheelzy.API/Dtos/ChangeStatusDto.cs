namespace Wheelzy.API.Dtos
{
    public record ChangeStatusDto(int StatusId, DateTime? StatusDate, string ChangedBy);
}
