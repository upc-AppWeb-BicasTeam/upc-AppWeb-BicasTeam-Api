using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BicasTeam.MoviGestion.API.IAM.Application.Internal.OutboundServices;
using BicasTeam.MoviGestion.API.IAM.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BicasTeam.MoviGestion.API.IAM.Infrastructure.Tokens.JWT.Services;

public class TokenService : ITokenService
{
    private readonly TokenSettings _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _tokenSettings = tokenSettings.Value;

        if (string.IsNullOrEmpty(_tokenSettings.Secret))
        {
            throw new ArgumentNullException(nameof(_tokenSettings.Secret), "El secreto del token no puede ser nulo o vacío.");
        }

        Console.WriteLine($"Secreto del token cargado correctamente.");
    }

    public string GenerateToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
        }

        if (string.IsNullOrEmpty(user.Username) || user.Id == null)
        {
            throw new ArgumentException("El ID del usuario y el nombre de usuario no pueden ser nulos o vacíos.");
        }

        // Verificar que la clave secreta está siendo cargada correctamente
        Console.WriteLine($"Clave secreta utilizada: {_tokenSettings.Secret}");

        // Convertimos el secreto del token a bytes usando UTF8
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));

        // Incluimos Issuer y Audience en el descriptor del token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),  
                new Claim(ClaimTypes.Name, user.Username),
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token válido por 7 días
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _tokenSettings.Issuer,  // Incluimos el Issuer configurado
            Audience = _tokenSettings.Audience  // Incluimos la Audience configurada
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Imprimir el token generado en los logs
        var tokenString = tokenHandler.WriteToken(token);
        Console.WriteLine($"Token generado: {tokenString}");

        return tokenString;  // Convertimos el token a string
    }


    public async Task<int?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)); // UTF8 para la clave secreta

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,  // Validamos el Issuer
                ValidateAudience = true,  // Validamos la Audience
                ValidateLifetime = true,  // Validamos la expiración del token
                ValidIssuer = _tokenSettings.Issuer,  // Issuer configurado
                ValidAudience = _tokenSettings.Audience,  // Audience configurada
                ClockSkew = TimeSpan.Zero  // Sin diferencia de tiempo permitida
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.Sid);

            return int.Parse(userIdClaim?.Value ?? "0");  // Devolvemos el ID del usuario si es válido
        }
        catch (SecurityTokenException e)
        {
            Console.WriteLine($"Error de validación del token: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error desconocido: {e.Message}");
            return null;
        }
    }
}
