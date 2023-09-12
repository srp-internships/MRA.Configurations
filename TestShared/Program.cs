using Microsoft.Extensions.Logging;
using Mra.Shared.OsonSms.SmsService;

var client = new HttpClient();
var logger = new Logger<SmsService>(new LoggerFactory());
var smsService = new SmsService(client, logger);

var response = await smsService.SendSmsAsync("985570302");

Console.WriteLine(response);