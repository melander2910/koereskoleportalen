using Nest;

namespace CvrSync.Service.Models;

public class DrivingSchoolQuery
{
    [PropertyName("VrproduktionsEnhed")]
    public Unit Unit { get; set; }
}

public class Unit
{
    [PropertyName("pNummer")]
    public int ProductionUnitNumber { get; set; }
    
    [PropertyName("virksomhedsrelation")]
    public IList<CompanyRelation> CompanyRelation { get; set; }
    
    [PropertyName("produktionsEnhedMetadata")]
    public MetaData MetaData { get; set; }
}

public class CompanyRelation
{
    [PropertyName("cvrNummer")]
    public int CompanyNumber { get; set; }
}
