using Exodus.Cotacao.Authorization.Models.Users;

namespace Exodus.Cotacao.Authorization.Context.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Register(User user);

        User? Check(string CPF);

        User? Login (string CPForEmail, string Password);
        User? Delete(string Password, string ConfirmPassword);
    }
}
