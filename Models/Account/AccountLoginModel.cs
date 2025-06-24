using System.ComponentModel.DataAnnotations;

namespace itec420.Models;

public class AccountLoginModel
{
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; } = true;
}