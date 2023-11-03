using System;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class EmailService
{


	

	private readonly string smtpHost = "mail.srvcentral.com";
    private readonly int smtpPort = 587;
    private readonly string smtpUsername = "solution@srvcentral.com";
    private readonly string smtpPassword = "7#530ltkF";

    

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                var message = new MailMessage(smtpUsername, toEmail, subject, body);
                message.IsBodyHtml = true;

                await client.SendMailAsync(message);

                return true;
            }
        }
        catch (Exception ex)
        {
            
            return false;
        }
    }
}
