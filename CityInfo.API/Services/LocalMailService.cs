using Microsoft.Extensions.Options;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        // Alternative:
        // public LocalMailService(IConfiguration configuration)
        public LocalMailService(IOptions<MailSettingsConfiguration> configurationSection)
        {
            // Alternative:
            // _mailTo = configuration.GetValue<string>("MailSettings:MailTo");
            // or
            // _mailTo = configuration["MailSettings:MailTo"];
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
