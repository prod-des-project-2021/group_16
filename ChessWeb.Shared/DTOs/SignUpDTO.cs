using ChessWeb.Shared.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Shared.DTOs
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Email address field is required")]
        [EmailAddress(ErrorMessage = "Invalid email address was given")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Username field is required")]
        [MaxLength(30, ErrorMessage = "Maximum length of username is 30 characters")]
        [MinLength(2, ErrorMessage = "Minimum length of username is 2 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [Password(ErrorMessage = "At least one number, lowercase, uppercase and special character is required. " +
            "Minimum length is 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
