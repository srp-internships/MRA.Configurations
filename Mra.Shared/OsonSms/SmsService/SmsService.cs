using Microsoft.Extensions.Logging;
using Mra.Shared.Common.Constants;
using Mra.Shared.Common.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace Mra.Shared.OsonSms.SmsService;
public class SmsService : ISmsService
{
    private readonly HttpClient _client;
    private readonly ILogger<SmsService> _logger;

    public SmsService(HttpClient client, ILogger<SmsService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public async Task<int> SendSmsAsync(string phoneNumber)
    {
        int code;
        var config = new Dictionary<string, string>();
        config["dlm"] = ";"; // do not change!!! 
        config["t"] = "23"; // do not change!!!

        config["login"] = ConfigurationKeys.OsonSmsLogin; // Your login
        config["pass_hash"] = ConfigurationKeys.OsonSmsPassHash; // Your hash code
        config["sender"] = ConfigurationKeys.OsonSmsSender; // Your alphanumeric

        var txn_id = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; // Has to be unique for each request

        var str_hash = Sha256Hash(txn_id + config["dlm"] + config["login"] + config["dlm"] + config["sender"] + config["dlm"] + phoneNumber + config["dlm"] + config["pass_hash"]);

        string url = $"https://api.osonsms.com/sendsms_v1.php?login={config["login"]}&from={config["sender"]}&phone_number={phoneNumber}&msg={GenerateMessage(out code)}&txn_id={txn_id}&str_hash={str_hash}";

        try
        {
            await _client.GetAsync(url);
            return code;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on sending sms");
            return -1;
        }
    }

    private static String Sha256Hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    private static String GenerateMessage(out int code)
    {
        Random random = new Random();
        code = random.Next(1000, 10001);
        return $"Your confirmation code is: {code}. Please enter this code to verify your phone number.";
    }
}
