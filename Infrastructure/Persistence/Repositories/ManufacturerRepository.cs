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
        
        return await connection.GetPageAsync<Manufacturer>(m => m.Name, sql, pageRequest);
    }

    public async Task<Manufacturer> GetManufacturerByIdAsync(Guid id) {
        var sql = $"SELECT * FROM Manufacturers m LEFT JOIN Cars c on m.Id = c.ManufacturerId WHERE m.Id = @{nameof(id)}";
        using var connection = _context.CreateConnection();

        Manufacturer manufacturer = null;
        await connection.QueryAsync<Manufacturer, Car, Manufacturer>(
            sql,
            (m, c) => {
                if (manufacturer is null) {
                    manufacturer = m;
                }

                manufacturer.Cars.Add(c);

                return m;
            }, 
            new { id });

        return manufacturer;
    }
}
