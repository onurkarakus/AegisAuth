using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class Tenant : Entity
{
    public string Name { get; private set; } = null!;
    public string Domain { get; private set; } = null!;
    public string? Description { get; private set; }
    public string Email { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public bool EmailConfirmed { get; private set; }

    // Navigation properties
    public ICollection<User> Users { get; set; } = [];
    public ICollection<Client> Clients { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    private Tenant() { }

    public static Tenant Create(string name, string domain, string email, string phoneNumber, string address, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(domain))
        {
            throw new ArgumentException("Domain cannot be null or empty.", nameof(domain));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tenant name cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be null or empty.", nameof(address));
        }

        return new Tenant
        {
            Name = name,
            Domain = domain,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            Description = description,
            IsActive = true,
            EmailConfirmed = false
        };
    }

    public void UpdateDetails(string name, string domain, string email, string phoneNumber, string address, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(domain))
        {
            throw new ArgumentException("Domain cannot be null or empty.", nameof(domain));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tenant name cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be null or empty.", nameof(address));
        }

        Name = name;
        Domain = domain;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Description = description;
        UpdateTimeStamp();
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
        UpdateTimeStamp();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimeStamp();
    }

    public void Activate()
    {
        IsActive = true;
        UpdateTimeStamp();
    }
}
