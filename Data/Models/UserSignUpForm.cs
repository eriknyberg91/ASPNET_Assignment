

using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class UserSignUpForm
{
    [Required (ErrorMessage = "Required")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Adress", Prompt = "Enter your email")]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email.")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone number", Prompt = "Enter your phone number")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [Compare("Password", ErrorMessage = "The Password didn't match.")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Terms and conditions", Prompt = "I accept the terms and conditions")]
    public bool TermsAndConditions { get; set; }
}
