namespace Application.Common.Models;
public class TokenRequestVM
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}
