using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

internal class CarRepository : ICarRepository {
    private readonly IDbContext _context;

    public CarRepository(IDbContext context) {
        this._context = context;
    }

    public async Task<IEnumerable<Car>> GetPageAsync(PageRequest pageRequest) {
        var sql = "SELECT * FROM Cars";
        using var connection = _context.CreateConnection();

        return await connection.GetPageAsync<Car>(c => c.Id, sql, pageRequest);
    }

    public async Task<Car> GetByIdAsync(Guid id) {
        var sql = $"SELECT * FROM Cars c INNER JOIN Manufacturers m on c.ManufacturerId = m.Id WHERE c.Id = @Id";
        using var connection = _context.CreateConnection();

        Car car = null;
        await connection.QueryAsync<Car, Manufacturer, Car>(
            sql,
            (c, m) => {
                if(car is null) {
                    car = c;
                }
                car.Manufacturer = m;

                return c;
            },
            new { Id = id });

        return car;
    }

    public async Task<Car> CreateAsync(Car car) {
        var id = Guid.NewGuid();
        car.Id = id;

        var sql = @"INSERT INTO Cars (Id, ManufacturerId, Model, Price, ManufDate, Mileage, Volume)
                    VALUES (@Id, @ManufacturerId, @Model, @Price, @ManufDate, @Mileage, @Volume)";
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(sql, new { car.Id, car.ManufacturerId, car.Model, car.Price, car.ManufDate, car.Mileage, car.Volume });

        var created = await connection.GetAsync<Car>(id);
        return created;
    }

    public async Task<Car> UpdateAsync(Car car) {
        using var connection = _context.CreateConnection();

        var existing = await connection.GetAsync<Car>(car.Id);
        if (existing == null) {
            throw new NotFoundException();
        }
        car.ManufacturerId = existing.ManufacturerId;

        var sql = @"UPDATE Cars Set Model=@Model, Price=@Price, ManufDate=@ManufDate, Mileage=@Mileage, Volume=@Volume WHERE Id=@Id";

        await connection.ExecuteAsync(sql, new { car.Model, car.Price, car.ManufDate, car.Mileage, car.Volume, car.Id });

        var updated = await connection.GetAsync<Car>(car.Id);
        return updated;
    }

    public async Task DeleteAsync(Guid id) {
        using var connection = _context.CreateConnection();

        var existing = await connection.GetAsync<Car>(id);
        if(existing == null) {
            throw new NotFoundException();
        }

        await connection.DeleteAsync(existing);
    }
}

