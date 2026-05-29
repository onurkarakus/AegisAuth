using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class RefreshToken : Entity
{
    public Guid TenantId { get; private set; }
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? ReplacedByToken { get; private set; }
    public string DeviceInfo { get; private set; }
    public string IpAddress { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsRevoked && !IsExpired;

    // Navigation properties
    public Tenant Tenant { get; set; } = null!;
    public User User { get; set; } = null!;
    public Client Client { get; set; } = null!;

    private RefreshToken() { }

    public static RefreshToken Create(Guid tenantId, string token, Guid userId, Guid clientId, DateTime expiresAt, string deviceInfo, string ipAddress)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));
        }
        if (string.IsNullOrWhiteSpace(deviceInfo))
        {
            throw new ArgumentException("Device info cannot be null or empty.", nameof(deviceInfo));
        }
        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            throw new ArgumentException("IP address cannot be null or empty.", nameof(ipAddress));
        }

        if (string.IsNullOrWhiteSpace(userId.ToString()))
        {
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(clientId.ToString()))
        {
            throw new ArgumentException("Client ID cannot be empty.", nameof(clientId));
        }

        return new RefreshToken
        {
            TenantId = tenantId,
            Token = token,
            UserId = userId,
            ClientId = clientId,
            ExpiresAt = expiresAt,
            DeviceInfo = deviceInfo,
            IpAddress = ipAddress
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
