using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MimeKit;

using UpakModelsLibrary.Models.DTOModels;

namespace Ultra77Razor.Pages
{
    public class ContactsModel : PageModel
    {

        [BindProperty]
        public EmailModel? EmailModel { get; set; }
		public void OnGet()
        {
        }
        public void OnPost()
		{
            EmailModel.To = "esp66@bk.ru";
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(EmailModel.From, "upak@gkultra.ru"));
            emailMessage.To.Add(new MailboxAddress("", EmailModel.To));
            emailMessage.Subject = EmailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<ul><h4>Содержание формы обратной связи</h4><li>Имя клиента: {EmailModel.FromTitle}, </li><li>Номер телефона: {EmailModel.FromPhone}, </li><li>Email: {EmailModel.From}, </li><li>Тема: {EmailModel.Subject}, </li><li>Обращение: {EmailModel.EmailTextBody}</li></ul>"            
            };

            using (var client = new SmtpClient())
            {
                client.Connect("mail.hosting.reg.ru", 465, true);
                client.Authenticate("upak@gkultra.ru", "Ultrasite2022.");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
