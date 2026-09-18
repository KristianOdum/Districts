using Districts.Infra;
using Districts.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register API services.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Read the database connection string from appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("Districts")
                       ?? throw new InvalidOperationException(
                           "Connection string 'Districts' was not found.");

// Register the repository with dependency injection.
// The API creates the repository, but the repository itself owns database access.
builder.Services.AddScoped(
    _ => new DistrictRepository(connectionString));
builder.Services.AddScoped(
    _ => new TestRepository(connectionString));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

// Map API controllers to their routes, e.g. /api/districts.
app.MapControllers();

app.Run();