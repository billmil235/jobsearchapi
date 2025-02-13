namespace JobSearch.Models.Lookups;

public record struct ApplicationTypeLookup
{
    public int ApplicationTypeId { get; init; }
    public string ApplicationTypeName { get; init; }
}