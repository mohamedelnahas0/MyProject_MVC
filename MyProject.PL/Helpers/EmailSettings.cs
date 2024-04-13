using MyProject.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MyProject.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("muhmedelnhas@gmail.com", "dcdtpfwlztvynzub ");
			Client.Send("muhmedelnhas@gmail.com", email.To, email.Subject, email.body);

		}
	}
}
