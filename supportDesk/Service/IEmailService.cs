using supportDesk.Helper;

namespace supportDesk.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
