namespace Exodus.Cotacao.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Name { get; set; } 
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
        public string Email { get; set; } 
        public string ConfirmEmail { get; set; } 
        public string Telephone { get; set; } 
        public string CPF { get; set; }
    }
}
