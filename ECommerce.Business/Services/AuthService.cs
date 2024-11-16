using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Business.Interfaces;
using ECommerce.Core.Helpers;
using ECommerce.Models.Dtos.UserDtos;
using ECommerce.Models.ViewModels.Helper;
using ECommerce.Models.ViewModels.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Business.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IOptions<JwtSettings> jwtOptions, ILogger<AuthService> logger)
    {
        _jwtSettings = jwtOptions.Value;
        _logger = logger;
    }
    public ApiResponse Authenticate(UserDto userDto)
    {
        if (userDto.UserName != "admin" || userDto.Password != "1")
        {
            return Helper.CreateResponse(
                code: "400",
                message: "Kullanıcı adı veya şifre yanlış!",
                success: false,
                data: null
            );
        }

        string token = GenerateJwtToken(userDto);

        return Helper.CreateResponse(
            code: "200",
            message: "Başarılı",
            success: true,
            data: new UserTokenDto
            {
                Token = token,
                UserName = userDto.UserName
            }
        );
    }


    public string GenerateJwtToken(UserDto userDto)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userDto.UserName)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHour),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}