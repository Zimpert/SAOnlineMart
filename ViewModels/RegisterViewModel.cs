using System.ComponentModel.DataAnnotations;

namespace SAOnlineMart.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid phone number (10 digits).")]
        public required string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}