using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure.Common.Email;

public interface IEmailService
{
    Task SendEmailForExceptionAsync(EmailConfiguration emailConfiguration,
        string emailName, string[] emailEmployeeIds, string emailSubject, string emailBody,
        string emailAttachment, string[] customEmails = null);

    Task<int> SendEmailAsync(string emailName, string[] emailEmployeeIds, string emailSubject, string emailBody,
        string emailAttachment, string[] customEmails = null);

    Task<int> SendEmailForAutomationAsync(int emailTypeId, string emailName, string[] emailEmployeeIds,
        string emailSubject, string emailBody, string emailAttachment, string[] customEmails = null);
}