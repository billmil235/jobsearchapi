using jobsearch.Context;
using JobSearch.Entities;
using JobSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jobsearch.Services.Commands.Contacts;

public class CreateApplicationContactCommand(JobSearchContext context)
{
    public async Task<IResult> ExecuteAsync([FromBody] ApplicationContactModel contact)
    {
        var application = await context.Applications
            .FirstOrDefaultAsync(x => x.ApplicationId == contact.ApplicationId);

        if (application == null)
        {
            return Results.Problem(new ProblemDetails
            {
                Detail = "Failed to create application contact.",
                Status = StatusCodes.Status404NotFound,
                Title = "Application not found"
            });
        }

        var newContact = new ApplicationContact
        {
            ApplicationId = application.ApplicationId,
            ContactType = contact.ContactType,
            ContactValue = contact.ContactValue
        };

        application.ApplicationContacts.Add(newContact);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}