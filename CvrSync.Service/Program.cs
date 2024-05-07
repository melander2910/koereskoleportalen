using CvrSync.Service.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // ElasticSearchService elasticSearchService = new ElasticSearchService();
        NewElasticSearchService newElasticSearchService = new NewElasticSearchService();
        
        // await elasticSearchService.GetProductionUnits();
        // await elasticSearchService.GetOrganisations();
        await newElasticSearchService.OrganisationsToSQL();
        await newElasticSearchService.ProductionUnitsToSQL();
        Console.WriteLine("Database seeded");
    }
    
    
}
