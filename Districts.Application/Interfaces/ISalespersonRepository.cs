using Districts.Application.Queries;
using Districts.Domain.Models;

namespace Districts.Application.Interfaces;

public interface ISalespersonRepository
{
    // Queries
    Task<List<Salesperson>> GetSalespersonsAsync();
}