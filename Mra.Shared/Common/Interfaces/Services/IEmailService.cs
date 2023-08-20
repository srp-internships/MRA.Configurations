namespace Mra.Shared.Common.Interfaces.Services;

public interface IEmailService
{
    /// <summary>
    ///  Method for sending email message
    /// Supporting html and text contents
    /// </summary>
    /// <param name="receives">List of receives</param>
    /// <param name="body">message body(text or html)</param>
    /// <param name="subject">subject of message</param>
    /// <returns>sending status(bool)</returns>
    public Task<bool> SendEmailAsync(IEnumerable<string> receives, string body, string subject);
}