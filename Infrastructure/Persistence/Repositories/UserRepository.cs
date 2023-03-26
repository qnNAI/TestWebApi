using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

internal class UserRepository : IUserRepository {
    private readonly IDbContext _context;

    public UserRepository(IDbContext context) {
        this._context = context;
    }

    public async Task<User> GetByNameAsync(string username) {
        var sql = $"SELECT * FROM Users WHERE Username = @Username";
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
    }

    public async Task<IEnumerable<User>> GetPageAsync(PageRequest page) {
        var sql = "SELECT Id, Username FROM Users";
        using var connection = _context.CreateConnection();

        return await connection.GetPageAsync<User>(u => u.Id, sql, page);
    }

    public async Task<User> CreateAsync(User user) {
        var id = Guid.NewGuid();
        user.Id = id;

        var sql = @"INSERT INTO Users (Id, Username, Password)
                    VALUES (@Id, @Username, @Password)";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { user.Id, user.Username, user.Password });

        var created = await connection.GetAsync<User>(id);
        return created;
    }
    public async Task<User> UpdateAsync(User user) {
        using var connection = _context.CreateConnection();

        var existing = await connection.GetAsync<User>(user.Id);
        if(existing == null) {
            throw new NotFoundException();
        }
       
        var sql = @"UPDATE Users Set Password=@Password WHERE Id=@Id";

        await connection.ExecuteAsync(sql, new { user.Password, user.Id });

        var updated = await connection.GetAsync<User>(user.Id);
        return updated;
    }

    public async Task DeleteAsync(string username) {
        using var connection = _context.CreateConnection();

        var existing = await GetByNameAsync(username);
        if(existing == null) {
            throw new NotFoundException();
        }

        await connection.DeleteAsync(existing);
    }
}
