using Mra.Shared.Services;

var foo = new FileSmsService();

var code = await foo.SendSmsAsync("985570302");
Console.WriteLine(code);