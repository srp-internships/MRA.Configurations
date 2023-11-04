using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MRA.Configurations.Common.Constants;
using MRA.Configurations.Common.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace MRA.Configurations.OsonSms.SmsService;
public class SmsService : ISmsService
{
    private readonly HttpClient _client;
    private readonly ILogger<SmsService> _logger;
    private readonly IConfiguration _configuration;

    public SmsService(HttpClient client, ILogger<SmsService> logger, IConfiguration configuration)
    {
        _client = client;
        _logger = logger;
        _configuration = configuration;
    }
    public async Task<bool> SendSmsAsync(string phoneNumber, string text)
    {
        var config = new Dictionary<string, string>();
        config["dlm"] = ";"; // do not change!!! 
        config["t"] = "23"; // do not change!!!

        config["login"] = _configuration[ConfigurationKeys.OsonSmsLogin]; // Your login
        config["pass_hash"] = _configuration[ConfigurationKeys.OsonSmsPassHash]; // Your hash code
        config["sender"] = _configuration[ConfigurationKeys.OsonSmsSender]; // Your alphanumeric

        var txn_id = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; // Has to be unique for each request

        var str_hash = Sha256Hash(txn_id + config["dlm"] + config["login"] + config["dlm"] + config["sender"] + config["dlm"] + phoneNumber + config["dlm"] + config["pass_hash"]);

        string url = $"https://api.osonsms.com/sendsms_v1.php?login={config["login"]}&from={config["sender"]}&phone_number={phoneNumber}&msg={text}&txn_id={txn_id}&str_hash={str_hash}";

        try
        {
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode) 
                return true;
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on sending sms");
            return false;
        }
    }

    private static String Sha256Hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }
        return Sb.ToString();
    }
}
