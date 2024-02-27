using Exodus.Cotacao.Authorization.Models.Token;
using Exodus.Cotacao.Authorization.Models.Users;

namespace Exodus.Cotacao.Authorization.Services.Interfaces
{
    public interface ITokenService
    {
        (string AccessToken, RefreshToken RefreshToken) GenerateToken(User user);
    }
}
