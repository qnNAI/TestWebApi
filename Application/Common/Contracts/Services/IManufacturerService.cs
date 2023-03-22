using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Application.Models.Manufacturer;
using Domain.Entities;

namespace Application.Common.Contracts.Services;

public interface IManufacturerService {
    Task<IEnumerable<ManufacturerDto>> GetPageAsync(PageRequest page);
    Task<ManufacturerDto> GetByIdAsync(Guid id);
    Task<ManufacturerDto> GetByNameAsync(string name);

    Task<ManufacturerDto> CreateAsync(CreateManufRequest createRequest);
    Task<ManufacturerDto> UpdateAsync(UpdateManufRequest updateRequest);
    Task DeleteAsync(Guid id);
}

