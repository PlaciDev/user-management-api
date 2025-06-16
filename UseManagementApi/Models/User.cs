namespace UseManagementApi.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? IsActive { get; set; }
    public string? CreatedAt { get; set; }

    public Role Role { get; set; }
}