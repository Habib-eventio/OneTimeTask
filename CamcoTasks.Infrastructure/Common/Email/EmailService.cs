using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.Common.Email;

/// <summary>
/// Basic email service placeholder so the application can run without the
/// original ERP email implementation.
/// </summary>
public class EmailService : IEmailService
{
    public Task SendEmailForExceptionAsync(EmailConfiguration emailConfiguration,
        string emailName, string[] emailEmployeeIds, string emailSubject, string emailBody,
        string emailAttachment, string[] customEmails = null)
    {
        // TODO: integrate actual email sending logic
        return Task.CompletedTask;
    }

    public Task<int> SendEmailAsync(string emailName, string[] emailEmployeeIds, string emailSubject,
        string emailBody, string emailAttachment, string[] customEmails = null)
    {
        // Return 0 to indicate no email was sent.
        return Task.FromResult(0);
    }

    public Task<int> SendEmailForAutomationAsync(int emailTypeId, string emailName, string[] emailEmployeeIds,
        string emailSubject, string emailBody, string emailAttachment, string[] customEmails = null)
    {
        // Automation emails are not implemented in this stub.
        return Task.FromResult(0);
    }
}
