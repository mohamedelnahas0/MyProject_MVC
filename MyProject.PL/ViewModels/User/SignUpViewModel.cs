using System.ComponentModel.DataAnnotations;

namespace MyProject.PL.ViewModels.User
{
	public class SignUpViewModel
	{

        [Required(ErrorMessage = "Fname is required")]
        public string Fname { get; set; }

		[Required(ErrorMessage = "Lname is required")]
		public string Lname { get; set; }



		[Required(ErrorMessage = "Email is required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is required")]
		[MinLength(5)]
		[MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[MinLength(5)]
		[MaxLength(15)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage ="Not Match Password")]
		public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }



    }
}
