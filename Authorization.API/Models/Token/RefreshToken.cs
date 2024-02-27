using System;

namespace Exodus.Cotacao.Authorization.Models.Token
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
    }

}
