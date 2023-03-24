using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Domain.Entities;

namespace Application.Common.Contracts.Repositories;

public interface IUserRepository {

    Task<User> GetByNameAsync(string username);
    Task<IEnumerable<User>> GetPageAsync(PageRequest page);

    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(string username);
}
