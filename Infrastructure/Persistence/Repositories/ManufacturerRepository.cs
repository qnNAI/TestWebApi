using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Repositories;
using Application.Common.Exceptions;
using Application.Models.Common;
using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

internal class ManufacturerRepository : IManufacturerRepository {
    private readonly IDbContext _context;

    public ManufacturerRepository(IDbContext context) {
        this._context = context;
    }

    public async Task<IEnumerable<Manufacturer>> GetPageAsync(PageRequest pageRequest) {
        var sql = "SELECT * FROM Manufacturers";
        using var connection = _context.CreateConnection();
        
        return await connection.GetPageAsync<Manufacturer>(m => m.Name, sql, pageRequest);
    }

    public async Task<ManufacturerFull> GetByIdAsync(Guid id) {
        var manufacturer = await GetByPropAsync(m => m.Id, id);
        return manufacturer;
    }

    public async Task<ManufacturerFull> GetByNameAsync(string name) {
        var manufacturer = await GetByPropAsync(m => m.Name, name);
        return manufacturer;
    }

    /// <summary>
    /// Retrieves manufacturer by property
    /// </summary>
    /// <typeparam name="T">member type</typeparam>
    /// <param name="expression">member getter</param>
    /// <param name="member">member value</param>
    private async Task<ManufacturerFull> GetByPropAsync<T>(Expression<Func<Manufacturer, object>> expression, T member) {
        var sql = $"SELECT * FROM Manufacturers m LEFT JOIN Cars c on m.Id = c.ManufacturerId WHERE m.{QueryHelper.GetMemberName(expression)} = @Property";
        using var connection = _context.CreateConnection();

        ManufacturerFull manufacturer = null;
        await connection.QueryAsync<ManufacturerFull, Car, ManufacturerFull>(
            sql,
            (m, c) => {
                if(manufacturer is null) {
                    manufacturer = m;
                }

                manufacturer.Cars.Add(c);

                return m;
            },
            new { Property = member });

        return manufacturer;
    }

    public async Task<Manufacturer> CreateAsync(Manufacturer manufacturer) {
        var id = Guid.NewGuid();
        manufacturer.Id = id;

        var sql = $"INSERT INTO Manufacturers (Id, Name) VALUES (@{nameof(manufacturer.Id)}, @{nameof(manufacturer.Name)})";
        using var connection = _context.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try {
            await connection.ExecuteAsync(sql, new { manufacturer.Id, manufacturer.Name }, transaction);

            transaction.Commit();
        } catch(Exception) {
            transaction.Rollback();
            throw;
        }
        var created = await connection.GetAsync<Manufacturer>(id);
        return created;
    }

    public async Task<Manufacturer> UpdateAsync(Manufacturer manufacturer) {
        using var connection = _context.CreateConnection();
        
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try {
            await connection.UpdateAsync(manufacturer, transaction);

            transaction.Commit();
        } catch(Exception) {
            transaction.Rollback();
            throw;
        }
        var updated = await connection.GetAsync<Manufacturer>(manufacturer.Id);
        return updated;
    }

    public async Task DeleteAsync(Guid id) {
        using var connection = _context.CreateConnection();

        var existing = await connection.GetAsync<Manufacturer>(id);
        if (existing == null) {
            throw new NotFoundException();
        }

        await connection.DeleteAsync(existing);
    }
}
