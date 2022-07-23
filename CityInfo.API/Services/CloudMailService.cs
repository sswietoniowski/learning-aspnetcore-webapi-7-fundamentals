using Microsoft.Extensions.Options;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public CloudMailService(IOptions<MailSettingsConfiguration> configurationSection)
        {
            _mailTo = configurationSection.Value.MailTo;
            _mailFrom = configurationSection.Value.MailFrom;
        }

        public void Send(string subject, string message)
        {
            // send mail
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
