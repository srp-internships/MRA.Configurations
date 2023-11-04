using System.Text.RegularExpressions;
using Newtonsoft.Json;
using IEmailService = MRA.Configurations.Common.Interfaces.Services.IEmailService;

namespace MRA.Configurations.Services;

public static class SendEmailData
{
    public static IEnumerable<string> Receivers { get; set; }
    public static string Body { get; set; }
    public static string Subject { get; set; }
}

public class FileEmailService : IEmailService
{
    public async Task<bool> SendEmailAsync(IEnumerable<string> receives, string body, string subject)
    {
        SendEmailData.Receivers = receives;
        SendEmailData.Body = body;
        SendEmailData.Subject = subject;
        
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "MRAEmails");
        
        var path = Path.Combine(directory, "Sandbox.txt");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
        await using (var file = File.AppendText(path))
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