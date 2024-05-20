using System.Linq.Expressions;
using BackOffice.API.Data;
using BackOffice.API.Models.DatabaseEntities;
using EntityGraphQL.Schema;

namespace BackOffice.API.Schema;

public class CoursesMutations
{   
    [GraphQLMutation("Add a new course to the system")]
    public Expression<Func<Context, Course>> AddNewCourse(Context db, string name, Guid productionUnitId, string tenantId, string subtenantId)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            Name = name,
            ProductionUnitId = productionUnitId,
            TenantId = tenantId,
            SubTenantId = subtenantId
        };
        
        db.Courses.Add(course);
        db.SaveChanges();

        return (context) => context.Courses.SingleOrDefault(x => x.Id == course.Id);
    }
}