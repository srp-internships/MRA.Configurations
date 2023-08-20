using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
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
    /// <param name="builder">dont worry about it</param>
    /// <param name="projectName">name of your project</param>
    public static void ConfigureAzureKeyVault(this WebApplicationBuilder builder,string projectName)
    {
            using var x509Store = new X509Store(StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadOnly);

            var thumbprint = builder.Configuration[ConfigurationKeys.AzureADCertThumbprint];

            if (thumbprint == null) throw new NullReferenceException($"{ConfigurationKeys.AzureADCertThumbprint} can not be null");
            var certificate = x509Store.Certificates
                .Find(
                    X509FindType.FindByThumbprint,
                    thumbprint,
                    validOnly: false)
                .OfType<X509Certificate2>()
                .Single();

            builder.Configuration.AddAzureKeyVault(
                new Uri($"https://{builder.Configuration[ConfigurationKeys.KeyVaultName]}.vault.azure.net/"),
                new ClientCertificateCredential(
                    builder.Configuration[ConfigurationKeys.AzureADDirectoryId],
                    builder.Configuration[ConfigurationKeys.AzureADApplicationId],
                    certificate), new PrefixKeyVaultSecretManager(projectName));
    }
}
