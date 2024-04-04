using System.ComponentModel.DataAnnotations;

namespace MyProject.PL.ViewModels.User
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is required")]
		[MinLength(5)]
		[MaxLength(15)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool Rememberme { get; set; }
    }
}
