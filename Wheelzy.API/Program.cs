using Microsoft.EntityFrameworkCore;
using Wheelzy.API.Dtos;
using Wheelzy.API.Endpoints;
using Wheelzy.Application.Commands.ChangeCaseStatus;
using Wheelzy.Application.Commands.CreateCase;
using Wheelzy.Application.Queries.GetCaseSummary;
using Wheelzy.Application.Services;
using Wheelzy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ---------- Configuration ----------
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ---------- Services ----------
builder.Services.AddDbContext<WheelzyDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMemoryCache();
var redisConf = builder.Configuration["Redis:Configuration"];
if (!string.IsNullOrWhiteSpace(redisConf))
{
    builder.Services.AddStackExchangeRedisCache(o => o.Configuration = redisConf);
}

// Register handlers & services
builder.Services.AddScoped<CreateCaseHandler>();
builder.Services.AddScoped<GetCaseSummaryHandler>();
builder.Services.AddScoped<ChangeCaseStatusHandler>();
builder.Services.AddScoped<OrderQueries>();
builder.Services.AddScoped<CustomerService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/whoami", () =>
{
    return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
});


// ---------- Map Endpoints ----------
app.MapCasesEndpoint();
app.MapOrdersEndpoint();
app.MapReferenceEnpoint();

app.Run();