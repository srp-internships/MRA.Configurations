using Microsoft.Extensions.Logging;
using Mra.Shared.Common.Interfaces.Services;
using Mra.Shared.OsonSms.SmsService;
using Newtonsoft.Json;

namespace Mra.Shared.Services;

public static class SendSmsData
{
    public static string PhoneNumber { get; set; }
}

public class FileSmsService : ISmsService
{
    private readonly ILogger<SmsService> _logger;

    public FileSmsService(ILogger<SmsService> logger)
    {
        _logger = logger;
    }
    public async Task<bool> SendSmsAsync(string phoneNumber, string text)
    {
        try
        {
            SendSmsData.PhoneNumber = phoneNumber;

            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MRAMessages");

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
                    phoneNumber,
                    message=text
                });

                await file.WriteLineAsync(jsonLine);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on writing to file");
            return false;
        }
    }
}
