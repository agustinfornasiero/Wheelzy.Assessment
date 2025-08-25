using Microsoft.AspNetCore.Mvc;
using Wheelzy.Application.Services;

namespace Wheelzy.API.Dtos
{
    public static class OrderEndpoint
    {
        public static IEndpointRouteBuilder MapOrdersEndpoint(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/orders", async ([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo,
                [FromQuery] string? customerIds, [FromQuery] string? statusIds,
                [FromQuery] bool? isActive, OrderQueries queries, CancellationToken ct) =>
            {
                var customers = string.IsNullOrEmpty(customerIds) ? new List<int>() :
                customerIds.Split(',').Select(int.Parse).ToList();

                var statuses = string.IsNullOrEmpty(statusIds) ? new List<int>() :
                statusIds.Split(',').Select(int.Parse).ToList();

                var list = await queries.GetOrders(dateFrom, dateTo, customers, statuses, isActive, ct);

                return Results.Ok(list);
            });

            return routes;

        }
    }
}
