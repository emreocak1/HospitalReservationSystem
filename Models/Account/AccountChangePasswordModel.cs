using System.ComponentModel.DataAnnotations;

namespace itec420.Models;

public class AccountChangePasswordModel
{
    [Required]
    [Display(Name = "Current Password")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; } = null!;


    [Required]
    [Display(Name = "New Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Confirm New Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords must be matched")]
    public string ConfirmPassword { get; set; } = null!;
}