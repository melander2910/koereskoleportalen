using BackOffice.API.Dto;
using BackOffice.API.Models;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Services;

public class ProductionUnitService : IProductionUnitService
{
    private readonly IProductionUnitRepository _productionUnitRepository;
    
    public ProductionUnitService(IProductionUnitRepository productionUnitRepository)
    {
        _productionUnitRepository = productionUnitRepository;
    }

    public async Task<ProductionUnit> AddAsync(ProductionUnitCreateDto productionUnitCreateDto)
    {
        // TODO: Implement
        var productionUnit = new ProductionUnit
        {
            ProductionUnitNumber = productionUnitCreateDto.ProductionUnitNumber,
            CVR = productionUnitCreateDto.CVR,
            Name = productionUnitCreateDto.Name,
            City = productionUnitCreateDto.City,
            PhoneNumber = productionUnitCreateDto.PhoneNumber,
            Email = productionUnitCreateDto.Email,
            StreetAddress = productionUnitCreateDto.StreetAddress,
            Zipcode = productionUnitCreateDto.Zipcode,
            OrganisationId = new Guid("079de1a7-f995-47bf-86f3-c98a9dcee55b"),
            TenantId = ""
        };
        
        return await _productionUnitRepository.AddAsync(productionUnit);
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllAsync()
    {
        return await _productionUnitRepository.GetAllAsync();
    }

    public async Task<IEnumerable<ProductionUnit>> GetAllByUserIdAsync(Guid userId)
    {
        return await _productionUnitRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<ProductionUnit> FindAsync(Guid id)
    {
        return await _productionUnitRepository.FindAsync(id);
    }

    public async Task<ProductionUnit> Update(Guid id, ProductionUnitUpdateDto productionUnitUpdateDto)
    {
        var productionUnit = await _productionUnitRepository.FindAsync(id);
        productionUnit.Name = productionUnitUpdateDto.Name;
        productionUnit.PhoneNumber = productionUnitUpdateDto.PhoneNumber;
        productionUnit.Email = productionUnitUpdateDto.Email;
        productionUnit.City = productionUnitUpdateDto.City;
        productionUnit.StreetAddress = productionUnitUpdateDto.StreetAddress;
        productionUnit.Zipcode = productionUnitUpdateDto.Zipcode;
        
        return await _productionUnitRepository.Update(id, productionUnit);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _productionUnitRepository.Delete(id);
    }
}