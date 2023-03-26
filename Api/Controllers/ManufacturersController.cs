using Application.Common.Contracts.Services;
using Application.Models.Common;
using Application.Models.Manufacturer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase {
	private readonly IManufacturerService _service;

	public ManufacturersController(IManufacturerService service) {
		this._service = service;
	}

	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<ManufacturerDto>), 200)]
	public async Task<IActionResult> GetAllAsync(int page, int pageSize) {
		var result = await _service.GetPageAsync(new PageRequest(page, pageSize));

		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(ManufacturerDto), 200)]
	[ProducesResponseType(404)]
	public async Task<IActionResult> GetById([FromRoute] Guid id) { 
		var result = await _service.GetByIdAsync(id);

		if (result is null) {
			return NotFound();
		}

		return Ok(result);
	}

	[HttpGet("{name}")]
    [ProducesResponseType(typeof(ManufacturerDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByName([FromRoute] string name) {
		var result = await _service.GetByNameAsync(name);

		if (result is null) {
			return NotFound();
		}

		return Ok(result);
	}

	[HttpPost("create")]
	[ProducesResponseType(typeof(ManufacturerDto), 201)]
	[ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] CreateManufRequest createRequest) {
		var result = await _service.CreateAsync(createRequest);

		if(result is null) {
			return BadRequest();
		}

		return CreatedAtAction("GetById", new { id = result.Id }, result);
	}

	[HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ManufacturerDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateManufRequest updateRequest, [FromRoute] Guid id) {
		if (updateRequest.Id != id) {
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
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) {
		await _service.DeleteAsync(id);
		return NoContent();
	}

}

