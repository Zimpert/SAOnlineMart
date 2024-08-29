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
        [MinLength(6)]
        public required string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        public required string Role { get; set; }    
    }
}
