using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments);
        Task SendEmailAsync(IEnumerable<string> to, string subject, string content);
    }
}
