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
            throw new ArgumentNullException($"{ConfigurationKeys.AzureADCertThumbprint} can not be null");
        var certificate = x509Store.Certificates
            .Find(
                X509FindType.FindByThumbprint,
                thumbprint,
                validOnly: false)
            .OfType<X509Certificate2>()
            .FirstOrDefault();


        if (certificate == null)
        {
            using var x509StoreLocal = new X509Store(StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly);
            certificate = x509StoreLocal.Certificates
                           .Find(
                               X509FindType.FindByThumbprint,
                               thumbprint,
                               validOnly: false)
                           .OfType<X509Certificate2>()
                           .FirstOrDefault();
        }

        if (certificate == null)
        {
            var path = Path.Combine(Directory.GetParent(typeof(WebApplicationBuilderAzureExtension).Assembly.Location).FullName, "cert.pfx");

            if (File.Exists(path))
                certificate = new X509Certificate2(File.ReadAllBytes(path));
        }

        if (certificate == null)
            throw new InvalidOperationException("Certificate not found");
        configurations.AddAzureKeyVault(
            new Uri($"https://{configurations[ConfigurationKeys.KeyVaultName]}.vault.azure.net/"),
            new ClientCertificateCredential(
                configurations[ConfigurationKeys.AzureADDirectoryId],
                configurations[ConfigurationKeys.AzureADApplicationId],
                certificate), new PrefixKeyVaultSecretManager(projectName));
    }
}