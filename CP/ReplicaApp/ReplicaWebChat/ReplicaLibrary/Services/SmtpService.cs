using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Cryptography;

namespace ReplicaLibrary.Services
{
    public class SmtpService
    {
        private readonly string _senderMailAddress = "no-reply@gmail.com";
        private readonly string _password = "";
        private readonly int _port = 465;
        private readonly string _host = "smtp.gmail.com";

        public DateTime start;
        public int code;

        public async Task SendCodeToMailAsync(string mail)
        {
            if (String.IsNullOrWhiteSpace(mail))
                throw new ArgumentException(
                    "Некорректный адрес получателя: ",
                    nameof(mail)
                    );

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Имя отправителя", _senderMailAddress));
            message.To.Add(new MailboxAddress("Имя получателя", mail.Trim()));

            message.Subject = "Код";

            code = RandomNumberGenerator.GetInt32(100000, 999999);

            start = DateTime.Now;

            var messageBody = new BodyBuilder
            {
                HtmlBody = $"""
                    <html>
                        <body
                            <h2>Код: {code}</h2>
                            <h4>Время действия кода: 5 минут</h4>
                        </body>
                    </html>
                """
            };

            message.Body = messageBody.ToMessageBody();

            try
            {
                using (SmtpClient smtpClient = new())
                {
                    await smtpClient.ConnectAsync(_host, _port);

                    await smtpClient.AuthenticateAsync(_senderMailAddress, _password);

                    await smtpClient.SendAsync(message);

                    await smtpClient.DisconnectAsync(true);
                }
            }

            catch (SmtpCommandException ex)
            {
            }

            catch (SmtpProtocolException ex)
            {
            }

            catch (Exception ex)
            {
            }
        }

        //public void CheckCodeAsync(int checkCode, List<int> activatedCode)
        //{
        //    if (DateTime.Now - start > TimeSpan.FromMinutes(5))
        //    {
        //        messageService.ShowErrorMessage("Код просрочен!");
        //        return;
        //    }
        //    if (activatedCode.Contains(code))
        //        messageService.ShowErrorMessage("Этот код уже введён!");
        //    else
        //        if (checkCode.Equals(code))
        //        {
        //            messageService.ShowSuccessSendMessage("Верный код!");
        //            activatedCode.Add(checkCode);
        //        }
        //        else
        //            messageService.ShowErrorMessage("Неверный код!");
        //}
    }
}
