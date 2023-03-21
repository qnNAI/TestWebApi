using System.Data;
using Application.Common.Contracts.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Contexts;

internal class DapperContext : IDbContext {
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration) {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("SqlConnection") ?? string.Empty;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}

