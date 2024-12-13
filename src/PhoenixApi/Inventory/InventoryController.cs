using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoenixApi.Inventory;

[Route("api/v1/inventory")]
[ApiController]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _service;

    public InventoryController(InventoryService service)
    {
        _service = service;
    }

    [HttpGet("{name}")]
    public IActionResult Get(string name)
    {
        var dto = _service.Get(name);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Insert(InventoryFormDTO dto)
    {
        _service.Insert(dto);
        return Ok(new {message = "Success Add Inventory"});
    }

    [HttpPut]
    public IActionResult Update(InventoryFormDTO dto)
    {
        _service.Update(dto);
        return Ok(new {message = "Success Update Inventory"});
    }
}
