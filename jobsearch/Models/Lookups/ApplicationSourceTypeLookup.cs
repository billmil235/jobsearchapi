namespace JobSearch.Models.Lookups;

public record struct ApplicationSourceTypeLookup
{
    public int ApplicationSourceTypeId { get; init; }
    public string ApplicationSourceTypeName { get; init; }
};