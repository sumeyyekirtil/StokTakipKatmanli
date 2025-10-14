using System.Net;
using System.Net.Mail; //MailMessage, SmtpClient

namespace StokTakipKatmanli.WebAPIUsing.Tools
{
	public class MailHelper
	{
		public static void SendMail(string to, string subject, string body) //contactus metot içi tanımlamalarına yönlendirir
		{
			MailMessage mail = new MailMessage();
			SmtpClient smtpClient = new SmtpClient("smtp@gmail.com");
			mail.From = new MailAddress("mail@gmail.com"); //gönderen eposta adresi
			mail.To.Add(to); //alıcı e posta adresi
			mail.Subject = subject; //e posta konusu
			mail.Body = body; //e posta içeriği
			mail.IsBodyHtml = true; //html formatında gönderim
			smtpClient.Port = 587; //smtp portu
			smtpClient.Credentials = new NetworkCredential("mail@gmail.com", "mailşifresi"); //gönderen e-posta ve şifre
			smtpClient.EnableSsl = true; //ssl kullanımı
			smtpClient.Send(mail); //e postayı gönder
		}
	}
}
