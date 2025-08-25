using Microsoft.AspNetCore.Mvc;
using Wheelzy.API.Dtos;
using Wheelzy.Application.Commands.ChangeCaseStatus;
using Wheelzy.Application.Commands.CreateCase;
using Wheelzy.Application.Queries.GetCaseSummary;

namespace Wheelzy.API.Endpoints
{
    public static class CasesEndpoint
    {
        public static IEndpointRouteBuilder MapCasesEndpoint(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/cases");

            //Create Case
            group.MapPost("/", async ([FromBody] CreateCaseCommand cmd, CreateCaseHandler handler, CancellationToken ct) =>
            {
                var id = await handler.Handle(cmd, ct);
                return Results.Created($"/cases/{id}", new { id });
            });

            //Get Summary
            group.MapGet("/{id:int}/summary", async (int id, GetCaseSummaryHandler handler, CancellationToken ct) =>
            {
                var result = await handler.Handle(new GetCaseSummaryQuery(id), ct);

                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            //Change Status
            group.MapPost("/{id:int}/status", async (int id, [FromBody] ChangeStatusDto dto, ChangeCaseStatusHandler handler, CancellationToken ct) =>
            {
                var cmd = new ChangeCaseStatusCommand(id, dto.StatusId, dto.StatusDate, dto.ChangedBy);

                try
                {
                    var ok = await handler.Handle(cmd, ct);
                    return ok ? Results.Ok() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });
        
            return routes;
        
        }
    }
}
