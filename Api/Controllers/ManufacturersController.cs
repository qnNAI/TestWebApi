using Application.Common.Contracts.Services;
using Application.Models.Common;
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
	public async Task<IActionResult> GetManufacturersAsync(int page, int pageSize) {
		var result = await _service.GetManufacturersAsync(new PageRequest(page, pageSize));

		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetManufacturerById([FromRoute] Guid id) { 
		var result = await _service.GetManufacturerByIdAsync(id);

		if (result is null) {
			return NotFound();
		}

		return Ok(result);
	}

}

