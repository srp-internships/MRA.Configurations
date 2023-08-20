namespace Mra.Shared.Common.Constants;

public static class ConfigurationKeys
{
    public const string AZURE_EMAIL_SENDER = "AzureEmail:Sender";
    public const string AZURE_EMAIL_CONNECTION = "AzureEmail:ConnectionString";
    
    public const string AzureADCertThumbprint = "AzureKeyVault:AzureADCertThumbprint";
    public const string KeyVaultName = "AzureKeyVault:KeyVaultName";
    public const string AzureADDirectoryId = "AzureKeyVault:AzureADDirectoryId";
    public const string AzureADApplicationId = "AzureKeyVault:AzureADApplicationId";
}