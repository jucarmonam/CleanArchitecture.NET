namespace Application.Common.Models;
public record AuthResultDto(string Token,string RefreshToken, DateTime ExpiresAt);
