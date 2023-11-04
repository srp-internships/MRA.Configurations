namespace MRA.Configurations.Common.Interfaces.Services;
/// <summary>
/// Represents a service for sending SMS messages.
/// </summary>
public interface ISmsService
{
    /// <summary>
    /// Sends an SMS message asynchronously to the specified phone number and returns a confirmation code.
    /// </summary>
    /// <param name="phoneNumber">The destination phone number.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result is an integer representing the confirmation code.
    /// </returns>
    Task<bool> SendSmsAsync(string phoneNumber, string text);
}
