using BackOffice.API.Services.Interfaces;

namespace BackOffice.API.Middleware;

public class TenantResolver
{
    private readonly RequestDelegate _next;

    public TenantResolver(RequestDelegate next)
    {
        _next = next;
    }

    // TODO: The only time you would resolve the tenant from a header or subdomain, is in an unauthenticated request like login.
    // Once the user is authenticated and issued a token, the tenant key would always be read from that token (or cookie)
    // which are imposible to modify. So in the tenant resolver middleware, you would first check to see if there is a token present and if so,
    // obtain the tenant value from a claim in that token.
    
    // , ICurrentSubTenantService currentSubTenantService
    
    public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
    {
        // var gg = DetermineTenant(context);
        // context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
        // tenantFromHeader = "Lisbeths Køreskole";
        // if (string.IsNullOrEmpty(tenantFromHeader) == false)
        // {
        //     await currentTenantService.SetTenant(tenantFromHeader);
        // }
        await _next(context);
    }

    // private string DetermineTenant(HttpContext context)
    // {
    //     var user = context.User;
    //     
    //     if (user.HasClaim(c => c.Type == "tenant"))
    //     {
    //         return user.FindFirst("tenant")?.Value ?? string.Empty;
    //     }
    //
    //     return "default-tenant";
    // }
}

// context.Request.Headers.TryGetValue("subTenant", out var subTenantFromHeader);

// if (string.IsNullOrEmpty(tenantFromHeader) == false)
// {
//     await currentSubTenantService.SetSubTenant(subTenantFromHeader);
// }
// await currentTenantService.SetTenant("Lisbeths Køreskole");
// await currentSubTenantService.SetSubTenant("1019542722");