using BackOffice.API.Dto;
using BackOffice.API.Models.DatabaseEntities;
using BackOffice.API.Repositories.Interfaces;
using BackOffice.API.Services.Interfaces;
using Contracts;
using MassTransit;

namespace BackOffice.API.Services;

public class ProductionUnitService : IProductionUnitService
{
    private readonly IProductionUnitRepository _productionUnitRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    
    // comment
    public ProductionUnitService(IProductionUnitRepository productionUnitRepository, IPublishEndpoint publishEndpoint)
    {
        _productionUnitRepository = productionUnitRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ProductionUnit> AddAsync(ProductionUnitCreateDto productionUnitCreateDto)
    {
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
        
        var updatedProductionUnit = await _productionUnitRepository.Update(id, productionUnit);
        
        await _publishEndpoint.Publish(
            new ProductionUnitUpdatedEvent
            {
                ProductionUnitNumber = Int32.Parse(productionUnit.ProductionUnitNumber),
                Name = productionUnitUpdateDto.Name,
                /*PhoneNumber = productionUnitUpdateDto.PhoneNumber,
                Email = productionUnitUpdateDto.Email,*/
                City = productionUnitUpdateDto.City,
                Address = productionUnitUpdateDto.StreetAddress,
                ZipCode = Int32.Parse(productionUnitUpdateDto.Zipcode)
            });

        return updatedProductionUnit;
    }

    public async Task<bool> Delete(Guid id)
    {
        var productionUnit = await _productionUnitRepository.FindAsync(id);
        if (productionUnit == null)
        {
            throw new Exception($"Production unit with id: ${id}, was not found");
        }
        
        ProductionUnitRemoved productionUnitRemoved = new ProductionUnitRemoved
        {
            ProductionUnit = productionUnit,
            TenantId = productionUnit.TenantId,
            RemovedDate = DateTime.UtcNow
        };
        
        return await _productionUnitRepository.Delete(id, productionUnitRemoved);
    }

    public async Task<ProductionUnit> FindByProductionUnitNumber(string productionUnitNumber)
    {
        return await _productionUnitRepository.FindByProductionUnitNumber(productionUnitNumber);
    }
}