using System;
using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class RefreshToken : Entity
{
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? ReplacedByToken { get; private set; }
    public string DevcieInfo { get; private set; }
    public string IpAddress { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsRevoked && !IsExpired;

    private RefreshToken() { }

    public static RefreshToken Create(string token, Guid userId, Guid clientId, DateTime expiresAt, string deviceInfo, string ipAdddress)
    {
        return new RefreshToken
        {
            Token = token,
            UserId = userId,
            ClientId = clientId,
            ExpiresAt = expiresAt,
            DevcieInfo = deviceInfo,
            IpAddress = ipAdddress
        };
    }

    public void Revoke(string? replacedByToken = null)
    {
        if (IsRevoked)
        {
            throw new InvalidOperationException("Token is already revoked.");
        }

        RevokedAt = DateTime.UtcNow;
        ReplacedByToken = replacedByToken;
        UpdateTimeStamp();
    }





}
