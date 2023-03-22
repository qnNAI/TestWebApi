using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Repositories;
using Application.Common.Contracts.Services;
using Application.Models.Common;
using Application.Models.Manufacturer;
using Domain.Entities;
using Mapster;
using MapsterMapper;

namespace Application.Services;

internal class ManufacturerService : IManufacturerService {
	private readonly IManufacturerRepository _repository;

	public ManufacturerService(IManufacturerRepository repository) {
		_repository = repository;
	}

	public async Task<IEnumerable<ManufacturerDto>> GetPageAsync(PageRequest page) {
		var entities = await _repository.GetPageAsync(page);

		return entities.Adapt<IEnumerable<ManufacturerDto>>();
	}

    public async Task<ManufacturerDto> GetByIdAsync(Guid id) {
        var entity = await _repository.GetByIdAsync(id);

		return entity.Adapt<ManufacturerDto>();
    }

    public async Task<ManufacturerDto> GetByNameAsync(string name) {
        var entity = await _repository.GetByNameAsync(name);

        return entity.Adapt<ManufacturerDto>();
    }

    public async Task<ManufacturerDto> CreateAsync(CreateManufRequest createRequest) {
        var created = await _repository.CreateAsync(createRequest.Adapt<Manufacturer>());

        return created.Adapt<ManufacturerDto>();
    }

    public async Task<ManufacturerDto> UpdateAsync(UpdateManufRequest updateRequest) {
        var updated = await _repository.UpdateAsync(updateRequest.Adapt<Manufacturer>());

        return updated.Adapt<ManufacturerDto>();
    }

    public async Task DeleteAsync(Guid id) {
        await _repository.DeleteAsync(id);
    }
}
