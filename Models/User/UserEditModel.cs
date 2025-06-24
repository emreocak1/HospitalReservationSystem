using System.ComponentModel.DataAnnotations;

namespace itec420.Models;

public class UserEditModel
{
    [Required]
    [Display(Name = "Fullname")]
    public string Fullname { get; set; } = null!;

    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = null!;

    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords must be matched")]
    public string? ConfirmPassword { get; set; } = null!;

    public IList<string>? SelectedRoles { get; set; }
}