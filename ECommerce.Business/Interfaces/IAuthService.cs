using ECommerce.Models.Dtos.UserDtos;
using ECommerce.Models.ViewModels.Helper;

namespace ECommerce.Business.Interfaces;

public interface IAuthService
{
    ApiResponse Authenticate(UserDto userDto);
    string GenerateJwtToken(UserDto userDto);
}