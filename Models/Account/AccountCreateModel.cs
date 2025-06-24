using System.ComponentModel.DataAnnotations;

namespace itec420.Models;

public class AccountCreateModel
{
    [Required]
    [Display(Name = "Fullname")]
    //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only numbers and letters")]
    public string Fullname { get; set; } = null!;

    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords must be matched")]
    public string ConfirmPassword { get; set; } = null!;
}