using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Wheelzy.Infrastructure;

namespace Wheelzy.API.Endpoints
{
    public static class ReferenceEndpoint
    {
        public static IEndpointRouteBuilder MapReferenceEnpoint(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/ref/makes", async (IMemoryCache cache, WheelzyDbContext db, CancellationToken ct) =>
            {
                var makes = await cache.GetOrCreateAsync("ref:makes:v1", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);
                    return await db.Makes.AsNoTracking().OrderBy(m => m.Name).ToListAsync(ct);
                });
                return Results.Ok(makes);
            });

            routes.MapGet("/ref/statuses", async (IDistributedCache dist, WheelzyDbContext db, CancellationToken ct) =>
            {
                var key = "ref:statuses:v1";
                var cached = await dist.GetAsync(key, ct);
                if (cached is not null)
                {
                    var fromCache = JsonSerializer.Deserialize<List<object>>(cached)!;

                    return Results.Ok(fromCache);
                }

                var statuses = await db.Statuses.AsNoTracking().Select(s => new { s.Id, s.Name }).ToListAsync(ct);

                var bytes = JsonSerializer.SerializeToUtf8Bytes(statuses);

                await dist.SetAsync(key, bytes, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) }, ct);

                return Results.Ok(statuses);
            });
            
            return routes;
        }
    }
}
