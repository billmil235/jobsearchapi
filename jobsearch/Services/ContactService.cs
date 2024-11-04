using JobSearch.Entities;

namespace JobSearch.Services;

public class ContactService(JobSearchContext jobSearchContext)
{
    private readonly JobSearchContext _jobSearchContext = jobSearchContext;

    public async Task AddContact()
    {
        Contact newContact = new();

        _jobSearchContext.Contacts.Add(newContact);
        await _jobSearchContext.SaveChangesAsync();
    }
}