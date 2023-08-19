using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace Mra.Shared.Azure.KeyVault;

public class PrefixKeyVaultSecretManager : KeyVaultSecretManager
{
    private readonly string jobsProjectName;

    public PrefixKeyVaultSecretManager(string jobsProjectName)
    {
        this.jobsProjectName = jobsProjectName;
    }

    public override string GetKey(KeyVaultSecret secret)
    {
        return secret.Name.StartsWith(jobsProjectName)
            ? secret.Name.Replace($"{jobsProjectName}-", "").Replace("-", ConfigurationPath.KeyDelimiter)
            : secret.Name.Replace("-", ConfigurationPath.KeyDelimiter);
    }
}