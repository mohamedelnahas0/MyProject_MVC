using System.ComponentModel.DataAnnotations;

namespace MyProject.PL.ViewModels.User
{
	public class ForgetPasswordViewModel
	{

		[Required(ErrorMessage = "Email is required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

	}
}
