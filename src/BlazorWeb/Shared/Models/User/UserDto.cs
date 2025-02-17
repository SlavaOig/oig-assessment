namespace BlazorWeb.Shared.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string LoginName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }
}
