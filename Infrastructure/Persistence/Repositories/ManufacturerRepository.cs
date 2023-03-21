using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Repositories;
using Application.Models.Common;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

internal class ManufacturerRepository : IManufacturerRepository {
    private readonly IDbContext _context;

    public ManufacturerRepository(IDbContext context) {
        this._context = context;
    }

    public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(PageRequest pageRequest) {
        var sql = "SELECT * FROM Manufacturers";
        using var connection = _context.CreateConnection();

        return pageRequest != null 
            ? await connection.GetPageAsync<Manufacturer>(m => m.Name, sql, pageRequest.Page, pageRequest.PageSize)
            : await connection.QueryAsync<Manufacturer>(sql);

    }
}
