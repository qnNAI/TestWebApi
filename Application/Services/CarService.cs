using Application.Common.Contracts.Repositories;
using Application.Common.Contracts.Services;
using Application.Models.Car;
using Application.Models.Common;
using Domain.Entities;
using Mapster;

namespace Application.Services;

internal class CarService : ICarService {
    private readonly ICarRepository _repository;

    public static string P { get; set; }

    public CarService(ICarRepository repository) {
        this._repository = repository;
    }

    public async Task<IEnumerable<CarDto>> GetPageAsync(PageRequest page) {
        var entities = await _repository.GetPageAsync(page);

        return entities.Adapt<IEnumerable<CarDto>>();
    }

    public async Task<CarDto> GetByIdAsync(Guid id) {
        var entity = await _repository.GetByIdAsync(id);

        return entity.Adapt<CarDto>();
    }

    public async Task<CarDto> CreateAsync(CreateCarRequest createRequest) {
        var created = await _repository.CreateAsync(createRequest.Adapt<Car>());

        return created.Adapt<CarDto>();
    }

    public async Task<CarDto> UpdateAsync(UpdateCarRequest updateRequest) {
        var updated = await _repository.UpdateAsync(updateRequest.Adapt<Car>());

        return updated.Adapt<CarDto>();
    }

    public Task DeleteAsync(Guid id) {
        return _repository.DeleteAsync(id);
    }
}

