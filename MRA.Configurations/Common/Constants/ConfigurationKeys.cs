namespace MRA.Configurations.Common.Constants;

public static class ConfigurationKeys
{
    public const string AZURE_EMAIL_SENDER = "AzureEmail:Sender";
    public const string AZURE_EMAIL_CONNECTION = "AzureEmail:ConnectionString";

    public const string AZURE_CLIENT_SECRET_VALUE = "AzureClientSecretValue";

    public const string AzureADCertThumbprint = "AzureKeyVault:AzureADCertThumbprint";
    public const string KeyVaultName = "AzureKeyVault:KeyVaultName";
    public const string AzureADDirectoryId = "AzureKeyVault:AzureADDirectoryId";
    public const string AzureADApplicationId = "AzureKeyVault:AzureADApplicationId";

    public const string OsonSmsLogin = "OsonSms:Login";
    public const string OsonSmsPassHash = "OsonSms:PassHash";
    public const string OsonSmsSender = "OsonSms:Sender";
}