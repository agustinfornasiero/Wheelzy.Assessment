
namespace Wheelzy.Application.Commands.ChangeCaseStatus
{
    public record ChangeCaseStatusCommand(int CaseId, int StatusId, DateTime? StatusDate, string ChangedBy);
}
