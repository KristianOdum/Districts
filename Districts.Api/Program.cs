using System.Text.Json.Serialization;
using Districts.Application.Commands;
using Districts.Application.Queries;
using Districts.Application.Interfaces;
using Districts.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register API services.
builder.Services.AddOpenApi();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()));
builder.Services.AddProblemDetails();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
});

// Read the database connection string from appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("Districts")
                       ?? throw new InvalidOperationException(
                           "Connection string 'Districts' was not found.");

// Register the repository with dependency injection.
// The API creates the repository, but the repository itself owns database access.
builder.Services.AddScoped<IDistrictRepository>(_ => new DistrictRepository(connectionString));
builder.Services.AddScoped<ISalespersonRepository>(_ => new SalespersonRepository(connectionString));
builder.Services.AddScoped<GetDistrictDetailsQueryHandler>();
builder.Services.AddScoped<AddSalespersonToDistrictCommandHandler>();
builder.Services.AddScoped<RemoveSalespersonFromDistrictCommandHandler>();
builder.Services.AddScoped(_ => new TestRepository(connectionString));

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();
// app.UseHttpsRedirection();
// app.UseExceptionHandler();

// Map API controllers to their routes, e.g. /api/districts.
app.MapControllers();

app.Run();