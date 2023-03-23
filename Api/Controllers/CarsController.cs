using Application.Common.Contracts.Services;
using Application.Models.Car;
using Application.Models.Common;
using Application.Models.Manufacturer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase {
    private readonly ICarService _service;

    public CarsController(ICarService service) {
        this._service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarDto>), 200)]
    public async Task<IActionResult> GetAllAsync(int page, int pageSize) {
        var result = await _service.GetPageAsync(new PageRequest(page, pageSize));

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById([FromRoute] Guid id) {
        var result = await _service.GetByIdAsync(id);

        if(result is null) {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CarDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddAsync([FromBody] CreateCarRequest createRequest) {
        var result = await _service.CreateAsync(createRequest);

        if(result is null) {
            return BadRequest();
        }

        return CreatedAtAction("GetById", new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCarRequest updateRequest, [FromRoute] Guid id) {
        if(updateRequest.Id != id) {
            return BadRequest("Invalid id");
        }

        var result = await _service.UpdateAsync(updateRequest);

        if(result is null) {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
