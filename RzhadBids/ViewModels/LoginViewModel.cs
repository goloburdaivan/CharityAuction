using System.ComponentModel.DataAnnotations;

namespace RzhadBids.ViewModels
{
    public class LoginViewModel
    {
        [Required]
		[EmailAddress(ErrorMessage = "Адреса електронної пошти не коректна")]
		[Display(Name = "Електронна пошта")]
		public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
