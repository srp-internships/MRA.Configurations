using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using MRA.Configurations.Azure.KeyVault;
using MRA.Configurations.Common.Constants;

namespace MRA.Configurations.Initializer.Azure.KeyVault;
/// <summary>
/// Extension methods for configuring Azure Key Vault in web applications.
/// </summary>
public static class WebApplicationBuilderAzureExtension
{
    /// <summary>
    /// Configures Azure Key Vault for the ConfigurationManager.
    /// </summary>
    /// <param name="configurations">The <see cref="ConfigurationManager"/> instance.</param>
    /// <param name="projectName">The name of the project used as a prefix for secret names.</param>
    public static void ConfigureAzureKeyVault(this ConfigurationManager configurations, string projectName)
    {
        Uri keyVaultUri = new Uri($"https://{configurations[ConfigurationKeys.KeyVaultName]}.vault.azure.net/");
        PrefixKeyVaultSecretManager secretManager = new PrefixKeyVaultSecretManager(projectName);

        try
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
                x509StoreLocal.Open(OpenFlags.ReadOnly);
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
            configurations.AddAzureKeyVault(
                keyVaultUri,
                new ClientCertificateCredential(
                    configurations[ConfigurationKeys.AzureADDirectoryId],
                    configurations[ConfigurationKeys.AzureADApplicationId],
                    certificate),
                secretManager);
        }
        catch (Exception)
        {
            var clientSecret = new ClientSecretCredential(
               configurations[ConfigurationKeys.AzureADDirectoryId],
               configurations[ConfigurationKeys.AzureADApplicationId],
               configurations[ConfigurationKeys.AZURE_CLIENT_SECRET_VALUE]);

            configurations.AddAzureKeyVault(keyVaultUri, clientSecret, secretManager);
        }


    }
}