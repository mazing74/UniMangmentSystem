using System.ComponentModel.DataAnnotations;

namespace UMS.PL.ViewModels
{
    public class ResetPasswordVM
    {

        [Required(ErrorMessage = "Password Is Required"), StringLength(100, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
