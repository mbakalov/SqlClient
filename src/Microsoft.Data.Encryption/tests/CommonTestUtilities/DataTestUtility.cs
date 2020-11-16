﻿using Azure.Identity;
using Microsoft.Data.Encryption.AzureKeyVaultProvider;
using Microsoft.Data.Encryption.Cryptography;
using Newtonsoft.Json;
using System.IO;

namespace Microsoft.Data.CommonTestUtilities
{
    public static class DataTestUtility
    {
        private static readonly Config configuration = InitializeConfiguration();

        public static byte[] plaintextEncryptionKeyBytes = { 26, 60, 114, 103, 139, 37, 229, 66, 170, 179, 244, 229, 233, 102, 44, 186, 234, 9, 5, 211, 216, 143, 103, 144, 252, 254, 96, 111, 233, 1, 149, 240 };

        public static readonly string keyEncryptionKeyPath = configuration.AzureKeyVaultKeyPath1;
        public static readonly string keyEncryptionKeyPath2 = configuration.AzureKeyVaultKeyPath2;
        public static readonly string clientId = configuration.AzureClientId;
        public static readonly string clientSecret = configuration.AzureClientSecret;
        public static readonly string tenantId = configuration.AzureTenantId;
        public static readonly string connectionString = configuration.SqlDatabaseConnectionString;
        public static readonly string connectionStringAE = configuration.SqlDatabaseConnectionStringAE;

