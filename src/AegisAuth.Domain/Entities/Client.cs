using System;
using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class Client : Entity
{
    public string ClientId { get; private set; }
    public string ClientSecretHash { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    private Client() { }

    public static Client Create(string clientId, string name, string clientSecretHash)
    {
        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new ArgumentException("ClientId cannot be null or empty.", nameof(clientId));
        }

        return new Client
        {
            ClientId = clientId,
            Name = name,
            ClientSecretHash = clientSecretHash,
            IsActive = true
        };
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimeStamp();
    }
}
