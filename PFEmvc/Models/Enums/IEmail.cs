using PFEmvc.dto;
using System.Threading.Tasks;

namespace PFEmvc.Models.Enums
{
    public interface IEmail
    {
        Task Send(string emailAddress, string body, EmailOptionsDTO options);
    }
}

