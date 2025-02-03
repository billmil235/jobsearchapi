using JobSearch.Entities;

namespace JobSearch.Services;

public class ContactService(JobSearchContext jobSearchContext)
{
    public async Task AddContact()
    {
        Contact newContact = new();

        jobSearchContext.Contacts.Add(newContact);
        await jobSearchContext.SaveChangesAsync();
    }
}