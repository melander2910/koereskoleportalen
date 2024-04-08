namespace CvrSync.Service.Models;
using Nest;


public class CompanyQuery
{
    [PropertyName("Vrvirksomhed")]
    public Organisation Organisation { get; set; }
}

public class Organisation
{
    [PropertyName("cvrNummer")]
    public int CompanyNumber { get; set; }
    [PropertyName("virksomhedMetadata")]
    public MetaData MetaData { get; set; }
    [PropertyName("penheder")]
    public IList<ProductionUnit> ProductionUnits { get; set; }
}


public class ProductionUnit
{
    [PropertyName("pNummer")]
    public int UnitNumber { get; set; }
}

public class MetaData
{
    [PropertyName("nyesteNavn")]
    public NewestName NewestName { get; set; }
    [PropertyName("nyesteHovedbranche")]
    public BusinessType BusinessType { get; set; }
    [PropertyName("nyesteBeliggenhedsadresse")]
    public Address Address { get; set; }
    [PropertyName("nyesteKontaktoplysninger")]
    public IList<string> ContactInformation { get; set; }
    [PropertyName("sammensatStatus")]
    public string Status { get; set; }
}

public class NewestName
{
    
    [PropertyName("navn")]
    public string Name { get; set; }
}

public class Address
{
    [PropertyName("vejnavn")]
    public string RoadName { get; set; }
    [PropertyName("husnummerFra")]
    public int? HouseNumber { get; set; }
    [PropertyName("etage")]
    public string? Story { get; set; }
    [PropertyName("sidedoer")]
    public string? Door { get; set; }
    
    [PropertyName("postnummer")]
    public int ZipCode { get; set; }
    [PropertyName("kommune")]
    public Municipality Municipality { get; set; }
}

public class Municipality
{
    [PropertyName("kommuneNavn")]
    public string Name { get; set; }
}

public class BusinessType
{
    [PropertyName("branchekode")]
    public string BusinessTypeCode { get; set; }
}