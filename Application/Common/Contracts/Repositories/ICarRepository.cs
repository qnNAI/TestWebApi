using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Domain.Entities;

namespace Application.Common.Contracts.Repositories {

    public interface ICarRepository {
        Task<IEnumerable<Car>> GetPageAsync(PageRequest pageRequest);
        Task<Car> GetByIdAsync(Guid id);

        Task<Car> CreateAsync(Car car);
        Task<Car> UpdateAsync(Car car);
        Task DeleteAsync(Guid id);
    }
}
