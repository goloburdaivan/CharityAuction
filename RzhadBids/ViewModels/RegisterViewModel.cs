using System.ComponentModel.DataAnnotations;

namespace RzhadBids.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ім'я має містити від 2 до 50 символів")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a surname.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Прізвище має містити від 2 до 50 символів")]
		[Display(Name = "Прізвище")]
		public string Surname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Адреса електронної пошти не коректна")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} має бути довжиною від {2} до {1} символів", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }
    }
}
