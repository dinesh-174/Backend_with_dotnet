using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}