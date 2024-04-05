using CvrSync.Service.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {
        ElasticSearchService elasticSearchService = new ElasticSearchService();
        
        await elasticSearchService.GetCompanies();
        await elasticSearchService.GetProductionUnits();
        
        Console.WriteLine("Database seeded");
    }
    
    
}
