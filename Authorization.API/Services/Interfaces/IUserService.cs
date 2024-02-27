using Exodus.Cotacao.Authorization.Models.Users;

namespace Exodus.Cotacao.Authorization.Services.Interfaces
{
    public interface IUserService
    {
        void Register(User user);
        User? Check(string CPF);
        User? Login(string CPForEmail, string Password);
        User? Delete(string Password, string ConfirmPassword);
    }
}
