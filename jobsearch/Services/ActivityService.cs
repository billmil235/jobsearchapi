namespace JobSearch.Services;

public class ActivityService(JobSearchContext jobSearchContext)
{
    private readonly JobSearchContext _jobSearchContext = jobSearchContext;
}