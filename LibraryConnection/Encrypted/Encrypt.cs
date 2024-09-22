using System.Security.Cryptography;
using System.Text;

namespace LibraryConnection.Encrypted
{
    public class Encrypt
    {
        private static string EncryptStringAES(string plainText, string password, string saltValue, string hashAlgorithm, int iterations, string initialVector, int keySize)
        {
            try
            {
                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes passwordBytes =
                    new PasswordDeriveBytes(password, saltValueBytes,
                        hashAlgorithm, iterations);

                byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

                using (var aesAlg = Aes.Create())
                {
                    aesAlg.Mode = CipherMode.CBC;

                    // Ensure the AES key size is set correctly
                    aesAlg.Key = keyBytes;
                    aesAlg.IV = initialVectorBytes;

                    // Create a decryptor to perform the stream transform
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                            csEncrypt.FlushFinalBlock();

                            // Convert the encrypted bytes from the MemoryStream to a string
                            byte[] cipherTextBytes = msEncrypt.ToArray();
                            string encryptedText = Convert.ToBase64String(cipherTextBytes);

                            return encryptedText;
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string DecryptStringAES(string encryptedText, string password, string saltValue, string hashAlgorithm, int iterations, string initialVector, int keySize)
        {
            try
            {
                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

                PasswordDeriveBytes passwordBytes =
                    new PasswordDeriveBytes(password, saltValueBytes,
                        hashAlgorithm, iterations);

                byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

                using (var aesAlg = Aes.Create())
                {
                    aesAlg.Mode = CipherMode.CBC;

                    // Ensure the AES key size is set correctly
                    aesAlg.Key = keyBytes;
                    aesAlg.IV = initialVectorBytes;

                    // Create a decryptor to perform the stream transform
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption
                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream and place them in a string
                                string plaintext = srDecrypt.ReadToEnd();

                                return plaintext;
                            }
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string EncryptAES(string strTexto)
        {
            return EncryptStringAES(strTexto, "taskAFP2024-", "taskAFP2024-", "SHA1", 1000, "asdfghjklq123456", 256);
        }

        public static string DecryptAES(string strEncriptado)
        {
            return DecryptStringAES(strEncriptado, "taskAFP2024-", "taskAFP2024-", "SHA1", 1000, "asdfghjklq123456", 256);
        }
    }
}
