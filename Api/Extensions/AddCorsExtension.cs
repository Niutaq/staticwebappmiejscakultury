namespace Api.Extensions;

public static class AddCorsExtension
{
    public static IServiceCollection CorsExtension(this IServiceCollection services)
    {
        services.AddCors(opt => 
        {
            opt.AddDefaultPolicy(pol =>
            {
                pol.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}