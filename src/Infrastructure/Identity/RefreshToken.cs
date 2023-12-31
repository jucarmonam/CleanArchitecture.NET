﻿using Domain.Common;

namespace Infrastructure.Identity;
public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public string JwtId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateExpire { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
