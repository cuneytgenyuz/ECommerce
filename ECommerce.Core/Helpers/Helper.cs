using ECommerce.Models.ViewModels.Helper;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Core.Helpers;

public class Helper
{
    public static ApiResponse CreateResponse(string code, string message, bool success, object? data = null)
    {
        return new ApiResponse
        {
            Code = code,
            Message = message,
            Success = success,
            Data = data
        };
    }

    public static string EncryptPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }
}