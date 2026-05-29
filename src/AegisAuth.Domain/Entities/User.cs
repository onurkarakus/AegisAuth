using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class User : Entity
{
    public Guid TenantId { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string FullName { get; private set; }
    public string PasswordHash { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public Tenant Tenant { get; set; } = null!;

    private User() { }

    public static User Create(Guid tenantId, string email, string username, string fullName, string passwordHash)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name cannot be null or empty.", nameof(fullName));
        }

        return new User
        {
            TenantId = tenantId,
            Email = email,
            Username = username,
            FullName = fullName,
            PasswordHash = passwordHash,
            EmailConfirmed = false,
            IsActive = true
        };
    }

    public void UpdateDetails(string email, string username, string fullName)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name cannot be null or empty.", nameof(fullName));
        }

        Email = email;
        Username = username;
        FullName = fullName;
        UpdateTimeStamp();
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
        UpdateTimeStamp();
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
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
