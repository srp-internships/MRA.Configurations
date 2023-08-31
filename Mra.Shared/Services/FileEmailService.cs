using System.Text.RegularExpressions;
using Newtonsoft.Json;
using IEmailService = Mra.Shared.Common.Interfaces.Services.IEmailService;

namespace Mra.Shared.Services;

public static class SendEmailData
{
    public static IEnumerable<string>? Receivers { get; set; }
    public static string? Body { get; set; }
    public static string? Subject { get; set; }
}

public class FileEmailService : IEmailService
{
    public async Task<bool> SendEmailAsync(IEnumerable<string> receives, string body, string subject)
    {
        SendEmailData.Receivers = receives;
        SendEmailData.Body = body;
        SendEmailData.Subject = subject;

        await using (var file = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                         "MRAEmails", "Sandbox.txt")))
        {
            var jsonLine = JsonConvert.SerializeObject(new
            {
                Receivers = receives.Aggregate("", (s, o) => s + o + " ").Trim(),
                Body = Regex.Replace(body, @"\t|\n|\r", ""),
                Subject = Regex.Replace(subject, @"\t|\n|\r", "")
            });

            await file.WriteLineAsync(jsonLine);
        }

        return true;
    }
}