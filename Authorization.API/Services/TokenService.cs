using Exodus.Cotacao.Authorization.Context.Repositories.Interfaces;
using Exodus.Cotacao.Authorization.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Exodus.Cotacao.Authorization.Models.Users;
using Exodus.Cotacao.Authorization.Models.Token;

public class TokenService : ITokenService
{
    private const string SecretKeyConfig = "Jwt:Key";
    private const string IssuerConfig = "Jwt:Issuer";
    private const string AudienceConfig = "Jwt:Audience";

    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public TokenService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public (string AccessToken, RefreshToken RefreshToken) GenerateToken(User user)
    {
        if (!IsValidUser(user))
            return (String.Empty, null);

        var secretKey = GetSecretKey();
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokensOptions = new JwtSecurityToken(
            issuer: _configuration[IssuerConfig] ?? String.Empty,
            audience: _configuration[AudienceConfig] ?? String.Empty,
            claims: new[]
            {
                new Claim(ClaimTypes.Name, user.CPF),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            },
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: signingCredentials
        );


        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokensOptions);

        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            Expiration = DateTime.Now.AddMinutes(15),
            UserId = user.CPF
        };

        return (accessToken, refreshToken);
    }


    private bool IsValidUser(User user)
    {
        var _userDataBase = _userRepository.Check(user.CPF);
        return user.CPF == _userDataBase.CPF && user.Password == _userDataBase.Password && user.Email == _userDataBase.Email;
    }

    private SymmetricSecurityKey GetSecretKey()
    {
        var secretKeyString = _configuration[SecretKeyConfig] ?? String.Empty;
        if (String.IsNullOrEmpty(secretKeyString))
        {
            throw new InvalidOperationException("A chave secreta não pode ser vazia.");
        }

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
    }
}
