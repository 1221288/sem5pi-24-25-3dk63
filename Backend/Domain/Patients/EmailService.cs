using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DDDSample1.Domain
{
    public class EmailService
    {
        public async Task SendConfirmationEmailAsync(string email, string token)
        {
            // Compose the email content
            var confirmationLink = $"https://localhost:5001/api/confirm-email?token={token}";
            var subject = "Confirm your registration";
            var body = $"Please click on the following link to confirm your registration: {confirmationLink}";

            // Code to send the email (using SMTP or other email service)
            await SendEmailAsync(email, subject, body);
        }

        private async Task SendEmailAsync(string email, string subject, string body)
    {
        try
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential("your-email@gmail.com", "your-email-password");
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Set to true if the body contains HTML content
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the email sending process
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
    }
    }
}
