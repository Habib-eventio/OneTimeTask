namespace CamcoTasks.Infrastructure.Common.Email;

public class EmailConfiguration
{
    public string From { get; set; }

    public string ExceptionEmailTo { get; set; }

    //  When exception arises
    public string ExceptionEmailFrom { get; set; }

    public string GoogleSmtpServer { get; set; }

    public int Port { get; set; }

    public string Password { get; set; }

    //  When exception arises
    public string ExceptionEmailPassword { get; set; }

    public string BaseAddress { get; set; }
}