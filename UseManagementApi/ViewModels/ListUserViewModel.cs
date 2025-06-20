namespace UseManagementApi.ViewModels;

public class ListUserViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public ListRoleViewModel? Role { get; set; }
    
}