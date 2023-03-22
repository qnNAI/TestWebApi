using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Common;
using Application.Models.Manufacturer;

namespace Application.Common.Contracts.Services;

public interface IManufacturerService {
    Task<IEnumerable<ManufacturerDto>> GetManufacturersAsync(PageRequest page);
    Task<ManufacturerDto> GetManufacturerByIdAsync(Guid id);
}

