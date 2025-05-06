using Microsoft.EntityFrameworkCore;

public class Admin
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}

