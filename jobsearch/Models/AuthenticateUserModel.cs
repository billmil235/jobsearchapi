namespace JobSearch.Models;

public record AuthenticateUserModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}