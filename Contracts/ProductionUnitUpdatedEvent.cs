namespace Contracts;

public class ProductionUnitUpdatedEvent
{
    public string ProductionUnitNumber { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string StreetAddress { get; set; }
    public string Zipcode { get; set; }
}