using System;
using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class User : Entity
{
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string FullName { get; private set; }
    public string PasswordHash { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }

    public static User Create(string email, string username, string fullName, string passwordHash)
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
            throw new ArgumentException("Full name cannot be null or empty.", nameof(username));
        }

        return new User
        {
            Email = email,
            Username = username,
            FullName = fullName,
            PasswordHash = passwordHash,
            EmailConfirmed = false,
            IsActive = true
        };
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

    public void Deactivate()
    {
        IsActive = false;
        UpdateTimeStamp();
    }


}
