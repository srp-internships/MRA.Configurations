using Mra.Shared.Common.Interfaces.Services;
using Newtonsoft.Json;

namespace Mra.Shared.Services;

public static class SendSmsData
{
    public static string? PhoneNumber { get; set; }
}

public class FileSmsService : ISmsService
{
    public async Task<int> SendSmsAsync(string phoneNumber)
    {
        SendSmsData.PhoneNumber = phoneNumber;
        int code;

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
                message = GenerateMessage(out code)
            });

            await file.WriteLineAsync(jsonLine);
        }

        return code;
    }
    private static string GenerateMessage(out int code)
    {
        Random random = new Random();
        code = random.Next(1000, 10001);
        return $"Your confirmation code is: {code}. Please enter this code to verify your phone number.";
    }
}
