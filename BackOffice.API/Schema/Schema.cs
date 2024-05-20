using BackOffice.API.Data;
using EntityGraphQL.AspNet;
using EntityGraphQL.Schema;

namespace BackOffice.API.Schema;

public static class Schema
{
    public static Action<AddGraphQLOptions<Context>> AddGraphQlOptions => options =>
    {
        options.ConfigureSchema = ConfigureSchema;
    };

    private static void ConfigureSchema(SchemaProvider<Context> schemaProvider)
    {
        schemaProvider.AddMutationsFrom<CoursesMutations>();
        // write to file
        File.WriteAllText("schema.graphql", schemaProvider.ToGraphQLSchemaString());
    }
}

