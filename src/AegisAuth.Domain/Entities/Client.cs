using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class Client : Entity
{
    public Guid TenantId { get; private set; }
    public string ClientId { get; private set; }
    public string ClientSecretHash { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public Tenant Tenant { get; set; } = null!;

    private Client() { }

    public static Client Create(Guid tenantId, string clientId, string name, string clientSecretHash)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
        }

        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new ArgumentException("ClientId cannot be null or empty.", nameof(clientId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(clientSecretHash))
        {
            throw new ArgumentException("Client secret hash cannot be null or empty.", nameof(clientSecretHash));
        }

        return new Client
        {
            TenantId = tenantId,
            ClientId = clientId,
            Name = name,
            ClientSecretHash = clientSecretHash,
            IsActive = true
        };
    }

    public void UpdateDetails(string name, string clientSecretHash)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(clientSecretHash))
        {
            throw new ArgumentException("Client secret hash cannot be null or empty.", nameof(clientSecretHash));
        }

        Name = name;
        ClientSecretHash = clientSecretHash;
        UpdateTimeStamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimeStamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimeStamp();
    }
}
