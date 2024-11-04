using JobSearch.Services;

namespace JobSearch;

public static class DependencyInjection
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddScoped<UsersService>();
        builder.Services.AddScoped<SearchService>();
        builder.Services.AddScoped<ActivityService>();
        builder.Services.AddScoped<ContactService>();
    }
}