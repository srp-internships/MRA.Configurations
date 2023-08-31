using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Mra.Shared.Azure.KeyVault;
using Mra.Shared.Common.Constants;

namespace Mra.Shared.Initializer.Azure.KeyVault;

public static class WebApplicationBuilderAzureExtension
{
    /// <summary>
    /// Method for configuring the Azure Key Vault.
    /// Your configuration will had AzureKeyVault section section will had:
    /// AzureADCertThumbprint
    /// AzureADDirectoryId
    /// KeyVaultName
    /// AzureADApplicationId
    /// </summary>
    /// <param name="configurations">dont worry about it</param>
    /// <param name="projectName">name of your project</param>
    public static void ConfigureAzureKeyVault(this ConfigurationManager configurations, string projectName)
    {
        using var x509Store = new X509Store(StoreLocation.CurrentUser);
        x509Store.Open(OpenFlags.ReadOnly);

        var thumbprint = configurations[ConfigurationKeys.AzureADCertThumbprint];

        if (thumbprint == null)
            throw new NullReferenceException($"{ConfigurationKeys.AzureADCertThumbprint} can not be null");
        var certificate = x509Store.Certificates
            .Find(
                X509FindType.FindByThumbprint,
                thumbprint,
                validOnly: false)
            .OfType<X509Certificate2>()
            .Single();

        configurations.AddAzureKeyVault(
            new Uri($"https://{configurations[ConfigurationKeys.KeyVaultName]}.vault.azure.net/"),
            new ClientCertificateCredential(
                configurations[ConfigurationKeys.AzureADDirectoryId],
                configurations[ConfigurationKeys.AzureADApplicationId],
                certificate), new PrefixKeyVaultSecretManager(projectName));
    }
}