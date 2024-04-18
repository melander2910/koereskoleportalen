using BackOffice.API.Dto;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductionUnitController : ControllerBase
{
    private readonly IProductionUnitService _productionUnitService;
    private readonly IUserService _userService;
    public ProductionUnitController(IProductionUnitService productionUnitService, IUserService userService)
    {
        _productionUnitService = productionUnitService;
        _userService = userService;
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
        var productionUnits = await _productionUnitService.GetAllAsync();
        return Ok(productionUnits);
    }
    
    // TODO: There is always at least one Production Unit matching the organisation?
    [HttpPost(Name = "CreateProductionUnit")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] ProductionUnitCreateDto productionUnitCreateDto)
    {
        var productionUnit = await _productionUnitService.AddAsync(productionUnitCreateDto);
        return Ok(productionUnit);
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
    
    [HttpGet]
    [Route("{id}/users", Name = "GetUsersByProductionUnitId")]
    public async Task<ActionResult<IEnumerable<ProductionUnit>>> GetUsersByProductionUnitId(Guid id)
    {
        var userProductionUnits = await _userService.GetAllByProductionUnitIdAsync(id);
        return Ok(userProductionUnits);
    }
}