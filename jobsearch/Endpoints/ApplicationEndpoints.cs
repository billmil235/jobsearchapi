using System.Security.Claims;
using JobSearch.Extensions;
using JobSearch.Models;
using JobSearch.Services;
using jobsearch.Services.Commands.Application;
using JobSearch.Services.Queries.Application;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Endpoints;

public static class ApplicationEndpoints
{
    public static RouteGroupBuilder RegisterApplicationEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", 
            (ApplicationModel application, ClaimsPrincipal user, CreateApplicationCommand createApplicationCommand) => createApplicationCommand.CreateApplication(application));
        
        group.MapPut("/", (ApplicationModel applicationModel, UpdateApplicationCommand updateApplicationCommand, ClaimsPrincipal user) => updateApplicationCommand.UpdateApplication(applicationModel));
        
        group.MapGet("/List/{searchId:guid}", 
            (Guid searchId, [FromQuery] bool activeOnly, ClaimsPrincipal user, GetAllApplicationsBySearchIdQuery getAllApplicationsBySearchIdQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) => getAllApplicationsBySearchIdQuery.ListApplications(searchId, pageNumber, pageSize, activeOnly));
        
        group.MapGet("/Types", 
           async (LookupService lookupService) => Results.Ok(await lookupService.GetApplicationTypes()) );

        group.MapGet("/Sources", 
            async (LookupService lookupService) => Results.Ok(await lookupService.GetApplicationSourceTypes()) );
        
        group.MapDelete("/{applicationId}",
            (string applicationId, DeleteApplicationByApplicationIdCommand deleteApplicationByApplicationIdCommand) => deleteApplicationByApplicationIdCommand.DeleteApplication(applicationId));

        group.MapGet("/Preview/{applicationId}",
            (string applicationId, ClaimsPrincipal user, GetApplicationPreviewByApplicationIdQuery getApplicationPreviewByApplicationIdQuery) => getApplicationPreviewByApplicationIdQuery.GetApplicationPreview(applicationId));

        group.MapGet("/{applicationId}",
            async (string applicationId, ClaimsPrincipal user, GetApplicationByApplicationIdQuery getApplicationByApplicationIdQuery) =>
            {
                Guid userId = user.GetGuid();
                Guid applicationGuid = new (applicationId);
                return await getApplicationByApplicationIdQuery.GetApplicationByApplicationId(applicationGuid, userId);
            });
        
        return group;
    }
}