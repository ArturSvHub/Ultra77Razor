using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Identity.UI.Services;

using MimeKit;

namespace UpakUtilitiesLibrary.Utility.EmailServices
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }
        public async Task Execute(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта upak77.ru", "upak@gkultra.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                client.Connect("mail.hosting.reg.ru", 465, true);
                client.Authenticate("upak@gkultra.ru", "Ultrasite2022.");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
