using System;
namespace CoffeManager.Common
{
    public class OAuthToken
    {
        public OAuthToken()
        {
        }

        public OAuthToken(string accessToken, string tokenType, DateTime expirationDate, string refreshToken)
        {
            AccessToken = accessToken;
            TokenType = tokenType;
            ExpirationDate = expirationDate;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string RefreshToken { get; set; }

        public static OAuthToken FromDTO(OAuthTokenDTO dto)
        {
            var entity = new OAuthToken
            {
                AccessToken = dto.AccessToken,
                TokenType = dto.TokenType,
                ExpirationDate = DateTime.UtcNow.AddSeconds(dto.ExpiresIn),
                RefreshToken = dto.RefreshToken
            };

            return entity;
        }
    }
}
