namespace UseManagementApi.Models;

public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    IList<User> Users {get;set;} = new List<User>();
}