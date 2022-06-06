using System.Threading.Tasks;

namespace PFEmvc.Models.Enums
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
