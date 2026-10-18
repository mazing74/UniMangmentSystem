using System.ComponentModel.DataAnnotations;

namespace UMS.PL.ViewModels
{

    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Full Name Is Requierd") , Display(Name = "Full name"), StringLength(100, MinimumLength = 8)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Is Required"), EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required"), StringLength(100, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public bool IsAgree { get; set; }
    }
}
