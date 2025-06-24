using Microsoft.AspNetCore.Identity;

namespace itec420.Models;

public class AppUser : IdentityUser<int>
{
    public string Fullname { get; set; } = null!;


}