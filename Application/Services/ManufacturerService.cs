using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Repositories;
using Application.Common.Contracts.Services;
using Application.Models.Common;
using Application.Models.Manufacturer;
using Mapster;
using MapsterMapper;

namespace Application.Services;

internal class ManufacturerService : IManufacturerService {
	private readonly IManufacturerRepository _repository;

	public ManufacturerService(IManufacturerRepository repository) {
		_repository = repository;
	}

	public async Task<IEnumerable<ManufacturerDto>> GetManufacturersAsync(PageRequest page) {
		var entities = await _repository.GetManufacturersAsync(page);

		return entities.Adapt<IEnumerable<ManufacturerDto>>();
	}

    public async Task<ManufacturerDto> GetManufacturerByIdAsync(Guid id) {
        var entity = await _repository.GetManufacturerByIdAsync(id);

		return entity.Adapt<ManufacturerDto>();
    }

}
