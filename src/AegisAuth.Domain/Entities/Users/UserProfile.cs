using System;
using AegisAuth.Domain.Entities.Base;

namespace AegisAuth.Domain.Entities.Users;

public class UserProfile : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? PreferredLanguage { get; set; } = "tr";
    public string? TimeZone { get; set; } = "Europe/Istanbul";

    public User User { get; set; }

}
