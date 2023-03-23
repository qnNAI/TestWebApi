using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Car;
using Application.Models.Common;

namespace Application.Common.Contracts.Services;

public interface ICarService {
    Task<IEnumerable<CarDto>> GetPageAsync(PageRequest page);
    Task<CarDto> GetByIdAsync(Guid id);

    Task<CarDto> CreateAsync(CreateCarRequest createRequest);
    Task<CarDto> UpdateAsync(UpdateCarRequest updateRequest);
    Task DeleteAsync(Guid id);
}
