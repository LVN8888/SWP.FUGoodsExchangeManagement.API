using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace SWP.FUGoodsExchangeManagement.Business.Service.SecretServices
{
    public static class SecretService
    {
        public static readonly string ConnectionString;

        private const string KeyVaultURI = "https://fugoodsexchangekeyvault.vault.azure.net/";
        private static readonly Uri keyVaultEndPoint;
        private static readonly SecretClient secretClient;

        static SecretService()
        {
            keyVaultEndPoint = new Uri(KeyVaultURI);
            secretClient = new SecretClient(keyVaultEndPoint, new DefaultAzureCredential());

            ConnectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            KeyVaultSecret keyVaultSecret = secretClient.GetSecret("FuGoodsExchangeConnectionString");

            return keyVaultSecret.Value;
        }
    }
}
