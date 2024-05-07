using CvrSync.Service.Models;
using Microsoft.Extensions.Configuration;
using Nest;
using Npgsql;

namespace CvrSync.Service.Services;

public class NewElasticSearchService
{
    private string _cvrUsername;
    private string _cvrPassword;
    private string _connectionString;
    
    public NewElasticSearchService()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        CvrAuthenticationSettings cvrAuthenticationSettingsSettings = config.GetRequiredSection("CvrAuth").Get<CvrAuthenticationSettings>();
        _connectionString = config.GetRequiredSection("BackOfficeDatabase").GetValue<string>("ConnectionString");
        _cvrUsername = cvrAuthenticationSettingsSettings.Username;
        _cvrPassword = cvrAuthenticationSettingsSettings.Password;
    }

    public async Task OrganisationsToSQL()
    {
        string organisationIndex = "/cvr-permanent/virksomhed/";
        var settings = new ConnectionSettings(new Uri("http://distribution.virk.dk"))
            .BasicAuthentication(_cvrUsername, _cvrPassword)
            .DefaultIndex(organisationIndex);
        
        // Create an Elasticsearch client
        var client = new ElasticClient(settings);
        
        // Querying Elastic Search API
        var response = await client.SearchAsync<OrganisationQuery>(s => s
            .Size(1000)
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Term(t => t.Organisation.MetaData.BusinessType.BusinessTypeCode, "855300"),
                        m => m.Term(t => t.Organisation.MetaData.Status, "aktiv"),
                        m => m.Range(r => r.Field(f => f.Organisation.MetaData.Address.ZipCode).GreaterThanOrEquals(1000).LessThanOrEquals(2999))
                    )
                )
            )
        );
        
        var doc = response.Documents;
        
        //var backofficeConnectionString = config.GetRequiredSection("BackOfficeDatabase").GetValue<string>("ConnectionString");
        using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        string sql = "INSERT INTO \"Organisations\" (\"Id\", \"CVR\", \"Name\", \"StreetAddress\", \"Zipcode\", \"City\", \"IndustryCode\", \"ClaimedByOwner\", \"CreatedDate\", \"ModifiedDate\", \"Country\", \"IndustryDescription\") VALUES (@Id, @CVR, @Name, @StreetAddress, @Zipcode, @City, @IndustryCode, @ClaimedByOwner, @CreatedDate, @ModifiedDate, @Country, @IndustryDescription)";
        // using NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO organisations (Id, CVR, Name, StreetAddress, Zipcode, City, Municipality, IndustryCode, ClaimedByOwner, CreatedDate, ModifiedDate) VALUES (CVR, Name, StreetAddress, Zipcode, City, IndustryCode, ClaimedByOwner)", connection);
        foreach (var query in doc)
        {
            var cmd = new NpgsqlCommand(sql, connection);
            var streetAddress = $"{query.Organisation.MetaData.Address.RoadName ?? ""} " +
                                $"{query.Organisation.MetaData.Address.HouseNumber?.ToString() ?? ""}" +
                                $"{(query.Organisation.MetaData.Address.Story != null ? ", " : "")}" +
                                $"{query.Organisation.MetaData.Address.Story?.ToString() ?? ""}" +
                                $"{(query.Organisation.MetaData.Address.Story != null ? "." : "")}" +
                                $"{query.Organisation.MetaData.Address.Door?.ToString() ?? ""}";
            
            Console.WriteLine(query.Organisation.MetaData.NewestName.Name);
            var guid = Guid.NewGuid();
            Console.WriteLine(guid);
            cmd.Parameters.AddWithValue("Id", guid);
            cmd.Parameters.AddWithValue("CVR", query.Organisation.OrganisationNumber);
            cmd.Parameters.AddWithValue("Name", query.Organisation.MetaData.NewestName.Name);
            cmd.Parameters.AddWithValue("StreetAddress", streetAddress);
            cmd.Parameters.AddWithValue("Zipcode", query.Organisation.MetaData.Address.ZipCode);
            // cmd.Parameters.AddWithValue("Municipality", query.Organisation.MetaData.Address.Municipality);
            cmd.Parameters.AddWithValue("City", query.Organisation.MetaData.Address.Municipality.Name);
            cmd.Parameters.AddWithValue("IndustryCode", 855300);
            cmd.Parameters.AddWithValue("ClaimedByOwner", false);
            cmd.Parameters.AddWithValue("CreatedDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("ModifiedDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("Country", "Danmark");
            cmd.Parameters.AddWithValue("IndustryDescription", "Køreskoler");
            // Set Status

            int rowsAffected = cmd.ExecuteNonQuery();
        }
    }
    
    public async Task ProductionUnitsToSQL()
    {
        string productionUnitIndex = "/cvr-permanent/produktionsenhed/";
        var settings = new ConnectionSettings(new Uri("http://distribution.virk.dk"))
            .BasicAuthentication(_cvrUsername, _cvrPassword)
            .DefaultIndex(productionUnitIndex);
        
        // Create an Elasticsearch client
        var client = new ElasticClient(settings);
        
        // Querying Elastic Search API
        var response = await client.SearchAsync<DrivingSchoolQuery>(s => s
            .Size(2000)
            .Query(q => q
                .Bool(b => b
                    .Must(
                        m => m.Term(t => t.Unit.MetaData.BusinessType.BusinessTypeCode, "855300"),
                        m => m.Term(t => t.Unit.MetaData.Status, "aktiv"),
                        m => m.Range(r => r.Field(f => f.Unit.MetaData.Address.ZipCode).GreaterThanOrEquals(1000).LessThanOrEquals(2999))
                    )
                )
            )
        );
        
        var doc = response.Documents;
        
        //var backofficeConnectionString = config.GetRequiredSection("BackOfficeDatabase").GetValue<string>("ConnectionString");
        using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        
        string sql = "INSERT INTO \"ProductionUnits\" (\"Id\", \"CVR\", \"TenantId\", \"OrganisationId\", \"ProductionUnitNumber\", \"Name\", \"StreetAddress\", \"Zipcode\", \"City\", \"IndustryCode\", \"CreatedDate\", \"ModifiedDate\", \"Country\", \"IndustryDescription\") VALUES (@Id, @CVR, @TenantId, @OrganisationId, @ProductionUnitNumber, @Name, @StreetAddress, @Zipcode, @City, @IndustryCode, @CreatedDate, @ModifiedDate, @Country, @IndustryDescription)";

        foreach (var query in doc)
        {
            using NpgsqlConnection connection2 = new NpgsqlConnection(_connectionString);
            connection2.Open();
            var organisationId = "";
            var organisationName = "";
            string selectSQL = "SELECT * FROM \"Organisations\" WHERE \"CVR\" = @CVR";
            
            using (NpgsqlCommand selectCmd = new NpgsqlCommand(selectSQL, connection2))
            {
                selectCmd.Parameters.AddWithValue("@CVR", query.Unit.OrganisationRelations[0].OrganisationNumber.ToString());
                NpgsqlDataReader reader = selectCmd.ExecuteReader();
                while(reader.Read()){
                    organisationId = reader["Id"].ToString();
                    organisationName = reader["Name"].ToString();
                }
            }
            
            connection2.Close();
            
            if (organisationId != "")
            {
            var cmd = new NpgsqlCommand(sql, connection);
            var streetAddress = $"{query.Unit.MetaData.Address.RoadName ?? ""} " +
                                $"{query.Unit.MetaData.Address.HouseNumber?.ToString() ?? ""}" +
                                $"{(query.Unit.MetaData.Address.Story != null ? ", " : "")}" +
                                $"{query.Unit.MetaData.Address.Story?.ToString() ?? ""}" +
                                $"{(query.Unit.MetaData.Address.Story != null ? "." : "")}" +
                                $"{query.Unit.MetaData.Address.Door?.ToString() ?? ""}";
            
            Console.WriteLine(query.Unit.MetaData.NewestName.Name);
            var id = organisationId;
            var guid = Guid.NewGuid();
            Console.WriteLine(guid);
            cmd.Parameters.AddWithValue("Id", guid);         
            cmd.Parameters.AddWithValue("CVR", query.Unit.OrganisationRelations[0].OrganisationNumber).ToString();
            cmd.Parameters.AddWithValue("ProductionUnitNumber", query.Unit.ProductionUnitNumber.ToString());
            cmd.Parameters.AddWithValue("OrganisationId", new Guid(organisationId));
            cmd.Parameters.AddWithValue("TenantId", query.Unit.OrganisationRelations[0].OrganisationNumber).ToString();

            cmd.Parameters.AddWithValue("Name", query.Unit.MetaData.NewestName.Name);
            cmd.Parameters.AddWithValue("StreetAddress", streetAddress);
            cmd.Parameters.AddWithValue("Zipcode", query.Unit.MetaData.Address.ZipCode);
            // cmd.Parameters.AddWithValue("Municipality", query.Organisation.MetaData.Address.Municipality);
            cmd.Parameters.AddWithValue("City", query.Unit.MetaData.Address.Municipality.Name);
            cmd.Parameters.AddWithValue("IndustryCode", 855300);
            cmd.Parameters.AddWithValue("CreatedDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("ModifiedDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("Country", "Danmark");
            cmd.Parameters.AddWithValue("IndustryDescription", "Køreskoler");
            
            // set TenantId to Name of organisation
            // set OrganisationId to Guid Id of organisation
            // Set Status
            
                int rowsAffected = cmd.ExecuteNonQuery();
            }

        }
    }
}