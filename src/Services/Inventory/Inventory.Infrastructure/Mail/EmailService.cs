using Inventory.Application.Contracts.Infrastructure;
using Inventory.Domain.VO;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Mail
{
    /// <summary>
    /// Email sending service.
    /// No functionality. Only for illustrative purposes to indicate that this type of services are located in the Infrastructure layer
    /// </summary>
    public class EmailService : IEmailService
    {
        public ILogger<EmailService> _logger { get; }

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmail(EmailVO email)
        {
            _logger.LogInformation("Email sent.");
           
            return true;
        }
    }
}