        public static readonly AzureKeyVaultKeyStoreProvider azureKeyProvider = new AzureKeyVaultKeyStoreProvider(new ClientSecretCredential(tenantId, clientId, clientSecret));
        public static readonly KeyEncryptionKey keyEncryptionKey = new KeyEncryptionKey("CMK", keyEncryptionKeyPath, azureKeyProvider);
        public static readonly byte[] encryptedDataEncryptionKey = { 1, 206, 0, 0, 1, 104, 0, 116, 0, 116, 0, 112, 0, 115, 0, 58, 0, 47, 0, 47, 0, 106, 0, 101, 0, 116, 0, 114, 0, 105, 0, 109, 0, 109, 0, 101, 0, 45, 0, 107, 0, 101, 0, 121, 0, 45, 0, 118, 0, 97, 0, 117, 0, 108, 0, 116, 0, 46, 0, 118, 0, 97, 0, 117, 0, 108, 0, 116, 0, 46, 0, 97, 0, 122, 0, 117, 0, 114, 0, 101, 0, 46, 0, 110, 0, 101, 0, 116, 0, 47, 0, 107, 0, 101, 0, 121, 0, 115, 0, 47, 0, 97, 0, 108, 0, 119, 0, 97, 0, 121, 0, 115, 0, 45, 0, 101, 0, 110, 0, 99, 0, 114, 0, 121, 0, 112, 0, 116, 0, 101, 0, 100, 0, 45, 0, 97, 0, 117, 0, 116, 0, 111, 0, 49, 0, 47, 0, 97, 0, 55, 0, 100, 0, 53, 0, 99, 0, 57, 0, 56, 0, 57, 0, 100, 0, 53, 0, 97, 0, 48, 0, 52, 0, 50, 0, 51, 0, 102, 0, 56, 0, 100, 0, 51, 0, 53, 0, 54, 0, 102, 0, 49, 0, 57, 0, 53, 0, 53, 0, 51, 0, 54, 0, 101, 0, 53, 0, 57, 0, 48, 0, 146, 197, 248, 185, 212, 19, 214, 85, 165, 169, 226, 123, 111, 175, 36, 125, 232, 11, 157, 149, 159, 59, 85, 94, 205, 149, 132, 235, 77, 85, 113, 234, 252, 191, 31, 138, 176, 171, 34, 177, 99, 108, 122, 127, 250, 60, 198, 101, 237, 238, 73, 109, 146, 56, 227, 101, 159, 141, 193, 102, 165, 82, 221, 233, 169, 55, 13, 102, 135, 162, 19, 133, 126, 147, 117, 254, 79, 128, 38, 251, 104, 60, 175, 40, 37, 73, 207, 66, 93, 25, 252, 150, 234, 122, 54, 166, 133, 208, 122, 221, 80, 139, 226, 186, 112, 158, 75, 154, 11, 61, 116, 18, 241, 187, 130, 251, 38, 222, 19, 179, 227, 115, 88, 194, 56, 28, 231, 94, 34, 153, 136, 26, 241, 109, 193, 150, 165, 91, 209, 210, 157, 196, 45, 171, 180, 10, 51, 130, 115, 100, 132, 54, 167, 192, 1, 41, 26, 99, 161, 206, 83, 172, 231, 44, 249, 232, 29, 25, 193, 12, 28, 133, 193, 7, 86, 78, 41, 151, 56, 13, 159, 8, 167, 226, 242, 31, 14, 51, 196, 36, 94, 109, 12, 181, 103, 126, 84, 208, 18, 134, 183, 74, 74, 209, 55, 40, 206, 187, 162, 159, 94, 208, 114, 188, 254, 25, 31, 79, 88, 126, 163, 167, 38, 245, 45, 217, 133, 149, 21, 141, 124, 34, 176, 39, 61, 177, 2, 124, 160, 138, 170, 65, 7, 61, 203, 40, 32, 57, 228, 172, 10, 193, 162, 30, 51, 121, 1, 185, 3, 43, 189, 28, 36, 109, 14, 153, 209, 17, 165, 201, 245, 18, 86, 215, 86, 104, 206, 109, 227, 78, 207, 14, 112, 148, 130, 136, 144, 115, 212, 4, 144, 194, 150, 234, 6, 53, 123, 51, 220, 126, 21, 75, 64, 186, 145, 208, 96, 176, 46, 249, 242, 10, 177, 18, 158, 131, 92, 76, 203, 28, 123, 218, 121, 112, 75, 215, 187, 226, 247, 116, 159, 244, 229, 30, 115, 206, 227, 175, 72, 80, 229, 117, 198, 184, 28, 35, 86, 185, 226, 192, 99, 178, 40, 153, 98, 155, 219, 43, 111, 190, 58, 183, 241, 234, 139, 155, 252, 109, 207, 237, 56, 222, 212, 163, 216, 35, 55, 57, 106, 60, 145, 102, 163, 132, 65, 128, 149, 48, 187, 174, 75, 62, 157, 31, 162, 38, 239, 43, 88, 140, 203, 221, 181, 244, 200, 182, 237, 36, 224, 241, 89, 40, 232, 107, 65, 64, 15, 164, 110, 21, 121, 183, 36, 200, 20, 223, 45, 238, 209, 43, 88, 123, 108, 252, 219, 75, 80, 197, 173, 244, 130, 193, 11, 96, 143, 7, 23, 250, 60, 21, 168, 69, 108, 168, 85, 8, 96, 78, 156, 122, 45, 202, 82, 180, 135, 200, 131, 220, 248, 42, 210, 234, 132, 100, 88, 80, 93, 212, 145, 253, 45, 117, 51, 163, 214, 134, 42, 167, 0, 120, 40, 165, 171, 114, 252, 151, 74, 0, 157, 190, 250, 132, 22, 141, 14, 146, 34, 155, 39, 103, 58, 226 };
        public static readonly ProtectedDataEncryptionKey dataEncryptionKey = ProtectedDataEncryptionKey.GetOrCreate("CEK", keyEncryptionKey, encryptedDataEncryptionKey);

        public static AeadAes256CbcHmac256EncryptionAlgorithm deterministicEncryptionAlgorithm = AeadAes256CbcHmac256EncryptionAlgorithm.GetOrCreate(dataEncryptionKey, EncryptionType.Deterministic);
        public static AeadAes256CbcHmac256EncryptionAlgorithm randomizedEncryptionAlgorithm = AeadAes256CbcHmac256EncryptionAlgorithm.GetOrCreate(dataEncryptionKey, EncryptionType.Randomized);

        static Config InitializeConfiguration()
        {
            using (StreamReader file = File.OpenText("config.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (Config)serializer.Deserialize(file, typeof(Config));
            }
        }

        private class Config
        {
            public string AzureClientId = null;
            public string AzureClientSecret = null;
            public string AzureKeyVaultKeyPath1 = null;
            public string AzureKeyVaultKeyPath2 = null;
            public string AzureTenantId = null;
            public string SqlDatabaseConnectionString = null;
            public string SqlDatabaseConnectionStringAE = null;
        }
    }
}
