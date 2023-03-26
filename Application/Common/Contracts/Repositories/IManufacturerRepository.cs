using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Domain.Entities;

namespace Application.Common.Contracts.Repositories;

public interface IManufacturerRepository {

    Task<IEnumerable<Manufacturer>> GetPageAsync(PageRequest pageRequest);
    Task<ManufacturerFull> GetByIdAsync(Guid id);
    Task<ManufacturerFull> GetByNameAsync(string name);

    Task<Manufacturer> CreateAsync(Manufacturer manufacturer);
    Task<Manufacturer> UpdateAsync(Manufacturer manufacturer);
    Task DeleteAsync(Guid id);
}
