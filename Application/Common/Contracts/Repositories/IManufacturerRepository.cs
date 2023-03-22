using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Domain.Entities;

namespace Application.Common.Contracts.Repositories;

public interface IManufacturerRepository {

    Task<IEnumerable<Manufacturer>> GetManufacturersAsync(PageRequest pageRequest);
    Task<Manufacturer> GetManufacturerByIdAsync(Guid id);
}
