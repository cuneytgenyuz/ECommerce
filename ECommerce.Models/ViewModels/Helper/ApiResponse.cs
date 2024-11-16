namespace ECommerce.Models.ViewModels.Helper;

public class ApiResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    public object Data { get; set; }
}