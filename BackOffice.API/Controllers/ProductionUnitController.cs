using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductionUnitController : ControllerBase
{
    private readonly IProductionUnitService _productionUnitService;
    public ProductionUnitController(IProductionUnitService productionUnitService)
    {
        _productionUnitService = productionUnitService;
    }
    
    [HttpGet("{id}" ,Name = "GetProductionUnitById")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var productionUnit = await _productionUnitService.FindAsync(id);
        return Ok(productionUnit);
    }

    [HttpGet(Name = "GetAllProductionUnits")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        return Ok(200);
    }

    [HttpPost(Name = "CreateProductionUnit")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] string model)
    {
        return Ok(200);
    }
    
    [HttpPut("{id}", Name = "UpdateProductionUnit")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id)
    {
        return Ok(200);
    }
    
    [HttpDelete("{id}", Name = "DeleteProductionUnit")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        return Ok(200);
    }
}