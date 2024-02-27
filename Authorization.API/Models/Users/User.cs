namespace Exodus.Cotacao.Authorization.Models.Users;

public enum Role
{
    Admin,
    User,
    Manager
}

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ConfirmEmail { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public Role Role { get; set; } 
}