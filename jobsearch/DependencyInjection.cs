using JobSearch.Services;
using jobsearch.Services.Commands.Application;
using JobSearch.Services.Commands.JobSearch;
using JobSearch.Services.Queries.Application;
using JobSearch.Services.Queries.JobSearch;

namespace JobSearch;

public static class DependencyInjection
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddScoped<UsersService>();
        builder.Services.AddScoped<LookupService>();

        builder.Services.AddScoped<CreateApplicationCommand>();
        builder.Services.AddScoped<DeleteApplicationByApplicationIdCommand>();
        builder.Services.AddScoped<GetApplicationPreviewByApplicationIdQuery>();
        builder.Services.AddScoped<GetAllApplicationsBySearchIdQuery>();
        builder.Services.AddScoped<GetApplicationByApplicationIdQuery>();

        builder.Services.AddScoped<GetJobSearchByUserIdQuery>();
        builder.Services.AddScoped<CreateJobSearchCommand>();
        builder.Services.AddScoped<DeleteSearchBySearchIdCommand>();
        builder.Services.AddScoped<UpdateJobSearchCommand>();
    }
}