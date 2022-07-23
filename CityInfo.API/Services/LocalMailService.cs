namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "admin@demo.com";
        private string _mailFrom = "noreply@demo.com";

        public void Send(string subject, string message)
        {
            // send mail
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
