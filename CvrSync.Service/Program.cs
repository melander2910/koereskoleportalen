using CvrSync.Service.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {
        ElasticSearchService elasticSearchService = new ElasticSearchService();
        
        await elasticSearchService.GetProductionUnits();
        await elasticSearchService.GetOrganisations();
        
        Console.WriteLine("Database seeded");
    }
    
    
}
